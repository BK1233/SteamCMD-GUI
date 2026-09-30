using System;
using System.Diagnostics;

namespace SteamCMD_GUI
{
    public class ServerManager
    {
        public event Action<string> ServerOutputReceived;
        public event Action ServerExited;

        private Process ServerProcess;

        public void StartServer(string path, string arguments)
        {
            if (ServerProcess != null && !ServerProcess.HasExited)
            {
                return;
            }

            ServerProcess = new Process();
            ServerProcess.StartInfo.FileName = path;
            ServerProcess.StartInfo.Arguments = arguments;
            ServerProcess.StartInfo.UseShellExecute = false;
            ServerProcess.StartInfo.RedirectStandardOutput = true;
            ServerProcess.StartInfo.RedirectStandardError = true;
            ServerProcess.StartInfo.CreateNoWindow = true;

            ServerProcess.EnableRaisingEvents = true;

            ServerProcess.OutputDataReceived += ServerProcess_OutputDataReceived;
            ServerProcess.ErrorDataReceived += ServerProcess_ErrorDataReceived;
            ServerProcess.Exited += ServerProcess_Exited;

            ServerProcess.Start();
            ServerProcess.BeginOutputReadLine();
            ServerProcess.BeginErrorReadLine();
        }

        public void StopServer()
        {
            if (ServerProcess != null && !ServerProcess.HasExited)
            {
                ServerProcess.Kill();
            }
        }

        private void ServerProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                ServerOutputReceived?.Invoke(e.Data);
            }
        }

        private void ServerProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                ServerOutputReceived?.Invoke($"ERROR: {e.Data}");
            }
        }

        private void ServerProcess_Exited(object sender, EventArgs e)
        {
            ServerExited?.Invoke();
            ServerProcess = null;
        }
    }
}
