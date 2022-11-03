' =========================================================================================================
' (C) 2022 MSRoth
'
' Released under XXX license.
' =========================================================================================================

Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports Newtonsoft.Json.Linq
Imports NLog


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

    Shared ReadOnly mGAME_STATUS_FUTURE_LABELS As String() = {"SCHEDULED", "PRE-GAME", "DELAYED", "POSTPONED"}
    Shared ReadOnly mGAME_STATUS_PRESENT_LABLES As String() = {"WARMUP", "IN PROGRESS", "MANAGER", "OFFICIAL"}
    Shared ReadOnly mGAME_STATUS_PAST_LABELS As String() = {"FINAL", "COMPLETE", "GAME OVER", "COMPLETED"}
    Public Shared ReadOnly mGAME_STATUS_FUTURE = 1
    Public Shared ReadOnly mGAME_STATUS_PRESENT = 0
    Public Shared ReadOnly mGAME_STATUS_PAST = -1

    Shared ReadOnly Logger As Logger = LogManager.GetCurrentClassLogger()

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
        Logger.Warn($"CheckGameStatus - unknown status: {GameStatus}")
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
                Logger.Error(ex)
            End Try
            Return dt
        End Get
    End Property

    Public ReadOnly Property RHE() As DataTable
        Get
            Return Me.mRHE
        End Get
    End Property

    Public ReadOnly Property Data() As JObject
        Get
            Return Me.mData
        End Get
    End Property

    Public ReadOnly Property LiveData() As JObject
        Get
            Return Me.mLiveData
        End Get
    End Property

    Public ReadOnly Property LineScoreData() As JObject
        Get
            Return Me.mLineData
        End Get
    End Property

    Public ReadOnly Property BoxScoreData() As JObject
        Get
            Return Me.mBoxData
        End Get
    End Property

    Public ReadOnly Property GameData() As JObject
        Get
            Return Me.mGameData
        End Get
    End Property

    Public ReadOnly Property CurrentPlayData() As JObject
        Get
            Return Me.mCurrentPlayData
        End Get
    End Property

    Public ReadOnly Property AllPlaysData() As JObject
        Get
            Return Me.mAllPlaysData
        End Get
    End Property

    Public ReadOnly Property Balls() As String
        Get
            Return Me.mBalls
        End Get
    End Property

    Public ReadOnly Property Strikes() As String
        Get
            Return Me.mStrikes
        End Get
    End Property

    Public ReadOnly Property Outs() As String
        Get
            Return Me.mOuts
        End Get
    End Property

    Public ReadOnly Property PitchCount() As String
        Get
            Return Me.mPitchCount
        End Get
    End Property

    Public Sub New(GamePk As String)
        Me.mGamePk = Convert.ToInt32(GamePk)
        LoadGameData()
        Logger.Debug($"New Game object created for id = {GamePk}")
    End Sub

    Private Sub RefreshMLBData()
        Try
            mData = mAPI.ReturnLiveFeedData(Me.mGamePk)
            mLiveData = mData.SelectToken("liveData")
            mLineData = mLiveData.SelectToken("linescore")
            mBoxData = mLiveData.SelectToken("boxscore")
            mGameData = mData.SelectToken("gameData")
            mAllPlaysData = mLiveData.SelectToken("plays")
            mCurrentPlayData = mLiveData.SelectToken("plays.currentPlay")
        Catch ex As Exception
            Logger.Error(ex)
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

            ' venue weather
            Me.mVenueWeather = $"Weather: {mGameData.SelectToken("weather.temp")}°F, {mGameData.SelectToken("weather.condition")}"

            ' game status
            Me.mGameStatus = mGameData.SelectToken("status.detailedState").ToString().ToUpper()
            Logger.Debug($"Game status = {Me.mGameStatus}")

            ' load RHE table
            Me.LoadRHE()

            ' load innings
            Me.LoadInnings()

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
                Logger.Warn($"LoadGameData - unknown status: {Me.GameStatus()}")
            End If

            ' save data for debugging
            If mProperties.GetProperty(mProperties.mKEEP_DATA_FILES_KEY, "0") = "1" Then
                Dim DataRoot As String = mProperties.GetProperty(mProperties.mDATA_FILES_PATH_KEY)
                File.WriteAllText($"{DataRoot}\\{Me.GamePk()}-{Me.AwayTeam.Abbr}-{Me.HomeTeam.Abbr}_data.json", mData.ToString())
                Logger.Info($"Writing data file: {DataRoot}\\{Me.GamePk()}-{Me.AwayTeam.Abbr}-{Me.HomeTeam.Abbr}_data.json")
            End If

        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Public Function GameTitleDate() As String
        Return $"{mAwayTeam.FullName} @ {mHomeTeam.FullName} - {DateTime.Parse(mGameDateTime).ToString("f")}"
    End Function

    Public Function GameTitleFull() As String
        Return $"{mAwayTeam.FullName} @ {mHomeTeam.FullName} (Game: {mGamePk}) - {DateTime.Parse(mGameDateTime).ToString("f")}"
    End Function

    Public Function GameTitleGamePk() As String
        Return $"{mAwayTeam.FullName} @ {mHomeTeam.FullName} (Game: {mGamePk})"
    End Function

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
            Logger.Error(ex)
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

                Me.mRHE.Rows(0).Item("R") = Me.mLineData.SelectToken("teams.away.runs").ToString()
                Me.mRHE.Rows(0).Item("H") = Me.mLineData.SelectToken("teams.away.hits").ToString()
                Me.mRHE.Rows(0).Item("E") = Me.mLineData.SelectToken("teams.away.errors").ToString()

                Me.mRHE.Rows(1).Item("R") = Me.mLineData.SelectToken("teams.home.runs").ToString()
                Me.mRHE.Rows(1).Item("H") = Me.mLineData.SelectToken("teams.home.hits").ToString()
                Me.mRHE.Rows(1).Item("E") = Me.mLineData.SelectToken("teams.home.errors").ToString()
            End If
        Catch ex As Exception
            Logger.Error(ex)
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
            Logger.Error(ex)
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
            Logger.Error(ex)
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
            Logger.Error(ex)
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
            Logger.Error(ex)
        End Try
        Return batterStats
    End Function

    Function GetCurrentBatterId() As String
        Try
            Dim batterId As String = Me.mCurrentPlayData.SelectToken("matchup.batter.id").ToString
            Return batterId
        Catch ex As Exception
            Logger.Error(ex)
        End Try
        Return ""
    End Function

    Function GetCurrentPitcherId() As String
        Try
            Dim pitcherId As String = Me.mCurrentPlayData.SelectToken("matchup.pitcher.id").ToString
            Return pitcherId
        Catch ex As Exception
            Logger.Error(ex)
        End Try
        Return ""
    End Function

    Public Function GetDueUpBatters() As String
        Dim lastBatterId As String = 0
        Dim battingTeam As MlbTeam
        Dim sb As StringBuilder = New StringBuilder()

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
                Logger.Debug($"due ups:  last batter = {lastBatterId}")

                ' find last batter position in list of batting order players
                For Each key In battingTeam.Lineup.Keys
                    If battingTeam.Lineup.Item(key).Id = lastBatterId Then
                        LastBattingPosition = key
                    End If
                Next

            Else
                ' set last batter id = batter #9 in lineup, which will
                ' result in first three batters due up
                LastBattingPosition = 9  ' zero-based array
            End If
            Logger.Debug($"due ups: last batter position = {LastBattingPosition}")

            ' figure out next three batters
            For i As Integer = 1 To 3
                'For DueUpPosition As Integer = LastBattingPosition + 1 To LastBattingPosition + 3
                Dim DueUpPosition As Integer = LastBattingPosition + i
                If DueUpPosition > 9 Then
                    DueUpPosition -= 9
                End If

                sb.Append($"  {battingTeam.Lineup.Item(DueUpPosition).FullName} {Me.GetBatterStats(battingTeam.Lineup.Item(DueUpPosition).Id, battingTeam)}")
                sb.Append(vbCr)
                Logger.Debug($"due ups:  next batter = {battingTeam.Lineup.Item(DueUpPosition).Id}")
            Next

        Catch ex As Exception
            Logger.Error(ex)
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
            Logger.Error(ex)
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
            Logger.Error(ex)
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
            Logger.Error(ex)
        End Try
        Return matchup
    End Function

    Public Function LastPlayScorebookEntry() As String
        Dim Scorebook As String = ""
        Try
            Dim currentPlayIdx As Integer = Me.LiveData.SelectToken("plays.currentPlay.atBatIndex")
            currentPlayIdx -= 1
            If currentPlayIdx <= 0 Then
                Return ""
            Else
                Scorebook = CreateScorebookEntry(currentPlayIdx)
            End If
            Logger.Debug($"Last play: {Scorebook}")
        Catch ex As Exception
            Logger.Error(ex)
        End Try
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

            Dim Details As New List(Of String)
            For r As Integer = 0 To lastPlayData.SelectToken("runners").Count - 1
                Dim RunnersData As JObject = lastPlayData.SelectToken("runners").Item(r)
                Dim Credits As JArray = RunnersData.SelectToken("credits")

                If Credits IsNot Nothing Then
                    For c As Integer = 0 To Credits.Count - 1
                        Dim PositionCode As String = Credits.Item(c).SelectToken("position.code")
                        Dim CreditType As String = Credits.Item(c).SelectToken("credit")
                        Details.Add(PositionCode)

                    Next
                End If
            Next

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
            ElseIf EventName.ToUpper.Contains("STOLEN") Then
                ScorebookEntry = $"{EventName.ToUpper}"
            Else
                ScorebookEntry = $"UNK ({EventName}-"
                For cnt As Integer = 1 To Details.Count - 1
                    If Details(cnt) <> Details(cnt - 1) Then
                        ScorebookEntry = $"{ScorebookEntry}-{Details(cnt)}"
                    End If
                Next
            End If

            ScorebookEntry = $"{ScorebookEntry}"

            Logger.Info($"Official Scoring = {ScorebookEntry}")
        Catch ex As Exception
            Logger.Error(ex)
        End Try
        Return ScorebookEntry

    End Function

    Public Function GetPlaySummary() As DataTable

        Dim dt As DataTable = New DataTable()

        ' init cols
        Dim ColNames As String() = {"Index", "Inning", "Half", "Out", "Scorebook", "Commentary"}
        For Each name As String In ColNames
            Dim col As DataColumn = New DataColumn()
            col.ColumnName = name
            dt.Columns.Add(col)
        Next

        ' get play summary data
        Try
            For Each play As JObject In Me.AllPlaysData.SelectToken("allPlays")
                Dim Inning As String = play.SelectToken("about.inning")
                Dim Half As String = play.SelectToken("about.halfInning")
                Dim Out As String = play.SelectToken("count.outs")
                Dim Commentary As String = play.SelectToken("result.description")
                Dim Index As String = play.SelectToken("about.atBatIndex")
                Dim Scorebook As String = Me.CreateScorebookEntry(Convert.ToInt32(Index))

                Dim row As DataRow = dt.NewRow()
                row.Item("Index") = Index
                row.Item("Inning") = Inning
                row.Item("Half") = Half.ToUpper()
                row.Item("Out") = Out
                row.Item("Scorebook") = Scorebook
                row.Item("Commentary") = Commentary

                dt.Rows.Add(row)
            Next
        Catch ex As Exception
            Logger.Error(ex)
        End Try
        Return dt

    End Function

    Public Function GetBoxscorePitchingNotes() As List(Of String)
        Dim notes As New List(Of String)

        Try
            ' pitching notes
            Dim PitchingNotes As JArray = Me.BoxScoreData.SelectToken("pitchingNotes")
            For i As Integer = 0 To PitchingNotes.Count - 1
                notes.Add($"{PitchingNotes.Item(i)}")
            Next
        Catch ex As Exception
            Logger.Error(ex)
        End Try

        Return notes
    End Function

    Public Function GetBoxscoreGameNotes() As Dictionary(Of String, String)
        Dim notes As New Dictionary(Of String, String)

        Try
            Dim GameInfo As JArray = Me.BoxScoreData.SelectToken("info")
            For i As Integer = 0 To GameInfo.Count - 1
                Dim info As JObject = GameInfo.Item(i)
                notes.Add($"{info.SelectToken("label")}", $"{info.SelectToken("value")}")
            Next
        Catch ex As Exception
            Logger.Error(ex)
        End Try

        Return notes
    End Function

    Public Function GetBoxscoreBattingNotes(AwayOrHome As String) As List(Of String)
        Dim notes As New List(Of String)

        Try
            Dim data As JArray = BoxScoreData.SelectToken($"teams.{AwayOrHome}.note")
            For Each note As JObject In data
                Dim label As String = note.SelectToken("label")
                Dim value As String = note.SelectToken("value")
                notes.Add($"{label} - {value}")
            Next
        Catch ex As Exception
            Logger.Error(ex)
        End Try
        Return notes
    End Function

    Public Function GetBoxscoreFieldingNotes(AwayOrHome) As Dictionary(Of String, List(Of String))
        Dim notes As New Dictionary(Of String, List(Of String))

        Try
            Dim Data As JArray = BoxScoreData.SelectToken($"teams.{AwayOrHome}.info")
            For Each info As JObject In Data
                Dim title As String = info.SelectToken("title")
                Dim fields As JArray = info.SelectToken("fieldList")
                Dim list As New List(Of String)
                For Each field As JObject In fields
                    Dim label As String = field.SelectToken("label")
                    Dim value As String = field.SelectToken("value")
                    list.Add($"{label} - {value}")
                Next
                notes.Add(title, list)
            Next

        Catch ex As Exception
            Logger.Error(ex)
        End Try
        Return notes
    End Function
    Public Function GetPlayerGameBattingStats(PlayerId As String, AwayOrHome As String) As Dictionary(Of String, String)
        Return GetPlayerGameStats(PlayerId, AwayOrHome, "batting")
    End Function

    Public Function GetPlayerGamePitchingStats(PlayerId As String, AwayOrHome As String) As Dictionary(Of String, String)
        Return GetPlayerGameStats(PlayerId, AwayOrHome, "pitching")
    End Function

    Public Function GetPlayerGameFieldingStats(PlayerId As String, AwayOrHome As String) As Dictionary(Of String, Dictionary(Of String, String))
        ' TODO - a player could play multiple positions in a single game
        ' right now hard coded until example data can be found
        Dim stats As New Dictionary(Of String, Dictionary(Of String, String))
        Dim statDic As Dictionary(Of String, String)
        Try
            statDic = GetPlayerGameStats(PlayerId, AwayOrHome, "fielding")
            Dim position As String = BoxScoreData.SelectToken($"teams.{AwayOrHome}.players.ID{PlayerId}.position.abbreviation")
            stats.Add(position, statDic)
        Catch ex As Exception
            Logger.Error(ex)
        End Try

        Return stats
    End Function

    Private Function GetPlayerGameStats(PlayerId As String, AwayOrHome As String, group As String) As Dictionary(Of String, String)
        ' group = batting, fielding, pitching
        Dim stats As New Dictionary(Of String, String)
        Dim TypeStats As JObject = BoxScoreData.SelectToken($"teams.{AwayOrHome}.players.ID{PlayerId}.stats.{group}")

        Try
            If TypeStats IsNot Nothing Then
                For Each stat As JProperty In TypeStats.Properties
                    stats.Add(stat.Name, stat.Value)
                Next
            End If

        Catch ex As Exception
            Logger.Error(ex)
        End Try

        Return stats
    End Function


End Class

'<SDG><

