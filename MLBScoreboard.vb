
Imports Newtonsoft.Json.Linq

Public Class MLBScoreboard

    Dim SBData As ScoreboardData = New ScoreboardData()
    Dim gamePk As Integer = 0
    Dim dtInnings As DataTable = New DataTable("Innings")
    Dim dtRHE As DataTable = New DataTable("RHE")
    Dim dtAwayRoster As DataTable
    Dim dtHomeRoster As DataTable


    Private Sub QuitToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles QuitToolStripMenuItem1.Click
        Application.Exit()
    End Sub

    Private Sub AboutToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem1.Click
        Dim frmAbout = AboutBox1
        frmAbout.ShowDialog()
    End Sub

    Private Sub setupRHEDataTable()
        'Me.dtRHE = New DataTable("RHE")

        Dim columnR As DataColumn = New DataColumn()
        columnR.DataType = System.Type.GetType("System.String")
        columnR.ColumnName = "R"
        Me.dtRHE.Columns.Add(columnR)

        Dim columnH As DataColumn = New DataColumn()
        columnH.DataType = System.Type.GetType("System.String")
        columnH.ColumnName = "H"
        Me.dtRHE.Columns.Add(columnH)

        Dim columnE As DataColumn = New DataColumn()
        columnE.DataType = System.Type.GetType("System.String")
        columnE.ColumnName = "E"
        Me.dtRHE.Columns.Add(columnE)

        Me.dtRHE.Rows.Add()
        Me.dtRHE.Rows.Add()

    End Sub

    Private Sub setupInningsDataTable()
        ' setup Innings datatable

        Dim column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = ""
        Me.dtInnings.Columns.Add(column)

        ' add first 9 innings
        For i = 1 To 9
            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = i
            Me.dtInnings.Columns.Add(column)
        Next

        ' add RHE columns
        column = New DataColumn
        column.ColumnName = "R"
        dtInnings.Columns.Add(column)
        column = New DataColumn
        column.ColumnName = "H"
        dtInnings.Columns.Add(column)
        column = New DataColumn
        column.ColumnName = "E"
        dtInnings.Columns.Add(column)

        ' add rows for Away and Home teams
        Me.dtInnings.Rows.Add()
        Me.dtInnings.Rows.Add()

    End Sub
    Private Sub MLBScoreboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.setupRHEDataTable()
        Me.setupInningsDataTable()

        ' set tables as datasource for grid views
        Me.dgvInnings.DataSource = Me.dtInnings
        Me.dgvAwayRoster.DataSource = Me.dtAwayRoster
        Me.dgvHomeRoster.DataSource = Me.dtHomeRoster

        Me.dgvInnings.ClearSelection()
        Me.dgvAwayRoster.ClearSelection()
        Me.dgvHomeRoster.ClearSelection()

        ' load teams into database
        SBData.loadTeamsDataIntoDB()

        ' load teams from database into CBX control
        Dim dt As DataTable = SBData.returnAllTeamNames()
        With Me.cbxTeam
            .DisplayMember = "full_name"
            .ValueMember = "id"
            .DataSource = dt
        End With
        Me.cbxTeam.SelectedIndex = 0

        ' stop the timer refreshing the current scoreboard
        Timer1.Stop()

    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Dim teamId As String = Me.cbxTeam.SelectedValue.ToString
        Dim teamName As String = Me.cbxTeam.Items(Me.cbxTeam.SelectedIndex)(3).ToString
        Dim gameDate As String = Me.calDatePicker.Value.ToString("MM/dd/yyyy")

        ' stop the timer refreshing the current scoreboard
        Timer1.Stop()

        ' get game data
        Me.gamePk = Me.SBData.findGamePk(teamId, gameDate)

        If Me.gamePk = 0 Then
            MsgBox(String.Format("{0} has no scheduled games {1}.", teamName, gameDate))
        Else
            ' run the scoreboard
            Me.runScoreboard()
            Timer1.Interval = 15000  ' 15 sec
            Timer1.Start()
        End If
    End Sub

    'Private Sub setGameStatus()
    '    Dim gameStatus As String = Me.SBData.getLiveData().SelectToken("gameData.status.detailedState")
    '    Me.lblStatus.Text = gameStatus
    'End Sub

    Private Sub setGameTitle()
        Dim result As DataTable = Me.SBData.getCurrentGameData()
        Dim gameStatus As String = Me.SBData.getLiveData().SelectToken("gameData.status.detailedState")
        Me.lblGameTitle.Text = String.Format("{0} @ {1} - {2} {3} (Game: {4}) - {5}", result.Rows(0).Field(Of String)(4),
                                             result.Rows(0).Field(Of String)(3), result.Rows(0).Field(Of String)(5),
                                             result.Rows(0).Field(Of String)(6), Me.gamePk, gameStatus.ToUpper)
    End Sub

    Private Sub updateLastPlayCommentary()
        Dim play = Me.SBData.getLastPlayDescription()
        Me.txtCommentary.Text = play
    End Sub

    Private Sub runScoreboard()

        ' get game date
        Dim gameDate As String = Me.calDatePicker.Value.ToString("MM/dd/yyyy")

        ' update status bar
        Me.ToolStripStatusLabel1.Text = "Updating data ... "

        ' get live data
        Me.SBData.refreshLiveData(gamePk)

        ' get game time
        Dim gameTime As String = Me.SBData.getLiveData().SelectToken("gameData.datetime.time").ToString + Me.SBData.getLiveData().SelectToken("gameData.datetime.ampm").ToString

        ' load current game data into database
        Me.SBData.loadCurrentGameDataIntoDB(Me.SBData.getLiveData(), Me.SBData.getBoxScoreData(), gameDate, gameTime, Me.gamePk)

        ' update status bar
        Me.ToolStripStatusLabel1.Text = "Updated " + Date.Now

        ' update game label
        Me.setGameTitle()

        ' update game status
        ' Me.setGameStatus()

        'update innings datatable
        Me.updateInnings()

        ' update runs, hits, errors datatable
        Me.updateRHE()

        ' clear inning label
        dtInnings.Columns(0).ColumnName = " "

        ' load team rosters
        Me.loadTeamRosters()

        If Me.SBData.getGameStatus().ToUpper() = "IN PROGRESS" Then

            ' update inning
            Dim inningLabel As String = SBData.getCurrentInningHalf() + " " + SBData.getCurrentInningNumber()
            dtInnings.Columns(0).ColumnName = inningLabel

            ' update Balls, Strikes, Outs
            Me.updateBSO()

            ' update pitcher-batter matchup
            Me.updatePitcherBatterMatchup()

            ' update last play
            Me.updateLastPlayCommentary()

            ' TODO update last pitch data
            Me.updateLastPitchDescription()

            'update base runners image
            Me.updateBaseRunners()

            ' update team rosters
            Me.updateTeamRosters()

        Else
            txtCommentary.Enabled = False
            dgvAwayRoster.Enabled = False
            dgvHomeRoster.Enabled = False
        End If

    End Sub

    Private Sub updateRHE()

        ' clear all data
        'For Each row As DataRow In Me.dtRHE.Rows
        '    For Each col As DataColumn In Me.dtRHE.Columns
        '        row(col) = ""
        '    Next
        'Next

        Dim lineScore As JObject = Me.SBData.getLineScoreData()

        ' Get R - H - E for away team
        Me.dtRHE.Rows(0).Item("R") = lineScore.SelectToken("teams.away.runs")
        Me.dtRHE.Rows(0).Item("H") = lineScore.SelectToken("teams.away.hits")
        Me.dtRHE.Rows(0).Item("E") = lineScore.SelectToken("teams.away.errors")

        ' Get R - H - E for home team
        Me.dtRHE.Rows(1).Item("R") = lineScore.SelectToken("teams.home.runs")
        Me.dtRHE.Rows(1).Item("H") = lineScore.SelectToken("teams.home.hits")
        Me.dtRHE.Rows(1).Item("E") = lineScore.SelectToken("teams.home.errors")

    End Sub

    Private Sub updateBSO()
        Dim playData = Me.SBData.getCurrentPlayData()
        Me.lblBalls.Text = String.Format("Balls: {0}", playData.SelectToken("count.balls"))
        Me.lblStrikes.Text = String.Format("Strikes: {0}", playData.SelectToken("count.strikes"))
        Me.lblOuts.Text = String.Format("Outs: {0}", playData.SelectToken("count.outs"))

    End Sub
    Private Sub updateInnings()

        ' clear all data (might be from a different game if new team selected)
        For Each row As DataRow In Me.dtInnings.Rows
            For Each col As DataColumn In Me.dtInnings.Columns
                row(col) = ""
            Next
        Next

        If dtInnings.Columns.Contains("R") Then
            dtInnings.Columns.Remove("R")
        End If

        If dtInnings.Columns.Contains("H") Then
            dtInnings.Columns.Remove("H")
        End If

        If dtInnings.Columns.Contains("E") Then
            dtInnings.Columns.Remove("E")
        End If

        ' add team names with records
        Dim awayWins As String = Me.SBData.getLiveData().SelectToken("gameData.teams.away.record.wins")
        Dim awayLoses As String = Me.SBData.getLiveData().SelectToken("gameData.teams.away.record.losses")
        Dim awayAbbrev As String = Me.SBData.getLiveData().SelectToken("gameData.teams.away.abbreviation")
        Dim awayNameAndRecord As String = String.Format("{0} ({1}-{2})", awayAbbrev, awayWins, awayLoses)
        Me.dtInnings.Rows(0).Item(0) = awayNameAndRecord

        Dim homeWins As String = Me.SBData.getLiveData().SelectToken("gameData.teams.home.record.wins")
        Dim homeLoses As String = Me.SBData.getLiveData().SelectToken("gameData.teams.home.record.losses")
        Dim homeAbbrev As String = Me.SBData.getLiveData().SelectToken("gameData.teams.home.abbreviation")
        Dim homeNameAndRecord As String = String.Format("{0} ({1}-{2})", homeAbbrev, homeWins, homeLoses)
        Me.dtInnings.Rows(1).Item(0) = homeNameAndRecord

        ' fill innings
        Dim lineScore As JObject = Me.SBData.getLineScoreData
        Dim innings As JArray = lineScore.SelectToken("innings")

        'if innings > 9, add columns to datatable before proceeding
        ' Test:  Nats 7/4/22
        If (innings.Count > dtInnings.Columns.Count - 1) Then    ' minus one for team name column
            Dim i As Integer = 1
            While innings.Count >= dtInnings.Columns.Count
                Dim dc = New DataColumn
                dc.ColumnName = (9 + i).ToString
                dtInnings.Columns.Add(dc)
                i = i + 1
            End While
        End If

        ' fill inning data
        For Each inning As JObject In innings

            ' away data
            Dim runs As String = inning.SelectToken("away.runs")
            If runs Is Nothing Then
                runs = " "
            End If
            Me.dtInnings.Rows(0).Item(Integer.Parse(inning.SelectToken("num"))) = runs

            ' Get R - H - E for away team
            'Me.dtInnings.Rows(0).Item("R") = lineScore.SelectToken("teams.away.runs")
            'Me.dtInnings.Rows(0).Item("H") = lineScore.SelectToken("teams.away.hits")
            'Me.dtInnings.Rows(0).Item("E") = lineScore.SelectToken("teams.away.errors")

            ' home data
            runs = inning.SelectToken("home.runs")
            If runs Is Nothing Then
                runs = " "
            End If
            Me.dtInnings.Rows(1).Item(Integer.Parse(inning.SelectToken("num"))) = runs

            ' Get R - H - E for home team
            'Me.dtInnings.Rows(1).Item("R") = lineScore.SelectToken("teams.home.runs")
            'Me.dtInnings.Rows(1).Item("H") = lineScore.SelectToken("teams.home.hits")
            'Me.dtInnings.Rows(1).Item("E") = lineScore.SelectToken("teams.home.errors")
        Next

        Dim columnR As DataColumn = New DataColumn()
        columnR.DataType = System.Type.GetType("System.String")
        columnR.ColumnName = "R"
        Me.dtInnings.Columns.Add(columnR)

        Dim columnH As DataColumn = New DataColumn()
        columnH.DataType = System.Type.GetType("System.String")
        columnH.ColumnName = "H"
        Me.dtInnings.Columns.Add(columnH)

        Dim columnE As DataColumn = New DataColumn()
        columnE.DataType = System.Type.GetType("System.String")
        columnE.ColumnName = "E"
        Me.dtInnings.Columns.Add(columnE)

        Me.dtInnings.Rows(0).Item("R") = lineScore.SelectToken("teams.away.runs").ToString
        Me.dtInnings.Rows(0).Item("H") = lineScore.SelectToken("teams.away.hits").ToString
        Me.dtInnings.Rows(0).Item("E") = lineScore.SelectToken("teams.away.errors").ToString

        Me.dtInnings.Rows(1).Item("R") = lineScore.SelectToken("teams.home.runs").ToString
        Me.dtInnings.Rows(1).Item("H") = lineScore.SelectToken("teams.home.hits").ToString
        Me.dtInnings.Rows(1).Item("E") = lineScore.SelectToken("teams.home.errors").ToString

        dgvInnings.ClearSelection()


    End Sub

    Private Sub updateBaseRunners()
        Dim lineData As JObject = Me.SBData.getLineScoreData()
        Dim offenseData As JObject = lineData.SelectToken("offense")
        Dim first As Boolean = False
        Dim second As Boolean = False
        Dim third As Boolean = False

        If offenseData.ContainsKey("first") Then
            first = True
        End If

        If offenseData.ContainsKey("second") Then
            second = True
        End If

        If offenseData.ContainsKey("third") Then
            third = True
        End If

        If first = False And second = False And third = False Then
            Me.imgDiamond.Image = My.Resources.diamond
        End If

        If first = True And second = False And third = False Then
            Me.imgDiamond.Image = My.Resources.diamond1
        End If

        If first = False And second = True And third = False Then
            Me.imgDiamond.Image = My.Resources.diamond2
        End If

        If first = False And second = False And third = True Then
            Me.imgDiamond.Image = My.Resources.diamond3
        End If

        If first = True And second = True And third = False Then
            Me.imgDiamond.Image = My.Resources.diamond1_2
        End If

        If first = False And second = True And third = True Then
            Me.imgDiamond.Image = My.Resources.diamond2_3
        End If

        If first = True And second = True And third = True Then
            Me.imgDiamond.Image = My.Resources.diamond1_2_3
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.runScoreboard()
    End Sub

    Private Sub loadTeamRosters()

        ' clear roster table before update
        Me.SBData.getDB.clearPlayersTable()

        ' get active batters and pitchers for away team
        Dim playerIds As List(Of String) = New List(Of String)
        Dim teamAbbr = SBData.getDB().returnTeamAbbr("away")
        For Each id As String In SBData.getBoxScoreData().SelectToken("teams.away.batters")
            playerIds.Add(id)
        Next
        'For Each id As String In SBData.getBoxScoreData().SelectToken("teams.away.pitchers")
        '    playerIds.Add(id)
        'Next

        ' get player data and add to database
        For Each id As String In playerIds
            Dim jerseyNumber As String = ""
            Dim fullName As String = ""
            Dim position As String = ""
            Dim lastName As String = ""

            For Each player As JProperty In SBData.getBoxScoreData().SelectToken("teams.away.players")
                If player.Value.Item("person").Item("id").ToString().Equals(id) Then
                    jerseyNumber = player.Value.Item("jerseyNumber")
                    fullName = player.Value.Item("person").Item("fullName")
                    position = player.Value.Item("position").Item("name")
                    SBData.getDB().insertPlayerDataIntoDB(player.Value.Item("person").Item("id").ToString(),
                                                          teamAbbr, fullName, position, jerseyNumber)

                    Exit For
                End If
            Next
        Next

        ' get active batters and pitchers for home team
        playerIds.Clear()
        teamAbbr = SBData.getDB().returnTeamAbbr("home")
        For Each id As String In SBData.getBoxScoreData().SelectToken("teams.home.batters")
            playerIds.Add(id)
        Next
        'For Each id As String In SBData.getBoxScoreData().SelectToken("teams.home.pitchers")
        '    playerIds.Add(id)
        'Next

        ' get player data and add to database
        For Each id As String In playerIds
            Dim jerseyNumber As String = ""
            Dim fullName As String = ""
            Dim position As String = ""
            Dim lastName As String = ""

            For Each player As JProperty In SBData.getBoxScoreData().SelectToken("teams.home.players")
                If player.Value.Item("person").Item("id").ToString().Equals(id) Then
                    jerseyNumber = player.Value.Item("jerseyNumber")
                    fullName = player.Value.Item("person").Item("fullName")
                    position = player.Value.Item("position").Item("name")
                    SBData.getDB().insertPlayerDataIntoDB(player.Value.Item("person").Item("id").ToString(),
                                                          teamAbbr, fullName, position, jerseyNumber)

                    Exit For
                End If
            Next
        Next

    End Sub

    Private Sub updateTeamRosters()

        Dim awayTeamAbbr As String = SBData.getDB().returnTeamAbbr("away")
        Dim homeTeamAbbr As String = SBData.getDB().returnTeamAbbr("home")
        lblAwayRoster.Text = awayTeamAbbr + " Roster"
        lblHomeRoster.Text = homeTeamAbbr + " Roster"
        dgvAwayRoster.DataSource = SBData.getDB().returnTeamRoster("away")
        dgvHomeRoster.DataSource = SBData.getDB().returnTeamRoster("home")
        dgvAwayRoster.ClearSelection()
        dgvHomeRoster.ClearSelection()

    End Sub

    Private Sub updatePitcherBatterMatchup()
        Dim playData As JObject = SBData.getCurrentPlayData()
        Dim pitcherId As String = "ID" + playData.SelectToken("matchup.pitcher.id").ToString
        Dim batterId As String = "ID" + playData.SelectToken("matchup.batter.id").ToString

        Dim pitcherName As String = playData.SelectToken("matchup.pitcher.fullName")
        Dim batterName As String = playData.SelectToken("matchup.batter.fullName")

        Dim pitcherStats As String = String.Empty
        Dim batterStats As String = String.Empty

        Dim awayTeamPlayers As JObject = SBData.getBoxScoreData().SelectToken("teams.away.players")
        Dim homeTeamPlayers As JObject = SBData.getBoxScoreData().SelectToken("teams.home.players")


        For Each player In awayTeamPlayers
            If player.Key = pitcherId Then
                Dim wins As String = player.Value.Item("seasonStats").Item("pitching").Item("wins")
                Dim losses As String = player.Value.Item("seasonStats").Item("pitching").Item("losses")
                Dim era As String = player.Value.Item("seasonStats").Item("pitching").Item("era")
                pitcherStats = String.Format(" ({0}-{1}, {2} ERA)", wins, losses, era)
            End If

            If player.Key = batterId Then
                Dim hits As String = player.Value.Item("stats").Item("batting").Item("hits")
                Dim atBats As String = player.Value.Item("stats").Item("batting").Item("atBats")
                Dim avg As String = player.Value.Item("seasonStats").Item("batting").Item("avg")
                batterStats = String.Format(" ({0}-{1}, {2} AVG)", hits, atBats, avg)
            End If
        Next


        For Each player In homeTeamPlayers
            If pitcherStats = String.Empty Then
                If player.Key = pitcherId Then
                    Dim wins As String = player.Value.Item("seasonStats").Item("pitching").Item("wins")
                    Dim losses As String = player.Value.Item("seasonStats").Item("pitching").Item("losses")
                    Dim era As String = player.Value.Item("seasonStats").Item("pitching").Item("era")
                    pitcherStats = String.Format(" ({0}-{1}, {2} ERA)", wins, losses, era)
                End If
            End If

            If batterStats = String.Empty Then
                If player.Key = batterId Then
                    Dim hits As String = player.Value.Item("stats").Item("batting").Item("hits")
                    Dim atBats As String = player.Value.Item("stats").Item("batting").Item("atBats")
                    Dim avg As String = player.Value.Item("seasonStats").Item("batting").Item("avg")
                    batterStats = String.Format(" ({0}-{1}, {2} AVG)", hits, atBats, avg)
                End If
            End If
        Next

        Dim matchup As String = String.Format("Pitcher: {0} {1} - Batter: {2} {3}", pitcherName, pitcherStats, batterName, batterStats)
        ' txbMatchup.Text = matchup
        lblMatchup.Text = matchup
    End Sub

    Private Sub updateLastPitchDescription()
        Dim currentPlayData As JObject = SBData.getCurrentPlayData()
        Dim playEvents As JArray = currentPlayData.SelectToken("playEvents")
        Dim lastEventIdx As Int32 = playEvents.Count - 1
        If lastEventIdx < 0 Then
            Return
        End If
        Dim pitchResults = playEvents.Item(lastEventIdx)
        Dim pitchType = pitchResults.SelectToken("details.type.description")
        Dim pitchCall As String = pitchResults.SelectToken("details.description")
        Dim startSpeed As String = pitchResults.SelectToken("pitchData.startSpeed")
        Dim endSpeed As String = pitchResults.SelectToken("pitchData.endSpeed")
        Dim pitchNum As String = pitchResults.SelectToken("pitchNumber")

        Dim desc As String
        If pitchType = String.Empty Then
            desc = String.Empty
        Else
            desc = String.Format("Pitch #{0}: {1} - {4}.  (Start {2}mph, End {3}mph)", pitchNum, pitchType, startSpeed, endSpeed, pitchCall.ToUpper
                                 )
        End If

        'Me.txbPitch.Text = desc
        Me.lblLastPitch.Text = desc

    End Sub


End Class
