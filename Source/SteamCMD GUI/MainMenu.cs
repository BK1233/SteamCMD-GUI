using System.IO.Compression;
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
    public partial class MainMenu : Form
    {
        private void ApplyLocalization()
        {
            var rm = new System.Resources.ResourceManager("SteamCMD_GUI.Resources.Strings", typeof(MainMenu).Assembly);
            try
            {
                UpdateTab.Text = rm.GetString("Tab_UpdateInstall") ?? "Update/Install";
                RunServerTab.Text = rm.GetString("Tab_RunServer") ?? "Run Server";
                ConsoleTab.Text = rm.GetString("Tab_Console") ?? "Console";
                RconTab.Text = rm.GetString("Tab_Rcon") ?? "RCON";
                ConfigEditorTab.Text = rm.GetString("Tab_ConfigEditor") ?? "Config Editor";
                BackupRestoreTab.Text = rm.GetString("Tab_BackupRestore") ?? "Backup / Restore";
            }
            catch (Exception ex)
            {
                UpdateStatus($"Failed to apply localization: {ex.Message}", true);
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private void txtSteamCmdPath_TextChanged(object sender, EventArgs e)
        {
            _updateManager.SetSteamCmdPath(txtSteamCmdPath.Text);
            var prefs = ConfigManager.Load();
            prefs.SteamCmdPath = txtSteamCmdPath.Text;
            ConfigManager.Save(prefs);
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtSrcdsPath.Text))
            {
                Process.Start("explorer.exe", txtSrcdsPath.Text);
            }
        }

        private void btnHelp1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Select the game you wish to install. If your game is not listed, use the Custom Mod option.", "Help");
        }

        private void btnHelp2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Select the game to run. Ensure the path to the srcds.exe is correct.", "Help");
        }

        private void chkCustomMod_CheckedChanged(object sender, EventArgs e)
        {
            txtCustomMod.Enabled = chkCustomMod.Checked;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnConsoleSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtConsoleInput.Text))
            {
                _serverManager.SendCommand(txtConsoleInput.Text);
                txtConsoleInput.Clear();
            }
        }
        private const string CustomGamesFileName = "custom_games.xml";

        private ServerManager _serverManager = new ServerManager();
        private UpdateManager _updateManager = new UpdateManager("steamcmd.exe");
        private BackupManager _backupManager = new BackupManager();
        private RCON rconClient;
        private string _loadedConfigPath = "";
        private List<string> _customGames = new List<string>();

        public MainMenu()
        {
            InitializeComponent();

            _serverManager.ServerOutputReceived += msg => AppendOutputText(msg);
            _serverManager.ServerExited += () => UpdateStatus("Server process exited");

            _updateManager.UpdateCheckCompleted += (hasUpdate, appId) => UpdateStatus(hasUpdate ? $"Update available for AppID {appId}" : $"AppID {appId} is up to date");
            _updateManager.UpdateCompleted += appId => UpdateStatus($"Update completed for AppID {appId}");
            _updateManager.OutputReceived += msg => AppendOutputText(msg);
            _updateManager.ErrorOccurred += msg => UpdateStatus(msg, true);

            _backupManager.BackupCreated += msg => UpdateStatus($"Backup created: {msg}");
            _backupManager.BackupRestored += msg => UpdateStatus($"Backup restored: {msg}");
            _backupManager.ErrorOccurred += msg => UpdateStatus(msg, true);

            LoadCustomGames();
            ApplyLocalization();
            var prefs = ConfigManager.Load();
            txtSteamCmdPath.Text = prefs.SteamCmdPath;
            txtInstallDir.Text = prefs.InstallDirectory;
            txtSrcdsPath.Text = prefs.SrcdsDirectory;
            _updateManager.SetSteamCmdPath(prefs.SteamCmdPath);

            if (!File.Exists(prefs.SteamCmdPath))
            {
                UpdateStatus("SteamCMD not found. Auto-installing...");
                btnDownloadSteamCMD_Click(null, null);
            }


            btnSourceMod.Click += (s, e) => Process.Start(new ProcessStartInfo("http://www.sourcemod.net") { UseShellExecute = true });
            btnMetamod.Click += (s, e) => Process.Start(new ProcessStartInfo("http://www.metamodsource.net") { UseShellExecute = true });
            btnEventScripts.Click += (s, e) => Process.Start(new ProcessStartInfo("http://www.eventscripts.com") { UseShellExecute = true });
            btnValveWiki.Click += (s, e) => Process.Start(new ProcessStartInfo("https://developer.valvesoftware.com/wiki/Main_Page") { UseShellExecute = true });
            btnCheckUpdates.Click += (s, e) => {
                UpdateStatus("Checking for updates...");
                _updateManager.CheckForUpdates();
            };

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


        private void LoadCustomGames()
        {
            if (File.Exists(CustomGamesFileName))
            {
                try {
                    using (var sr = new StreamReader(CustomGamesFileName))
                    {
                        var xs = new XmlSerializer(typeof(List<string>));
                        _customGames = (List<string>)xs.Deserialize(sr);
                        foreach(var g in _customGames) {
                            if (!cmbGameToInstall.Items.Contains(g)) cmbGameToInstall.Items.Add(g);
                        }
                    }
                } catch (Exception ex) {
                    UpdateStatus($"Failed to load custom games: {ex.Message}", true);
                }
            }
        }

        private void SaveCustomGames()
        {
            try {
                using (var sw = new StreamWriter(CustomGamesFileName))
                {
                    var xs = new XmlSerializer(typeof(List<string>));
                    xs.Serialize(sw, _customGames);
                }
            } catch (Exception ex) {
                UpdateStatus($"Failed to save custom games: {ex.Message}", true);
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
                UpdateStatus("Download complete. Extracting steamcmd.zip...");
                if (File.Exists("steamcmd.exe")) File.Delete("steamcmd.exe");
                ZipFile.ExtractToDirectory("steamcmd.zip", Directory.GetCurrentDirectory());
                File.Delete("steamcmd.zip");
                UpdateStatus("Extraction complete. SteamCMD is ready!");
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
            else if (gameName == "Team Fortress 2 Classic") appId = "244310"; // Source SDK Base 2013 Dedicated Server
            else appId = gameName; // In case it's a custom numeric appID

            _updateManager.SetSteamCmdPath(txtSteamCmdPath.Text);
            var prefs = ConfigManager.Load();
            prefs.SteamCmdPath = txtSteamCmdPath.Text;
            ConfigManager.Save(prefs);
            UpdateStatus($"Updating server (AppId: {appId}) to {txtInstallDir.Text}...");



            string loginArg = chkLoginAnonymous.Checked ? "+login anonymous" : $"+login {txtLogin.Text} {txtPassword.Text}";
            string validateArg = chkValidate.Checked ? " validate" : "";

            string args = $"{loginArg} +force_install_dir \"{txtInstallDir.Text}\" +app_update {appId}{validateArg} +quit";

            _updateManager.UpdateServerWithArgs(args, txtInstallDir.Text, appId);
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            string exe = Path.Combine(txtSrcdsPath.Text, "srcds.exe");

            string selectedGame = cmbGameToRun.Text;
            string gameMod = "cstrike";
            if (selectedGame == "Team Fortress 2") gameMod = "tf";
            else if (selectedGame == "Garry's Mod") gameMod = "garrysmod";
            else if (selectedGame == "Half-Life 2: Deathmatch") gameMod = "hl2mp";
            else if (selectedGame == "Left 4 Dead") gameMod = "left4dead";
            else if (selectedGame == "Left 4 Dead 2") gameMod = "left4dead2";
            else if (selectedGame == "Day of Defeat: Source") gameMod = "dod";
            else if (selectedGame == "Alien Swarm") gameMod = "swarm";
            else if (selectedGame == "Counter-Strike: Global Offensive") gameMod = "csgo";
            else if (selectedGame == "Team Fortress 2 Classic") gameMod = "tf2classic";
            else gameMod = selectedGame; // Custom user input


            if (chkCustomMod.Checked && !string.IsNullOrWhiteSpace(txtCustomMod.Text))
            {
                gameMod = txtCustomMod.Text;
            }

            string args = $"-console -game {gameMod} +maxplayers {numMaxPlayers.Value} +map {cmbMap.Text} -port {numUdpPort.Value} +rcon_password \"{txtRcon.Text}\"";



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
                    var prefs = ConfigManager.Load();
                    prefs.InstallDirectory = txtInstallDir.Text;
                    ConfigManager.Save(prefs);
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
                    var prefs = ConfigManager.Load();
                    prefs.SrcdsDirectory = txtSrcdsPath.Text;
                    ConfigManager.Save(prefs);
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
