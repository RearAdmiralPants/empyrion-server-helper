namespace EmpyrionManager.Helpers
{
    using System.Configuration;
    public static class AppSettingRetriever
    {
        public static string GetAppSetting(string settingName)
        {
            var reader = new AppSettingsReader();
            return (string)reader.GetValue(settingName, typeof(string));
        }
    }
}
