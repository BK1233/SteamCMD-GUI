Imports System.Globalization
Imports System.Threading
Imports System.IO
Imports System.Net
Imports System.Xml
Imports System.Text
Imports System.Xml.Linq

Module Module1
    Public SteamCMDExePath, SteamAppID, Login, ServerPathInstallation, ValidateApp, GoldSrcMod, Program, Game, PathForLog As String
    ' Run Server
    Public SrcdsExePath, GameMod, ServerName, ServerMap, NetworkType, MaxPlayers, RCON, UDPPort, DebugMode, SourceTV, ConsoleMode, InsecureMode, NoBots, DevMode, AdditionalCommands, Parameters As String
    ' Strings
    Public CantFindSteamCMDString As String
    Public GameDictionary As Dictionary(Of String, String) = New Dictionary(Of String, String)
End Module


Public Class MainMenu
    Dim WithEvents WC As New WebClient

    Dim LocalHost As String = Dns.GetHostName
    Dim IPs As IPHostEntry = Dns.GetHostEntry(LocalHost)
    Dim PublicIP As String

    Private Const GOLDSRC_APP_ID As Integer = 90

    Private Declare Function GetInputState Lib "user32" () As Int32

    Private Sub Form1_Load() Handles MyBase.Load
        If My.Computer.Network.IsAvailable Then
            Try
                PublicIP = WC.DownloadString("http://ipv4.icanhazip.com/")
            Catch ex As WebException
                PublicIP = "Network down"
                UpdateStatus("Could not retrieve public IP: " & ex.Message, True)
            End Try
        Else
            PublicIP = "Network down"
        End If

        Icon = My.Resources.SteamCMDGUI_Icon
        TabMenu.Size = New Size(417, 303)
        ThrSteamCMD = New Thread(AddressOf ThreadTaskSteamCMD)
        ModList.SelectedIndex = 1
        NetworkComboBox.SelectedIndex = 0
        ConsoleCommandList.SelectedIndex = 0
        Status.Text = ""
        Tips()
        IPPrint()
        If Not Directory.Exists("Settings") Then
            Directory.CreateDirectory("Settings")
        End If
        If Not Directory.Exists("Logs") Then
            Directory.CreateDirectory("Logs")
        End If
        If File.Exists("Settings/SteamCMDPath.xml") Then
            Try
                Dim xDoc = XDocument.Load("Settings/SteamCMDPath.xml")
                Dim cmdPathElement = xDoc.Descendants("CMDPath").FirstOrDefault()
                If cmdPathElement IsNot Nothing Then
                    ExePath.Text = cmdPathElement.Value
                    FolderBrowserDialog1.SelectedPath = ExePath.Text
                    SteamCMDExePath = ExePath.Text
                    LogMenu.Enabled = True
                End If
            Catch ex As Exception
                UpdateStatus("Error reading SteamCMD path configuration: " & ex.Message, True)
            End Try
        End If
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

    Private Sub UpdateStatus(text As String, Optional isError As Boolean = False)
        Status.Text = text
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
        For Each LocalIP As System.Net.IPAddress In IPs.AddressList
            ConsoleOutput.Text = "Local IP address:" & vbCr & vbTab & LocalIP.ToString & vbCr & vbCr & "Public IP address:" & vbCr & vbTab & PublicIP
        Next
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
            DonwloadBar.Show()
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
        DonwloadBar.Hide()
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
        DonwloadBar.Value = e.ProgressPercentage
        If DonwloadBar.Value = 100 Then
            UpdateStatus("The file 'steamcmd.zip' has been downloaded. Please, unzip it.")
            DonwloadBar.Value = 0
            My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
            SteamCMDDownloadButton.Enabled = True
        End If
    End Sub

    Private Sub ExePath_Browser() Handles ExePath.Click, ExeBrowserButton.Click
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            If My.Computer.FileSystem.FileExists(FolderBrowserDialog1.SelectedPath & "\steamcmd.exe") Then
                ExePath.Text = FolderBrowserDialog1.SelectedPath
                SteamCMDExePath = FolderBrowserDialog1.SelectedPath

                Dim xDoc As New XDocument(
                    New XComment("Config used by SteamCMD GUI"),
                    New XComment("This config it's loaded automatically."),
                    New XElement("SteamCMD-Config",
                        New XElement("CMDPath", SteamCMDExePath)
                    )
                )
                xDoc.Save("Settings/SteamCMDPath.xml")

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
        Process.Start("https://developer.valvesoftware.com/wiki/Dedicated_Servers_List")
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
        FolderBrowserDialog1.SelectedPath = SteamCMDExePath
        If My.Computer.FileSystem.FileExists(FolderBrowserDialog1.SelectedPath & "\steamcmd.exe") Then
            If String.IsNullOrWhiteSpace(SteamAppID) Then
                UpdateStatus("Please select a game to install/update.", True)
            Else
                If AnonymousCheckBox.Checked = True Then
                    Login = "anonymous"
                Else
                    Dim UserName As String
                    Dim Passwd As String
                    UserName = UsernameTextBox.Text
                    Passwd = PasswdTextBox.Text
                    Login = UserName & " " & Passwd
                End If
                If String.IsNullOrWhiteSpace(UsernameTextBox.Text) AndAlso AnonymousCheckBox.Checked = False Then
                    UpdateStatus("Please, type your Steam name.", True)
                Else
                    If String.IsNullOrWhiteSpace(PasswdTextBox.Text) AndAlso AnonymousCheckBox.Checked = False Then
                        UpdateStatus("Please, type your Steam password. You can install many games as 'anonymous'.", True)
                    Else
                        If String.IsNullOrWhiteSpace(ServerPath.Text) Then
                            UpdateStatus("Please, select the path where you want to install the server.", True)
                        Else
                            If GoldSrcModInput.Visible = True Then
                                If Not String.IsNullOrEmpty(GoldSrcModInput.Text) Then
                                    GoldSrcMod = " +app_set_config " & GOLDSRC_APP_ID & " mod " & GoldSrcModInput.Text
                                Else
                                    UpdateStatus("Half-Life mod not defined. Installing a default one.", True)
                                    GoldSrcMod = " +app_set_config " & GOLDSRC_APP_ID & " mod valve" ' Default to valve
                                End If
                            Else
                                GoldSrcMod = ""
                            End If
                            ServerPathInstallation = Chr(34) & ServerPath.Text & Chr(34)
                            UpdateStatus("Installing/Updating...")

                            If CheckBoxConsole.Checked = False Then
                                p = New Process
                                With (p.StartInfo)
                                    .FileName = SteamCMDExePath & "\steamcmd.exe"
                                    .UseShellExecute = False
                                    .Arguments = String.Format("SteamCmd +login {0} +force_install_dir {1}{2} +app_update {3}{4}", Login, ServerPathInstallation, GoldSrcMod, SteamAppID, ValidateApp)
                                End With
                                p.Start()
                            Else
                                ConsoleTab_Click()
                                TabMenu.SelectedTab = ConsoleTab

                                ' Clear console, Run subprocess and stream
                                ConsoleOutput.Clear()
                                ThrSteamCMD.Start()
                            End If
                        End If
                    End If
                End If
            End If
        Else
            UpdateStatus(CantFindSteamCMDString, True)
        End If
    End Sub

    Private ThrSteamCMD As Thread
    Private WithEvents p As Process

    Private Delegate Sub EnableConsoleInputDelegate()
    Private Sub EnableConsoleInput()
        If Me.InvokeRequired Then
            Me.Invoke(New EnableConsoleInputDelegate(AddressOf EnableConsoleInput))
        Else
            ConsoleInput.Enabled = True
            ConsoleButton.Enabled = True
        End If
    End Sub

    Private Sub ThreadTaskSteamCMD()
        ' Control.CheckForIllegalCrossThreadCalls = False ' This is unsafe, using Invoke instead
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
            EnableConsoleInput() ' Safely enable console controls
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
        Process.Start("explorer.exe", SrcdsExePath)
    End Sub

    Private Sub ModList_SelectedIndex() Handles ModList.SelectedIndexChanged, ModList.EnabledChanged
        Select Case ModList.Text
            Case "Alien Swarm"
                GameMod = "alienswarm"
            Case "Counter-Strike: Global Offensive"
                GameMod = "csgo"
            Case "Counter-Strike: Source"
                GameMod = "cstrike"
            Case "Day of Defeat: Source"
                GameMod = "dod"
            Case "Dota 2"
                GameMod = "dota"
            Case "Garry's Mod"
                GameMod = "garrysmod"
            Case "Half-Life 2: Deathmatch"
                GameMod = "hl2mp"
            Case "Left 4 Dead"
                GameMod = "left4dead"
            Case "Left 4 Dead 2"
                GameMod = "left4dead2"
            Case "Team Fortress 2"
                GameMod = "tf"
            Case Else
                GameMod = "" ' Handle case where no match is found
        End Select
        UpdateStatus("Game/Mod to run: " & ModList.Text & " - Game parameter: " & GameMod)
    End Sub

    Private Sub ModHelpButton_Click() Handles ModHelpButton.Click
        Process.Start("https://developer.valvesoftware.com/wiki/Game_Name_Abbreviations")
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
        CommandLineOptionsWindow.Show()
    End Sub

    Private Sub RunServerButton_Click() Handles RunServerButton.Click
        Dim srcdsFinalPath As String
        Dim baseServerPath As String = SrcdsExePathTextBox.Text

        ' NOTE: You need to add a CheckBox named Is64BitCheckBox to the form for this to work.
        If Is64BitCheckBox.Checked AndAlso GameMod = "tf" Then
            srcdsFinalPath = Path.Combine(baseServerPath, "bin", "win64", "srcds.exe")
        Else
            srcdsFinalPath = Path.Combine(baseServerPath, "srcds.exe")
        End If

        If My.Computer.FileSystem.FileExists(srcdsFinalPath) Then
            If String.IsNullOrWhiteSpace(GameMod) Then
                UpdateStatus("Please, select a game.", True)
            Else
                If String.IsNullOrWhiteSpace(ServerName) Then
                    UpdateStatus("Please, type a name for the server.", True)
                Else
                    If String.IsNullOrWhiteSpace(ServerMap) Then
                        UpdateStatus("Select the default map.", True)
                    Else
                        Parameters = DebugMode & SourceTV & ConsoleMode & InsecureMode & NoBots & DevMode
                        UpdateStatus("Running server...")

                        Dim p As New Process
                        With (p.StartInfo)
                            .FileName = srcdsFinalPath
                            .UseShellExecute = False
                            .CreateNoWindow = False
                            .Arguments = String.Format("{0}-game {1} -port {2} +hostname ""{3}"" +map {4} +maxplayers {5} +sv_lan {6} {7}",
                                                       Parameters, GameMod, UDPPort, ServerName, ServerMap, MaxPlayers, NetworkComboBox.SelectedIndex, AdditionalCommands)
                        End With

                        p.Start()
                    End If
                End If
            End If
        Else
            UpdateStatus("Can't find the file 'srcds.exe' at: " & srcdsFinalPath, True)
        End If
    End Sub

    ' Tools buttons
    Private Sub VDCButton_Click() Handles VDCButton.Click
        Process.Start("https://developer.valvesoftware.com/wiki/SteamCMD")
    End Sub

    Private Sub CheckUpdatesButton_Click() Handles CheckUpdatesButton.Click
        Process.Start("https://github.com/DioJoestar/SteamCMD-GUI#last-changes")
    End Sub

    Private Sub SMButton_Click() Handles SMButton.Click
        Process.Start("http://www.sourcemod.net")
    End Sub

    Private Sub MMButton_Click() Handles MMButton.Click
        Process.Start("http://www.sourcemm.net")
    End Sub

    Private Sub ESButton_Click() Handles ESButton.Click
        Process.Start("http://addons.eventscripts.com")
    End Sub

    'Private Sub MAPButton_Click() Handles MAPButton.Click
    '    Process.Start("http://mani-admin-plugin.com")
    'End Sub

    Private Sub AboutButton_Click() Handles AboutButton.Click, AboutToolStripMenuItem.Click
        AboutWindow.Show()
    End Sub

    Private Sub ExitButton_Click() Handles ExitButton.Click, ExitMenu.Click
        Close()
    End Sub

    Private Sub DonateButton_Click() Handles DonateButton.Click
        Process.Start("https://www.paypal.me/DioJoestar")
    End Sub

    'Menu buttons
    Private Sub SaveMenu_Click() Handles SaveMenu.Click, SaveButton.Click
        SaveFileDialog1.InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Settings")
        SaveFileDialog1.Filter = "Extensible Markup Language (*.xml)|*.xml"
        SaveFileDialog1.FileName = "Config.xml"

        If String.IsNullOrWhiteSpace(SrcdsExePath) Then
            UpdateStatus("Please, select where is located the file 'srcds.exe'.", True)
        Else
            If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
                Dim configFile = SaveFileDialog1.FileName
                Dim serverConfigElements As New List(Of XElement)
                serverConfigElements.Add(New XElement("HostName", ServerName))

                If ModList.Enabled = False Then
                    serverConfigElements.Add(New XElement("CustomMod", CustomModTextBox.Text))
                Else
                    serverConfigElements.Add(New XElement("Mod", ModList.Text))
                End If

                serverConfigElements.Add(New XElement("Map", ServerMap))
                serverConfigElements.Add(New XElement("Network", NetworkType))
                serverConfigElements.Add(New XElement("Players", MaxPlayers))
                serverConfigElements.Add(New XElement("RCON", RCON))
                serverConfigElements.Add(New XElement("Port", UDPPort))

                If Not String.IsNullOrWhiteSpace(AdditionalCommands) Then
                    serverConfigElements.Add(New XElement("AdditionalCommands", AdditionalCommands))
                End If

                Dim xDoc As New XDocument(
                    New XComment("Config used by SteamCMD GUI"),
                    New XElement("Config",
                        New XElement("Srcds-Config",
                            New XElement("Path", SrcdsExePath)
                        ),
                        New XElement("Server-Config", serverConfigElements)
                    )
                )
                xDoc.Save(configFile)
                UpdateStatus(Path.GetFileName(configFile) & " file saved.")
                My.Computer.Audio.PlaySystemSound(Media.SystemSounds.Exclamation)
            End If
        End If
    End Sub

    Private Sub LoadMenu_Click() Handles LoadMenu.Click
        XmlConfigOpenFileDialog.InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Settings")
        XmlConfigOpenFileDialog.FileName = "*.xml"
        XmlConfigOpenFileDialog.Filter = "Extensible Markup Language (*.xml)|*.xml"

        If XmlConfigOpenFileDialog.ShowDialog() = DialogResult.OK Then
            Try
                Dim xDoc = XDocument.Load(XmlConfigOpenFileDialog.FileName)
                Dim config = xDoc.Root

                ' Helper to get element value or nothing
                Func(Of String, String) getValue = Function(name) config.Descendants(name).FirstOrDefault()?.Value

                SrcdsExePath = getValue("Path")
                If Not String.IsNullOrEmpty(SrcdsExePath) Then
                    SrcdsExePathTextBox.Text = SrcdsExePath
                    MapList.Enabled = True
                    CFGMenu.Enabled = True
                    CommonFilesMenu.Enabled = True
                    SMMenu.Enabled = True
                    RunServerButton.Enabled = True
                    SrcdsExePathOpen.Enabled = True
                End If

                ServerNameTextBox.Text = getValue("HostName")

                Dim modValue = getValue("Mod")
                If Not String.IsNullOrEmpty(modValue) Then
                    ModList.Text = modValue
                    ModList_SelectedIndex()
                End If

                Dim customModValue = getValue("CustomMod")
                If Not String.IsNullOrEmpty(customModValue) Then
                    CustomModTextBox.Text = customModValue
                    CustomModCheckBox.Checked = True
                End If

                ServerMap = getValue("Map")
                MapList.Text = ServerMap
                If Not String.IsNullOrEmpty(ServerMap) Then MapList.Enabled = True

                Dim networkValue = getValue("Network")
                If Not String.IsNullOrEmpty(networkValue) Then NetworkComboBox.SelectedIndex = CInt(networkValue)

                MaxPlayers = getValue("Players")
                MaxPlayersTexBox.Text = MaxPlayers

                RCON = getValue("RCON")
                RconTextBox.Text = RCON
                CheckBoxMask.Checked = Not String.IsNullOrEmpty(RCON)

                UDPPort = getValue("Port")
                UDPPortTexBox.Text = UDPPort

                AdditionalCommands = getValue("AdditionalCommands")

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
        Process.Start(path)
    End Sub

    Private Sub NewFile_Click() Handles NewFileToolStripMenuItem.Click
        SaveFileDialog1.InitialDirectory = SrcdsExePathTextBox.Text & "\" & GameMod & "\cfg"
        SaveFileDialog1.Filter = "Configuration files (*.cfg)|*.cfg"
        SaveFileDialog1.FileName = "Config.cfg"
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            File.Create(SaveFileDialog1.FileName).Dispose()
            Process.Start(SaveFileDialog1.FileName)
            UpdateStatus("File " & SaveFileDialog1.FileName & " has been saved.")
        End If
    End Sub

    Private Sub MenuTxt_Click(ByVal sender As System.Object, ByVal e As EventArgs) Handles MotdTxtButton.Click, MapcycleTxtButton.Click, MaplistTxtButton.Click
        Dim TxtFile As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim MotdPath As String = SrcdsExePath & "\" & GameMod & "\" & TxtFile.Text & ".txt"
        If File.Exists(MotdPath) Then
            Process.Start(MotdPath)
        Else
            File.Create(MotdPath).Dispose()
            Process.Start(MotdPath)
            UpdateStatus(TxtFile.Text & " file not found. New one created.")
        End If
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
        Process.Start(path)
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
        Process.Start(path)
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
        Process.Start("explorer.exe", ".\Logs")
    End Sub

    Private Sub ConsoleSaveLog_Click() Handles ConsoleSaveLog.Click
        SaveFileDialog1.InitialDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs")
        SaveFileDialog1.DefaultExt = "*.txt"
        SaveFileDialog1.Filter = "Text Files (*.txt)|*.txt"
        SaveFileDialog1.FileName = "log.txt"

        If (SaveFileDialog1.ShowDialog() = DialogResult.OK) _
            AndAlso (SaveFileDialog1.FileName.Length > 0) Then
            File.WriteAllText(SaveFileDialog1.FileName, ConsoleOutput.Text)
            Process.Start(SaveFileDialog1.FileName)
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

    Private Sub LoadGamesList()
        Try
            Dim xDoc = XDocument.Load("Settings/SteamCMDGames.xml")
            GameDictionary = xDoc.Descendants("Game").ToDictionary(
                Function(g) g.Attribute("id").Value,
                Function(g) g.Value
            )
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
        Dim xDoc As New XDocument(
            New XComment("Custom Games Config used by SteamCMD GUI"),
            New XComment("This config is loaded automatically."),
            New XElement("SteamCMD-Games",
                From kvp In dict
                Select New XElement("Game", New XAttribute("id", kvp.Key), kvp.Value)
            )
        )
        xDoc.Save("Settings/SteamCMDGames.xml")
    End Sub

    Private Sub IPButton_Click() Handles IPButton.Click
        Clipboard.SetText(PublicIP, TextDataFormat.UnicodeText)
        UpdateStatus("Public IP copied")
    End Sub
End Class