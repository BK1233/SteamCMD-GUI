namespace SteamCMD_GUI
{
    partial class MainMenu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MainMenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TabMenu = new System.Windows.Forms.TabControl();

            // Tab 1: Update/Install
            this.UpdateTab = new System.Windows.Forms.TabPage();
            this.grpSteamCmdConfig = new System.Windows.Forms.GroupBox();
            this.lblSteamCmdPath = new System.Windows.Forms.Label();
            this.txtSteamCmdPath = new System.Windows.Forms.TextBox();
            this.btnBrowseSteamCmd = new System.Windows.Forms.Button();
            this.grpServerConfig = new System.Windows.Forms.GroupBox();
            this.cmbGameToInstall = new System.Windows.Forms.ComboBox();
            this.btnAddCustom = new System.Windows.Forms.Button();
            this.btnHelp1 = new System.Windows.Forms.Button();
            this.lblLogin = new System.Windows.Forms.Label();
            this.txtLogin = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.chkLoginAnonymous = new System.Windows.Forms.CheckBox();
            this.lblServerPath = new System.Windows.Forms.Label();
            this.txtInstallDir = new System.Windows.Forms.TextBox();
            this.btnBrowseServerPath = new System.Windows.Forms.Button();
            this.chkValidate = new System.Windows.Forms.CheckBox();
            this.chkUseConsole = new System.Windows.Forms.CheckBox();
            this.btnUpdateInstall = new System.Windows.Forms.Button();

            this.grpTools = new System.Windows.Forms.GroupBox();
            this.btnDownloadSteamCMD = new System.Windows.Forms.Button();
            this.btnValveWiki = new System.Windows.Forms.Button();
            this.btnCheckUpdates = new System.Windows.Forms.Button();

            this.grpAddons = new System.Windows.Forms.GroupBox();
            this.btnSourceMod = new System.Windows.Forms.Button();
            this.btnMetamod = new System.Windows.Forms.Button();
            this.btnEventScripts = new System.Windows.Forms.Button();

            // Tab 2: Run Server
            this.RunServerTab = new System.Windows.Forms.TabPage();
            this.grpSrcdsConfig = new System.Windows.Forms.GroupBox();
            this.lblSrcdsPath = new System.Windows.Forms.Label();
            this.txtSrcdsPath = new System.Windows.Forms.TextBox();
            this.btnBrowseSrcds = new System.Windows.Forms.Button();
            this.btnOpenFolder = new System.Windows.Forms.Button();

            this.grpGameConfigRun = new System.Windows.Forms.GroupBox();
            this.cmbGameToRun = new System.Windows.Forms.ComboBox();
            this.txtCustomMod = new System.Windows.Forms.TextBox();
            this.chkCustomMod = new System.Windows.Forms.CheckBox();
            this.btnHelp2 = new System.Windows.Forms.Button();
            this.lblServerName = new System.Windows.Forms.Label();


            this.txtServerName = new System.Windows.Forms.TextBox();
            this.lblMap = new System.Windows.Forms.Label();
            this.cmbMap = new System.Windows.Forms.ComboBox();
            this.lblNetwork = new System.Windows.Forms.Label();
            this.cmbNetwork = new System.Windows.Forms.ComboBox();
            this.lblMaxPlayers = new System.Windows.Forms.Label();
            this.numMaxPlayers = new System.Windows.Forms.NumericUpDown();
            this.lblUdpPort = new System.Windows.Forms.Label();
            this.numUdpPort = new System.Windows.Forms.NumericUpDown();
            this.lblRcon = new System.Windows.Forms.Label();
            this.txtRcon = new System.Windows.Forms.TextBox();
            this.chkSecure = new System.Windows.Forms.CheckBox();

            this.chkDebugMode = new System.Windows.Forms.CheckBox();
            this.chkSourceTV = new System.Windows.Forms.CheckBox();
            this.chkConsoleMode = new System.Windows.Forms.CheckBox();
            this.chkInsecure = new System.Windows.Forms.CheckBox();
            this.chkDisableBots = new System.Windows.Forms.CheckBox();
            this.chkDevMessages = new System.Windows.Forms.CheckBox();

            this.btnRunServer = new System.Windows.Forms.Button();

            // Tab 3: Console
            this.ConsoleTab = new System.Windows.Forms.TabPage();
            this.ConsoleOutput = new System.Windows.Forms.RichTextBox();

            // Tab 4: RCON
            this.RconTab = new System.Windows.Forms.TabPage();
            this.txtRconIp = new System.Windows.Forms.TextBox();
            this.txtRconPort = new System.Windows.Forms.TextBox();
            this.txtRconPassword = new System.Windows.Forms.TextBox();
            this.btnRconConnect = new System.Windows.Forms.Button();
            this.txtRconCommand = new System.Windows.Forms.TextBox();
            this.btnRconSend = new System.Windows.Forms.Button();

            // Tab 5: Config Editor
            this.ConfigEditorTab = new System.Windows.Forms.TabPage();
            this.btnOpenConfig = new System.Windows.Forms.Button();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.txtConfigArea = new System.Windows.Forms.RichTextBox();

            // Tab 6: Backup / Restore
            this.BackupRestoreTab = new System.Windows.Forms.TabPage();
            this.btnCreateBackup = new System.Windows.Forms.Button();
            this.btnRestoreBackup = new System.Windows.Forms.Button();
            this.txtBackupSource = new System.Windows.Forms.TextBox();
            this.txtBackupDest = new System.Windows.Forms.TextBox();

            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Status = new System.Windows.Forms.ToolStripStatusLabel();

            this.MainMenuStrip1.SuspendLayout();
            this.TabMenu.SuspendLayout();
            this.UpdateTab.SuspendLayout();
            this.grpSteamCmdConfig.SuspendLayout();
            this.grpServerConfig.SuspendLayout();
            this.grpTools.SuspendLayout();
            this.grpAddons.SuspendLayout();
            this.RunServerTab.SuspendLayout();
            this.grpSrcdsConfig.SuspendLayout();
            this.grpGameConfigRun.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxPlayers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUdpPort)).BeginInit();
            this.ConsoleTab.SuspendLayout();
            this.RconTab.SuspendLayout();
            this.ConfigEditorTab.SuspendLayout();
            this.BackupRestoreTab.SuspendLayout();
            this.StatusStrip1.SuspendLayout();
            this.SuspendLayout();

            // Menu
            this.MainMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.fileToolStripMenuItem, this.editToolStripMenuItem, this.helpToolStripMenuItem });
            this.MainMenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip1.Name = "MainMenuStrip1";
            this.MainMenuStrip1.Size = new System.Drawing.Size(684, 24);

            this.fileToolStripMenuItem.Text = "File";
            this.editToolStripMenuItem.Text = "Edit";
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.loadConfigToolStripMenuItem, this.saveConfigToolStripMenuItem, this.exitToolStripMenuItem });
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            this.loadConfigToolStripMenuItem.Text = "Load Configuration";
            this.loadConfigToolStripMenuItem.Click += new System.EventHandler(this.btnOpenConfig_Click);
            this.saveConfigToolStripMenuItem.Text = "Save Configuration";
            this.saveConfigToolStripMenuItem.Click += new System.EventHandler(this.btnSaveConfig_Click);

            this.motdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapcycleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.motdToolStripMenuItem, this.mapcycleToolStripMenuItem });
            this.motdToolStripMenuItem.Text = "Motd.txt";
            this.motdToolStripMenuItem.Click += new System.EventHandler(this.btnOpenConfig_Click);
            this.mapcycleToolStripMenuItem.Text = "Mapcycle.txt";
            this.mapcycleToolStripMenuItem.Click += new System.EventHandler(this.btnOpenConfig_Click);







            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.aboutToolStripMenuItem, this.commandsToolStripMenuItem });
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            this.commandsToolStripMenuItem.Text = "Commands";
            this.commandsToolStripMenuItem.Click += new System.EventHandler(this.commandsToolStripMenuItem_Click);

            // TabMenu
            this.TabMenu.Controls.Add(this.UpdateTab);
            this.TabMenu.Controls.Add(this.RunServerTab);
            this.TabMenu.Controls.Add(this.ConsoleTab);
            this.TabMenu.Controls.Add(this.RconTab);
            this.TabMenu.Controls.Add(this.ConfigEditorTab);
            this.TabMenu.Controls.Add(this.BackupRestoreTab);
            this.TabMenu.Location = new System.Drawing.Point(12, 27);
            this.TabMenu.Size = new System.Drawing.Size(480, 380);

            // UpdateTab
            this.UpdateTab.Text = "Update/Install";

            this.grpSteamCmdConfig.Text = "SteamCMD Configuration";
            this.grpSteamCmdConfig.Location = new System.Drawing.Point(6, 6);
            this.grpSteamCmdConfig.Size = new System.Drawing.Size(460, 60);
            this.UpdateTab.Controls.Add(this.grpSteamCmdConfig);

            this.lblSteamCmdPath.Text = "SteamCMD path";
            this.lblSteamCmdPath.Location = new System.Drawing.Point(6, 22);
            this.lblSteamCmdPath.AutoSize = true;
            this.txtSteamCmdPath.Location = new System.Drawing.Point(100, 20);
            this.txtSteamCmdPath.Size = new System.Drawing.Size(270, 20);
            this.txtSteamCmdPath.Text = "steamcmd.exe";
            this.btnBrowseSteamCmd.Text = "Browser";
            this.btnBrowseSteamCmd.Location = new System.Drawing.Point(376, 18);
            this.btnBrowseSteamCmd.Click += new System.EventHandler(this.btnBrowseSteamCmd_Click);
            this.grpSteamCmdConfig.Controls.Add(this.lblSteamCmdPath);
            this.grpSteamCmdConfig.Controls.Add(this.txtSteamCmdPath);
            this.grpSteamCmdConfig.Controls.Add(this.btnBrowseSteamCmd);

            this.grpServerConfig.Text = "Server Configuration";
            this.grpServerConfig.Location = new System.Drawing.Point(6, 72);
            this.grpServerConfig.Size = new System.Drawing.Size(460, 240);
            this.UpdateTab.Controls.Add(this.grpServerConfig);

            this.cmbGameToInstall.Location = new System.Drawing.Point(6, 20);
            this.cmbGameToInstall.Size = new System.Drawing.Size(200, 21);
            this.cmbGameToInstall.Text = "Counter-Strike: Global Offensive";
            this.cmbGameToInstall.Items.Add("Counter-Strike: Global Offensive");
            this.cmbGameToInstall.Items.Add("Team Fortress 2");
            this.cmbGameToInstall.Items.Add("Garry's Mod");
            this.cmbGameToInstall.Items.Add("Half-Life Deathmatch: Source");
            this.cmbGameToInstall.Items.Add("Left 4 Dead 2");
            this.cmbGameToInstall.Items.Add("Team Fortress 2 Classic");

            this.btnAddCustom.Text = "Add Custom";
            this.btnAddCustom.Location = new System.Drawing.Point(296, 20);
            this.btnHelp1.Text = "?";
            this.btnHelp1.Click += new System.EventHandler(this.btnHelp1_Click);
            this.btnHelp1.Location = new System.Drawing.Point(376, 20);
            this.btnHelp1.Size = new System.Drawing.Size(25, 23);

            this.lblLogin.Text = "Login";
            this.lblLogin.Location = new System.Drawing.Point(6, 50);
            this.lblLogin.AutoSize = true;
            this.txtLogin.Location = new System.Drawing.Point(6, 68);
            this.txtLogin.Size = new System.Drawing.Size(120, 20);
            this.txtPassword.Location = new System.Drawing.Point(6, 94);
            this.txtPassword.Size = new System.Drawing.Size(120, 20);
            this.txtPassword.PasswordChar = '*';
            this.chkLoginAnonymous.Text = "Login as Anonymous";
            this.chkLoginAnonymous.Location = new System.Drawing.Point(135, 70);
            this.chkLoginAnonymous.Checked = true;

            this.lblServerPath.Text = "Server Path";
            this.lblServerPath.Location = new System.Drawing.Point(6, 125);
            this.lblServerPath.AutoSize = true;
            this.txtInstallDir.Location = new System.Drawing.Point(6, 143);
            this.txtInstallDir.Size = new System.Drawing.Size(360, 20);
            this.btnBrowseServerPath.Text = "Browser";
            this.btnBrowseServerPath.Location = new System.Drawing.Point(376, 141);
            this.btnBrowseServerPath.Click += new System.EventHandler(this.btnBrowseServerPath_Click);

            this.chkValidate.Text = "Validate Files";
            this.chkValidate.Location = new System.Drawing.Point(6, 175);
            this.chkValidate.Checked = true;
            this.chkUseConsole.Text = "Use the Console";
            this.chkUseConsole.Location = new System.Drawing.Point(120, 175);

            this.btnUpdateInstall.Text = "Update/Install";
            this.btnUpdateInstall.Location = new System.Drawing.Point(350, 170);
            this.btnUpdateInstall.Size = new System.Drawing.Size(100, 25);
            this.btnUpdateInstall.Click += new System.EventHandler(this.btnUpdateServer_Click);

            this.grpServerConfig.Controls.Add(this.cmbGameToInstall);
            this.grpServerConfig.Controls.Add(this.btnAddCustom);
            this.grpServerConfig.Controls.Add(this.btnHelp1);
            this.grpServerConfig.Controls.Add(this.lblLogin);
            this.grpServerConfig.Controls.Add(this.txtLogin);
            this.grpServerConfig.Controls.Add(this.txtPassword);
            this.grpServerConfig.Controls.Add(this.chkLoginAnonymous);
            this.grpServerConfig.Controls.Add(this.lblServerPath);
            this.grpServerConfig.Controls.Add(this.txtInstallDir);
            this.grpServerConfig.Controls.Add(this.btnBrowseServerPath);
            this.grpServerConfig.Controls.Add(this.chkValidate);
            this.grpServerConfig.Controls.Add(this.chkUseConsole);
            this.grpServerConfig.Controls.Add(this.btnUpdateInstall);

            this.grpTools.Text = "Tools";
            this.grpTools.Location = new System.Drawing.Point(500, 27);
            this.grpTools.Size = new System.Drawing.Size(170, 110);

            this.btnDownloadSteamCMD.Text = "Download SteamCMD";
            this.btnDownloadSteamCMD.Location = new System.Drawing.Point(6, 19);
            this.btnDownloadSteamCMD.Size = new System.Drawing.Size(130, 23);
            this.btnDownloadSteamCMD.Click += new System.EventHandler(this.btnDownloadSteamCMD_Click);

            this.btnValveWiki.Text = "Valve Developer Community";
            this.btnValveWiki.Location = new System.Drawing.Point(6, 48);
            this.btnValveWiki.Size = new System.Drawing.Size(155, 23);

            this.btnCheckUpdates.Text = "Check for Updates";
            this.btnCheckUpdates.Location = new System.Drawing.Point(6, 77);
            this.btnCheckUpdates.Size = new System.Drawing.Size(155, 23);

            this.grpTools.Controls.Add(this.btnDownloadSteamCMD);
            this.grpTools.Controls.Add(this.btnValveWiki);
            this.grpTools.Controls.Add(this.btnCheckUpdates);

            this.grpAddons.Text = "Addons";
            this.grpAddons.Location = new System.Drawing.Point(500, 150);
            this.grpAddons.Size = new System.Drawing.Size(170, 110);

            this.btnSourceMod.Text = "SourceMod";
            this.btnSourceMod.Location = new System.Drawing.Point(6, 19);
            this.btnSourceMod.Size = new System.Drawing.Size(155, 23);

            this.btnMetamod.Text = "Metamod:Source";
            this.btnMetamod.Location = new System.Drawing.Point(6, 48);
            this.btnMetamod.Size = new System.Drawing.Size(155, 23);

            this.btnEventScripts.Text = "EventScripts";
            this.btnEventScripts.Location = new System.Drawing.Point(6, 77);
            this.btnEventScripts.Size = new System.Drawing.Size(155, 23);

            this.grpAddons.Controls.Add(this.btnSourceMod);
            this.grpAddons.Controls.Add(this.btnMetamod);
            this.grpAddons.Controls.Add(this.btnEventScripts);

            // RunServerTab
            this.RunServerTab.Text = "Run Server";

            this.grpSrcdsConfig.Text = "Srcds Configuration";
            this.grpSrcdsConfig.Location = new System.Drawing.Point(6, 6);
            this.grpSrcdsConfig.Size = new System.Drawing.Size(460, 60);
            this.RunServerTab.Controls.Add(this.grpSrcdsConfig);

            this.lblSrcdsPath.Text = "Srcds path";
            this.lblSrcdsPath.Location = new System.Drawing.Point(6, 22);
            this.lblSrcdsPath.AutoSize = true;
            this.txtSrcdsPath.Location = new System.Drawing.Point(100, 20);
            this.txtSrcdsPath.Size = new System.Drawing.Size(240, 20);
            this.btnBrowseSrcds.Text = "Browser";
            this.btnBrowseSrcds.Location = new System.Drawing.Point(346, 18);
            this.btnBrowseSrcds.Click += new System.EventHandler(this.btnBrowseSrcds_Click);
            this.btnOpenFolder.Text = "O";
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            this.btnOpenFolder.Location = new System.Drawing.Point(426, 18);
            this.btnOpenFolder.Size = new System.Drawing.Size(25, 23);

            this.grpSrcdsConfig.Controls.Add(this.lblSrcdsPath);
            this.grpSrcdsConfig.Controls.Add(this.txtSrcdsPath);
            this.grpSrcdsConfig.Controls.Add(this.btnBrowseSrcds);
            this.grpSrcdsConfig.Controls.Add(this.btnOpenFolder);

            this.grpGameConfigRun.Text = "Game Configuration";
            this.grpGameConfigRun.Location = new System.Drawing.Point(6, 72);
            this.grpGameConfigRun.Size = new System.Drawing.Size(460, 286);
            this.RunServerTab.Controls.Add(this.grpGameConfigRun);

            this.cmbGameToRun.Location = new System.Drawing.Point(6, 48);


            this.cmbGameToRun.Size = new System.Drawing.Size(200, 21);
            this.cmbGameToRun.Text = "Counter-Strike: Source";
            this.cmbGameToRun.Items.Add("Counter-Strike: Source");
            this.cmbGameToRun.Items.Add("Team Fortress 2");
            this.cmbGameToRun.Items.Add("Garry's Mod");
            this.cmbGameToRun.Items.Add("Half-Life 2: Deathmatch");
            this.cmbGameToRun.Items.Add("Left 4 Dead");
            this.cmbGameToRun.Items.Add("Left 4 Dead 2");
            this.cmbGameToRun.Items.Add("Day of Defeat: Source");
            this.cmbGameToRun.Items.Add("Alien Swarm");
            this.cmbGameToRun.Items.Add("Counter-Strike: Global Offensive");
            this.cmbGameToRun.Items.Add("Team Fortress 2 Classic");

            this.txtCustomMod.Location = new System.Drawing.Point(216, 48);
            this.txtCustomMod.Size = new System.Drawing.Size(80, 20);
            this.chkCustomMod.Text = "Custom Mod";
            this.chkCustomMod.CheckedChanged += new System.EventHandler(this.chkCustomMod_CheckedChanged);
            this.chkCustomMod.Location = new System.Drawing.Point(300, 50);
            this.btnHelp2.Text = "?";
            this.btnHelp2.Click += new System.EventHandler(this.btnHelp2_Click);
            this.btnHelp2.Location = new System.Drawing.Point(400, 48);
            this.btnHelp2.Size = new System.Drawing.Size(25, 23);

            this.lblServerName.Text = "Server Name";
            this.lblServerName.Location = new System.Drawing.Point(6, 76);
            this.lblServerName.AutoSize = true;
            this.txtServerName.Location = new System.Drawing.Point(90, 74);
            this.txtServerName.Size = new System.Drawing.Size(300, 20);
            this.txtServerName.Text = "Source Dedicated Server";

            this.lblMap.Text = "Map";
            this.lblMap.Location = new System.Drawing.Point(6, 102);
            this.lblMap.AutoSize = true;
            this.cmbMap.Location = new System.Drawing.Point(90, 100);
            this.cmbMap.Size = new System.Drawing.Size(300, 21);
            this.cmbMap.Text = "de_dust2";

            this.lblNetwork.Text = "Network";
            this.lblNetwork.Location = new System.Drawing.Point(6, 128);
            this.lblNetwork.AutoSize = true;
            this.cmbNetwork.Location = new System.Drawing.Point(90, 126);
            this.cmbNetwork.Size = new System.Drawing.Size(120, 21);
            this.cmbNetwork.Text = "Internet";

            this.lblMaxPlayers.Text = "Max Players";
            this.lblMaxPlayers.Location = new System.Drawing.Point(220, 128);
            this.lblMaxPlayers.AutoSize = true;
            this.numMaxPlayers.Location = new System.Drawing.Point(300, 126);
            this.numMaxPlayers.Size = new System.Drawing.Size(50, 20);
            this.numMaxPlayers.Value = 24;

            this.lblUdpPort.Text = "UDP Port";
            this.lblUdpPort.Location = new System.Drawing.Point(6, 154);
            this.lblUdpPort.AutoSize = true;
            this.numUdpPort.Location = new System.Drawing.Point(90, 152);
            this.numUdpPort.Size = new System.Drawing.Size(80, 20);
            this.numUdpPort.Value = 27015;
            this.numUdpPort.Maximum = 65535;

            this.lblRcon.Text = "RCON";
            this.lblRcon.Location = new System.Drawing.Point(220, 154);
            this.lblRcon.AutoSize = true;
            this.txtRcon.Location = new System.Drawing.Point(270, 152);
            this.txtRcon.Size = new System.Drawing.Size(100, 20);
            this.chkSecure.Location = new System.Drawing.Point(380, 154);
            this.chkSecure.Size = new System.Drawing.Size(20, 20);
            this.chkSecure.Checked = true;

            this.chkDebugMode.Text = "Debug Mode";
            this.chkDebugMode.Location = new System.Drawing.Point(6, 186);
            this.chkSourceTV.Text = "SourceTV";
            this.chkSourceTV.Location = new System.Drawing.Point(120, 186);
            this.chkConsoleMode.Text = "Console Mode";
            this.chkConsoleMode.Location = new System.Drawing.Point(220, 186);
            this.chkConsoleMode.AutoSize = true;
            this.chkConsoleMode.Checked = true;

            this.chkInsecure.Text = "Insecure";
            this.chkInsecure.Location = new System.Drawing.Point(6, 211);
            this.chkDisableBots.Text = "Disable Bots";
            this.chkDisableBots.Location = new System.Drawing.Point(120, 211);
            this.chkDevMessages.Text = "Dev Messages";
            this.chkDevMessages.Location = new System.Drawing.Point(220, 211);
            this.chkDevMessages.AutoSize = true;

            this.btnRunServer.Text = "Run Server";
            this.btnRunServer.Location = new System.Drawing.Point(350, 206);
            this.btnRunServer.Size = new System.Drawing.Size(80, 25);
            this.btnRunServer.Click += new System.EventHandler(this.btnStartServer_Click);

            this.grpGameConfigRun.Controls.Add(this.cmbGameToRun);
            this.grpGameConfigRun.Controls.Add(this.txtCustomMod);
            this.grpGameConfigRun.Controls.Add(this.chkCustomMod);
            this.grpGameConfigRun.Controls.Add(this.btnHelp2);
            this.grpGameConfigRun.Controls.Add(this.lblServerName);

            this.grpGameConfigRun.Controls.Add(this.txtServerName);
            this.grpGameConfigRun.Controls.Add(this.lblMap);
            this.grpGameConfigRun.Controls.Add(this.cmbMap);
            this.grpGameConfigRun.Controls.Add(this.lblNetwork);
            this.grpGameConfigRun.Controls.Add(this.cmbNetwork);
            this.grpGameConfigRun.Controls.Add(this.lblMaxPlayers);
            this.grpGameConfigRun.Controls.Add(this.numMaxPlayers);
            this.grpGameConfigRun.Controls.Add(this.lblUdpPort);
            this.grpGameConfigRun.Controls.Add(this.numUdpPort);
            this.grpGameConfigRun.Controls.Add(this.lblRcon);
            this.grpGameConfigRun.Controls.Add(this.txtRcon);
            this.grpGameConfigRun.Controls.Add(this.chkSecure);
            this.grpGameConfigRun.Controls.Add(this.chkDebugMode);
            this.grpGameConfigRun.Controls.Add(this.chkSourceTV);
            this.grpGameConfigRun.Controls.Add(this.chkConsoleMode);
            this.grpGameConfigRun.Controls.Add(this.chkInsecure);
            this.grpGameConfigRun.Controls.Add(this.chkDisableBots);
            this.grpGameConfigRun.Controls.Add(this.chkDevMessages);
            this.grpGameConfigRun.Controls.Add(this.btnRunServer);

            this.ConsoleTab.Text = "Console";
            this.ConsoleOutput.Location = new System.Drawing.Point(8, 8);
            this.ConsoleOutput.Size = new System.Drawing.Size(460, 360);
            this.ConsoleTab.Controls.Add(this.ConsoleOutput);

            this.txtConsoleInput = new System.Windows.Forms.TextBox();
            this.btnConsoleSend = new System.Windows.Forms.Button();

            this.txtConsoleInput.Location = new System.Drawing.Point(8, 380);
            this.txtConsoleInput.Size = new System.Drawing.Size(380, 20);
            this.btnConsoleSend.Location = new System.Drawing.Point(390, 378);
            this.btnConsoleSend.Size = new System.Drawing.Size(75, 23);
            this.btnConsoleSend.Text = "Send";
            this.btnConsoleSend.Click += new System.EventHandler(this.btnConsoleSend_Click);

            this.ConsoleTab.Controls.Add(this.txtConsoleInput);
            this.ConsoleTab.Controls.Add(this.btnConsoleSend);


            // RCON Tab
            this.RconTab.Text = "RCON";
            this.txtRconIp.Location = new System.Drawing.Point(8, 8);
            this.txtRconIp.Size = new System.Drawing.Size(100, 20);
            this.txtRconIp.Text = "127.0.0.1";

            this.txtRconPort.Location = new System.Drawing.Point(114, 8);
            this.txtRconPort.Size = new System.Drawing.Size(50, 20);
            this.txtRconPort.Text = "27015";

            this.txtRconPassword.Location = new System.Drawing.Point(170, 8);
            this.txtRconPassword.PasswordChar = '*';
            this.txtRconPassword.Size = new System.Drawing.Size(100, 20);
            this.txtRconPassword.Text = "password";

            this.btnRconConnect.Location = new System.Drawing.Point(276, 6);
            this.btnRconConnect.Size = new System.Drawing.Size(75, 23);
            this.btnRconConnect.Text = "Connect";
            this.btnRconConnect.Click += new System.EventHandler(this.btnRconConnect_Click);

            this.txtRconCommand.Location = new System.Drawing.Point(8, 34);
            this.txtRconCommand.Size = new System.Drawing.Size(262, 20);

            this.btnRconSend.Location = new System.Drawing.Point(276, 32);
            this.btnRconSend.Size = new System.Drawing.Size(75, 23);
            this.btnRconSend.Text = "Send";
            this.btnRconSend.Click += new System.EventHandler(this.btnRconSend_Click);

            this.RconTab.Controls.Add(this.txtRconIp);
            this.RconTab.Controls.Add(this.txtRconPort);
            this.RconTab.Controls.Add(this.txtRconPassword);
            this.RconTab.Controls.Add(this.btnRconConnect);
            this.RconTab.Controls.Add(this.txtRconCommand);
            this.RconTab.Controls.Add(this.btnRconSend);

            // Config Editor Tab
            this.ConfigEditorTab.Text = "Config Editor";
            this.btnOpenConfig.Location = new System.Drawing.Point(8, 8);
            this.btnOpenConfig.Size = new System.Drawing.Size(100, 23);
            this.btnOpenConfig.Text = "Open Config...";
            this.btnOpenConfig.Click += new System.EventHandler(this.btnOpenConfig_Click);

            this.btnSaveConfig.Location = new System.Drawing.Point(114, 8);
            this.btnSaveConfig.Size = new System.Drawing.Size(100, 23);
            this.btnSaveConfig.Text = "Save Config...";
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);

            this.txtConfigArea.Location = new System.Drawing.Point(8, 37);
            this.txtConfigArea.Size = new System.Drawing.Size(460, 320);

            this.ConfigEditorTab.Controls.Add(this.btnOpenConfig);
            this.ConfigEditorTab.Controls.Add(this.btnSaveConfig);
            this.ConfigEditorTab.Controls.Add(this.txtConfigArea);

            // Backup / Restore Tab
            this.BackupRestoreTab.Text = "Backup / Restore";
            this.txtBackupSource.Location = new System.Drawing.Point(8, 8);
            this.txtBackupSource.Size = new System.Drawing.Size(150, 20);
            this.txtBackupSource.Text = "Source Folder";

            this.txtBackupDest.Location = new System.Drawing.Point(164, 8);
            this.txtBackupDest.Size = new System.Drawing.Size(150, 20);
            this.txtBackupDest.Text = "Destination Folder";

            this.btnCreateBackup.Location = new System.Drawing.Point(8, 34);
            this.btnCreateBackup.Size = new System.Drawing.Size(100, 23);
            this.btnCreateBackup.Text = "Create Backup";
            this.btnCreateBackup.Click += new System.EventHandler(this.btnCreateBackup_Click);

            this.btnRestoreBackup.Location = new System.Drawing.Point(114, 34);
            this.btnRestoreBackup.Size = new System.Drawing.Size(100, 23);
            this.btnRestoreBackup.Text = "Restore Backup";
            this.btnRestoreBackup.Click += new System.EventHandler(this.btnRestoreBackup_Click);

            this.BackupRestoreTab.Controls.Add(this.btnCreateBackup);
            this.BackupRestoreTab.Controls.Add(this.btnRestoreBackup);
            this.BackupRestoreTab.Controls.Add(this.txtBackupSource);
            this.BackupRestoreTab.Controls.Add(this.txtBackupDest);

            this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.Status });
            this.StatusStrip1.Location = new System.Drawing.Point(0, 428);
            this.Status.Text = "Ready";

            this.ClientSize = new System.Drawing.Size(684, 450);
            this.Controls.Add(this.TabMenu);
            this.Controls.Add(this.grpTools);
            this.Controls.Add(this.grpAddons);
            this.Controls.Add(this.MainMenuStrip1);
            this.Controls.Add(this.StatusStrip1);
            this.MainMenuStrip = this.MainMenuStrip1;
            this.Name = "MainMenu";
            this.Text = "SteamCMD GUI";

            this.MainMenuStrip1.ResumeLayout(false);
            this.TabMenu.ResumeLayout(false);
            this.UpdateTab.ResumeLayout(false);
            this.grpSteamCmdConfig.ResumeLayout(false);
            this.grpServerConfig.ResumeLayout(false);
            this.grpTools.ResumeLayout(false);
            this.grpAddons.ResumeLayout(false);
            this.RunServerTab.ResumeLayout(false);
            this.grpSrcdsConfig.ResumeLayout(false);
            this.grpGameConfigRun.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numMaxPlayers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUdpPort)).EndInit();
            this.ConsoleTab.ResumeLayout(false);
            this.RconTab.ResumeLayout(false);
            this.ConfigEditorTab.ResumeLayout(false);
            this.BackupRestoreTab.ResumeLayout(false);
            this.StatusStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.MenuStrip MainMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commandsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem motdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapcycleToolStripMenuItem;
        private System.Windows.Forms.TabControl TabMenu;
        private System.Windows.Forms.TabPage UpdateTab;
        private System.Windows.Forms.TabPage RunServerTab;
        private System.Windows.Forms.TabPage ConsoleTab;
        private System.Windows.Forms.TabPage RconTab;
        private System.Windows.Forms.TabPage ConfigEditorTab;
        private System.Windows.Forms.TabPage BackupRestoreTab;

        private System.Windows.Forms.GroupBox grpSteamCmdConfig;
        private System.Windows.Forms.Label lblSteamCmdPath;
        private System.Windows.Forms.TextBox txtSteamCmdPath;
        private System.Windows.Forms.Button btnBrowseSteamCmd;

        private System.Windows.Forms.GroupBox grpServerConfig;
        private System.Windows.Forms.ComboBox cmbGameToInstall;
        private System.Windows.Forms.Button btnAddCustom;
        private System.Windows.Forms.Button btnHelp1;
        private System.Windows.Forms.Label lblLogin;
        private System.Windows.Forms.TextBox txtLogin;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkLoginAnonymous;
        private System.Windows.Forms.Label lblServerPath;
        private System.Windows.Forms.TextBox txtInstallDir;
        private System.Windows.Forms.Button btnBrowseServerPath;
        private System.Windows.Forms.CheckBox chkValidate;
        private System.Windows.Forms.CheckBox chkUseConsole;
        private System.Windows.Forms.Button btnUpdateInstall;

        private System.Windows.Forms.GroupBox grpSrcdsConfig;
        private System.Windows.Forms.Label lblSrcdsPath;
        private System.Windows.Forms.TextBox txtSrcdsPath;
        private System.Windows.Forms.Button btnBrowseSrcds;
        private System.Windows.Forms.Button btnOpenFolder;

        private System.Windows.Forms.GroupBox grpGameConfigRun;
        private System.Windows.Forms.ComboBox cmbGameToRun;
        private System.Windows.Forms.TextBox txtCustomMod;
        private System.Windows.Forms.CheckBox chkCustomMod;
        private System.Windows.Forms.Button btnHelp2;
        private System.Windows.Forms.Label lblServerName;


        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Label lblMap;
        private System.Windows.Forms.ComboBox cmbMap;
        private System.Windows.Forms.Label lblNetwork;
        private System.Windows.Forms.ComboBox cmbNetwork;
        private System.Windows.Forms.Label lblMaxPlayers;
        private System.Windows.Forms.NumericUpDown numMaxPlayers;
        private System.Windows.Forms.Label lblUdpPort;
        private System.Windows.Forms.NumericUpDown numUdpPort;
        private System.Windows.Forms.Label lblRcon;
        private System.Windows.Forms.TextBox txtRcon;
        private System.Windows.Forms.CheckBox chkSecure;

        private System.Windows.Forms.CheckBox chkDebugMode;
        private System.Windows.Forms.CheckBox chkSourceTV;
        private System.Windows.Forms.CheckBox chkConsoleMode;
        private System.Windows.Forms.CheckBox chkInsecure;
        private System.Windows.Forms.CheckBox chkDisableBots;
        private System.Windows.Forms.CheckBox chkDevMessages;
                private System.Windows.Forms.Button btnRunServer;
                private System.Windows.Forms.GroupBox grpTools;
        private System.Windows.Forms.Button btnDownloadSteamCMD;
        private System.Windows.Forms.Button btnValveWiki;
        private System.Windows.Forms.Button btnCheckUpdates;

        private System.Windows.Forms.GroupBox grpAddons;
        private System.Windows.Forms.Button btnSourceMod;
        private System.Windows.Forms.Button btnMetamod;
        private System.Windows.Forms.Button btnEventScripts;

        private System.Windows.Forms.TextBox txtRconIp;
        private System.Windows.Forms.TextBox txtRconPort;
        private System.Windows.Forms.TextBox txtRconPassword;
        private System.Windows.Forms.Button btnRconConnect;
        private System.Windows.Forms.TextBox txtRconCommand;
        private System.Windows.Forms.Button btnRconSend;

        private System.Windows.Forms.Button btnOpenConfig;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.RichTextBox txtConfigArea;

        private System.Windows.Forms.Button btnCreateBackup;
        private System.Windows.Forms.Button btnRestoreBackup;
        private System.Windows.Forms.TextBox txtBackupSource;
        private System.Windows.Forms.TextBox txtBackupDest;

        private System.Windows.Forms.RichTextBox ConsoleOutput;
        private System.Windows.Forms.TextBox txtConsoleInput;
        private System.Windows.Forms.Button btnConsoleSend;
        private System.Windows.Forms.StatusStrip StatusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Status;
    }
}
