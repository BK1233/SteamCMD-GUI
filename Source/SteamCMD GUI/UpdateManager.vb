Imports System.Diagnostics
Imports System.IO

Public Class UpdateManager
    Public Event UpdateCheckCompleted(updateAvailable As Boolean, appId As String)
    Public Event UpdateCompleted(appId As String)
    Public Event ErrorOccurred(message As String)

    Private ReadOnly _steamCmdPath As String

    Public Sub New(steamCmdPath As String)
        _steamCmdPath = steamCmdPath
    End Sub

    Public Sub CheckForUpdate(appId As String, installDir As String)
        ' For simplicity, we'll just run the update command with -validate
        ' and check the output. A more robust solution would be needed for production.
        Dim arguments As String = $"+login anonymous +force_install_dir ""{installDir}"" +app_update {appId} validate +quit"
        RunSteamCmd(arguments, Sub(output)
                                   Dim updateAvailable As Boolean = output.Contains("Success!") ' Simplified check
                                   RaiseEvent UpdateCheckCompleted(updateAvailable, appId)
                               End Sub)
    End Sub

    Public Sub UpdateServer(appId As String, installDir As String)
        Dim arguments As String = $"+login anonymous +force_install_dir ""{installDir}"" +app_update {appId} +quit"
        RunSteamCmd(arguments, Sub(output) RaiseEvent UpdateCompleted(appId))
    End Sub

    Private Sub RunSteamCmd(arguments As String, outputHandler As Action(Of String))
        Try
            Dim process As New Process()
            With process.StartInfo
                .FileName = _steamCmdPath
                .Arguments = arguments
                .UseShellExecute = False
                .RedirectStandardOutput = True
                .CreateNoWindow = True
            End With

            Dim outputBuilder As New System.Text.StringBuilder()
            AddHandler process.OutputDataReceived, Sub(sender, e)
                                                       If e.Data IsNot Nothing Then
                                                           outputBuilder.AppendLine(e.Data)
                                                       End If
                                                   End Sub

            process.Start()
            process.BeginOutputReadLine()
            process.WaitForExit()

            outputHandler(outputBuilder.ToString())

        Catch ex As Exception
            RaiseEvent ErrorOccurred("SteamCMD execution failed: " & ex.Message)
        End Try
    End Sub
End Class
