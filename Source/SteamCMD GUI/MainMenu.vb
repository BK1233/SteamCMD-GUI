Imports System.Globalization
Imports System.Threading
Imports System.IO
Imports System.Net
Imports System.Xml
Imports System.Text
Imports System.Xml.Linq

Public Class MainMenu
    ' SteamCMD Installation
    Private SteamCMDExePath, SteamAppID, Login, ServerPathInstallation, ValidateApp, GoldSrcMod, Program, Game, PathForLog As String
    ' Run Server
    Private SrcdsExePath, GameMod, ServerName, ServerMap, NetworkType, MaxPlayers, RCON, UDPPort, DebugMode, SourceTV, ConsoleMode, InsecureMode, NoBots, DevMode, Parameters As String
    Public AdditionalCommands As String
    ' Strings
    Private CantFindSteamCMDString As String
    Private GameDictionary As New Dictionary(Of String, String)

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
    End Sub

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

    Private ThrSteamCMD As Thread
    Private WithEvents p As Process

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
        AppendOutputText(vbCrLf & e.Data)
    End Sub

    Private Sub ExecuteButton_Click() Handles ConsoleButton.Click
        p.StandardInput.WriteLine(ConsoleInput.Text)
        p.StandardInput.Flush()
        ConsoleInput.Text = ""
    End Sub

    Private Delegate Sub AppendOutputTextDelegate(ByVal text As String)
    Private Sub AppendOutputText(ByVal text As String)
        If ConsoleOutput.InvokeRequired Then
            Dim myDelegate As New AppendOutputTextDelegate(AddressOf AppendOutputText)
            Me.Invoke(myDelegate, text)
        Else
            ConsoleOutput.AppendText(text)
        End If
    End Sub

    'Run server inputs
    Private Sub SrcdsExePath_Browser() Handles SrcdsExePathTextBox.Click, SrcdsExeBrowserButton.Click
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            Dim selectedPath = FolderBrowserDialog1.SelectedPath
            Dim srcds32Path = Path.Combine(selectedPath, "srcds.exe")
            Dim srcds64Path = Path.Combine(selectedPath, "bin", "win64", "srcds.exe")

            If My.Computer.FileSystem.FileExists(srcds32Path) OrElse My.Computer.FileSystem.FileExists(srcds64Path) Then
                SrcdsExePathTextBox.Text = selectedPath
                SrcdsExePath = selectedPath
                MapList.Enabled = True
                UpdateStatus("Current path of 'srcds.exe' is " & selectedPath)
                SrcdsExePathOpen.Enabled = True
                CFGMenu.Enabled = True
                CommonFilesMenu.Enabled = True
                SMMenu.Enabled = True
                RunServerButton.Enabled = True
            Else
                SrcdsExePathOpen.Enabled = False
                MapList.Enabled = False
                CFGMenu.Enabled = False
                CommonFilesMenu.Enabled = False
                SMMenu.Enabled = False
                RunServerButton.Enabled = False
                UpdateStatus("Can't find 'srcds.exe' in the selected folder. Please ensure it's a valid server installation directory.", True)
            End If
        End If
    End Sub

    Private Sub SrcdsExePathOpen_Click() Handles SrcdsExePathOpen.Click
        Try
            Process.Start("explorer.exe", SrcdsExePath)
        Catch ex As Exception
            UpdateStatus($"Failed to open folder: {ex.Message}", True)
        End Try
    End Sub

    Private Sub ModList_SelectedIndex() Handles ModList.SelectedIndexChanged, ModList.EnabledChanged
        Dim gameMods As New Dictionary(Of String, String) From {
            {"Alien Swarm", "alienswarm"},
            {"Counter-Strike: Global Offensive", "csgo"},
            {"Counter-Strike: Source", "cstrike"},
            {"Day of Defeat: Source", "dod"},
            {"Dota 2", "dota"},
            {"Garry's Mod", "garrysmod"},
            {"Half-Life 2: Deathmatch", "hl2mp"},
            {"Left 4 Dead", "left4dead"},
            {"Left 4 Dead 2", "left4dead2"},
            {"Team Fortress 2", "tf"}
        }

        If gameMods.ContainsKey(ModList.Text) Then
            GameMod = gameMods(ModList.Text)
            UpdateStatus("Game/Mod to run: " & ModList.Text & " - Game parameter: " & GameMod)
        End If
    End Sub

    Private Sub ModHelpButton_Click() Handles ModHelpButton.Click
        Try
            Process.Start("https://developer.valvesoftware.com/wiki/Game_Name_Abbreviations")
        Catch ex As Exception
            UpdateStatus($"Failed to open link: {ex.Message}", True)
        End Try
    End Sub

    Private Sub CustomModCheckBox_CheckedChanged() Handles CustomModCheckBox.CheckedChanged, CustomModTextBox.TextChanged
        If CustomModCheckBox.Checked = True Then
            ModList.Enabled = False
            CustomModTextBox.Enabled = True
            GameMod = CustomModTextBox.Text
            DebugModeCheckBox.Enabled = False
            SourceTVCheckBox.Enabled = False
            ConsoleCheckBox.Checked = False
            ConsoleCheckBox.Enabled = False
            InsecureCheckBox.Enabled = False
            BotsCheckBox.Enabled = False
            DevModeCheckBox.Enabled = False
            UpdateStatus("Custom Mod: " & GameMod)
        Else
            ModList.Enabled = True
            CustomModTextBox.Enabled = False
            DebugModeCheckBox.Enabled = True
            SourceTVCheckBox.Enabled = True
            ConsoleCheckBox.Checked = True
            ConsoleCheckBox.Enabled = True
            InsecureCheckBox.Enabled = True
            BotsCheckBox.Enabled = True
            DevModeCheckBox.Enabled = True
        End If
    End Sub

    Private Sub ServerNameTextBox_TextChanged() Handles ServerNameTextBox.TextChanged
        ServerName = ServerNameTextBox.Text
        UpdateStatus("The name of the server will be: " & ServerName)
    End Sub

    Private Sub MapList_DropDown() Handles MapList.DropDown
        MapList.Items.Clear()
        Dim mapfolderpath As String
        mapfolderpath = SrcdsExePathTextBox.Text & "\" & GameMod & "\maps"
        If Directory.Exists(mapfolderpath) Then
            For Each MapFile As String In My.Computer.FileSystem.GetFiles _
                (mapfolderpath, FileIO.SearchOption.SearchTopLevelOnly, "*.bsp")
                MapList.Items.Add(Path.GetFileNameWithoutExtension(MapFile))
            Next
        Else
            UpdateStatus("The 'map' folder is empty or doesn't exist!", True)
        End If
    End Sub

    Private Sub MapList_ChooseMap() Handles MapList.SelectedIndexChanged
        ServerMap = MapList.Text
        UpdateStatus("The map of the server will be: " & ServerMap)
    End Sub

    Private Sub CheckBoxMask_CheckedChanged() Handles CheckBoxMask.CheckedChanged
        If CheckBoxMask.Checked = True Then
            RconTextBox.PasswordChar = "*"
            RconTextBox.Font = New Font("Microsoft Sans Serif", 9.75, FontStyle.Bold)
        Else
            RconTextBox.PasswordChar = ""
            RconTextBox.Font = New Font("Microsoft Sans Serif", 8.25, FontStyle.Regular)
        End If
    End Sub

    Private Sub MaxPlayersTexBox_ValueChanged() Handles MaxPlayersTexBox.TextChanged
        Dim players As Integer
        If Integer.TryParse(MaxPlayersTexBox.Text, players) Then
            MaxPlayers = players.ToString()
            UpdateStatus("Max players set to " & MaxPlayers)
        Else
            UpdateStatus("Invalid number for Max Players.", True)
        End If
    End Sub

    Private Sub NetworkComboBox_SelectedIndexChanged() Handles NetworkComboBox.SelectedIndexChanged
        NetworkType = NetworkComboBox.SelectedIndex
        UpdateStatus("Cvar sv_lan set to " & NetworkType)
    End Sub

    Private Sub RconTextBox_MaskInputRejected() Handles RconTextBox.TextChanged
        RCON = RconTextBox.Text
    End Sub

    Private Sub UDPPortTexBox_ValueChanged() Handles UDPPortTexBox.TextChanged
        Dim port As Integer
        If Integer.TryParse(UDPPortTexBox.Text, port) Then
            UDPPort = port.ToString()
            UpdateStatus("UPD port set to " & UDPPort)
        Else
            UpdateStatus("Invalid number for UDP Port.", True)
        End If
    End Sub

    'Command-line Arguments
    Private Sub DebugModeCheckBox_CheckedChanged() Handles DebugModeCheckBox.CheckedChanged
        If DebugModeCheckBox.Checked = True Then
            DebugMode = "-debug "
        Else
            DebugMode = ""
        End If
    End Sub

    Private Sub SourceTVCheckBox_CheckedChanged() Handles SourceTVCheckBox.CheckedChanged
        If SourceTVCheckBox.Checked = True Then
            SourceTV = ""
        Else
            SourceTV = "-nohltv "
        End If
    End Sub

    Private Sub ConsoleCheckBox_CheckedChanged() Handles ConsoleCheckBox.CheckedChanged
        If ConsoleCheckBox.Checked = True Then
            ConsoleMode = "-console "
        Else
            ConsoleMode = ""
        End If
    End Sub

    Private Sub InsecureCheckBox_CheckedChanged() Handles InsecureCheckBox.CheckedChanged
        If InsecureCheckBox.Checked = True Then
            InsecureMode = "-insecure "
        Else
            InsecureMode = ""
        End If
    End Sub

    Private Sub BotsCheckBox_CheckedChanged() Handles BotsCheckBox.CheckedChanged
        If BotsCheckBox.Checked = True Then
            NoBots = "-nobots "
        Else
            NoBots = ""
        End If
    End Sub

    Private Sub DevModeCheckBox_CheckedChanged() Handles DevModeCheckBox.CheckedChanged
        If DevModeCheckBox.Checked = True Then
            DevMode = "-dev "
        Else
            DevMode = ""
        End If
    End Sub

    Private Sub AddButton_Click() Handles AddButton.Click
        Dim optionsWindow As New CommandLineOptionsWindow()
        If optionsWindow.ShowDialog(Me) = DialogResult.OK Then
            AdditionalCommands = optionsWindow.AdditionalCommands
        End If
    End Sub

    Private Sub RunServerButton_Click() Handles RunServerButton.Click
        Dim serverManager As New ServerManager(Me)
        serverManager.RunServer()
    End Sub

    ' Tools buttons
    Private Sub VDCButton_Click() Handles VDCButton.Click
        Try
            Process.Start("https://developer.valvesoftware.com/wiki/SteamCMD")
        Catch ex As Exception
            UpdateStatus($"Failed to open link: {ex.Message}", True)
        End Try
    End Sub

    Private Sub CheckUpdatesButton_Click() Handles CheckUpdatesButton.Click
        Try
            Process.Start("https://github.com/BK1233/SteamCMD-GUI#last-changes")
        Catch ex As Exception
            UpdateStatus($"Failed to open link: {ex.Message}", True)
        End Try
    End Sub

    Private Sub SMButton_Click() Handles SMButton.Click
        Try
            Process.Start("http://www.sourcemod.net")
        Catch ex As Exception
            UpdateStatus($"Failed to open link: {ex.Message}", True)
        End Try
    End Sub

    Private Sub MMButton_Click() Handles MMButton.Click
        Try
            Process.Start("http://www.sourcemm.net")
        Catch ex As Exception
            UpdateStatus($"Failed to open link: {ex.Message}", True)
        End Try
    End Sub

    Private Sub ESButton_Click() Handles ESButton.Click
        Try
            Process.Start("http://addons.eventscripts.com")
        Catch ex As Exception
            UpdateStatus($"Failed to open link: {ex.Message}", True)
        End Try
    End Sub

    'Private Sub MAPButton_Click() Handles MAPButton.Click
    '    Process.Start("http://mani-admin-plugin.com")
    'End Sub

    Private Sub AboutButton_Click() Handles AboutButton.Click, AboutToolStripMenuItem.Click
        Dim about As New AboutWindow()
        about.ShowDialog(Me)
    End Sub

    Private Sub ExitButton_Click() Handles ExitButton.Click, ExitMenu.Click
        Close()
    End Sub

    Private Sub DonateButton_Click() Handles DonateButton.Click
        Try
            Process.Start("https://www.paypal.me/DioJoestar")
        Catch ex As Exception
            UpdateStatus($"Failed to open link: {ex.Message}", True)
        End Try
    End Sub

    'Menu buttons
    Private Sub SaveMenu_Click() Handles SaveMenu.Click, SaveButton.Click
        If String.IsNullOrWhiteSpace(SrcdsExePath) Then
            UpdateStatus("Please, select where 'srcds.exe' is located.", True)
            Return
        End If

        SaveFileDialog1.InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Settings")
        SaveFileDialog1.Filter = "Extensible Markup Language (*.xml)|*.xml"
        SaveFileDialog1.FileName = "Config.xml"

        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            Try
                Dim config As New XDocument(
                    New XComment("Config used by SteamCMD GUI"),
                    New XElement("Config",
                        New XElement("Srcds-Config",
                            New XElement("Path", SrcdsExePath)
                        ),
                        New XElement("Server-Config",
                            New XElement("HostName", ServerName),
                            If(ModList.Enabled = False,
                                New XElement("CustomMod", CustomModTextBox.Text),
                                New XElement("Mod", ModList.Text)),
                            New XElement("Map", ServerMap),
                            New XElement("Network", NetworkType),
                            New XElement("Players", MaxPlayers),
                            New XElement("RCON", RCON),
                            New XElement("Port", UDPPort),
                            If(Not String.IsNullOrEmpty(AdditionalCommands),
                                New XElement("AdditionalCommands", AdditionalCommands), Nothing)
                        )
                    )
                )
                config.Save(SaveFileDialog1.FileName)
                UpdateStatus($"{Path.GetFileName(SaveFileDialog1.FileName)} file saved.")
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
            Catch ex As Exception
                UpdateStatus($"Error saving config file: {ex.Message}", True)
            End Try
        End If
    End Sub

    Private Sub LoadMenu_Click() Handles LoadMenu.Click
        XmlConfigOpenFileDialog.InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Settings")
        XmlConfigOpenFileDialog.FileName = "*.xml"
        XmlConfigOpenFileDialog.Filter = "Extensible Markup Language (*.xml)|*.xml"

        If XmlConfigOpenFileDialog.ShowDialog() = DialogResult.OK Then
            Try
                Dim xdoc = XDocument.Load(XmlConfigOpenFileDialog.FileName)
                SrcdsExePath = xdoc.Descendants("Path").FirstOrDefault()?.Value
                SrcdsExePathTextBox.Text = SrcdsExePath
                MapList.Enabled = True
                CFGMenu.Enabled = True
                CommonFilesMenu.Enabled = True
                SMMenu.Enabled = True
                RunServerButton.Enabled = True
                SrcdsExePathOpen.Enabled = True

                ServerNameTextBox.Text = xdoc.Descendants("HostName").FirstOrDefault()?.Value
                ModList.Text = xdoc.Descendants("Mod").FirstOrDefault()?.Value
                ModList_SelectedIndex()

                Dim customMod = xdoc.Descendants("CustomMod").FirstOrDefault()
                If customMod IsNot Nothing Then
                    CustomModTextBox.Text = customMod.Value
                    CustomModCheckBox.Checked = True
                End If

                MapList.Enabled = True
                ServerMap = xdoc.Descendants("Map").FirstOrDefault()?.Value
                MapList.Text = ServerMap

                NetworkComboBox.SelectedIndex = CInt(xdoc.Descendants("Network").FirstOrDefault()?.Value)
                MaxPlayers = xdoc.Descendants("Players").FirstOrDefault()?.Value
                MaxPlayersTexBox.Value = CInt(MaxPlayers)
                RCON = xdoc.Descendants("RCON").FirstOrDefault()?.Value
                RconTextBox.Text = RCON
                CheckBoxMask.Checked = True
                UDPPort = xdoc.Descendants("Port").FirstOrDefault()?.Value
                UDPPortTexBox.Value = CInt(UDPPort)
                AdditionalCommands = xdoc.Descendants("AdditionalCommands").FirstOrDefault()?.Value

                TabMenu.SelectedTab = RunTab
                GroupBox1.Show()
                GroupBox3.Show()
                UpdateStatus("The config file has been loaded.")
            Catch ex As Exception
                UpdateStatus("Error loading config file: " & ex.Message, True)
            End Try
        End If
    End Sub

    Private Sub CFGMenu_DropDownOpening() Handles ToolsMenu.Click
        If CFGMenu.Enabled = True Then
            CFGMenu.DropDownItems.Clear()
            CFGMenu.DropDownItems.Add(NewFileToolStripMenuItem)
            CFGMenu.DropDownItems.Add("-")
            Dim cfgfolderpath As String
            cfgfolderpath = SrcdsExePathTextBox.Text & "\" & GameMod & "\cfg"
            If Directory.Exists(cfgfolderpath) = True Then
                'Create new submenu for each cfg file
                For Each CfgFile As String In My.Computer.FileSystem.GetFiles _
                        (cfgfolderpath, FileIO.SearchOption.SearchTopLevelOnly, "*.cfg")
                    Dim text = Path.GetFileNameWithoutExtension(CfgFile)
                    Dim item As ToolStripItem = CFGMenu.DropDownItems.Add(text)
                    item.Tag = CfgFile
                    AddHandler item.Click, AddressOf CfgMenuItems_Click
                    'This works thanks to Hans Passant ^^
                Next
            Else
                UpdateStatus("Can't find the CFG folder. New one created.")
                Directory.CreateDirectory(cfgfolderpath)
            End If
        Else
            UpdateStatus("Can't find the server files!", True)
        End If
    End Sub

    Private Sub CfgMenuItems_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim item = CType(sender, ToolStripItem)
        Dim path = CStr(item.Tag)
        Try
            Process.Start(path)
        Catch ex As Exception
            UpdateStatus($"Failed to open file: {ex.Message}", True)
        End Try
    End Sub

    Private Sub NewFile_Click() Handles NewFileToolStripMenuItem.Click
        SaveFileDialog1.InitialDirectory = SrcdsExePathTextBox.Text & "\" & GameMod & "\cfg"
        SaveFileDialog1.Filter = "Configuration files (*.cfg)|*.cfg"
        SaveFileDialog1.FileName = "Config.cfg"
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            File.Create(SaveFileDialog1.FileName).Dispose()
            Try
                Process.Start(SaveFileDialog1.FileName)
            Catch ex As Exception
                UpdateStatus($"Failed to open file: {ex.Message}", True)
            End Try
            UpdateStatus("File " & SaveFileDialog1.FileName & " has been saved.")
        End If
    End Sub

    Private Sub MenuTxt_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MotdTxtButton.Click, MapcycleTxtButton.Click, MaplistTxtButton.Click
        Dim TxtFile As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim MotdPath As String = SrcdsExePath & "\" & GameMod & "\" & TxtFile.Text & ".txt"
        Try
            If File.Exists(MotdPath) Then
                Process.Start(MotdPath)
            Else
                File.Create(MotdPath).Dispose()
                Process.Start(MotdPath)
                UpdateStatus(TxtFile.Text & " file not found. New one created.")
            End If
        Catch ex As Exception
            UpdateStatus($"Failed to open file: {ex.Message}", True)
        End Try
    End Sub

    Private Sub SMMenu_Click() Handles SMMenu.MouseHover, SMMenu.Click
        If SMMenu.Enabled = True Then
            SMMenu.DropDownItems.Clear()
            Dim SMFilesPath As String
            SMFilesPath = SrcdsExePathTextBox.Text & "\" & GameMod & "\addons\sourcemod\configs"
            If Directory.Exists(SMFilesPath) Then
                'Create new submenu for each cfg and txt file
                For Each SMFile As String In My.Computer.FileSystem.GetFiles _
                        (SMFilesPath, FileIO.SearchOption.SearchTopLevelOnly, "*.cfg", "*.txt", "*.ini")
                    Dim text = Path.GetFileNameWithoutExtension(SMFile)
                    Dim item As ToolStripItem = SMMenu.DropDownItems.Add(text)
                    item.Tag = SMFile
                    AddHandler item.Click, AddressOf SMFileMenuItems_Click
                Next
            Else
                UpdateStatus("Seems that SourceMod isn't installed.", True)
            End If
        End If
    End Sub

    Private Sub SMFileMenuItems_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim item = CType(sender, ToolStripItem)
        Dim path = CStr(item.Tag)
        Try
            Process.Start(path)
        Catch ex As Exception
            UpdateStatus($"Failed to open file: {ex.Message}", True)
        End Try
    End Sub

    Private Sub LogMenu_Click() Handles LogMenu.MouseHover, LogMenu.Click
        If LogMenu.Enabled = True Then
            LogMenu.DropDownItems.Clear()
            Dim LogFilesPath As String
            LogFilesPath = ExePath.Text & "\logs"
            If Directory.Exists(LogFilesPath) Then
                'Create new submenu for each txt file
                For Each LogFile As String In My.Computer.FileSystem.GetFiles _
                        (LogFilesPath, FileIO.SearchOption.SearchTopLevelOnly, "*.txt")
                    Dim text = Path.GetFileNameWithoutExtension(LogFile)
                    Dim item As ToolStripItem = LogMenu.DropDownItems.Add(text)
                    item.Tag = LogFile
                    AddHandler item.Click, AddressOf LogFileMenuItems_Click
                Next
            End If
        End If
    End Sub

    Private Sub LogFileMenuItems_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim item = CType(sender, ToolStripItem)
        Dim path = CStr(item.Tag)
        Try
            Process.Start(path)
        Catch ex As Exception
            UpdateStatus($"Failed to open file: {ex.Message}", True)
        End Try
    End Sub

    ' Console Tab
    Private Sub ConsoleConnect_Click() Handles ConsoleConnect.Click
        'Stop steamcmd.exe
        For Each proc As Process In Process.GetProcessesByName("steamcmd")
            Dim result As Integer = MessageBox.Show("Really want to stop and close SteamCMD?", "Stop SteamCMD", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                If Not proc.HasExited Then
                    Game = "Game: " & GamesList.Text
                    Program = "SteamCmd.exe"
                    PathForLog = "Server path: " & ServerPathInstallation
                    SaveLog()
                    proc.Kill()
                    ConsoleInput.Enabled = False
                    ConsoleButton.Enabled = False
                End If
                UpdateStatus("SteamCMD closed.", True)
            End If
        Next proc
    End Sub

    Private Sub ConsoleOpenLog_Click() Handles ConsoleOpenLog.Click
        Try
            Process.Start("explorer.exe", ".\Logs")
        Catch ex As Exception
            UpdateStatus($"Failed to open folder: {ex.Message}", True)
        End Try
    End Sub

    Private Sub ConsoleSaveLog_Click() Handles ConsoleSaveLog.Click
        SaveFileDialog1.InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs")
        SaveFileDialog1.DefaultExt = "*.txt"
        SaveFileDialog1.Filter = "Text Files (*.txt)|*.txt"
        SaveFileDialog1.FileName = "log.txt"

        If (SaveFileDialog1.ShowDialog() = DialogResult.OK) _
            AndAlso (SaveFileDialog1.FileName.Length > 0) Then
            File.WriteAllText(SaveFileDialog1.FileName, ConsoleOutput.Text)
            Try
                Process.Start(SaveFileDialog1.FileName)
            Catch ex As Exception
                UpdateStatus($"Failed to open file: {ex.Message}", True)
            End Try
            UpdateStatus("File " & Path.GetFileName(SaveFileDialog1.FileName) & " has been saved in Logs folder.")
        End If
    End Sub

    Private Sub ConsoleClearLog_Click() Handles ConsoleClearLog.Click
        Dim result As Integer = MessageBox.Show("Really want to clear all the content?", "Clear console", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            ConsoleOutput.Clear()
            UpdateStatus("The console has been cleaned.")
        End If
    End Sub

    Private Sub AddCustomGameButton_Click(ByVal sender As Object, ByVal e As EventArgs) Handles AddCustomGameButton.Click
        Dim Name As String = ""
        Dim ID As String = ""

        Name = InputBox("Custom Game Name")
        ID = InputBox("Custom Game App ID")

        If ("" = Name) Then
            My.Computer.Audio.PlaySystemSound( _
            Media.SystemSounds.Hand)
            MessageBox.Show("Custom Game Name was not entered.", "Add Custom Game Error")
            Return
        End If

        If ("" = ID) Then
            My.Computer.Audio.PlaySystemSound( _
            Media.SystemSounds.Hand)
            MessageBox.Show("Custom Game ID was not entered.", "Add Custom Game Error")
            Return
        End If

        Dim TestInt As Integer = 0
        Integer.TryParse(ID, TestInt)
        If (TestInt = 0) Then
            My.Computer.Audio.PlaySystemSound( _
            Media.SystemSounds.Hand)
            MessageBox.Show("Custom Game ID was not a number (e.x 444880).", "Add Custom Game Error")
            Return
        End If

        GameDictionary.Add(ID, Name)
        'GamesList.DataSource.ResetBindings(False)
        WriteOutDictionaryAsXml(GameDictionary)
        GamesList.DataSource = New BindingSource(GameDictionary, Nothing)

        GamesList.SelectedIndex = GamesList.FindStringExact(Name)
        UpdateStatus("Added custom game: " & Name)
    End Sub

    Private Sub LoadSteamCMDPath()
        Dim steamCmdPathFile As String = "Settings/SteamCMDPath.xml"
        If File.Exists(steamCmdPathFile) Then
            Try
                Dim xdoc = XDocument.Load(steamCmdPathFile)
                Dim cmdPathElement = xdoc.Descendants("CMDPath").FirstOrDefault()
                If cmdPathElement IsNot Nothing Then
                    Dim cmdPath = cmdPathElement.Value
                    ExePath.Text = cmdPath
                    FolderBrowserDialog1.SelectedPath = cmdPath
                    SteamCMDExePath = cmdPath
                    LogMenu.Enabled = True
                End If
            Catch ex As Exception
                UpdateStatus("Error reading SteamCMD path configuration: " & ex.Message, True)
            End Try
        End If
    End Sub

    Private Sub LoadGamesList()
        Try
            GameDictionary.Clear()
            Dim xdoc = XDocument.Load("Settings/SteamCMDGames.xml")
            For Each gameElement In xdoc.Descendants("Game")
                Dim id = gameElement.Attribute("id")?.Value
                Dim name = gameElement.Value
                If id IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(name) Then
                    GameDictionary.Add(id, name)
                End If
            Next
        Catch ex As Exception
            UpdateStatus("Error loading games list: " & ex.Message, True)
            InitializeDefaultGamesList()
        End Try
    End Sub

    Private Sub InitializeDefaultGamesList()
        GameDictionary.Clear()
        GameDictionary.Add("635", "Alien Swarm")
        GameDictionary.Add("740", "Counter-Strike: Global Offensive")
        GameDictionary.Add("232330", "Counter-Strike: Source")
        GameDictionary.Add("232290", "Day of Defeat: Source")
        GameDictionary.Add("570", "Dota 2")
        GameDictionary.Add("4020", "Garry's Mod")
        GameDictionary.Add("90", "Half-Life Dedicated Server")
        GameDictionary.Add("232370", "Half-Life 2: Deathmatch")
        GameDictionary.Add("510", "Left 4 Dead")
        GameDictionary.Add("222860", "Left 4 Dead 2")
        GameDictionary.Add("232250", "Team Fortress 2")
        WriteOutDictionaryAsXml(GameDictionary)
    End Sub

    Private Sub WriteOutDictionaryAsXml(ByVal dict As Dictionary(Of String, String))
        Dim games = dict.Select(Function(kvp) New XElement("Game", New XAttribute("id", kvp.Key), kvp.Value))
        Dim xdoc As New XDocument(
            New XComment("Custom Games Config used by SteamCMD GUI"),
            New XComment("This config is loaded automatically."),
            New XElement("SteamCMD-Games", games)
        )
        xdoc.Save("Settings/SteamCMDGames.xml")
    End Sub

    Private Sub IPButton_Click() Handles IPButton.Click
        Clipboard.SetText(PublicIP, TextDataFormat.UnicodeText)
        UpdateStatus("Public IP copied")
    End Sub
End Class

Public Class ServerManager
    Private ReadOnly mainMenu As MainMenu

    Public ReadOnly Property SteamCMDExePath As String
    Public ReadOnly Property SteamAppID As String
    Public ReadOnly Property Login As String
    Public ReadOnly Property ServerPathInstallation As String
    Public ReadOnly Property ValidateApp As String
    Public ReadOnly Property GoldSrcMod As String
    Public ReadOnly Property Program As String
    Public ReadOnly Property Game As String
    Public ReadOnly Property PathForLog As String
    Public ReadOnly Property SrcdsExePath As String
    Public ReadOnly Property GameMod As String
    Public ReadOnly Property ServerName As String
    Public ReadOnly Property ServerMap As String
    Public ReadOnly Property NetworkType As String
    Public ReadOnly Property MaxPlayers As String
    Public ReadOnly Property RCON As String
    Public ReadOnly Property UDPPort As String
    Public ReadOnly Property DebugMode As String
    Public ReadOnly Property SourceTV As String
    Public ReadOnly Property ConsoleMode As String
    Public ReadOnly Property InsecureMode As String
    Public ReadOnly Property NoBots As String
    Public ReadOnly Property DevMode As String
    Public ReadOnly Property Parameters As String
    Public ReadOnly Property AdditionalCommands As String

    Public Sub New(ByVal mainMenu As MainMenu)
        Me.mainMenu = mainMenu
        Me.SteamCMDExePath = mainMenu.SteamCMDExePath
        Me.SteamAppID = mainMenu.SteamAppID
        Me.Login = mainMenu.Login
        Me.ServerPathInstallation = mainMenu.ServerPathInstallation
        Me.ValidateApp = mainMenu.ValidateApp
        Me.GoldSrcMod = mainMenu.GoldSrcMod
        Me.Program = mainMenu.Program
        Me.Game = mainMenu.Game
        Me.PathForLog = mainMenu.PathForLog
        Me.SrcdsExePath = mainMenu.SrcdsExePath
        Me.GameMod = mainMenu.GameMod
        Me.ServerName = mainMenu.ServerName
        Me.ServerMap = mainMenu.ServerMap
        Me.NetworkType = mainMenu.NetworkType
        Me.MaxPlayers = mainMenu.MaxPlayers
        Me.RCON = mainMenu.RCON
        Me.UDPPort = mainMenu.UDPPort
        Me.DebugMode = mainMenu.DebugMode
        Me.SourceTV = mainMenu.SourceTV
        Me.ConsoleMode = mainMenu.ConsoleMode
        Me.InsecureMode = mainMenu.InsecureMode
        Me.NoBots = mainMenu.NoBots
        Me.DevMode = mainMenu.DevMode
        Me.Parameters = mainMenu.Parameters
        Me.AdditionalCommands = mainMenu.AdditionalCommands
    End Sub

    Public Sub RunServer()
        Dim baseServerPath As String = mainMenu.SrcdsExePathTextBox.Text
        If String.IsNullOrWhiteSpace(baseServerPath) Then
            mainMenu.UpdateStatus("SRCDS path is not set. Please configure it in the 'Run' tab.", True)
            Return
        End If

        Dim is64Bit As Boolean = mainMenu.Is64BitCheckBox.Checked
        Dim srcdsFinalPath As String = If(is64Bit, Path.Combine(baseServerPath, "bin", "win64", "srcds.exe"), Path.Combine(baseServerPath, "srcds.exe"))

        If Not My.Computer.FileSystem.FileExists(srcdsFinalPath) Then
            mainMenu.UpdateStatus("Can't find 'srcds.exe' at: " & srcdsFinalPath, True)
            Return
        End If

        If String.IsNullOrWhiteSpace(GameMod) Then
            mainMenu.UpdateStatus("Please, select a game.", True)
            Return
        End If

        If String.IsNullOrWhiteSpace(mainMenu.ServerNameTextBox.Text) Then
            mainMenu.UpdateStatus("Please, type a name for the server.", True)
            Return
        End If

        If String.IsNullOrWhiteSpace(mainMenu.MapList.Text) Then
            mainMenu.UpdateStatus("Select the default map.", True)
            Return
        End If

        Dim argsBuilder As New StringBuilder()
        argsBuilder.Append(DebugMode)
        argsBuilder.Append(SourceTV)
        argsBuilder.Append(ConsoleMode)
        argsBuilder.Append(InsecureMode)
        argsBuilder.Append(NoBots)
        argsBuilder.Append(DevMode)
        argsBuilder.AppendFormat("-game {0} ", GameMod)
        argsBuilder.AppendFormat("-port {0} ", UDPPort)
        argsBuilder.AppendFormat("+hostname ""{0}"" ", mainMenu.ServerNameTextBox.Text)
        argsBuilder.AppendFormat("+map {0} ", mainMenu.MapList.Text)
        argsBuilder.AppendFormat("+maxplayers {0} ", mainMenu.MaxPlayersTexBox.Text)
        argsBuilder.AppendFormat("+sv_lan {0} ", mainMenu.NetworkComboBox.SelectedIndex)
        argsBuilder.Append(AdditionalCommands)

        mainMenu.UpdateStatus("Running server...")

        Dim p As New Process
        With p.StartInfo
            .FileName = srcdsFinalPath
            .UseShellExecute = False
            .CreateNoWindow = False
            .UseShellExecute = False
            .CreateNoWindow = False
            .Arguments = argsBuilder.ToString()
        End With

        Try
            p.Start()
        Catch ex As Exception
            mainMenu.UpdateStatus("Failed to start server: " & ex.Message, True)
        End Try
    End Sub
End Class