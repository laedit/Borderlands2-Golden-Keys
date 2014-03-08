using System.ComponentModel.DataAnnotations;

namespace Borderlands2GoldendKeys.Models
{
    public class TwitterSettings
    {
        [Required, Display(Name="API key")]
        public string APIKey { get; set; }

        [Required, Display(Name = "API secret")]
        public string APISecret { get; set; }

        [Required, Display(Name="Access token")]
        public string AccessToken { get; set; }

        [Required, Display(Name="Access token secret")]
        public string AccessTokenSecret { get; set; }
    }
}