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

        Me.Cursor = Cursors.WaitCursor

        ' set titles
        Me.lblTitle.Text = $"{Me.mThisGame.AwayTeam.FullName} @ {Me.mThisGame.HomeTeam.FullName}"
        Me.lblGamePk.Text = Me.mThisGame.GamePk

        ' init batting tables
        Me.lblAwayBatters.Text = $"{Me.mThisGame.AwayTeam.ShortName} Batters"
        Me.lblHomeBatters.Text = $"{Me.mThisGame.HomeTeam.ShortName} Batters"
        Me.dgvAwayBatting.DataSource = InitBattingStatsTable(mThisGame.AwayTeam.ShortName)
        Me.dgvHomeBatting.DataSource = InitBattingStatsTable(mThisGame.HomeTeam.ShortName)
        'Me.dgvAwayBatting.Columns("Id").Visible = False
        'Me.dgvHomeBatting.Columns("Id").Visible = False


        ' init pitching tables
        Me.lblAwayPitchers.Text = $"{Me.mThisGame.AwayTeam.ShortName} Pitchers"
        Me.lblHomePitchers.Text = $"{Me.mThisGame.HomeTeam.ShortName} Pitchers"
        Me.dgvAwayPitchers.DataSource = InitPitchingStatsTable(mThisGame.AwayTeam.ShortName)
        Me.dgvHomePitchers.DataSource = InitPitchingStatsTable(mThisGame.HomeTeam.ShortName)
        'Me.dgvAwayPitchers.Columns("Id").Visible = False
        'Me.dgvHomePitchers.Columns("Id").Visible = False

        ' load batting data into controls
        LoadBattingStats(Me.mThisGame.AwayTeam)
        LoadBattingAndFieldingInfo(Me.mThisGame.AwayTeam)
        LoadBattingStats(Me.mThisGame.HomeTeam)
        LoadBattingAndFieldingInfo(Me.mThisGame.HomeTeam)

        ' load pitching data into controls
        LoadPitchingStats(Me.mThisGame.AwayTeam)
        LoadPitchingStats(Me.mThisGame.HomeTeam)

        ' load game summary info
        LoadGameInfo()

        ' format data grids
        dgvAwayBatting.ColumnHeadersDefaultCellStyle.Font = New Font(dgvAwayBatting.DefaultFont, FontStyle.Bold)
        dgvHomeBatting.ColumnHeadersDefaultCellStyle.Font = New Font(dgvHomeBatting.DefaultFont, FontStyle.Bold)
        dgvAwayBatting.ClearSelection()
        dgvAwayPitchers.ColumnHeadersDefaultCellStyle.Font = New Font(dgvAwayPitchers.DefaultFont, FontStyle.Bold)
        dgvHomePitchers.ColumnHeadersDefaultCellStyle.Font = New Font(dgvHomePitchers.DefaultFont, FontStyle.Bold)
        dgvAwayPitchers.ClearSelection()

        Me.Cursor = Cursors.Default
    End Sub

    Private Sub LoadPitchingStats(team As MlbTeam)
        Dim AwayOrHome As String = ""
        Dim dt As DataTable
        Dim row As DataRow
        Dim thisTeam As MlbTeam

        ' setup away or home
        If Me.mThisGame.AwayTeam.Id = team.Id Then
            AwayOrHome = "away"
            dt = dgvAwayPitchers.DataSource
            thisTeam = Me.mThisGame.AwayTeam
        Else
            AwayOrHome = "home"
            dt = dgvHomePitchers.DataSource
            thisTeam = Me.mThisGame.HomeTeam
        End If

        ' TODO - could loop through obj team.roster looking for pitchers


        Dim pitcherIds As JArray = Me.mThisGame.BoxScoreData.SelectToken($"teams.{AwayOrHome}.pitchers")
        For i As Integer = 0 To pitcherIds.Count - 1
            Dim pId As String = pitcherIds(i)
            Dim PitcherStats As JObject = Me.mThisGame.BoxScoreData.SelectToken($"teams.{AwayOrHome}.players.ID{pId}.stats.pitching")
            row = dt.NewRow()

            ' get this player obj
            Dim thisPlayer As MlbPlayer = thisTeam.GetPlayer(pId)
            row.Item("Id") = pId
            row.Item(1) = thisPlayer.ShortName
            row.Item("IP") = PitcherStats.Item("inningsPitched")
            row.Item("H") = PitcherStats.Item("hits")
            row.Item("R") = PitcherStats.Item("runs")
            row.Item("ER") = PitcherStats.Item("earnedRuns")
            row.Item("B") = PitcherStats.Item("balls")
            row.Item("K") = PitcherStats.Item("strikes")
            row.Item("HR") = PitcherStats.Item("homeRuns")
            row.Item("ERA") = thisPlayer.ERA
            dt.Rows.Add(row)

        Next

    End Sub

    Private Sub LoadBattingStats(team As MlbTeam)

        Dim AwayOrHome As String = ""
        Dim dt As DataTable
        Dim row As DataRow
        Dim thisTeam As MlbTeam
        'Dim NoteLabelLetters As String() = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"}
        Dim NoteLabelLettersIndex As Integer = 1
        Dim NoteLabelNumbersIndex = 1

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

        ' TODO - loop thorugh obj team.lineup

        ' load sorted dic with batter ids in batting order order, including subs
        Dim dicBattingOrder = New SortedDictionary(Of String, String)
        Dim playerData As JObject = Me.mThisGame.BoxScoreData.SelectToken($"teams.{AwayOrHome}.players")
        For Each player In playerData
            Dim id As String = player.Key.Replace("ID", "")
            Dim BattingOrderId As String = player.Value.Item("battingOrder")
            If BattingOrderId IsNot Nothing And BattingOrderId <> "" Then
                dicBattingOrder.Add(BattingOrderId, id)
            End If
        Next
        For Each key In dicBattingOrder.Keys
            'Trace.WriteLine($"key={key}, value={dicBattingOrder(key)}")
        Next

        ' get batting stats for all playes in sorted dic
        For Each bId As String In dicBattingOrder.Keys
            Dim pId As String = dicBattingOrder(bId)
            Dim BattingStats As JObject = Me.mThisGame.BoxScoreData.SelectToken($"teams.{AwayOrHome}.players.ID{pId}.stats.batting")
            row = dt.NewRow()

            ' get this player obj
            Dim thisPlayer As MlbPlayer = thisTeam.GetPlayer(pId)
            row.Item("Id") = bId

            ' TODO - fix id

            ' determine if sub
            ' if the batting order number does not end in "0", then a sub
            ' if AB = 0, pinch runner and note is number, else pinch hitter and note is letter
            Dim PlayerName As String = $"{thisPlayer.ShortName} ({thisPlayer.ShortPosition})"
            If bId.Substring(bId.Length - 1, 1) <> "0" Then
                Dim noteId As String = ""
                If Integer.Parse(BattingStats.Item("atBats")) = 0 Then
                    row.Item(1) = $"   {NoteLabelNumbersIndex}-{PlayerName}"
                    NoteLabelNumbersIndex += 1
                Else
                    row.Item(1) = $"   {Chr(96 + NoteLabelLettersIndex)}-{PlayerName}"
                    NoteLabelLettersIndex += 1
                End If
            Else
                row.Item(1) = PlayerName
            End If

            row.Item("AB") = BattingStats.Item("atBats")
            row.Item("R") = BattingStats.Item("runs")
            row.Item("H") = BattingStats.Item("hits")
            row.Item("RBI") = BattingStats.Item("rbi")
            row.Item("BB") = BattingStats.Item("baseOnBalls")
            row.Item("K") = BattingStats.Item("strikeOuts")
            row.Item("LOB") = BattingStats.Item("leftOnBase")
            'row.Item("AVG") = Me.mThisGame.BoxScoreData.SelectToken($"teams.{AwayOrHome}.players.ID{pId}.seasonStats.batting.avg")
            'row.Item("OPS") = Me.mThisGame.BoxScoreData.SelectToken($"teams.{AwayOrHome}.players.ID{pId}.seasonStats.batting.ops")
            row.Item("AVG") = thisPlayer.Avg
            row.Item("OPS") = thisPlayer.OPS
            dt.Rows.Add(row)
        Next


        ' team totals
        row = dt.NewRow()
        Dim TeamBattingStats As JObject = Me.mThisGame.BoxScoreData.SelectToken($"teams.{AwayOrHome}.teamStats.batting")
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

    End Sub

    Private Sub LoadBattingAndFieldingInfo(team As MlbTeam)
        Dim AwayOrHome As String = ""
        Dim rtb As RichTextBox
        Dim thisTeam As MlbTeam
        Dim sb As New StringBuilder()

        ' setup away or home
        If Me.mThisGame.AwayTeam.Id = team.Id Then
            AwayOrHome = "away"
            rtb = Me.rtbAwayBattingFieldingDetails
            thisTeam = Me.mThisGame.AwayTeam
        Else
            AwayOrHome = "home"
            rtb = Me.rtbHomeBattingFieldingDetails
            thisTeam = Me.mThisGame.HomeTeam
        End If

        ' load batting notes
        Dim data As JArray = Me.mThisGame.BoxScoreData.SelectToken($"teams.{AwayOrHome}.note")
        For Each note As JObject In data
            Dim label As String = note.SelectToken("label")
            Dim value As String = note.SelectToken("value")
            sb.Append($"{label} - {value}")
            sb.Append(vbCr)
        Next

        ' loading fielding and baserunning info
        sb.Append(vbCr)
        data = Me.mThisGame.BoxScoreData.SelectToken($"teams.{AwayOrHome}.info")
        For Each info As JObject In data
            Dim title As String = info.SelectToken("title")
            sb.Append(title)
            sb.Append(vbCr)
            Dim fields As JArray = info.SelectToken("fieldList")
            For Each field As JObject In fields
                Dim label As String = field.SelectToken("label")
                Dim value As String = field.SelectToken("value")
                sb.Append($"{label} - {value}")
                sb.Append(vbCr)
            Next
        Next

        rtb.Text = sb.ToString()

    End Sub


    Private Function InitBattingStatsTable(team As String) As DataTable
        Dim dt As DataTable = New DataTable()
        Dim col As DataColumn = New DataColumn()
        col.ColumnName = "Id"
        dt.Columns.Add(col)
        col = New DataColumn()
        col.ColumnName = "Batters"
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
        col.ColumnName = "Pitchers"
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
        If TabControl1.SelectedIndex = 0 Then
            'Me.dgvAwayBatting.Columns("Id").Visible = False
            'Me.dgvAwayPitchers.Columns("Id").Visible = False
            Me.dgvAwayBatting.ClearSelection()
            Me.dgvAwayPitchers.ClearSelection()
        Else
            'Me.dgvHomeBatting.Columns("Id").Visible = False
            'Me.dgvHomePitchers.Columns("Id").Visible = False
            Me.dgvHomeBatting.ClearSelection()
            Me.dgvHomePitchers.ClearSelection()
        End If
    End Sub
End Class