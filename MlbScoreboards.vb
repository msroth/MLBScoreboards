' =========================================================================================================
' (C) 2022 MSRoth
'
' Released under XXX license.
' =========================================================================================================

Imports System.Text
Imports Newtonsoft.Json.Linq
Imports NLog

Public Class MlbScoreboards

    Private mAPI As MlbApi = New MlbApi()
    Private mProperties As SBProperties = New SBProperties()
    Private mCurrentGame As MlbGame = Nothing
    Private mAllGames As Dictionary(Of String, MlbGame) = New Dictionary(Of String, MlbGame)

    Shared ReadOnly Logger As Logger = LogManager.GetCurrentClassLogger()


    Private Sub MLBScoreboards_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try

            ' config logger
            Dim config = New Config.LoggingConfiguration()
            Dim logfile = New Targets.FileTarget("logfile")
            logfile.FileName = ".\\logs\\MLBScoreboards.log"
            logfile.ArchiveFileName = ".\\logs\\MLBScoreboard" + "{####}.log"
            logfile.ArchiveAboveSize = 1000000000
            logfile.ArchiveNumbering = Targets.ArchiveNumberingMode.Sequence
            logfile.MaxArchiveFiles = 3
            Dim logconsole = New Targets.ConsoleTarget("logconsole")
            Dim outputconsole = New Targets.DebuggerTarget()
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logconsole)
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile)
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, outputconsole)
            LogManager.Configuration = config

            Logger.Info($"*** Application Started ***")

            Me.Cursor = Cursors.WaitCursor

            ' show splash screen
            Dim splash As New Splash
            splash.Show()
            splash.Refresh()

            ' load all games for today's date
            'Dim gameDate As String = Me.calDatePicker.Value.ToString("MM/dd/yyyy")
            'Me.mAllGames = Me.LoadAllGamesData(gameDate)

            ' set scoreboard timer for every 60 sec or as configured, and start it
            ' this time updates the list of games
            Dim SBUpdateSeconds As Integer = Convert.ToInt32(mProperties.GetProperty(mProperties.mSCOREBOARD_TIMER_KEY, "60"))
            SetScoreboardTimerInterval(SBUpdateSeconds)

            ' set game time for every 30 sec or as configured - do not start it yet
            ' this time updates all the controls for the currently selected game
            Dim GameUpdateSeconds As Integer = Convert.ToInt32(mProperties.GetProperty(mProperties.mGAME_TIMER_KEY, "25"))
            SetGameUpdateTimerInterval(GameUpdateSeconds, False)

            Me.Cursor = Cursors.Default

            ' if a favorite team specified, find game and load it
            'Me.SetCurrentGame()

            ' start the update timer
            'Me.GameUpdateTimer.Start()

            ' run the scoreboard
            ResetScreenControls()
            RunScoreboards()

            ' close splash screen
            splash.Close()

        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Private Sub RunScoreboards()
        Try
            Me.Cursor = Cursors.WaitCursor

            ' if mAllGames cleared by calendar pic, reload using new date
            If Me.mAllGames.Count = 0 Then
                Dim gameDate As String = Me.calDatePicker.Value.ToString("MM/dd/yyyy")
                mAllGames = Me.LoadAllGamesData(gameDate)
            End If

            ' show games
            Me.dgvGames.DataSource = Nothing
            Me.LoadAllGamesDataGrid()

            ' if the current game is not selected, or we switched dates, load
            ' game for favorite team
            Me.SetCurrentGame()

            ' set menu options accordingly
            If Me.mCurrentGame Is Nothing Then
                PlayRecapToolStripMenuItem.Enabled = False
                BoxscoreToolStripMenuItem.Enabled = False
            ElseIf MlbGame.CheckGameStatus(Me.mCurrentGame.GameStatus) = MlbGame.mGAME_STATUS_FUTURE Then
                PlayRecapToolStripMenuItem.Enabled = False
                BoxscoreToolStripMenuItem.Enabled = False
            Else
                PlayRecapToolStripMenuItem.Enabled = True
                BoxscoreToolStripMenuItem.Enabled = True
            End If

            ' update status bar
            Me.AllGamesUpdateData.Text = $"All Games Data Updated {Date.Now}  "

            ' run current game
            Me.RunGame()

        Catch ex As Exception
            Logger.Error(ex)
        End Try

        ' restore cursor
        Me.Cursor = Cursors.Default
    End Sub

    Sub RunGame()
        Try
            ' set menu options accordingly
            If Me.mCurrentGame Is Nothing Then
                Me.PlayRecapToolStripMenuItem.Enabled = False
                Me.BroadcastsToolStripMenuItem.Enabled = False
                Return
            Else

                Me.Cursor = Cursors.WaitCursor

                ' this forces a refresh of MLB data from API
                Me.mCurrentGame.LoadGameData()

                Me.Cursor = Cursors.Default

                ' update status bar
                Me.ThisGameUpdateData.Text = $"  Current Game ({Me.mCurrentGame.AwayTeam.Abbr} @ {Me.mCurrentGame.HomeTeam.Abbr}) Data Updated {Date.Now}"

                ' enable menu item
                Me.PlayRecapToolStripMenuItem.Enabled = True

                ' load team players
                Me.mCurrentGame.AwayTeam.LoadPlayerData(Me.mCurrentGame)
                Me.mCurrentGame.HomeTeam.LoadPlayerData(Me.mCurrentGame)
            End If

            ' update game label
            Me.SetGameTitle()

            'update innings datatable
            Me.UpdateInnings()

            ' clear inning label
            Me.mCurrentGame.Innings.Columns(0).ColumnName = " "

            ' update team logos
            Me.LoadTeamLogos()

            ' load weather
            Me.lblWeather.Text = Me.mCurrentGame.VenueWeather()

            ' set game status
            Dim status As String = Me.mCurrentGame.GameStatus
            Logger.Debug($"Game status is {status}")

            ' set screen controls for future game
            If MlbGame.CheckGameStatus(status) = MlbGame.mGAME_STATUS_FUTURE Then
                tbxCommentary.Visible = False
                dgvAwayLineup.Visible = True
                dgvHomeLineup.Visible = True
                lblAwayLineup.Visible = True
                lblHomeLineup.Visible = True
                lblWeather.Visible = True
                lblBalls.Visible = False
                lblStrikes.Visible = False
                lblOuts.Visible = False
                lblPitchCount.Visible = False
                imgDiamond.Visible = True
                imgDiamond.Image = My.Resources.diamond
                lblAwayWinnerLoser.Visible = False
                lblHomeWinnerLoser.Visible = False

                ' turn on/off menu items
                Me.PlayRecapToolStripMenuItem.Enabled = False
                Me.BroadcastsToolStripMenuItem.Enabled = True
                Me.BoxscoreToolStripMenuItem.Enabled = False

                ' load team roster
                Me.LoadTeamRosterGrids()

                ' load pitcher match up
                lblMatchupPitcher.Text = Me.mCurrentGame.GetPitchingMatchup()
                lblMatchupPitcher.Visible = True

                ' stop game update timer
                Me.GameUpdateTimer.Stop()

                ' set screen controls for past game
            ElseIf MlbGame.CheckGameStatus(status) = MlbGame.mGAME_STATUS_PAST Then
                tbxCommentary.Visible = True
                lblBalls.Visible = False
                lblStrikes.Visible = False
                lblOuts.Visible = False
                lblPitchCount.Visible = False
                imgDiamond.Visible = True
                imgDiamond.Image = My.Resources.diamond
                dgvAwayLineup.Visible = True
                dgvHomeLineup.Visible = True
                lblAwayLineup.Visible = True
                lblHomeLineup.Visible = True
                lblWeather.Visible = False
                lblAwayWinnerLoser.Visible = True
                lblHomeWinnerLoser.Visible = True
                lblMatchupPitcher.Visible = False

                ' turn on/off menu items
                Me.PlayRecapToolStripMenuItem.Enabled = True
                Me.BroadcastsToolStripMenuItem.Enabled = False
                Me.BoxscoreToolStripMenuItem.Enabled = True

                ' load winning - losing pitchers
                tbxCommentary.Text = UpdateWinnerLoserPitchers()

                ' set winner - loser labels on logos
                If mCurrentGame.WinningTeam.Abbr = mCurrentGame.AwayTeam.Abbr Then
                    lblAwayWinnerLoser.Text = "Winner"
                    lblHomeWinnerLoser.Text = "Loser"
                Else
                    lblAwayWinnerLoser.Text = "Loser"
                    lblHomeWinnerLoser.Text = "Winner"
                End If
                lblAwayWinnerLoser.Visible = True
                lblHomeWinnerLoser.Visible = True

                ' fill X in unplayed bottom of last inning
                Dim lastInning = Me.mCurrentGame.Innings.Rows(1).Item(Me.mCurrentGame.Innings.Columns.Count - 4).ToString()
                If lastInning = " " Then
                    Me.mCurrentGame.Innings.Rows(1).Item(Me.mCurrentGame.Innings.Columns.Count - 4) = "X"
                End If

                ' deselect any selected cell or row
                dgvInnings.ClearSelection()

                ' set final game info in commentary box
                Me.tbxCommentary.Text += $"{vbCr}{vbCr}{GetFinalGameStats()}"

                ' load team rosters
                Me.LoadTeamRosterGrids()

                ' stop game update timer
                Me.GameUpdateTimer.Stop()

                ' set screen controls for current game
            ElseIf MlbGame.CheckGameStatus(status) = MlbGame.mGAME_STATUS_PRESENT Then
                tbxCommentary.Visible = True
                lblBalls.Visible = True
                lblStrikes.Visible = True
                lblOuts.Visible = True
                lblPitchCount.Visible = True
                imgDiamond.Visible = True
                imgDiamond.Image = My.Resources.diamond
                dgvAwayLineup.Visible = True
                dgvHomeLineup.Visible = True
                lblAwayLineup.Visible = True
                lblHomeLineup.Visible = True
                lblWeather.Visible = True
                lblHomeWinnerLoser.Visible = False
                lblAwayWinnerLoser.Visible = False

                ' turn on/off menu items
                Me.PlayRecapToolStripMenuItem.Enabled = True
                Me.BroadcastsToolStripMenuItem.Enabled = True
                Me.BoxscoreToolStripMenuItem.Enabled = True

                ' update inning label in inning table
                If Me.mCurrentGame.Innings.Columns.Count > 1 Then
                    Dim inningLabel As String
                    If Me.mCurrentGame.CurrentInningState = "MIDDLE" Or Me.mCurrentGame.CurrentInningState = "END" Then
                        inningLabel = $"{Me.mCurrentGame.CurrentInningState} {Me.mCurrentGame.CurrentInning}"
                    Else
                        inningLabel = $"{Me.mCurrentGame.CurrentInningHalf} {Me.mCurrentGame.CurrentInning}"
                    End If
                    Me.mCurrentGame.Innings.Columns(0).ColumnName = inningLabel
                End If

                ' highlight the current inning
                Me.HighlightCurrentInning()

                ' update Balls, Strikes, Outs
                Me.UpdateBSO()

                ' update pitch count
                Me.lblPitchCount.Text = $"Pitch Count: {Me.mCurrentGame.PitchCount()}"

                ' update pitcher-batter matchup
                Dim matchup As String = Me.mCurrentGame.GetPitcherBatterMatchup()
                Dim pitcher As String = matchup.Substring(0, matchup.IndexOf(" - "))
                Dim batter As String = matchup.Substring(matchup.IndexOf(" - ") + 3, matchup.Length - pitcher.Length - 3)
                lblMatchupPitcher.Text = pitcher
                lblMatchupBatter.Text = batter

                ' update last play
                Me.UpdatePlayCommentary()

                'update scorebook entry
                Dim scorebook As String = Me.mCurrentGame.LastPlayScorebookEntry()
                If scorebook.Length > 0 Then
                    Me.tbxCommentary.Text += $"  (Scorebook: {scorebook})"
                End If

                'update base runners image
                Me.UpdateBaseRunners()

                ' load team lineups
                Me.LoadTeamLineupGrids()

                ' if mid inning or end inning show due ups
                If Me.mCurrentGame.CurrentInningState() = "END" Or Me.mCurrentGame.CurrentInningState() = "MIDDLE" Then
                    Me.tbxCommentary.Text += Me.mCurrentGame.GetDueUpBatters()
                End If
            Else
                Logger.Warn($"Unknown game state: {status}")
            End If

            ' redraw all controls
            Me.Refresh()

            'Logger.Debug(Me.mCurrentGame.ToString())

        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Private Sub ResetScreenControls()
        lblAwayLineup.Text = "Away Roster"
        lblAwayLineup.Visible = False
        lblHomeLineup.Text = "Home Roster"
        lblHomeLineup.Visible = False
        lblBalls.Text = "Balls: 0"
        lblBalls.Visible = True
        lblStrikes.Text = "Strikes: 0"
        lblStrikes.Visible = True
        lblOuts.Text = "Outs: 0"
        lblOuts.Visible = True
        lblPitchCount.Text = "Pitch Count: 0"
        lblPitchCount.Visible = True
        lblGameTitle.Text = "Away @ Home - mm/dd/yyyy (gamePk)"
        lblGameTitle.Visible = True
        lblMatchupPitcher.Text = "Match up"
        lblMatchupPitcher.Visible = True
        imgDiamond.Image = My.Resources.diamond
        imgDiamond.Visible = True
        imgAwayLogo.Image = My.Resources.MLB
        imgAwayLogo.Visible = True
        imgHomeLogo.Image = My.Resources.MLB
        imgHomeLogo.Visible = True
        tbxCommentary.Text = ""
        tbxCommentary.Visible = False
        dgvAwayLineup.DataSource = Nothing
        dgvHomeLineup.DataSource = Nothing
        dgvInnings.DataSource = InitBlankInningsTable()
        lblWeather.Text = "Weather"
        lblWeather.Visible = True
        lblAwayWinnerLoser.Text = "Winner-Loser"
        lblAwayWinnerLoser.Visible = True
        lblHomeWinnerLoser.Text = "Winner-Loser"
        lblHomeWinnerLoser.Visible = True
        lblGamePk.Text = "GamePk"
        lblGamePk.Visible = True
        lblStatus.Text = "Game Status"
        lblStatus.Visible = True
        Me.mCurrentGame = Nothing
        dgvGames.DataSource = Nothing
        BoxscoreToolStripMenuItem.Enabled = False
        PlayRecapToolStripMenuItem.Enabled = False
        StandingsToolStripMenuItem.Enabled = True

    End Sub

    Private Function InitBlankInningsTable() As DataTable
        Dim dt As DataTable = New DataTable("Innings")
        Dim dc As DataColumn = New DataColumn(" ")

        ' add a blank column
        dt.Columns.Add(dc)

        ' add inning columns
        For i = 1 To 9
            dc = New DataColumn()
            dc.ColumnName = i
            dt.Columns.Add(dc)
        Next

        ' add RHE columns
        dc = New DataColumn("R")
        dt.Columns.Add(dc)
        dc = New DataColumn("H")
        dt.Columns.Add(dc)
        dc = New DataColumn("E")
        dt.Columns.Add(dc)

        ' add rows for away and home scores
        dt.Rows.Add(dt.NewRow())
        dt.Rows.Add(dt.NewRow())

        Return dt
    End Function

    Private Function GetFinalGameStats() As String
        Dim sb As New StringBuilder

        Try
            ' get final data
            Dim attendance As String = Me.mCurrentGame.GameData.SelectToken("gameInfo.attendance")
            Dim duration As String = Me.mCurrentGame.GameData.SelectToken("gameInfo.gameDurationMinutes")
            Dim delay As String = Me.mCurrentGame.GameData.SelectToken("gameInfo.delayDurationMinutes")

            ' format attendance
            If attendance IsNot Nothing Then
                If attendance.Length > 0 Then
                    Dim a As Integer = Integer.Parse(attendance)
                    sb.Append($"Attendance: {a.ToString("#,###")}")
                End If
            End If

            ' format duration
            If duration IsNot Nothing Then
                If duration.Length > 0 Then
                    Dim d As Integer = Integer.Parse(duration)
                    Dim ts As New TimeSpan(0, d, 0)
                    sb.Append($"{vbCr}Game duration: {ts.Hours}:{ts.Minutes.ToString("00")}")
                End If
            End If

            ' format delay
            If delay IsNot Nothing Then
                If delay.Length > 0 Then
                    Dim d As Integer = Integer.Parse(delay)
                    Dim ts As New TimeSpan(0, d, 0)
                    sb.Append($"{vbCr}Delayed start: {ts.Hours}:{ts.Minutes.ToString("00")}")
                End If
            End If
        Catch ex As Exception
            Logger.Error(ex)
        End Try

        ' return final game stats
        Return sb.ToString

    End Function
    Private Sub SetGameTitle()
        Try
            ' set game title
            Me.lblGameTitle.Text = Me.mCurrentGame.GameTitleDate()
            Me.lblGamePk.Text = "Game Id: " + Me.mCurrentGame.GamePk.ToString()
            Me.lblStatus.Text = "Game Status: " + Me.mCurrentGame.GameStatus
        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Private Sub UpdatePlayCommentary()
        Try
            If Me.mCurrentGame.LiveData IsNot Nothing Then

                ' if not between innings, include pitch in play description
                Dim pitch = ""
                If Me.mCurrentGame.CurrentInningState <> "MIDDLE" Or Me.mCurrentGame.CurrentInningState <> "END" Then
                    pitch = Me.mCurrentGame.GetLastPitchDescription()
                End If

                ' get last play commentary
                Dim play = Me.mCurrentGame.GetLastPlayDescription()

                ' assign text to control
                If pitch = String.Empty Then
                    Me.tbxCommentary.Text = play
                Else
                    Me.tbxCommentary.Text = pitch + vbCr + vbCr + play
                End If
            End If
        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Private Sub LoadTeamLogos()
        Try
            Me.imgAwayLogo.Image = My.Resources.ResourceManager().GetObject(Me.mCurrentGame.AwayTeam.Abbr)
            Me.imgHomeLogo.Image = My.Resources.ResourceManager().GetObject(Me.mCurrentGame.HomeTeam.Abbr)
        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Private Sub UpdateBSO()
        Try
            Me.lblBalls.Text = $"Balls:    {Me.mCurrentGame.Balls()}"
            Me.lblStrikes.Text = $"Strikes: {Me.mCurrentGame.Strikes()}"
            Me.lblOuts.Text = $"Outs:    {Me.mCurrentGame.Outs()}"
        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Private Sub UpdateInnings()
        Try
            ' clears columns so extra innings show correctly
            dgvInnings.Columns.Clear()
            dgvInnings.DataSource = Me.mCurrentGame.Innings

            ' patch up the grid view
            dgvInnings.ClearSelection()
            dgvInnings.ColumnHeadersDefaultCellStyle.Font = New Font(dgvInnings.DefaultFont, FontStyle.Bold)
        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Private Function UpdateWinnerLoserPitchers() As String

        Dim winLoseLine = String.Empty
        Try
            ' get pitchers names
            Dim winnerName As String = Me.mCurrentGame.WinningTeam.GetPlayer(Me.mCurrentGame.WinningPitcherId).FullName()
            Dim loserName As String = Me.mCurrentGame.LosingTeam.GetPlayer(Me.mCurrentGame.LosingPitcherId).FullName()
            Dim saveName As String = String.Empty
            If Me.mCurrentGame.SavePitcherId IsNot Nothing Then
                saveName = Me.mCurrentGame.WinningTeam.GetPlayer(Me.mCurrentGame.SavePitcherId).FullName()
            End If

            ' get pitcher stats
            Dim winnerStats As String = Me.mCurrentGame.GetPitchingStats(Me.mCurrentGame.WinningPitcherId, Me.mCurrentGame.WinningTeam)
            Dim loserStats As String = Me.mCurrentGame.GetPitchingStats(Me.mCurrentGame.LosingPitcherId, Me.mCurrentGame.LosingTeam)
            Dim saveStats As String = String.Empty
            If Not saveName.Equals(String.Empty) Then
                saveStats = Me.mCurrentGame.GetPitchingStats(Me.mCurrentGame.SavePitcherId, Me.mCurrentGame.WinningTeam)
            End If

            ' set control text
            If Not saveName.Equals(String.Empty) Then
                winLoseLine = $"Winner: {winnerName} {winnerStats}{vbCr}Loser: {loserName} {loserStats}{vbCr}Save: {saveName} {saveStats}"
            Else
                winLoseLine = $"Winner: {winnerName} {winnerStats}{vbCr}Loser: {loserName} {loserStats}"
            End If
        Catch ex As Exception
            Logger.Error(ex)
        End Try
        Return winLoseLine
    End Function

    Private Sub UpdateBaseRunners()
        Dim first As Boolean = False
        Dim second As Boolean = False
        Dim third As Boolean = False

        Try
            Dim offenseData As JObject = Me.mCurrentGame.LineScoreData.SelectToken("offense")

            If offenseData.ContainsKey("first") Then
                first = True
            End If

            If offenseData.ContainsKey("second") Then
                second = True
            End If

            If offenseData.ContainsKey("third") Then
                third = True
            End If

            ' assign proper diamond image according to base runners
            If first = False And second = False And third = False Then
                Me.imgDiamond.Image = My.Resources.diamond
            ElseIf first = True And second = False And third = False Then
                Me.imgDiamond.Image = My.Resources.diamond1
            ElseIf first = False And second = True And third = False Then
                Me.imgDiamond.Image = My.Resources.diamond2
            ElseIf first = False And second = False And third = True Then
                Me.imgDiamond.Image = My.Resources.diamond3
            ElseIf first = True And second = True And third = False Then
                Me.imgDiamond.Image = My.Resources.diamond1_2
            ElseIf first = False And second = True And third = True Then
                Me.imgDiamond.Image = My.Resources.diamond2_3
            ElseIf first = True And second = False And third = True Then
                Me.imgDiamond.Image = My.Resources.diamond1_3
            ElseIf first = True And second = True And third = True Then
                Me.imgDiamond.Image = My.Resources.diamond1_2_3
            Else
                Me.imgDiamond.Image = My.Resources.diamond
            End If
        Catch ex As Exception
            Logger.Error(ex)
        End Try

    End Sub

    Private Sub LoadTeamLineupGrids()
        Try
            ' load lineups from team objects
            dgvAwayLineup.DataSource = Me.mCurrentGame.AwayTeam.GetLinupTable()
            dgvHomeLineup.DataSource = Me.mCurrentGame.HomeTeam.GetLinupTable()

            ' set label text
            lblAwayLineup.Text = Me.mCurrentGame.AwayTeam.ShortName() + " Lineup"
            lblHomeLineup.Text = Me.mCurrentGame.HomeTeam.ShortName() + " Lineup"

            ' format grids
            dgvAwayLineup.ColumnHeadersDefaultCellStyle.Font = New Font(dgvAwayLineup.DefaultFont, FontStyle.Bold)
            dgvHomeLineup.ColumnHeadersDefaultCellStyle.Font = New Font(dgvHomeLineup.DefaultFont, FontStyle.Bold)

            ' hid unnecessary columns
            dgvAwayLineup.Columns("Id").Visible = False
            dgvAwayLineup.Columns("BattingOrder").Visible = False
            dgvHomeLineup.Columns("Id").Visible = False
            dgvHomeLineup.Columns("BattingOrder").Visible = False

            dgvAwayLineup.ClearSelection()
            dgvHomeLineup.ClearSelection()
        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Private Sub LoadTeamRosterGrids()
        Try
            ' load rosters from team objects
            dgvAwayLineup.DataSource = Me.mCurrentGame.AwayTeam.GetRosterTable()
            dgvHomeLineup.DataSource = Me.mCurrentGame.HomeTeam.GetRosterTable()

            ' set label test
            lblAwayLineup.Text = Me.mCurrentGame.AwayTeam.ShortName() + " Roster"
            lblHomeLineup.Text = Me.mCurrentGame.HomeTeam.ShortName() + " Roster"

            ' format grids
            dgvAwayLineup.ColumnHeadersDefaultCellStyle.Font = New Font(dgvAwayLineup.DefaultFont, FontStyle.Bold)
            dgvHomeLineup.ColumnHeadersDefaultCellStyle.Font = New Font(dgvHomeLineup.DefaultFont, FontStyle.Bold)

            ' hide unnecesary columns
            dgvAwayLineup.Columns("Id").Visible = False
            dgvHomeLineup.Columns("Id").Visible = False

            dgvAwayLineup.ClearSelection()
            dgvHomeLineup.ClearSelection()
        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub


    Function LoadAllGamesData(gameDate As String) As Dictionary(Of String, MlbGame)
        ' dict of GamePk and game object
        Dim ListOfGames As Dictionary(Of String, MlbGame) = New Dictionary(Of String, MlbGame)

        ' reset progress bar
        Me.AllGamesUpdateProgressBar.Visible = True
        Me.AllGamesUpdateProgressBar.Value = 0

        Try
            ' get data
            Logger.Debug($"Loading games for {gameDate}")
            Dim schedule As JObject = Me.mAPI.ReturnScheduleData(gameDate)
            Dim gameDates As JArray = schedule.SelectToken("dates")

            For Each gDate As JObject In gameDates
                Dim games As JArray = gDate.SelectToken("games")

                ' set progress bar max to number of games
                Me.AllGamesUpdateProgressBar.Maximum = games.Count

                For Each game As JObject In games

                    ' create new game object and add to list.  This will have the side effect
                    ' of loading team and player data also
                    Dim gamePk As String = game.SelectToken("gamePk")
                    Dim oGame As MlbGame = New MlbGame(gamePk)
                    ListOfGames.Add(gamePk, oGame)
                    Logger.Debug($"Loaded game {oGame.GamePk} - {oGame.AwayTeam.Abbr}@{oGame.HomeTeam.Abbr}")

                    ' update status bar also
                    Me.AllGamesUpdateData.Text = $"Loading game {oGame.GamePk} {oGame.AwayTeam.Abbr} @ {oGame.HomeTeam.Abbr} data...  "
                    Me.AllGamesUpdateProgressBar.PerformStep()
                    Me.StatusStrip1.Refresh()
                Next
            Next

        Catch ex As Exception
            Logger.Error(ex)
        End Try

        ' hide progress bar
        Me.AllGamesUpdateProgressBar.Visible = False

        Return ListOfGames
    End Function

    Private Function FindTeamGame(teamAbbrev As String) As MlbGame
        Try
            Logger.Debug($"Looking for {teamAbbrev} game...")
            ' find game object for team name
            For Each game In mAllGames.Values()
                If game.AwayTeam.Abbr = teamAbbrev Or game.HomeTeam.Abbr = teamAbbrev Then
                    Logger.Debug($"Found {game.GamePk}")
                    Return game
                End If
            Next
        Catch ex As Exception
            Logger.Error(ex)
        End Try
        Logger.Debug($"Did not find game for {teamAbbrev}")
        Return Nothing
    End Function

    Private Sub SetCurrentGame()
        Try
            Dim FaveTeam As String = mProperties.GetProperty(mProperties.mFAVORITE_TEAM_KEY, "")
            Logger.Debug($"Favorite team = {FaveTeam}")

            If Not FaveTeam = String.Empty Then
                Me.mCurrentGame = FindTeamGame(FaveTeam)
            End If

            ' if no favorite team or team isn't playing today, pick first game in list
            If Me.mCurrentGame Is Nothing Then
                If Me.mAllGames.Count > 0 Then
                    Me.mCurrentGame = Me.mAllGames(Me.mAllGames.Keys(0))
                    Logger.Debug($"Using {Me.mCurrentGame.GamePk} as current game")
                End If
            End If
        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Sub HighlightCurrentInning()
        Dim row As Integer

        Try
            Dim inning As Integer = Me.mCurrentGame.CurrentInning()
            Dim inningHalf As String = Me.mCurrentGame.CurrentInningHalf()

            If inningHalf = "TOP" Then
                row = 0
            Else
                row = 1
            End If

            dgvInnings.Rows(row).Cells(inning).Style.BackColor = Color.LightBlue
        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Private Sub LoadAllGamesDataGrid()
        Dim dt As DataTable = New DataTable()

        Try
            dt.Columns.Add("Id")
            dt.Columns.Add("Away")
            dt.Columns.Add("Score")
            dt.Columns.Add("Home")
            dt.Columns.Add("Status")
            dt.Columns.Add("Inning")

            For Each game As MlbGame In Me.mAllGames.Values()
                Dim row As DataRow = dt.NewRow()
                row("Id") = game.GamePk
                row("Away") = $"{game.AwayTeam.Abbr} ({game.AwayTeam.Wins} - {game.AwayTeam.Loses})"
                row("Home") = $"{game.HomeTeam.Abbr} ({game.HomeTeam.Wins} - {game.HomeTeam.Loses})"
                row("Status") = game.GameStatus
                row("Score") = $"{game.AwayTeamRuns} - {game.HomeTeamRuns}"

                ' fill Inning column according to game status
                If MlbGame.CheckGameStatus(game.GameStatus) = MlbGame.mGAME_STATUS_PAST Then
                    If game.Innings.Columns.Count > 13 Then  ' account for team name column and RHE appended to end
                        row("Inning") = game.Innings.Columns.Count - 4
                    Else
                        row("Inning") = ""
                    End If
                ElseIf MlbGame.CheckGameStatus(game.GameStatus) = MlbGame.mGAME_STATUS_FUTURE Then
                    row("Inning") = ""
                Else
                    If game.CurrentInningState().ToUpper() = "MIDDLE" Or game.CurrentInningState().ToUpper() = "END" Then
                        row("Inning") = $"{game.CurrentInningState()} {game.CurrentInning().ToString()}"
                    Else
                        row("Inning") = $"{game.CurrentInningHalf()} {game.CurrentInning().ToString()}"
                    End If
                End If

                dt.Rows.Add(row)
            Next

            ' hide id column and sort by id
            dgvGames.DataSource = dt
            dgvGames.Columns("Id").Visible = False
            dgvGames.ColumnHeadersDefaultCellStyle.Font = New Font(dgvGames.DefaultFont, FontStyle.Bold)
            dgvGames.ClearSelection()

            Me.AllGamesUpdateProgressBar.Visible = False

        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Private Sub dgvGames_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvGames.CellClick

        Try
            ' bail if header row clicked
            If e.RowIndex < 0 Then
                Return
            End If

            ' get the gamePk for the clicked row
            Dim id As String = dgvGames.Rows(e.RowIndex).Cells("Id").Value.ToString
            Logger.Debug($"Clicked row {e.RowIndex}, GamePk = {id}")

            ' find selected game and make it current
            Me.mCurrentGame = Me.mAllGames(id)

            ' reset the game update time when new game selected
            Me.GameUpdateTimer.Stop()
            Me.GameUpdateTimer.Start()

            ' force repaint which should highlight current selected game
            dgvGames.ClearSelection()
            dgvGames.InvalidateRow(e.RowIndex)

            ' disable menu items not applicable to future games
            If MlbGame.CheckGameStatus(Me.mCurrentGame.GameStatus) = MlbGame.mGAME_STATUS_FUTURE Then
                PlayRecapToolStripMenuItem.Enabled = False
                BoxscoreToolStripMenuItem.Enabled = False
            Else
                PlayRecapToolStripMenuItem.Enabled = True
                BoxscoreToolStripMenuItem.Enabled = True
            End If

            ' run the selected game
            Me.RunGame()
        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Private Sub ScoreboardUpdateTimer_Tick(sender As Object, e As EventArgs) Handles ScoreboardUpdateTimer.Tick
        Logger.Info($"Scoreboard timer tick called {DateTime.Now()}")
        Dim gameDate As DateTime = DateTime.Parse(Me.calDatePicker.Value.ToString("MM/dd/yyyy"))
        Dim today As DateTime = DateTime.Today().Date()

        ' if any date other than today is selected, do not update (data won't change)
        If Not gameDate.Equals(today) Then
            Return
        End If

        Me.mAllGames.Clear() ' clear game cache so it is reloaded
        Me.RunScoreboards()
    End Sub

    Private Sub dgvGames_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvGames.CellPainting

        ' if no current game selected, bail out
        If Me.mCurrentGame Is Nothing Then
            Return
        End If

        Try
            ' highlight selected game
            For Each row As DataGridViewRow In dgvGames.Rows
                Dim id As Integer = Convert.ToInt32(row.Cells("Id").Value)
                If id = Me.mCurrentGame.GamePk Then
                    row.DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    row.DefaultCellStyle.BackColor = Color.White
                End If
            Next
        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Private Sub dgvAwayRoster_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvAwayLineup.CellPainting

        If Me.mCurrentGame Is Nothing Or MlbGame.CheckGameStatus(Me.mCurrentGame.GameStatus) <> MlbGame.mGAME_STATUS_PRESENT Then
            Return
        End If

        ' highlight pitcher and batter in line up grids
        Try
            Dim pitcherId As String = Me.mCurrentGame.GetCurrentPitcherId()
            Dim batterId As String = Me.mCurrentGame.GetCurrentBatterId()

            Me.LineupDGVCellHighlight(pitcherId, batterId, sender)
        Catch ex As Exception
            Logger.Error(ex)
        End Try

    End Sub

    Private Sub dgvHomeRoster_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvHomeLineup.CellPainting

        If Me.mCurrentGame Is Nothing Or MlbGame.CheckGameStatus(Me.mCurrentGame.GameStatus) <> MlbGame.mGAME_STATUS_PRESENT Then
            Return
        End If

        ' highlight pitcher and batter in line up grids
        Try
            Dim pitcherId As String = Me.mCurrentGame.GetCurrentPitcherId()
            Dim batterId As String = Me.mCurrentGame.GetCurrentBatterId()

            Me.LineupDGVCellHighlight(pitcherId, batterId, sender)
        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Private Sub LineupDGVCellHighlight(pitcherId As String, batterId As String, LineUpDGV As DataGridView)

        ' single function for highlighting rows in away and home line up data grids
        Try
            For Each row As DataGridViewRow In LineUpDGV.Rows
                If pitcherId = row.Cells(0).Value.ToString Or batterId = row.Cells(0).Value.ToString Then

                    ' if row scrolled off screen, make sure it is visible
                    EnsureRowVisible(LineUpDGV, row.Index)
                    row.DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    row.DefaultCellStyle.BackColor = Color.White
                End If
            Next
        Catch ex As Exception
            Logger.Error(ex)
        End Try

    End Sub

    Private Sub EnsureRowVisible(view As DataGridView, rowIdx As Integer)

        ' make sure the highlighted row is visible
        Try
            If rowIdx >= 0 And rowIdx < view.RowCount Then
                Dim countVisible As Integer = view.DisplayedRowCount(False)
                Dim firstVisible As Integer = view.FirstDisplayedScrollingRowIndex
                If rowIdx < firstVisible Then
                    view.FirstDisplayedScrollingRowIndex = rowIdx
                ElseIf rowIdx >= firstVisible + countVisible Then
                    view.FirstDisplayedScrollingRowIndex = rowIdx - countVisible + 1
                End If
            End If
        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Private Sub QuitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QuitToolStripMenuItem.Click
        Logger.Info("*** Application Ended by User")
        Application.Exit()
    End Sub

    Private Sub ConfigureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfigureToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            ' show config form
            Dim frmConfig = New SBConfigure
            frmConfig.ShowDialog()
            Me.Cursor = Cursors.Default

            ' reset timer if value changed in dialog
            SetScoreboardTimerInterval(Convert.ToInt32(mProperties.GetProperty(mProperties.mGAME_TIMER_KEY, "60")))
            SetGameUpdateTimerInterval(Convert.ToInt32(mProperties.GetProperty(mProperties.mGAME_TIMER_KEY, "20")), True)

        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem1.Click
        Dim frmAbout = AboutBox
        frmAbout.ShowDialog()
    End Sub

    Private Sub SetScoreboardTimerInterval(interval As Integer)
        ' set scoreboard timer to interval value and restart it
        ScoreboardUpdateTimer.Stop()
        ScoreboardUpdateTimer.Interval = interval * 1000
        ScoreboardUpdateTimer.Start()
        Logger.Debug($"Scoreboard timer interval set to {interval}")
    End Sub

    Private Sub SetGameUpdateTimerInterval(interval As Integer, start As Boolean)

        ' set game timer to interval value and optionaly restart it
        GameUpdateTimer.Stop()
        GameUpdateTimer.Interval = interval * 1000
        If start Then
            GameUpdateTimer.Start()
        End If
        Logger.Debug($"Game timer interval set to {interval}")
    End Sub

    Private Sub GameUpdateTimer_Tick(sender As Object, e As EventArgs) Handles GameUpdateTimer.Tick
        Logger.Info($"Game timer tick called {DateTime.Now()}")
        ' run the game, which will force an update
        Me.RunGame()
    End Sub

    Private Sub dgvAwayLineup_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAwayLineup.CellClick
        Try
            Dim PlayerId As String = dgvAwayLineup.Rows(e.RowIndex).Cells("Id").Value
            Dim ThisPlayer As MlbPlayer = Me.mCurrentGame.AwayTeam.GetPlayer(PlayerId)
            Logger.Debug($"Clicked AWAY row {e.RowIndex}, player = {PlayerId}")
            ShowPlayerStats(ThisPlayer, "AWAY")
        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Private Sub dgvHomeLineup_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvHomeLineup.CellClick
        Try
            Dim PlayerId As String = dgvHomeLineup.Rows(e.RowIndex).Cells("Id").Value
            Dim ThisPlayer As MlbPlayer = Me.mCurrentGame.HomeTeam.GetPlayer(PlayerId)
            Logger.Debug($"Clicked HOME row {e.RowIndex}, player = {PlayerId}")
            ShowPlayerStats(ThisPlayer, "HOME")
        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Private Sub ShowPlayerStats(ThisPlayer As MlbPlayer, TeamSide As String)
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim frmPlayerStats As MlbPlayerStats = New MlbPlayerStats()
            frmPlayerStats.Player = ThisPlayer
            frmPlayerStats.Game = Me.mCurrentGame
            frmPlayerStats.AwayOrHome = TeamSide
            frmPlayerStats.Show()
        Catch ex As Exception
            Logger.Error(ex)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub PlayRecapToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PlayRecapToolStripMenuItem.Click
        Try
            If Me.mCurrentGame Is Nothing Or MlbGame.CheckGameStatus(Me.mCurrentGame.GameStatus) = MlbGame.mGAME_STATUS_FUTURE Then
                Return
            End If
            Me.Cursor = Cursors.WaitCursor
            Dim frmPlaySummary = New MlbPlaySummary()
            frmPlaySummary.Game = Me.mCurrentGame
            frmPlaySummary.ShowDialog()
        Catch ex As Exception
            Logger.Error(ex)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub StandingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StandingsToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim year As String = DateTime.Parse(Me.calDatePicker.Text).Year.ToString()
            Dim frmStandings = New MlbStandings()
            frmStandings.Year = year
            frmStandings.ShowDialog()
        Catch ex As Exception
            Logger.Error(ex)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub BoxscoreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BoxscoreToolStripMenuItem.Click
        Try
            Me.Cursor = Cursors.WaitCursor
            Dim frmBoxscore As New MlbBoxscore()
            frmBoxscore.Game = Me.mCurrentGame
            frmBoxscore.ShowDialog()
        Catch ex As Exception
            Logger.Error(ex)
        End Try
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub btnFindGames_Click(sender As Object, e As EventArgs) Handles btnFindGames.Click
        Try
            Me.GameUpdateTimer.Stop()
            Me.mCurrentGame = Nothing
            Me.ResetScreenControls()
            Me.Refresh()
            Me.ThisGameUpdateData.Text = ""
            Me.mAllGames.Clear()
            Me.RunScoreboards()
        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Private Sub BroadcastsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BroadcastsToolStripMenuItem.Click
        Try
            Dim frmBroadcasts As MlbBroadcasters = New MlbBroadcasters
            frmBroadcasts.Game = Me.mCurrentGame
            frmBroadcasts.ShowDialog()
        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub



    ' ===============================================
    ' Experimental code to use team logos and graphics in the list of games


    'Imports System.Drawing.Imaging
    'Imports System.IO

    'Public Class Form1
    '    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load


    '        'Dim img As Image = Image.FromStream(createBitmap())

    '        Dim col1 As New DataGridViewTextBoxColumn()
    '        col1.Name = "Id"
    '        Me.DataGridView1.Columns.Add(col1)

    '        Dim col2 As New DataGridViewImageColumn()
    '        col2.Name = "Content"
    '        Me.DataGridView1.Columns.Add(col2)

    '        Me.DataGridView1.Rows.Add()
    '        Me.DataGridView1.Rows(0).Cells(0).Value = "1"
    '        Me.DataGridView1.Rows(0).Cells(1).Value = Image.FromStream(createBitmap("Mets", "0", "Marlins", "3", "IN PROGRESS", "TOP 2"))

    '        Me.DataGridView1.Rows.Add()
    '        Me.DataGridView1.Rows(1).Cells(0).Value = "2"
    '        Me.DataGridView1.Rows(1).Cells(1).Value = Image.FromStream(createBitmap("Nationals", "10", "Reds", "3", "IN PROGRESS", "BOTTOM 5"))

    '        Me.DataGridView1.Rows.Add()
    '        Me.DataGridView1.Rows(2).Cells(0).Value = "3"
    '        Me.DataGridView1.Rows(2).Cells(1).Value = Image.FromStream(createBitmap("Yankess", "", "Orioles", "", "SCHEDULED", "10:05pm"))

    '        Me.DataGridView1.Rows.Add()
    '        Me.DataGridView1.Rows(3).Cells(0).Value = "4"
    '        Me.DataGridView1.Rows(3).Cells(1).Value = Image.FromStream(createBitmap("Dodgers", "10", "Giants", "5", "IN PROGRESS", "TOP 12"))

    '        Me.DataGridView1.Columns("Id").Visible = False
    '    End Sub

    '    Function createBitmap(AwayTeam As String, AwayRuns As String, HomeTeam As String, HomeRuns As String, GameStatus As String, Inning As String) As MemoryStream
    '        Dim TotalWidth = 360
    '        Dim TotalHeight = 150
    '        Dim LogoWidth = 60
    '        Dim LogoHeight = 60
    '        Dim MarginOffset = 2
    '        Dim TeamNameX = LogoWidth + 10
    '        Dim ScoreX = Convert.ToInt16(3 * (TotalWidth / 4))

    '        Dim AwayLineY = MarginOffset
    '        Dim HomeLineY = LogoHeight + (3 * MarginOffset)

    '        Dim AwayNameY = (LogoHeight + MarginOffset) / 2
    '        Dim HomeNameY = LogoHeight + (LogoHeight + MarginOffset) / 2

    '        Dim AwayScoreY = AwayLineY + 12
    '        Dim HomeScoreY = HomeLineY + 12

    '        Dim GameStatusX = 10
    '        Dim GameStatusY = TotalHeight - 20

    '        Dim DividerLineY = TotalHeight - 1

    '        ' setup graphics objects
    '        Dim bitmap As New Bitmap(TotalWidth, TotalHeight)
    '        Dim graphic As Graphics = Graphics.FromImage(bitmap)
    '        Dim whiteBrush As New SolidBrush(Color.White)
    '        Dim blackBrush As New SolidBrush(Color.Black)
    '        Dim blackPen As New Pen(blackBrush)

    '        ' create border
    '        graphic.FillRectangle(whiteBrush, 0, 0, bitmap.Width, bitmap.Height)

    '        ' set font and embedded image
    '        Dim font12 As New Font("Arial", 12)
    '        Dim font24 As New Font("Arial", 24)
    '        Dim awayLogo As New Bitmap(My.Resources.test)
    '        Dim homeLogo As New Bitmap(My.Resources.test)
    '        Dim ScoreXOffset = 0

    '        ' away team
    '        graphic.DrawImage(awayLogo, MarginOffset, AwayLineY, LogoWidth, LogoHeight)
    '        graphic.DrawString(AwayTeam, font12, blackBrush, TeamNameX, AwayNameY)

    '        If Not AwayRuns = String.Empty Then
    '            graphic.DrawRectangle(blackPen, ScoreX, AwayLineY, LogoWidth, LogoHeight)

    '            If Convert.ToInt16(AwayRuns) < 10 Then
    '                ScoreXOffset = 15
    '            Else
    '                ScoreXOffset = 5
    '            End If
    '            graphic.DrawString(AwayRuns, font24, blackBrush, ScoreX + ScoreXOffset, AwayScoreY)
    '        End If


    '        ' home team
    '        graphic.DrawImage(homeLogo, MarginOffset, HomeLineY, LogoWidth, LogoHeight)
    '        graphic.DrawString(HomeTeam, font12, blackBrush, TeamNameX, HomeNameY)
    '        If Not HomeRuns = String.Empty Then
    '            graphic.DrawRectangle(blackPen, ScoreX, HomeLineY, LogoWidth, LogoHeight)
    '            If Convert.ToInt16(HomeRuns) < 10 Then
    '                ScoreXOffset = 15
    '            Else
    '                ScoreXOffset = 5
    '            End If
    '            graphic.DrawString(HomeRuns, font24, blackBrush, ScoreX + ScoreXOffset, HomeScoreY)
    '        End If


    '        ' game status
    '        graphic.DrawString($"{GameStatus} - {Inning}", font12, blackBrush, GameStatusX, GameStatusY)

    '        ' divider
    '        graphic.DrawLine(blackPen, 0, DividerLineY, TotalWidth, DividerLineY)

    '        Dim ms As MemoryStream = New MemoryStream()
    '        bitmap.Save(ms, ImageFormat.Png)
    '        Return ms


    '    End Function
    'End Class



End Class

'<SDG><
