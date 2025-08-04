<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainMenu
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose( disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainMenu))
        Me.GamesList = New System.Windows.Forms.ComboBox()
        Me.SteamCMDDownloadButton = New System.Windows.Forms.Button()
        Me.DonwloadBar = New System.Windows.Forms.ProgressBar()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.OpenFolderButton = New System.Windows.Forms.Button()
        Me.CheckUpdatesButton = New System.Windows.Forms.Button()
        Me.VDCButton = New System.Windows.Forms.Button()
        Me.AboutButton = New System.Windows.Forms.Button()
        Me.ExitButton = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.AddCustomGameButton = New System.Windows.Forms.Button()
        Me.CheckBoxConsole = New System.Windows.Forms.CheckBox()
        Me.ValidateCheckBox = New System.Windows.Forms.CheckBox()
        Me.IdHelpButton = New System.Windows.Forms.Button()
        Me.BrowserButton = New System.Windows.Forms.Button()
        Me.UpdateServerButton = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ServerPath = New System.Windows.Forms.TextBox()
        Me.AnonymousCheckBox = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.UsernameTextBox = New System.Windows.Forms.TextBox()
        Me.PasswdTextBox = New System.Windows.Forms.MaskedTextBox()
        Me.GoldSrcModLabel = New System.Windows.Forms.Label()
        Me.GoldSrcModInput = New System.Windows.Forms.TextBox()
        Me.Status = New System.Windows.Forms.TextBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.ESButton = New System.Windows.Forms.Button()
        Me.MMButton = New System.Windows.Forms.Button()
        Me.SMButton = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.ExeBrowserButton = New System.Windows.Forms.Button()
        Me.ExePath = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.LogViewerTab = New System.Windows.Forms.TabPage()
        Me.SearchLabel = New System.Windows.Forms.Label()
        Me.SearchTextBox = New System.Windows.Forms.TextBox()
        Me.SearchButton = New System.Windows.Forms.Button()
        Me.AutoScrollCheckBox = New System.Windows.Forms.CheckBox()
        Me.LogOutputTextBox = New System.Windows.Forms.RichTextBox()
        Me.RconTab = New System.Windows.Forms.TabPage()
        Me.RconConnectionGroupBox = New System.Windows.Forms.GroupBox()
        Me.HostTextBox = New System.Windows.Forms.TextBox()
        Me.PortTextBox = New System.Windows.Forms.TextBox()
        Me.PasswordTextBox = New System.Windows.Forms.MaskedTextBox()
        Me.HostLabel = New System.Windows.Forms.Label()
        Me.PortLabel = New System.Windows.Forms.Label()
        Me.PasswordLabel = New System.Windows.Forms.Label()
        Me.RconConnectButton = New System.Windows.Forms.Button()
        Me.RconDisconnectButton = New System.Windows.Forms.Button()
        Me.RconConsoleGroupBox = New System.Windows.Forms.GroupBox()
        Me.RconSendCommand = New System.Windows.Forms.Button()
        Me.RconCommandTextBox = New System.Windows.Forms.TextBox()
        Me.RconOutputTextBox = New System.Windows.Forms.RichTextBox()
        Me.ConfigEditorTab = New System.Windows.Forms.TabPage()
        Me.ConfigTreeView = New System.Windows.Forms.TreeView()
        Me.ConfigEditorTextBox = New System.Windows.Forms.RichTextBox()
        Me.ConfigOpenButton = New System.Windows.Forms.Button()
        Me.ConfigRefreshButton = New System.Windows.Forms.Button()
        Me.ConfigSaveButton = New System.Windows.Forms.Button()
        Me.BackupRestoreTab = New System.Windows.Forms.TabPage()
        Me.CreateBackupGroupBox = New System.Windows.Forms.GroupBox()
        Me.SelectSourceButton = New System.Windows.Forms.Button()
        Me.SelectDestinationButton = New System.Windows.Forms.Button()
        Me.CreateBackupButton = New System.Windows.Forms.Button()
        Me.RestoreBackupGroupBox = New System.Windows.Forms.GroupBox()
        Me.BackupListBox = New System.Windows.Forms.ListBox()
        Me.RestoreBackupButton = New System.Windows.Forms.Button()
        Me.DeleteBackupButton = New System.Windows.Forms.Button()
        Me.BackupScheduleGroupBox = New System.Windows.Forms.GroupBox()
        Me.EnableScheduleCheckBox = New System.Windows.Forms.CheckBox()
        Me.WorkshopModsTab = New System.Windows.Forms.TabPage()
        Me.ModEnableButton = New System.Windows.Forms.Button()
        Me.ModDisableButton = New System.Windows.Forms.Button()
        Me.ModUpdateButton = New System.Windows.Forms.Button()
        Me.ModUnsubscribeButton = New System.Windows.Forms.Button()
        Me.ModSubscribeButton = New System.Windows.Forms.Button()
        Me.ModSearchButton = New System.Windows.Forms.Button()
        Me.ModSearchTextBox = New System.Windows.Forms.TextBox()
        Me.ModListLabel = New System.Windows.Forms.Label()
        Me.ModSearchLabel = New System.Windows.Forms.Label()
        Me.ModListBox = New System.Windows.Forms.ListBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolsMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.CommonFilesMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.MotdTxtButton = New System.Windows.Forms.ToolStripMenuItem()
        Me.MapcycleTxtButton = New System.Windows.Forms.ToolStripMenuItem()
        Me.MaplistTxtButton = New System.Windows.Forms.ToolStripMenuItem()
        Me.CFGMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.NewFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.SMMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.Empty = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogMenu = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.XmlConfigOpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.DonateButton = New System.Windows.Forms.PictureBox()
        Me.IPTextbox = New System.Windows.Forms.TextBox()
        Me.IPButton = New System.Windows.Forms.Button()
        Me.UpdatesTab = New System.Windows.Forms.TabPage()
        Me.UpdatesLabel = New System.Windows.Forms.Label()
        Me.CheckForUpdatesButton = New System.Windows.Forms.Button()
        Me.InstallUpdateButton = New System.Windows.Forms.Button()
        Me.UpdatesListBox = New System.Windows.Forms.ListBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.LogViewerTab.SuspendLayout()
        Me.RconTab.SuspendLayout()
        Me.RconConnectionGroupBox.SuspendLayout()
        Me.RconConsoleGroupBox.SuspendLayout()
        Me.ConfigEditorTab.SuspendLayout()
        Me.BackupRestoreTab.SuspendLayout()
        Me.CreateBackupGroupBox.SuspendLayout()
        Me.RestoreBackupGroupBox.SuspendLayout()
        Me.BackupScheduleGroupBox.SuspendLayout()
        Me.WorkshopModsTab.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.DonateButton, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UpdatesTab.SuspendLayout()
        Me.SuspendLayout()
        '
        'GamesList
        '
        Me.GamesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.GamesList.FormattingEnabled = True
        resources.ApplyResources(Me.GamesList, "GamesList")
        Me.GamesList.Items.AddRange(New Object() {resources.GetString("GamesList.Items"), resources.GetString("GamesList.Items1"), resources.GetString("GamesList.Items2"), resources.GetString("GamesList.Items3"), resources.GetString("GamesList.Items4"), resources.GetString("GamesList.Items5"), resources.GetString("GamesList.Items6"), resources.GetString("GamesList.Items7"), resources.GetString("GamesList.Items8"), resources.GetString("GamesList.Items9"), resources.GetString("GamesList.Items10")})
        Me.GamesList.Name = "GamesList"
        '
        'SteamCMDDownloadButton
        '
        resources.ApplyResources(Me.SteamCMDDownloadButton, "SteamCMDDownloadButton")
        Me.SteamCMDDownloadButton.Name = "SteamCMDDownloadButton"
        Me.SteamCMDDownloadButton.UseVisualStyleBackColor = True
        '
        'DonwloadBar
        '
        resources.ApplyResources(Me.DonwloadBar, "DonwloadBar")
        Me.DonwloadBar.Name = "DonwloadBar"
        Me.DonwloadBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.OpenFolderButton)
        Me.GroupBox1.Controls.Add(Me.SteamCMDDownloadButton)
        Me.GroupBox1.Controls.Add(Me.CheckUpdatesButton)
        Me.GroupBox1.Controls.Add(Me.VDCButton)
        resources.ApplyResources(Me.GroupBox1, "GroupBox1")
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.TabStop = False
        '
        'OpenFolderButton
        '
        Me.OpenFolderButton.BackgroundImage = Global.SteamCMD_GUI.My.Resources.Resources.Folder
        resources.ApplyResources(Me.OpenFolderButton, "OpenFolderButton")
        Me.OpenFolderButton.Name = "OpenFolderButton"
        Me.OpenFolderButton.TabStop = False
        Me.OpenFolderButton.UseVisualStyleBackColor = True
        '
        'CheckUpdatesButton
        '
        resources.ApplyResources(Me.CheckUpdatesButton, "CheckUpdatesButton")
        Me.CheckUpdatesButton.Name = "CheckUpdatesButton"
        Me.CheckUpdatesButton.UseVisualStyleBackColor = True
        '
        'VDCButton
        '
        resources.ApplyResources(Me.VDCButton, "VDCButton")
        Me.VDCButton.Name = "VDCButton"
        Me.VDCButton.UseVisualStyleBackColor = True
        '
        'AboutButton
        '
        resources.ApplyResources(Me.AboutButton, "AboutButton")
        Me.AboutButton.Name = "AboutButton"
        Me.AboutButton.UseVisualStyleBackColor = True
        '
        'ExitButton
        '
        resources.ApplyResources(Me.ExitButton, "ExitButton")
        Me.ExitButton.Name = "ExitButton"
        Me.ExitButton.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.AddCustomGameButton)
        Me.GroupBox2.Controls.Add(Me.CheckBoxConsole)
        Me.GroupBox2.Controls.Add(Me.ValidateCheckBox)
        Me.GroupBox2.Controls.Add(Me.IdHelpButton)
        Me.GroupBox2.Controls.Add(Me.BrowserButton)
        Me.GroupBox2.Controls.Add(Me.UpdateServerButton)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.ServerPath)
        Me.GroupBox2.Controls.Add(Me.AnonymousCheckBox)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.UsernameTextBox)
        Me.GroupBox2.Controls.Add(Me.PasswdTextBox)
        Me.GroupBox2.Controls.Add(Me.GamesList)
        Me.GroupBox2.Controls.Add(Me.GoldSrcModLabel)
        Me.GroupBox2.Controls.Add(Me.GoldSrcModInput)
        resources.ApplyResources(Me.GroupBox2, "GroupBox2")
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.TabStop = False
        '
        'AddCustomGameButton
        '
        resources.ApplyResources(Me.AddCustomGameButton, "AddCustomGameButton")
        Me.AddCustomGameButton.Name = "AddCustomGameButton"
        Me.AddCustomGameButton.UseVisualStyleBackColor = True
        '
        'CheckBoxConsole
        '
        resources.ApplyResources(Me.CheckBoxConsole, "CheckBoxConsole")
        Me.CheckBoxConsole.Name = "CheckBoxConsole"
        Me.CheckBoxConsole.UseVisualStyleBackColor = True
        '
        'ValidateCheckBox
        '
        resources.ApplyResources(Me.ValidateCheckBox, "ValidateCheckBox")
        Me.ValidateCheckBox.Checked = True
        Me.ValidateCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ValidateCheckBox.Name = "ValidateCheckBox"
        Me.ValidateCheckBox.TabStop = False
        Me.ValidateCheckBox.UseVisualStyleBackColor = True
        '
        'IdHelpButton
        '
        Me.IdHelpButton.BackgroundImage = Global.SteamCMD_GUI.My.Resources.Resources.Help
        resources.ApplyResources(Me.IdHelpButton, "IdHelpButton")
        Me.IdHelpButton.Name = "IdHelpButton"
        Me.IdHelpButton.TabStop = False
        Me.IdHelpButton.UseVisualStyleBackColor = False
        '
        'BrowserButton
        '
        resources.ApplyResources(Me.BrowserButton, "BrowserButton")
        Me.BrowserButton.Name = "BrowserButton"
        Me.BrowserButton.UseVisualStyleBackColor = True
        '
        'UpdateServerButton
        '
        resources.ApplyResources(Me.UpdateServerButton, "UpdateServerButton")
        Me.UpdateServerButton.Name = "UpdateServerButton"
        Me.UpdateServerButton.UseVisualStyleBackColor = True
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'ServerPath
        '
        resources.ApplyResources(Me.ServerPath, "ServerPath")
        Me.ServerPath.Name = "ServerPath"
        Me.ServerPath.TabStop = False
        '
        'AnonymousCheckBox
        '
        resources.ApplyResources(Me.AnonymousCheckBox, "AnonymousCheckBox")
        Me.AnonymousCheckBox.Checked = True
        Me.AnonymousCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.AnonymousCheckBox.Name = "AnonymousCheckBox"
        Me.AnonymousCheckBox.UseVisualStyleBackColor = True
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'UsernameTextBox
        '
        resources.ApplyResources(Me.UsernameTextBox, "UsernameTextBox")
        Me.UsernameTextBox.Name = "UsernameTextBox"
        '
        'PasswdTextBox
        '
        resources.ApplyResources(Me.PasswdTextBox, "PasswdTextBox")
        Me.PasswdTextBox.Name = "PasswdTextBox"
        Me.PasswdTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        '
        'GoldSrcModLabel
        '
        resources.ApplyResources(Me.GoldSrcModLabel, "GoldSrcModLabel")
        Me.GoldSrcModLabel.Name = "GoldSrcModLabel"
        '
        'GoldSrcModInput
        '
        resources.ApplyResources(Me.GoldSrcModInput, "GoldSrcModInput")
        Me.GoldSrcModInput.Name = "GoldSrcModInput"
        '
        'Status
        '
        Me.Status.BackColor = System.Drawing.SystemColors.Control
        resources.ApplyResources(Me.Status, "Status")
        Me.Status.ForeColor = System.Drawing.SystemColors.WindowText
        Me.Status.Name = "Status"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.ESButton)
        Me.GroupBox3.Controls.Add(Me.MMButton)
        Me.GroupBox3.Controls.Add(Me.SMButton)
        resources.ApplyResources(Me.GroupBox3, "GroupBox3")
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.TabStop = False
        '
        'ESButton
        '
        resources.ApplyResources(Me.ESButton, "ESButton")
        Me.ESButton.Name = "ESButton"
        Me.ESButton.UseVisualStyleBackColor = True
        '
        'MMButton
        '
        resources.ApplyResources(Me.MMButton, "MMButton")
        Me.MMButton.Name = "MMButton"
        Me.MMButton.UseVisualStyleBackColor = True
        '
        'SMButton
        '
        resources.ApplyResources(Me.SMButton, "SMButton")
        Me.SMButton.Name = "SMButton"
        Me.SMButton.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.ExeBrowserButton)
        Me.GroupBox4.Controls.Add(Me.ExePath)
        Me.GroupBox4.Controls.Add(Me.Label4)
        resources.ApplyResources(Me.GroupBox4, "GroupBox4")
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.TabStop = False
        '
        'ExeBrowserButton
        '
        resources.ApplyResources(Me.ExeBrowserButton, "ExeBrowserButton")
        Me.ExeBrowserButton.Name = "ExeBrowserButton"
        Me.ExeBrowserButton.UseVisualStyleBackColor = True
        '
        'ExePath
        '
        resources.ApplyResources(Me.ExePath, "ExePath")
        Me.ExePath.Name = "ExePath"
        Me.ExePath.TabStop = False
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.LogViewerTab)
        Me.TabControl1.Controls.Add(Me.RconTab)
        Me.TabControl1.Controls.Add(Me.ConfigEditorTab)
        Me.TabControl1.Controls.Add(Me.BackupRestoreTab)
        Me.TabControl1.Controls.Add(Me.WorkshopModsTab)
        Me.TabControl1.Controls.Add(Me.UpdatesTab)
        Me.TabControl1.Location = New System.Drawing.Point(12, 36)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(776, 402)
        Me.TabControl1.TabIndex = 1
        '
        'LogViewerTab
        '
        Me.LogViewerTab.Controls.Add(Me.SearchLabel)
        Me.LogViewerTab.Controls.Add(Me.SearchTextBox)
        Me.LogViewerTab.Controls.Add(Me.SearchButton)
        Me.LogViewerTab.Controls.Add(Me.AutoScrollCheckBox)
        Me.LogViewerTab.Controls.Add(Me.LogOutputTextBox)
        Me.LogViewerTab.Location = New System.Drawing.Point(4, 22)
        Me.LogViewerTab.Name = "LogViewerTab"
        Me.LogViewerTab.Padding = New System.Windows.Forms.Padding(3)
        Me.LogViewerTab.Size = New System.Drawing.Size(768, 376)
        Me.LogViewerTab.TabIndex = 0
        Me.LogViewerTab.Text = "Log Viewer"
        Me.LogViewerTab.UseVisualStyleBackColor = True
        '
        'SearchLabel
        '
        Me.SearchLabel.AutoSize = True
        Me.SearchLabel.Location = New System.Drawing.Point(6, 10)
        Me.SearchLabel.Name = "SearchLabel"
        Me.SearchLabel.Size = New System.Drawing.Size(41, 13)
        Me.SearchLabel.TabIndex = 0
        Me.SearchLabel.Text = "Search"
        '
        'SearchTextBox
        '
        Me.SearchTextBox.Location = New System.Drawing.Point(53, 7)
        Me.SearchTextBox.Name = "SearchTextBox"
        Me.SearchTextBox.Size = New System.Drawing.Size(200, 20)
        Me.SearchTextBox.TabIndex = 1
        '
        'SearchButton
        '
        Me.SearchButton.Image = Global.SteamCMD_GUI.My.Resources.Resources.search_24
        Me.SearchButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.SearchButton.Location = New System.Drawing.Point(259, 5)
        Me.SearchButton.Name = "SearchButton"
        Me.SearchButton.Size = New System.Drawing.Size(75, 23)
        Me.SearchButton.TabIndex = 2
        Me.SearchButton.Text = "Find"
        Me.SearchButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.SearchButton.UseVisualStyleBackColor = True
        '
        'AutoScrollCheckBox
        '
        Me.AutoScrollCheckBox.AutoSize = True
        Me.AutoScrollCheckBox.Location = New System.Drawing.Point(340, 8)
        Me.AutoScrollCheckBox.Name = "AutoScrollCheckBox"
        Me.AutoScrollCheckBox.Size = New System.Drawing.Size(82, 17)
        Me.AutoScrollCheckBox.TabIndex = 3
        Me.AutoScrollCheckBox.Text = "Auto-Scroll"
        Me.AutoScrollCheckBox.UseVisualStyleBackColor = True
        '
        'LogOutputTextBox
        '
        Me.LogOutputTextBox.Location = New System.Drawing.Point(6, 34)
        Me.LogOutputTextBox.Name = "LogOutputTextBox"
        Me.LogOutputTextBox.Size = New System.Drawing.Size(756, 336)
        Me.LogOutputTextBox.TabIndex = 4
        Me.LogOutputTextBox.Text = ""
        '
        'RconTab
        '
        Me.RconTab.Controls.Add(Me.RconConnectionGroupBox)
        Me.RconTab.Controls.Add(Me.RconConsoleGroupBox)
        Me.RconTab.Location = New System.Drawing.Point(4, 22)
        Me.RconTab.Name = "RconTab"
        Me.RconTab.Padding = New System.Windows.Forms.Padding(3)
        Me.RconTab.Size = New System.Drawing.Size(768, 376)
        Me.RconTab.TabIndex = 1
        Me.RconTab.Text = "RCON Client"
        Me.RconTab.UseVisualStyleBackColor = True
        '
        'RconConnectionGroupBox
        '
        Me.RconConnectionGroupBox.Controls.Add(Me.RconDisconnectButton)
        Me.RconConnectionGroupBox.Controls.Add(Me.RconConnectButton)
        Me.RconConnectionGroupBox.Controls.Add(Me.PasswordLabel)
        Me.RconConnectionGroupBox.Controls.Add(Me.PortLabel)
        Me.RconConnectionGroupBox.Controls.Add(Me.HostLabel)
        Me.RconConnectionGroupBox.Controls.Add(Me.PasswordTextBox)
        Me.RconConnectionGroupBox.Controls.Add(Me.PortTextBox)
        Me.RconConnectionGroupBox.Controls.Add(Me.HostTextBox)
        Me.RconConnectionGroupBox.Location = New System.Drawing.Point(6, 6)
        Me.RconConnectionGroupBox.Name = "RconConnectionGroupBox"
        Me.RconConnectionGroupBox.Size = New System.Drawing.Size(250, 150)
        Me.RconConnectionGroupBox.TabIndex = 0
        Me.RconConnectionGroupBox.TabStop = False
        Me.RconConnectionGroupBox.Text = "Connection"
        '
        'HostTextBox
        '
        Me.HostTextBox.Location = New System.Drawing.Point(70, 19)
        Me.HostTextBox.Name = "HostTextBox"
        Me.HostTextBox.Size = New System.Drawing.Size(174, 20)
        Me.HostTextBox.TabIndex = 0
        '
        'PortTextBox
        '
        Me.PortTextBox.Location = New System.Drawing.Point(70, 45)
        Me.PortTextBox.Name = "PortTextBox"
        Me.PortTextBox.Size = New System.Drawing.Size(174, 20)
        Me.PortTextBox.TabIndex = 1
        '
        'PasswordTextBox
        '
        Me.PasswordTextBox.Location = New System.Drawing.Point(70, 71)
        Me.PasswordTextBox.Name = "PasswordTextBox"
        Me.PasswordTextBox.Size = New System.Drawing.Size(174, 20)
        Me.PasswordTextBox.TabIndex = 2
        '
        'HostLabel
        '
        Me.HostLabel.AutoSize = True
        Me.HostLabel.Location = New System.Drawing.Point(6, 22)
        Me.HostLabel.Name = "HostLabel"
        Me.HostLabel.Size = New System.Drawing.Size(29, 13)
        Me.HostLabel.TabIndex = 3
        Me.HostLabel.Text = "Host"
        '
        'PortLabel
        '
        Me.PortLabel.AutoSize = True
        Me.PortLabel.Location = New System.Drawing.Point(6, 48)
        Me.PortLabel.Name = "PortLabel"
        Me.PortLabel.Size = New System.Drawing.Size(26, 13)
        Me.PortLabel.TabIndex = 4
        Me.PortLabel.Text = "Port"
        '
        'PasswordLabel
        '
        Me.PasswordLabel.AutoSize = True
        Me.PasswordLabel.Location = New System.Drawing.Point(6, 74)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(53, 13)
        Me.PasswordLabel.TabIndex = 5
        Me.PasswordLabel.Text = "Password"
        '
        'RconConnectButton
        '
        Me.RconConnectButton.Image = Global.SteamCMD_GUI.My.Resources.Resources.connect_24
        Me.RconConnectButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RconConnectButton.Location = New System.Drawing.Point(70, 97)
        Me.RconConnectButton.Name = "RconConnectButton"
        Me.RconConnectButton.Size = New System.Drawing.Size(85, 23)
        Me.RconConnectButton.TabIndex = 6
        Me.RconConnectButton.Text = "Connect"
        Me.RconConnectButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.RconConnectButton.UseVisualStyleBackColor = True
        '
        'RconDisconnectButton
        '
        Me.RconDisconnectButton.Image = Global.SteamCMD_GUI.My.Resources.Resources.clear_24
        Me.RconDisconnectButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RconDisconnectButton.Location = New System.Drawing.Point(161, 97)
        Me.RconDisconnectButton.Name = "RconDisconnectButton"
        Me.RconDisconnectButton.Size = New System.Drawing.Size(83, 23)
        Me.RconDisconnectButton.TabIndex = 7
        Me.RconDisconnectButton.Text = "Disconnect"
        Me.RconDisconnectButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.RconDisconnectButton.UseVisualStyleBackColor = True
        '
        'RconConsoleGroupBox
        '
        Me.RconConsoleGroupBox.Controls.Add(Me.RconSendCommand)
        Me.RconConsoleGroupBox.Controls.Add(Me.RconCommandTextBox)
        Me.RconConsoleGroupBox.Controls.Add(Me.RconOutputTextBox)
        Me.RconConsoleGroupBox.Location = New System.Drawing.Point(262, 6)
        Me.RconConsoleGroupBox.Name = "RconConsoleGroupBox"
        Me.RconConsoleGroupBox.Size = New System.Drawing.Size(500, 364)
        Me.RconConsoleGroupBox.TabIndex = 1
        Me.RconConsoleGroupBox.TabStop = False
        Me.RconConsoleGroupBox.Text = "Console"
        '
        'RconOutputTextBox
        '
        Me.RconOutputTextBox.Location = New System.Drawing.Point(6, 19)
        Me.RconOutputTextBox.Name = "RconOutputTextBox"
        Me.RconOutputTextBox.Size = New System.Drawing.Size(488, 310)
        Me.RconOutputTextBox.TabIndex = 0
        Me.RconOutputTextBox.Text = ""
        '
        'RconCommandTextBox
        '
        Me.RconCommandTextBox.Location = New System.Drawing.Point(6, 335)
        Me.RconCommandTextBox.Name = "RconCommandTextBox"
        Me.RconCommandTextBox.Size = New System.Drawing.Size(407, 20)
        Me.RconCommandTextBox.TabIndex = 1
        '
        'RconSendCommand
        '
        Me.RconSendCommand.Image = Global.SteamCMD_GUI.My.Resources.Resources.plus_24
        Me.RconSendCommand.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RconSendCommand.Location = New System.Drawing.Point(419, 333)
        Me.RconSendCommand.Name = "RconSendCommand"
        Me.RconSendCommand.Size = New System.Drawing.Size(75, 23)
        Me.RconSendCommand.TabIndex = 2
        Me.RconSendCommand.Text = "Send"
        Me.RconSendCommand.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.RconSendCommand.UseVisualStyleBackColor = True
        '
        'ConfigEditorTab
        '
        Me.ConfigEditorTab.Controls.Add(Me.ConfigTreeView)
        Me.ConfigEditorTab.Controls.Add(Me.ConfigEditorTextBox)
        Me.ConfigEditorTab.Controls.Add(Me.ConfigOpenButton)
        Me.ConfigEditorTab.Controls.Add(Me.ConfigRefreshButton)
        Me.ConfigEditorTab.Controls.Add(Me.ConfigSaveButton)
        Me.ConfigEditorTab.Location = New System.Drawing.Point(4, 22)
        Me.ConfigEditorTab.Name = "ConfigEditorTab"
        Me.ConfigEditorTab.Padding = New System.Windows.Forms.Padding(3)
        Me.ConfigEditorTab.Size = New System.Drawing.Size(776, 376)
        Me.ConfigEditorTab.TabIndex = 2
        Me.ConfigEditorTab.Text = "Config Editor"
        Me.ConfigEditorTab.UseVisualStyleBackColor = True
        '
        'ConfigTreeView
        '
        Me.ConfigTreeView.Location = New System.Drawing.Point(6, 35)
        Me.ConfigTreeView.Name = "ConfigTreeView"
        Me.ConfigTreeView.Size = New System.Drawing.Size(200, 335)
        Me.ConfigTreeView.TabIndex = 0
        '
        'ConfigEditorTextBox
        '
        Me.ConfigEditorTextBox.Location = New System.Drawing.Point(212, 35)
        Me.ConfigEditorTextBox.Name = "ConfigEditorTextBox"
        Me.ConfigEditorTextBox.Size = New System.Drawing.Size(550, 335)
        Me.ConfigEditorTextBox.TabIndex = 1
        Me.ConfigEditorTextBox.Text = ""
        '
        'ConfigOpenButton
        '
        Me.ConfigOpenButton.Image = Global.SteamCMD_GUI.My.Resources.Resources.folder_open_24
        Me.ConfigOpenButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ConfigOpenButton.Location = New System.Drawing.Point(6, 6)
        Me.ConfigOpenButton.Name = "ConfigOpenButton"
        Me.ConfigOpenButton.Size = New System.Drawing.Size(75, 23)
        Me.ConfigOpenButton.TabIndex = 2
        Me.ConfigOpenButton.Text = "Open"
        Me.ConfigOpenButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ConfigOpenButton.UseVisualStyleBackColor = True
        '
        'ConfigRefreshButton
        '
        Me.ConfigRefreshButton.Image = Global.SteamCMD_GUI.My.Resources.Resources.refresh_24
        Me.ConfigRefreshButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ConfigRefreshButton.Location = New System.Drawing.Point(87, 6)
        Me.ConfigRefreshButton.Name = "ConfigRefreshButton"
        Me.ConfigRefreshButton.Size = New System.Drawing.Size(75, 23)
        Me.ConfigRefreshButton.TabIndex = 3
        Me.ConfigRefreshButton.Text = "Refresh"
        Me.ConfigRefreshButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ConfigRefreshButton.UseVisualStyleBackColor = True
        '
        'ConfigSaveButton
        '
        Me.ConfigSaveButton.Image = Global.SteamCMD_GUI.My.Resources.Resources.save_24
        Me.ConfigSaveButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ConfigSaveButton.Location = New System.Drawing.Point(168, 6)
        Me.ConfigSaveButton.Name = "ConfigSaveButton"
        Me.ConfigSaveButton.Size = New System.Drawing.Size(75, 23)
        Me.ConfigSaveButton.TabIndex = 4
        Me.ConfigSaveButton.Text = "Save"
        Me.ConfigSaveButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ConfigSaveButton.UseVisualStyleBackColor = True
        '
        'BackupRestoreTab
        '
        Me.BackupRestoreTab.Controls.Add(Me.BackupScheduleGroupBox)
        Me.BackupRestoreTab.Controls.Add(Me.RestoreBackupGroupBox)
        Me.BackupRestoreTab.Controls.Add(Me.CreateBackupGroupBox)
        Me.BackupRestoreTab.Location = New System.Drawing.Point(4, 22)
        Me.BackupRestoreTab.Name = "BackupRestoreTab"
        Me.BackupRestoreTab.Padding = New System.Windows.Forms.Padding(3)
        Me.BackupRestoreTab.Size = New System.Drawing.Size(776, 376)
        Me.BackupRestoreTab.TabIndex = 3
        Me.BackupRestoreTab.Text = "Backup/Restore"
        Me.BackupRestoreTab.UseVisualStyleBackColor = True
        '
        'CreateBackupGroupBox
        '
        Me.CreateBackupGroupBox.Controls.Add(Me.CreateBackupButton)
        Me.CreateBackupGroupBox.Controls.Add(Me.SelectDestinationButton)
        Me.CreateBackupGroupBox.Controls.Add(Me.SelectSourceButton)
        Me.CreateBackupGroupBox.Location = New System.Drawing.Point(6, 6)
        Me.CreateBackupGroupBox.Name = "CreateBackupGroupBox"
        Me.CreateBackupGroupBox.Size = New System.Drawing.Size(200, 120)
        Me.CreateBackupGroupBox.TabIndex = 0
        Me.CreateBackupGroupBox.TabStop = False
        Me.CreateBackupGroupBox.Text = "Create Backup"
        '
        'SelectSourceButton
        '
        Me.SelectSourceButton.Location = New System.Drawing.Point(6, 19)
        Me.SelectSourceButton.Name = "SelectSourceButton"
        Me.SelectSourceButton.Size = New System.Drawing.Size(188, 23)
        Me.SelectSourceButton.TabIndex = 0
        Me.SelectSourceButton.Text = "Select Source"
        Me.SelectSourceButton.UseVisualStyleBackColor = True
        '
        'SelectDestinationButton
        '
        Me.SelectDestinationButton.Location = New System.Drawing.Point(6, 48)
        Me.SelectDestinationButton.Name = "SelectDestinationButton"
        Me.SelectDestinationButton.Size = New System.Drawing.Size(188, 23)
        Me.SelectDestinationButton.TabIndex = 1
        Me.SelectDestinationButton.Text = "Select Destination"
        Me.SelectDestinationButton.UseVisualStyleBackColor = True
        '
        'CreateBackupButton
        '
        Me.CreateBackupButton.Image = Global.SteamCMD_GUI.My.Resources.Resources.backup_24
        Me.CreateBackupButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CreateBackupButton.Location = New System.Drawing.Point(6, 77)
        Me.CreateBackupButton.Name = "CreateBackupButton"
        Me.CreateBackupButton.Size = New System.Drawing.Size(188, 23)
        Me.CreateBackupButton.TabIndex = 2
        Me.CreateBackupButton.Text = "Create Backup"
        Me.CreateBackupButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CreateBackupButton.UseVisualStyleBackColor = True
        '
        'RestoreBackupGroupBox
        '
        Me.RestoreBackupGroupBox.Controls.Add(Me.DeleteBackupButton)
        Me.RestoreBackupGroupBox.Controls.Add(Me.RestoreBackupButton)
        Me.RestoreBackupGroupBox.Controls.Add(Me.BackupListBox)
        Me.RestoreBackupGroupBox.Location = New System.Drawing.Point(212, 6)
        Me.RestoreBackupGroupBox.Name = "RestoreBackupGroupBox"
        Me.RestoreBackupGroupBox.Size = New System.Drawing.Size(550, 200)
        Me.RestoreBackupGroupBox.TabIndex = 1
        Me.RestoreBackupGroupBox.TabStop = False
        Me.RestoreBackupGroupBox.Text = "Restore Backup"
        '
        'BackupListBox
        '
        Me.BackupListBox.FormattingEnabled = True
        Me.BackupListBox.Location = New System.Drawing.Point(6, 19)
        Me.BackupListBox.Name = "BackupListBox"
        Me.BackupListBox.Size = New System.Drawing.Size(538, 147)
        Me.BackupListBox.TabIndex = 0
        '
        'RestoreBackupButton
        '
        Me.RestoreBackupButton.Image = Global.SteamCMD_GUI.My.Resources.Resources.restore_24
        Me.RestoreBackupButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.RestoreBackupButton.Location = New System.Drawing.Point(6, 171)
        Me.RestoreBackupButton.Name = "RestoreBackupButton"
        Me.RestoreBackupButton.Size = New System.Drawing.Size(120, 23)
        Me.RestoreBackupButton.TabIndex = 1
        Me.RestoreBackupButton.Text = "Restore Selected"
        Me.RestoreBackupButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.RestoreBackupButton.UseVisualStyleBackColor = True
        '
        'DeleteBackupButton
        '
        Me.DeleteBackupButton.Image = Global.SteamCMD_GUI.My.Resources.Resources.delete_24
        Me.DeleteBackupButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.DeleteBackupButton.Location = New System.Drawing.Point(132, 171)
        Me.DeleteBackupButton.Name = "DeleteBackupButton"
        Me.DeleteBackupButton.Size = New System.Drawing.Size(120, 23)
        Me.DeleteBackupButton.TabIndex = 2
        Me.DeleteBackupButton.Text = "Delete Selected"
        Me.DeleteBackupButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.DeleteBackupButton.UseVisualStyleBackColor = True
        '
        'BackupScheduleGroupBox
        '
        Me.BackupScheduleGroupBox.Controls.Add(Me.EnableScheduleCheckBox)
        Me.BackupScheduleGroupBox.Location = New System.Drawing.Point(6, 132)
        Me.BackupScheduleGroupBox.Name = "BackupScheduleGroupBox"
        Me.BackupScheduleGroupBox.Size = New System.Drawing.Size(200, 50)
        Me.BackupScheduleGroupBox.TabIndex = 2
        Me.BackupScheduleGroupBox.TabStop = False
        Me.BackupScheduleGroupBox.Text = "Schedule"
        '
        'EnableScheduleCheckBox
        '
        Me.EnableScheduleCheckBox.AutoSize = True
        Me.EnableScheduleCheckBox.Location = New System.Drawing.Point(6, 19)
        Me.EnableScheduleCheckBox.Name = "EnableScheduleCheckBox"
        Me.EnableScheduleCheckBox.Size = New System.Drawing.Size(130, 17)
        Me.EnableScheduleCheckBox.TabIndex = 0
        Me.EnableScheduleCheckBox.Text = "Enable Daily Backups"
        Me.EnableScheduleCheckBox.UseVisualStyleBackColor = True
        '
        'WorkshopModsTab
        '
        Me.WorkshopModsTab.Controls.Add(Me.ModEnableButton)
        Me.WorkshopModsTab.Controls.Add(Me.ModDisableButton)
        Me.WorkshopModsTab.Controls.Add(Me.ModUpdateButton)
        Me.WorkshopModsTab.Controls.Add(Me.ModUnsubscribeButton)
        Me.WorkshopModsTab.Controls.Add(Me.ModSubscribeButton)
        Me.WorkshopModsTab.Controls.Add(Me.ModSearchButton)
        Me.WorkshopModsTab.Controls.Add(Me.ModSearchTextBox)
        Me.WorkshopModsTab.Controls.Add(Me.ModListLabel)
        Me.WorkshopModsTab.Controls.Add(Me.ModSearchLabel)
        Me.WorkshopModsTab.Controls.Add(Me.ModListBox)
        Me.WorkshopModsTab.Location = New System.Drawing.Point(4, 22)
        Me.WorkshopModsTab.Name = "WorkshopModsTab"
        Me.WorkshopModsTab.Padding = New System.Windows.Forms.Padding(3)
        Me.WorkshopModsTab.Size = New System.Drawing.Size(776, 376)
        Me.WorkshopModsTab.TabIndex = 4
        Me.WorkshopModsTab.Text = "Workshop Mods"
        Me.WorkshopModsTab.UseVisualStyleBackColor = True
        '
        'ModEnableButton
        '
        Me.ModEnableButton.Image = Global.SteamCMD_GUI.My.Resources.Resources.check_24
        Me.ModEnableButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ModEnableButton.Location = New System.Drawing.Point(340, 143)
        Me.ModEnableButton.Name = "ModEnableButton"
        Me.ModEnableButton.Size = New System.Drawing.Size(100, 23)
        Me.ModEnableButton.TabIndex = 8
        Me.ModEnableButton.Text = "Enable"
        Me.ModEnableButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ModEnableButton.UseVisualStyleBackColor = True
        '
        'ModDisableButton
        '
        Me.ModDisableButton.Image = Global.SteamCMD_GUI.My.Resources.Resources.block_24
        Me.ModDisableButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ModDisableButton.Location = New System.Drawing.Point(340, 172)
        Me.ModDisableButton.Name = "ModDisableButton"
        Me.ModDisableButton.Size = New System.Drawing.Size(100, 23)
        Me.ModDisableButton.TabIndex = 9
        Me.ModDisableButton.Text = "Disable"
        Me.ModDisableButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ModDisableButton.UseVisualStyleBackColor = True
        '
        'ModUpdateButton
        '
        Me.ModUpdateButton.Image = Global.SteamCMD_GUI.My.Resources.Resources.update_24
        Me.ModUpdateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ModUpdateButton.Location = New System.Drawing.Point(340, 114)
        Me.ModUpdateButton.Name = "ModUpdateButton"
        Me.ModUpdateButton.Size = New System.Drawing.Size(100, 23)
        Me.ModUpdateButton.TabIndex = 7
        Me.ModUpdateButton.Text = "Update"
        Me.ModUpdateButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ModUpdateButton.UseVisualStyleBackColor = True
        '
        'ModUnsubscribeButton
        '
        Me.ModUnsubscribeButton.Image = Global.SteamCMD_GUI.My.Resources.Resources.minus_24
        Me.ModUnsubscribeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ModUnsubscribeButton.Location = New System.Drawing.Point(340, 85)
        Me.ModUnsubscribeButton.Name = "ModUnsubscribeButton"
        Me.ModUnsubscribeButton.Size = New System.Drawing.Size(100, 23)
        Me.ModUnsubscribeButton.TabIndex = 6
        Me.ModUnsubscribeButton.Text = "Unsubscribe"
        Me.ModUnsubscribeButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ModUnsubscribeButton.UseVisualStyleBackColor = True
        '
        'ModSubscribeButton
        '
        Me.ModSubscribeButton.Image = Global.SteamCMD_GUI.My.Resources.Resources.plus_24
        Me.ModSubscribeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ModSubscribeButton.Location = New System.Drawing.Point(340, 56)
        Me.ModSubscribeButton.Name = "ModSubscribeButton"
        Me.ModSubscribeButton.Size = New System.Drawing.Size(100, 23)
        Me.ModSubscribeButton.TabIndex = 5
        Me.ModSubscribeButton.Text = "Subscribe"
        Me.ModSubscribeButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ModSubscribeButton.UseVisualStyleBackColor = True
        '
        'ModSearchButton
        '
        Me.ModSearchButton.Image = Global.SteamCMD_GUI.My.Resources.Resources.search_24
        Me.ModSearchButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ModSearchButton.Location = New System.Drawing.Point(259, 5)
        Me.ModSearchButton.Name = "ModSearchButton"
        Me.ModSearchButton.Size = New System.Drawing.Size(75, 23)
        Me.ModSearchButton.TabIndex = 2
        Me.ModSearchButton.Text = "Search"
        Me.ModSearchButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ModSearchButton.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.SystemColors.Menu
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileMenu, Me.ToolsMenu, Me.HelpToolStripMenuItem})
        resources.ApplyResources(Me.MenuStrip1, "MenuStrip1")
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        '
        'FileMenu
        '
        Me.FileMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveMenu, Me.LoadMenu, Me.ToolStripSeparator3, Me.ExitMenu})
        Me.FileMenu.Name = "FileMenu"
        resources.ApplyResources(Me.FileMenu, "FileMenu")
        '
        'SaveMenu
        '
        Me.SaveMenu.Name = "SaveMenu"
        resources.ApplyResources(Me.SaveMenu, "SaveMenu")
        '
        'LoadMenu
        '
        Me.LoadMenu.Name = "LoadMenu"
        resources.ApplyResources(Me.LoadMenu, "LoadMenu")
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        resources.ApplyResources(Me.ToolStripSeparator3, "ToolStripSeparator3")
        '
        'ExitMenu
        '
        Me.ExitMenu.Name = "ExitMenu"
        resources.ApplyResources(Me.ExitMenu, "ExitMenu")
        '
        'ToolsMenu
        '
        Me.ToolsMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CommonFilesMenu, Me.CFGMenu, Me.ToolStripSeparator4, Me.SMMenu})
        Me.ToolsMenu.Name = "ToolsMenu"
        resources.ApplyResources(Me.ToolsMenu, "ToolsMenu")
        '
        'CommonFilesMenu
        '
        Me.CommonFilesMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MotdTxtButton, Me.MapcycleTxtButton, Me.MaplistTxtButton})
        resources.ApplyResources(Me.CommonFilesMenu, "CommonFilesMenu")
        Me.CommonFilesMenu.Name = "CommonFilesMenu"
        '
        'MotdTxtButton
        '
        Me.MotdTxtButton.Name = "MotdTxtButton"
        resources.ApplyResources(Me.MotdTxtButton, "MotdTxtButton")
        '
        'MapcycleTxtButton
        '
        Me.MapcycleTxtButton.Name = "MapcycleTxtButton"
        resources.ApplyResources(Me.MapcycleTxtButton, "MapcycleTxtButton")
        '
        'MaplistTxtButton
        '
        Me.MaplistTxtButton.Name = "MaplistTxtButton"
        resources.ApplyResources(Me.MaplistTxtButton, "MaplistTxtButton")
        '
        'CFGMenu
        '
        Me.CFGMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewFileToolStripMenuItem, Me.ToolStripSeparator2})
        resources.ApplyResources(Me.CFGMenu, "CFGMenu")
        Me.CFGMenu.Name = "CFGMenu"
        '
        'NewFileToolStripMenuItem
        '
        Me.NewFileToolStripMenuItem.Name = "NewFileToolStripMenuItem"
        resources.ApplyResources(Me.NewFileToolStripMenuItem, "NewFileToolStripMenuItem")
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        resources.ApplyResources(Me.ToolStripSeparator2, "ToolStripSeparator2")
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        resources.ApplyResources(Me.ToolStripSeparator4, "ToolStripSeparator4")
        '
        'SMMenu
        '
        Me.SMMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Empty})
        Me.SMMenu.Name = "SMMenu"
        resources.ApplyResources(Me.SMMenu, "SMMenu")
        '
        'Empty
        '
        Me.Empty.Name = "Empty"
        resources.ApplyResources(Me.Empty, "Empty")
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LogMenu, Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        resources.ApplyResources(Me.HelpToolStripMenuItem, "HelpToolStripMenuItem")
        '
        'LogMenu
        '
        Me.LogMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem4})
        resources.ApplyResources(Me.LogMenu, "LogMenu")
        Me.LogMenu.Name = "LogMenu"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        resources.ApplyResources(Me.ToolStripMenuItem4, "ToolStripMenuItem4")
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        resources.ApplyResources(Me.AboutToolStripMenuItem, "AboutToolStripMenuItem")
        '
        'XmlConfigOpenFileDialog
        '
        Me.XmlConfigOpenFileDialog.RestoreDirectory = True
        '
        'DonateButton
        '
        Me.DonateButton.BackgroundImage = Global.SteamCMD_GUI.My.Resources.Resources.PPDonateButton
        resources.ApplyResources(Me.DonateButton, "DonateButton")
        Me.DonateButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.DonateButton.Name = "DonateButton"
        Me.DonateButton.TabStop = False
        '
        'IPTextbox
        '
        Me.IPTextbox.Cursor = System.Windows.Forms.Cursors.IBeam
        resources.ApplyResources(Me.IPTextbox, "IPTextbox")
        Me.IPTextbox.Name = "IPTextbox"
        Me.IPTextbox.ReadOnly = True
        Me.IPTextbox.TabStop = False
        '
        'IPButton
        '
        Me.IPButton.BackColor = System.Drawing.Color.Transparent
        resources.ApplyResources(Me.IPButton, "IPButton")
        Me.IPButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.IPButton.Name = "IPButton"
        Me.IPButton.TabStop = False
        Me.IPButton.UseVisualStyleBackColor = False
        '
        'UpdatesTab
        '
        Me.UpdatesTab.Controls.Add(Me.UpdatesLabel)
        Me.UpdatesTab.Controls.Add(Me.CheckForUpdatesButton)
        Me.UpdatesTab.Controls.Add(Me.InstallUpdateButton)
        Me.UpdatesTab.Controls.Add(Me.UpdatesListBox)
        Me.UpdatesTab.Location = New System.Drawing.Point(4, 22)
        Me.UpdatesTab.Name = "UpdatesTab"
        Me.UpdatesTab.Padding = New System.Windows.Forms.Padding(3)
        Me.UpdatesTab.Size = New System.Drawing.Size(768, 376)
        Me.UpdatesTab.TabIndex = 5
        Me.UpdatesTab.Text = "Updates"
        Me.UpdatesTab.UseVisualStyleBackColor = True
        '
        'UpdatesLabel
        '
        Me.UpdatesLabel.AutoSize = True
        Me.UpdatesLabel.Location = New System.Drawing.Point(6, 10)
        Me.UpdatesLabel.Name = "UpdatesLabel"
        Me.UpdatesLabel.Size = New System.Drawing.Size(47, 13)
        Me.UpdatesLabel.TabIndex = 0
        Me.UpdatesLabel.Text = "Updates:"
        '
        'CheckForUpdatesButton
        '
        Me.CheckForUpdatesButton.Image = Global.SteamCMD_GUI.My.Resources.Resources.refresh_24
        Me.CheckForUpdatesButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CheckForUpdatesButton.Location = New System.Drawing.Point(9, 335)
        Me.CheckForUpdatesButton.Name = "CheckForUpdatesButton"
        Me.CheckForUpdatesButton.Size = New System.Drawing.Size(120, 23)
        Me.CheckForUpdatesButton.TabIndex = 1
        Me.CheckForUpdatesButton.Text = "Check for Updates"
        Me.CheckForUpdatesButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.CheckForUpdatesButton.UseVisualStyleBackColor = True
        '
        'InstallUpdateButton
        '
        Me.InstallUpdateButton.Image = Global.SteamCMD_GUI.My.Resources.Resources.install_24
        Me.InstallUpdateButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.InstallUpdateButton.Location = New System.Drawing.Point(135, 335)
        Me.InstallUpdateButton.Name = "InstallUpdateButton"
        Me.InstallUpdateButton.Size = New System.Drawing.Size(120, 23)
        Me.InstallUpdateButton.TabIndex = 2
        Me.InstallUpdateButton.Text = "Install Update"
        Me.InstallUpdateButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.InstallUpdateButton.UseVisualStyleBackColor = True
        '
        'UpdatesListBox
        '
        Me.UpdatesListBox.FormattingEnabled = True
        Me.UpdatesListBox.Location = New System.Drawing.Point(9, 26)
        Me.UpdatesListBox.Name = "UpdatesListBox"
        Me.UpdatesListBox.Size = New System.Drawing.Size(746, 303)
        Me.UpdatesListBox.TabIndex = 3
        '
        'MainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.LanguageComboBox)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MainMenu"
        Me.Text = "SteamCMD GUI"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.LogViewerTab.ResumeLayout(False)
        Me.LogViewerTab.PerformLayout()
        Me.RconTab.ResumeLayout(False)
        Me.RconConnectionGroupBox.ResumeLayout(False)
        Me.RconConnectionGroupBox.PerformLayout()
        Me.RconConsoleGroupBox.ResumeLayout(False)
        Me.RconConsoleGroupBox.PerformLayout()
        Me.ConfigEditorTab.ResumeLayout(False)
        Me.BackupRestoreTab.ResumeLayout(False)
        Me.CreateBackupGroupBox.ResumeLayout(False)
        Me.RestoreBackupGroupBox.ResumeLayout(False)
        Me.BackupScheduleGroupBox.ResumeLayout(False)
        Me.BackupScheduleGroupBox.PerformLayout()
        Me.WorkshopModsTab.ResumeLayout(False)
        Me.WorkshopModsTab.PerformLayout()
        Me.UpdatesTab.ResumeLayout(False)
        Me.UpdatesTab.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.DonateButton, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GamesList As System.Windows.Forms.ComboBox
    Friend WithEvents SteamCMDDownloadButton As System.Windows.Forms.Button
    Friend WithEvents DonwloadBar As System.Windows.Forms.ProgressBar
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Status As System.Windows.Forms.TextBox
    Friend WithEvents AnonymousCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents UsernameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PasswdTextBox As System.Windows.Forms.MaskedTextBox
    Friend WithEvents BrowserButton As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ServerPath As System.Windows.Forms.TextBox
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents VDCButton As System.Windows.Forms.Button
    Friend WithEvents UpdateServerButton As System.Windows.Forms.Button
    Friend WithEvents CheckUpdatesButton As System.Windows.Forms.Button
    Friend WithEvents ExitButton As System.Windows.Forms.Button
    Friend WithEvents OpenFolderButton As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents AboutButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents ESButton As System.Windows.Forms.Button
    Friend WithEvents MMButton As System.Windows.Forms.Button
    Friend WithEvents SMButton As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents ExeBrowserButton As System.Windows.Forms.Button
    Friend WithEvents ExePath As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents LogViewerTab As System.Windows.Forms.TabPage
    Friend WithEvents SearchLabel As System.Windows.Forms.Label
    Friend WithEvents SearchTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SearchButton As System.Windows.Forms.Button
    Friend WithEvents AutoScrollCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LogOutputTextBox As System.Windows.Forms.RichTextBox
    Friend WithEvents RconTab As System.Windows.Forms.TabPage
    Friend WithEvents RconConnectionGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents HostTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PortTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PasswordTextBox As System.Windows.Forms.MaskedTextBox
    Friend WithEvents HostLabel As System.Windows.Forms.Label
    Friend WithEvents PortLabel As System.Windows.Forms.Label
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents RconConnectButton As System.Windows.Forms.Button
    Friend WithEvents RconDisconnectButton As System.Windows.Forms.Button
    Friend WithEvents RconConsoleGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents RconSendCommand As System.Windows.Forms.Button
    Friend WithEvents RconCommandTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RconOutputTextBox As System.Windows.Forms.RichTextBox
    Friend WithEvents ConfigEditorTab As System.Windows.Forms.TabPage
    Friend WithEvents ConfigTreeView As System.Windows.Forms.TreeView
    Friend WithEvents ConfigEditorTextBox As System.Windows.Forms.RichTextBox
    Friend WithEvents ConfigOpenButton As System.Windows.Forms.Button
    Friend WithEvents ConfigRefreshButton As System.Windows.Forms.Button
    Friend WithEvents ConfigSaveButton As System.Windows.Forms.Button
    Friend WithEvents BackupRestoreTab As System.Windows.Forms.TabPage
    Friend WithEvents CreateBackupGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents SelectSourceButton As System.Windows.Forms.Button
    Friend WithEvents SelectDestinationButton As System.Windows.Forms.Button
    Friend WithEvents CreateBackupButton As System.Windows.Forms.Button
    Friend WithEvents RestoreBackupGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents BackupListBox As System.Windows.Forms.ListBox
    Friend WithEvents RestoreBackupButton As System.Windows.Forms.Button
    Friend WithEvents DeleteBackupButton As System.Windows.Forms.Button
    Friend WithEvents BackupScheduleGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents EnableScheduleCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents WorkshopModsTab As System.Windows.Forms.TabPage
    Friend WithEvents ModEnableButton As System.Windows.Forms.Button
    Friend WithEvents ModDisableButton As System.Windows.Forms.Button
    Friend WithEvents ModUpdateButton As System.Windows.Forms.Button
    Friend WithEvents ModUnsubscribeButton As System.Windows.Forms.Button
    Friend WithEvents ModSubscribeButton As System.Windows.Forms.Button
    Friend WithEvents ModSearchButton As System.Windows.Forms.Button
    Friend WithEvents ModSearchTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ModListLabel As System.Windows.Forms.Label
    Friend WithEvents ModSearchLabel As System.Windows.Forms.Label
    Friend WithEvents ModListBox As System.Windows.Forms.ListBox
    Friend WithEvents UpdatesTab As System.Windows.Forms.TabPage
    Friend WithEvents UpdatesLabel As System.Windows.Forms.Label
    Friend WithEvents CheckForUpdatesButton As System.Windows.Forms.Button
    Friend WithEvents InstallUpdateButton As System.Windows.Forms.Button
    Friend WithEvents UpdatesListBox As System.Windows.Forms.ListBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoadMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents XmlConfigOpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SrcdsExePathOpen As System.Windows.Forms.Button
    Friend WithEvents ToolsMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CFGMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NewFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CommonFilesMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MotdTxtButton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MapcycleTxtButton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MaplistTxtButton As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SMMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Empty As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddButton As System.Windows.Forms.Button
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents LogMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ValidateCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ConsoleOutput As System.Windows.Forms.RichTextBox
    Friend WithEvents ConsoleTab As System.Windows.Forms.TabPage
    Friend WithEvents ConsoleButton As System.Windows.Forms.Button
    Friend WithEvents ConsoleInput As System.Windows.Forms.TextBox
    Friend WithEvents ConsoleCommandList As System.Windows.Forms.ComboBox
    Friend WithEvents ConsoleSaveLog As System.Windows.Forms.Button
    Friend WithEvents ConsoleConnect As System.Windows.Forms.Button
    Friend WithEvents ConsoleClearLog As System.Windows.Forms.Button
    Friend WithEvents ConsoleOpenLog As System.Windows.Forms.Button
    Friend WithEvents GoldSrcModLabel As System.Windows.Forms.Label
    Friend WithEvents GoldSrcModInput As System.Windows.Forms.TextBox
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CheckBoxConsole As System.Windows.Forms.CheckBox
    Friend WithEvents AddCustomGameButton As System.Windows.Forms.Button
    Friend WithEvents DonateButton As System.Windows.Forms.PictureBox
    Friend WithEvents ConsoleIPPrint As System.Windows.Forms.Button
    Friend WithEvents IPTextbox As System.Windows.Forms.TextBox
    Friend WithEvents IPButton As System.Windows.Forms.Button
    Friend WithEvents Is64BitCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents RemoveProfileButton As System.Windows.Forms.Button
    Friend WithEvents SaveProfileButton As System.Windows.Forms.Button
    Friend WithEvents AddProfileButton As System.Windows.Forms.Button
    Friend WithEvents ProfileComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents SearchTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SearchButton As System.Windows.Forms.Button
    Friend WithEvents AutoScrollCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents RconTab As System.Windows.Forms.TabPage
    Friend WithEvents RconConnectionGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents RconIpLabel As System.Windows.Forms.Label
    Friend WithEvents RconIpTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RconPortLabel As System.Windows.Forms.Label
    Friend WithEvents RconPortNumericUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents RconPasswordLabel As System.Windows.Forms.Label
    Friend WithEvents RconPasswordTextBox As System.Windows.Forms.MaskedTextBox
    Friend WithEvents RconConnectButton As System.Windows.Forms.Button
    Friend WithEvents RconDisconnectButton As System.Windows.Forms.Button
    Friend WithEvents RconConsoleGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents RconSendCommand As System.Windows.Forms.Button
    Friend WithEvents RconCommandTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RconOutputTextBox As System.Windows.Forms.RichTextBox
    Friend WithEvents LanguageComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ConfigEditorTab As System.Windows.Forms.TabPage
    Friend WithEvents ConfigTreeView As System.Windows.Forms.TreeView
    Friend WithEvents ConfigEditorTextBox As System.Windows.Forms.RichTextBox
    Friend WithEvents ConfigOpenButton As System.Windows.Forms.Button
    Friend WithEvents ConfigRefreshButton As System.Windows.Forms.Button
    Friend WithEvents ConfigSaveButton As System.Windows.Forms.Button
    Friend WithEvents BackupRestoreTab As System.Windows.Forms.TabPage
    Friend WithEvents CreateBackupGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents SelectSourceButton As System.Windows.Forms.Button
    Friend WithEvents SelectDestinationButton As System.Windows.Forms.Button
    Friend WithEvents CreateBackupButton As System.Windows.Forms.Button
    Friend WithEvents RestoreBackupGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents BackupListBox As System.Windows.Forms.ListBox
    Friend WithEvents RestoreBackupButton As System.Windows.Forms.Button
    Friend WithEvents DeleteBackupButton As System.Windows.Forms.Button
    Friend WithEvents BackupScheduleGroupBox As System.Windows.Forms.GroupBox
    Friend WithEvents EnableScheduleCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents WorkshopModsTab As System.Windows.Forms.TabPage
    Friend WithEvents ModEnableButton As System.Windows.Forms.Button
    Friend WithEvents ModDisableButton As System.Windows.Forms.Button
    Friend WithEvents ModUpdateButton As System.Windows.Forms.Button
    Friend WithEvents ModUnsubscribeButton As System.Windows.Forms.Button
    Friend WithEvents ModSubscribeButton As System.Windows.Forms.Button
    Friend WithEvents ModSearchButton As System.Windows.Forms.Button
    Friend WithEvents ModSearchTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ModListLabel As System.Windows.Forms.Label
    Friend WithEvents ModSearchLabel As System.Windows.Forms.Label
    Friend WithEvents ModListBox As System.Windows.Forms.ListBox
    Friend WithEvents UpdatesTab As System.Windows.Forms.TabPage
    Friend WithEvents UpdatesLabel As System.Windows.Forms.Label
    Friend WithEvents CheckForUpdatesButton As System.Windows.Forms.Button
    Friend WithEvents InstallUpdateButton As System.Windows.Forms.Button
    Friend WithEvents UpdatesListBox As System.Windows.Forms.ListBox
End Class
