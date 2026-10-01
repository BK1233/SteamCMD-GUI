using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;

namespace SteamCMD_GUI
{
    public class UpdateManager
    {
        private string _steamCmdPath;

        public event Action<bool, string> UpdateCheckCompleted;
        public event Action<string> UpdateCompleted;
        public event Action<string> OutputReceived;
        public event Action<string> ErrorOccurred;

        public UpdateManager(string steamCmdPath)
        {
            _steamCmdPath = steamCmdPath;
        }

        public void UpdateServer(string appId, string installDir)
        {
            UpdateServerWithArgs($"+login anonymous +force_install_dir \"{installDir}\" +app_update {appId} validate +quit", appId);
        }


        public void UpdateServerWithArgs(string args, string contextId = "Custom")
        {
            Task.Run(() =>
            {
                try
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = _steamCmdPath,
                        Arguments = args,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    };

                    Process process = new Process { StartInfo = psi };
                    process.OutputDataReceived += (s, e) => {
                        if (e.Data != null) OutputReceived?.Invoke(e.Data);
                    };
                    process.ErrorDataReceived += (s, e) => {
                        if (e.Data != null) ErrorOccurred?.Invoke($"Error: {e.Data}");
                    };
                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    process.WaitForExit();
                    UpdateCompleted?.Invoke(contextId);
                }
                catch (Exception ex)
                {
                    ErrorOccurred?.Invoke($"Failed to start SteamCMD: {ex.Message}");
                }
            });
        }

        public void SetSteamCmdPath(string path)
        {
            _steamCmdPath = path;
        }

        public void CheckForUpdates()
        {
            Task.Delay(500).ContinueWith(_ => UpdateCheckCompleted?.Invoke(false, "GUI"));
        }
    }
}
