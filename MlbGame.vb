Imports System.IO
Imports System.Text
Imports Newtonsoft.Json.Linq


Public Class MlbGame

    Private mGamePk As String
    Private mAwayTeam As MlbTeam
    Private mHomeTeam As MlbTeam
    Private mGameDateTime As String
    Private mVenueWeather As String
    Private mGameStatus As String
    Private mAwaySchedPitcherId As String
    Private mHomeSchedPitcherId As String
    Private mAwayTeamRuns As String
    Private mHomeTeamRuns As String
    Private mWinningTeam As MlbTeam
    Private mLosingTeam As MlbTeam
    Private mWinningPitcherId As String
    Private mLosingPitcherId As String
    Private mSavePitcherId As String
    Private mCurrentInning As Integer
    Private mCurrentInningHalf As String
    Private mCurrentInningState As String
    Private mBalls As String
    Private mStrikes As String
    Private mOuts As String
    Private mPitchCount As String
    Private mInnings As DataTable
    Private mRHE As DataTable
    Private mData As JObject
    Private mLiveData As JObject
    Private mLineData As JObject
    Private mBoxData As JObject
    Private mGameData As JObject
    Private mCurrentPlayData As JObject
    Private mAllPlaysData As JObject
    Private mAPI As MlbApi = New MlbApi()
    Private mProperties As SBProperties = New SBProperties()

    Shared ReadOnly mGAME_STATUS_FUTURE_LABELS As String() = {"SCHEDULED", "WARMUP", "PRE-GAME", "DELAYED", "POSTPONED"}
    Shared ReadOnly mGAME_STATUS_PRESENT_LABLES As String() = {"IN PROGRESS", "MANAGER", "OFFICIAL"}
    Shared ReadOnly mGAME_STATUS_PAST_LABELS As String() = {"FINAL", "COMPLETE", "GAME OVER", "COMPLETED"}
    Public Shared ReadOnly mGAME_STATUS_FUTURE = 1
    Public Shared ReadOnly mGAME_STATUS_PRESENT = 0
    Public Shared ReadOnly mGAME_STATUS_PAST = -1

    Public Shared Function CheckGameStatus(GameStatus As String) As Integer
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
        Trace.WriteLine($"CheckGameStatus - unknown status: {GameStatus}")
        Return -100
    End Function

    Public ReadOnly Property GamePk() As Integer
        Get
            Return Me.mGamePk
        End Get
    End Property

    Public ReadOnly Property AwayTeam() As MlbTeam
        Get
            Return Me.mAwayTeam
        End Get
    End Property

    Public ReadOnly Property HomeTeam() As MlbTeam
        Get
            Return Me.mHomeTeam
        End Get
    End Property

    Public ReadOnly Property GameDateTime() As String
        Get
            Return Me.mGameDateTime
        End Get
    End Property

    Public ReadOnly Property VenueWeather() As String
        Get
            Return Me.mVenueWeather
        End Get
    End Property

    Public ReadOnly Property GameStatus() As String
        Get
            Return Me.mGameStatus
        End Get
    End Property

    Public ReadOnly Property AwayScheduledPitcherId() As String
        Get
            Return Me.mAwaySchedPitcherId
        End Get
    End Property

    Public ReadOnly Property HomeScheduledPitcherId() As String
        Get
            Return Me.mHomeSchedPitcherId
        End Get
    End Property

    Public ReadOnly Property AwayTeamRuns() As String
        Get
            Return Me.mAwayTeamRuns
        End Get
    End Property

    Public ReadOnly Property HomeTeamRuns() As String
        Get
            Return Me.mHomeTeamRuns
        End Get
    End Property

    Public ReadOnly Property WinningTeam() As MlbTeam
        Get
            Return Me.mWinningTeam
        End Get
    End Property

    Public ReadOnly Property LosingTeam() As MlbTeam
        Get
            Return Me.mLosingTeam
        End Get
    End Property

    Public ReadOnly Property WinningPitcherId() As String
        Get
            Return Me.mWinningPitcherId
        End Get
    End Property

    Public ReadOnly Property LosingPitcherId() As String
        Get
            Return Me.mLosingPitcherId
        End Get
    End Property

    Public ReadOnly Property SavePitcherId() As String
        Get
            Return Me.mSavePitcherId
        End Get
    End Property

    Public ReadOnly Property CurrentInning() As Integer
        Get
            Return Me.mCurrentInning
        End Get
    End Property

    Public ReadOnly Property CurrentInningHalf() As String
        Get
            Return Me.mCurrentInningHalf
        End Get
    End Property

    Public ReadOnly Property CurrentInningState() As String
        Get
            Return Me.mCurrentInningState
        End Get
    End Property

    Public ReadOnly Property Innings() As DataTable
        Get
            Dim dt As DataTable = Me.mInnings
            Try
                For Each col As DataColumn In Me.mRHE.Columns
                    If Not dt.Columns.Contains(col.ColumnName) Then
                        Dim RHEcol As DataColumn = New DataColumn()
                        RHEcol.ColumnName = col.ColumnName
                        dt.Columns.Add(RHEcol)
                    End If

                    For Each row As DataRow In Me.mRHE.Rows
                        dt.Rows(0).Item(col.ColumnName) = mRHE.Rows(0).Item(col.ColumnName).ToString()
                        dt.Rows(1).Item(col.ColumnName) = mRHE.Rows(1).Item(col.ColumnName).ToString()
                    Next
                Next
            Catch ex As Exception
                Trace.WriteLine($"ERROR: Innings - {ex}")
            End Try
            Return dt
        End Get
    End Property

    Public ReadOnly Property RHE() As DataTable
        Get
            Return Me.mRHE
        End Get
    End Property

    Public ReadOnly Property Data As JObject
        Get
            Return Me.mData
        End Get
    End Property

    Public ReadOnly Property LiveData As JObject
        Get
            Return Me.mLiveData
        End Get
    End Property

    Public ReadOnly Property LineScoreData As JObject
        Get
            Return Me.mLineData
        End Get
    End Property

    Public ReadOnly Property BoxScoreData As JObject
        Get
            Return Me.mBoxData
        End Get
    End Property

    Public ReadOnly Property GameData As JObject
        Get
            Return Me.mGameData
        End Get
    End Property

    Public ReadOnly Property CurrentPlayData As JObject
        Get
            Return Me.mCurrentPlayData
        End Get
    End Property

    Public ReadOnly Property AllPlaysData As JObject
        Get
            Return Me.mAllPlaysData
        End Get
    End Property

    Public ReadOnly Property Balls As String
        Get
            Return Me.mBalls
        End Get
    End Property

    Public ReadOnly Property Strikes As String
        Get
            Return Me.mStrikes
        End Get
    End Property

    Public ReadOnly Property Outs As String
        Get
            Return Me.mOuts
        End Get
    End Property

    Public ReadOnly Property PitchCount As String
        Get
            Return Me.mPitchCount
        End Get
    End Property

    Public Sub New(gamePk As String)
        Me.mGamePk = Convert.ToInt32(gamePk)
        LoadGameData()
    End Sub

    Private Sub RefreshMLBData()
        ' call API and parse Json into various data packages
        Try
            mData = mAPI.ReturnLiveFeedData(Me.mGamePk)
            mLiveData = mData.SelectToken("liveData")
            mLineData = mLiveData.SelectToken("linescore")
            mBoxData = mLiveData.SelectToken("boxscore")
            mGameData = mData.SelectToken("gameData")
            mAllPlaysData = mLiveData.SelectToken("plays")
            mCurrentPlayData = mLiveData.SelectToken("plays.currentPlay")
        Catch ex As Exception
            Trace.WriteLine($"ERROR: RefreshMLBData - {ex}")
        End Try
    End Sub

    Public Sub LoadGameData()

        Try
            ' get fresh data
            RefreshMLBData()

            ' get teams
            Me.mAwayTeam = New MlbTeam(Convert.ToInt32(mBoxData.SelectToken("teams.away.team.id")))
            Me.mHomeTeam = New MlbTeam(Convert.ToInt32(mBoxData.SelectToken("teams.home.team.id")))

            ' set teams win/loss record
            ' This data is not available from the team data
            Me.mAwayTeam.Wins = mGameData.SelectToken("teams.away.record.wins")
            Me.mAwayTeam.Loses = mGameData.SelectToken("teams.away.record.losses")
            Me.mHomeTeam.Wins = mGameData.SelectToken("teams.home.record.wins")
            Me.mHomeTeam.Loses = mGameData.SelectToken("teams.home.record.losses")

            ' game date and time
            Dim gameDate As String = mGameData.SelectToken("datetime.officialDate").ToString()
            Dim gameTime As String = mGameData.SelectToken("datetime.time").ToString()
            Dim gameTimeAmPm As String = mGameData.SelectToken("datetime.ampm").ToString()
            Me.mGameDateTime = $"{gameDate} {gameTime}{gameTimeAmPm}"

            ' TODO - convert to local time
            'Dim gameLocalDateTime As String = ConvertDateTimeToLocalTimeZone()

            ' venue weather
            Me.mVenueWeather = $"Weather: {mGameData.SelectToken("weather.temp")}°F, {mGameData.SelectToken("weather.condition")}"

            ' game status
            Me.mGameStatus = mGameData.SelectToken("status.detailedState").ToString().ToUpper()

            ' load RHE table
            Me.LoadRHE()

            ' load innings
            Me.LoadInnings()

            ' load team player data - delay this load until needed to improve performance
            'Me.AwayTeam.LoadPlayerData(Me)
            'Me.HomeTeam.LoadPlayerData(Me)

            'current game

            If MlbGame.CheckGameStatus(Me.GameStatus) = MlbGame.mGAME_STATUS_PRESENT Then

                ' away runs
                Dim AwayRuns As String = mLineData.SelectToken("teams.away.runs").ToString()
                If Not AwayRuns.Equals(String.Empty) Then
                    Me.mAwayTeamRuns = Convert.ToInt32(AwayRuns)
                Else
                    Me.mAwayTeamRuns = 0
                End If

                ' home runs
                Dim HomeRuns As String = mLineData.SelectToken("teams.home.runs").ToString()
                If Not HomeRuns.Equals(String.Empty) Then
                    Me.mHomeTeamRuns = Convert.ToInt32(HomeRuns)
                Else
                    Me.mHomeTeamRuns = 0
                End If

                ' inning data
                Me.mCurrentInning = mLineData.SelectToken("currentInning")
                Me.mCurrentInningHalf = mLineData.SelectToken("inningHalf").ToString().ToUpper()
                Me.mCurrentInningState = mLineData.SelectToken("inningState").ToString().ToUpper()

                ' BSO data
                Me.mBalls = Me.CurrentPlayData.SelectToken("count.balls")
                Me.mStrikes = Me.CurrentPlayData.SelectToken("count.strikes")
                Me.mOuts = Me.CurrentPlayData.SelectToken("count.outs")

                ' update total pitch count
                Me.mPitchCount = Me.GetPitchCount()

            ElseIf MlbGame.CheckGameStatus(Me.GameStatus()) = MlbGame.mGAME_STATUS_FUTURE Then

                Me.mAwaySchedPitcherId = mGameData.SelectToken("probablePitchers.away.id")
                Me.mHomeSchedPitcherId = mGameData.SelectToken("probablePitchers.home.id")

            ElseIf MlbGame.CheckGameStatus(Me.GameStatus()) = MlbGame.mGAME_STATUS_PAST Then

                ' runs
                Me.mAwayTeamRuns = mLineData.SelectToken("teams.away.runs")
                Me.mHomeTeamRuns = mLineData.SelectToken("teams.home.runs")

                ' winning pitcher id
                Me.mWinningPitcherId = mLiveData.SelectToken("decisions.winner.id").ToString()

                ' losing pitcher id
                Me.mLosingPitcherId = mLiveData.SelectToken("decisions.loser.id").ToString()

                ' saving pitcher id
                If mLiveData.SelectToken("decisions.save.id") IsNot Nothing Then
                    mSavePitcherId = mLiveData.SelectToken("decisions.save.id").ToString()
                End If

                If Convert.ToInt32(mAwayTeamRuns) > Convert.ToInt32(mHomeTeamRuns) Then
                    Me.mWinningTeam = AwayTeam()
                    Me.mLosingTeam = HomeTeam()
                Else
                    Me.mWinningTeam = HomeTeam()
                    Me.mLosingTeam = AwayTeam()
                End If

            Else
                Trace.WriteLine($"LoadGameData - unknown status: {Me.GameStatus()}")
            End If

            ' save data for debugging
            If mProperties.GetProperty(mProperties.mKEEP_DATA_FILES_KEY, "0") = "1" Then
                Dim DataRoot As String = mProperties.GetProperty(mProperties.mDATA_FILES_PATH_KEY)
                File.WriteAllText($"{DataRoot}\\{Me.GamePk()}-{Me.AwayTeam.Abbr}-{Me.HomeTeam.Abbr}_data.json", mData.ToString())
            End If

            'Trace.WriteLine($"Loading game {Me.GamePk} {Me.AwayTeam.Abbr} @ {Me.HomeTeam.Abbr} data")
        Catch ex As Exception
            Trace.WriteLine($"ERROR: LoadGameData - {ex}")
        End Try
    End Sub


    Public Overrides Function ToString() As String
        Dim sb As StringBuilder = New StringBuilder()
        sb.Append(vbCr + "======" + vbCr)
        sb.Append($"Game Id: {Me.GamePk()}")
        sb.Append(vbCr)
        sb.Append($"Game Date-Time: {Me.GameDateTime()}")
        sb.Append(vbCr)
        sb.Append($"Game Status: {Me.GameStatus()}")
        sb.Append(vbCr)
        sb.Append($"Inning: {Me.CurrentInningHalf()} {Me.CurrentInning()}")
        sb.Append(vbCr)
        sb.Append($"Venue Weather: {Me.VenueWeather()}")
        sb.Append(vbCr)
        sb.Append($"Away Team: {Me.AwayTeam().FullName()} - {Me.AwayTeamRuns()} runs")
        sb.Append(vbCr)
        sb.Append($"Home Team: {Me.HomeTeam().FullName()} - {Me.HomeTeamRuns()} runs")
        sb.Append(vbCr)
        If Not Me.WinningTeam() Is Nothing Then
            sb.Append($"Winning Team: {Me.WinningTeam().ShortName()}")
            sb.Append(vbCr)
            sb.Append($"Winning PitcherId: {Me.WinningPitcherId()}")
            sb.Append(vbCr)
        End If
        Return sb.ToString()
    End Function

    Private Sub LoadInnings()

        Try
            Me.mInnings = New DataTable("Innings")

            ' add rows for away and home lines
            Me.mInnings.Rows.Add()
            Me.mInnings.Rows.Add()

            ' add column for team name and record
            Dim col As DataColumn = New DataColumn()
            col.ColumnName = ""
            Me.mInnings.Columns.Add(col)

            Dim awayNameAndRecord As String = String.Format("{0} ({1}-{2})", Me.AwayTeam.Abbr, Me.AwayTeam.Wins, Me.AwayTeam.Loses)
            Me.mInnings.Rows(0).Item(0) = awayNameAndRecord
            Dim homeNameAndRecord As String = String.Format("{0} ({1}-{2})", Me.HomeTeam.Abbr, Me.HomeTeam.Wins, Me.HomeTeam.Loses)
            Me.mInnings.Rows(1).Item(0) = homeNameAndRecord

            ' fill innings
            Dim innings As JArray = Me.mLineData.SelectToken("innings")
            Dim inningCount As Integer = 0
            For Each inning As JObject In innings

                ' create a column for inning
                col = New DataColumn()
                col.ColumnName = inning.SelectToken("num")
                Me.mInnings.Columns.Add(col)
                inningCount += 1

                ' away data
                Dim runs As String = inning.SelectToken("away.runs")
                If runs Is Nothing Then
                    runs = " "
                End If
                Me.mInnings.Rows(0).Item(Integer.Parse(inning.SelectToken("num"))) = runs

                ' home data
                runs = inning.SelectToken("home.runs")
                If runs Is Nothing Then
                    runs = " "
                End If
                Me.mInnings.Rows(1).Item(Integer.Parse(inning.SelectToken("num"))) = runs
            Next

            ' fill in remaining empty columns for unplayed innings
            If inningCount < 9 Then
                For i As Integer = inningCount + 1 To 9
                    col = New DataColumn()
                    col.ColumnName = i
                    Me.mInnings.Columns.Add(col)
                Next
            End If
        Catch ex As Exception
            Trace.WriteLine($"ERROR: LoadInnings - {ex}")
        End Try
    End Sub

    Private Sub LoadRHE()
        Try
            Me.mRHE = New DataTable("RHE")
            ' add rows for away and home lines
            Me.mRHE.Rows.Add()
            Me.mRHE.Rows.Add()

            Dim RHE() As String = {"R", "H", "E"}
            For Each stat As String In RHE
                Dim col = New DataColumn()
                col.ColumnName = stat
                Me.mRHE.Columns.Add(col)
            Next

            ' fill R-H-E columns
            If MlbGame.CheckGameStatus(Me.GameStatus()) = MlbGame.mGAME_STATUS_PAST Or MlbGame.CheckGameStatus(Me.GameStatus()) = MlbGame.mGAME_STATUS_PRESENT Then
                'If Me.GameStatus() = "IN PROGRESS" Or
                '    Me.GameStatus() = "FINAL" Or
                '    Me.GameStatus() = "COMPLETE" Or
                '    Me.GameStatus() = "GAME OVER" Or
                '    Me.GameStatus().Contains("MANAGER") Or
                '    Me.GameStatus().Contains("OFFICIAL") Then
                Me.mRHE.Rows(0).Item("R") = Me.mLineData.SelectToken("teams.away.runs").ToString()
                Me.mRHE.Rows(0).Item("H") = Me.mLineData.SelectToken("teams.away.hits").ToString()
                Me.mRHE.Rows(0).Item("E") = Me.mLineData.SelectToken("teams.away.errors").ToString()

                Me.mRHE.Rows(1).Item("R") = Me.mLineData.SelectToken("teams.home.runs").ToString()
                Me.mRHE.Rows(1).Item("H") = Me.mLineData.SelectToken("teams.home.hits").ToString()
                Me.mRHE.Rows(1).Item("E") = Me.mLineData.SelectToken("teams.home.errors").ToString()
            End If
        Catch ex As Exception
            Trace.WriteLine($"ERROR: LoadRHE - {ex}")
        End Try
    End Sub

    Private Function GetPitchCount() As String
        Dim PitchCount As String = ""
        Dim AwayOrHome As String

        If Me.mCurrentInningHalf.ToUpper = "TOP" Then
            AwayOrHome = "home"
        Else
            AwayOrHome = "away"
        End If
        Try
            Dim playData As JObject = Me.CurrentPlayData()
            Dim pitcherId As String = playData.SelectToken("matchup.pitcher.id").ToString
            PitchCount = Me.BoxScoreData.SelectToken($"teams.{AwayOrHome}.players.ID{pitcherId}.stats.pitching.pitchesThrown")

        Catch ex As Exception
            Trace.WriteLine($"ERROR: GetPitchCount - {ex}")
        End Try

        Return PitchCount

    End Function
    Public Function GetPitchingMatchup() As String
        Dim matchup As String = ""

        Try
            ' get scheduled away pitcher
            Dim awayPitcherName As String
            If Me.AwayTeam.GetPlayer(Me.AwayScheduledPitcherId()) Is Nothing Then
                awayPitcherName = "Unknown"
            Else
                awayPitcherName = Me.AwayTeam.GetPlayer(Me.AwayScheduledPitcherId()).FullName
            End If

            ' get scheduled home pitcher
            Dim homePitcherName As String
            If Me.HomeTeam.GetPlayer(Me.HomeScheduledPitcherId()) Is Nothing Then
                homePitcherName = "Unknown"
            Else
                homePitcherName = Me.HomeTeam.GetPlayer(Me.HomeScheduledPitcherId()).FullName
            End If

            Dim awayPitchingStats As String = Me.GetPitchingStats(Me.AwayScheduledPitcherId(), Me.AwayTeam())
            Dim homePitchingStats As String = GetPitchingStats(Me.HomeScheduledPitcherId(), Me.HomeTeam())
            matchup = String.Format("Preview: {0} {1} vs. {2} {3}", awayPitcherName, awayPitchingStats, homePitcherName, homePitchingStats)
        Catch ex As Exception
            Trace.WriteLine($"ERROR: GetPitchingMatchup - {ex}")
        End Try
        Return matchup
    End Function

    Public Function GetPitchingStats(pitcherId As Integer, ThisTeam As MlbTeam) As String
        Dim pitchingStats As String = String.Empty

        Try
            Dim playerData As JObject
            If ThisTeam.Id = Me.AwayTeam.Id Then
                playerData = Me.BoxScoreData().SelectToken("teams.away.players")
            Else
                playerData = Me.BoxScoreData().SelectToken("teams.home.players")
            End If

            Dim pId As String = "ID" + pitcherId.ToString()
            For Each player In playerData
                If player.Key = pId Then
                    Dim wins As String = player.Value.Item("seasonStats").Item("pitching").Item("wins")
                    Dim losses As String = player.Value.Item("seasonStats").Item("pitching").Item("losses")
                    Dim era As String = player.Value.Item("seasonStats").Item("pitching").Item("era")
                    pitchingStats = String.Format(" ({0}-{1}, {2} ERA)", wins, losses, era)
                    Exit For
                End If
            Next
        Catch ex As Exception
            Trace.WriteLine($"ERROR: GetPitchingStats - {ex}")
        End Try
        Return pitchingStats
    End Function

    Public Function GetBatterStats(batterId As Integer, ThisTeam As MlbTeam) As String
        Dim batterStats As String = String.Empty

        Try
            Dim playerData As JObject
            If ThisTeam.Id = AwayTeam.Id Then
                playerData = Me.BoxScoreData().SelectToken("teams.away.players")
            Else
                playerData = Me.BoxScoreData().SelectToken("teams.home.players")
            End If

            Dim bId As String = "ID" + batterId.ToString
            For Each player In playerData
                If player.Key = bId Then
                    Dim hits As String = player.Value.Item("stats").Item("batting").Item("hits")
                    Dim atBats As String = player.Value.Item("stats").Item("batting").Item("atBats")
                    Dim avg As String = player.Value.Item("seasonStats").Item("batting").Item("avg")
                    batterStats = String.Format(" ({0}-{1}, {2} AVG)", hits, atBats, avg)
                    Exit For
                End If
            Next
        Catch ex As Exception
            Trace.WriteLine($"ERROR: GetBatterStats - {ex}")
        End Try
        Return batterStats
    End Function


    'Function ConvertDateTimeToLocalTimeZone() As String

    '    Dim gameTime As String = mGameData.SelectToken("datetime.dateTime").ToString()
    '    Dim gameTZ As String = mGameData.SelectToken("venue.timeZone.tz").ToString()
    '    Dim gameTZI As TimeZoneInfo
    '    If gameTZ = "EDT" Then
    '        gameTZI = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")
    '    ElseIf gameTZ = "EST" Then
    '        gameTZI = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")
    '    ElseIf gameTZ = "CDT" Then
    '        gameTZI = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")
    '    ElseIf gameTZ = "CST" Then
    '        gameTZI = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")
    '    ElseIf gameTZ = "MDT" Then
    '        gameTZI = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time")
    '    ElseIf gameTZ = "MST" Then
    '        gameTZI = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time")
    '    ElseIf gameTZ = "PDT" Then
    '        gameTZI = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")
    '    ElseIf gameTZ = "PST" Then
    '        gameTZI = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")
    '    End If

    '    Dim gt As DateTime = DateTime.Parse(gameTime)
    '    Dim ds As String = TimeZoneInfo.ConvertTime(gt, gameTZI, TimeZoneInfo.Local)

    '    Return ds
    'End Function

    Function GetCurrentBatterId() As String
        Try
            Dim batterId As String = Me.mCurrentPlayData.SelectToken("matchup.batter.id").ToString
            Return batterId
        Catch ex As Exception
            Trace.WriteLine($"ERROR: GetCurrentBatterId - {ex}")
        End Try
        Return ""
    End Function

    Function GetCurrentPitcherId() As String
        Try
            Dim pitcherId As String = Me.mCurrentPlayData.SelectToken("matchup.pitcher.id").ToString
            Return pitcherId
        Catch ex As Exception
            Trace.WriteLine($"ERROR: GetCurrentPitcherId - {ex}")
        End Try
        Return ""
    End Function

    Public Function GetDueUpBatters() As String
        Dim lastBatterId As String = 0
        Dim battingTeam As MlbTeam
        Dim sb As StringBuilder = New StringBuilder()
        'Dim battingTeamLineup As DataTable

        sb.Append(vbCr + vbCr + "Due up:" + vbCr)

        Try
            ' last inning number
            Dim lastInning As Integer = Me.CurrentInning() - 1 ' array is 0-based
            If lastInning <= 0 Then
                lastInning = 0
            End If

            ' get correct team obj and build path to last inning's plays
            Dim jsonPath As String
            If Me.CurrentInningState() = "END" Then
                battingTeam = Me.AwayTeam
                jsonPath = $"playsByInning[{lastInning}].top"
            Else
                battingTeam = Me.HomeTeam
                jsonPath = $"playsByInning[{lastInning - 1}].bottom"
            End If

            'battingTeamLineup = battingTeam.GetLinupTable()

            ' find the last batter id
            ' if last inning = 0, it's 1st inning and no batters yet
            Dim LastBattingPosition As Integer = 0

            If lastInning > 0 Then
                Dim allPlayData As JObject = Me.LiveData().SelectToken("plays")

                ' get play data
                Dim data As JArray = allPlayData.SelectToken(jsonPath)
                If data Is Nothing Or data.Count < 1 Then
                    Return sb.ToString()
                End If

                Dim lastPlayIndex = data.Item(data.Count() - 1)  ' zero based
                jsonPath = $"allPlays[{lastPlayIndex.ToString()}].matchup.batter.id"

                ' get last batter id
                lastBatterId = allPlayData.SelectToken(jsonPath)

                ' find last batter position in list of batting order players
                For Each key In battingTeam.Lineup.Keys
                    If battingTeam.Lineup.Item(key).Id = lastBatterId Then
                        LastBattingPosition = key
                    End If
                Next
                'Dim i As Integer = 0
                'For Each player In battingTeam.BattingOrder
                '    If lastBatterId = player.Id Then
                '        LastBattingPosition = i
                '        Exit For
                '    End If
                '    i += 1
                'Next

            Else
                ' set last batter id = batter #9 in lineup, which will
                ' result in first three batters due up

                LastBattingPosition = 9  ' zero-based array
            End If


            ' figure out next three batters
            Dim DueUpPosition = LastBattingPosition
            For i As Integer = 1 To 3
                DueUpPosition += 1
                If DueUpPosition > 9 Then
                    DueUpPosition -= 9
                End If

                'For Each row As DataRow In battingTeamLineup.Rows()
                '    'If battingPosition = row("BattingOrder") Then
                '    If DueUpPosition = row("BattingOrder") Then
                '        sb.Append($"  {row("Name")} {Me.GetBatterStats(row("Id"), battingTeam)}")
                '        sb.Append(vbCr)
                '    End If
                'Next


                sb.Append($"  {battingTeam.Lineup.Item(DueUpPosition).FullName} {Me.GetBatterStats(battingTeam.Lineup.Item(DueUpPosition).Id, battingTeam)}")
                sb.Append(vbCr)

            Next

        Catch ex As Exception
            Trace.WriteLine($"ERROR: GetDueUpBatters - {ex}")
        End Try

        Return sb.ToString()
    End Function

    Public Function GetLastPitchDescription() As String
        Dim desc As String = ""

        Try
            Dim currentPlayData As JObject = Me.CurrentPlayData()
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

            If pitchType = String.Empty Then
                desc = String.Empty
            ElseIf startSpeed = "" Then
                desc = $"{pitchCall}"
            Else
                desc = $"Pitch #{pitchNum}: {pitchType} - {pitchCall}  (Start {startSpeed}mph, End {endSpeed}mph)"
            End If
        Catch ex As Exception
            Trace.WriteLine($"ERROR: GetLastPitchDescription - {ex}")
        End Try
        Return desc
    End Function

    Public Function GetLastPlayDescription() As String
        Dim desc As String = ""
        Try
            Dim currentPlayIdx As Integer = Me.LiveData.SelectToken("plays.currentPlay.atBatIndex")
            If currentPlayIdx = 0 Then
                Return ""
            End If

            Dim allPlaysData As JArray = Me.LiveData.SelectToken("plays.allPlays")
            Dim lastPlayData = allPlaysData(currentPlayIdx - 1)

            desc = lastPlayData.SelectToken("result.description")
            Dim playInning As String = lastPlayData.SelectToken("about.inning")
            Dim playInningHalf As String = lastPlayData.SelectToken("about.halfInning")


            If playInning.Equals(Me.CurrentInning.ToString) And playInningHalf.ToUpper.Equals(Me.CurrentInningHalf.ToUpper) Then
                desc = $"Last play: {desc}"
            Else
                desc = $"{playInningHalf.ToUpper()} {playInning}: {desc}"
            End If

        Catch ex As Exception
            Trace.WriteLine($"ERROR: GetLastPlayDescription - {ex}")
        End Try
        Return desc
    End Function


    Public Function GetPitcherBatterMatchup() As String
        Dim matchup As String = ""
        Try
            Dim playData As JObject = Me.CurrentPlayData()

            ' get current pitcher and batter
            Dim pitcherId As String = playData.SelectToken("matchup.pitcher.id").ToString
            Dim batterId As String = playData.SelectToken("matchup.batter.id").ToString
            Dim pitcherName As String = playData.SelectToken("matchup.pitcher.fullName")
            Dim batterName As String = playData.SelectToken("matchup.batter.fullName")

            ' get stats
            Dim pitcherStats As String = String.Empty
            Dim batterStats As String = String.Empty

            If Me.CurrentInningHalf = "TOP" Then
                pitcherStats = Me.GetPitchingStats(pitcherId, Me.HomeTeam)
                batterStats = Me.GetBatterStats(batterId, Me.AwayTeam)
            Else
                pitcherStats = Me.GetPitchingStats(pitcherId, Me.AwayTeam)
                batterStats = Me.GetBatterStats(batterId, Me.HomeTeam)
            End If

            matchup = $"Pitcher: {pitcherName} {pitcherStats} - Batter: {batterName} {batterStats}"

        Catch ex As Exception
            Trace.WriteLine($"ERROR: UpdatePitcherBatterMatchup - {ex}")
        End Try
        Return matchup
    End Function

    Public Function LastPlayScorebookEntry() As String
        Dim Scorebook As String = ""
        Dim currentPlayIdx As Integer = Me.LiveData.SelectToken("plays.currentPlay.atBatIndex")
        currentPlayIdx -= 1
        If currentPlayIdx <= 0 Then
            Return ""
        Else
            Scorebook = CreateScorebookEntry(currentPlayIdx)
        End If
        Return Scorebook

    End Function

    Public Function CreateScorebookEntry(PlayIndex As Integer) As String
        Dim ScorebookEntry As String = ""

        Try
            Dim allPlaysData As JArray = Me.LiveData.SelectToken("plays.allPlays")
            Dim lastPlayData = allPlaysData(PlayIndex)

            Dim EventName As String = lastPlayData.SelectToken("result.event")
            If EventName = "" Then
                Return ""
            End If

            'Trace.WriteLine($"  Event: {EventName}")

            Dim Details As New List(Of String)
            For r As Integer = 0 To lastPlayData.SelectToken("runners").Count - 1
                Dim RunnersData As JObject = lastPlayData.SelectToken("runners").Item(r)
                'Trace.WriteLine($" runner: {r}")
                Dim Credits As JArray = RunnersData.SelectToken("credits")

                If Credits IsNot Nothing Then
                    For c As Integer = 0 To Credits.Count - 1
                        Dim PositionCode As String = Credits.Item(c).SelectToken("position.code")
                        Dim CreditType As String = Credits.Item(c).SelectToken("credit")
                        ' Trace.WriteLine($"  [{c}] position: {PositionCode}, type: {CreditType}")
                        Details.Add(PositionCode)

                    Next
                End If

            Next

            'Dim OfficialScoring As String = ""
            If EventName.ToUpper.Contains("STRIKEOUT") Then
                ScorebookEntry = "K"
            ElseIf EventName.ToUpper.Contains("WALK") Then
                ScorebookEntry = "BB"
            ElseIf EventName.ToUpper.Contains("HIT BY PITCH") Then
                ScorebookEntry = "HBP"
            ElseIf EventName.ToUpper.Contains("INTENT WALK") Then
                ScorebookEntry = "IBB"
            ElseIf EventName.ToUpper.Contains("SINGLE") Then
                ScorebookEntry = $"SINGLE"
            ElseIf EventName.ToUpper.Contains("DOUBLE") Then
                ScorebookEntry = $"DOUBLE"
            ElseIf EventName.ToUpper.Contains("TRIPLE") Then
                ScorebookEntry = $"TRIPLE"
            ElseIf EventName.ToUpper.Contains("HOME RUN") Then
                ScorebookEntry = "HR"
            ElseIf EventName.ToUpper.Contains("FLYOUT") Or EventName.ToUpper.Contains("SAV FLY") Then
                ScorebookEntry = $"{Details(0)}"
            ElseIf EventName.ToUpper.Contains("FOUL") Then
                ScorebookEntry = $"F{Details(0)}"
            ElseIf EventName.ToUpper.Contains("LINEOUT") Then
                ScorebookEntry = $"L{Details(0)}"
            ElseIf EventName.ToUpper.Contains("RUNNER OUT") Or EventName.ToUpper.Contains("FORCEOUT") Then
                ScorebookEntry = $"{Details(0)}"
                For cnt As Integer = 1 To Details.Count - 1
                    If Details(cnt) <> Details(cnt - 1) Then
                        ScorebookEntry = $"{ScorebookEntry}-{Details(cnt)}"
                    End If
                Next
            ElseIf EventName.ToUpper.Contains("POP OUT") Then
                ScorebookEntry = $"P{Details(0)}"
            ElseIf EventName.ToUpper.Contains("DOUBLE PLAY") Or EventName.ToUpper.Contains("DP") Then
                ScorebookEntry = $"DP{Details(0)}"
                For cnt As Integer = 1 To Details.Count - 1
                    If Details(cnt) <> Details(cnt - 1) Then
                        ScorebookEntry = $"{ScorebookEntry}-{Details(cnt)}"
                    End If
                Next
            ElseIf EventName.ToUpper.Contains("TRIPLE PLAY") Or EventName.ToUpper.Contains("TP") Then
                ScorebookEntry = $"TP{Details(0)}"
                For cnt As Integer = 1 To Details.Count - 1
                    If Details(cnt) <> Details(cnt - 1) Then
                        ScorebookEntry = $"{ScorebookEntry}-{Details(cnt)}"
                    End If
                Next
            ElseIf EventName.ToUpper.Contains("CAUGHT STEALING") Then
                ScorebookEntry = $"CS{Details(0)}"
                For cnt As Integer = 1 To Details.Count - 1
                    If Details(cnt) <> Details(cnt - 1) Then
                        ScorebookEntry = $"{ScorebookEntry}-{Details(cnt)}"
                    End If
                Next
            ElseIf EventName.ToUpper.Contains("GROUNDOUT") Then
                ScorebookEntry = $"G{Details(0)}"
                For cnt As Integer = 1 To Details.Count - 1
                    If Details(cnt) <> Details(cnt - 1) Then
                        ScorebookEntry = $"{ScorebookEntry}-{Details(cnt)}"
                    End If
                Next
            ElseIf EventName.ToUpper.Contains("SAC") Then
                ScorebookEntry = $"SAC{Details(0)}"
                For cnt As Integer = 1 To Details.Count - 1
                    If Details(cnt) <> Details(cnt - 1) Then
                        ScorebookEntry = $"{ScorebookEntry}-{Details(cnt)}"
                    End If
                Next
            ElseIf EventName.ToUpper.Contains("FIELDERS CHOICE") Then
                ScorebookEntry = $"FC{Details(0)}"
                For cnt As Integer = 1 To Details.Count - 1
                    If Details(cnt) <> Details(cnt - 1) Then
                        ScorebookEntry = $"{ScorebookEntry}-{Details(cnt)}"
                    End If
                Next
            ElseIf EventName.ToUpper.Contains("ERROR") Then
                ScorebookEntry = $"E{Details(0)}"
            Else
                ScorebookEntry = $"UNK ({EventName}-"
                For cnt As Integer = 1 To Details.Count - 1
                    If Details(cnt) <> Details(cnt - 1) Then
                        ScorebookEntry = $"{ScorebookEntry}-{Details(cnt)}"
                    End If
                Next
                ScorebookEntry = $"{ScorebookEntry})"
            End If

            'Trace.WriteLine($"Official Scoring = {OfficialScoring}")
        Catch ex As Exception
            Trace.WriteLine($"ERROR: CreateScorebookEntry - {ex}")
        End Try
        Return ScorebookEntry

    End Function



End Class

'<SDG><

