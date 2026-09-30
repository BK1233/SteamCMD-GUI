using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text;
using System.Net;
using System.Net.Http;
using CoreRCON;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace SteamCMD_GUI
{
    public class Profile
    {
        public string Name { get; set; }
        public string AppId { get; set; }
        public string InstallDir { get; set; }
        public string ModId { get; set; }
        public string RconIp { get; set; }
        public string RconPort { get; set; }
        public string RconPassword { get; set; }
        public bool AutoValidate { get; set; }
        public string BackupSource { get; set; }
        public string BackupDest { get; set; }
    }

    public partial class SteamCMDMainMenu : Form
    {
        private string steamCmdPath = "steamcmd.exe";
        private RCON rconClient;
        private BackupManager _backupManager = new BackupManager();
        private ServerManager _serverManager = new ServerManager();
        private WorkshopManager _workshopManager = new WorkshopManager("steamcmd.exe");
        private UpdateManager _updateManager = new UpdateManager("steamcmd.exe");
        private List<SteamCMD_GUI.Profile> _profiles = new List<SteamCMD_GUI.Profile>();
        private string _loadedConfigPath = "";


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutWindow().ShowDialog();
        }

        private void commandsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CommandsWindow().ShowDialog();
        }

        public SteamCMDMainMenu()
        {
            InitializeComponent();

            // Wire up event handlers for managers
            _backupManager.BackupCreated += msg => UpdateStatus($"Backup created: {msg}");
            _backupManager.BackupRestored += msg => UpdateStatus($"Backup restored: {msg}");
            _backupManager.ErrorOccurred += msg => UpdateStatus(msg, true);

            _serverManager.ServerOutputReceived += msg => AppendOutputText(msg);
            _serverManager.ServerExited += () => UpdateStatus("Server process exited");

            _updateManager.UpdateCheckCompleted += (hasUpdate, appId) => UpdateStatus(hasUpdate ? $"Update available for AppID {appId}" : $"AppID {appId} is up to date");
            _updateManager.UpdateCompleted += appId => UpdateStatus($"Update completed for AppID {appId}");
            _updateManager.ErrorOccurred += msg => UpdateStatus(msg, true);

            _workshopManager.WorkshopSearchCompleted += results =>
            {
                Invoke(new Action(() =>
                {
                    lstWorkshopResults.Items.Clear();
                    foreach (var r in results) lstWorkshopResults.Items.Add(r);
                }));
            };
            _workshopManager.WorkshopInstallCompleted += modId => UpdateStatus($"Workshop Mod {modId} Installed");
            _workshopManager.ErrorOccurred += msg => UpdateStatus(msg, true);

            LoadProfiles();
        }

        // Run Tab
        private async void btnDownloadSteamCMD_Click(object sender, EventArgs e)
        {
            try {
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync("https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip");
                    using (var fs = new FileStream("steamcmd.zip", FileMode.CreateNew))
                    {
                        await response.Content.CopyToAsync(fs);
                    }
                }
                UpdateStatus("Download complete. Extract steamcmd.zip");
            } catch (Exception ex) {
                UpdateStatus("Download failed: " + ex.Message, true);
            }
        }

        // Update Tab
        private void btnUpdateServer_Click(object sender, EventArgs e)
        {
            UpdateStatus($"Updating server (AppId: {txtAppId.Text}) to {txtInstallDir.Text}...");
            _updateManager.UpdateServer(txtAppId.Text, txtInstallDir.Text);
        }

        private void btnInstallMod_Click(object sender, EventArgs e)
        {
            UpdateStatus($"Installing mod {txtModId.Text}...");
            _workshopManager.Install(txtAppId.Text, txtModId.Text);
        }

        // RCON Tab
        private async void btnRconConnect_Click(object sender, EventArgs e)
        {
            try
            {
                rconClient = new RCON(IPAddress.Parse(txtRconIp.Text), ushort.Parse(txtRconPort.Text), txtRconPassword.Text);
                await rconClient.ConnectAsync();
                UpdateStatus("RCON Connected.");
            }
            catch (Exception ex)
            {
                UpdateStatus($"RCON Error: {ex.Message}", true);
            }
        }

        private async void btnRconSend_Click(object sender, EventArgs e)
        {
            if (rconClient != null)
            {
                try {
                    string response = await rconClient.SendCommandAsync(txtRconCommand.Text);
                    AppendOutputText(response);
                } catch (Exception ex) {
                    UpdateStatus($"RCON Error: {ex.Message}", true);
                }
            } else {
                UpdateStatus("Not connected to RCON", true);
            }
        }

        // Backup / Restore Tab
        private async void btnCreateBackup_Click(object sender, EventArgs e)
        {
            UpdateStatus("Creating backup...");
            await Task.Run(() => _backupManager.CreateBackup(txtBackupSource.Text, txtBackupDest.Text));
        }

        private async void btnRestoreBackup_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "ZIP archives (*.zip)|*.zip";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    UpdateStatus("Restoring backup...");
                    await Task.Run(() => _backupManager.RestoreBackup(ofd.FileName, txtBackupSource.Text));
                }
            }
        }

        // Config Editor Tab
        private void btnOpenConfig_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try {
                        txtConfigArea.Text = File.ReadAllText(ofd.FileName);
                        _loadedConfigPath = ofd.FileName;
                        UpdateStatus("Loaded config: " + ofd.FileName);
                    } catch (Exception ex) {
                        UpdateStatus("Error reading config: " + ex.Message, true);
                    }
                }
            }
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_loadedConfigPath))
            {
                try {
                    File.WriteAllText(_loadedConfigPath, txtConfigArea.Text);
                    UpdateStatus("Config saved.");
                } catch (Exception ex) {
                    UpdateStatus("Error saving config: " + ex.Message, true);
                }
            } else {
                UpdateStatus("No config file loaded", true);
            }
        }

        // Workshop Manager Tab
        private void btnSearchWorkshop_Click(object sender, EventArgs e)
        {
            lstWorkshopResults.Items.Clear();
            _workshopManager.Search(txtAppId.Text, txtWorkshopSearch.Text);
            UpdateStatus("Searching workshop...");
        }

        // Server Manager Tab
        private void btnStartServer_Click(object sender, EventArgs e)
        {
            string exe = Path.Combine(txtInstallDir.Text, "srcds.exe");
            if (File.Exists(exe))
            {
                _serverManager.StartServer(exe, "-console -game cstrike");
                UpdateStatus("Starting server...");
            } else {
                UpdateStatus("Server executable not found at: " + exe, true);
            }
        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            _serverManager.StopServer();
            UpdateStatus("Stopped server.");
        }

        // Profiles Tab
        private void btnSaveProfile_Click(object sender, EventArgs e)
        {
            var p = new SteamCMD_GUI.Profile {
                Name = cmbProfiles.Text,
                AppId = txtAppId.Text,
                InstallDir = txtInstallDir.Text,
                ModId = txtModId.Text,
                RconIp = txtRconIp.Text,
                RconPort = txtRconPort.Text,
                RconPassword = txtRconPassword.Text,
                AutoValidate = chkAutoValidate.Checked,
                BackupSource = txtBackupSource.Text,
                BackupDest = txtBackupDest.Text
            };
            var existing = _profiles.Find(x => x.Name == p.Name);
            if (existing != null) {
                existing.AppId = p.AppId;
                existing.InstallDir = p.InstallDir;
                existing.ModId = p.ModId;
                existing.RconIp = p.RconIp;
                existing.RconPort = p.RconPort;
                existing.RconPassword = p.RconPassword;
                existing.AutoValidate = p.AutoValidate;
                existing.BackupSource = p.BackupSource;
                existing.BackupDest = p.BackupDest;
            } else {
                _profiles.Add(p);
                cmbProfiles.Items.Add(p.Name);
            }
            SaveProfiles();
            UpdateStatus("Profile saved.");
        }

        private void btnLoadProfile_Click(object sender, EventArgs e)
        {
            var p = _profiles.Find(x => x.Name == cmbProfiles.Text);
            if (p != null)
            {
                txtAppId.Text = p.AppId;
                txtInstallDir.Text = p.InstallDir;
                txtModId.Text = p.ModId;
                txtRconIp.Text = p.RconIp;
                txtRconPort.Text = p.RconPort;
                txtRconPassword.Text = p.RconPassword;
                chkAutoValidate.Checked = p.AutoValidate;
                txtBackupSource.Text = p.BackupSource;
                txtBackupDest.Text = p.BackupDest;
                UpdateStatus("Profile loaded.");
            } else {
                UpdateStatus("Profile not found.", true);
            }
        }

        private void SaveProfiles()
        {
            try {
                using (var sw = new StreamWriter("profiles.xml"))
                {
                    var xs = new XmlSerializer(typeof(List<SteamCMD_GUI.Profile>));
                    xs.Serialize(sw, _profiles);
                }
            } catch { }
        }

        private void LoadProfiles()
        {
            if (File.Exists("profiles.xml"))
            {
                try {
                    using (var sr = new StreamReader("profiles.xml"))
                    {
                        var xs = new XmlSerializer(typeof(List<SteamCMD_GUI.Profile>));
                        _profiles = (List<SteamCMD_GUI.Profile>)xs.Deserialize(sr);
                        foreach (var p in _profiles) cmbProfiles.Items.Add(p.Name);
                    }
                } catch { }
            }
        }

        // Console Tab
        private void btnLogSearch_Click(object sender, EventArgs e)
        {
            int index = ConsoleOutput.Text.IndexOf(txtLogSearch.Text, StringComparison.OrdinalIgnoreCase);
            if (index != -1)
            {
                ConsoleOutput.Select(index, txtLogSearch.Text.Length);
                ConsoleOutput.Focus();
            } else {
                UpdateStatus("Search text not found.", true);
            }
        }

        // UI Utils
        public void UpdateStatus(string text, bool isError = false)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string, bool>(UpdateStatus), text, isError);
                return;
            }

            Color color = isError ? Color.Red : Color.Black;
            AppendOutputText(text, color);
            Status.Text = $"[{DateTime.Now:HH:mm:ss}] {text}";
            Status.BackColor = isError ? Color.FromArgb(240, 200, 200) : Color.FromArgb(240, 240, 240);
        }

        private void AppendOutputText(string text, Color? color = null)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string, Color?>(AppendOutputText), text, color);
                return;
            }

            ConsoleOutput.SelectionStart = ConsoleOutput.TextLength;
            ConsoleOutput.SelectionLength = 0;
            ConsoleOutput.SelectionColor = color ?? Color.Black;
            ConsoleOutput.AppendText(text + Environment.NewLine);
            ConsoleOutput.SelectionColor = ConsoleOutput.ForeColor;
            ConsoleOutput.ScrollToCaret();
        }
    }
}
