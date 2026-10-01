using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using CoreRCON;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SteamCMD_GUI
{
    public class ServerProfile
    {
        public string Name { get; set; }
        public string Game { get; set; }
        public string Map { get; set; }
        public string Network { get; set; }
        public int MaxPlayers { get; set; }
        public int UdpPort { get; set; }
        public string RconPassword { get; set; }
        public bool Secure { get; set; }
        public bool DebugMode { get; set; }
        public bool SourceTV { get; set; }
        public bool ConsoleMode { get; set; }
        public bool Insecure { get; set; }
        public bool DisableBots { get; set; }
        public bool DevMessages { get; set; }
        public string CustomMod { get; set; }
    }

    public partial class MainMenu : Form
    {

        private void btnConsoleSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtConsoleInput.Text))
            {
                _serverManager.SendCommand(txtConsoleInput.Text);
                txtConsoleInput.Clear();
            }
        }
        private ServerManager _serverManager = new ServerManager();
        private UpdateManager _updateManager = new UpdateManager("steamcmd.exe");
        private BackupManager _backupManager = new BackupManager();
        private RCON rconClient;
        private string _loadedConfigPath = "";
        private List<ServerProfile> _profiles = new List<ServerProfile>();
        private List<string> _customGames = new List<string>();

        public MainMenu()
        {
            InitializeComponent();

            _serverManager.ServerOutputReceived += msg => AppendOutputText(msg);
            _serverManager.ServerExited += () => UpdateStatus("Server process exited");

            _updateManager.UpdateCheckCompleted += (hasUpdate, appId) => UpdateStatus(hasUpdate ? $"Update available for AppID {appId}" : $"AppID {appId} is up to date");
            _updateManager.UpdateCompleted += appId => UpdateStatus($"Update completed for AppID {appId}");
            _updateManager.ErrorOccurred += msg => UpdateStatus(msg, true);

            _backupManager.BackupCreated += msg => UpdateStatus($"Backup created: {msg}");
            _backupManager.BackupRestored += msg => UpdateStatus($"Backup restored: {msg}");
            _backupManager.ErrorOccurred += msg => UpdateStatus(msg, true);

            LoadProfiles();
            LoadCustomGames();

            btnSourceMod.Click += (s, e) => Process.Start(new ProcessStartInfo("http://www.sourcemod.net") { UseShellExecute = true });
            btnMetamod.Click += (s, e) => Process.Start(new ProcessStartInfo("http://www.metamodsource.net") { UseShellExecute = true });
            btnEventScripts.Click += (s, e) => Process.Start(new ProcessStartInfo("http://www.eventscripts.com") { UseShellExecute = true });
            btnValveWiki.Click += (s, e) => Process.Start(new ProcessStartInfo("https://developer.valvesoftware.com/wiki/Main_Page") { UseShellExecute = true });
            btnCheckUpdates.Click += (s, e) => {
                UpdateStatus("Checking for updates...");
                _updateManager.CheckForUpdates();
            };

            btnAddProfile.Click += BtnAddProfile_Click;
            btnSaveProfile.Click += BtnSaveProfile_Click;

            btnAddCustom.Click += (s, e) => {
                string input = Microsoft.VisualBasic.Interaction.InputBox("Enter Custom App ID:", "Add Custom Game", "");
                if (!string.IsNullOrWhiteSpace(input) && !cmbGameToInstall.Items.Contains(input)) {
                    cmbGameToInstall.Items.Add(input);
                    cmbGameToInstall.Text = input;
                    _customGames.Add(input);
                    SaveCustomGames();
                }
            };
        }

        private void cmbProfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            var p = _profiles.Find(x => x.Name == cmbProfiles.Text);
            if (p != null)
            {
                cmbGameToRun.Text = p.Game;
                cmbMap.Text = p.Map;
                cmbNetwork.Text = p.Network;
                numMaxPlayers.Value = p.MaxPlayers;
                numUdpPort.Value = p.UdpPort;
                txtRcon.Text = p.RconPassword;
                chkSecure.Checked = p.Secure;
                chkDebugMode.Checked = p.DebugMode;
                chkSourceTV.Checked = p.SourceTV;
                chkConsoleMode.Checked = p.ConsoleMode;
                chkInsecure.Checked = p.Insecure;
                chkDisableBots.Checked = p.DisableBots;
                chkDevMessages.Checked = p.DevMessages;
                txtCustomMod.Text = p.CustomMod;
                UpdateStatus($"Profile {p.Name} loaded.");
            }
        }

        private void LoadCustomGames()
        {
            if (File.Exists("custom_games.xml"))
            {
                try {
                    using (var sr = new StreamReader("custom_games.xml"))
                    {
                        var xs = new XmlSerializer(typeof(List<string>));
                        _customGames = (List<string>)xs.Deserialize(sr);
                        foreach(var g in _customGames) {
                            if (!cmbGameToInstall.Items.Contains(g)) cmbGameToInstall.Items.Add(g);
                        }
                    }
                } catch { }
            }
        }

        private void SaveCustomGames()
        {
            try {
                using (var sw = new StreamWriter("custom_games.xml"))
                {
                    var xs = new XmlSerializer(typeof(List<string>));
                    xs.Serialize(sw, _customGames);
                }
            } catch { }
        }

        private void BtnAddProfile_Click(object sender, EventArgs e)
        {
            cmbProfiles.Text = "New Server Profile";
            cmbGameToRun.Text = "";
            cmbMap.Text = "";
            UpdateStatus("Ready to create a new profile. Fill in fields and click Save.");
        }

        private void BtnSaveProfile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cmbProfiles.Text)) return;
            var p = new ServerProfile
            {
                Name = cmbProfiles.Text,
                Game = cmbGameToRun.Text,
                Map = cmbMap.Text,
                Network = cmbNetwork.Text,
                MaxPlayers = (int)numMaxPlayers.Value,
                UdpPort = (int)numUdpPort.Value,
                RconPassword = txtRcon.Text,
                Secure = chkSecure.Checked,
                DebugMode = chkDebugMode.Checked,
                SourceTV = chkSourceTV.Checked,
                ConsoleMode = chkConsoleMode.Checked,
                Insecure = chkInsecure.Checked,
                DisableBots = chkDisableBots.Checked,
                DevMessages = chkDevMessages.Checked,
                CustomMod = txtCustomMod.Text
            };

            _profiles.RemoveAll(x => x.Name == p.Name);
            _profiles.Add(p);
            SaveProfiles();

            if (!cmbProfiles.Items.Contains(p.Name)) cmbProfiles.Items.Add(p.Name);
            UpdateStatus($"Profile {p.Name} saved.");
        }

        private void SaveProfiles()
        {
            try {
                using (var sw = new StreamWriter("profiles.xml"))
                {
                    var xs = new XmlSerializer(typeof(List<ServerProfile>));
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
                        var xs = new XmlSerializer(typeof(List<ServerProfile>));
                        _profiles = (List<ServerProfile>)xs.Deserialize(sr);
                        cmbProfiles.Items.Clear();
                        foreach(var p in _profiles) cmbProfiles.Items.Add(p.Name);
                    }
                } catch { }
            }
        }

        private async void btnDownloadSteamCMD_Click(object sender, EventArgs e)
        {
            try {
                UpdateStatus("Starting SteamCMD download...");
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetAsync("https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip");
                    using (var fs = new FileStream("steamcmd.zip", FileMode.Create))
                    {
                        await response.Content.CopyToAsync(fs);
                    }
                }
                UpdateStatus("Download complete. Extract steamcmd.zip");
            } catch (Exception ex) {
                UpdateStatus("Download failed: " + ex.Message, true);
            }
        }

        private void btnUpdateServer_Click(object sender, EventArgs e)
        {


            string gameName = cmbGameToInstall.Text;
            string appId = "740"; // default
            if (gameName == "Counter-Strike: Global Offensive") appId = "740";
            else if (gameName == "Team Fortress 2") appId = "232250";
            else if (gameName == "Garry's Mod") appId = "4020";
            else if (gameName == "Half-Life Deathmatch: Source") appId = "255470";
            else if (gameName == "Left 4 Dead 2") appId = "222860";
            else appId = gameName; // In case it's a custom numeric appID

            _updateManager.SetSteamCmdPath(txtSteamCmdPath.Text);
            UpdateStatus($"Updating server (AppId: {appId}) to {txtInstallDir.Text}...");



            string loginArg = chkLoginAnonymous.Checked ? "+login anonymous" : $"+login {txtLogin.Text} {txtPassword.Text}";
            string validateArg = chkValidate.Checked ? " validate" : "";

            string args = $"{loginArg} +force_install_dir \"{txtInstallDir.Text}\" +app_update {appId}{validateArg} +quit";

            _updateManager.UpdateServerWithArgs(args, appId);
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            string exe = Path.Combine(txtSrcdsPath.Text, "srcds.exe");
            string args = $"-console -game {cmbGameToRun.Text} +maxplayers {numMaxPlayers.Value} +map {cmbMap.Text} -port {numUdpPort.Value} +rcon_password \"{txtRcon.Text}\"";

            if (chkInsecure.Checked) args += " -insecure";
            if (chkDisableBots.Checked) args += " -nobots";
            if (chkDebugMode.Checked) args += " -debug";
            if (chkConsoleMode.Checked) args += " -console";
            if (chkSourceTV.Checked) args += " +tv_enable 1";
            if (chkDevMessages.Checked) args += " -dev";

            if (File.Exists("commands.txt")) args += " " + File.ReadAllText("commands.txt").Trim();

            if (File.Exists(exe))
            {
                _serverManager.StartServer(exe, args);
                UpdateStatus($"Starting server with args: {args}");
            } else {
                UpdateStatus($"Error: Server executable not found at: {exe}", true);
            }
        }

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

        private void btnBrowseSteamCmd_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Executable files (*.exe)|*.exe";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtSteamCmdPath.Text = ofd.FileName;
                }
            }
        }

        private void btnBrowseServerPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtInstallDir.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnBrowseSrcds_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtSrcdsPath.Text = fbd.SelectedPath;
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutWindow().ShowDialog();
        }

        private void commandsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CommandsWindow().ShowDialog();
        }

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
