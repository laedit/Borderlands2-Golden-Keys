
namespace Borderlands2GoldendKeys.Models
{
    public class SettingsViewModel
    {
        public TwitterSettings TwitterSettings { get; set; }
        public bool DisableFillDatabaseButton { get; set; }
        public bool DisableLaunchUpdateProcessButton { get; set; }
        public bool DisableDeleteAllButton { get; set; }
    }

    public class SettingsMessage
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}