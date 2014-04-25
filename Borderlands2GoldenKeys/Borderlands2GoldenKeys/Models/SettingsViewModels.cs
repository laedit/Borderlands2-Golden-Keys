using Raven.Imports.Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Borderlands2GoldenKeys.Models
{
    public class SettingsViewModel
    {
        public Settings Settings { get; set; }
        public bool DisableFillDatabaseButton { get; set; }
        public bool DisableLaunchUpdateProcessButton { get; set; }
        public bool DisableDeleteAllButton { get; set; }
        public bool UpdateRunning { get; set; }
    }

    public class Settings
    {
        public const string UniqueId = "5AE2947A-0C12-49ED-AAB4-AA3345B0FA6A";

        public string Id { get; set; }

        public TwitterSettings Twitter { get; set; }

        public MailSettings Mail { get; set; }

        public ReCaptchaSettings ReCaptcha { get; set; }

        [Display(Name="Enable trace")]
        public bool IsTraceEnabled { get; set; }

        public void UpdateFrom(Settings settings)
        {
            Twitter.APIKey = settings.Twitter.APIKey;
            Twitter.APISecret= settings.Twitter.APISecret;
            Mail = settings.Mail;
            ReCaptcha = settings.ReCaptcha;
            IsTraceEnabled = settings.IsTraceEnabled;
        }
    }

    public class TwitterSettings
    {
        [Display(Name = "API key")]
        public string APIKey { get; set; }

        [Display(Name = "API secret")]
        public string APISecret { get; set; }

        public List<SourceAccount> SourceAccounts { get; set; }

        [JsonIgnore]
        public bool IsComplete
        {
            get { return !string.IsNullOrEmpty(APIKey) && !string.IsNullOrEmpty(APISecret) && SourceAccounts.Count > 0; }
        }

        public TwitterSettings()
        {
            SourceAccounts = new List<SourceAccount>();
        }
    }

    public class SourceAccount
    {
        public string Name { get; set; }
        public bool IsInitialized { get; set; }
    }

    public class MailSettings
    {
        [Display(Name = "SMTP host")]
        public string SmtpHost { get; set; }

        [Display(Name = "SMTP port")]
        public int SmtpPort { get; set; }

        [Display(Name = "Use SSL")]
        public bool UseSsl { get; set; }

        [Display(Name = "Destination mail address")]
        public string DestinationMail { get; set; }

        public string Password { get; set; }

        [JsonIgnore]
        public bool IsComplete
        {
            get { return !string.IsNullOrEmpty(SmtpHost) && !string.IsNullOrEmpty(DestinationMail) && !string.IsNullOrEmpty(Password); }
        }
    }

    public class ReCaptchaSettings
    {
        [Display(Name = "Public key")]
        public string PublicKey { get; set; }

        [Display(Name = "Private key")]
        public string PrivateKey { get; set; }
    }
}