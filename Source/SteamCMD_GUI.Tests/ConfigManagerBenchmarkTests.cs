using System.Diagnostics;
using System.IO;
using Xunit;
using Xunit.Abstractions;

namespace SteamCMD_GUI.Tests
{
    public class ConfigManagerBenchmarkTests
    {
        private readonly ITestOutputHelper _output;

        public ConfigManagerBenchmarkTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void BenchmarkConfigLoadAndSave()
        {
            const int iterations = 100;

            // Measure un-cached / uncached load + debounced save
            var sw = Stopwatch.StartNew();

            for (int i = 0; i < iterations; i++)
            {
                var prefs = ConfigManager.Load();
                prefs.SteamCmdPath = $"C:\\steamcmd_{i}.exe";
                ConfigManager.SaveDebounced(prefs, 100);
            }

            sw.Stop();
            _output.WriteLine($"Time taken for {iterations} cached Load and SaveDebounced operations: {sw.ElapsedMilliseconds} ms");

            // Flush debounced save to disk
            ConfigManager.FlushDebouncedSave();

            // Verify final value is persisted
            var finalPrefs = ConfigManager.Load(forceReload: true);
            Assert.Equal($"C:\\steamcmd_{iterations - 1}.exe", finalPrefs.SteamCmdPath);

            // Clean up created appsettings.json if present
            if (File.Exists("appsettings.json"))
            {
                try { File.Delete("appsettings.json"); } catch { }
            }
        }
    }
}
