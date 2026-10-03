using System;
using System.IO;
using SteamCMD_GUI;
using Xunit;

namespace SteamCMD_GUI.Tests
{
    public class MapManagerTests
    {
        [Theory]
        [InlineData("Counter-Strike: Source", false, "", "cstrike")]
        [InlineData("Team Fortress 2", false, "", "tf")]
        [InlineData("Garry's Mod", false, "", "garrysmod")]
        [InlineData("Half-Life 2: Deathmatch", false, "", "hl2mp")]
        [InlineData("Left 4 Dead", false, "", "left4dead")]
        [InlineData("Left 4 Dead 2", false, "", "left4dead2")]
        [InlineData("Day of Defeat: Source", false, "", "dod")]
        [InlineData("Alien Swarm", false, "", "swarm")]
        [InlineData("Counter-Strike: Global Offensive", false, "", "csgo")]
        [InlineData("Team Fortress 2 Classic", false, "", "tf2classic")]
        [InlineData("Unknown Game", false, "", "Unknown Game")]
        [InlineData("", false, "", "cstrike")]
        [InlineData("Team Fortress 2", true, "custom_mod", "custom_mod")]
        public void GetGameMod_ReturnsExpectedMod(string selectedGame, bool isCustom, string customText, string expected)
        {
            string mod = MapManager.GetGameMod(selectedGame, isCustom, customText);
            Assert.Equal(expected, mod);
        }

        [Fact]
        [Trait("Category", "MapManager")]
        public void GetAvailableMaps_ReturnsSortedBspFileNames()
        {
            string tempDir = Path.Combine(Path.GetTempPath(), "MapManagerTests_" + Guid.NewGuid().ToString("N"));
            string mapsDir = Path.Combine(tempDir, "tf", "maps");

            try
            {
                Directory.CreateDirectory(mapsDir);
                File.WriteAllText(Path.Combine(mapsDir, "2_cp_badlands.bsp"), "");
                File.WriteAllText(Path.Combine(mapsDir, "1_2fort.bsp"), "");
                File.WriteAllText(Path.Combine(mapsDir, "readme.txt"), "");

                var maps = MapManager.GetAvailableMaps(tempDir, "tf");

                Assert.Equal(2, maps.Count);
                Assert.Equal("1_2fort", maps[0]);
                Assert.Equal("2_cp_badlands", maps[1]);
            }
            finally
            {
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
        }

        [Fact]
        [Trait("Category", "MapManager")]
        public void GetAvailableMaps_NonExistentDirectory_ReturnsEmptyList()
        {
            string tempDir = Path.Combine(Path.GetTempPath(), "NonExistent_" + Guid.NewGuid().ToString("N"));
            var maps = MapManager.GetAvailableMaps(tempDir, "tf");
            Assert.Empty(maps);
        }

        [Fact]
        [Trait("Category", "MapManager")]
        public void GetAvailableMaps_NullOrEmptyInputs_ReturnsEmptyList()
        {
            Assert.Empty(MapManager.GetAvailableMaps("", "tf"));
            Assert.Empty(MapManager.GetAvailableMaps("C:\\SomePath", ""));
        }
    }
}
