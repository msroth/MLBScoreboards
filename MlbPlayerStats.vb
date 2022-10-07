Imports System.IO
Imports Newtonsoft.Json.Linq

Public Class MlbPlayerStats

    Private mAPI As MlbApi = New MlbApi()
    Private mThisPlayer As MlbPlayer = Nothing
    Private mLiveData As JObject
    Private mBoxscoreData As JObject
    Private mGamePk As Integer = 0
    Private mAwayOrHome As String = ""
    Private mBattingGameStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mBattingSeasonStats As Dictionary(Of String, Dictionary(Of String, String)) = New Dictionary(Of String, Dictionary(Of String, String))
    Private mBattingCareerStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mPitchingGameStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mPitchingSeasonStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mPitchingCareerStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mFieldingGameStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mFieldingSeasonStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mFieldingCareerStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mProperties As SBProperties = New SBProperties()

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

    Private Function InitDataGrids2() As DataTable
        Dim dt As DataTable = New DataTable()
        Dim col As New DataColumn
        col.ColumnName = "Stat"
        dt.Columns.Add(col)
        col = New DataColumn
        col.ColumnName = "Game"
        dt.Columns.Add(col)

        For Each team As String In mBattingSeasonStats.Keys
            col = New DataColumn
            col.ColumnName = $"Season ({team})"
            dt.Columns.Add(col)
        Next

        col = New DataColumn
        col.ColumnName = "Career"
        dt.Columns.Add(col)
        Return dt
    End Function


    Sub LoadStats()

        ' arrays of stats to collect
        'Dim FieldingStatsKeys As String() = {"gamesPlayed", "assists", "putOuts", "errors", "chances", "fielding", "innings", "doublePlays",
        '                                     "triplePlays", "throwingErrors"}

        Dim FieldingStatsKeys2 As New SortedDictionary(Of String, String)
        FieldingStatsKeys2.Add("gamesPlayed", "Games Played")
        FieldingStatsKeys2.Add("gamesStarted", "Games Started")
        FieldingStatsKeys2.Add("assists", "Assists")
        FieldingStatsKeys2.Add("putOuts", "Put Outs")
        FieldingStatsKeys2.Add("errors", "Errors")
        FieldingStatsKeys2.Add("chances", "Chances")
        FieldingStatsKeys2.Add("fielding", "Fielding")
        FieldingStatsKeys2.Add("innings", "Innings Played")
        FieldingStatsKeys2.Add("doublePlays", "Double Plays")
        FieldingStatsKeys2.Add("triplePlays", "Triple Plays")
        FieldingStatsKeys2.Add("throwingErrors", "Throwing Errors")
        FieldingStatsKeys2.Add("numTeams", "Number of Teams")

        'Dim BattingStatsKeys As String() = {"runs", "doubles", "triples", "homeRuns", "strikeOuts", "baseOnBalls", "hits", "hitByPitch",
        '                                    "avg", "atBats", "obp", "slg", "ops", "caughtStealing", "stolenBases", "stolenBasePercentage",
        '                                    "plateAppearances", "totalBases", "rbi", "sacBunts", "babip", "atBatsPerHomeRun"}

        Dim BattingStatsKeys2 As New SortedDictionary(Of String, String)
        BattingStatsKeys2.Add("runs", "Runs")
        BattingStatsKeys2.Add("doubles", "Doubles")
        BattingStatsKeys2.Add("triples", "Triples")
        BattingStatsKeys2.Add("homeRuns", "Home Runs")
        BattingStatsKeys2.Add("strikeOuts", "Strike Outs")
        BattingStatsKeys2.Add("baseOnBalls", "Walks")
        BattingStatsKeys2.Add("hits", "Hits")
        BattingStatsKeys2.Add("hitByPitch", "Hit By Pitch")
        BattingStatsKeys2.Add("avg", "Batting Average")
        BattingStatsKeys2.Add("atBats", "At Bats")
        BattingStatsKeys2.Add("obp", "On Base Percentage (OBP)")
        BattingStatsKeys2.Add("slg", "Slugging")
        BattingStatsKeys2.Add("ops", "OBP + Slugging (OPS)")
        BattingStatsKeys2.Add("caughtStealing", "Caught Stealing")
        BattingStatsKeys2.Add("stolenBases", "Stolen Bases")
        BattingStatsKeys2.Add("stolenBasePercentage", "Stolen Base Percentage")
        BattingStatsKeys2.Add("plateAppearances", "Plate Appearances")
        BattingStatsKeys2.Add("totalBases", "Total Bases")
        BattingStatsKeys2.Add("rbi", "RBIs")
        BattingStatsKeys2.Add("sacBunts", "Sacrifice Bunts")
        BattingStatsKeys2.Add("atBatsPerHomeRun", "ABs / Home Runs")

        'Dim PitchingStatsKeys As String() = {"gamesStarted", "groundOuts", "airOuts", "intentionalWalks", "numberOfPitches", "era",
        '                                     "inningsPitched", "wins",
        '                                     "losses", "saves", "holds", "blownSaves", "earnedRuns", "whip", "battersFaced", "outs",
        '                                     "gamesPitched", "completeGames", "shutouts", "pitchesThrown", "balls", "strikes",
        '                                     "strikePercentage", "hitBatsmen", "balks", "wildPitches", "pickoffs", "groundOutsToAirouts",
        '                                     "rbi", "winPercentage", "pitchesPerInning", "gamesFinished", "strikeoutWalkRatio",
        '                                     "strikeoutsPer9Inn", "walksPer9Inn", "hitsPer9Inn", "runsScoredPer9", "homeRunsPer9",
        '                                     "inheritedRunners", "inheritedRunnersScored", "catchersInterference", "sacBunts", "sacFlies",
        '                                     "passedBall"}

        Dim PitchingStatsKeys2 As New SortedDictionary(Of String, String)
        PitchingStatsKeys2.Add("gamesStarted", "Games Started")
        PitchingStatsKeys2.Add("groundOuts", "Ground Outs")
        PitchingStatsKeys2.Add("airOuts", "Air Outs")
        PitchingStatsKeys2.Add("intentionalWalks", "Intentional Walks")
        PitchingStatsKeys2.Add("numberOfPitches", "Number of Pitches")
        PitchingStatsKeys2.Add("era", "ERA")
        PitchingStatsKeys2.Add("inningsPitched", "Innings Pitched")
        PitchingStatsKeys2.Add("wins", "Wins")
        PitchingStatsKeys2.Add("losses", "Losses")
        PitchingStatsKeys2.Add("saves", "Saves")
        PitchingStatsKeys2.Add("holds", "Holds")
        PitchingStatsKeys2.Add("blownSaves", "Blown Saves")
        PitchingStatsKeys2.Add("earnedRuns", "Earned Runs")
        PitchingStatsKeys2.Add("whip", "WHIP")
        PitchingStatsKeys2.Add("battersFaced", "Batters Faced")
        PitchingStatsKeys2.Add("outs", "Outs")
        PitchingStatsKeys2.Add("gamesPitched", "Games Pitched")
        PitchingStatsKeys2.Add("completeGames", "Complete Games")
        PitchingStatsKeys2.Add("shutouts", "Shutouts")
        PitchingStatsKeys2.Add("pitchesThrown", "Pitches Thrown")
        PitchingStatsKeys2.Add("balls", "Balls")
        PitchingStatsKeys2.Add("strikes", "Strikes")
        PitchingStatsKeys2.Add("strikePercentage", "Percent Strikes")
        PitchingStatsKeys2.Add("hitBatsmen", "Hit Batters")
        PitchingStatsKeys2.Add("balks", "Balks")
        PitchingStatsKeys2.Add("wildPitches", "Wild Pitches")
        PitchingStatsKeys2.Add("pickoffs", "Pickoffs")
        PitchingStatsKeys2.Add("groundOutsToAirouts", "GO to AO Ration")
        PitchingStatsKeys2.Add("rbi", "RBIs")
        PitchingStatsKeys2.Add("winPercentage", "Win Percentage")
        PitchingStatsKeys2.Add("pitchesPerInning", "Pitches per Inning")
        PitchingStatsKeys2.Add("gamesFinished", "Games Finished")
        PitchingStatsKeys2.Add("strikeoutWalkRatio", "SO to Walk Ratio")
        PitchingStatsKeys2.Add("strikeoutsPer9Inn", "SO per 9 Innings")
        PitchingStatsKeys2.Add("walksPer9Inn", "Walks per 9 Innings")
        PitchingStatsKeys2.Add("hitsPer9Inn", "Hits per 9 Innings")
        PitchingStatsKeys2.Add("runsScoredPer9", "Runs per 9 Iniings")
        PitchingStatsKeys2.Add("homeRunsPer9", "HRs per 9 Innings")
        PitchingStatsKeys2.Add("inheritedRunners", "Inherited Runners")
        PitchingStatsKeys2.Add("inheritedRunnersScored", "Inherited Runners Scored")
        PitchingStatsKeys2.Add("sacBunts", "Sacrifice Bunts")
        PitchingStatsKeys2.Add("sacFlies", "Sacrifice Flies")


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

                ' save data for debugging
                If mProperties.GetProperty(mProperties.mKEEP_DATA_FILES_KEY, "0") = "1" Then
                    Dim DataRoot As String = mProperties.GetProperty(mProperties.mDATA_FILES_PATH_KEY)
                    If PitchingSeasonStatsData IsNot Nothing Then
                        File.WriteAllText($"{DataRoot}\\{Me.mThisPlayer.Id}-{Me.mThisPlayer.FullName}_pitching_season_stats_data.json", PitchingSeasonStatsData.ToString())
                    End If
                    If PitchingGameStatsData IsNot Nothing Then
                        File.WriteAllText($"{DataRoot}\\{Me.mThisPlayer.Id}-{Me.mThisPlayer.FullName}_pitching_game_stats_data.json", PitchingGameStatsData.ToString())
                    End If
                    If PitchingCareerStatsData IsNot Nothing Then
                        File.WriteAllText($"{DataRoot}\\{Me.mThisPlayer.Id}-{Me.mThisPlayer.FullName}_pitching_career_stats_data.json", PitchingCareerStatsData.ToString())
                    End If

                    If BattingSeasonStatsData IsNot Nothing Then
                        File.WriteAllText($"{DataRoot}\\{Me.mThisPlayer.Id}-{Me.mThisPlayer.FullName}_batting_season_stats_data.json", BattingSeasonStatsData.ToString())
                    End If
                    If BattingGameStatsData IsNot Nothing Then
                        File.WriteAllText($"{DataRoot}\\{Me.mThisPlayer.Id}-{Me.mThisPlayer.FullName}_batting_game_stats_data.json", BattingGameStatsData.ToString())
                    End If
                    If BattingCareerStatsData IsNot Nothing Then
                        File.WriteAllText($"{DataRoot}\\{Me.mThisPlayer.Id}-{Me.mThisPlayer.FullName}_batting_career_stats_data.json", BattingCareerStatsData.ToString())
                    End If

                    If FieldingSeasonStatsData IsNot Nothing Then
                        File.WriteAllText($"{DataRoot}\\{Me.mThisPlayer.Id}-{Me.mThisPlayer.FullName}_fielding_season_stats_data.json", FieldingSeasonStatsData.ToString())
                    End If
                    If FieldingGameStatsData IsNot Nothing Then
                        File.WriteAllText($"{DataRoot}\\{Me.mThisPlayer.Id}-{Me.mThisPlayer.FullName}_fielding_game_stats_data.json", FieldingGameStatsData.ToString())
                    End If
                    If FieldingCareerStatsData IsNot Nothing Then
                        File.WriteAllText($"{DataRoot}\\{Me.mThisPlayer.Id}-{Me.mThisPlayer.FullName}_fielding_career_stats_data.json", FieldingCareerStatsData.ToString())
                    End If

                    Exit For
                End If

            End If
        Next

        Me.Cursor = Cursors.Default

        ' process all that data

        ' load fielding stats
        'If FieldingGameStatsData IsNot Nothing Then
        '    For Each key As String In FieldingStatsKeys2.Keys
        '        Dim value = ""
        '        If FieldingGameStatsData.ContainsKey(key) Then
        '            value = FieldingGameStatsData(key)
        '        End If
        '        Me.mFieldingGameStats.Add(FieldingStatsKeys2(key), value)
        '    Next
        'End If

        '' only pulls stats for primary postion
        '' TODO - pull stats for all positions played.  Will require different UI
        'If FieldingSeasonStatsData IsNot Nothing Then
        '    Dim stats As JArray = FieldingSeasonStatsData.SelectToken("people[0].stats[0].splits")
        '    If stats IsNot Nothing Then
        '        For i = 0 To stats.Count - 1
        '            Dim statsdata As JObject = stats.Item(i)
        '            Dim position As String = statsdata.SelectToken("position.abbreviation")
        '            If position.ToUpper() = Me.mThisPlayer.ShortPosition Then
        '                For Each key As String In FieldingStatsKeys2.Keys
        '                    Dim value As String = statsdata.SelectToken($"stat.{key}")
        '                    If Me.mFieldingSeasonStats.Keys.Contains(FieldingStatsKeys2(key)) Then
        '                        Me.mFieldingSeasonStats(FieldingStatsKeys2(key)) = Me.mFieldingSeasonStats(FieldingStatsKeys2(key)) + value
        '                    Else
        '                        Me.mFieldingSeasonStats.Add(FieldingStatsKeys2(key), value)
        '                    End If

        '                Next
        '            End If
        '        Next
        '    End If
        'End If

        '' ditto above
        'If FieldingCareerStatsData IsNot Nothing Then
        '    Dim stats As JArray = FieldingCareerStatsData.SelectToken("people[0].stats[0].splits")
        '    If stats IsNot Nothing Then
        '        For i = 0 To stats.Count - 1
        '            Dim statsdata As JObject = stats.Item(i)
        '            Dim position As String = statsdata.SelectToken("position.abbreviation")
        '            If position.ToUpper() = Me.mThisPlayer.ShortPosition Then
        '                For Each key As String In FieldingStatsKeys2.Keys
        '                    Dim value As String = statsdata.SelectToken($"stat.{key}")
        '                    Me.mFieldingCareerStats.Add(FieldingStatsKeys2(key), value)
        '                Next
        '            End If
        '        Next
        '    End If
        'End If

        Dim dt As DataTable = InitDataGrids()
        'For Each key As String In mFieldingSeasonStats.Keys
        '    Dim dr As DataRow = dt.NewRow()
        '    dr.Item("Stat") = key
        '    If mFieldingGameStats.ContainsKey(key) Then
        '        dr.Item("Game") = mFieldingGameStats(key)
        '    End If
        '    If mFieldingSeasonStats.ContainsKey(key) Then
        '        dr.Item("Season") = mFieldingSeasonStats(key)
        '    End If
        '    If mFieldingCareerStats.ContainsKey(key) Then
        '        dr.Item("Career") = mFieldingCareerStats(key)
        '    End If
        '    dt.Rows.Add(dr)
        'Next
        'dgvFieldingStats.DataSource = dt

        ' load batting stats

        If BattingGameStatsData IsNot Nothing Then
            For Each key As String In BattingStatsKeys2.Keys
                Dim value = ""
                If BattingGameStatsData.ContainsKey(key) Then
                    value = BattingGameStatsData(key)
                End If
                Me.mBattingGameStats.Add(BattingStatsKeys2(key), value)
            Next
        End If

        If BattingSeasonStatsData IsNot Nothing Then
            Dim stats As JArray = BattingSeasonStatsData.SelectToken("people[0].stats[0].splits")

            If stats IsNot Nothing Then
                For i = 0 To stats.Count - 1
                    Dim statsdata As JObject = stats.Item(i)

                    ' create dic for player stats for each team played on this season
                    Dim statsDic As Dictionary(Of String, String) = New Dictionary(Of String, String)

                    Dim teamName As String
                    If i = 0 Then
                        teamName = BattingSeasonStatsData.SelectToken("people[0].currentTeam.name")
                    Else
                        teamName = statsdata.SelectToken("team.name")
                    End If

                    For Each key As String In BattingStatsKeys2.Keys
                        Dim value As String = statsdata.SelectToken($"stat.{key}")
                        If statsDic.ContainsKey(BattingStatsKeys2(key)) Then
                            statsDic(BattingStatsKeys2(key)) = value
                        Else
                            statsDic.Add(BattingStatsKeys2(key), value)
                        End If

                    Next
                    Dim dupIndex As Integer = 1
                    While mBattingSeasonStats.Keys.Contains(teamName)
                        teamName = $"{teamName}-{dupIndex}"
                        dupIndex += 1
                    End While
                    Me.mBattingSeasonStats.Add(teamName, statsDic)
                Next
            End If
        End If

        If BattingCareerStatsData IsNot Nothing Then
            Dim stats As JArray = BattingCareerStatsData.SelectToken("people[0].stats[0].splits")
            If stats IsNot Nothing Then
                For i = 0 To stats.Count - 1
                    Dim statsdata As JObject = stats.Item(i)
                    For Each key As String In BattingStatsKeys2.Keys
                        Dim value As String = statsdata.SelectToken($"stat.{key}")
                        If mBattingCareerStats.ContainsKey(key) Then
                            Me.mBattingCareerStats(key) = value
                        Else
                            Me.mBattingCareerStats.Add(BattingStatsKeys2(key), value)
                        End If

                    Next
                Next
            End If
        End If


        dt = InitDataGrids2()

        For Each key In mBattingGameStats.Keys
            Dim dr As DataRow = dt.NewRow()
            dr.Item("Stat") = key
            dr.Item("Game") = mBattingGameStats.Item(key)
            dt.Rows.Add(dr)
        Next

        For Each key In mBattingCareerStats.Keys
            Dim dr As DataRow = dt.Select($"Stat = '{key}'")(0)
            If dr Is Nothing Then
                dr = dt.NewRow()
                dr.Item("Career") = mBattingCareerStats.Item(key)
                dt.Rows.Add(dr)
            Else
                dr.Item("Career") = mBattingCareerStats.Item(key)
            End If
        Next


        For Each team As String In mBattingSeasonStats.Keys
            Dim statsDic = mBattingSeasonStats.Item(team)

            For Each key In statsDic.Keys
                Dim dr As DataRow = dt.Select($"Stat = '{key}'")(0)
                If dr Is Nothing Then
                    dr = dt.NewRow()
                    dr.Item($"Season ({team})") = statsDic.Item(key)
                    dt.Rows.Add(dr)
                Else
                    dr.Item($"Season ({team})") = statsDic.Item(key)
                End If

            Next

        Next


            dgvBattingStats.DataSource = dt

        'dt = InitDataGrids()
        'For Each key As String In mBattingSeasonStats.Keys
        '    Dim dr As DataRow = dt.NewRow()
        '    dr.Item("Stat") = key
        '    If mBattingGameStats.ContainsKey(key) Then
        '        dr.Item("Game") = mBattingGameStats(key)
        '    End If
        '    If mBattingSeasonStats.ContainsKey(key) Then
        '        dr.Item("Season") = mBattingSeasonStats(key)
        '    End If
        '    If mBattingCareerStats.ContainsKey(key) Then
        '        dr.Item("Career") = mBattingCareerStats(key)
        '    End If
        '    dt.Rows.Add(dr)
        'Next
        'dgvBattingStats.DataSource = dt

        ' load pitching stats

        'If PitchingGameStatsData IsNot Nothing Then
        '        For Each key As String In PitchingStatsKeys
        '            Dim value = ""
        '            If PitchingGameStatsData.ContainsKey(key) Then
        '                value = PitchingGameStatsData(key)
        '            End If
        '            Me.mPitchingGameStats.Add(key, value)
        '        Next
        '    End If

        'If PitchingSeasonStatsData IsNot Nothing Then
        '    Dim stats As JArray = PitchingSeasonStatsData.SelectToken("people[0].stats[0].splits")
        '    If stats IsNot Nothing Then
        '        For i = 0 To stats.Count - 1
        '            Dim statsdata As JObject = stats.Item(i)
        '            'Dim position As String = statsdata.SelectToken("position.abbreviation")
        '            'If position.ToUpper() = Me.mThisPlayer.ShortPosition Then
        '            For Each key As String In PitchingStatsKeys2.Keys
        '                Dim value As String = statsdata.SelectToken($"stat.{key}")
        '                Me.mPitchingSeasonStats.Add(PitchingStatsKeys2(key), value)
        '            Next
        '            'End If
        '        Next
        '    End If
        'End If


        'If PitchingCareerStatsData IsNot Nothing Then
        '    Dim stats As JArray = PitchingCareerStatsData.SelectToken("people[0].stats[0].splits")
        '    If stats IsNot Nothing Then
        '        For i = 0 To stats.Count - 1
        '            Dim statsdata As JObject = stats.Item(i)
        '            'Dim position As String = statsdata.SelectToken("position.abbreviation")
        '            'If position.ToUpper() = Me.mThisPlayer.ShortPosition Then
        '            For Each key As String In PitchingStatsKeys2.Keys
        '                Dim value As String = statsdata.SelectToken($"stat.{key}")
        '                Me.mPitchingCareerStats.Add(PitchingStatsKeys2(key), value)
        '            Next
        '            'End If
        '        Next
        '    End If
        'End If

        'dt = InitDataGrids()
        '    For Each key As String In mPitchingSeasonStats.Keys
        '        Dim dr As DataRow = dt.NewRow()
        '        dr.Item("Stat") = key
        '        If mPitchingGameStats.ContainsKey(key) Then
        '            dr.Item("Game") = mPitchingGameStats(key)
        '        End If
        '        If mPitchingSeasonStats.ContainsKey(key) Then
        '            dr.Item("Season") = mPitchingSeasonStats(key)
        '        End If
        '        If mPitchingCareerStats.ContainsKey(key) Then
        '            dr.Item("Career") = mPitchingCareerStats(key)
        '        End If
        '        dt.Rows.Add(dr)
        '    Next
        '    dgvPitchingStats.DataSource = dt


        dgvBattingStats.ColumnHeadersDefaultCellStyle.Font = New Font(dgvBattingStats.DefaultFont, FontStyle.Bold)
        dgvFieldingStats.ColumnHeadersDefaultCellStyle.Font = New Font(dgvFieldingStats.DefaultFont, FontStyle.Bold)
        dgvPitchingStats.ColumnHeadersDefaultCellStyle.Font = New Font(dgvPitchingStats.DefaultFont, FontStyle.Bold)

    End Sub


End Class

'<SDG><

