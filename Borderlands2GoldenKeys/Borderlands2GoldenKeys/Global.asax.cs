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
        public static string BaseUrl { get; private set; }

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
                // Store some ClapTrap's quotes if needed
                if (!documentSession.Query<ClapTrapQuote>().Any())
                {
                    ClapTrapQuote.GetBaseQuotes().ForEach(q => documentSession.Store(q));
                    documentSession.SaveChanges();
                }

                var updateProcess = DependencyResolver.Current.GetService<ShiftCodeUpdateProcess>();
                var settings = documentSession.Load<Settings>(Settings.UniqueId);
                Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(new Exception((settings.Twitter != null) + " / " + settings.Twitter.IsComplete + " / " + settings.Twitter.APIKey + " / " + settings.Twitter.APISecret)));
                if (!updateProcess.IsRunning && settings != null && settings.Twitter != null && settings.Twitter.IsComplete)
                {
                    Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(new Exception("Start update process")));
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
                Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(new Exception("RestardUpdateProcess: Request")));
                var updateProcess = DependencyResolver.Current.GetService<ShiftCodeUpdateProcess>();
                if (!updateProcess.IsRunning)
                {
                    Elmah.ErrorLog.GetDefault(null).Log(new Elmah.Error(new Exception("RestardUpdateProcess: Start")));
                    updateProcess.Start();
                }
            }
        }
    }
}
