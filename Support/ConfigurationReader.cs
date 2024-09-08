using Microsoft.Extensions.Configuration;
using TechTalk.SpecFlow.Configuration;

namespace SauceDemoSamuel.Support
{
    public static class ConfigurationReader
    {
        // Holds the configuration object
        private static IConfiguration Configuration { get; set; }

        // Static constructor to initialize the configuration
        static ConfigurationReader()
        {
            // Build the configuration from the "AppSettings.json" file
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("AppSettings.json")  // Load settings from the specified JSON file
                .Build();  // Build the configuration
        }

        // Property to get the current environment setting
        public static string CurrentEnvironment => Configuration["CurrentEnvironment"];

        // Property to get the configuration section for the current environment
        public static IConfigurationSection EnvironmentDetails =>
            Configuration.GetSection("Environments").GetSection(CurrentEnvironment);

        // Property to get the SauceDemo URL for the current environment
        public static string SauceDemoURL => EnvironmentDetails["SauceDemoURL"];

        // Property to get the standard user username for the current environment
        public static string StandardUserUsername => EnvironmentDetails["StandardUserUsername"];

        // Property to get the standard user password for the current environment
        public static string StandardUserPassword => EnvironmentDetails["StandardUserPassword"];

        // Property to get the locked user username for the current environment
        public static string LockedUserUsername => EnvironmentDetails["lockedUserUsername"];

        // Property to get the locked user password for the current environment
        public static string LockedUserPassword => EnvironmentDetails["lockedUserPassword"];
    }
}