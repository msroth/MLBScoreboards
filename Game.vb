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
    Private mAwayRuns As Integer
    Private mHomeRuns As Integer
    Private mWinningTeam As Team
    Private mLosingTeam As Team
    Private mWinningPitcherId As String
    Private mLosingPitcherId As String
    Private mCurrentInning As Integer
    Private mCurrentInningHalf As String
    Private mCurrentInningState As String

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

    Public ReadOnly Property AwayRuns() As Integer
        Get
            Return Me.mAwayRuns
        End Get
    End Property

    Public ReadOnly Property HomeRuns() As Integer
        Get
            Return Me.mHomeRuns
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


    Public Sub New(gamePk As String)
        Me.mGamePk = Convert.ToInt32(gamePk)
        LoadGameData()
    End Sub

    Public Sub LoadGameData()
        Dim Data As JObject = mAPI.ReturnLiveFeedData(Me.mGamePk)
        Dim LiveData As JObject = Data.SelectToken("liveData")
        Dim LineData As JObject = LiveData.SelectToken("linescore")
        Dim BoxData As JObject = LiveData.SelectToken("boxscore")
        Dim GameData As JObject = Data.SelectToken("gameData")

        File.WriteAllText($"c:\\temp\\{Me.GamePk()}-gamedata.json", GameData.ToString())
        File.WriteAllText($"c:\\temp\\{Me.GamePk()}-livedata.json", LiveData.ToString())
        File.WriteAllText($"c:\\temp\\{Me.GamePk()}-linescoredata.json", LineData.ToString())
        File.WriteAllText($"c:\\temp\\{Me.GamePk()}-boxscoredata.json", BoxData.ToString())

        ' get teams
        mAwayTeam = New Team(Convert.ToInt32(BoxData.SelectToken("teams.away.team.id")))
        mHomeTeam = New Team(Convert.ToInt32(BoxData.SelectToken("teams.home.team.id")))

        ' game date and time
        mGameDateTime = GameData.SelectToken("datetime.time").ToString() + GameData.SelectToken("datetime.ampm").ToString()

        ' venue weather
        Dim temp As String = GameData.SelectToken("weather.temp")
        Dim conditions As String = GameData.SelectToken("weather.condition")
        mVenueWeather = $"Weather: {temp}°F, {conditions}"

        ' game status
        mGameStatus = GameData.SelectToken("status.detailedState").ToString().ToUpper()


        If Me.GameStatus() = "IN PROGRESS" Or Me.GameStatus().Contains("MANAGER") Or Me.GameStatus().Contains("OFFICIAL") Then

            ' away runs
            Dim AwayRuns As String = LineData.SelectToken("teams.away.runs").ToString()
            If Not AwayRuns.Equals(String.Empty) Then
                mAwayRuns = Convert.ToInt32(AwayRuns)
            Else
                mAwayRuns = 0
            End If

            ' home runs
            Dim HomeRuns As String = LineData.SelectToken("teams.home.runs").ToString()
            If Not HomeRuns.Equals(String.Empty) Then
                mHomeRuns = Convert.ToInt32(HomeRuns)
            Else
                mHomeRuns = 0
            End If

            ' inning data
            mCurrentInning = LineData.SelectToken("currentInning")
            mCurrentInningHalf = LineData.SelectToken("inningHalf").ToString().ToUpper()
            mCurrentInningState = LineData.SelectToken("inningState").ToString().ToUpper()

        End If


        If Me.GameStatus() = "SCHEDULED" Or Me.GameStatus() = "WARMUP" Or Me.GameStatus() = "PRE-GAME" Or Me.GameStatus().StartsWith("DELAYED") Or Me.GameStatus() = "POSTPONED" Then

            mAwaySchedPitcherId = GameData.SelectToken("probablePitchers.away.id")
            mHomeSchedPitcherId = GameData.SelectToken("probablePitchers.home.id")

        End If


        If Me.GameStatus() = "FINAL" Or Me.GameStatus() = "COMPLETE" Or Me.GameStatus() = "GAME OVER" Then

            ' runs
            mAwayRuns = LineData.SelectToken("teams.away.runs")
            mHomeRuns = LineData.SelectToken("teams.home.runs")

            ' winning pitcher id
            mWinningPitcherId = LiveData.SelectToken("decisions.winner.id").ToString()

            ' losing pitcher id
            mLosingPitcherId = LiveData.SelectToken("decisions.loser.id").ToString()

            If Convert.ToInt32(mAwayRuns) > Convert.ToInt32(mHomeRuns) Then
                mWinningTeam = AwayTeam()
                mLosingTeam = HomeTeam()
            Else
                mWinningTeam = HomeTeam()
                mLosingTeam = AwayTeam()
            End If

        End If
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
        sb.Append($"Away Team: {Me.AwayTeam().FullName()} - {Me.HomeRuns()} runs")
        sb.Append(vbCr)
        sb.Append($"Home Team: {Me.HomeTeam().FullName()} - {Me.AwayRuns()} runs")
        sb.Append(vbCr)
        If Not Me.WinningTeam() Is Nothing Then
            sb.Append($"Winning Team: {Me.WinningTeam().ShortName()}")
            sb.Append(vbCr)
            sb.Append($"Winning PitcherId: {Me.WinningPitcherId()}")
            sb.Append(vbCr)
        End If

        Return sb.ToString()

    End Function

End Class
