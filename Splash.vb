Public Class Splash

    Shared ReadOnly Logger As NLog.Logger = NLog.LogManager.GetCurrentClassLogger()

    Private Sub Splash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Logger.Debug($"Loaded {Me.Name}")
    End Sub
End Class