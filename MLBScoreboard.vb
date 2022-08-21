
Imports Newtonsoft.Json.Linq

Public Class MLBScoreboard

    Dim mSBData As ScoreboardData = New ScoreboardData()
    Dim mCurrentGame As Game = Nothing
    'Dim dtInnings As DataTable = New DataTable("Innings")
    'Dim dtAllGames As DataTable
    Dim mAllTeams As Dictionary(Of String, Team) = New Dictionary(Of String, Team)
    Dim mAllGames As Dictionary(Of String, Game) = New Dictionary(Of String, Game)


    'Private Sub setupInningsDataTable()
    '    ' clear datatable
    '    If dtInnings.Columns.Count > 0 Then
    '        For i As Integer = 0 To dtInnings.Columns.Count
    '            dtInnings.Columns.Remove(i)
    '        Next
    '    End If

    '    If dtInnings.Rows.Count > 0 Then
    '        For i As Integer = 0 To dtInnings.Rows.Count
    '            Dim dr As DataRow = dtInnings.Rows(i)
    '            dtInnings.Rows.Remove(dr)
    '        Next
    '    End If

    '    ' clear grid
    '    If Me.dgvInnings.Columns.Count > 0 Then
    '        For i As Integer = 0 To dgvInnings.Rows.Count
    '            dgvInnings.Columns.Remove(i)
    '        Next
    '    End If

    '    If Me.dgvInnings.Rows.Count > 0 Then
    '        For i As Integer = 0 To dgvInnings.Rows.Count
    '            Dim dr As DataGridViewRow = dgvInnings.Rows(i)
    '            dgvInnings.Rows.Remove(dr)
    '        Next
    '    End If

    '    ' setup new table
    '    Dim column = New DataColumn()
    '    column.DataType = System.Type.GetType("System.String")
    '    column.ColumnName = " "
    '    Me.dtInnings.Columns.Add(column)

    '    ' add first 9 innings
    '    For i = 1 To 9
    '        column = New DataColumn()
    '        column.DataType = System.Type.GetType("System.String")
    '        column.ColumnName = i
    '        Me.dtInnings.Columns.Add(column)
    '    Next

    '    ' add RHE columns
    '    column = New DataColumn
    '    column.ColumnName = "R"
    '    dtInnings.Columns.Add(column)
    '    column = New DataColumn
    '    column.ColumnName = "H"
    '    dtInnings.Columns.Add(column)
    '    column = New DataColumn
    '    column.ColumnName = "E"
    '    dtInnings.Columns.Add(column)

    '    ' add rows for Away and Home teams
    '    Me.dtInnings.Rows.Add()
    '    Me.dtInnings.Rows.Add()

    '    'setup new grid
    '    dgvInnings.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

    'End Sub
    Private Sub MLBScoreboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' setup inning table with empty innings
            'Me.setupInningsDataTable()

            ' set tables as datasource for grid views
            'Me.dgvInnings.DataSource = Me.dtInnings
            'Me.dgvGames.DataSource = Me.dtAllGames

            Me.dgvInnings.ClearSelection()
            Me.dgvAwayLineup.ClearSelection()
            Me.dgvHomeLineup.ClearSelection()
            Me.dgvGames.ClearSelection()

            ' load teams into array of objects
            mAllTeams = mSBData.LoadTeamsData()

            ' force event to load games for today
            Me.calDatePicker_CloseUp(Nothing, Nothing)

            ' set timer for every 15 sec or as configured
            Dim Props As Properties = mSBData.getProperties()
            Dim UpdateSeconds As Integer = Convert.ToInt32(Props.GetProperty(Props.TIMER_KEY, "15"))
            SetTimerInterval(UpdateSeconds)

            ' if a favorite team specified, find gamePk and load it
            Dim FaveTeam As String = Props.GetProperty(Props.FAVORITE_TEAM_KEY)
            If Not FaveTeam = String.Empty Then
                'Me.mGamePk = FindTeamGamePk(FaveTeam)
                Me.mCurrentGame = FindTeamGame(FaveTeam)
                RunScoreboard()
            End If
        Catch ex As Exception
            Trace.WriteLine($"ERROR: MLBScoreboard_Load - {ex}")
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
        dgvAwayLineup.DataSource = Nothing
        dgvHomeLineup.DataSource = Nothing
        dgvInnings.DataSource = Nothing
        dgvGames.DataSource = Nothing
        lblWeather.Text = "Weather"
        lblAwayWinnerLoser.Text = "Winner-Loser"
        lblHomeWinnerLoser.Text = "Winner-Loser"
        lblGamePk.Text = "GamePk"
        lblStatus.Text = "Game Status"
        'Me.mGamePk = 0
        Me.mCurrentGame = Nothing
    End Sub

    Private Sub SetGameTitle()
        Try
            Me.lblGameTitle.Text = String.Format("{0} @ {1} - {2}", Me.mCurrentGame.AwayTeam.FullName, Me.mCurrentGame.HomeTeam.FullName,
                                             Me.mCurrentGame.GameDateTime)
            Me.lblGamePk.Text = "Game Id: " + Me.mCurrentGame.GamePk.ToString()
            Me.lblStatus.Text = "Game Status: " + Me.mCurrentGame.GameStatus
        Catch ex As Exception
            Trace.WriteLine($"ERROR: SetGameTitle - {ex}")
        End Try
    End Sub

    Private Sub UpdatePlayCommentary()
        Try
            If Me.mCurrentGame.LiveData IsNot Nothing Then
                Dim pitch = Me.mSBData.GetLastPitchDescription(Me.mCurrentGame)
                Dim play = Me.mSBData.GetLastPlayDescription(Me.mCurrentGame)
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

    Private Sub RunScoreboard()
        Try
            ' not needed becuase loaded in cal close event in load method
            ' load all game data
            'Me.Cursor = Cursors.WaitCursor
            'For Each game As Game In mAllGames.Values()
            'Game.LoadGameData()
            'Next

            Me.dgvGames.DataSource = Nothing
            Me.LoadAllGamesDataGrid()
            Me.Cursor = Cursors.Default

            ' update status bar
            Me.ToolStripStatusLabel1.Text = "Data updated " + Date.Now

            ' update game data?
            If Me.mCurrentGame IsNot Nothing Then
                Me.RunGame()
            End If
        Catch ex As Exception
            Trace.WriteLine($"ERROR: RunScoreboard - {ex}")
        End Try
    End Sub

    Sub RunGame()
        Try
            If Me.mCurrentGame Is Nothing Then
                Return
            End If

            ' load line up data
            Me.mCurrentGame.AwayTeam.LoadLineupAndRosterData(Me.mCurrentGame)
            Me.mCurrentGame.HomeTeam.LoadLineupAndRosterData(Me.mCurrentGame)

            Trace.WriteLine(Me.mCurrentGame.ToString())
            Trace.WriteLine(Me.mCurrentGame.AwayTeam.ToString())
            Trace.WriteLine(Me.mCurrentGame.HomeTeam.ToString())

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

            If status = "SCHEDULED" Or
                status = "WARMUP" Or
                status = "PRE-GAME" Or
                status.StartsWith("DELAYED") Or
                status = "POSTPONED" Then

                lblMatchup.Text = Me.mCurrentGame.GetPitchingMatchup()

                ' turn off unused controls
                tbxCommentary.Visible = False
                dgvAwayLineup.Visible = False
                dgvHomeLineup.Visible = False
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

            If status = "FINAL" Or
                status = "COMPLETE" Or
                status = "GAME OVER" Then

                lblMatchup.Text = UpdateWinnerLoserPitchers()

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

                ' set winner - loser labels
                If mCurrentGame.WinningTeam.Abbr = mCurrentGame.AwayTeam.Abbr Then
                    lblAwayWinnerLoser.Text = "Winner"
                    lblHomeWinnerLoser.Text = "Loser"
                Else
                    lblAwayWinnerLoser.Text = "Loser"
                    lblHomeWinnerLoser.Text = "Winner"
                End If

                ' fill X in unplayed bottom of last inning
                Dim lastInning = Me.mCurrentGame.Innings.Rows(1).Item(Me.mCurrentGame.Innings.Columns.Count - 4).ToString
                If lastInning = " " Then
                    Me.mCurrentGame.Innings.Rows(1).Item(Me.mCurrentGame.Innings.Columns.Count - 4) = "X"
                End If

                dgvInnings.ClearSelection()
            End If

            If status = "IN PROGRESS" Or
                status.Contains("MANAGER") Or
                status.Contains("OFFICIAL") Then

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

                'Update inning label in inning table
                If Me.mCurrentGame.Innings.Columns.Count > 1 Then
                    Dim inningLabel As String
                    If Me.mCurrentGame.CurrentInningState = "MIDDLE" Then
                        inningLabel = $"{Me.mCurrentGame.CurrentInningState} {Me.mCurrentGame.CurrentInning}"
                    Else
                        inningLabel = $"{Me.mCurrentGame.CurrentInningHalf} {Me.mCurrentGame.CurrentInning}"
                    End If
                    Me.mCurrentGame.Innings.Columns(0).ColumnName = inningLabel
                End If

                ' highlight the current inning
                HighlightCurrentInning()

                ' update Balls, Strikes, Outs
                Me.UpdateBSO()

                ' update team rosters
                'Me.UpdateTeamLineups()

                ' update pitcher-batter matchup
                Me.updatePitcherBatterMatchup()

                ' update last play
                Me.UpdatePlayCommentary()

                'update base runners image
                Me.UpdateBaseRunners()

                ' load team rosters
                Me.LoadTeamLineupGrids()

                ' if mid inning or end inning show due ups
                If Me.mCurrentGame.CurrentInningState() = "END" Or
                    Me.mCurrentGame.CurrentInningState() = "MIDDLE" Then
                    Dim dueUpBatterInfo(3) As String
                    Dim dueUpBatterNames(3) As String
                    Dim grid As DataGridView

                    Dim lastBatterId = mSBData.getLastOutBatterId(Me.mCurrentGame)
                    Dim dueUpIds As List(Of String) = Me.getDueUpIds(lastBatterId)
                    Dim team As String

                    For i As Integer = 0 To dueUpIds.Count - 1
                        If Me.mCurrentGame.CurrentInningState() = "END" Then
                            grid = dgvAwayLineup
                            team = "away"
                        Else
                            grid = dgvHomeLineup
                            team = "home"
                        End If

                        For Each row As DataGridViewRow In grid.Rows
                            If dueUpIds(i) = row.Cells("id").Value.ToString Then
                                dueUpBatterNames(i) = row.Cells("Name").Value.ToString
                                Exit For
                            End If
                        Next

                        dueUpBatterInfo(i) = String.Format("  {0} {1}", dueUpBatterNames(i),
                                                           Me.mCurrentGame.GetBatterStats(dueUpIds(i), Me.mCurrentGame))
                    Next

                    Dim dueUps As String = vbCr + vbCr + "Due up:" + vbCr
                    For Each batter As String In dueUpBatterInfo
                        dueUps += batter + vbCr
                    Next

                    Me.tbxCommentary.Text += dueUps
                End If
            End If
        Catch ex As Exception
            Trace.WriteLine($"ERROR: RunGame - {ex}")
        End Try
    End Sub

    Private Sub LoadTeamLogos()
        Try
            Me.imgAwayLogo.Image = My.Resources.ResourceManager().GetObject(Me.mCurrentGame.AwayTeam.Abbr)
            Me.imgHomeLogo.Image = My.Resources.ResourceManager().GetObject(Me.mCurrentGame.HomeTeam.Abbr)
        Catch ex As Exception
            Trace.WriteLine($"ERROR: LoadTeamLogos - {ex}")
        End Try
    End Sub
    Function getDueUpIds(lastBatterId As String) As List(Of String)
        Dim ids As List(Of String) = New List(Of String)
        Dim startIdx As Integer
        Dim grid As DataGridView

        Try

            If Me.mCurrentGame.CurrentInningState() = "END" Then
                grid = dgvAwayLineup
            Else
                grid = dgvHomeLineup
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
        Catch ex As Exception
            Trace.WriteLine($"ERROR: GetDueUpIds - {ex}")
        End Try
        Return ids
    End Function

    Private Sub UpdateBSO()
        Try
            Dim playData = Me.mSBData.getCurrentPlayData(Me.mCurrentGame)
            Me.lblBalls.Text = $"Balls:    {playData.SelectToken("count.balls")}"
            Me.lblStrikes.Text = $"Strikes: {playData.SelectToken("count.strikes")}"
            Me.lblOuts.Text = $"Outs:    {playData.SelectToken("count.outs")}"
        Catch ex As Exception
            Trace.WriteLine($"ERROR: UpdateBSO - {ex}")
        End Try
    End Sub

    Private Sub UpdateInnings()
        Try
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
            Dim winnerName As String = Me.mCurrentGame.WinningTeam.GetPlayer(Me.mCurrentGame.WinningPitcherId).Name()
            Dim loserName As String = Me.mCurrentGame.LosingTeam.GetPlayer(Me.mCurrentGame.LosingPitcherId).Name()
            Dim saveName As String = String.Empty
            If Me.mCurrentGame.SavePitcherId IsNot Nothing Then
                saveName = Me.mCurrentGame.WinningTeam.GetPlayer(Me.mCurrentGame.SavePitcherId).Name()
            End If
            Dim winnerStats As String = Me.mCurrentGame.GetPitchingStats(Me.mCurrentGame.WinningPitcherId, Me.mCurrentGame.WinningTeam)
            Dim loserStats As String = Me.mCurrentGame.GetPitchingStats(Me.mCurrentGame.LosingPitcherId, Me.mCurrentGame.LosingTeam)
            Dim saveStats As String = String.Empty
            If Not saveName.Equals(String.Empty) Then
                saveStats = Me.mCurrentGame.GetPitchingStats(Me.mCurrentGame.SavePitcherId, Me.mCurrentGame.WinningTeam)
            End If

            If Not saveName.Equals(String.Empty) Then
                winLoseLine = String.Format("Winner: {0} {1}, Loser: {2} {3} {4}Save: {5} {6}", winnerName, winnerStats, loserName, loserStats,
                                        vbCr, saveName, saveStats)
            Else
                winLoseLine = String.Format("Winner: {0} {1}, Loser: {2} {3}", winnerName, winnerStats, loserName, loserStats)
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
            dgvAwayLineup.DataSource = Me.mCurrentGame.AwayTeam.GetLinup()
            dgvHomeLineup.DataSource = Me.mCurrentGame.HomeTeam.GetLinup()
            lblAwayLineup.Text = Me.mCurrentGame.AwayTeam.ShortName() + " Lineup"
            lblHomeLineup.Text = Me.mCurrentGame.HomeTeam.ShortName() + " Lineup"
            dgvAwayLineup.Columns("id").Visible = False
            dgvHomeLineup.Columns("id").Visible = False
            dgvAwayLineup.ColumnHeadersDefaultCellStyle.Font = New Font(dgvAwayLineup.DefaultFont, FontStyle.Bold)
            dgvHomeLineup.ColumnHeadersDefaultCellStyle.Font = New Font(dgvHomeLineup.DefaultFont, FontStyle.Bold)
            dgvAwayLineup.ClearSelection()
            dgvHomeLineup.ClearSelection()
        Catch ex As Exception
            Trace.WriteLine($"ERROR: LoadTeamLineupGrids - {ex}")
        End Try
    End Sub

    'Private Sub UpdateTeamLineups()

    '    lblAwayLineup.Text = Me.mCurrentGame.AwayTeam.ShortName() + " Lineup"
    '    lblHomeLineup.Text = Me.mCurrentGame.HomeTeam.ShortName() + " Lineup"
    '    dgvAwayLineup.DataSource = Me.mCurrentGame.AwayTeam.Lineup()
    '    dgvHomeLineup.DataSource = Me.mCurrentGame.HomeTeam.Lineup()
    '    dgvAwayLineup.Columns("id").Visible = False
    '    dgvHomeLineup.Columns("id").Visible = False
    '    dgvAwayLineup.ColumnHeadersDefaultCellStyle.Font = New Font(dgvAwayLineup.DefaultFont, FontStyle.Bold)
    '    dgvHomeLineup.ColumnHeadersDefaultCellStyle.Font = New Font(dgvHomeLineup.DefaultFont, FontStyle.Bold)
    '    dgvAwayLineup.ClearSelection()
    '    dgvHomeLineup.ClearSelection()

    'End Sub

    Private Sub updatePitcherBatterMatchup()
        Try
            Dim playData As JObject = mSBData.getCurrentPlayData(Me.mCurrentGame)

            ' get current pitcher and batter
            Dim pitcherId As String = playData.SelectToken("matchup.pitcher.id").ToString
            Dim batterId As String = playData.SelectToken("matchup.batter.id").ToString
            Dim pitcherName As String = playData.SelectToken("matchup.pitcher.fullName")
            Dim batterName As String = playData.SelectToken("matchup.batter.fullName")

            ' get stats
            Dim pitcherStats As String = String.Empty
            Dim batterStats As String = String.Empty

            If Me.mCurrentGame.CurrentInningHalf = "TOP" Then
                pitcherStats = Me.mCurrentGame.GetPitchingStats(pitcherId, Me.mCurrentGame.AwayTeam)
                batterStats = Me.mCurrentGame.GetBatterStats(batterId, Me.mCurrentGame)
            Else
                pitcherStats = Me.mCurrentGame.GetPitchingStats(pitcherId, Me.mCurrentGame.AwayTeam)
                batterStats = Me.mCurrentGame.GetBatterStats(batterId, Me.mCurrentGame)
            End If

            ' update GUI
            Dim matchup As String = String.Format("Pitcher: {0} {1} - Batter: {2} {3}", pitcherName, pitcherStats, batterName, batterStats)
            lblMatchup.Text = matchup
        Catch ex As Exception
            Trace.WriteLine($"ERROR: UpdatePitcherBatterMatchup - {ex}")
        End Try
    End Sub

    Private Function FindTeamGame(teamAbbrev As String) As Game
        For Each game In mAllGames.Values()
            If game.AwayTeam.Abbr = teamAbbrev Or game.HomeTeam.Abbr = teamAbbrev Then
                Return game
            End If
        Next
        Return Nothing
    End Function

    Private Function FindSelectedGame(id As Integer) As Game
        'For Each game In mAllGames
        '    If game.GamePk = id Then
        '        Return game
        '    End If
        'Next
        'Return Nothing
        Return mAllGames(id)
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

                If game.GameStatus = "FINAL" Or
                   game.GameStatus = "COMPLETE" Or
                   game.GameStatus = "GAME OVER" Then
                    If game.Innings.Columns.Count > 13 Then  ' account for team name column and RHE appended to end
                        row("Inning") = game.Innings.Columns.Count - 4
                    Else
                        row("Inning") = ""
                    End If
                ElseIf game.GameStatus = "SCHEDULED" Or
                       game.GameStatus = "WARMUP" Or
                       game.GameStatus = "PRE-GAME" Or
                       game.GameStatus.StartsWith("DELAYED") Or
                       game.GameStatus = "POSTPONED" Then
                    row("Inning") = ""
                Else
                    row("Inning") = game.CurrentInningHalf + " " + game.CurrentInning.ToString()
                End If

                dt.Rows.Add(row)
            Next

            ' TODO sort by start time?

            dgvGames.DataSource = dt
            dgvGames.Columns("Id").Visible = False
            dgvGames.ColumnHeadersDefaultCellStyle.Font = New Font(dgvGames.DefaultFont, FontStyle.Bold)
            dgvGames.ClearSelection()
        Catch ex As Exception
            Trace.WriteLine($"ERROR: LoadAllGamesDataGrid - {ex}")
        End Try
    End Sub

    Private Sub dgvGames_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvGames.CellClick, dgvGames.CellContentDoubleClick, dgvGames.CellContentClick

        Try
            ' bail if header row clicked
            If e.RowIndex < 0 Then
                Return
            End If

            ' get the gamePk for the clicked row
            Dim id As String = dgvGames.Rows(e.RowIndex).Cells("Id").Value.ToString
            Me.mCurrentGame = FindSelectedGame(id)

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
        Dim gameDate As String = Me.calDatePicker.Value.ToString("MM/dd/yyyy")
        mAllGames.Clear()
        mAllGames = mSBData.LoadAllGamesData(gameDate)
        Me.RunScoreboard()
    End Sub

    Private Sub UpdateTimer_Tick(sender As Object, e As EventArgs) Handles UpdateTimer.Tick
        Trace.WriteLine($"Timer tick called {DateTime.Now()}")
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

        'Dim ThisGame As Game
        'For Each game As Game In mAllGames
        '    If game.GamePk = Me.mCurrentGame.GamePk Then
        '        ThisGame = game
        '        Exit For
        '    End If
        'Next

        Try
            Dim pitcherId As String = mSBData.getCurrentPitcherId(Me.mCurrentGame)
            Dim batterId As String = mSBData.getCurrentBatterId(Me.mCurrentGame)

            For Each row As DataGridViewRow In dgvAwayLineup.Rows
                If pitcherId = row.Cells(0).Value.ToString Or batterId = row.Cells(0).Value.ToString Then
                    EnsureRowVisible(dgvAwayLineup, row.Index)
                    row.DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    row.DefaultCellStyle.BackColor = Color.White
                End If
            Next
            dgvAwayLineup.ClearSelection()
        Catch ex As Exception
            Trace.WriteLine($"ERROR: dgvAwayRoster_CellPainting - {ex}")
        End Try

    End Sub

    Private Sub dgvHomeRoster_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvHomeLineup.CellPainting

        'Dim ThisGame As Game
        'For Each game As Game In mAllGames
        '    If game.GamePk = Me.mCurrentGame.GamePk Then
        '        ThisGame = game
        '        Exit For
        '    End If
        'Next
        Try
            Dim pitcherId As String = mSBData.getCurrentPitcherId(Me.mCurrentGame)
            Dim batterId As String = mSBData.getCurrentBatterId(Me.mCurrentGame)

            For Each row As DataGridViewRow In dgvHomeLineup.Rows
                If pitcherId = row.Cells(0).Value.ToString Or batterId = row.Cells(0).Value.ToString Then
                    EnsureRowVisible(dgvHomeLineup, row.Index)
                    row.DefaultCellStyle.BackColor = Color.LightBlue
                Else
                    row.DefaultCellStyle.BackColor = Color.White
                End If
            Next
            dgvHomeLineup.ClearSelection()
        Catch ex As Exception
            Trace.WriteLine($"ERROR: dgvHomeRoster_CellPainting - {ex}")
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
        frmConfig.AllTeams = mAllTeams
        frmConfig.Show()
        SetTimerInterval(Convert.ToInt32(mSBData.getProperties.GetProperty(mSBData.getProperties.TIMER_KEY)))
    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem1.Click
        Dim frmAbout = AboutBox
        frmAbout.ShowDialog()
    End Sub

    Private Sub SetTimerInterval(interval As Integer)
        UpdateTimer.Stop()
        UpdateTimer.Interval = interval * 1000
        UpdateTimer.Start()
        Trace.WriteLine($"Timer interval set to {interval}")
    End Sub

    Private Sub PitcherStatsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PitcherStatsToolStripMenuItem.Click


        ' get pitcher stats
        ' boxscore.teams.away.players.ID.stats.pitching


    End Sub


End Class
