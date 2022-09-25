Imports System.Numerics
Imports System.Security.Cryptography
Imports System.Text
Imports Newtonsoft.Json.Linq

Public Class MlbBoxscore
    Private mThisGame As MlbGame

    WriteOnly Property Game() As MlbGame
        Set(game As MlbGame)
            Me.mThisGame = game
        End Set
    End Property

    Private Sub MlbBoxscore_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If mThisGame Is Nothing Then
            Return
        End If

        Me.lblTitle.Text = $"{Me.mThisGame.AwayTeam.FullName} @ {Me.mThisGame.HomeTeam.FullName}"
        Me.lblGamePk.Text = Me.mThisGame.GamePk

        ' init batting tables
        Me.lblAwayBatters.Text = $"{Me.mThisGame.AwayTeam.ShortName} Batters"
        Me.lblHomeBatters.Text = $"{Me.mThisGame.HomeTeam.ShortName} Batters"
        Me.dgvAwayBatting.DataSource = InitBattingStatsTable(mThisGame.AwayTeam.ShortName)
        Me.dgvHomeBatting.DataSource = InitBattingStatsTable(mThisGame.HomeTeam.ShortName)
        Me.dgvAwayBatting.Columns("Id").Visible = False
        Me.dgvHomeBatting.Columns("Id").Visible = False

        ' init pitching tables
        Me.lblAwayPitchers.Text = $"{Me.mThisGame.AwayTeam.ShortName} Pitchers"
        Me.lblHomePitchers.Text = $"{Me.mThisGame.HomeTeam.ShortName} Pitchers"
        Me.dgvAwayPitchers.DataSource = InitPitchingStatsTable(mThisGame.AwayTeam.ShortName)
        Me.dgvHomePitchers.DataSource = InitPitchingStatsTable(mThisGame.HomeTeam.ShortName)
        Me.dgvAwayPitchers.Columns("Id").Visible = False
        Me.dgvHomePitchers.Columns("Id").Visible = False


        LoadBattingStats(Me.mThisGame.AwayTeam)
        LoadBattingStats(Me.mThisGame.HomeTeam)


        LoadGameInfo()


    End Sub

    Private Sub LoadBattingStats(team As MlbTeam)

        Dim AwayOrHome As String = ""
        Dim jsonPath As String = ""
        Dim dt As DataTable
        Dim row As DataRow
        Dim thisTeam As MlbTeam

        ' setup away or home
        If Me.mThisGame.AwayTeam.Id = team.Id Then
            AwayOrHome = "away"
            dt = dgvAwayBatting.DataSource
            thisTeam = Me.mThisGame.AwayTeam
        Else
            AwayOrHome = "home"
            dt = dgvHomeBatting.DataSource
            thisTeam = Me.mThisGame.HomeTeam
        End If

        Dim BatterIds As JArray = Me.mThisGame.BoxScoreData.SelectToken($"teams.{AwayOrHome}.batters")
        For Each BatterId As String In BatterIds
            row = dt.NewRow()

            ' get this player obj
            Dim thisPlayer As MlbPlayer = thisTeam.GetPlayer(BatterId)

            ' if not a pitcher
            If thisPlayer.ShortPosition <> "P" Then
                jsonPath = $"teams.{AwayOrHome}.players.ID{BatterId}.stats.batting"
                Dim BattingStats As JObject = Me.mThisGame.BoxScoreData.SelectToken(jsonPath)

                row.Item("Id") = Me.GetBattingOrderNumber(AwayOrHome, BatterId)
                row.Item(1) = $"{thisPlayer.ShortName} ({thisPlayer.ShortPosition})"
                row.Item("AB") = BattingStats.Item("atBats")
                row.Item("R") = BattingStats.Item("runs")
                row.Item("H") = BattingStats.Item("hits")
                row.Item("RBI") = BattingStats.Item("rbi")
                row.Item("BB") = BattingStats.Item("baseOnBalls")
                row.Item("K") = BattingStats.Item("strikeOuts")
                row.Item("LOB") = BattingStats.Item("leftOnBase")
                'row.Item("AVG") = Me.mThisGame.BoxScoreData.SelectToken($"teams.{AwayOrHome}.players.ID{BatterId}.seasonStats.batting.avg")
                'row.Item("OPS") = Me.mThisGame.BoxScoreData.SelectToken($"teams.{AwayOrHome}.players.ID{BatterId}.seasonStats.batting.ops")
                row.Item("AVG") = thisTeam.GetPlayer(BatterId).Avg
                row.Item("OPS") = thisTeam.GetPlayer(BatterId).OPS
                dt.Rows.Add(row)
            End If
        Next

        ' team totals
        row = dt.NewRow()
        jsonPath = $"teams.{AwayOrHome}.teamStats.batting"
        Dim TeamBattingStats As JObject = Me.mThisGame.BoxScoreData.SelectToken(jsonPath)

        row.Item(0) = ""
        row.Item(1) = "Team Totals"
        row.Item("AB") = TeamBattingStats.Item("atBats")
        row.Item("R") = TeamBattingStats.Item("runs")
        row.Item("H") = TeamBattingStats.Item("hits")
        row.Item("RBI") = TeamBattingStats.Item("rbi")
        row.Item("BB") = TeamBattingStats.Item("baseOnBalls")
        row.Item("K") = TeamBattingStats.Item("strikeOut")
        row.Item("LOB") = TeamBattingStats.Item("leftOnBase")
        row.Item("AVG") = TeamBattingStats.Item("avg")
        row.Item("OPS") = TeamBattingStats.Item("ops")
        dt.Rows.Add(row)

        ' subs
        Dim playerData As JObject = Me.mThisGame.BoxScoreData.SelectToken($"teams.{AwayOrHome}.players")
        For Each player In playerData
            Dim id As String = player.Key
            Dim BattingOrderId As String = GetBattingOrderNumber(AwayOrHome, id)

            If BattingOrderId <> "" Then
                Dim subNumber = BattingOrderId.Substring(BattingOrderId.Length - 1, 1)
                Dim BatterNumber = BattingOrderId.Substring(0, 1)
                If subNumber <> "0" Then

                    row = dt.NewRow()

                    ' find row with id of batting order number
                    ' insert a row after with sub info
                    'dt.Rows(Convert.ToInt32(BatterNumber) - 1).Item(0) = $"{dt.Rows(Convert.ToInt32(BatterNumber) - 1).Item(0)}{vbCr}   {subNumber} - {player.Value.Item("person").Item("fullName")}"



                End If
            End If

        Next



    End Sub

    Private Function GetBattingOrderNumber(AwayOrHome As String, batterId As String) As String
        Dim playerData As JObject = Me.mThisGame.BoxScoreData.SelectToken($"teams.{AwayOrHome}.players")
        Dim BattingOrderId As String = ""
        For Each player In playerData
            If player.Key = batterId Then
                BattingOrderId = player.Value.Item("battingOrder")
            End If
        Next
        Return BattingOrderId
    End Function

    Private Function FindRowIndex(dt As DataTable, id As String) As Integer
        For Each row As DataRow In dt.Rows
            If row.Item(0) = id Then

            End If
        Next

    End Function
    Private Function InitBattingStatsTable(team As String) As DataTable
        Dim dt As DataTable = New DataTable()
        Dim col As DataColumn = New DataColumn()
        col.ColumnName = "Id"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = $"{team} Batters"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "AB"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "R"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "H"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "RBI"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "BB"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "K"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "LOB"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "AVG"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "OPS"
        dt.Columns.Add(col)

        Return dt
    End Function

    Private Function InitPitchingStatsTable(team As String) As DataTable
        Dim dt As DataTable = New DataTable()
        Dim col As DataColumn = New DataColumn()
        col.ColumnName = "Id"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = $"{team} Pitchers"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "IP"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "H"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "R"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "ER"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "B"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "K"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "HR"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "ERA"
        dt.Columns.Add(col)

        Return dt

    End Function

    Private Sub LoadGameInfo()
        Dim sb As StringBuilder = New StringBuilder()
        Dim BoxData As JObject = mThisGame.BoxScoreData()

        ' pitching notes
        Dim PitchingNotes As JArray = BoxData.SelectToken("pitchingNotes")
        For i As Integer = 0 To PitchingNotes.Count - 1
            sb.Append($"{PitchingNotes.Item(i)}")
            sb.Append(vbCr)
        Next

        ' info
        Dim GameInfo As JArray = BoxData.SelectToken("info")
        For i As Integer = 0 To GameInfo.Count - 1
            Dim info As JObject = GameInfo.Item(i)
            sb.Append($"{info.SelectToken("label")}: {info.SelectToken("value")}")
            sb.Append(vbCr)
        Next

        rtbGameInfo.Text = sb.ToString()
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged

    End Sub
End Class