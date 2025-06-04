using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;


namespace BusinessLogic.AppSettings
{
    public class ConfigHelper
    {
        private static readonly IConfigurationRoot Configuration = GetCurrentSettings();
        private static readonly Dictionary<string, string> AppSettings = new();
        public static string EmailTemplatePathForms => Path.Combine(Environment.CurrentDirectory, "wwwroot", "_emailTemplateEnvironment.cshtml");


        public static string Settings(string parent, string key)
        {

            return AppSettings.TryGetValue(key, out var setting) ? setting : GetKeyFromAppSettingJson(parent, key);

        }

        private static string GetKeyFromAppSettingJson(string parent, string key)
        {
            var value = Configuration.GetSection(parent).GetValue<string>(key);
            if (!AppSettings.ContainsKey(key) && (value != null))
                AppSettings.Add(key, value);
            return value ?? string.Empty;
        }

        private static IConfigurationRoot GetCurrentSettings()
        {
            var builder = new ConfigurationBuilder()
             .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false,
                    reloadOnChange: true)
                .AddEnvironmentVariables();
            return builder.Build();
        }
    }
}
