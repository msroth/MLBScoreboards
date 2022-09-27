Public Class Loading


    Public Sub SetLabel(Label As String)
        Me.lblStatus.Text = Label
        Me.Refresh()
    End Sub

    Public Sub SetProgressMax(max As Integer)
        Me.ProgressBar1.Maximum = max
    End Sub

    Public Sub SetValue(value As Integer)
        Me.ProgressBar1.Value = value
        Me.ProgressBar1.Refresh()
    End Sub

    Private Sub Loading_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.lblStatus.Text = ""
        Me.ProgressBar1.Step = 1
    End Sub

    Public Sub DoStep()
        Me.ProgressBar1.PerformStep()
    End Sub
End Class