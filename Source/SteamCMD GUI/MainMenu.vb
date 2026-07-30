Imports System.Globalization
Imports System.Threading
Imports System.IO
Imports System.Net
Imports System.Xml
Imports System.Text
Imports System.Xml.Linq
Imports CoreRCON
Imports System.Resources
Imports System.IO.Compression

Public Class MainMenu
    Private components As System.ComponentModel.IContainer
    Private WithEvents BackupManager As New BackupManager()
    Private WithEvents SteamCMD As New Process()
    Private WithEvents SteamCMD_Find As New Process()
    Private ReadOnly SteamCMD_Games As New XmlDocument()
    Private ReadOnly SteamCMD_Path As String = My.Application.Info.DirectoryPath & "\steamcmd.exe"

    Private WithEvents RconClient As New RconClient()

    ' SteamCMD Installation
    Private SteamCMDExePath, SteamAppID, Login, ServerPathInstallation, ValidateApp, GoldSrcMod, Program, Game, PathForLog As String
    ' Run Server
    Private SrcdsExePath, GameMod, ServerName, ServerMap, NetworkType, MaxPlayers, RCON, UDPPort, DebugMode, SourceTV, ConsoleMode, InsecureMode, NoBots, DevMode, Parameters As String
    Public AdditionalCommands As String
    ' Strings
    Private CantFindSteamCMDString As String
    Private GameDictionary As New Dictionary(Of String, String)

    ' Thread and Process objects
    Private ThrSteamCMD As Thread
    Private WithEvents p As Process

    Dim WithEvents WC As New WebClient

    Dim LocalHost As String = Dns.GetHostName
    Dim IPs As IPHostEntry = Dns.GetHostEntry(LocalHost)
    Dim PublicIP As String

    Private Const GOLDSRC_APP_ID As Integer = 90

    Private Declare Function GetInputState Lib "user32" () As Int32

    Private Async Sub Form1_Load() Handles MyBase.Load
        Await GetPublicIPAsync()

        Icon = My.Resources.SteamCMDGUI_Icon
        TabMenu.Size = New Size(417, 303)
        ThrSteamCMD = New Thread(AddressOf ThreadTaskSteamCMD)
        ModList.SelectedIndex = 1
        NetworkComboBox.SelectedIndex = 0
        ConsoleCommandList.SelectedIndex = 0
        Status.Text = ""
        Tips()
        IPPrint()
        Directory.CreateDirectory("Settings")
        Directory.CreateDirectory("Logs")
        LoadSteamCMDPath()
        LoadProfiles()
        If File.Exists("Settings/SteamCMDGames.xml") Then
            LoadGamesList()
        Else
            InitializeDefaultGamesList()
        End If
        GamesList.DataSource = New BindingSource(GameDictionary, Nothing)
        GamesList.DisplayMember = "Value"
        GamesList.ValueMember = "Key"
        GamesList.DataBindings.DefaultDataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
        GamesList.SelectedIndex = 1

        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font

        ' Load games from SteamCMDGames.xml
        SteamCMD_Games.Load(My.Application.Info.DirectoryPath & "\Resources\SteamCMDGames.xml")
        For Each Game As XmlNode In SteamCMD_Games.SelectNodes("//game")
            ComboBox1.Items.Add(Game.SelectSingleNode("name").InnerText)
        Next

        LoadSettings()
        ApplyLocalizedStrings()
        ApplyDarkTheme()

    End Sub

    Private Sub MainMenu_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        SaveSettings()
    End Sub

    Private Sub LoadSettings()
        ' Load settings
        HostTextBox.Text = My.Settings.RconHost
        PortTextBox.Text = My.Settings.RconPort
        PasswordTextBox.Text = My.Settings.RconPassword
        SourcePath = My.Settings.BackupSourcePath
        DestinationPath = My.Settings.BackupDestinationPath
        EnableScheduleCheckBox.Checked = My.Settings.IsBackupScheduleEnabled
        LanguageComboBox.SelectedItem = My.Settings.SelectedLanguage
        If Not String.IsNullOrEmpty(My.Settings.ConfigEditorPath) Then
            PopulateConfigTreeView(My.Settings.ConfigEditorPath)
        End If
        PopulateBackupListBox()
    End Sub

    Private Sub SaveSettings()
        ' Save settings
        My.Settings.RconHost = HostTextBox.Text
        My.Settings.RconPort = PortTextBox.Text
        My.Settings.RconPassword = PasswordTextBox.Text
        My.Settings.BackupSourcePath = SourcePath
        My.Settings.BackupDestinationPath = DestinationPath
        My.Settings.IsBackupScheduleEnabled = EnableScheduleCheckBox.Checked
        My.Settings.SelectedLanguage = LanguageComboBox.SelectedItem.ToString()
        If ConfigTreeView.Nodes.Count > 0 Then
            My.Settings.ConfigEditorPath = ConfigTreeView.Nodes(0).Tag.ToString()
        End If
        My.Settings.Save()
    End Sub

    Private Sub ApplyDarkTheme()
        ' Apply a modern dark theme
        Dim darkGray As Color = Color.FromArgb(45, 45, 48)
        Dim lightGray As Color = Color.FromArgb(62, 62, 66)
        Dim whiteText As Color = Color.FromArgb(241, 241, 241)

        Me.BackColor = darkGray
        Me.ForeColor = whiteText

        For Each ctrl As Control In Me.Controls
            ApplyThemeToControl(ctrl, darkGray, lightGray, whiteText)
        Next
    End Sub

    Private Sub ApplyThemeToControl(parent As Control, backColor As Color, controlBackColor As Color, foreColor As Color)
        parent.BackColor = backColor
        parent.ForeColor = foreColor

        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is Button Or TypeOf ctrl Is TextBox Or TypeOf ctrl Is ComboBox Or TypeOf ctrl Is RichTextBox Or TypeOf ctrl Is ListBox Or TypeOf ctrl Is TreeView Then
                ctrl.BackColor = controlBackColor
                ctrl.ForeColor = foreColor
            Else
                ctrl.BackColor = backColor
                ctrl.ForeColor = foreColor
            End If

            If ctrl.HasChildren Then
                ApplyThemeToControl(ctrl, backColor, controlBackColor, foreColor)
            End If
        Next
    End Sub

    Private Sub ApplyLocalizedStrings()
        ' Apply localized strings to controls
        Me.Text = My.Resources.Strings_en.MainMenu_Title
        LogViewerTab.Text = My.Resources.Strings_en.Tab_LogViewer
        RconTab.Text = My.Resources.Strings_en.Tab_RCON
        SearchLabel.Text = My.Resources.Strings_en.Label_Search
        SearchButton.Text = My.Resources.Strings_en.Button_Search
        AutoScrollCheckBox.Text = My.Resources.Strings_en.CheckBox_AutoScroll
        RconConnectionGroupBox.Text = My.Resources.Strings_en.GroupBox_RconConnection
        HostLabel.Text = My.Resources.Strings_en.Label_RconHost
        PortLabel.Text = My.Resources.Strings_en.Label_RconPort
        PasswordLabel.Text = My.Resources.Strings_en.Label_RconPassword
        RconConnectButton.Text = My.Resources.Strings_en.Button_RconConnect
        RconDisconnectButton.Text = My.Resources.Strings_en.Button_RconDisconnect
        RconConsoleGroupBox.Text = My.Resources.Strings_en.GroupBox_RconConsole
        RconSendCommand.Text = My.Resources.Strings_en.Button_RconSend
        ConfigEditorTab.Text = My.Resources.Strings_en.Tab_ConfigEditor
        ConfigOpenButton.Text = My.Resources.Strings_en.Button_OpenFile
        ConfigRefreshButton.Text = My.Resources.Strings_en.Button_Refresh
        ConfigSaveButton.Text = My.Resources.Strings_en.Button_Save
        BackupRestoreTab.Text = My.Resources.Strings_en.Tab_BackupRestore
        CreateBackupGroupBox.Text = My.Resources.Strings_en.GroupBox_CreateBackup
        SelectSourceButton.Text = My.Resources.Strings_en.Button_SelectSource
        SelectDestinationButton.Text = My.Resources.Strings_en.Button_SelectDestination
        CreateBackupButton.Text = My.Resources.Strings_en.Button_CreateBackup
        RestoreBackupGroupBox.Text = My.Resources.Strings_en.GroupBox_RestoreBackup
        RestoreBackupButton.Text = My.Resources.Strings_en.Button_RestoreBackup
        DeleteBackupButton.Text = My.Resources.Strings_en.Button_DeleteBackup
        BackupScheduleGroupBox.Text = My.Resources.Strings_en.GroupBox_Schedule
        EnableScheduleCheckBox.Text = My.Resources.Strings_en.CheckBox_EnableSchedule
        ' WorkshopModsTab.Text = My.Resources.Strings_en.Tab_WorkshopMods
        ' ModSearchLabel.Text = My.Resources.Strings_en.Label_ModSearch
        ' ModListLabel.Text = My.Resources.Strings_en.Label_ModList
        ' ModSearchButton.Text = My.Resources.Strings_en.Button_ModSearch
        ' ModSubscribeButton.Text = My.Resources.Strings_en.Button_ModSubscribe
        ' ModUnsubscribeButton.Text = My.Resources.Strings_en.Button_ModUnsubscribe
        ' ModUpdateButton.Text = My.Resources.Strings_en.Button_ModUpdate
        ' ModEnableButton.Text = My.Resources.Strings_en.Button_ModEnable
        ' ModDisableButton.Text = My.Resources.Strings_en.Button_ModDisable
        UpdatesTab.Text = My.Resources.Strings_en.Tab_Updates
        CheckForUpdatesButton.Text = My.Resources.Strings_en.Button_CheckForUpdates
        InstallUpdateButton.Text = My.Resources.Strings_en.Button_InstallUpdate
    End Sub

    ' Workshop Mods logic
    ' Private Sub ModSearchButton_Click(sender As Object, e As EventArgs) Handles ModSearchButton.Click
    '     WorkshopManager.Search(AppIdTextBox.Text, SearchTextBox.Text)
    ' End Sub

    ' Private Sub InstallModButton_Click(sender As Object, e As EventArgs) Handles InstallModButton.Click
    '     If WorkshopListBox.SelectedItem IsNot Nothing Then
    '         Dim modId As String = WorkshopListBox.SelectedItem.ToString().Split(" "c)(0) ' Assuming format "ID - Name"
    '         WorkshopManager.Install(AppIdTextBox.Text, modId)
    '     End If
    ' End Sub

    ' WorkshopManager event handlers
    ' Private Sub WorkshopManager_WorkshopSearchCompleted(results As List(Of String)) Handles WorkshopManager.WorkshopSearchCompleted
    '     WorkshopListBox.Items.Clear()
    '     For Each result As String In results
    '         WorkshopListBox.Items.Add(result)
    '     Next
    '     MessageBox.Show("Workshop search completed.", "Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
    ' End Sub

    ' Private Sub WorkshopManager_WorkshopInstallCompleted(modId As String) Handles WorkshopManager.WorkshopInstallCompleted
    '     MessageBox.Show($"Mod {modId} installed successfully.", "Install", MessageBoxButtons.OK, MessageBoxIcon.Information)
    ' End Sub

    ' Private Sub WorkshopManager_ErrorOccurred(message As String) Handles WorkshopManager.ErrorOccurred
    '     MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    ' End Sub

    Private Async Function GetPublicIPAsync() As Task
        If My.Computer.Network.IsAvailable Then
            Try
                PublicIP = Await WC.DownloadStringTaskAsync("http://ipv4.icanhazip.com/")
                PublicIP = PublicIP.Trim()
            Catch ex As WebException
                PublicIP = "Network down"
                UpdateStatus("Could not retrieve public IP: " & ex.Message, True)
            End Try
        Else
            PublicIP = "Network down"
        End If
    End Function

    Public Sub UpdateStatus(text As String, Optional isError As Boolean = False)
        Dim color As Color = If(isError, Color.Red, Color.Black)
        AppendOutputText(text, color)
        Status.Text = String.Format("[{0}] {1}", DateTime.Now.ToString("HH:mm:ss"), text)
        If isError Then
            Status.BackColor = Color.FromArgb(240, 200, 200)
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Hand)
        Else
            Status.BackColor = Color.FromArgb(240, 240, 240)
        End If
    End Sub

    Private Sub Tips()
        ToolTip1.SetToolTip(OpenFolderButton, "Open current folder")
        ToolTip1.SetToolTip(CheckBoxMask, "Mask/Unmask RCON")
        ToolTip1.SetToolTip(AddButton, "Add more command-line parameters")
        ToolTip1.SetToolTip(ConsoleConnect, "Connect to server")
        ToolTip1.SetToolTip(ConsoleOpenLog, "Open logs folder")
        ToolTip1.SetToolTip(ConsoleSaveLog, "Save the current log")
        ToolTip1.SetToolTip(ConsoleClearLog, "Clear log")
        ToolTip1.SetToolTip(DonateButton, "Donate via PayPal")
        CantFindSteamCMDString = "Can't find the file 'steamcmd.exe'!"
    End Sub

    Private Sub IPPrint() Handles ConsoleIPPrint.Click
        Dim sb As New Text.StringBuilder()
        sb.AppendLine("Local IP address(es):")
        For Each LocalIP As Net.IPAddress In IPs.AddressList
            sb.Append(vbTab).AppendLine(LocalIP.ToString())
        Next
        sb.AppendLine()
        sb.AppendLine("Public IP address:")
        sb.Append(vbTab).Append(PublicIP)
        ConsoleOutput.Text = sb.ToString()
        IPTextbox.Text = PublicIP
    End Sub

    ' Autosave log
    Private Sub SaveLog()
        Dim ConsoleContent As String = DateTime.Now & " from " & Program & vbCrLf & "______________________" & vbCrLf & Game & vbCrLf & PathForLog & vbCrLf & "______________________" & vbCrLf & ConsoleOutput.Text

        Dim LogFileName As String = Program & " Log-" & DateTime.Now.ToString("dd.MM.yyyy") & " @ " & DateTime.Now.ToString("HH;mm")
        File.WriteAllText("Logs\" & LogFileName & ".txt", ConsoleContent)
    End Sub

    ' Resize tabs
    Private Sub Tab_Click() Handles UpdateTab.Enter, RunTab.Enter
        If GroupBox1.Visible = False Then
            GroupBox1.Show()
            GroupBox3.Show()
            AboutButton.Show()
            ExitButton.Show()
            DonateButton.Show()
            DownloadBar.Show()
            TabMenu.Size = New Size(417, 303)
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        End If
    End Sub

    Private Sub ConsoleTab_Click() Handles ConsoleTab.Enter
        GroupBox1.Hide()
        GroupBox3.Hide()
        AboutButton.Hide()
        ExitButton.Hide()
        DonateButton.Hide()
        DownloadBar.Hide()
        TabMenu.Size = New Size(588, 303)
        ConsoleTab.Size = New Size(580, 277)
        ConsoleOutput.Size = New Size(539, 238)
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    End Sub

    ' Update/install server inputs
    Private Sub SteamCMDDownload_Click() Handles SteamCMDDownloadButton.Click
        SteamCMDDownloadButton.Enabled = False
        If My.Computer.FileSystem.FileExists("steamcmd.zip") Then
            UpdateStatus("The file has already been downloaded!", True)
            SteamCMDDownloadButton.Enabled = True
        Else
            Try
                WC.DownloadFileAsync(New Uri("http://media.steampowered.com/installer/steamcmd.zip"), "steamcmd.zip")
                UpdateStatus("Downloading...")
            Catch ex As Exception
                UpdateStatus("Error downloading SteamCMD: " & ex.Message, True)
                SteamCMDDownloadButton.Enabled = True
            End Try
        End If
    End Sub

    Private Sub OpenFolderButton_Click() Handles OpenFolderButton.Click
        Process.Start("explorer.exe", ".")
    End Sub

    Private Sub WC_DownloadProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs) Handles WC.DownloadProgressChanged
        DownloadBar.Value = e.ProgressPercentage
        If DownloadBar.Value = 100 Then
            UpdateStatus("The file 'steamcmd.zip' has been downloaded. Please, unzip it.")
            DownloadBar.Value = 0
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
            SteamCMDDownloadButton.Enabled = True
        End If
    End Sub

    Private Sub ExePath_Browser() Handles ExePath.Click, ExeBrowserButton.Click
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            If My.Computer.FileSystem.FileExists(FolderBrowserDialog1.SelectedPath & "\steamcmd.exe") Then
                ExePath.Text = FolderBrowserDialog1.SelectedPath
                SteamCMDExePath = FolderBrowserDialog1.SelectedPath

                Dim CMDConfig As New XmlWriterSettings()
                CMDConfig.Indent = True

                Dim XmlWrt As XmlWriter = XmlWriter.Create("Settings/SteamCMDPath.xml", CMDConfig)
                With XmlWrt
                    .WriteStartDocument()
                    .WriteComment("Config used by SteamCMD GUI")
                    .WriteComment("This config it's loaded automatically.")
                    .WriteStartElement("SteamCMD-Config")

                    .WriteStartElement("CMDPath")
                    .WriteString(SteamCMDExePath)
                    .WriteEndElement()

                    .WriteEndElement()
                    .WriteEndDocument()
                End With
                XmlWrt.Close()

                LogMenu.Enabled = True
                UpdateStatus("Current path of 'steamcmd.exe' is " & FolderBrowserDialog1.SelectedPath)
            Else
                LogMenu.Enabled = False
                UpdateStatus(CantFindSteamCMDString & " Please select the correct installation folder.", True)
            End If
        End If
    End Sub

    Private Sub AnonymousCheckBox_CheckedChanged() Handles AnonymousCheckBox.CheckedChanged
        If AnonymousCheckBox.Checked = True Then
            UsernameTextBox.Enabled = False
            PasswdTextBox.Enabled = False
        Else
            UsernameTextBox.Enabled = True
            PasswdTextBox.Enabled = True
        End If
    End Sub

    Private Sub IdHelpButton_Click() Handles IdHelpButton.Click
        Try
            Process.Start("https://developer.valvesoftware.com/wiki/Dedicated_Servers_List")
        Catch ex As Exception
            UpdateStatus($"Failed to open link: {ex.Message}", True)
        End Try
    End Sub

    Private Sub BrowserButton_Browser() Handles BrowserButton.Click, ServerPath.Click
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            ServerPath.Text = FolderBrowserDialog1.SelectedPath
            Dim ServerInstallPath As String
            ServerInstallPath = FolderBrowserDialog1.SelectedPath
        End If
        If String.IsNullOrWhiteSpace(ServerPath.Text) Then
            UpdateStatus("Please, select a folder for install/update the server.", True)
        Else
            UpdateStatus("The server will be installed/updated in '" & ServerPath.Text & "'")
            UpdateServerButton.Enabled = True
        End If
    End Sub

    Private Sub GamesList_SelectedIndexChanged() Handles GamesList.SelectedIndexChanged, GamesList.EnabledChanged
        If TypeOf (GamesList.SelectedValue) Is KeyValuePair(Of String, String) Then
            SteamAppID = GamesList.SelectedValue.Key
        ElseIf TypeOf (GamesList.SelectedValue) Is Integer Then
            SteamAppID = GamesList.SelectedValue.ToString()
        ElseIf TypeOf (GamesList.SelectedValue) Is String Then
            SteamAppID = GamesList.SelectedValue
        End If


        If Not SteamAppID = GOLDSRC_APP_ID.ToString() Then
            GoldSrcModInput.Hide()
            GoldSrcModLabel.Hide()
            AddCustomGameButton.Show()
        Else
            GoldSrcModInput.Show()
            GoldSrcModLabel.Show()
            AddCustomGameButton.Hide()
        End If
        UpdateStatus("Game to install: " & GamesList.Text & " - Steam App ID:" & SteamAppID)
    End Sub

    Private Sub ValidateCheckBox_CheckedChanged() Handles ValidateCheckBox.CheckedChanged
        If ValidateCheckBox.Checked = True Then
            ValidateApp = " validate"
            UpdateStatus("The files will be checked and validated.")
        Else
            ValidateApp = ""
        End If
    End Sub

    Private Sub UpdateServerButton_Click() Handles UpdateServerButton.Click
        If Not ValidateUpdateInputs() Then Return

        SetLoginCredentials()

        If Not AreCredentialsSet() Then Return

        If Not IsServerPathSet() Then Return

        SetGoldSrcMod()

        ServerPathInstallation = Chr(34) & ServerPath.Text & Chr(34)
        UpdateStatus("Installing/Updating...")

        StartSteamCMDProcess()
    End Sub

    Private Function ValidateUpdateInputs() As Boolean
        If Not My.Computer.FileSystem.FileExists(Path.Combine(SteamCMDExePath, "steamcmd.exe")) Then
            UpdateStatus(CantFindSteamCMDString, True)
            Return False
        End If

        If String.IsNullOrWhiteSpace(SteamAppID) Then
            UpdateStatus("Please select a game to install/update.", True)
            Return False
        End If
        Return True
    End Function

    Private Sub SetLoginCredentials()
        If AnonymousCheckBox.Checked Then
            Login = "anonymous"
        Else
            Login = $"{UsernameTextBox.Text} {PasswdTextBox.Text}"
        End If
    End Sub

    Private Function AreCredentialsSet() As Boolean
        If Not AnonymousCheckBox.Checked Then
            If String.IsNullOrWhiteSpace(UsernameTextBox.Text) Then
                UpdateStatus("Please, type your Steam name.", True)
                Return False
            End If
            If String.IsNullOrWhiteSpace(PasswdTextBox.Text) Then
                UpdateStatus("Please, type your Steam password. You can install many games as 'anonymous'.", True)
                Return False
            End If
        End If
        Return True
    End Function

    Private Function IsServerPathSet() As Boolean
        If String.IsNullOrWhiteSpace(ServerPath.Text) Then
            UpdateStatus("Please, select the path where you want to install the server.", True)
            Return False
        End If
        Return True
    End Function

    Private Sub SetGoldSrcMod()
        If GoldSrcModInput.Visible Then
            If Not String.IsNullOrEmpty(GoldSrcModInput.Text) Then
                GoldSrcMod = $" +app_set_config {GOLDSRC_APP_ID} mod {GoldSrcModInput.Text}"
            Else
                UpdateStatus("Half-Life mod not defined. Installing a default one.", True)
                GoldSrcMod = $" +app_set_config {GOLDSRC_APP_ID} mod valve" ' Default to valve
            End If
        Else
            GoldSrcMod = ""
        End If
    End Sub

    Private Sub StartSteamCMDProcess()
        If CheckBoxConsole.Checked Then
            ConsoleTab_Click()
            TabMenu.SelectedTab = ConsoleTab
            ConsoleOutput.Clear()
            ThrSteamCMD.Start()
        Else
            Try
                Dim p As New Process
                With p.StartInfo
                    .FileName = Path.Combine(SteamCMDExePath, "steamcmd.exe")
                    .UseShellExecute = False
                    .Arguments = $"SteamCmd +login {Login} +force_install_dir ""{ServerPath.Text}""{GoldSrcMod} +app_update {SteamAppID}{ValidateApp}"
                End With
                p.Start()
            Catch ex As Exception
                UpdateStatus($"Failed to start SteamCMD: {ex.Message}", True)
            End Try
        End If
    End Sub

    Private Sub ThreadTaskSteamCMD()
        Control.CheckForIllegalCrossThreadCalls = False
        p = New Process
        With (p.StartInfo)
            .FileName = SteamCMDExePath & "\steamcmd.exe"
            .UseShellExecute = False
            .CreateNoWindow = True
            .RedirectStandardOutput = True
            .RedirectStandardInput = True
            .RedirectStandardError = True
            .Arguments = String.Format("SteamCmd +login {0} +force_install_dir {1}{2} +app_update {3}{4}", Login, ServerPathInstallation, GoldSrcMod, SteamAppID, ValidateApp)
        End With

        p.Start()

        If CheckBoxConsole.Checked = True Then
            Dim pStreamWriter As StreamWriter = p.StandardInput
            p.BeginOutputReadLine()
            p.BeginErrorReadLine()
            ConsoleInput.Enabled = True
            ConsoleButton.Enabled = True
            p.WaitForExit()
        End If
    End Sub

    Private Sub p_OutputDataReceived(ByVal sender As Object, ByVal e As System.Diagnostics.DataReceivedEventArgs) Handles p.OutputDataReceived
        AppendOutputText(vbCrLf & e.Data, Color.DarkBlue)
    End Sub

    Private Sub ExecuteButton_Click() Handles ConsoleButton.Click
        p.StandardInput.WriteLine(ConsoleInput.Text)
        p.StandardInput.Flush()
        ConsoleInput.Text = ""
    End Sub

    Private Delegate Sub AppendOutputTextDelegate(ByVal text As String, Optional color As Color = Nothing)
    Private Sub AppendOutputText(ByVal text As String, Optional color As Color = Nothing)
        If ConsoleOutput.InvokeRequired Then
            Dim myDelegate As New AppendOutputTextDelegate(AddressOf AppendOutputText)
            Me.Invoke(myDelegate, text, color)
        Else
            Dim timestamp As String = $"[{DateTime.Now:HH:mm:ss}] "
            ConsoleOutput.SelectionStart = ConsoleOutput.TextLength
            ConsoleOutput.SelectionLength = 0

            ConsoleOutput.SelectionColor = Color.Gray
            ConsoleOutput.AppendText(timestamp)

            ConsoleOutput.SelectionColor = If(color, ConsoleOutput.ForeColor)
            ConsoleOutput.AppendText(text)

            If AutoScrollCheckBox.Checked Then
                ConsoleOutput.ScrollToCaret()
            End If
        End If
    End Sub

    Private Sub SelectSourceButton_Click(sender As Object, e As EventArgs) Handles SelectSourceButton.Click
        Using dialog As New FolderBrowserDialog()
            If dialog.ShowDialog() = DialogResult.OK Then
                SourcePath = dialog.SelectedPath
            End If
        End Using
    End Sub

    Private Sub SelectDestinationButton_Click(sender As Object, e As EventArgs) Handles SelectDestinationButton.Click
        Using dialog As New FolderBrowserDialog()
            If dialog.ShowDialog() = DialogResult.OK Then
                DestinationPath = dialog.SelectedPath
                PopulateBackupListBox()
            End If
        End Using
    End Sub

    Private Sub CreateBackupButton_Click(sender As Object, e As EventArgs) Handles CreateBackupButton.Click
        If Not String.IsNullOrEmpty(SourcePath) AndAlso Not String.IsNullOrEmpty(DestinationPath) Then
            BackupManager.CreateBackup(SourcePath, DestinationPath)
        End If
    End Sub

    Private Sub RestoreBackupButton_Click(sender As Object, e As EventArgs) Handles RestoreBackupButton.Click
        If BackupListBox.SelectedItem IsNot Nothing Then
            Dim backupFileName As String = BackupListBox.SelectedItem.ToString()
            Dim backupFilePath As String = Path.Combine(DestinationPath, backupFileName)
            Using dialog As New FolderBrowserDialog()
                If dialog.ShowDialog() = DialogResult.OK Then
                    Dim restorePath As String = dialog.SelectedPath
                    BackupManager.RestoreBackup(backupFilePath, restorePath)
                End If
            End Using
        End If
    End Sub

    Private Sub DeleteBackupButton_Click(sender As Object, e As EventArgs) Handles DeleteBackupButton.Click
        If BackupListBox.SelectedItem IsNot Nothing Then
            Dim backupFileName As String = BackupListBox.SelectedItem.ToString()
            Dim backupFilePath As String = Path.Combine(DestinationPath, backupFileName)
            BackupManager.DeleteBackup(backupFilePath)
        End If
    End Sub

    Private Sub EnableScheduleCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles EnableScheduleCheckBox.CheckedChanged
        If EnableScheduleCheckBox.Checked Then
            BackupTimer.Start()
        Else
            BackupTimer.Stop()
        End If
    End Sub

    Private Sub BackupTimer_Tick(sender As Object, e As EventArgs) Handles BackupTimer.Tick
        If Not String.IsNullOrEmpty(SourcePath) AndAlso Not String.IsNullOrEmpty(DestinationPath) Then
            BackupManager.CreateBackup(SourcePath, DestinationPath)
        End If
    End Sub

    Private Sub PopulateBackupListBox()
        BackupListBox.Items.Clear()
        If Not String.IsNullOrEmpty(DestinationPath) AndAlso Directory.Exists(DestinationPath) Then
            For Each backupName As String In BackupManager.ListBackups(DestinationPath)
                BackupListBox.Items.Add(backupName)
            Next
        End If
    End Sub

    ' BackupManager event handlers
    Private Sub BackupManager_BackupCreated(path As String) Handles BackupManager.BackupCreated
        PopulateBackupListBox()
        MessageBox.Show($"Backup created: {path}", "Backup", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub BackupManager_BackupRestored(path As String) Handles BackupManager.BackupRestored
        MessageBox.Show($"Backup restored to: {path}", "Restore", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub BackupManager_BackupDeleted(path As String) Handles BackupManager.BackupDeleted
        PopulateBackupListBox()
        MessageBox.Show($"Backup deleted: {path}", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub BackupManager_ErrorOccurred(message As String) Handles BackupManager.ErrorOccurred
        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private SourcePath As String
    Private DestinationPath As String
    Private WithEvents BackupTimer As New Timer() With {.Interval = 24 * 60 * 60 * 1000} ' 24 hours

    Private WithEvents ServerManager As New ServerManager()
    Private WithEvents UpdateManager As New UpdateManager(My.Application.Info.DirectoryPath & "\steamcmd.exe")

    Private Sub CheckForUpdatesButton_Click(sender As Object, e As EventArgs) Handles CheckForUpdatesButton.Click
        ' You would need to get the App ID and install directory for the selected server
        ' For this example, I'll use the values from the update/install tab
        Dim appId As String = SteamAppID
        Dim installDir As String = ServerPath.Text
        If Not String.IsNullOrEmpty(appId) AndAlso Not String.IsNullOrEmpty(installDir) Then
            UpdateManager.CheckForUpdate(appId, installDir)
        Else
            MessageBox.Show("Please select a game and server installation path first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub InstallUpdateButton_Click(sender As Object, e As EventArgs) Handles InstallUpdateButton.Click
        ' You would need to get the App ID and install directory for the selected server
        ' For this example, I'll use the values from the update/install tab
        Dim appId As String = SteamAppID
        Dim installDir As String = ServerPath.Text
        If Not String.IsNullOrEmpty(appId) AndAlso Not String.IsNullOrEmpty(installDir) Then
            UpdateManager.UpdateServer(appId, installDir)
        Else
            MessageBox.Show("Please select a game and server installation path first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub UpdateManager_UpdateCheckCompleted(updateAvailable As Boolean, appId As String) Handles UpdateManager.UpdateCheckCompleted
        If updateAvailable Then
            UpdatesListBox.Items.Add($"Update available for App ID: {appId}")
        Else
            UpdatesListBox.Items.Add($"No updates available for App ID: {appId}")
        End If
    End Sub

    Private Sub UpdateManager_UpdateCompleted(appId As String) Handles UpdateManager.UpdateCompleted
        MessageBox.Show($"Update for App ID: {appId} completed.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ' Refresh the updates list
        UpdatesListBox.Items.Clear()
    End Sub

    Private Sub UpdateManager_ErrorOccurred(message As String) Handles UpdateManager.ErrorOccurred
        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    ' RCON client event handlers and click handlers
    Private Sub RconConnectButton_Click(sender As Object, e As EventArgs) Handles RconConnectButton.Click
        Dim host As String = HostTextBox.Text
        Dim port As Integer
        If Not Integer.TryParse(PortTextBox.Text, port) Then
            MessageBox.Show("Please enter a valid port number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If
        Dim password As String = PasswordTextBox.Text
        RconClient.Connect(host, port, password)
    End Sub

    Private Sub RconDisconnectButton_Click(sender As Object, e As EventArgs) Handles RconDisconnectButton.Click
        RconClient.Disconnect()
    End Sub

    Private Sub RconSendCommand_Click(sender As Object, e As EventArgs) Handles RconSendCommand.Click
        Dim command As String = RconCommandTextBox.Text
        If Not String.IsNullOrEmpty(command) Then
            RconClient.SendCommand(command)
            RconCommandTextBox.Clear()
        End If
    End Sub

    Private Sub RconClient_OutputReceived(text As String) Handles RconClient.OutputReceived
        RconOutputTextBox.AppendText(text & vbCrLf)
        RconOutputTextBox.ScrollToCaret()
    End Sub

    Private Sub RconClient_ConnectionStatusChanged(connected As Boolean) Handles RconClient.ConnectionStatusChanged
        If connected Then
            RconConnectButton.Enabled = False
            RconDisconnectButton.Enabled = True
            UpdateStatus("RCON Connected successfully.")
        Else
            RconConnectButton.Enabled = True
            RconDisconnectButton.Enabled = False
            UpdateStatus("RCON Disconnected.")
        End If
    End Sub

    ' Config Editor methods and event handlers
    Private Sub PopulateConfigTreeView(path As String)
        ConfigTreeView.Nodes.Clear()
        If Directory.Exists(path) Then
            Dim rootNode As New TreeNode(Path.GetFileName(path)) With {.Tag = path}
            ConfigTreeView.Nodes.Add(rootNode)
            PopulateTreeViewNodes(path, rootNode)
            rootNode.Expand()
        End If
    End Sub

    Private Sub PopulateTreeViewNodes(dirPath As String, parentNode As TreeNode)
        Try
            For Each dir As String In Directory.GetDirectories(dirPath)
                Dim node As New TreeNode(Path.GetFileName(dir)) With {.Tag = dir}
                parentNode.Nodes.Add(node)
                PopulateTreeViewNodes(dir, node)
            Next
            For Each file As String In Directory.GetFiles(dirPath)
                Dim ext As String = Path.GetExtension(file).ToLower()
                If ext = ".cfg" OrElse ext = ".ini" OrElse ext = ".txt" OrElse ext = ".json" OrElse ext = ".xml" Then
                    Dim node As New TreeNode(Path.GetFileName(file)) With {.Tag = file}
                    parentNode.Nodes.Add(node)
                End If
            Next
        Catch ex As Exception
            ' Handle errors gracefully
        End Try
    End Sub

    Private Sub ConfigTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles ConfigTreeView.AfterSelect
        Dim path As String = e.Node.Tag.ToString()
        If File.Exists(path) Then
            Try
                ConfigEditorTextBox.Text = File.ReadAllText(path)
            Catch ex As Exception
                MessageBox.Show("Error reading file: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub ConfigOpenButton_Click(sender As Object, e As EventArgs) Handles ConfigOpenButton.Click
        Using dialog As New FolderBrowserDialog()
            If dialog.ShowDialog() = DialogResult.OK Then
                PopulateConfigTreeView(dialog.SelectedPath)
            End If
        End Using
    End Sub

    Private Sub ConfigRefreshButton_Click(sender As Object, e As EventArgs) Handles ConfigRefreshButton.Click
        If ConfigTreeView.Nodes.Count > 0 Then
            Dim path As String = ConfigTreeView.Nodes(0).Tag.ToString()
            PopulateConfigTreeView(path)
        End If
    End Sub

    Private Sub ConfigSaveButton_Click(sender As Object, e As EventArgs) Handles ConfigSaveButton.Click
        If ConfigTreeView.SelectedNode IsNot Nothing Then
            Dim path As String = ConfigTreeView.SelectedNode.Tag.ToString()
            If File.Exists(path) Then
                Try
                    File.WriteAllText(path, ConfigEditorTextBox.Text)
                    MessageBox.Show("File saved successfully.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show("Error writing file: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End If
    End Sub

    ' Profiles and custom games methods and event handlers
    Private Sub LoadProfiles()
        ProfileComboBox.Items.Clear()
        If File.Exists("Settings/Profiles.xml") Then
            Try
                Dim doc As New XmlDocument()
                doc.Load("Settings/Profiles.xml")
                For Each profileNode As XmlNode In doc.SelectNodes("//Profile")
                    ProfileComboBox.Items.Add(profileNode.Attributes("Name").Value)
                Next
            Catch ex As Exception
                UpdateStatus("Error loading profiles: " & ex.Message, True)
            End Try
        End If
        If ProfileComboBox.Items.Count > 0 Then
            ProfileComboBox.SelectedIndex = 0
        End If
    End Sub

    Private Sub AddProfileButton_Click(sender As Object, e As EventArgs) Handles AddProfileButton.Click
        Dim profileName As String = InputBox("Enter profile name:", "Add Profile")
        If Not String.IsNullOrEmpty(profileName) Then
            If Not ProfileComboBox.Items.Contains(profileName) Then
                ProfileComboBox.Items.Add(profileName)
                ProfileComboBox.SelectedItem = profileName
                SaveProfile(profileName)
            Else
                MessageBox.Show("Profile already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub SaveProfileButton_Click(sender As Object, e As EventArgs) Handles SaveProfileButton.Click
        If ProfileComboBox.SelectedItem IsNot Nothing Then
            SaveProfile(ProfileComboBox.SelectedItem.ToString())
        Else
            MessageBox.Show("Please select or add a profile first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub RemoveProfileButton_Click(sender As Object, e As EventArgs) Handles RemoveProfileButton.Click
        If ProfileComboBox.SelectedItem IsNot Nothing Then
            Dim profileName As String = ProfileComboBox.SelectedItem.ToString()
            If MessageBox.Show($"Are you sure you want to delete profile '{profileName}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                DeleteProfile(profileName)
                ProfileComboBox.Items.Remove(profileName)
                If ProfileComboBox.Items.Count > 0 Then
                    ProfileComboBox.SelectedIndex = 0
                Else
                    ClearProfileFields()
                End If
            End If
        End If
    End Sub

    Private Sub ProfileComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ProfileComboBox.SelectedIndexChanged
        If ProfileComboBox.SelectedItem IsNot Nothing Then
            Dim name As String = ProfileComboBox.SelectedItem.ToString()
            LoadProfile(name)
        End If
    End Sub

    Private Sub SaveProfile(name As String)
        Try
            Dim doc As New XmlDocument()
            If File.Exists("Settings/Profiles.xml") Then
                doc.Load("Settings/Profiles.xml")
            Else
                Dim root As XmlElement = doc.CreateElement("Profiles")
                doc.AppendChild(root)
            End If

            Dim rootNode As XmlNode = doc.SelectSingleNode("//Profiles")
            Dim existingNode As XmlNode = doc.SelectSingleNode($"//Profile[@Name='{name}']")
            If existingNode IsNot Nothing Then
                rootNode.RemoveChild(existingNode)
            End If

            Dim profileNode As XmlElement = doc.CreateElement("Profile")
            profileNode.SetAttribute("Name", name)

            AddProfileValue(doc, profileNode, "ServerPath", ServerPath.Text)
            AddProfileValue(doc, profileNode, "SteamAppID", SteamAppID)
            AddProfileValue(doc, profileNode, "Username", UsernameTextBox.Text)
            AddProfileValue(doc, profileNode, "Anonymous", AnonymousCheckBox.Checked.ToString())
            AddProfileValue(doc, profileNode, "RconHost", HostTextBox.Text)
            AddProfileValue(doc, profileNode, "RconPort", PortTextBox.Text)
            AddProfileValue(doc, profileNode, "RconPassword", PasswordTextBox.Text)

            rootNode.AppendChild(profileNode)
            doc.Save("Settings/Profiles.xml")
            UpdateStatus($"Profile '{name}' saved successfully.")
        Catch ex As Exception
            UpdateStatus($"Error saving profile: {ex.Message}", True)
        End Try
    End Sub

    Private Sub AddProfileValue(doc As XmlDocument, parent As XmlElement, key As String, value As String)
        Dim elem As XmlElement = doc.CreateElement(key)
        elem.InnerText = value
        parent.AppendChild(elem)
    End Sub

    Private Sub DeleteProfile(name As String)
        If File.Exists("Settings/Profiles.xml") Then
            Try
                Dim doc As New XmlDocument()
                doc.Load("Settings/Profiles.xml")
                Dim node As XmlNode = doc.SelectSingleNode($"//Profile[@Name='{name}']")
                If node IsNot Nothing Then
                    node.ParentNode.RemoveChild(node)
                    doc.Save("Settings/Profiles.xml")
                    UpdateStatus($"Profile '{name}' deleted.")
                End If
            Catch ex As Exception
                UpdateStatus($"Error deleting profile: {ex.Message}", True)
            End Try
        End If
    End Sub

    Private Sub LoadProfile(name As String)
        If File.Exists("Settings/Profiles.xml") Then
            Try
                Dim doc As New XmlDocument()
                doc.Load("Settings/Profiles.xml")
                Dim profileNode As XmlNode = doc.SelectSingleNode($"//Profile[@Name='{name}']")
                If profileNode IsNot Nothing Then
                    ServerPath.Text = GetProfileValue(profileNode, "ServerPath", "")
                    Dim appID As String = GetProfileValue(profileNode, "SteamAppID", "")
                    If Not String.IsNullOrEmpty(appID) Then
                        GamesList.SelectedValue = appID
                    End If
                    UsernameTextBox.Text = GetProfileValue(profileNode, "Username", "")
                    Dim anon As Boolean
                    If Boolean.TryParse(GetProfileValue(profileNode, "Anonymous", "False"), anon) Then
                        AnonymousCheckBox.Checked = anon
                    End If
                    HostTextBox.Text = GetProfileValue(profileNode, "RconHost", "")
                    PortTextBox.Text = GetProfileValue(profileNode, "RconPort", "")
                    PasswordTextBox.Text = GetProfileValue(profileNode, "RconPassword", "")
                    UpdateStatus($"Profile '{name}' loaded.")
                End If
            Catch ex As Exception
                UpdateStatus($"Error loading profile '{name}': {ex.Message}", True)
            End Try
        End If
    End Sub

    Private Function GetProfileValue(parentNode As XmlNode, key As String, defaultValue As String) As String
        Dim node As XmlNode = parentNode.SelectSingleNode(key)
        If node IsNot Nothing Then
            Return node.InnerText
        End If
        Return defaultValue
    End Function

    Private Sub ClearProfileFields()
        ' Optionally clear controls if no profiles remain
    End Sub

    ' Custom games list methods and event handlers
    Private Sub InitializeDefaultGamesList()
        Try
            Dim sourcePath As String = Path.Combine(My.Application.Info.DirectoryPath, "Resources", "SteamCMDGames.xml")
            If File.Exists(sourcePath) Then
                File.Copy(sourcePath, "Settings/SteamCMDGames.xml", True)
                LoadGamesList()
            Else
                GameDictionary.Clear()
                GameDictionary.Add("740", "Counter-Strike: Global Offensive")
                GameDictionary.Add("90", "Half-Life Dedicated Server")
                GameDictionary.Add("222860", "Left 4 Dead 2")
                GameDictionary.Add("232250", "Team Fortress 2")
                SaveGamesList()
            End If
        Catch ex As Exception
            UpdateStatus("Error initializing games list: " & ex.Message, True)
        End Try
    End Sub

    Private Sub LoadGamesList()
        GameDictionary.Clear()
        If File.Exists("Settings/SteamCMDGames.xml") Then
            Try
                Dim doc As New XmlDocument()
                doc.Load("Settings/SteamCMDGames.xml")
                For Each gameNode As XmlNode In doc.SelectNodes("//Game")
                    Dim id As String = gameNode.Attributes("id").Value
                    Dim name As String = gameNode.InnerText
                    If Not GameDictionary.ContainsKey(id) Then
                        GameDictionary.Add(id, name)
                    End If
                Next
            Catch ex As Exception
                UpdateStatus("Error loading games list: " & ex.Message, True)
            End Try
        End If
    End Sub

    Private Sub SaveGamesList()
        Try
            Dim doc As New XmlDocument()
            Dim root As XmlElement = doc.CreateElement("SteamCMD-Games")
            doc.AppendChild(root)

            For Each kvp As KeyValuePair(Of String, String) In GameDictionary
                Dim gameElem As XmlElement = doc.CreateElement("Game")
                gameElem.SetAttribute("id", kvp.Key)
                gameElem.InnerText = kvp.Value
                root.AppendChild(gameElem)
            Next

            doc.Save("Settings/SteamCMDGames.xml")
        Catch ex As Exception
            UpdateStatus("Error saving games list: " & ex.Message, True)
        End Try
    End Sub

    Private Sub AddCustomGameButton_Click(sender As Object, e As EventArgs) Handles AddCustomGameButton.Click
        Dim gameName As String = InputBox("Enter game name:", "Add Custom Game")
        If String.IsNullOrEmpty(gameName) Then Return
        Dim appID As String = InputBox("Enter Steam App ID:", "Add Custom Game")
        If String.IsNullOrEmpty(appID) Then Return

        If Not GameDictionary.ContainsKey(appID) Then
            GameDictionary.Add(appID, gameName)
            SaveGamesList()
            GamesList.DataSource = Nothing
            GamesList.DataSource = New BindingSource(GameDictionary, Nothing)
            GamesList.DisplayMember = "Value"
            GamesList.ValueMember = "Key"
            GamesList.SelectedValue = appID
            UpdateStatus($"Added custom game: {gameName} ({appID})")
        Else
            MessageBox.Show("A game with this App ID already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
End Class