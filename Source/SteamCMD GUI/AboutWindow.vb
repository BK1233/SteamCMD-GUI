Imports System.IO
Imports System.ComponentModel

Public Class AboutWindow
    Private Sub AboutWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set the version label using string interpolation.
        VersionLabel.Text = $"Version {My.Application.Info.Version}"
        ' Set the author label from assembly information.
        AuthorLabel.Text = My.Application.Info.CompanyName
        ' Set the window icon.
        Icon = My.Resources.SteamCMDGUI_Icon
    End Sub

    ''' <summary>
    ''' Handles the Click event of the AuthorLabel.
    ''' </summary>
    Private Sub AuthorLabel_Click(sender As Object, e As EventArgs) Handles AuthorLabel.Click
        Try
            Process.Start("http://steamcommunity.com/profiles/76561198000420180")
        Catch ex As Win32Exception
            MessageBox.Show("Could not open the link. Please check if you have a default web browser.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Handles the Click event of the LicenseURL.
    ''' </summary>
    Private Sub LicenseURL_Click(sender As Object, e As EventArgs) Handles LicenseURL.Click
        Try
            Dim licensePath = Path.Combine(My.Application.Info.DirectoryPath, "LICENSE")
            If File.Exists(licensePath) Then
                Process.Start(licensePath)
            Else
                Process.Start("https://raw.githubusercontent.com/BK1233/SteamCMD-GUI/master/LICENSE")
            End If
        Catch ex As Win32Exception
            MessageBox.Show("Could not open the link. Please check if you have a default web browser.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Handles the Click event of the ChangelogButton.
    ''' </summary>
    Private Sub ChangelogButton_Click(sender As Object, e As EventArgs) Handles ChangelogButton.Click
        Try
            Dim changelogPath = Path.Combine(My.Application.Info.DirectoryPath, "Documentation", "Changelog.md")
            If File.Exists(changelogPath) Then
                Process.Start(changelogPath)
            Else
                Process.Start("https://raw.githubusercontent.com/BK1233/SteamCMD-GUI/master/Documentation/Changelog.md")
            End If
        Catch ex As Win32Exception
            MessageBox.Show("Could not open the link. Please check if you have a default web browser.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Handles the Click event of the Close button.
    ''' </summary>
    Private Sub Close2_Click(sender As Object, e As EventArgs) Handles Close2.Click
        Close()
    End Sub
End Class