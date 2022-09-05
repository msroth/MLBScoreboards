﻿Imports System.IO
Imports Newtonsoft.Json.Linq

Public Class PlayerStats

    Private mAPI As MLB_API = New MLB_API()
    Private mThisPlayer As Player = Nothing
    Private mLiveData As JObject
    Private mGameData As JObject
    Private mBoxscoreData As JObject
    Private mGamePk As Integer = 0
    Private mAwayOrHome As String = ""
    Private mStatsTable As DataTable
    Private mBattingGameStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mBattingSeasonStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mBattingCareerStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mGameFieldingStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mPitchingGameStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mPitchingSeasonStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mPitchingCareerStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mFieldingGameStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mFieldingSeasonStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mFieldingCareerStats As Dictionary(Of String, String) = New Dictionary(Of String, String)

    Property Player As Player
        Set(ThisPlayer As Player)
            Me.mThisPlayer = ThisPlayer
        End Set
        Get
            Return Me.mThisPlayer
        End Get
    End Property

    Property GamePk As Integer
        Set(GamePk As Integer)
            Me.mGamePk = GamePk
        End Set
        Get
            Return Me.mGamePk
        End Get
    End Property

    Property AwayOrHome As String
        Set(TeamSide As String)
            Me.mAwayOrHome = TeamSide
        End Set
        Get
            Return Me.mAwayOrHome
        End Get
    End Property
    Private Sub PlayerStats_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Player Is Nothing Or GamePk = 0 Or AwayOrHome = "" Then
            Trace.WriteLine($"ERROR - called PlayerStats form with no args")
            Close()
        End If

        ' make API calls to get data
        Me.Cursor = Cursors.WaitCursor

        ' get a full player object
        Me.mThisPlayer = Player.ConvertToFullObject()

        ' set labels
        Me.lblPlayer.Text = $"{Me.mThisPlayer.FullName} (#{Me.mThisPlayer.Number})"
        Me.lblPosition.Text = Me.mThisPlayer.FullPosition
        Me.lblPlayerID.Text = $"ID: {Me.mThisPlayer.Id}"

        ' get data
        Me.mLiveData = mAPI.ReturnLiveFeedData(Me.mGamePk).SelectToken("liveData")
        Me.mBoxscoreData = mLiveData.SelectToken("boxscore")

        LoadStats()

    End Sub

    Private Function InitDataGrids() As DataTable
        Dim dt As DataTable = New DataTable()
        Dim col As New DataColumn
        col.ColumnName = "Stat"
        dt.Columns.Add(col)
        col = New DataColumn
        col.ColumnName = "Game"
        dt.Columns.Add(col)
        col = New DataColumn
        col.ColumnName = "Season"
        dt.Columns.Add(col)
        col = New DataColumn
        col.ColumnName = "Career"
        dt.Columns.Add(col)
        Return dt
    End Function

    Sub LoadStats()
        ' == Game Stats ==
        Dim BattingGameStatsData As JObject
        Dim PitchingGameStatsData As JObject
        Dim FieldingGameStatsData As JObject

        ' == Season Stats ==
        Dim BattingSeasonStatsData As JObject
        Dim PitchingSeasonStatsData As JObject
        Dim FieldingSeasonStatsData As JObject

        ' == Career Stats ==
        Dim BattingCareerStatsData As JObject
        Dim PitchingCareerStatsData As JObject
        Dim FieldingCareerStatsData As JObject

        ' find player in data
        For Each aPlayer As JProperty In Me.mBoxscoreData.SelectToken($"teams.{AwayOrHome().ToLower()}.players")
            Dim pId As String = aPlayer.Value.Item("person").Item("id")
            If Me.mThisPlayer.Id = pId Then

                ' get pitching
                If Me.mThisPlayer.ShortPosition = "P" Then
                    PitchingSeasonStatsData = aPlayer.Value.Item("seasonStats").Item("pitching")
                    PitchingGameStatsData = aPlayer.Value.Item("stats").Item("pitching")
                    PitchingCareerStatsData = Me.mAPI.ReturnPlayerStats(Me.mThisPlayer.Id, "career", "pitching")
                End If

                ' get batting stats
                BattingSeasonStatsData = aPlayer.Value.Item("seasonStats").Item("batting")
                BattingGameStatsData = aPlayer.Value.Item("stats").Item("batting")
                BattingCareerStatsData = Me.mAPI.ReturnPlayerStats(Me.mThisPlayer.Id, "career", "hitting")

                ' get fielding stats
                FieldingSeasonStatsData = aPlayer.Value.Item("seasonStats").Item("fielding")
                FieldingGameStatsData = aPlayer.Value.Item("stats").Item("fielding")
                FieldingCareerStatsData = Me.mAPI.ReturnPlayerStats(Me.mThisPlayer.Id, "career", "fielding")

                'File.WriteAllText("c:\\temp\\battingseasonstatsdata.json", BattingSeasonStatsData.ToString)
                'File.WriteAllText("c:\\temp\\battinggamestatsdata.json", BattingGameStatsData.ToString)
                'File.WriteAllText("c:\\temp\\battingcareerstatsdata.json", BattingCareerStatsData.ToString)
                'File.WriteAllText("c:\\temp\\fieldinggamestatsdata.json", FieldingGameStatsData.ToString)
                'File.WriteAllText("c:\\temp\\fieldingseasonstatsdata.json", FieldingSeasonStatsData.ToString)
                'File.WriteAllText("c:\\temp\\fieldingcareerstatsdata.json", FieldingCareerStatsData.ToString)
                'If PitchingCareerStatsData IsNot Nothing And PitchingGameStatsData IsNot Nothing And PitchingSeasonStatsData IsNot Nothing Then
                '    File.WriteAllText("c:\\temp\\pitchinggamestatsdata.json", PitchingGameStatsData.ToString)
                '    File.WriteAllText("c:\\temp\\pitchingseasonstatsdata.json", PitchingSeasonStatsData.ToString)
                '    File.WriteAllText("c:\\temp\\pitchingcareerstatsdata.json", PitchingCareerStatsData.ToString)
                'End If

                Exit For
            End If
        Next

        Me.Cursor = Cursors.Default

        ' process all that data
        Dim FieldingStatsKeys As String() = {"gamesPlayed", "assists", "putOuts", "errors", "chances", "fielding", "innings", "doublePlays",
                                             "triplePlays", "throwingErrors"}

        Dim BattingStatsKeys As String() = {"runs", "doubles", "triples", "homeRuns", "strikeOuts", "baseOnBalls", "hits", "hitByPitch",
                                            "avg", "atBats", "obp", "slg", "ops", "caughtStealing", "stolenBases", "stolenBasePercentage",
                                            "plateAppearances", "totalBases", "rbi", "sacBunts", "babip", "atBatsPerHomeRun"}

        Dim PitchingStatsKeys As String() = {"gamesStarted", "groundOuts", "airOuts", "intentionalWalks", "numberOfPitches", "era",
                                             "inningsPitched", "wins",
                                             "losses", "saves", "holds", "blownSaves", "earnedRuns", "whip", "battersFaced", "outs",
                                             "gamesPitched", "completeGames", "shutouts", "pitchesThrown", "balls", "strikes",
                                             "strikePercentage", "hitBatsmen", "balks", "wildPitches", "pickoffs", "groundOutsToAirouts",
                                             "rbi", "winPercentage", "pitchesPerInning", "gamesFinished", "strikeoutWalkRatio",
                                             "strikeoutsPer9Inn", "walksPer9Inn", "hitsPer9Inn", "runsScoredPer9", "homeRunsPer9",
                                             "inheritedRunners", "inheritedRunnersScored", "catchersInterference", "sacBunts", "sacFlies",
                                             "passedBall"}

        ' load fielding stats
        If FieldingGameStatsData IsNot Nothing Then
            For Each key As String In FieldingStatsKeys
                Dim value = ""
                If FieldingGameStatsData.ContainsKey(key) Then
                    value = FieldingGameStatsData(key)
                End If
                Me.mFieldingGameStats.Add(key, value)
            Next
        End If

        If FieldingSeasonStatsData IsNot Nothing Then
            For Each key As String In FieldingStatsKeys
                Dim value = ""
                If FieldingSeasonStatsData.ContainsKey(key) Then
                    value = FieldingSeasonStatsData(key)
                End If
                Me.mFieldingSeasonStats.Add(key, value)
            Next
        End If

        ' career stats
        'Dim Data As JObject = JObject.Parse(File.ReadAllText("C:\\temp\\fieldingcareerstatsdata.json"))
        'Dim JData As JArray = Data.SelectToken("stats[0].splits")

        'For Each entry As JObject In JData

        '    Dim id As String = entry.SelectToken("player.id")
        '    If id = Me.mThisPlayer.Id Then
        '        Dim stats As JObject = entry.SelectToken("stat")
        '        For Each key As String In FieldingStatsKeys
        '            Dim value = ""
        '            If stats.ContainsKey(key) Then
        '                value = stats.SelectToken(key)
        '            End If
        '            Me.mFieldingCareerStats.Add(key, value)
        '        Next

        '    End If
        'Next

        Dim dt As DataTable = InitDataGrids()
        For Each key As String In mFieldingSeasonStats.Keys
            Dim dr As DataRow = dt.NewRow()
            dr.Item("Stat") = key
            If mFieldingGameStats.ContainsKey(key) Then
                dr.Item("Game") = mFieldingGameStats(key).ToString()
            End If
            If mFieldingSeasonStats.ContainsKey(key) Then
                dr.Item("Season") = mFieldingSeasonStats(key).ToString()
            End If
            If mFieldingCareerStats.ContainsKey(key) Then
                dr.Item("Career") = mFieldingCareerStats(key).ToString()
            End If
            dt.Rows.Add(dr)
        Next
        dgvFieldingStats.DataSource = dt

        ' load batting stats
        If BattingGameStatsData IsNot Nothing Then
            For Each key As String In BattingStatsKeys
                Dim value = ""
                If BattingGameStatsData.ContainsKey(key) Then
                    value = BattingGameStatsData(key)
                End If
                Me.mBattingGameStats.Add(key, value)
            Next
        End If

        If BattingSeasonStatsData IsNot Nothing Then
            For Each key As String In BattingStatsKeys
                Dim value = ""
                If BattingSeasonStatsData.ContainsKey(key) Then
                    value = BattingSeasonStatsData(key)
                End If
                Me.mBattingSeasonStats.Add(key, value)
            Next
        End If

        ' career stats

        dt = InitDataGrids()
        For Each key As String In mBattingSeasonStats.Keys
            Dim dr As DataRow = dt.NewRow()
            dr.Item("Stat") = key
            If mBattingGameStats.ContainsKey(key) Then
                dr.Item("Game") = mBattingGameStats(key).ToString()
            End If
            If mBattingSeasonStats.ContainsKey(key) Then
                dr.Item("Season") = mBattingSeasonStats(key).ToString()
            End If
            If mBattingCareerStats.ContainsKey(key) Then
                dr.Item("Career") = mBattingCareerStats(key).ToString()
            End If
            dt.Rows.Add(dr)
        Next
        dgvBattingStats.DataSource = dt

        ' load pitching stats
        If PitchingGameStatsData IsNot Nothing Then
            For Each key As String In PitchingStatsKeys
                Dim value = ""
                If PitchingGameStatsData.ContainsKey(key) Then
                    value = PitchingGameStatsData(key)
                End If
                Me.mPitchingGameStats.Add(key, value)
            Next
        End If

        If PitchingSeasonStatsData IsNot Nothing Then
            For Each key As String In PitchingStatsKeys
                Dim value = ""
                If PitchingSeasonStatsData.ContainsKey(key) Then
                    value = PitchingSeasonStatsData(key)
                End If
                Me.mPitchingSeasonStats.Add(key, value)
            Next
        End If

        ' career stats

        dt = InitDataGrids()
        For Each key As String In mPitchingSeasonStats.Keys
            Dim dr As DataRow = dt.NewRow()
            dr.Item("Stat") = key
            If mPitchingGameStats.ContainsKey(key) Then
                dr.Item("Game") = mPitchingGameStats(key).ToString()
            End If
            If mPitchingSeasonStats.ContainsKey(key) Then
                dr.Item("Season") = mPitchingSeasonStats(key).ToString()
            End If
            If mPitchingCareerStats.ContainsKey(key) Then
                dr.Item("Career") = mPitchingCareerStats(key).ToString()
            End If
            dt.Rows.Add(dr)
        Next
        dgvPitchingStats.DataSource = dt



    End Sub


End Class