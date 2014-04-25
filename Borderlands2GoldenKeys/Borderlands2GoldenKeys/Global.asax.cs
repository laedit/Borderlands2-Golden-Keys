using Borderlands2GoldenKeys.Helpers;
using Borderlands2GoldenKeys.Models;
using Raven.Client;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Borderlands2GoldenKeys
{
    public class MvcApplication : System.Web.HttpApplication
    {
        internal static string BaseUrl { get; private set; }

        internal static bool IsTraceEnabled { get; set; }

        protected void Application_Start()
        {
            //MvcHandler.DisableMvcResponseHeader = true;

            AreaRegistration.RegisterAllAreas();
            UnityConfig.RegisterComponents();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Application_Initialization();
        }

        private void Application_Initialization()
        {
            using (IDocumentSession documentSession = DependencyResolver.Current.GetService<IDocumentSession>())
            {
                var settings = documentSession.Load<Settings>(Settings.UniqueId);

                if (settings == null)
                {
                    settings = new Settings();
                    settings.Twitter = new TwitterSettings();

                    documentSession.Store(settings);
                    documentSession.SaveChanges();
                }

                // Store twitter accounts to parse
                if (settings.Twitter.SourceAccounts.Count == 0)
                {
                    settings.Twitter.SourceAccounts.Add(new SourceAccount { Name = "GearboxSoftware", IsInitialized = documentSession.Query<ShiftCode>().Any() });
                    settings.Twitter.SourceAccounts.Add(new SourceAccount { Name = "Borderlands", IsInitialized = false });

                    documentSession.Store(settings);
                    documentSession.SaveChanges();
                }

                // Store some ClapTrap's quotes if needed
                if (!documentSession.Query<ClapTrapQuote>().Any())
                {
                    ClapTrapQuote.GetBaseQuotes().ForEach(q => documentSession.Store(q));
                    documentSession.SaveChanges();
                }

                IsTraceEnabled = settings.IsTraceEnabled;

                if (settings.Twitter != null)
                {
                    Trace("{0} / {1} / {2}", settings.Twitter.IsComplete, settings.Twitter.APIKey, settings.Twitter.APISecret);
                }

                var updateProcess = DependencyResolver.Current.GetService<ShiftCodeUpdateProcess>();

                if (!updateProcess.IsRunning && settings.Twitter != null && settings.Twitter.IsComplete)
                {
                    Trace("Start update process");
                    updateProcess.Start();
                }
            }
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(BaseUrl))
            {
                BaseUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                string path = HttpContext.Current.Request.Url.AbsolutePath;
                if (!string.IsNullOrEmpty(path))
                {
                    if (path == "/")
                    {
                        BaseUrl = BaseUrl.Remove(BaseUrl.Length - 1);
                    }
                    else
                    {
                        BaseUrl = BaseUrl.Replace(path, string.Empty);
                    }
                }
            }

            if (HttpContext.Current.Request.RawUrl.EndsWith("/Settings/RestardUpdateProcess"))
            {
                if (IsTraceEnabled)
                {
                    Trace("RestardUpdateProcess: Request");
                }
                var updateProcess = DependencyResolver.Current.GetService<ShiftCodeUpdateProcess>();
                if (!updateProcess.IsRunning)
                {
                    if (IsTraceEnabled)
                    {
                        Trace("RestardUpdateProcess: Start");
                    }
                    updateProcess.Start();
                }
            }
        }

        internal static void Trace(string message, params object[] args)
        {
            if (IsTraceEnabled)
            {
                Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(new Exception(string.Format(message, args))));
            }
        }

        internal static void Log(string message, params object[] args)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(new InvalidOperationException(string.Format(message, args)));
        }

        internal static void Log(Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }
}
