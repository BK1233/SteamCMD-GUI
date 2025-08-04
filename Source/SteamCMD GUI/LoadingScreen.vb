Public NotInheritable Class LoadingScreen
    Private Sub LoadingScreen_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' Set the info label text using string interpolation and assembly information.
        InfoLabelSplash.Text = $"{My.Application.Info.CompanyName}{Environment.NewLine}Version: {My.Application.Info.Version}{Environment.NewLine}CC BY-NC-SA 4.0"

        ' Make the label background transparent.
        TransparencyKey = BackColor
        InfoLabelSplash.Parent = BannerSplash
        InfoLabelSplash.Location = New Point(InfoLabelSplash.Location.X - BannerSplash.Left, InfoLabelSplash.Location.Y - BannerSplash.Top)
    End Sub
End Class
