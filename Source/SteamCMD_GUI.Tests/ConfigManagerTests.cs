using System;
using System.IO;
using SteamCMD_GUI;
using Xunit;

namespace SteamCMD_GUI.Tests
{
    public class ConfigManagerTests
    {
        private sealed class TempDirectoryContext : IDisposable
        {
            public string DirectoryPath { get; }
            private readonly string _originalDirectory;

            public TempDirectoryContext()
            {
                _originalDirectory = Directory.GetCurrentDirectory();
                DirectoryPath = Path.Combine(Path.GetTempPath(), "ConfigManagerTests_" + Guid.NewGuid().ToString("N"));
                Directory.CreateDirectory(DirectoryPath);
                Directory.SetCurrentDirectory(DirectoryPath);
            }

            public void Dispose()
            {
                Directory.SetCurrentDirectory(_originalDirectory);
                if (Directory.Exists(DirectoryPath))
                {
                    try
                    {
                        Directory.Delete(DirectoryPath, true);
                    }
                    catch
                    {
                        // Best effort cleanup
                    }
                }
            }
        }

        [Fact]
        public void Load_WhenConfigFileMissing_CreatesDefaultConfigFileAndReturnsDefaults()
        {
            using (new TempDirectoryContext())
            {
                // Ensure appsettings.json does not exist initially
                string configFile = "appsettings.json";
                Assert.False(File.Exists(configFile));

                // Act
                PreferencesConfig prefs = ConfigManager.Load();

                // Assert
                Assert.NotNull(prefs);
                Assert.Equal("steamcmd.exe", prefs.SteamCmdPath);
                Assert.Equal("", prefs.InstallDirectory);
                Assert.Equal("", prefs.SrcdsDirectory);

                // Verify file was created on disk
                Assert.True(File.Exists(configFile));

                // Verify file content structure
                string content = File.ReadAllText(configFile);
                Assert.Contains("Preferences", content);
                Assert.Contains("steamcmd.exe", content);
            }
        }

        [Fact]
        public void Load_WhenConfigFileExists_LoadsPreferencesFromConfigFile()
        {
            using (new TempDirectoryContext())
            {
                var customPreferences = new PreferencesConfig
                {
                    SteamCmdPath = @"D:\steamcmd\steamcmd.exe",
                    InstallDirectory = @"D:\servers\tf2",
                    SrcdsDirectory = @"D:\servers\tf2\srcds.exe"
                };
                ConfigManager.Save(customPreferences);

                // Act
                PreferencesConfig loaded = ConfigManager.Load();

                // Assert
                Assert.NotNull(loaded);
                Assert.Equal(@"D:\steamcmd\steamcmd.exe", loaded.SteamCmdPath);
                Assert.Equal(@"D:\servers\tf2", loaded.InstallDirectory);
                Assert.Equal(@"D:\servers\tf2\srcds.exe", loaded.SrcdsDirectory);
            }
        }

        [Fact]
        public void Save_WritesPreferencesToConfigFile()
        {
            using (new TempDirectoryContext())
            {
                string configFile = "appsettings.json";
                Assert.False(File.Exists(configFile));

                var prefsToSave = new PreferencesConfig
                {
                    SteamCmdPath = @"C:\Games\steamcmd.exe",
                    InstallDirectory = @"C:\Games\Servers",
                    SrcdsDirectory = @"C:\Games\Servers\srcds.exe"
                };

                // Act
                ConfigManager.Save(prefsToSave);

                // Assert
                Assert.True(File.Exists(configFile));
                string jsonContent = File.ReadAllText(configFile);
                Assert.Contains("SteamCmdPath", jsonContent);

                PreferencesConfig reloaded = ConfigManager.Load();
                Assert.Equal(prefsToSave.SteamCmdPath, reloaded.SteamCmdPath);
                Assert.Equal(prefsToSave.InstallDirectory, reloaded.InstallDirectory);
                Assert.Equal(prefsToSave.SrcdsDirectory, reloaded.SrcdsDirectory);
            }
        }
    }
}
