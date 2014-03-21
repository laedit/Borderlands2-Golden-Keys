using Borderlands2GoldendKeys.Models;
using PoliteCaptcha;
using Raven.Client;

namespace Borderlands2GoldendKeys.Helpers
{
    public class RavenReCaptchaConfigurationSource : IConfigurationSource
    {
        private IDocumentSession _documentSession;

        public RavenReCaptchaConfigurationSource(IDocumentSession documentSession)
        {
            _documentSession = documentSession;
        }

        public string GetConfigurationValue(string key)
        {
            string value = null;
            var settings = _documentSession.Load<Settings>(Settings.UniqueId);
            if(key == "reCAPTCHA::PublicKey")
            {
                value = settings.ReCaptcha.PublicKey;
            }
            else if(key == "reCAPTCHA::PrivateKey")
            {
                value = settings.ReCaptcha.PrivateKey;
            }
            return value;
        }
    }
}