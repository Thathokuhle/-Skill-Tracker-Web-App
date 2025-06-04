using Microsoft.Extensions.Configuration;

namespace BusinessLogic.AppSettings
{
    public static class AppSettings
    {


        private static readonly IConfigurationRoot Configuration = GetCurrentSettings();
        private static readonly Dictionary<string, string> ConfigSettings = new();

        //public static readonly string EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;
        public static readonly string Salt = Environment.GetEnvironmentVariable("Salt") ?? string.Empty;
        public static readonly string AdminUserHash = Environment.GetEnvironmentVariable("AdminUserHash") ?? string.Empty;
        public static readonly string BaseUrl = Settings("AppSettings", "BaseUrl");
        public static readonly string ReportTitle = "ReportTitle";
        public static readonly string DbConnectionString = Environment.GetEnvironmentVariable("SkillTrackerAppConnectionString") ?? string.Empty;
        public static readonly string GoogleClientId = Settings("AppSettings", "GoogleClientId");
        public static readonly string GoogleSecret = Settings("AppSettings", "GoogleSecret");
        public static readonly string GoopleMapsApi = Settings("AppSettings", "GoogleMapsApi");
        public static readonly string MVCSecret = Settings("AppSettings", "MVCSecret");
        public static readonly string UploadPath = Settings("UploadSettings", "UploadPath");


        public static string Settings(string parent, string key)
        {
            return ConfigSettings.ContainsKey(key) ? ConfigSettings[key] : GetKeyFromAppSettingJson(parent, key);
        }

        private static string GetKeyFromAppSettingJson(string parent, string key)
        {
            var value = Configuration.GetSection(parent).GetValue<string>(key) ?? string.Empty;
            if (!ConfigSettings.ContainsKey(key))
                ConfigSettings.Add(key, value);
            return value;
        }

        private static IConfigurationRoot GetCurrentSettings()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false,
                    reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        public static string GetJWTTokenKey()
        {
            var secret = Environment.GetEnvironmentVariable("JWTTokenKey");
            if (string.IsNullOrEmpty(secret))
            {
                throw new Exception("Cannot read the JWTToken key !");
            }
            return secret;
        }

        public static string GetUrLEncryptionKey()
        {
            var secret = Settings("AppSettings", "MVCSecret");
            if (string.IsNullOrEmpty(secret))
            {
                throw new Exception("Cannot read the URL encryption key !");
            }
            return secret;
        }

    }
}
