﻿Imports System.IO
Imports Newtonsoft.Json.Linq

Public Class MlbPlayerStats

    Private mAPI As MlbApi = New MlbApi()
    Private mThisPlayer As MlbPlayer = Nothing
    Private mLiveData As JObject
    Private mBoxscoreData As JObject
    Private mGamePk As Integer = 0
    Private mAwayOrHome As String = ""
    Private mBattingGameStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mBattingSeasonStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mBattingCareerStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mPitchingGameStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mPitchingSeasonStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mPitchingCareerStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mFieldingGameStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mFieldingSeasonStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mFieldingCareerStats As Dictionary(Of String, String) = New Dictionary(Of String, String)

    Property Player As MlbPlayer
        Set(ThisPlayer As MlbPlayer)
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

        ' get a full player object
        Me.mThisPlayer = Player.ConvertToFullObject()

        ' set labels
        Me.lblPlayer.Text = $"{Me.mThisPlayer.FullName} (#{Me.mThisPlayer.Number})"
        Me.lblPosition.Text = Me.mThisPlayer.FullPosition
        Me.lblPlayerID.Text = $"ID: {Me.mThisPlayer.Id}"

        ' make API calls to get data
        Me.Cursor = Cursors.WaitCursor
        Me.mLiveData = mAPI.ReturnLiveFeedData(Me.mGamePk).SelectToken("liveData")
        Me.mBoxscoreData = mLiveData.SelectToken("boxscore")
        Me.Cursor = Cursors.Default

        ' parse stats and load into grids
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

        ' array of stats to collect
        Dim FieldingStatsKeys As String() = {"gamesPlayed", "assists", "putOuts", "errors", "chances", "fielding", "innings", "doublePlays",
                                             "triplePlays", "throwingErrors"}

        ' TOTO need to add AVG and OPS
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

        Me.Cursor = Cursors.WaitCursor

        ' find player in data
        For Each aPlayer As JProperty In Me.mBoxscoreData.SelectToken($"teams.{AwayOrHome().ToLower()}.players")
            Dim pId As String = aPlayer.Value.Item("person").Item("id")
            If Me.mThisPlayer.Id = pId Then

                ' get pitching
                PitchingSeasonStatsData = Me.mAPI.ReturnPlayerStats(Me.mThisPlayer.Id, "season", "pitching")
                PitchingGameStatsData = aPlayer.Value.Item("stats").Item("pitching")
                PitchingCareerStatsData = Me.mAPI.ReturnPlayerStats(Me.mThisPlayer.Id, "career", "pitching")

                ' get batting stats
                BattingSeasonStatsData = Me.mAPI.ReturnPlayerStats(Me.mThisPlayer.Id, "season", "hitting")
                BattingGameStatsData = aPlayer.Value.Item("stats").Item("batting")
                BattingCareerStatsData = Me.mAPI.ReturnPlayerStats(Me.mThisPlayer.Id, "career", "hitting")

                ' get fielding stats
                FieldingSeasonStatsData = Me.mAPI.ReturnPlayerStats(Me.mThisPlayer.Id, "season", "fielding")
                FieldingGameStatsData = aPlayer.Value.Item("stats").Item("fielding")
                FieldingCareerStatsData = Me.mAPI.ReturnPlayerStats(Me.mThisPlayer.Id, "career", "fielding")

                Exit For
            End If
        Next

        Me.Cursor = Cursors.Default

        ' process all that data

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

        ' only pulls stats for primary postion
        ' TODO - pull stats for all positions played.  Will require different UI
        If FieldingSeasonStatsData IsNot Nothing Then
            Dim stats As JArray = FieldingSeasonStatsData.SelectToken("people[0].stats[0].splits")
            For i = 0 To stats.Count - 1
                Dim statsdata As JObject = stats.Item(i)
                Dim position As String = statsdata.SelectToken("position.abbreviation")
                If position.ToUpper() = Me.mThisPlayer.ShortPosition Then
                    For Each key As String In FieldingStatsKeys
                        Dim value As String = statsdata.SelectToken($"stat.{key}")
                        Me.mFieldingSeasonStats.Add(key, value)
                    Next
                End If
            Next
        End If

        ' ditto above
        If FieldingCareerStatsData IsNot Nothing Then
            Dim stats As JArray = FieldingCareerStatsData.SelectToken("people[0].stats[0].splits")
            For i = 0 To stats.Count - 1
                Dim statsdata As JObject = stats.Item(i)
                Dim position As String = statsdata.SelectToken("position.abbreviation")
                If position.ToUpper() = Me.mThisPlayer.ShortPosition Then
                    For Each key As String In FieldingStatsKeys
                        Dim value As String = statsdata.SelectToken($"stat.{key}")
                        Me.mFieldingCareerStats.Add(key, value)
                    Next
                End If
            Next
        End If

        Dim dt As DataTable = InitDataGrids()
        For Each key As String In mFieldingSeasonStats.Keys
            Dim dr As DataRow = dt.NewRow()
            dr.Item("Stat") = key
            If mFieldingGameStats.ContainsKey(key) Then
                dr.Item("Game") = mFieldingGameStats(key)
            End If
            If mFieldingSeasonStats.ContainsKey(key) Then
                dr.Item("Season") = mFieldingSeasonStats(key)
            End If
            If mFieldingCareerStats.ContainsKey(key) Then
                dr.Item("Career") = mFieldingCareerStats(key)
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
            Dim stats As JArray = BattingSeasonStatsData.SelectToken("people[0].stats[0].splits")
            For i = 0 To stats.Count - 1
                Dim statsdata As JObject = stats.Item(i)
                For Each key As String In BattingStatsKeys
                    Dim value As String = statsdata.SelectToken($"stat.{key}")
                    If mBattingSeasonStats.ContainsKey(key) Then
                        mBattingSeasonStats(key) = value
                    Else
                        Me.mBattingSeasonStats.Add(key, value)
                    End If

                Next
            Next
        End If

        If BattingCareerStatsData IsNot Nothing Then
            Dim stats As JArray = BattingCareerStatsData.SelectToken("people[0].stats[0].splits")
            For i = 0 To stats.Count - 1
                Dim statsdata As JObject = stats.Item(i)
                For Each key As String In BattingStatsKeys
                    Dim value As String = statsdata.SelectToken($"stat.{key}")
                    If mBattingCareerStats.ContainsKey(key) Then
                        Me.mBattingCareerStats(key) = value
                    Else
                        Me.mBattingCareerStats.Add(key, value)
                    End If

                Next
            Next
        End If

        dt = InitDataGrids()
        For Each key As String In mBattingSeasonStats.Keys
            Dim dr As DataRow = dt.NewRow()
            dr.Item("Stat") = key
            If mBattingGameStats.ContainsKey(key) Then
                dr.Item("Game") = mBattingGameStats(key)
            End If
            If mBattingSeasonStats.ContainsKey(key) Then
                dr.Item("Season") = mBattingSeasonStats(key)
            End If
            If mBattingCareerStats.ContainsKey(key) Then
                dr.Item("Career") = mBattingCareerStats(key)
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
                Dim stats As JArray = PitchingSeasonStatsData.SelectToken("people[0].stats[0].splits")
                For i = 0 To stats.Count - 1
                    Dim statsdata As JObject = stats.Item(i)
                    'Dim position As String = statsdata.SelectToken("position.abbreviation")
                    'If position.ToUpper() = Me.mThisPlayer.ShortPosition Then
                    For Each key As String In PitchingStatsKeys
                        Dim value As String = statsdata.SelectToken($"stat.{key}")
                        Me.mPitchingSeasonStats.Add(key, value)
                    Next
                    'End If
                Next
            End If

            If PitchingCareerStatsData IsNot Nothing Then
                Dim stats As JArray = PitchingCareerStatsData.SelectToken("people[0].stats[0].splits")
                For i = 0 To stats.Count - 1
                    Dim statsdata As JObject = stats.Item(i)
                    'Dim position As String = statsdata.SelectToken("position.abbreviation")
                    'If position.ToUpper() = Me.mThisPlayer.ShortPosition Then
                    For Each key As String In PitchingStatsKeys
                        Dim value As String = statsdata.SelectToken($"stat.{key}")
                        Me.mPitchingCareerStats.Add(key, value)
                    Next
                    'End If
                Next
            End If

            dt = InitDataGrids()
            For Each key As String In mPitchingSeasonStats.Keys
                Dim dr As DataRow = dt.NewRow()
                dr.Item("Stat") = key
                If mPitchingGameStats.ContainsKey(key) Then
                    dr.Item("Game") = mPitchingGameStats(key)
                End If
                If mPitchingSeasonStats.ContainsKey(key) Then
                    dr.Item("Season") = mPitchingSeasonStats(key)
                End If
                If mPitchingCareerStats.ContainsKey(key) Then
                    dr.Item("Career") = mPitchingCareerStats(key)
                End If
                dt.Rows.Add(dr)
            Next
            dgvPitchingStats.DataSource = dt


        dgvBattingStats.ColumnHeadersDefaultCellStyle.Font = New Font(dgvBattingStats.DefaultFont, FontStyle.Bold)
        dgvFieldingStats.ColumnHeadersDefaultCellStyle.Font = New Font(dgvFieldingStats.DefaultFont, FontStyle.Bold)
        dgvPitchingStats.ColumnHeadersDefaultCellStyle.Font = New Font(dgvPitchingStats.DefaultFont, FontStyle.Bold)

    End Sub


End Class

'<SDG><

