using Borderlands2GoldenKeys.Models;
using Elmah;
using Raven.Client;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Borderlands2GoldenKeys.Helpers
{
    public class RavenDBErrorMailModule : ErrorMailModule
    {
        protected override object GetConfig()
        {
            using (IDocumentSession documentSession = DependencyResolver.Current.GetService<IDocumentSession>())
            {
                var settings = documentSession.Load<Settings>(Settings.UniqueId);
                if (settings != null)
                {
                    var mailSettings = settings.Mail;
                    if (mailSettings != null && mailSettings.IsComplete)
                    {
                        return new Dictionary<string, string> 
                                    {
                                        { "to", mailSettings.DestinationMail },
                                        { "from", mailSettings.DestinationMail },
                                        {"cc", string.Empty },
                                        {"subject", string.Empty },
                                        { "priority", "High" },
                                        { "async", bool.FalseString}, // => for the use of Fiddler
                                        { "smtpServer", mailSettings.SmtpHost },
                                        { "smtpPort", mailSettings.SmtpPort.ToString() },
                                        { "userName", mailSettings.DestinationMail },
                                        { "password", mailSettings.Password },
                                        { "noYsod", bool.FalseString },
                                        { "useSsl", mailSettings.UseSsl.ToString() }
                                    };
                    }
                }
                return base.GetConfig();
            }
        }
    }
}