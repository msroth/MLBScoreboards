Imports Newtonsoft.Json.Linq
Imports NLog

Public Class MlbPlaySummary

    Private mCurrentGame As MlbGame

    Shared ReadOnly Logger As Logger = LogManager.GetCurrentClassLogger()

    WriteOnly Property Game() As MlbGame
        Set(game As MlbGame)
            Me.mCurrentGame = game
        End Set
    End Property

    Private Sub PlaySummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Logger.Debug($"Loaded {Me.Name}")

        ' set game title
        SetGameTitle()

        ' display play summary
        dgvPlays.DataSource = Me.mCurrentGame.GetPlaySummary()
        dgvPlays.ColumnHeadersDefaultCellStyle.Font = New Font(dgvPlays.DefaultFont, FontStyle.Bold)
        dgvPlays.ClearSelection()

    End Sub

    Private Sub SetGameTitle()
        Dim title As String = $"{Me.mCurrentGame.AwayTeam.FullName} @ {Me.mCurrentGame.HomeTeam.FullName} (Game: {Me.mCurrentGame.GamePk})"
        Me.lblGameTitle.Text = title
    End Sub

    Private Sub dgvPlays_Paint(sender As Object, e As PaintEventArgs) Handles dgvPlays.Paint

        ' paint bottom half innings blue
        Try
            For Each row As DataGridViewRow In dgvPlays.Rows
                If row.Cells("Half").Value.ToString.ToUpper() = "BOTTOM" Then
                    row.DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    row.DefaultCellStyle.BackColor = Color.White
                End If
            Next
        Catch ex As Exception
            Trace.WriteLine($"ERROR: Paint - {ex}")
        End Try
    End Sub

End Class

'<SDG><
