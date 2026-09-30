namespace SteamCMD_GUI
{
    partial class SteamCMDMainMenu
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
            this.TabMenu = new System.Windows.Forms.TabControl();
            this.RunTab = new System.Windows.Forms.TabPage();
            this.btnDownloadSteamCMD = new System.Windows.Forms.Button();

            this.UpdateTab = new System.Windows.Forms.TabPage();
            this.txtAppId = new System.Windows.Forms.TextBox();
            this.txtInstallDir = new System.Windows.Forms.TextBox();
            this.txtModId = new System.Windows.Forms.TextBox();
            this.btnUpdateServer = new System.Windows.Forms.Button();
            this.btnInstallMod = new System.Windows.Forms.Button();
            this.chkAutoValidate = new System.Windows.Forms.CheckBox();

            this.BackupRestoreTab = new System.Windows.Forms.TabPage();
            this.btnCreateBackup = new System.Windows.Forms.Button();
            this.btnRestoreBackup = new System.Windows.Forms.Button();
            this.txtBackupSource = new System.Windows.Forms.TextBox();
            this.txtBackupDest = new System.Windows.Forms.TextBox();

            this.ConfigEditorTab = new System.Windows.Forms.TabPage();
            this.btnOpenConfig = new System.Windows.Forms.Button();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.txtConfigArea = new System.Windows.Forms.RichTextBox();

            this.WorkshopManagerTab = new System.Windows.Forms.TabPage();
            this.btnSearchWorkshop = new System.Windows.Forms.Button();
            this.txtWorkshopSearch = new System.Windows.Forms.TextBox();
            this.lstWorkshopResults = new System.Windows.Forms.ListBox();

            this.ServerManagerTab = new System.Windows.Forms.TabPage();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.btnStopServer = new System.Windows.Forms.Button();

            this.ProfilesTab = new System.Windows.Forms.TabPage();
            this.cmbProfiles = new System.Windows.Forms.ComboBox();
            this.btnSaveProfile = new System.Windows.Forms.Button();
            this.btnLoadProfile = new System.Windows.Forms.Button();

            this.SettingsTab = new System.Windows.Forms.TabPage();
            this.chkAutoUpdate = new System.Windows.Forms.CheckBox();

            this.RconTab = new System.Windows.Forms.TabPage();
            this.txtRconIp = new System.Windows.Forms.TextBox();
            this.txtRconPort = new System.Windows.Forms.TextBox();
            this.txtRconPassword = new System.Windows.Forms.TextBox();
            this.txtRconCommand = new System.Windows.Forms.TextBox();
            this.btnRconConnect = new System.Windows.Forms.Button();
            this.btnRconSend = new System.Windows.Forms.Button();

            this.ConsoleTab = new System.Windows.Forms.TabPage();
            this.ConsoleOutput = new System.Windows.Forms.RichTextBox();
            this.txtLogSearch = new System.Windows.Forms.TextBox();
            this.btnLogSearch = new System.Windows.Forms.Button();

            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Status = new System.Windows.Forms.ToolStripStatusLabel();


            this.MainMenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuStrip1.SuspendLayout();

            this.MainMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.aboutToolStripMenuItem, this.commandsToolStripMenuItem });
            this.MainMenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip1.Name = "MainMenuStrip1";
            this.MainMenuStrip1.Size = new System.Drawing.Size(784, 24);
            this.MainMenuStrip1.TabIndex = 2;

            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);

            this.commandsToolStripMenuItem.Name = "commandsToolStripMenuItem";
            this.commandsToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.commandsToolStripMenuItem.Text = "Commands";
            this.commandsToolStripMenuItem.Click += new System.EventHandler(this.commandsToolStripMenuItem_Click);

            this.TabMenu.SuspendLayout();
            this.RunTab.SuspendLayout();
            this.UpdateTab.SuspendLayout();
            this.BackupRestoreTab.SuspendLayout();
            this.ConfigEditorTab.SuspendLayout();
            this.WorkshopManagerTab.SuspendLayout();
            this.ServerManagerTab.SuspendLayout();
            this.ProfilesTab.SuspendLayout();
            this.SettingsTab.SuspendLayout();
            this.RconTab.SuspendLayout();
            this.ConsoleTab.SuspendLayout();
            this.StatusStrip1.SuspendLayout();
            this.SuspendLayout();

            // TabMenu
            this.TabMenu.Controls.Add(this.RunTab);
            this.TabMenu.Controls.Add(this.UpdateTab);
            this.TabMenu.Controls.Add(this.BackupRestoreTab);
            this.TabMenu.Controls.Add(this.ConfigEditorTab);
            this.TabMenu.Controls.Add(this.WorkshopManagerTab);
            this.TabMenu.Controls.Add(this.ServerManagerTab);
            this.TabMenu.Controls.Add(this.ProfilesTab);
            this.TabMenu.Controls.Add(this.SettingsTab);
            this.TabMenu.Controls.Add(this.RconTab);
            this.TabMenu.Controls.Add(this.ConsoleTab);
            this.TabMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabMenu.Location = new System.Drawing.Point(0, 0);
            this.TabMenu.Name = "TabMenu";
            this.TabMenu.SelectedIndex = 0;
            this.TabMenu.Size = new System.Drawing.Size(784, 428);
            this.TabMenu.TabIndex = 0;

            // RunTab
            this.RunTab.Controls.Add(this.btnDownloadSteamCMD);
            this.RunTab.Location = new System.Drawing.Point(4, 22);
            this.RunTab.Name = "RunTab";
            this.RunTab.Size = new System.Drawing.Size(776, 402);
            this.RunTab.TabIndex = 0;
            this.RunTab.Text = "Run";
            this.RunTab.UseVisualStyleBackColor = true;

            this.btnDownloadSteamCMD.Location = new System.Drawing.Point(8, 8);
            this.btnDownloadSteamCMD.Name = "btnDownloadSteamCMD";
            this.btnDownloadSteamCMD.Size = new System.Drawing.Size(150, 23);
            this.btnDownloadSteamCMD.TabIndex = 0;
            this.btnDownloadSteamCMD.Text = "Download SteamCMD";
            this.btnDownloadSteamCMD.UseVisualStyleBackColor = true;
            this.btnDownloadSteamCMD.Click += new System.EventHandler(this.btnDownloadSteamCMD_Click);

            // UpdateTab
            this.UpdateTab.Controls.Add(this.txtAppId);
            this.UpdateTab.Controls.Add(this.txtInstallDir);
            this.UpdateTab.Controls.Add(this.txtModId);
            this.UpdateTab.Controls.Add(this.btnUpdateServer);
            this.UpdateTab.Controls.Add(this.btnInstallMod);
            this.UpdateTab.Controls.Add(this.chkAutoValidate);
            this.UpdateTab.Location = new System.Drawing.Point(4, 22);
            this.UpdateTab.Name = "UpdateTab";
            this.UpdateTab.Size = new System.Drawing.Size(776, 402);
            this.UpdateTab.TabIndex = 1;
            this.UpdateTab.Text = "Update";
            this.UpdateTab.UseVisualStyleBackColor = true;

            this.txtAppId.Location = new System.Drawing.Point(8, 8);
            this.txtAppId.Name = "txtAppId";
            this.txtAppId.Size = new System.Drawing.Size(100, 20);
            this.txtAppId.TabIndex = 0;
            this.txtAppId.Text = "AppID";

            this.txtInstallDir.Location = new System.Drawing.Point(114, 8);
            this.txtInstallDir.Name = "txtInstallDir";
            this.txtInstallDir.Size = new System.Drawing.Size(100, 20);
            this.txtInstallDir.TabIndex = 1;
            this.txtInstallDir.Text = "Install Directory";

            this.txtModId.Location = new System.Drawing.Point(220, 8);
            this.txtModId.Name = "txtModId";
            this.txtModId.Size = new System.Drawing.Size(100, 20);
            this.txtModId.TabIndex = 2;
            this.txtModId.Text = "ModID";

            this.chkAutoValidate.Location = new System.Drawing.Point(326, 10);
            this.chkAutoValidate.Name = "chkAutoValidate";
            this.chkAutoValidate.Size = new System.Drawing.Size(100, 20);
            this.chkAutoValidate.Text = "Auto-Validate";

            this.btnUpdateServer.Location = new System.Drawing.Point(8, 34);
            this.btnUpdateServer.Name = "btnUpdateServer";
            this.btnUpdateServer.Size = new System.Drawing.Size(100, 23);
            this.btnUpdateServer.TabIndex = 3;
            this.btnUpdateServer.Text = "Update Server";
            this.btnUpdateServer.UseVisualStyleBackColor = true;
            this.btnUpdateServer.Click += new System.EventHandler(this.btnUpdateServer_Click);

            this.btnInstallMod.Location = new System.Drawing.Point(220, 34);
            this.btnInstallMod.Name = "btnInstallMod";
            this.btnInstallMod.Size = new System.Drawing.Size(100, 23);
            this.btnInstallMod.TabIndex = 4;
            this.btnInstallMod.Text = "Install Mod";
            this.btnInstallMod.UseVisualStyleBackColor = true;
            this.btnInstallMod.Click += new System.EventHandler(this.btnInstallMod_Click);

            // BackupRestoreTab
            this.BackupRestoreTab.Controls.Add(this.btnCreateBackup);
            this.BackupRestoreTab.Controls.Add(this.btnRestoreBackup);
            this.BackupRestoreTab.Controls.Add(this.txtBackupSource);
            this.BackupRestoreTab.Controls.Add(this.txtBackupDest);
            this.BackupRestoreTab.Location = new System.Drawing.Point(4, 22);
            this.BackupRestoreTab.Name = "BackupRestoreTab";
            this.BackupRestoreTab.Size = new System.Drawing.Size(776, 402);
            this.BackupRestoreTab.TabIndex = 4;
            this.BackupRestoreTab.Text = "Backup / Restore";
            this.BackupRestoreTab.UseVisualStyleBackColor = true;

            this.txtBackupSource.Location = new System.Drawing.Point(8, 8);
            this.txtBackupSource.Size = new System.Drawing.Size(150, 20);
            this.txtBackupSource.Text = "Source Folder";
            this.txtBackupSource.Name = "txtBackupSource";

            this.txtBackupDest.Location = new System.Drawing.Point(164, 8);
            this.txtBackupDest.Size = new System.Drawing.Size(150, 20);
            this.txtBackupDest.Text = "Destination Folder";
            this.txtBackupDest.Name = "txtBackupDest";

            this.btnCreateBackup.Location = new System.Drawing.Point(8, 34);
            this.btnCreateBackup.Name = "btnCreateBackup";
            this.btnCreateBackup.Size = new System.Drawing.Size(100, 23);
            this.btnCreateBackup.Text = "Create Backup";
            this.btnCreateBackup.Click += new System.EventHandler(this.btnCreateBackup_Click);

            this.btnRestoreBackup.Location = new System.Drawing.Point(114, 34);
            this.btnRestoreBackup.Name = "btnRestoreBackup";
            this.btnRestoreBackup.Size = new System.Drawing.Size(100, 23);
            this.btnRestoreBackup.Text = "Restore Backup";
            this.btnRestoreBackup.Click += new System.EventHandler(this.btnRestoreBackup_Click);

            // ConfigEditorTab
            this.ConfigEditorTab.Controls.Add(this.btnOpenConfig);
            this.ConfigEditorTab.Controls.Add(this.btnSaveConfig);
            this.ConfigEditorTab.Controls.Add(this.txtConfigArea);
            this.ConfigEditorTab.Location = new System.Drawing.Point(4, 22);
            this.ConfigEditorTab.Name = "ConfigEditorTab";
            this.ConfigEditorTab.Size = new System.Drawing.Size(776, 402);
            this.ConfigEditorTab.TabIndex = 5;
            this.ConfigEditorTab.Text = "Config Editor";
            this.ConfigEditorTab.UseVisualStyleBackColor = true;

            this.btnOpenConfig.Location = new System.Drawing.Point(8, 8);
            this.btnOpenConfig.Name = "btnOpenConfig";
            this.btnOpenConfig.Size = new System.Drawing.Size(100, 23);
            this.btnOpenConfig.Text = "Open Config...";
            this.btnOpenConfig.Click += new System.EventHandler(this.btnOpenConfig_Click);

            this.btnSaveConfig.Location = new System.Drawing.Point(114, 8);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(100, 23);
            this.btnSaveConfig.Text = "Save Config...";
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);

            this.txtConfigArea.Location = new System.Drawing.Point(8, 37);
            this.txtConfigArea.Size = new System.Drawing.Size(760, 350);
            this.txtConfigArea.Name = "txtConfigArea";

            // WorkshopManagerTab
            this.WorkshopManagerTab.Controls.Add(this.btnSearchWorkshop);
            this.WorkshopManagerTab.Controls.Add(this.txtWorkshopSearch);
            this.WorkshopManagerTab.Controls.Add(this.lstWorkshopResults);
            this.WorkshopManagerTab.Location = new System.Drawing.Point(4, 22);
            this.WorkshopManagerTab.Name = "WorkshopManagerTab";
            this.WorkshopManagerTab.Size = new System.Drawing.Size(776, 402);
            this.WorkshopManagerTab.TabIndex = 6;
            this.WorkshopManagerTab.Text = "Workshop Manager";
            this.WorkshopManagerTab.UseVisualStyleBackColor = true;

            this.txtWorkshopSearch.Location = new System.Drawing.Point(8, 8);
            this.txtWorkshopSearch.Size = new System.Drawing.Size(200, 20);
            this.txtWorkshopSearch.Name = "txtWorkshopSearch";

            this.btnSearchWorkshop.Location = new System.Drawing.Point(214, 6);
            this.btnSearchWorkshop.Name = "btnSearchWorkshop";
            this.btnSearchWorkshop.Size = new System.Drawing.Size(100, 23);
            this.btnSearchWorkshop.Text = "Search";
            this.btnSearchWorkshop.Click += new System.EventHandler(this.btnSearchWorkshop_Click);

            this.lstWorkshopResults.Location = new System.Drawing.Point(8, 35);
            this.lstWorkshopResults.Size = new System.Drawing.Size(760, 350);
            this.lstWorkshopResults.Name = "lstWorkshopResults";

            // ServerManagerTab
            this.ServerManagerTab.Controls.Add(this.btnStartServer);
            this.ServerManagerTab.Controls.Add(this.btnStopServer);
            this.ServerManagerTab.Location = new System.Drawing.Point(4, 22);
            this.ServerManagerTab.Name = "ServerManagerTab";
            this.ServerManagerTab.Size = new System.Drawing.Size(776, 402);
            this.ServerManagerTab.TabIndex = 7;
            this.ServerManagerTab.Text = "Server Manager";
            this.ServerManagerTab.UseVisualStyleBackColor = true;

            this.btnStartServer.Location = new System.Drawing.Point(8, 8);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(100, 23);
            this.btnStartServer.Text = "Start Server";
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);

            this.btnStopServer.Location = new System.Drawing.Point(120, 8);
            this.btnStopServer.Name = "btnStopServer";
            this.btnStopServer.Size = new System.Drawing.Size(100, 23);
            this.btnStopServer.Text = "Stop Server";
            this.btnStopServer.Click += new System.EventHandler(this.btnStopServer_Click);

            // ProfilesTab
            this.ProfilesTab.Controls.Add(this.cmbProfiles);
            this.ProfilesTab.Controls.Add(this.btnSaveProfile);
            this.ProfilesTab.Controls.Add(this.btnLoadProfile);
            this.ProfilesTab.Location = new System.Drawing.Point(4, 22);
            this.ProfilesTab.Name = "ProfilesTab";
            this.ProfilesTab.Size = new System.Drawing.Size(776, 402);
            this.ProfilesTab.TabIndex = 8;
            this.ProfilesTab.Text = "Profiles";
            this.ProfilesTab.UseVisualStyleBackColor = true;

            this.cmbProfiles.Location = new System.Drawing.Point(8, 8);
            this.cmbProfiles.Size = new System.Drawing.Size(200, 20);
            this.cmbProfiles.Name = "cmbProfiles";

            this.btnSaveProfile.Location = new System.Drawing.Point(214, 6);
            this.btnSaveProfile.Name = "btnSaveProfile";
            this.btnSaveProfile.Size = new System.Drawing.Size(100, 23);
            this.btnSaveProfile.Text = "Save Profile";
            this.btnSaveProfile.Click += new System.EventHandler(this.btnSaveProfile_Click);

            this.btnLoadProfile.Location = new System.Drawing.Point(320, 6);
            this.btnLoadProfile.Name = "btnLoadProfile";
            this.btnLoadProfile.Size = new System.Drawing.Size(100, 23);
            this.btnLoadProfile.Text = "Load Profile";
            this.btnLoadProfile.Click += new System.EventHandler(this.btnLoadProfile_Click);

            // SettingsTab
            this.SettingsTab.Controls.Add(this.chkAutoUpdate);
            this.SettingsTab.Location = new System.Drawing.Point(4, 22);
            this.SettingsTab.Name = "SettingsTab";
            this.SettingsTab.Size = new System.Drawing.Size(776, 402);
            this.SettingsTab.TabIndex = 9;
            this.SettingsTab.Text = "Settings";
            this.SettingsTab.UseVisualStyleBackColor = true;

            this.chkAutoUpdate.Location = new System.Drawing.Point(8, 8);
            this.chkAutoUpdate.Size = new System.Drawing.Size(150, 20);
            this.chkAutoUpdate.Name = "chkAutoUpdate";
            this.chkAutoUpdate.Text = "Auto Update App";

            // RconTab
            this.RconTab.Controls.Add(this.txtRconIp);
            this.RconTab.Controls.Add(this.txtRconPort);
            this.RconTab.Controls.Add(this.txtRconPassword);
            this.RconTab.Controls.Add(this.btnRconConnect);
            this.RconTab.Controls.Add(this.txtRconCommand);
            this.RconTab.Controls.Add(this.btnRconSend);
            this.RconTab.Location = new System.Drawing.Point(4, 22);
            this.RconTab.Name = "RconTab";
            this.RconTab.Size = new System.Drawing.Size(776, 402);
            this.RconTab.TabIndex = 2;
            this.RconTab.Text = "RCON";
            this.RconTab.UseVisualStyleBackColor = true;

            this.txtRconIp.Location = new System.Drawing.Point(8, 8);
            this.txtRconIp.Name = "txtRconIp";
            this.txtRconIp.Size = new System.Drawing.Size(100, 20);
            this.txtRconIp.TabIndex = 0;
            this.txtRconIp.Text = "127.0.0.1";

            this.txtRconPort.Location = new System.Drawing.Point(114, 8);
            this.txtRconPort.Name = "txtRconPort";
            this.txtRconPort.Size = new System.Drawing.Size(50, 20);
            this.txtRconPort.TabIndex = 1;
            this.txtRconPort.Text = "27015";

            this.txtRconPassword.Location = new System.Drawing.Point(170, 8);
            this.txtRconPassword.Name = "txtRconPassword";
            this.txtRconPassword.PasswordChar = '*';
            this.txtRconPassword.Size = new System.Drawing.Size(100, 20);
            this.txtRconPassword.TabIndex = 2;
            this.txtRconPassword.Text = "password";

            this.btnRconConnect.Location = new System.Drawing.Point(276, 6);
            this.btnRconConnect.Name = "btnRconConnect";
            this.btnRconConnect.Size = new System.Drawing.Size(75, 23);
            this.btnRconConnect.TabIndex = 3;
            this.btnRconConnect.Text = "Connect";
            this.btnRconConnect.UseVisualStyleBackColor = true;
            this.btnRconConnect.Click += new System.EventHandler(this.btnRconConnect_Click);

            this.txtRconCommand.Location = new System.Drawing.Point(8, 34);
            this.txtRconCommand.Name = "txtRconCommand";
            this.txtRconCommand.Size = new System.Drawing.Size(262, 20);
            this.txtRconCommand.TabIndex = 4;

            this.btnRconSend.Location = new System.Drawing.Point(276, 32);
            this.btnRconSend.Name = "btnRconSend";
            this.btnRconSend.Size = new System.Drawing.Size(75, 23);
            this.btnRconSend.TabIndex = 5;
            this.btnRconSend.Text = "Send";
            this.btnRconSend.UseVisualStyleBackColor = true;
            this.btnRconSend.Click += new System.EventHandler(this.btnRconSend_Click);

            // ConsoleTab
            this.ConsoleTab.Controls.Add(this.btnLogSearch);
            this.ConsoleTab.Controls.Add(this.txtLogSearch);
            this.ConsoleTab.Controls.Add(this.ConsoleOutput);
            this.ConsoleTab.Location = new System.Drawing.Point(4, 22);
            this.ConsoleTab.Name = "ConsoleTab";
            this.ConsoleTab.Size = new System.Drawing.Size(776, 402);
            this.ConsoleTab.TabIndex = 3;
            this.ConsoleTab.Text = "Console (Log Viewer)";
            this.ConsoleTab.UseVisualStyleBackColor = true;

            this.txtLogSearch.Location = new System.Drawing.Point(8, 8);
            this.txtLogSearch.Size = new System.Drawing.Size(200, 20);
            this.txtLogSearch.Name = "txtLogSearch";

            this.btnLogSearch.Location = new System.Drawing.Point(214, 6);
            this.btnLogSearch.Name = "btnLogSearch";
            this.btnLogSearch.Size = new System.Drawing.Size(100, 23);
            this.btnLogSearch.Text = "Search Logs";
            this.btnLogSearch.Click += new System.EventHandler(this.btnLogSearch_Click);

            // ConsoleOutput
            this.ConsoleOutput.Location = new System.Drawing.Point(8, 34);
            this.ConsoleOutput.Name = "ConsoleOutput";
            this.ConsoleOutput.ReadOnly = true;
            this.ConsoleOutput.Size = new System.Drawing.Size(760, 360);
            this.ConsoleOutput.TabIndex = 0;
            this.ConsoleOutput.Text = "";

            // StatusStrip1
            this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.Status });
            this.StatusStrip1.Location = new System.Drawing.Point(0, 428);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.Size = new System.Drawing.Size(784, 22);
            this.StatusStrip1.TabIndex = 1;
            this.StatusStrip1.Text = "StatusStrip1";

            // Status
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(39, 17);
            this.Status.Text = "Ready";

            // MainMenu
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 450);
            this.Controls.Add(this.MainMenuStrip1);
            this.MainMenuStrip = this.MainMenuStrip1;
            this.Controls.Add(this.TabMenu);
            this.Controls.Add(this.StatusStrip1);
            this.Name = "SteamCMDMainMenu";
            this.Text = "SteamCMD GUI (C# Port - Full Recreation)";

            this.TabMenu.ResumeLayout(false);
            this.RunTab.ResumeLayout(false);
            this.UpdateTab.ResumeLayout(false);
            this.UpdateTab.PerformLayout();
            this.BackupRestoreTab.ResumeLayout(false);
            this.BackupRestoreTab.PerformLayout();
            this.ConfigEditorTab.ResumeLayout(false);
            this.WorkshopManagerTab.ResumeLayout(false);
            this.WorkshopManagerTab.PerformLayout();
            this.ServerManagerTab.ResumeLayout(false);
            this.ProfilesTab.ResumeLayout(false);
            this.SettingsTab.ResumeLayout(false);
            this.SettingsTab.PerformLayout();
            this.RconTab.ResumeLayout(false);
            this.RconTab.PerformLayout();
            this.ConsoleTab.ResumeLayout(false);
            this.ConsoleTab.PerformLayout();
            this.StatusStrip1.ResumeLayout(false);
            this.StatusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TabControl TabMenu;
        private System.Windows.Forms.TabPage RunTab;
        private System.Windows.Forms.Button btnDownloadSteamCMD;
        private System.Windows.Forms.TabPage UpdateTab;
        private System.Windows.Forms.TextBox txtAppId;
        private System.Windows.Forms.TextBox txtInstallDir;
        private System.Windows.Forms.TextBox txtModId;
        private System.Windows.Forms.Button btnUpdateServer;
        private System.Windows.Forms.Button btnInstallMod;
        private System.Windows.Forms.CheckBox chkAutoValidate;

        private System.Windows.Forms.TabPage BackupRestoreTab;
        private System.Windows.Forms.Button btnCreateBackup;
        private System.Windows.Forms.Button btnRestoreBackup;
        private System.Windows.Forms.TextBox txtBackupSource;
        private System.Windows.Forms.TextBox txtBackupDest;

        private System.Windows.Forms.TabPage ConfigEditorTab;
        private System.Windows.Forms.Button btnOpenConfig;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.RichTextBox txtConfigArea;

        private System.Windows.Forms.TabPage WorkshopManagerTab;
        private System.Windows.Forms.Button btnSearchWorkshop;
        private System.Windows.Forms.TextBox txtWorkshopSearch;
        private System.Windows.Forms.ListBox lstWorkshopResults;

        private System.Windows.Forms.TabPage ServerManagerTab;
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.Button btnStopServer;

        private System.Windows.Forms.TabPage ProfilesTab;
        private System.Windows.Forms.ComboBox cmbProfiles;
        private System.Windows.Forms.Button btnSaveProfile;
        private System.Windows.Forms.Button btnLoadProfile;

        private System.Windows.Forms.TabPage SettingsTab;
        private System.Windows.Forms.CheckBox chkAutoUpdate;

        private System.Windows.Forms.TabPage RconTab;
        private System.Windows.Forms.TextBox txtRconIp;
        private System.Windows.Forms.TextBox txtRconPort;
        private System.Windows.Forms.TextBox txtRconPassword;
        private System.Windows.Forms.TextBox txtRconCommand;
        private System.Windows.Forms.Button btnRconConnect;
        private System.Windows.Forms.Button btnRconSend;
        private System.Windows.Forms.TabPage ConsoleTab;
        private System.Windows.Forms.RichTextBox ConsoleOutput;
        private System.Windows.Forms.TextBox txtLogSearch;
        private System.Windows.Forms.Button btnLogSearch;
        private System.Windows.Forms.StatusStrip StatusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Status;
        private System.Windows.Forms.MenuStrip MainMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commandsToolStripMenuItem;
    }
}
