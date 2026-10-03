using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SteamCMD_GUI
{
    public static class MapManager
    {
#nullable enable
        public static string GetGameMod(string selectedGame, bool isCustomMod = false, string? customModText = null)
        {
            if (isCustomMod && !string.IsNullOrWhiteSpace(customModText))
            {
                return customModText.Trim();
            }

            return selectedGame switch
            {
                "Team Fortress 2" => "tf",
                "Garry's Mod" => "garrysmod",
                "Half-Life 2: Deathmatch" => "hl2mp",
                "Left 4 Dead" => "left4dead",
                "Left 4 Dead 2" => "left4dead2",
                "Day of Defeat: Source" => "dod",
                "Alien Swarm" => "swarm",
                "Counter-Strike: Global Offensive" => "csgo",
                "Team Fortress 2 Classic" => "tf2classic",
                "Counter-Strike: Source" => "cstrike",
                _ => string.IsNullOrWhiteSpace(selectedGame) ? "cstrike" : selectedGame
            };
        }

        public static List<string> GetAvailableMaps(string srcdsPath, string gameMod)
        {
            var maps = new List<string>();
            if (string.IsNullOrWhiteSpace(srcdsPath) || string.IsNullOrWhiteSpace(gameMod))
            {
                return maps;
            }

            string mapsDirectory = Path.Combine(srcdsPath, gameMod, "maps");
            if (Directory.Exists(mapsDirectory))
            {
                try
                {
                    string[] bspFiles = Directory.GetFiles(mapsDirectory, "*.bsp");
                    foreach (string file in bspFiles)
                    {
                        string mapName = Path.GetFileNameWithoutExtension(file);
                        if (!string.IsNullOrWhiteSpace(mapName))
                        {
                            maps.Add(mapName);
                        }
                    }
                    maps.Sort(StringComparer.OrdinalIgnoreCase);
                }
                catch
                {
                    // Ignore directory access errors
                }
            }

            return maps;
        }
    }
}
