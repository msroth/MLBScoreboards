﻿
Imports Newtonsoft.Json.Linq

Public Class MLBScoreboard

    Dim SBData As ScoreboardData = New ScoreboardData()
    Dim gamePk As Integer = 0
    Dim dtInnings As DataTable = New DataTable("Innings")
    Dim dtAllGames As DataTable
    Dim mAllTeams As List(Of Team) = New List(Of Team)
    Dim mAllGames As List(Of Game) = New List(Of Game)


    Private Sub setupInningsDataTable()
        ' clear datatable
        If dtInnings.Columns.Count > 0 Then
            For i As Integer = 0 To dtInnings.Columns.Count
                dtInnings.Columns.Remove(i)
            Next
        End If

        If dtInnings.Rows.Count > 0 Then
            For i As Integer = 0 To dtInnings.Rows.Count
                Dim dr As DataRow = dtInnings.Rows(i)
                dtInnings.Rows.Remove(dr)
            Next
        End If

        ' clear grid
        If Me.dgvInnings.Columns.Count > 0 Then
            For i As Integer = 0 To dgvInnings.Rows.Count
                dgvInnings.Columns.Remove(i)
            Next
        End If

        If Me.dgvInnings.Rows.Count > 0 Then
            For i As Integer = 0 To dgvInnings.Rows.Count
                Dim dr As DataGridViewRow = dgvInnings.Rows(i)
                dgvInnings.Rows.Remove(dr)
            Next
        End If

        ' setup new table
        Dim column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = " "
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

        'setup new grid
        dgvInnings.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    End Sub
    Private Sub MLBScoreboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' setup inning table with empty innings
        Me.setupInningsDataTable()

        ' set tables as datasource for grid views
        Me.dgvInnings.DataSource = Me.dtInnings
        Me.dgvGames.DataSource = Me.dtAllGames

        Me.dgvInnings.ClearSelection()
        Me.dgvAwayRoster.ClearSelection()
        Me.dgvHomeRoster.ClearSelection()
        Me.dgvGames.ClearSelection()

        ' load teams into database
        SBData.loadTeamsDataIntoDB()


        ' load teams into array of objects
        mAllTeams = SBData.LoadTeamsData()




        ' force event to load games for today
        Me.calDatePicker_CloseUp(Nothing, Nothing)

        ' set time for every 15 sec or as configured
        Dim Props As Properties = SBData.getProperties()
        Dim UpdateSeconds As Integer = Convert.ToInt32(Props.GetProperty(Props.TIMER_KEY, "15"))
        SetTimerInterval(UpdateSeconds)

        ' if a favorite team specified, find gamePk and load it
        If Me.gamePk = 0 Then
            Dim FaveTeam As String = Props.GetProperty(Props.FAVORITE_TEAM_KEY)
            If Not FaveTeam = String.Empty Then
                Me.gamePk = findTeamGamePk(FaveTeam)
                RunScoreboard()
            End If
        End If

    End Sub

    Private Sub ResetScreenControls()
        lblAwayLineup.Text = "Away Roster"
        lblAwayLineup.Visible = False
        lblHomeLineup.Text = "Home Roster"
        lblHomeLineup.Visible = False
        lblBalls.Text = "Balls: 0"
        lblBalls.Visible = True
        lblStrikes.Text = "Strikes: 0"
        lblStrikes.Text = True
        lblOuts.Text = "Outs: 0"
        lblOuts.Visible = True
        lblGameTitle.Text = "Away @ Home - mm/dd/yyyy (gamePk)"
        lblMatchup.Text = "Match up"
        lblMatchup.Visible = True
        imgDiamond.Image = My.Resources.diamond
        imgAwayLogo.Image = My.Resources.MLB
        imgHomeLogo.Image = My.Resources.MLB
        tbxCommentary.Text = ""
        tbxCommentary.Visible = False
        dgvAwayRoster.DataSource = Nothing
        dgvHomeRoster.DataSource = Nothing
        dgvInnings.DataSource = Nothing
        dgvGames.DataSource = Nothing
        lblWeather.Text = "Weather"
        lblAwayWinnerLoser.Text = "Winner-Loser"
        lblHomeWinnerLoser.Text = "Winner-Loser"
        lblGamePk.Text = "GamePk"
        lblStatus.Text = "Game Status"
        Me.gamePk = 0
    End Sub

    Private Sub setGameTitle()
        Dim result As DataTable = Me.SBData.getCurrentGameData()
        Dim gameStatus As String = Me.SBData.getLiveData().SelectToken("gameData.status.detailedState")
        Me.lblGameTitle.Text = String.Format("{0} @ {1} - {2} {3}", result.Rows(0).Field(Of String)(4),
                                             result.Rows(0).Field(Of String)(3), result.Rows(0).Field(Of String)(5),
                                             result.Rows(0).Field(Of String)(6))
        Me.lblGamePk.Text = "Game Id: " + gamePk.ToString
        Me.lblStatus.Text = "Game Status: " + gameStatus
    End Sub

    Private Sub updatePlayCommentary()
        Dim pitch = Me.updateLastPitchDescription()
        Dim play = Me.SBData.getLastPlayDescription()
        If pitch = String.Empty Then
            Me.tbxCommentary.Text = play
        Else
            Me.tbxCommentary.Text = pitch + vbCr + vbCr + play
        End If

    End Sub

    Private Sub RunScoreboard()

        ' get game date
        Dim gameDate As String = Me.calDatePicker.Value.ToString("MM/dd/yyyy")

        ' load games into array of objects
        Me.Cursor = Cursors.WaitCursor
        mAllGames.Clear()
        mAllGames = SBData.LoadAllGamesData(gameDate)

        ' load all game data for date and update grid
        'SBData.LoadAllGamesData(gameDate)
        Me.dgvGames.DataSource = Nothing
        Me.LoadAllGamesDataGrid()
        Me.Cursor = Cursors.Default


        ' update status bar
        Me.ToolStripStatusLabel1.Text = "Data updated " + Date.Now

        ' update game data?
        If Me.gamePk > 0 Then
            Me.RunGame()
        End If

    End Sub

    Sub RunGame()

        If Me.gamePk = 0 Then
            Return
        End If

        ' get live data using gamepk
        Me.Cursor = Cursors.WaitCursor
        SBData.refreshLiveData(Me.gamePk)

        Dim liveData As JObject = SBData.getLiveData()
        Dim boxData As JObject = SBData.getBoxScoreData()

        ' get game date
        Dim gameDate As String = Me.calDatePicker.Value.ToString("MM/dd/yyyy")

        ' get game time
        Dim gameTime As String = liveData.SelectToken("gameData.datetime.time").ToString + liveData.SelectToken("gameData.datetime.ampm").ToString

        ' load current game data into database
        Me.SBData.loadCurrentGameDataIntoDB(liveData, Me.SBData.getBoxScoreData(), gameDate, gameTime, Me.gamePk)




        '------------------
        ' load game object
        Dim ThisGame As Game = New Game(Me.gamePk)
        Trace.WriteLine(ThisGame.ToString())

        ThisGame.AwayTeam.LoadLineupData(Me.gamePk)
        ThisGame.HomeTeam.LoadLineupData(Me.gamePk)
        Trace.WriteLine(ThisGame.AwayTeam.ToString())
        Trace.WriteLine(ThisGame.HomeTeam.ToString())








        Me.Cursor = Cursors.Default

        ' update game label
        Me.setGameTitle()

        'update innings datatable
        Me.updateInnings()

        ' clear inning label
        dtInnings.Columns(0).ColumnName = " "

        ' update team logos
        Me.loadTeamLogos()

        ' load weather
        Me.lblWeather.Text = SBData.getVenueWeather()

        Dim status As String = SBData.getGameStatus().ToUpper()

        If status = "SCHEDULED" Or status = "WARMUP" Or status = "PRE-GAME" Or status.StartsWith("DELAYED") Or status = "POSTPONED" Then
            lblMatchup.Text = updatePitcherMatchup(ThisGame)
            tbxCommentary.Visible = False
            dgvAwayRoster.Visible = False
            dgvHomeRoster.Visible = False
            lblAwayLineup.Visible = False
            lblHomeLineup.Visible = False
            lblWeather.Visible = True
            lblBalls.Visible = False
            lblStrikes.Visible = False
            lblOuts.Visible = False
            imgDiamond.Visible = False
            lblAwayWinnerLoser.Visible = False
            lblHomeWinnerLoser.Visible = False

        End If

        If status = "FINAL" Or status = "COMPLETE" Or status = "GAME OVER" Then

            'lblMatchup.Text = updateWinnerLosserPitchers()
            lblMatchup.Text = UpdateWinnerLoserPitchers(ThisGame)

            ' turn off unused controls
            tbxCommentary.Visible = False
            lblBalls.Visible = False
            lblStrikes.Visible = False
            lblOuts.Visible = False
            imgDiamond.Visible = False
            dgvAwayRoster.Visible = False
            dgvHomeRoster.Visible = False
            lblAwayLineup.Visible = False
            lblHomeLineup.Visible = False
            lblWeather.Visible = False
            lblAwayWinnerLoser.Visible = True
            lblHomeWinnerLoser.Visible = True

            ' set winner - loser labels
            If ThisGame.WinningTeam.Abbr = ThisGame.AwayTeam.Abbr Then
                lblAwayWinnerLoser.Text = "Winner"
                lblHomeWinnerLoser.Text = "Loser"
            Else
                lblAwayWinnerLoser.Text = "Loser"
                lblHomeWinnerLoser.Text = "Winner"
            End If

            ' fill X in unplayed bottom of last inning
            Dim v = dtInnings.Rows(1).Item(dtInnings.Columns.Count - 4).ToString
            If dtInnings.Rows(1).Item(dtInnings.Columns.Count - 4).ToString = " " Then
                dtInnings.Rows(1).Item(dtInnings.Columns.Count - 4) = "X"
            End If

            dgvInnings.ClearSelection()
        End If

        If status = "IN PROGRESS" Or status.Contains("MANAGER") Or status.Contains("OFFICIAL") Then

            tbxCommentary.Visible = True
            lblBalls.Visible = True
            lblStrikes.Visible = True
            lblOuts.Visible = True
            imgDiamond.Visible = True
            dgvAwayRoster.Visible = True
            dgvHomeRoster.Visible = True
            lblAwayLineup.Visible = True
            lblHomeLineup.Visible = True
            lblWeather.Visible = True
            lblHomeWinnerLoser.Visible = False
            lblAwayWinnerLoser.Visible = False



            '' load team object line ups
            'If AwayTeam IsNot Nothing Then
            '    AwayTeam.LoadLineupData(Me.gamePk)
            '    Trace.WriteLine(AwayTeam.ToString())
            'End If

            'If HomeTeam IsNot Nothing Then
            '    HomeTeam.LoadLineupData(Me.gamePk)
            '    Trace.WriteLine(HomeTeam.ToString())
            'End If





            ' update inning label in inning table
            Dim inningLabel As String
            If SBData.getCurrentInningState() = "MIDDLE" Then
                inningLabel = SBData.getCurrentInningState() + " " + SBData.getCurrentInningNumber()
            Else
                inningLabel = SBData.getCurrentInningHalf() + " " + SBData.getCurrentInningNumber()
            End If
            dtInnings.Columns(0).ColumnName = inningLabel
            HighlightCurrentInning()

            ' update Balls, Strikes, Outs
            Me.updateBSO()

            ' update team rosters
            Me.updateTeamLineups()

            ' update pitcher-batter matchup
            Me.updatePitcherBatterMatchup()

            ' update last play
            Me.updatePlayCommentary()

            ' update last pitch data
            Me.updateLastPitchDescription()

            'update base runners image
            Me.updateBaseRunners()

            ' load team rosters
            Me.loadTeamLineups(ThisGame)

            ' if mid inning or end inning show due ups
            If SBData.getCurrentInningState() = "END" Or SBData.getCurrentInningState() = "MIDDLE" Then
                Dim dueUpBatterInfo(3) As String
                Dim dueUpBatterNames(3) As String
                Dim grid As DataGridView

                Dim lastBatterId = SBData.getLastOutBatterId()
                Dim dueUpIds As List(Of String) = Me.getDueUpIds(lastBatterId)
                Dim team As String

                For i As Integer = 0 To dueUpIds.Count - 1
                    If SBData.getCurrentInningState() = "END" Then
                        grid = dgvAwayRoster
                        team = "away"
                    Else
                        grid = dgvHomeRoster
                        team = "home"
                    End If

                    For Each row As DataGridViewRow In grid.Rows
                        If dueUpIds(i) = row.Cells("id").Value.ToString Then
                            dueUpBatterNames(i) = row.Cells("Name").Value.ToString
                            Exit For
                        End If
                    Next

                    dueUpBatterInfo(i) = String.Format("  {0} {1}", dueUpBatterNames(i), SBData.getBatterStats(dueUpIds(i), team))
                Next

                Dim dueUps As String = vbCr + vbCr + "Due up:" + vbCr
                For Each batter As String In dueUpBatterInfo
                    dueUps += batter + vbCr
                Next

                Me.tbxCommentary.Text += dueUps
            End If

        End If

    End Sub

    Private Sub loadTeamLogos()


        Dim awayTeam As String = SBData.GetAwayTeamAbbr()
        Dim homeTeam As String = SBData.GetHomeTeamAbbr()
        Dim awayImg = My.Resources.ResourceManager().GetObject(awayTeam)
        Dim homeImg = My.Resources.ResourceManager().GetObject(homeTeam)
        Me.imgAwayLogo.Image = awayImg
        Me.imgHomeLogo.Image = homeImg


    End Sub
    Function getDueUpIds(lastBatterId As String) As List(Of String)
        Dim ids As List(Of String) = New List(Of String)
        Dim startIdx As Integer
        Dim grid As DataGridView

        'Trace.WriteLine($"lastBatterId={lastBatterId}")

        If SBData.getCurrentInningState() = "END" Then
            grid = dgvAwayRoster
        Else
            grid = dgvHomeRoster
        End If

        ' find last batter in lineup
        For Each row As DataGridViewRow In grid.Rows
            If row.Cells("id").Value.ToString.Equals(lastBatterId) Then
                startIdx = row.Index
                'Trace.WriteLine($"startIdx={startIdx}")
                Exit For
            End If
        Next


        ' get next two batter ids - skip pitchers
        Dim idx As Integer = startIdx + 1
        While ids.Count < 3
            If idx >= grid.RowCount Then
                idx = idx - grid.RowCount
            End If
            'Trace.WriteLine($"idx={idx}")
            If Not grid.Rows(idx).Cells("Position").Value.ToString.ToUpper = "P" Then
                ids.Add(grid.Rows(idx).Cells("Id").Value.ToString)

            End If
            idx += 1
        End While

        Return ids
    End Function

    Private Sub updateBSO()
        Dim playData = Me.SBData.getCurrentPlayData()
        Me.lblBalls.Text = String.Format("Balls: {0}", playData.SelectToken("count.balls"))
        Me.lblStrikes.Text = String.Format("Strikes: {0}", playData.SelectToken("count.strikes"))
        Me.lblOuts.Text = String.Format("Outs: {0}", playData.SelectToken("count.outs"))
    End Sub

    Private Sub updateInnings()

        ' clears grid and data
        dtInnings = New DataTable("Innings")
        dgvInnings.DataSource = dtInnings

        ' add rows for away and home lines
        dtInnings.Rows.Add()
        dtInnings.Rows.Add()

        ' add column for team name and record
        Dim col As DataColumn = New DataColumn()
        col.ColumnName = ""
        dtInnings.Columns.Add(col)

        Dim awayWins As String = Me.SBData.getLiveData().SelectToken("gameData.teams.away.record.wins")
        Dim awayLoses As String = Me.SBData.getLiveData().SelectToken("gameData.teams.away.record.losses")
        Dim awayAbbrev As String = Me.SBData.getLiveData().SelectToken("gameData.teams.away.abbreviation")
        Dim awayNameAndRecord As String = String.Format("{0} ({1}-{2})", awayAbbrev, awayWins, awayLoses)
        dtInnings.Rows(0).Item(0) = awayNameAndRecord

        Dim homeWins As String = Me.SBData.getLiveData().SelectToken("gameData.teams.home.record.wins")
        Dim homeLoses As String = Me.SBData.getLiveData().SelectToken("gameData.teams.home.record.losses")
        Dim homeAbbrev As String = Me.SBData.getLiveData().SelectToken("gameData.teams.home.abbreviation")
        Dim homeNameAndRecord As String = String.Format("{0} ({1}-{2})", homeAbbrev, homeWins, homeLoses)
        dtInnings.Rows(1).Item(0) = homeNameAndRecord

        ' fill innings
        Dim lineScore As JObject = Me.SBData.getLineScoreData
        Dim innings As JArray = lineScore.SelectToken("innings")
        Dim inningCount As Int32 = 0
        For Each inning As JObject In innings

            ' create a column for inning
            col = New DataColumn()
            col.ColumnName = inning.SelectToken("num")
            dtInnings.Columns.Add(col)
            inningCount += 1

            ' away data
            Dim runs As String = inning.SelectToken("away.runs")
            If runs Is Nothing Then
                runs = " "
            End If
            Me.dtInnings.Rows(0).Item(Integer.Parse(inning.SelectToken("num"))) = runs

            ' home data
            runs = inning.SelectToken("home.runs")
            If runs Is Nothing Then
                runs = " "
            End If
            Me.dtInnings.Rows(1).Item(Integer.Parse(inning.SelectToken("num"))) = runs
        Next

        ' fill in remaining empty columns for unplayed innings
        If inningCount < 9 Then
            For i As Int32 = inningCount + 1 To 9
                col = New DataColumn()
                col.ColumnName = i
                dtInnings.Columns.Add(col)
            Next
        End If

        ' create R-H-E coluumns
        Dim RHE() As String = {"R", "H", "E"}
        For Each stat As String In RHE
            col = New DataColumn()
            col.ColumnName = stat
            dtInnings.Columns.Add(col)
        Next

        ' fill R-H-E columns
        Dim status As String = SBData.getGameStatus().ToUpper
        If status = "IN PROGRESS" Or status = "FINAL" Or status = "COMPLETE" Or status = "GAME OVER" Or status.Contains("MANAGER") Or status.Contains("OFFICIAL") Then
            Me.dtInnings.Rows(0).Item("R") = lineScore.SelectToken("teams.away.runs").ToString()
            Me.dtInnings.Rows(0).Item("H") = lineScore.SelectToken("teams.away.hits").ToString()
            Me.dtInnings.Rows(0).Item("E") = lineScore.SelectToken("teams.away.errors").ToString()

            Me.dtInnings.Rows(1).Item("R") = lineScore.SelectToken("teams.home.runs").ToString()
            Me.dtInnings.Rows(1).Item("H") = lineScore.SelectToken("teams.home.hits").ToString()
            Me.dtInnings.Rows(1).Item("E") = lineScore.SelectToken("teams.home.errors").ToString()
        End If

        ' patch up the grid view
        dgvInnings.ClearSelection()
        dgvInnings.ColumnHeadersDefaultCellStyle.Font = New Font(dgvInnings.DefaultFont, FontStyle.Bold)

    End Sub

    Private Function UpdateWinnerLoserPitchers(game As Game) As String

        ' get pitchers wins, loses, And ERA
        Dim winnerStats As String = SBData.getPitchingStats(game.WinningPitcherId, game.WinningTeam.Abbr())
        Dim loserStats As String = SBData.getPitchingStats(game.LosingPitcherId, game.LosingTeam.Abbr())
        Dim winnerName As String = game.WinningTeam.GetPlayerData(game.WinningPitcherId).Name()
        Dim loserName As String = game.LosingTeam.GetPlayerData(game.LosingPitcherId).Name()

        Dim winLoseLine = String.Format("Winner: {0} {1}, Loser: {2} {3}", winnerName, winnerStats, loserName, loserStats)
        Return winLoseLine


    End Function


    Private Function updateWinnerLosserPitchers() As String
        Dim liveData As JObject = SBData.getLiveData
        Dim winnerName = liveData.SelectToken("liveData.decisions.winner.fullName")
        Dim loserName = liveData.SelectToken("liveData.decisions.loser.fullName")
        Dim winnerId = liveData.SelectToken("liveData.decisions.winner.id")
        Dim loserId = liveData.SelectToken("liveData.decisions.loser.id")
        Dim winTeam As String
        Dim loseTeam As String



        If SBData.IsAwayTeamWinner() = True Then
            winTeam = SBData.GetAwayTeamAbbr()
            loseTeam = SBData.GetHomeTeamAbbr()
        Else
            winTeam = SBData.GetHomeTeamAbbr()
            loseTeam = SBData.GetAwayTeamAbbr()
        End If




        ' get pitchers wins, loses, And ERA
        Dim winnerStats As String = SBData.getPitchingStats(winnerId, winTeam)
        Dim loserStats As String = SBData.getPitchingStats(loserId, loseTeam)

        Dim winLoseLine = String.Format("Winner: {0} {1}, Loser: {2} {3}", winnerName, winnerStats, loserName, loserStats)
        Return winLoseLine
    End Function

    Private Function updatePitcherMatchup(CurrentGame As Game) As String
        Dim matchup As String = ""

        'Dim livedata As JObject = SBData.getLiveData()
        'Dim awayPitcherName As String = livedata.SelectToken("gameData.probablePitchers.away.fullName")
        'Dim awayPitcherId As String = livedata.SelectToken("gameData.probablePitchers.away.id")
        'Dim homePitcherName As String = livedata.SelectToken("gameData.probablePitchers.home.fullName")
        'Dim homePitcherId As String = livedata.SelectToken("gameData.probablePitchers.home.id")


        Dim awayPitcherId As String = CurrentGame.AwayScheduledPitcherId()
        Dim awayPitcherName As String = CurrentGame.AwayTeam.GetPlayerData(awayPitcherId).Name()
        Dim homePitcherId As String = CurrentGame.HomeScheduledPitcherId()
        Dim homePitcherName As String = CurrentGame.HomeTeam.GetPlayerData(homePitcherId).Name()

        Dim awayPitchingStats As String = SBData.getPitchingStats(awayPitcherId, SBData.GetAwayTeamAbbr())
        Dim homePitchingStats As String = SBData.getPitchingStats(homePitcherId, SBData.GetHomeTeamAbbr())

        matchup = String.Format("{0} {1} vs. {2} {3}", awayPitcherName, awayPitchingStats, homePitcherName, homePitchingStats)
        Return matchup

    End Function


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

        If first = True And second = False And third = True Then
            Me.imgDiamond.Image = My.Resources.diamond1_3
        End If

        If first = True And second = True And third = True Then
            Me.imgDiamond.Image = My.Resources.diamond1_2_3
        End If

    End Sub


    Private Sub loadTeamLineups(game As Game)

        dgvAwayRoster.DataSource = game.AwayTeam.GetLinup()
        dgvHomeRoster.DataSource = game.HomeTeam.GetLinup()


        '' clear roster table before update
        'Me.SBData.getDB.clearPlayersTable()

        '' get active batters and pitchers for away team
        'Dim playerIds As List(Of String) = New List(Of String)
        'Dim awayTeamAbbr = SBData.GetAwayTeamAbbr()
        'For Each id As String In SBData.getBoxScoreData().SelectToken("teams.away.batters")
        '    playerIds.Add(id)
        'Next

        '' get player data and add to database
        'For Each id As String In playerIds
        '    Dim jerseyNumber As String = ""
        '    Dim fullName As String = ""
        '    Dim position As String = ""
        '    Dim lastName As String = ""

        '    For Each player As JProperty In SBData.getBoxScoreData().SelectToken("teams.away.players")
        '        If player.Value.Item("person").Item("id").ToString().Equals(id) Then
        '            jerseyNumber = player.Value.Item("jerseyNumber")
        '            fullName = player.Value.Item("person").Item("fullName")
        '            position = player.Value.Item("position").Item("name")
        '            SBData.getDB().insertPlayerDataIntoDB(player.Value.Item("person").Item("id").ToString(),
        '                                                  awayTeamAbbr, fullName, position, jerseyNumber)

        '            Exit For
        '        End If
        '    Next
        'Next

        '' get active batters and pitchers for home team
        'playerIds.Clear()
        'Dim homeTeamAbbr = SBData.GetHomeTeamAbbr()
        'For Each id As String In SBData.getBoxScoreData().SelectToken("teams.home.batters")
        '    playerIds.Add(id)
        'Next

        '' get player data and add to database
        'For Each id As String In playerIds
        '    Dim jerseyNumber As String = ""
        '    Dim fullName As String = ""
        '    Dim position As String = ""
        '    Dim lastName As String = ""

        '    For Each player As JProperty In SBData.getBoxScoreData().SelectToken("teams.home.players")
        '        If player.Value.Item("person").Item("id").ToString().Equals(id) Then
        '            jerseyNumber = player.Value.Item("jerseyNumber")
        '            fullName = player.Value.Item("person").Item("fullName")
        '            position = player.Value.Item("position").Item("name")
        '            SBData.getDB().insertPlayerDataIntoDB(player.Value.Item("person").Item("id").ToString(),
        '                                                  homeTeamAbbr, fullName, position, jerseyNumber)

        '            Exit For
        '        End If
        '    Next
        'Next

    End Sub

    Private Sub updateTeamLineups()

        Dim awayTeamAbbr As String = SBData.getAwayTeamAbbr()
        Dim homeTeamAbbr As String = SBData.getHomeTeamAbbr()
        Dim awayTeamName As String = SBData.GetAwayTeamShortName()
        Dim homeTeamName As String = SBData.GetHomeTeamShortName()
        lblAwayLineup.Text = awayTeamName + " Lineup"
        lblHomeLineup.Text = homeTeamName + " Lineup"
        dgvAwayRoster.DataSource = SBData.getTeamLineup(awayTeamAbbr)
        dgvHomeRoster.DataSource = SBData.getTeamLineup(homeTeamAbbr)
        dgvAwayRoster.Columns("id").Visible = False
        dgvHomeRoster.Columns("id").Visible = False
        dgvAwayRoster.ColumnHeadersDefaultCellStyle.Font = New Font(dgvAwayRoster.DefaultFont, FontStyle.Bold)
        dgvHomeRoster.ColumnHeadersDefaultCellStyle.Font = New Font(dgvHomeRoster.DefaultFont, FontStyle.Bold)
        dgvAwayRoster.ClearSelection()
        dgvHomeRoster.ClearSelection()

    End Sub

    Private Sub updatePitcherBatterMatchup()
        Dim playData As JObject = SBData.getCurrentPlayData()

        ' get current pitcher and batter
        Dim pitcherId As String = playData.SelectToken("matchup.pitcher.id").ToString
        Dim batterId As String = playData.SelectToken("matchup.batter.id").ToString
        Dim pitcherName As String = playData.SelectToken("matchup.pitcher.fullName")
        Dim batterName As String = playData.SelectToken("matchup.batter.fullName")

        ' get stats
        Dim pitcherStats As String = String.Empty
        Dim batterStats As String = String.Empty

        If SBData.getCurrentInningHalf() = "TOP" Then
            pitcherStats = SBData.getPitchingStats(pitcherId, SBData.getHomeTeamAbbr())
            batterStats = SBData.getBatterStats(batterId, SBData.getAwayTeamAbbr())
        Else
            pitcherStats = SBData.getPitchingStats(pitcherId, SBData.getAwayTeamAbbr())
            batterStats = SBData.getBatterStats(batterId, SBData.getHomeTeamAbbr())
        End If

        ' update GUI
        Dim matchup As String = String.Format("Pitcher: {0} {1} - Batter: {2} {3}", pitcherName, pitcherStats, batterName, batterStats)
        lblMatchup.Text = matchup

        ' highlight pitcher and batter in lineup
        'HighlightPitcherBatterInLineup(pitcherId, batterId)

    End Sub

    Sub HighlightPitcherBatterInLineup(pitcherId As String, batterId As String)
        'dgvAwayRoster.ClearSelection()
        'dgvHomeRoster.ClearSelection()

        'For Each row As DataGridViewRow In dgvAwayRoster.Rows
        '    If pitcherId = row.Cells(0).Value.ToString Or batterId = row.Cells(0).Value.ToString Then
        '        row.DefaultCellStyle.BackColor = Color.LightBlue
        '        row.Selected = True
        '    Else
        '        row.DefaultCellStyle.BackColor = Color.Empty
        '    End If
        'Next

        'For Each row As DataGridViewRow In dgvHomeRoster.Rows
        '    If pitcherId = row.Cells(0).Value.ToString Or batterId = row.Cells(0).Value.ToString Then
        '        row.DefaultCellStyle.BackColor = Color.LightBlue
        '        row.Selected = True
        '    Else
        '        row.DefaultCellStyle.BackColor = Color.Empty
        '    End If
        'Next
        dgvAwayRoster.ClearSelection()
        dgvHomeRoster.ClearSelection()
        dgvAwayRoster.Refresh()
        dgvHomeRoster.Refresh()


    End Sub

    Private Function findTeamGamePk(teamAbbrev As String) As Integer
        Dim dt As DataTable = dgvGames.DataSource
        For Each row As DataRow In dt.Rows
            If row("Away") = teamAbbrev Or row("Home") = teamAbbrev Then
                Return row("id")
            End If
        Next
        Return 0
    End Function

    Private Function updateLastPitchDescription() As String
        Dim currentPlayData As JObject = SBData.getCurrentPlayData()
        Dim playEvents As JArray = currentPlayData.SelectToken("playEvents")
        Dim lastEventIdx As Int32 = playEvents.Count - 1
        If lastEventIdx < 0 Then
            Return String.Empty
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
        ElseIf startSpeed = "" Then
            desc = String.Format("{0}", pitchCall)
        Else
            desc = String.Format("Pitch #{0}: {1} - {4}  (Start {2}mph, End {3}mph)", pitchNum, pitchType, startSpeed, endSpeed, pitchCall)
        End If

        Return desc
    End Function

    Sub HighlightCurrentInning()
        Dim row As Integer
        Dim inning As Integer = Convert.ToInt32(SBData.getCurrentInningNumber())
        Dim inningHalf As String = SBData.getCurrentInningHalf().ToUpper

        If inningHalf = "TOP" Then
            row = 0
        Else
            row = 1
        End If

        ' TODO - better color?
        dgvInnings.Rows(row).Cells(inning).Style.BackColor = Color.LightBlue

    End Sub

    Private Sub QuitToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Application.Exit()
    End Sub

    Private Sub LoadAllGamesDataGrid()
        'Dim dt As DataTable = SBData.GetAllGamesData()
        'dgvGames.DataSource = dt
        'dgvGames.Columns("id").Visible = False
        'dgvGames.ClearSelection()
        'dgvGames.ColumnHeadersDefaultCellStyle.Font = New Font(dgvGames.DefaultFont, FontStyle.Bold)

        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("Id")
        dt.Columns.Add("Away")
        dt.Columns.Add("Score")
        dt.Columns.Add("Home")
        dt.Columns.Add("Status")
        dt.Columns.Add("Inning")
        For Each game As Game In Me.mAllGames
            Dim row As DataRow = dt.NewRow()
            row("Id") = game.GamePk
            row("Away") = game.AwayTeam.Abbr
            row("Home") = game.HomeTeam.Abbr
            row("Status") = game.GameStatus
            row("Score") = $"{game.AwayRuns} - {game.HomeRuns}"

            If game.GameStatus = "FINAL" Or game.GameStatus = "COMPLETE" Or game.GameStatus = "GAME OVER" Then
                If game.CurrentInning > 9 Then
                    row("Inning") = game.CurrentInning.ToString()
                Else
                    row("Inning") = ""
                End If
            ElseIf game.GameStatus = "SCHEDULED" Or game.GameStatus = "WARMUP" Or game.GameStatus = "PRE-GAME" Or game.GameStatus.StartsWith("DELAYED") Or game.GameStatus = "POSTPONED" Then
                row("Inning") = ""
            Else
                row("Inning") = game.CurrentInningHalf + " " + game.CurrentInning.ToString()
            End If

            dt.Rows.Add(row)
        Next

        dgvGames.DataSource = dt
        dgvGames.Columns("Id").Visible = False
        dgvGames.ClearSelection()
        dgvGames.ColumnHeadersDefaultCellStyle.Font = New Font(dgvGames.DefaultFont, FontStyle.Bold)

    End Sub

    Private Sub dgvGames_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvGames.CellClick, dgvGames.CellContentDoubleClick, dgvGames.CellContentClick

        ' bail if header row clicked
        If e.RowIndex < 0 Then
            Return
        End If

        ' get the gamePk for the clicked row
        Dim id As String = dgvGames.Rows(e.RowIndex).Cells(0).Value.ToString
        Me.gamePk = Convert.ToUInt32(id)

        ' force repaint which should highlight current selected game
        dgvGames.ClearSelection()
        dgvGames.InvalidateRow(e.RowIndex)

        ' run the selected game
        Me.RunGame()
    End Sub

    Private Sub calDatePicker_CloseUp(sender As Object, e As EventArgs) Handles calDatePicker.CloseUp
        Me.ResetScreenControls()
        Me.RunScoreboard()
    End Sub

    Private Sub UpdateTimer_Tick(sender As Object, e As EventArgs) Handles UpdateTimer.Tick
        Me.RunScoreboard()
    End Sub



    Private Sub dgvGames_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvGames.CellPainting
        ' if no current game selected, bail out
        If Me.gamePk = 0 Then
            Return
        End If

        ' color each cell of each row according to whether the game is running or not
        For Each row As DataGridViewRow In dgvGames.Rows
            Dim id As Integer = Convert.ToInt32(row.Cells(0).Value.ToString)
            If id = Me.gamePk Then
                row.DefaultCellStyle.BackColor = Color.LightBlue
            Else
                row.DefaultCellStyle.BackColor = Color.White
            End If
        Next
    End Sub

    Private Sub dgvAwayRoster_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvAwayRoster.CellPainting
        Dim pitcherId As String = SBData.getCurrentPitcherId()
        Dim batterId As String = SBData.getCurrentBatterId()

        For Each row As DataGridViewRow In dgvAwayRoster.Rows
            If pitcherId = row.Cells(0).Value.ToString Or batterId = row.Cells(0).Value.ToString Then
                EnsureRowVisible(dgvAwayRoster, row.Index)
                row.DefaultCellStyle.BackColor = Color.LightBlue
            Else
                row.DefaultCellStyle.BackColor = Color.White
            End If
        Next

    End Sub

    Private Sub dgvHomeRoster_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvHomeRoster.CellPainting
        Dim pitcherId As String = SBData.getCurrentPitcherId()
        Dim batterId As String = SBData.getCurrentBatterId()

        For Each row As DataGridViewRow In dgvHomeRoster.Rows
            If pitcherId = row.Cells(0).Value.ToString Or batterId = row.Cells(0).Value.ToString Then
                EnsureRowVisible(dgvHomeRoster, row.Index)
                row.DefaultCellStyle.BackColor = Color.LightBlue
            Else
                row.DefaultCellStyle.BackColor = Color.White
            End If
        Next

    End Sub

    Private Sub EnsureRowVisible(view As DataGridView, rowIdx As Integer)
        If rowIdx >= 0 And rowIdx < view.RowCount Then
            Dim countVisible As Integer = view.DisplayedRowCount(False)
            Dim firstVisible As Integer = view.FirstDisplayedScrollingRowIndex
            If rowIdx < firstVisible Then
                view.FirstDisplayedScrollingRowIndex = rowIdx
            ElseIf rowIdx >= firstVisible + countVisible Then
                view.FirstDisplayedScrollingRowIndex = rowIdx - countVisible + 1
            End If
        End If

    End Sub

    Private Sub QuitToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles QuitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub ConfigureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfigureToolStripMenuItem.Click
        Dim frmConfig = New Configure
        frmConfig.Show()
        SetTimerInterval(Convert.ToInt32(SBData.getProperties.GetProperty(SBData.getProperties.TIMER_KEY)))
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem1.Click
        Dim frmAbout = AboutBox
        frmAbout.ShowDialog()
    End Sub

    Private Sub SetTimerInterval(interval As Integer)
        UpdateTimer.Stop()
        UpdateTimer.Interval = interval * 1000
        UpdateTimer.Start()
        Trace.WriteLine($"timer interval set to {interval}")
    End Sub

    Private Sub PitcherStatsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PitcherStatsToolStripMenuItem.Click


        ' get pitcher stats
        ' boxscore.teams.away.players.ID.stats.pitching


    End Sub
End Class
