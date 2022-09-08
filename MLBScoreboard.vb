
Imports System.Text
Imports Newtonsoft.Json.Linq

Public Class MLBScoreboard

    'Private mSBData As ScoreboardData = New ScoreboardData()
    Private mAPI As MLB_API = New MLB_API()
    Private mProperties As SBProperties = New SBProperties()
    Private mCurrentGame As Game = Nothing
    Private mAllGames As Dictionary(Of String, Game) = New Dictionary(Of String, Game)

    Private ReadOnly mGAME_STATUS_FUTURE_LABELS As String() = {"SCHEDULED", "WARMUP", "PRE-GAME", "DELAYED", "POSTPONED"}
    Private ReadOnly mGAME_STATUS_PRESENT_LABLES As String() = {"IN PROGRESS", "MANAGER", "OFFICIAL"}
    Private ReadOnly mGAME_STATUS_PAST_LABELS As String() = {"FINAL", "COMPLETE", "GAME OVER"}
    Private ReadOnly mGAME_STATUS_FUTURE = 1
    Private ReadOnly mGAME_STATUS_PRESENT = 0
    Private ReadOnly mGAME_STATUS_PAST = -1

    Private Sub MLBScoreboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.Cursor = Cursors.WaitCursor

            ' load all games for today's date
            Dim gameDate As String = Me.calDatePicker.Value.ToString("MM/dd/yyyy")
            Me.mAllGames = Me.LoadAllGamesData(gameDate)

            ' set scoreboard timer for every 30 sec or as configured, and start it
            ' this time updates the list of games
            Dim SBUpdateSeconds As Integer = Convert.ToInt32(mProperties.GetProperty(mProperties.mSCOREBOARD_TIMER_KEY, "30"))
            SetScoreboardTimerInterval(SBUpdateSeconds)

            ' set game time for every 30 sec or as configured - do not start it yet
            ' this time updates all the controls for the currently selected game
            Dim GameUpdateSeconds As Integer = Convert.ToInt32(mProperties.GetProperty(mProperties.mGAME_TIMER_KEY, "30"))
            SetGameUpdateTimerInterval(GameUpdateSeconds, False)
            Me.Cursor = Cursors.Default

            ' if a favorite team specified, find game and load it
            Dim FaveTeam As String = mProperties.GetProperty(mProperties.mFAVORITE_TEAM_KEY, "")
            If Not FaveTeam = String.Empty Then
                Me.mCurrentGame = FindTeamGame(FaveTeam)
                ' start the update timer for the game
                Me.GameUpdateTimer.Start()
            End If

            ' run the scoreboard
            ResetScreenControls()
            RunScoreboard()
        Catch ex As Exception
            Trace.WriteLine($"ERROR: MLBScoreboard_Load - {ex}")
        End Try
    End Sub

    Private Sub RunScoreboard()
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

            ' if the current game is not selected, or we swithced dates, load
            ' game for favorite team
            If Me.mCurrentGame Is Nothing Then
                Dim FaveTeam As String = mProperties.GetProperty(mProperties.mFAVORITE_TEAM_KEY, "")
                If Not FaveTeam = String.Empty Then
                    Me.mCurrentGame = FindTeamGame(FaveTeam)
                    Me.GameUpdateTimer.Start()
                    Me.RunGame()
                End If
            End If

            ' update status bar
            Me.ToolStripStatusLabel1.Text = "All Games Data Updated " + Date.Now

        Catch ex As Exception
            Trace.WriteLine($"ERROR: RunScoreboard - {ex}")
        End Try

        Me.Cursor = Cursors.Default
    End Sub

    Sub RunGame()
        Try
            If Me.mCurrentGame Is Nothing Then
                Return
            Else
                ' this forces a refresch of MLB data from API
                Me.mCurrentGame.LoadGameData()
                ' update status bar
                Me.ToolStripStatusLabel1.Text = $"Current Game ({Me.mCurrentGame.AwayTeam.Abbr} @ {Me.mCurrentGame.HomeTeam.Abbr}) Data Updated {Date.Now}"
            End If

            Trace.WriteLine("=== Run Game ===>")
            Trace.WriteLine(Me.mCurrentGame.ToString())
            Trace.WriteLine(Me.mCurrentGame.AwayTeam.ToString())
            Trace.WriteLine(Me.mCurrentGame.HomeTeam.ToString())
            Trace.WriteLine("<=== Run Game ===")

            ' load line up data
            Me.mCurrentGame.AwayTeam.LoadPlayerData(Me.mCurrentGame)
            Me.mCurrentGame.HomeTeam.LoadPlayerData(Me.mCurrentGame)

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

            ' status
            Dim status As String = Me.mCurrentGame.GameStatus

            If CheckGameStatus(status) = mGAME_STATUS_FUTURE Then
                ' turn off unused controls
                tbxCommentary.Visible = False
                dgvAwayLineup.Visible = True
                dgvHomeLineup.Visible = True
                lblAwayLineup.Visible = True
                lblHomeLineup.Visible = True
                lblWeather.Visible = True
                lblBalls.Visible = True
                lblStrikes.Visible = True
                lblOuts.Visible = True
                imgDiamond.Visible = True
                lblAwayWinnerLoser.Visible = False
                lblHomeWinnerLoser.Visible = False
                lblMatchup.Text = Me.mCurrentGame.GetPitchingMatchup()
                lblMatchup.Visible = True

                ' load team roster
                Me.LoadTeamRosterGrids()
            End If

            If CheckGameStatus(status) = mGAME_STATUS_PAST Then
                ' turn off unused controls
                tbxCommentary.Visible = False
                lblBalls.Visible = False
                lblStrikes.Visible = False
                lblOuts.Visible = False
                imgDiamond.Visible = False
                dgvAwayLineup.Visible = False
                dgvHomeLineup.Visible = False
                lblAwayLineup.Visible = False
                lblHomeLineup.Visible = False
                lblWeather.Visible = False
                lblAwayWinnerLoser.Visible = True
                lblHomeWinnerLoser.Visible = True

                lblMatchup.Text = UpdateWinnerLoserPitchers()
                lblMatchup.Visible = True

                ' set winner - loser labels
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
            End If

            If CheckGameStatus(status) = mGAME_STATUS_PRESENT Then
                ' turn on controls
                tbxCommentary.Visible = True
                lblBalls.Visible = True
                lblStrikes.Visible = True
                lblOuts.Visible = True
                imgDiamond.Visible = True
                dgvAwayLineup.Visible = True
                dgvHomeLineup.Visible = True
                lblAwayLineup.Visible = True
                lblHomeLineup.Visible = True
                lblWeather.Visible = True
                lblHomeWinnerLoser.Visible = False
                lblAwayWinnerLoser.Visible = False

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
                lblMatchup.Text = Me.mCurrentGame.GetPitcherBatterMatchup()

                ' update last play
                Me.UpdatePlayCommentary()

                'update base runners image
                Me.UpdateBaseRunners()

                ' load team rosters
                Me.LoadTeamLineupGrids()

                ' if mid inning or end inning show due ups
                If Me.mCurrentGame.CurrentInningState() = "END" Or
                    Me.mCurrentGame.CurrentInningState() = "MIDDLE" Then

                    Me.tbxCommentary.Text += Me.mCurrentGame.GetDueUpBatters()

                End If
            End If

            ' redraw all controls
            Me.Refresh()

        Catch ex As Exception
            Trace.WriteLine($"ERROR: RunGame - {ex}")
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
        lblGameTitle.Text = "Away @ Home - mm/dd/yyyy (gamePk)"
        lblGameTitle.Visible = True
        lblMatchup.Text = "Match up"
        lblMatchup.Visible = True
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
        dgvInnings.DataSource = BlankInningsTable()
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
    End Sub

    Private Function BlankInningsTable() As DataTable
        Dim dt As DataTable = New DataTable("Innings")
        Dim dc As DataColumn = New DataColumn(" ")
        dt.Columns.Add(dc)
        For i = 1 To 9
            dc = New DataColumn()
            dc.ColumnName = i
            dt.Columns.Add(dc)
        Next
        dc = New DataColumn("R")
        dt.Columns.Add(dc)
        dc = New DataColumn("H")
        dt.Columns.Add(dc)
        dc = New DataColumn("E")
        dt.Columns.Add(dc)

        dt.Rows.Add(dt.NewRow())
        dt.Rows.Add(dt.NewRow())

        Return dt
    End Function
    Private Sub SetGameTitle()
        Try
            Me.lblGameTitle.Text = $"{Me.mCurrentGame.AwayTeam.FullName} @ {Me.mCurrentGame.HomeTeam.FullName} - {Me.mCurrentGame.GameDateTime}"
            Me.lblGamePk.Text = "Game Id: " + Me.mCurrentGame.GamePk.ToString()
            Me.lblStatus.Text = "Game Status: " + Me.mCurrentGame.GameStatus
        Catch ex As Exception
            Trace.WriteLine($"ERROR: SetGameTitle - {ex}")
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

                Dim play = Me.mCurrentGame.GetLastPlayDescription()
                If pitch = String.Empty Then
                    Me.tbxCommentary.Text = play
                Else
                    Me.tbxCommentary.Text = pitch + vbCr + vbCr + play
                End If
            End If
        Catch ex As Exception
            Trace.WriteLine($"ERROR: UpdatePlayCommentary - {ex}")
        End Try
    End Sub

    Private Function CheckGameStatus(GameStatus As String) As Integer
        For Each label As String In mGAME_STATUS_FUTURE_LABELS
            If GameStatus.ToUpper().Contains(label.ToUpper()) Then
                Return mGAME_STATUS_FUTURE
            End If
        Next

        For Each label As String In mGAME_STATUS_PRESENT_LABLES
            If GameStatus.ToUpper().Contains(label.ToUpper()) Then
                Return mGAME_STATUS_PRESENT
            End If
        Next

        For Each label As String In mGAME_STATUS_PAST_LABELS
            If GameStatus.ToUpper().Contains(label.ToUpper()) Then
                Return mGAME_STATUS_PAST
            End If
        Next
        Return -100
    End Function

    Private Sub LoadTeamLogos()
        Try
            Me.imgAwayLogo.Image = My.Resources.ResourceManager().GetObject(Me.mCurrentGame.AwayTeam.Abbr)
            Me.imgHomeLogo.Image = My.Resources.ResourceManager().GetObject(Me.mCurrentGame.HomeTeam.Abbr)
        Catch ex As Exception
            Trace.WriteLine($"ERROR: LoadTeamLogos - {ex}")
        End Try
    End Sub

    Private Sub UpdateBSO()
        Try
            Me.lblBalls.Text = $"Balls:    {Me.mCurrentGame.Balls()}"
            Me.lblStrikes.Text = $"Strikes: {Me.mCurrentGame.Strikes()}"
            Me.lblOuts.Text = $"Outs:    {Me.mCurrentGame.Outs()}"
        Catch ex As Exception
            Trace.WriteLine($"ERROR: UpdateBSO - {ex}")
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
            Trace.WriteLine($"ERROR: UpdateInnings - {ex}")
        End Try
    End Sub

    Private Function UpdateWinnerLoserPitchers() As String

        Dim winLoseLine = String.Empty
        Try
            ' get pitchers wins, loses, And ERA
            Dim winnerName As String = Me.mCurrentGame.WinningTeam.GetPlayer(Me.mCurrentGame.WinningPitcherId).FullName()
            Dim loserName As String = Me.mCurrentGame.LosingTeam.GetPlayer(Me.mCurrentGame.LosingPitcherId).FullName()
            Dim saveName As String = String.Empty
            If Me.mCurrentGame.SavePitcherId IsNot Nothing Then
                saveName = Me.mCurrentGame.WinningTeam.GetPlayer(Me.mCurrentGame.SavePitcherId).FullName()
            End If
            Dim winnerStats As String = Me.mCurrentGame.GetPitchingStats(Me.mCurrentGame.WinningPitcherId, Me.mCurrentGame.WinningTeam)
            Dim loserStats As String = Me.mCurrentGame.GetPitchingStats(Me.mCurrentGame.LosingPitcherId, Me.mCurrentGame.LosingTeam)
            Dim saveStats As String = String.Empty
            If Not saveName.Equals(String.Empty) Then
                saveStats = Me.mCurrentGame.GetPitchingStats(Me.mCurrentGame.SavePitcherId, Me.mCurrentGame.WinningTeam)
            End If

            If Not saveName.Equals(String.Empty) Then
                winLoseLine = $"Winner: {winnerName} {winnerStats}, Loser: {loserName} {loserStats} {vbCr}Save: {saveName} {saveStats}"
            Else
                winLoseLine = $"Winner: {winnerName} {winnerStats}, Loser: {loserName} {loserStats}"
            End If
        Catch ex As Exception
            Trace.WriteLine($"ERROR: UpdateWinnerLoserPitcher - {ex}")
        End Try
        Return winLoseLine
    End Function

    Private Sub UpdateBaseRunners()
        Dim offenseData As JObject = Me.mCurrentGame.LineScoreData.SelectToken("offense")
        Dim first As Boolean = False
        Dim second As Boolean = False
        Dim third As Boolean = False

        Try
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
            Trace.WriteLine($"ERROR: UpdateBaseRunners - {ex}")
        End Try

    End Sub

    Private Sub LoadTeamLineupGrids()
        Try
            dgvAwayLineup.DataSource = Me.mCurrentGame.AwayTeam.GetLinupTable()
            dgvHomeLineup.DataSource = Me.mCurrentGame.HomeTeam.GetLinupTable()
            lblAwayLineup.Text = Me.mCurrentGame.AwayTeam.ShortName() + " Lineup"
            lblHomeLineup.Text = Me.mCurrentGame.HomeTeam.ShortName() + " Lineup"
            dgvAwayLineup.Columns("Id").Visible = False
            dgvAwayLineup.Columns("BattingOrder").Visible = False
            dgvHomeLineup.Columns("Id").Visible = False
            dgvHomeLineup.Columns("BattingOrder").Visible = False
            dgvAwayLineup.ColumnHeadersDefaultCellStyle.Font = New Font(dgvAwayLineup.DefaultFont, FontStyle.Bold)
            dgvHomeLineup.ColumnHeadersDefaultCellStyle.Font = New Font(dgvHomeLineup.DefaultFont, FontStyle.Bold)
            dgvAwayLineup.ClearSelection()
            dgvHomeLineup.ClearSelection()
        Catch ex As Exception
            Trace.WriteLine($"ERROR: LoadTeamLineupGrids - {ex}")
        End Try
    End Sub

    Private Sub LoadTeamRosterGrids()
        Try
            dgvAwayLineup.DataSource = Me.mCurrentGame.AwayTeam.GetRosterTable()
            dgvHomeLineup.DataSource = Me.mCurrentGame.HomeTeam.GetRosterTable()
            lblAwayLineup.Text = Me.mCurrentGame.AwayTeam.ShortName() + " Roster"
            lblHomeLineup.Text = Me.mCurrentGame.HomeTeam.ShortName() + " Roster"
            dgvAwayLineup.Columns("Id").Visible = False
            dgvHomeLineup.Columns("Id").Visible = False
            dgvAwayLineup.ColumnHeadersDefaultCellStyle.Font = New Font(dgvAwayLineup.DefaultFont, FontStyle.Bold)
            dgvHomeLineup.ColumnHeadersDefaultCellStyle.Font = New Font(dgvHomeLineup.DefaultFont, FontStyle.Bold)
            dgvAwayLineup.ClearSelection()
            dgvHomeLineup.ClearSelection()
        Catch ex As Exception
            Trace.WriteLine($"ERROR: LoadTeamLineupGrids - {ex}")
        End Try
    End Sub


    Function LoadAllGamesData(gameDate As String) As Dictionary(Of String, Game)
        Dim ListOfGames As Dictionary(Of String, Game) = New Dictionary(Of String, Game)

        Try
            Dim schedule As JObject = Me.mAPI.ReturnScheduleData(gameDate)
            Dim gameDates As JArray = schedule.SelectToken("dates")

            For Each gDate As JObject In gameDates
                Dim games As JArray = gDate.SelectToken("games")

                For Each game As JObject In games
                    Dim gamePk As String = game.SelectToken("gamePk")
                    Dim oGame As Game = New Game(gamePk)
                    ListOfGames.Add(gamePk, oGame)
                Next
            Next
        Catch ex As Exception
            Trace.WriteLine($"ERROR: LoadAllTeamsData - {ex}")
        End Try
        Return ListOfGames
    End Function

    Private Function FindTeamGame(teamAbbrev As String) As Game
        For Each game In mAllGames.Values()
            If game.AwayTeam.Abbr = teamAbbrev Or game.HomeTeam.Abbr = teamAbbrev Then
                Return game
            End If
        Next
        Return Nothing
    End Function

    Sub HighlightCurrentInning()
        Dim row As Integer
        Dim inning As Integer = Me.mCurrentGame.CurrentInning()
        Dim inningHalf As String = Me.mCurrentGame.CurrentInningHalf()
        Try
            If inningHalf = "TOP" Then
                row = 0
            Else
                row = 1
            End If

            dgvInnings.Rows(row).Cells(inning).Style.BackColor = Color.LightBlue
        Catch ex As Exception
            Trace.WriteLine($"ERROR: HighlightCurrentInning - {ex}")
        End Try
    End Sub

    Private Sub QuitToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Application.Exit()
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
            For Each game As Game In Me.mAllGames.Values()
                Dim row As DataRow = dt.NewRow()
                row("Id") = game.GamePk
                row("Away") = $"{game.AwayTeam.Abbr} ({game.AwayTeam.Wins} - {game.AwayTeam.Loses})"
                row("Home") = $"{game.HomeTeam.Abbr} ({game.HomeTeam.Wins} - {game.HomeTeam.Loses})"
                row("Status") = game.GameStatus
                row("Score") = $"{game.AwayTeamRuns} - {game.HomeTeamRuns}"

                If CheckGameStatus(game.GameStatus) = mGAME_STATUS_PAST Then
                    If game.Innings.Columns.Count > 13 Then  ' account for team name column and RHE appended to end
                        row("Inning") = game.Innings.Columns.Count - 4
                    Else
                        row("Inning") = ""
                    End If
                ElseIf CheckGameStatus(game.GameStatus) = mGAME_STATUS_FUTURE Then
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
        Catch ex As Exception
            Trace.WriteLine($"ERROR: LoadAllGamesDataGrid - {ex}")
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
            'Me.mCurrentGame = FindSelectedGame(id)
            Me.mCurrentGame = Me.mAllGames(id)
            Me.GameUpdateTimer.Start()

            ' force repaint which should highlight current selected game
            dgvGames.ClearSelection()
            dgvGames.InvalidateRow(e.RowIndex)

            ' run the selected game
            Me.RunGame()
        Catch ex As Exception
            Trace.WriteLine($"ERROR: dgvGames_CellClick - {ex}")
        End Try
    End Sub

    Private Sub calDatePicker_CloseUp(sender As Object, e As EventArgs) Handles calDatePicker.CloseUp
        'Dim gameDate As String = Me.calDatePicker.Value.ToString("MM/dd/yyyy")
        Me.mCurrentGame = Nothing
        Me.GameUpdateTimer.Stop()
        Me.ResetScreenControls()
        Me.mAllGames.Clear()
        'mAllGames = mSBData.LoadAllGamesData(gameDate)
        Me.RunScoreboard()
    End Sub

    Private Sub ScoreboardUpdateTimer_Tick(sender As Object, e As EventArgs) Handles ScoreboardUpdateTimer.Tick
        Trace.WriteLine($"Scoreboard timer tick called {DateTime.Now()}")
        Dim gameDate As DateTime = DateTime.Parse(Me.calDatePicker.Value.ToString("MM/dd/yyyy"))
        Dim today As DateTime = DateTime.Today().Date()
        If Not gameDate.Equals(today) Then
            Return
        End If
        Me.mAllGames.Clear() ' clear game cache so it is reloaded
        Me.RunScoreboard()
    End Sub

    Private Sub dgvGames_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvGames.CellPainting
        ' if no current game selected, bail out
        If Me.mCurrentGame Is Nothing Then
            Return
        End If
        Try
            ' color each cell of each row according to whether the game is running or not
            For Each row As DataGridViewRow In dgvGames.Rows
                Dim id As Integer = Convert.ToInt32(row.Cells("Id").Value)
                'If id = Me.mGamePk Then
                If id = Me.mCurrentGame.GamePk Then
                    row.DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    row.DefaultCellStyle.BackColor = Color.White
                End If
            Next
        Catch ex As Exception
            Trace.WriteLine($"ERROR: dgvGames_CellPaint - {ex}")
        End Try
    End Sub

    Private Sub dgvAwayRoster_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvAwayLineup.CellPainting

        If Me.mCurrentGame Is Nothing Or Me.CheckGameStatus(Me.mCurrentGame.GameStatus) = mGAME_STATUS_FUTURE Then
            Return
        End If
        Try
            Dim pitcherId As String = Me.mCurrentGame.GetCurrentPitcherId()
            Dim batterId As String = Me.mCurrentGame.GetCurrentBatterId()

            Me.LineupDGVCellHighlight(pitcherId, batterId, sender)
        Catch ex As Exception
            Trace.WriteLine($"ERROR: dgvAwayRoster_CellPainting - {ex}")
        End Try

    End Sub

    Private Sub dgvHomeRoster_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvHomeLineup.CellPainting

        If Me.mCurrentGame Is Nothing Or Me.CheckGameStatus(Me.mCurrentGame.GameStatus) = mGAME_STATUS_FUTURE Then
            Return
        End If

        Try
            Dim pitcherId As String = Me.mCurrentGame.GetCurrentPitcherId()
            Dim batterId As String = Me.mCurrentGame.GetCurrentBatterId()
            Me.LineupDGVCellHighlight(pitcherId, batterId, sender)
        Catch ex As Exception
            Trace.WriteLine($"ERROR: dgvHomeRoster_CellPainting - {ex}")
        End Try
    End Sub

    Private Sub LineupDGVCellHighlight(pitcherId As String, batterId As String, LineUpDGV As DataGridView)
        Try

            For Each row As DataGridViewRow In LineUpDGV.Rows
                If pitcherId = row.Cells(0).Value.ToString Or batterId = row.Cells(0).Value.ToString Then
                    EnsureRowVisible(LineUpDGV, row.Index)
                    row.DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    row.DefaultCellStyle.BackColor = Color.White
                End If
            Next
        Catch ex As Exception
            Trace.WriteLine($"ERROR: LineupDGVCellHighlight - {ex}")
        End Try

    End Sub

    Private Sub EnsureRowVisible(view As DataGridView, rowIdx As Integer)
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
            Trace.WriteLine($"ERROR: EnsureRowVisible - {ex}")
        End Try
    End Sub

    Private Sub QuitToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles QuitToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub ConfigureToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConfigureToolStripMenuItem.Click
        Dim frmConfig = New Configure
        frmConfig.ShowDialog()
        SetScoreboardTimerInterval(Convert.ToInt32(mProperties.GetProperty(mProperties.mGAME_TIMER_KEY, "20")))
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem1.Click
        Dim frmAbout = AboutBox
        frmAbout.ShowDialog()
    End Sub

    Private Sub SetScoreboardTimerInterval(interval As Integer)
        ScoreboardUpdateTimer.Stop()
        ScoreboardUpdateTimer.Interval = interval * 1000
        ScoreboardUpdateTimer.Start()
        Trace.WriteLine($"Scoreboard timer interval set to {interval}")
    End Sub

    Private Sub SetGameUpdateTimerInterval(interval As Integer, start As Boolean)
        GameUpdateTimer.Stop()
        GameUpdateTimer.Interval = interval * 1000
        If start Then
            GameUpdateTimer.Start()
        End If
        Trace.WriteLine($"Game timer interval set to {interval}")
    End Sub

    Private Sub GameUpdateTimer_Tick(sender As Object, e As EventArgs) Handles GameUpdateTimer.Tick
        Trace.WriteLine($"Game timer tick called {DateTime.Now()}")
        Dim gameDate As DateTime = DateTime.Parse(Me.calDatePicker.Value.ToString("MM/dd/yyyy"))
        Dim today As DateTime = DateTime.Today().Date()
        If Not gameDate.Equals(today) Then
            Return
        End If
        Me.RunGame()
    End Sub

    Private Sub dgvAwayLineup_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvAwayLineup.CellClick
        Dim PlayerId As String = dgvAwayLineup.Rows(e.RowIndex).Cells("Id").Value
        Dim ThisPlayer As Player = Me.mCurrentGame.AwayTeam.GetPlayer(PlayerId)
        ShowPlayerStats(ThisPlayer, "AWAY")
    End Sub

    Private Sub dgvHomeLineup_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvHomeLineup.CellClick
        Dim PlayerId As String = dgvHomeLineup.Rows(e.RowIndex).Cells("Id").Value
        Dim ThisPlayer As Player = Me.mCurrentGame.HomeTeam.GetPlayer(PlayerId)
        ShowPlayerStats(ThisPlayer, "HOME")
    End Sub

    Private Sub ShowPlayerStats(ThisPlayer As Player, TeamSide As String)
        Dim frmPlayerStats As PlayerStats = New PlayerStats()
        frmPlayerStats.Player = ThisPlayer
        frmPlayerStats.GamePk = Me.mCurrentGame.GamePk
        frmPlayerStats.AwayOrHome = TeamSide
        frmPlayerStats.Show()
    End Sub

    Private Sub PlayRecapToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PlayRecapToolStripMenuItem.Click
        If Me.mCurrentGame Is Nothing Or Me.mGAME_STATUS_FUTURE = Me.CheckGameStatus(Me.mCurrentGame.GameStatus) Then
            Return
        End If
        Dim frmPlaySummary As PlaySummary = New PlaySummary()
        frmPlaySummary.Game = Me.mCurrentGame
        frmPlaySummary.ShowDialog()
    End Sub

    Private Sub StandingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StandingsToolStripMenuItem.Click

    End Sub


    ' ===============================================

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
