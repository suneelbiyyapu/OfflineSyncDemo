using OfflineSyncDemo.Contracts.Services.General;
using Xamarin.Essentials;

namespace OfflineSyncDemo.Services.General
{
    public class SettingsService : ISettingsService
    {
        // private readonly ISettings _settings;
        private const string UserName = "UserName";
        private const string UserId = "UserId";

        public SettingsService()
        {
            //_settings = CrossSettings.Current;
        }

        public void AddItem(string key, string value)
        {
            Preferences.Set(key, value);
        }

        public string GetItem(string key)
        {
            return Preferences.Get(key, string.Empty);
        }

        public string UserNameSetting
        {
            get => GetItem(UserName);
            set => AddItem(UserName, value);
        }

        public string UserIdSetting
        {
            get => GetItem(UserId);
            set => AddItem(UserId, value);
        }
    }
}
