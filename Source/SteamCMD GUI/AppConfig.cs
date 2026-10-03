using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
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
        private static PreferencesConfig _cachedPreferences;
        private static readonly object _lock = new object();
        private static CancellationTokenSource _debounceCts;

        public static PreferencesConfig Load(bool forceReload = false)
        {
            lock (_lock)
            {
                if (_cachedPreferences != null && !forceReload)
                {
                    return _cachedPreferences;
                }

                if (!File.Exists(ConfigFile))
                {
                    var defaults = new PreferencesConfig();
                    SaveInternal(defaults);
                    _cachedPreferences = defaults;
                    return defaults;
                }

                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(ConfigFile, optional: false, reloadOnChange: true);

                var config = builder.Build();
                var prefs = new PreferencesConfig();
                config.GetSection("Preferences").Bind(prefs);
                _cachedPreferences = prefs;
                return prefs;
            }
        }

        public static void Save(PreferencesConfig preferences)
        {
            lock (_lock)
            {
                _cachedPreferences = preferences;
                SaveInternal(preferences);
            }
        }

        public static async Task SaveAsync(PreferencesConfig preferences)
        {
            PreferencesConfig prefsCopy;
            lock (_lock)
            {
                _cachedPreferences = preferences;
                prefsCopy = preferences;
            }

            await Task.Run(() => SaveInternal(prefsCopy)).ConfigureAwait(false);
        }

        public static void SaveDebounced(PreferencesConfig preferences, int delayMilliseconds = 500)
        {
            lock (_lock)
            {
                _cachedPreferences = preferences;
                _debounceCts?.Cancel();
                _debounceCts = new CancellationTokenSource();
                var token = _debounceCts.Token;
                var prefsCopy = preferences;

                Task.Delay(delayMilliseconds, token).ContinueWith(task =>
                {
                    if (task.IsCompletedSuccessfully && !token.IsCancellationRequested)
                    {
                        SaveInternal(prefsCopy);
                    }
                }, TaskScheduler.Default);
            }
        }

        public static void FlushDebouncedSave()
        {
            lock (_lock)
            {
                if (_debounceCts != null)
                {
                    _debounceCts.Cancel();
                    _debounceCts = null;
                }
                if (_cachedPreferences != null)
                {
                    SaveInternal(_cachedPreferences);
                }
            }
        }

        private static void SaveInternal(PreferencesConfig preferences)
        {
            var root = new { Preferences = preferences };
            string json = JsonSerializer.Serialize(root, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigFile, json);
        }
    }
}
