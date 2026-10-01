using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace SteamCMD_GUI
{
    public class PreferencesConfig
    {
        public string SteamCmdPath { get; set; } = "steamcmd.exe";
        public string InstallDirectory { get; set; } = "";
        public string SrcdsDirectory { get; set; } = "";
    }

    public static class ConfigManager
    {
        private const string ConfigFile = "appsettings.json";

        public static PreferencesConfig Load()
        {
            if (!File.Exists(ConfigFile))
            {
                var defaults = new PreferencesConfig();
                Save(defaults);
                return defaults;
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(ConfigFile, optional: false, reloadOnChange: true);

            var config = builder.Build();
            var prefs = new PreferencesConfig();
            config.GetSection("Preferences").Bind(prefs);
            return prefs;
        }

        public static void Save(PreferencesConfig preferences)
        {
            var root = new { Preferences = preferences };
            string json = JsonSerializer.Serialize(root, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigFile, json);
        }
    }
}
