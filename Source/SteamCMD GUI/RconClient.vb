Imports CoreRCON
Imports System.Net.Sockets

Public Class RconClient
    Private client As RCON
    Public Event OutputReceived(text As String)
    Public Event ConnectionStatusChanged(connected As Boolean)

    Public Sub Connect(host As String, port As Integer, password As String)
        Try
            client = New RCON(host, port, password)
            RaiseEvent ConnectionStatusChanged(True)
        Catch ex As Exception
            RaiseEvent OutputReceived("RCON connection failed: " & ex.Message)
            RaiseEvent ConnectionStatusChanged(False)
        End Try
    End Sub

    Public Sub Disconnect()
        If client IsNot Nothing Then
            client.Dispose()
            client = Nothing
            RaiseEvent ConnectionStatusChanged(False)
        End If
    End Sub

    Public Async Sub SendCommand(command As String)
        If client IsNot Nothing Then
            Try
                Dim response = Await client.SendCommandAsync(command)
                RaiseEvent OutputReceived(response)
            Catch ex As Exception
                RaiseEvent OutputReceived("RCON command failed: " & ex.Message)
            End Try
        Else
            RaiseEvent OutputReceived("RCON not connected.")
        End If
    End Sub

    Public ReadOnly Property IsConnected As Boolean
        Get
            Return client IsNot Nothing
        End Get
    End Property
End Class
