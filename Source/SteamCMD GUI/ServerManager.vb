Imports System.Diagnostics

Public Class ServerManager
    Public Event ServerOutputReceived(data As String)
    Public Event ServerExited()

    Private WithEvents ServerProcess As Process

    Public Sub StartServer(path As String, arguments As String)
        If ServerProcess IsNot Nothing AndAlso Not ServerProcess.HasExited Then
            ' Server is already running
            Return
        End If

        ServerProcess = New Process()
        With ServerProcess.StartInfo
            .FileName = path
            .Arguments = arguments
            .UseShellExecute = False
            .RedirectStandardOutput = True
            .RedirectStandardError = True
            .CreateNoWindow = True
        End With

        ServerProcess.EnableRaisingEvents = True
        ServerProcess.Start()
        ServerProcess.BeginOutputReadLine()
        ServerProcess.BeginErrorReadLine()
    End Sub

    Public Sub StopServer()
        If ServerProcess IsNot Nothing AndAlso Not ServerProcess.HasExited Then
            ServerProcess.Kill()
        End If
    End Sub

    Private Sub ServerProcess_OutputDataReceived(sender As Object, e As DataReceivedEventArgs) Handles ServerProcess.OutputDataReceived
        If e.Data IsNot Nothing Then
            RaiseEvent ServerOutputReceived(e.Data)
        End If
    End Sub

    Private Sub ServerProcess_ErrorDataReceived(sender As Object, e As DataReceivedEventArgs) Handles ServerProcess.ErrorDataReceived
        If e.Data IsNot Nothing Then
            RaiseEvent ServerOutputReceived($"ERROR: {e.Data}")
        End If
    End Sub

    Private Sub ServerProcess_Exited(sender As Object, e As EventArgs) Handles ServerProcess.Exited
        RaiseEvent ServerExited()
        ServerProcess = Nothing
    End Sub

End Class
