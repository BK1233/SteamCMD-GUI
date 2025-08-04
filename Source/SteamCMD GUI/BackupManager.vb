Imports System.IO
Imports System.IO.Compression

Public Class BackupManager
    Public Event BackupCreated(path As String)
    Public Event BackupRestored(path As String)
    Public Event BackupDeleted(path As String)
    Public Event ErrorOccurred(message As String)

    Public Sub CreateBackup(sourcePath As String, destinationPath As String)
        Try
            Dim backupFileName As String = $"backup_{DateTime.Now:yyyyMMddHHmmss}.zip"
            Dim backupFilePath As String = Path.Combine(destinationPath, backupFileName)
            ZipFile.CreateFromDirectory(sourcePath, backupFilePath)
            RaiseEvent BackupCreated(backupFilePath)
        Catch ex As Exception
            RaiseEvent ErrorOccurred("Backup creation failed: " & ex.Message)
        End Try
    End Sub

    Public Sub RestoreBackup(backupFilePath As String, restorePath As String)
        Try
            ZipFile.ExtractToDirectory(backupFilePath, restorePath)
            RaiseEvent BackupRestored(restorePath)
        Catch ex As Exception
            RaiseEvent ErrorOccurred("Backup restore failed: " & ex.Message)
        End Try
    End Sub

    Public Sub DeleteBackup(backupFilePath As String)
        Try
            File.Delete(backupFilePath)
            RaiseEvent BackupDeleted(backupFilePath)
        Catch ex As Exception
            RaiseEvent ErrorOccurred("Backup deletion failed: " & ex.Message)
        End Try
    End Sub

    Public Function ListBackups(destinationPath As String) As List(Of String)
        Dim backups As New List(Of String)()
        If Directory.Exists(destinationPath) Then
            For Each filePath As String In Directory.GetFiles(destinationPath, "backup_*.zip")
                backups.Add(Path.GetFileName(filePath))
            Next
        End If
        Return backups
    End Function
End Class
