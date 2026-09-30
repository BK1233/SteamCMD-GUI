using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SteamCMD_GUI
{
    public class UpdateManager
    {
        public event Action<bool, string> UpdateCheckCompleted;
        public event Action<string> UpdateCompleted;
        public event Action<string> ErrorOccurred;
        public event Action<string> StreamOutput;

        private readonly string _steamCmdPath;

        public UpdateManager(string steamCmdPath)
        {
            _steamCmdPath = steamCmdPath;
        }

        public async void CheckForUpdate(string appId, string installDir)
        {
            string arguments = $"+login anonymous +force_install_dir \"{installDir}\" +app_update {appId} validate +quit";
            await RunSteamCmd(arguments, (string output) =>
            {
                bool updateAvailable = output.Contains("Success!");
                UpdateCheckCompleted?.Invoke(updateAvailable, appId);
            });
        }

        public async void UpdateServer(string appId, string installDir)
        {
            string arguments = $"+login anonymous +force_install_dir \"{installDir}\" +app_update {appId} +quit";
            await RunSteamCmd(arguments, (string output) => UpdateCompleted?.Invoke(appId));
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

                    StringBuilder outputBuilder = new StringBuilder();
                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (e.Data != null)
                        {
                            outputBuilder.AppendLine(e.Data);
                            StreamOutput?.Invoke(e.Data);
                        }
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    await process.WaitForExitAsync();

                    outputHandler?.Invoke(outputBuilder.ToString());
                }
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke("SteamCMD execution failed: " + ex.Message);
            }
        }
    }
}
