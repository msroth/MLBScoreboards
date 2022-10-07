﻿Imports Newtonsoft.Json.Linq

Public Class MlbPlaySummary

    Private mCurrentGame As MlbGame

    WriteOnly Property Game() As MlbGame
        Set(game As MlbGame)
            Me.mCurrentGame = game
        End Set
    End Property

    Private Sub PlaySummary_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' set game title
        SetGameTitle()

        Dim dt As DataTable = InitPlaysTable()

        For Each play As JObject In Me.mCurrentGame.AllPlaysData.SelectToken("allPlays")
            Dim Inning As String = play.SelectToken("about.inning")
            Dim Half As String = play.SelectToken("about.halfInning")
            Dim Out As String = play.SelectToken("count.outs")
            Dim Commentary As String = play.SelectToken("result.description")
            Dim Index As String = play.SelectToken("about.atBatIndex")
            Dim Scorebook As String = Me.mCurrentGame.CreateScorebookEntry(Convert.ToInt32(Index))

            Dim row As DataRow = dt.NewRow()
            row.Item("Index") = Index
            row.Item("Inning") = Inning
            row.Item("Half") = Half.ToUpper()
            row.Item("Out") = Out
            row.Item("Scorebook") = Scorebook
            row.Item("Commentary") = Commentary

            dt.Rows.Add(row)
        Next

        dgvPlays.DataSource = dt
        dgvPlays.ColumnHeadersDefaultCellStyle.Font = New Font(dgvPlays.DefaultFont, FontStyle.Bold)
        dgvPlays.ClearSelection()

    End Sub

    Private Function InitPlaysTable() As DataTable

        Dim dt As New DataTable()
        Dim col As New DataColumn()
        col.ColumnName = "Index"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "Inning"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "Half"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "Out"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "Scorebook"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "Commentary"
        dt.Columns.Add(col)

        Return dt
    End Function

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