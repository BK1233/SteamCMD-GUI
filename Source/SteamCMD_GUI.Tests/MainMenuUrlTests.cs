using System;
using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace SteamCMD_GUI.Tests
{
    public class MainMenuUrlTests
    {
        [Fact]
        public void MainMenu_DoesNotContainInsecureHttpUrls()
        {
            string baseDir = AppContext.BaseDirectory;
            string solutionDir = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", ".."));
            string mainMenuFile = Path.Combine(solutionDir, "SteamCMD GUI", "MainMenu.cs");

            Assert.True(File.Exists(mainMenuFile), $"MainMenu.cs not found at path: {mainMenuFile}");

            string content = File.ReadAllText(mainMenuFile);

            var httpMatches = Regex.Matches(content, @"http://[^\s""']+", RegexOptions.IgnoreCase);

            Assert.Empty(httpMatches);
        }

        [Fact]
        public void MainMenu_ContainsSecureHttpsUrlsForExternalLinks()
        {
            string baseDir = AppContext.BaseDirectory;
            string solutionDir = Path.GetFullPath(Path.Combine(baseDir, "..", "..", "..", ".."));
            string mainMenuFile = Path.Combine(solutionDir, "SteamCMD GUI", "MainMenu.cs");

            Assert.True(File.Exists(mainMenuFile), $"MainMenu.cs not found at path: {mainMenuFile}");

            string content = File.ReadAllText(mainMenuFile);

            Assert.Contains("https://www.sourcemod.net", content);
            Assert.Contains("https://www.metamodsource.net", content);
            Assert.Contains("https://www.eventscripts.com", content);
        }
    }
}
