using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace SteamCMD_GUI
{
    public class WorkshopManager
    {
        public event Action<List<string>> WorkshopSearchCompleted;
        public event Action<string> WorkshopInstallCompleted;
        public event Action<string> ErrorOccurred;
        public event Action<string> StreamOutput;

        private readonly string _steamCmdPath;

        public WorkshopManager(string steamCmdPath)
        {
            _steamCmdPath = steamCmdPath;
        }

        public async void Search(string appId, string query)
        {
            string arguments = $"+login anonymous +workshop_search \"{query}\" +quit";
            await RunSteamCmd(arguments, HandleSearchOutput);
        }

        public async void Install(string appId, string modId)
        {
            string arguments = $"+login anonymous +workshop_download_item {appId} {modId} +quit";
            await RunSteamCmd(arguments, (string output) => WorkshopInstallCompleted?.Invoke(modId));
        }

        private async Task RunSteamCmd(string arguments, Action<string> outputHandler)
        {
            try
            {
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = _steamCmdPath;
                    process.StartInfo.Arguments = arguments;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.CreateNoWindow = true;

                    string output = "";
                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (e.Data != null)
                        {
                            output += e.Data + Environment.NewLine;
                            StreamOutput?.Invoke(e.Data);
                        }
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    await process.WaitForExitAsync();

                    outputHandler?.Invoke(output);
                }
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke("SteamCMD execution failed: " + ex.Message);
            }
        }

        private void HandleSearchOutput(string output)
        {
            List<string> results = new List<string>();
            using (StringReader reader = new StringReader(output))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    if (line.Contains("workshop_item"))
                    {
                        results.Add(line);
                    }
                    line = reader.ReadLine();
                }
            }
            WorkshopSearchCompleted?.Invoke(results);
        }
    }
}
