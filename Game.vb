Imports System.IO
Imports System.Text
Imports Newtonsoft.Json.Linq


Public Class Game

    Private mGamePk As String
    Private mAwayTeam As Team
    Private mHomeTeam As Team
    Private mGameDateTime As String
    Private mVenueWeather As String
    Private mGameStatus As String
    Private mAwaySchedPitcherId As String
    Private mHomeSchedPitcherId As String
    Private mAwayTeamRuns As Integer
    Private mHomeTeamRuns As Integer
    Private mWinningTeam As Team
    Private mLosingTeam As Team
    Private mWinningPitcherId As String
    Private mLosingPitcherId As String
    Private mSavePitcherId As String
    Private mCurrentInning As Integer
    Private mCurrentInningHalf As String
    Private mCurrentInningState As String
    Private mInnings As DataTable
    Private mRHE As DataTable
    Private mData As JObject
    Private mLiveData As JObject
    Private mLineData As JObject
    Private mBoxData As JObject
    Private mGameData As JObject
    Private mCurrentPlayData As JObject
    Private mAllPlaysData As JObject
    Private mAPI As MLB_API = New MLB_API()


    Public ReadOnly Property GamePk() As Integer
        Get
            Return Me.mGamePk
        End Get
    End Property

    Public ReadOnly Property AwayTeam() As Team
        Get
            Return Me.mAwayTeam
        End Get
    End Property

    Public ReadOnly Property HomeTeam() As Team
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

    Public ReadOnly Property AwayTeamRuns() As Integer
        Get
            Return Me.mAwayTeamRuns
        End Get
    End Property

    Public ReadOnly Property HomeTeamRuns() As Integer
        Get
            Return Me.mHomeTeamRuns
        End Get
    End Property

    Public ReadOnly Property WinningTeam() As Team
        Get
            Return Me.mWinningTeam
        End Get
    End Property

    Public ReadOnly Property LosingTeam() As Team
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

    Public Sub New(gamePk As String)
        Me.mGamePk = Convert.ToInt32(gamePk)
        LoadGameData()
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
            Trace.WriteLine($"ERROR: RefreshMLBData - {ex}")
        End Try
    End Sub

    Public Sub LoadGameData()

        Try
            RefreshMLBData()
            File.WriteAllText($"c:\\temp\\{Me.GamePk()}-gamedata.json", mData.ToString())
            File.WriteAllText($"c:\\temp\\{Me.GamePk()}-boxdata.json", mBoxData.ToString())
            File.WriteAllText($"c:\\temp\\{Me.GamePk()}-linedata.json", mLineData.ToString())

            ' get teams
            Me.mAwayTeam = New Team(Convert.ToInt32(mBoxData.SelectToken("teams.away.team.id")))
            Me.mHomeTeam = New Team(Convert.ToInt32(mBoxData.SelectToken("teams.home.team.id")))

            ' set teams win/loss record
            ' TODO this should be done in the Team object but need to do it without another API call
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

            ' load team player data
            Me.AwayTeam.LoadLineupAndRosterData(Me)
            Me.HomeTeam.LoadLineupAndRosterData(Me)

            If Me.GameStatus() = "IN PROGRESS" Or
                Me.GameStatus().Contains("MANAGER") Or
                Me.GameStatus().Contains("OFFICIAL") Then

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

            End If


            If Me.GameStatus() = "SCHEDULED" Or
                Me.GameStatus() = "WARMUP" Or
                Me.GameStatus() = "PRE-GAME" Or
                Me.GameStatus().StartsWith("DELAYED") Or
                Me.GameStatus() = "POSTPONED" Then

                Me.mAwaySchedPitcherId = mGameData.SelectToken("probablePitchers.away.id")
                Me.mHomeSchedPitcherId = mGameData.SelectToken("probablePitchers.home.id")
            End If


            If Me.GameStatus() = "FINAL" Or
                Me.GameStatus() = "COMPLETE" Or
                Me.GameStatus() = "GAME OVER" Then

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

            End If
            Trace.WriteLine($"Loading game {Me.GamePk} {Me.AwayTeam.Abbr} @ {Me.HomeTeam.Abbr} data")
        Catch ex As Exception
            Trace.WriteLine($"ERROR: LoadGameData - {ex}")
        End Try
    End Sub


    Public Function ToString() As String
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
        sb.Append($"Away Team: {Me.AwayTeam().FullName()} - {Me.HomeTeamRuns()} runs")
        sb.Append(vbCr)
        sb.Append($"Home Team: {Me.HomeTeam().FullName()} - {Me.AwayTeamRuns()} runs")
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
            If Me.GameStatus() = "IN PROGRESS" Or
                Me.GameStatus() = "FINAL" Or
                Me.GameStatus() = "COMPLETE" Or
                Me.GameStatus() = "GAME OVER" Or
                Me.GameStatus().Contains("MANAGER") Or
                Me.GameStatus().Contains("OFFICIAL") Then
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

    Public Function GetPitchingMatchup() As String
        Dim matchup As String = ""

        Try
            Dim awayPitcherName As String
            If Me.AwayTeam.GetPlayer(Me.AwayScheduledPitcherId()) Is Nothing Then
                awayPitcherName = "Unknown"
            Else
                awayPitcherName = Me.AwayTeam.GetPlayer(Me.AwayScheduledPitcherId()).Name
            End If

            Dim homePitcherName As String
            If Me.HomeTeam.GetPlayer(Me.HomeScheduledPitcherId()) Is Nothing Then
                homePitcherName = "Unknown"
            Else
                homePitcherName = Me.HomeTeam.GetPlayer(Me.HomeScheduledPitcherId()).Name
            End If

            Dim awayPitchingStats As String = Me.GetPitchingStats(Me.AwayScheduledPitcherId(), Me.AwayTeam())
            Dim homePitchingStats As String = GetPitchingStats(Me.HomeScheduledPitcherId(), Me.HomeTeam())
            matchup = String.Format("Preview: {0} {1} vs. {2} {3}", awayPitcherName, awayPitchingStats, homePitcherName, homePitchingStats)
        Catch ex As Exception
            Trace.WriteLine($"ERROR: GetPitchingMatchup - {ex}")
        End Try
        Return matchup
    End Function

    Public Function GetPitchingStats(pitcherId As Integer, ThisTeam As Team) As String
        Dim pitchingStats As String = String.Empty

        Try
            Dim playerData As JObject
            If ThisTeam.Id = Me.AwayTeam.Id Then
                playerData = Me.BoxScoreData().SelectToken("teams.away.players")
            Else
                playerData = Me.BoxScoreData().SelectToken("teams.home.players")
            End If
            ' TODO - load this data into Player object

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

    Public Function GetBatterStats(batterId As Integer, ThisGame As Game) As String
        Dim batterStats As String = String.Empty

        Try
            Dim playerData As JObject
            If ThisGame.AwayTeam.Id = Me.AwayTeam.Id Then
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
                End If
            Next
        Catch ex As Exception
            Trace.WriteLine($"ERROR: GetBatterStats - {ex}")
        End Try
        Return batterStats
    End Function


    Function ConvertDateTimeToLocalTimeZone() As String

        Dim gameTime As String = mGameData.SelectToken("datetime.dateTime").ToString()
        Dim gameTZ As String = mGameData.SelectToken("venue.timeZone.tz").ToString()
        Dim gameTZI As TimeZoneInfo
        If gameTZ = "EDT" Then
            gameTZI = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")
        ElseIf gameTZ = "EST" Then
            gameTZI = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")
        ElseIf gameTZ = "CDT" Then
            gameTZI = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")
        ElseIf gameTZ = "CST" Then
            gameTZI = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time")
        ElseIf gameTZ = "MDT" Then
            gameTZI = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time")
        ElseIf gameTZ = "MST" Then
            gameTZI = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time")
        ElseIf gameTZ = "PDT" Then
            gameTZI = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")
        ElseIf gameTZ = "PST" Then
            gameTZI = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time")
        End If

        Dim gt As DateTime = DateTime.Parse(gameTime)
        Dim ds As String = TimeZoneInfo.ConvertTime(gt, gameTZI, TimeZoneInfo.Local)

        Return ds
    End Function

    Function GetCurrentBatterId() As String
        Dim batterId As String = Me.mCurrentPlayData.SelectToken("matchup.batter.id").ToString
        Return batterId
    End Function

    Function GetCurrentPitcherId() As String
        Dim pitcherId As String = Me.mCurrentPlayData.SelectToken("matchup.pitcher.id").ToString
        Return pitcherId
    End Function

End Class
