Imports System.ComponentModel

Public Class CommandLineOptionsWindow

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Load additional commands into the textbox.
        CommandsTextbox.Text = AdditionalCommands
    End Sub

    ''' <summary>
    ''' Handles the Click event of the OKButton.
    ''' </summary>
    Private Sub OKButton_Click(sender As Object, e As EventArgs) Handles OKButton.Click
        AdditionalCommands = CommandsTextbox.Text
        DialogResult = DialogResult.OK
        Close()
    End Sub

    ''' <summary>
    ''' Handles the Click event of the CancelButton.
    ''' </summary>
    Private Sub CancelButton_Click(sender As Object, e As EventArgs) Handles CancelButton.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    ''' <summary>
    ''' Handles the Click event of the CommandHelpButton.
    ''' </summary>
    Private Sub CommandHelpButton_Click(sender As Object, e As EventArgs) Handles CommandHelpButton.Click
        Try
            Process.Start("https://developer.valvesoftware.com/wiki/Command_Line_Options")
        Catch ex As Win32Exception
            MessageBox.Show("Could not open the link. Please check if you have a default web browser.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class