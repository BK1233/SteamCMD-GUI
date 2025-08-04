Imports System.IO
Imports System.Diagnostics

Public Class WorkshopManager
    Public Event WorkshopSearchCompleted(results As List(Of String))
    Public Event WorkshopInstallCompleted(modId As String)
    Public Event ErrorOccurred(message As String)

    Private ReadOnly _steamCmdPath As String

    Public Sub New(steamCmdPath As String)
        _steamCmdPath = steamCmdPath
    End Sub

    Public Sub Search(appId As String, query As String)
        Dim arguments As String = $"+login anonymous +workshop_search ""{query}"" +quit"
        RunSteamCmd(arguments, AddressOf HandleSearchOutput)
    End Sub

    Public Sub Install(appId As String, modId As String)
        Dim arguments As String = $"+login anonymous +workshop_download_item {appId} {modId} +quit"
        RunSteamCmd(arguments, Sub(output) RaiseEvent WorkshopInstallCompleted(modId))
    End Sub

    Private Sub RunSteamCmd(arguments As String, outputHandler As Action(Of String))
        Try
            Dim process As New Process()
            process.StartInfo.FileName = _steamCmdPath
            process.StartInfo.Arguments = arguments
            process.StartInfo.UseShellExecute = False
            process.StartInfo.RedirectStandardOutput = True
            process.StartInfo.CreateNoWindow = True

            Dim output As String = ""
            process.OutputDataReceived.AddHandler(Sub(sender, e)
                                                      If e.Data IsNot Nothing Then
                                                          output &= e.Data & vbCrLf
                                                      End If
                                                  End Sub)

            process.Start()
            process.BeginOutputReadLine()
            process.WaitForExit()

            outputHandler(output)

        Catch ex As Exception
            RaiseEvent ErrorOccurred("SteamCMD execution failed: " & ex.Message)
        End Try
    End Sub

    Private Sub HandleSearchOutput(output As String)
        Dim results As New List(Of String)()
        ' Basic parsing, this would need to be improved for real-world use
        Using reader As New StringReader(output)
            Dim line As String = reader.ReadLine()
            While line IsNot Nothing
                If line.Contains("workshop_item") Then
                    results.Add(line)
                End If
                line = reader.ReadLine()
            End While
        End Using
        RaiseEvent WorkshopSearchCompleted(results)
    End Sub
End Class
