Imports System.IO
Imports Newtonsoft.Json.Linq

Public Class MlbPlayerStats

    Private mAPI As MlbApi = New MlbApi()
    Private mThisPlayer As MlbPlayer = Nothing
    Private mLiveData As JObject
    Private mBoxscoreData As JObject
    Private mGamePk As Integer = 0
    Private mAwayOrHome As String = ""

    ' == Game Stats ==
    Private BattingGameStatsData As JObject
    Private PitchingGameStatsData As JObject
    Private FieldingGameStatsData As JObject

    ' == Season Stats ==
    Private BattingSeasonStatsData As JObject
    Private PitchingSeasonStatsData As JObject
    Private FieldingSeasonStatsData As JObject

    ' == Career Stats ==
    Private BattingCareerStatsData As JObject
    Private PitchingCareerStatsData As JObject
    Private FieldingCareerStatsData As JObject

    Private FieldingStatsKeys As New SortedDictionary(Of String, String)
    Private BattingStatsKeys As New SortedDictionary(Of String, String)
    Private PitchingStatsKeys As New SortedDictionary(Of String, String)

    Private mBattingGameStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mBattingSeasonStats As Dictionary(Of String, Dictionary(Of String, String)) = New Dictionary(Of String, Dictionary(Of String, String))
    Private mBattingCareerStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mPitchingGameStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mPitchingSeasonStats As Dictionary(Of String, Dictionary(Of String, String)) = New Dictionary(Of String, Dictionary(Of String, String))
    Private mPitchingCareerStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mFieldingGameStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mFieldingSeasonStats As Dictionary(Of String, Dictionary(Of String, String)) = New Dictionary(Of String, Dictionary(Of String, String))
    Private mFieldingCareerStats As Dictionary(Of String, Dictionary(Of String, String)) = New Dictionary(Of String, Dictionary(Of String, String))
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

        ' set labels
        Me.lblPlayer.Text = $"{Me.mThisPlayer.FullName} (#{Me.mThisPlayer.Number})"
        Me.lblPosition.Text = Me.mThisPlayer.FullPosition
        Me.lblPlayerID.Text = $"ID: {Me.mThisPlayer.Id}"

        ' make API calls to get data
        Me.Cursor = Cursors.WaitCursor
        Me.mLiveData = mAPI.ReturnLiveFeedData(Me.mGamePk).SelectToken("liveData")
        Me.mBoxscoreData = mLiveData.SelectToken("boxscore")
        Me.Cursor = Cursors.Default

        ' load data from API into data structures
        LoadStatsData()

        ' extract and format data into datatables
        ProcessPitchingData()
        ProcessFieldingData()
        ProcessBattingData()

        ' tweak tables
        dgvBattingStats.ColumnHeadersDefaultCellStyle.Font = New Font(dgvBattingStats.DefaultFont, FontStyle.Bold)
        dgvFieldingStats.ColumnHeadersDefaultCellStyle.Font = New Font(dgvFieldingStats.DefaultFont, FontStyle.Bold)
        dgvPitchingStats.ColumnHeadersDefaultCellStyle.Font = New Font(dgvPitchingStats.DefaultFont, FontStyle.Bold)


    End Sub


    Private Function InitBattingAndPitchingGridColumns(seasonTeamNames As List(Of String)) As DataTable
        Dim dt As DataTable = New DataTable()
        Dim col As New DataColumn
        col.ColumnName = "Stat"
        dt.Columns.Add(col)
        col = New DataColumn
        col.ColumnName = "Game"
        dt.Columns.Add(col)

        For Each team As String In seasonTeamNames
            col = New DataColumn
            col.ColumnName = $"Season ({team})"
            dt.Columns.Add(col)
        Next

        col = New DataColumn
        col.ColumnName = "Career"
        dt.Columns.Add(col)
        Return dt
    End Function

    Private Function InitFieldingGridColumns(gamePositions As List(Of String), seasonPositions As List(Of String), careerPositions As List(Of String)) As DataTable
        Dim dt As DataTable = New DataTable()
        Dim col As New DataColumn
        col.ColumnName = "Stat"
        dt.Columns.Add(col)

        For Each position As String In gamePositions
            col = New DataColumn
            col.ColumnName = $"{position}"
            dt.Columns.Add(col)
        Next

        For Each position As String In seasonPositions
            col = New DataColumn
            col.ColumnName = $"{position}"
            dt.Columns.Add(col)
        Next

        For Each position As String In careerPositions
            col = New DataColumn
            col.ColumnName = $"{position}"
            dt.Columns.Add(col)
        Next

        Return dt
    End Function


    Sub LoadStatsData()

        ' fielding stats
        FieldingStatsKeys.Add("gamesPlayed", "Games Played")
        FieldingStatsKeys.Add("gamesStarted", "Games Started")
        FieldingStatsKeys.Add("assists", "Assists")
        FieldingStatsKeys.Add("putOuts", "Put Outs")
        FieldingStatsKeys.Add("errors", "Errors")
        FieldingStatsKeys.Add("chances", "Chances")
        FieldingStatsKeys.Add("fielding", "Fielding")
        FieldingStatsKeys.Add("innings", "Innings Played")
        FieldingStatsKeys.Add("doublePlays", "Double Plays")
        FieldingStatsKeys.Add("triplePlays", "Triple Plays")
        FieldingStatsKeys.Add("throwingErrors", "Throwing Errors")
        FieldingStatsKeys.Add("numTeams", "Number of Teams")

        ' batting stats
        BattingStatsKeys.Add("runs", "Runs")
        BattingStatsKeys.Add("doubles", "Doubles")
        BattingStatsKeys.Add("triples", "Triples")
        BattingStatsKeys.Add("homeRuns", "Home Runs")
        BattingStatsKeys.Add("strikeOuts", "Strike Outs")
        BattingStatsKeys.Add("baseOnBalls", "Walks")
        BattingStatsKeys.Add("hits", "Hits")
        BattingStatsKeys.Add("hitByPitch", "Hit By Pitch")
        BattingStatsKeys.Add("avg", "Batting Average")
        BattingStatsKeys.Add("atBats", "At Bats")
        BattingStatsKeys.Add("obp", "On Base Percentage (OBP)")
        BattingStatsKeys.Add("slg", "Slugging")
        BattingStatsKeys.Add("ops", "OBP + Slugging (OPS)")
        BattingStatsKeys.Add("caughtStealing", "Caught Stealing")
        BattingStatsKeys.Add("stolenBases", "Stolen Bases")
        BattingStatsKeys.Add("stolenBasePercentage", "Stolen Base Percentage")
        BattingStatsKeys.Add("plateAppearances", "Plate Appearances")
        BattingStatsKeys.Add("totalBases", "Total Bases")
        BattingStatsKeys.Add("rbi", "RBIs")
        BattingStatsKeys.Add("sacBunts", "Sacrifice Bunts")
        BattingStatsKeys.Add("atBatsPerHomeRun", "ABs / Home Runs")

        ' pitching stats
        PitchingStatsKeys.Add("gamesStarted", "Games Started")
        PitchingStatsKeys.Add("groundOuts", "Ground Outs")
        PitchingStatsKeys.Add("airOuts", "Air Outs")
        PitchingStatsKeys.Add("intentionalWalks", "Intentional Walks")
        PitchingStatsKeys.Add("numberOfPitches", "Number of Pitches")
        PitchingStatsKeys.Add("era", "ERA")
        PitchingStatsKeys.Add("inningsPitched", "Innings Pitched")
        PitchingStatsKeys.Add("wins", "Wins")
        PitchingStatsKeys.Add("losses", "Losses")
        PitchingStatsKeys.Add("saves", "Saves")
        PitchingStatsKeys.Add("holds", "Holds")
        PitchingStatsKeys.Add("blownSaves", "Blown Saves")
        PitchingStatsKeys.Add("earnedRuns", "Earned Runs")
        PitchingStatsKeys.Add("whip", "WHIP")
        PitchingStatsKeys.Add("battersFaced", "Batters Faced")
        PitchingStatsKeys.Add("outs", "Outs")
        PitchingStatsKeys.Add("gamesPitched", "Games Pitched")
        PitchingStatsKeys.Add("completeGames", "Complete Games")
        PitchingStatsKeys.Add("shutouts", "Shutouts")
        PitchingStatsKeys.Add("pitchesThrown", "Pitches Thrown")
        PitchingStatsKeys.Add("balls", "Balls")
        PitchingStatsKeys.Add("strikes", "Strikes")
        PitchingStatsKeys.Add("strikePercentage", "Percent Strikes")
        PitchingStatsKeys.Add("hitBatsmen", "Hit Batters")
        PitchingStatsKeys.Add("balks", "Balks")
        PitchingStatsKeys.Add("wildPitches", "Wild Pitches")
        PitchingStatsKeys.Add("pickoffs", "Pickoffs")
        PitchingStatsKeys.Add("groundOutsToAirouts", "GO to AO Ration")
        PitchingStatsKeys.Add("rbi", "RBIs")
        PitchingStatsKeys.Add("winPercentage", "Win Percentage")
        PitchingStatsKeys.Add("pitchesPerInning", "Pitches per Inning")
        PitchingStatsKeys.Add("gamesFinished", "Games Finished")
        PitchingStatsKeys.Add("strikeoutWalkRatio", "SO to Walk Ratio")
        PitchingStatsKeys.Add("strikeoutsPer9Inn", "SO per 9 Innings")
        PitchingStatsKeys.Add("walksPer9Inn", "Walks per 9 Innings")
        PitchingStatsKeys.Add("hitsPer9Inn", "Hits per 9 Innings")
        PitchingStatsKeys.Add("runsScoredPer9", "Runs per 9 Iniings")
        PitchingStatsKeys.Add("homeRunsPer9", "HRs per 9 Innings")
        PitchingStatsKeys.Add("inheritedRunners", "Inherited Runners")
        PitchingStatsKeys.Add("inheritedRunnersScored", "Inherited Runners Scored")
        PitchingStatsKeys.Add("sacBunts", "Sacrifice Bunts")
        PitchingStatsKeys.Add("sacFlies", "Sacrifice Flies")

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

    End Sub

    Sub ProcessFieldingData()

        Dim gamePositions As New List(Of String)
        Dim seasonPositions As New List(Of String)
        Dim careerPositions As New List(Of String)

        ' load game fielding stats
        If FieldingGameStatsData IsNot Nothing Then

            For Each key As String In FieldingStatsKeys.Keys
                Dim value As String = FieldingGameStatsData.SelectToken(key)
                If mFieldingGameStats.ContainsKey(FieldingStatsKeys(key)) Then
                    mFieldingGameStats(FieldingStatsKeys(key)) = value
                Else
                    mFieldingGameStats.Add(FieldingStatsKeys(key), value)
                End If
            Next
            gamePositions.Add("Game")
        End If


        If FieldingSeasonStatsData IsNot Nothing Then
            Dim stats As JArray = FieldingSeasonStatsData.SelectToken("people[0].stats[0].splits")

            If stats IsNot Nothing Then
                For i = 0 To stats.Count - 1
                    Dim statsdata As JObject = stats.Item(i)

                    ' create dic for player stats for each team played on this season
                    Dim statsDic As Dictionary(Of String, String) = New Dictionary(Of String, String)

                    Dim position As String
                    Dim teamName As String
                    Dim numTeams As Integer = Convert.ToInt32(statsdata.SelectToken("numTeams"))

                    If i = 0 Then  '' 0th entry is summary of all season
                        If numTeams > 1 Then
                            position = ""
                            teamName = "Totals"
                        Else
                            position = FieldingSeasonStatsData.SelectToken("people[0].primaryPosition.abbreviation")
                            teamName = FieldingSeasonStatsData.SelectToken("people[0].currentTeam.name")
                        End If

                    Else
                        position = statsdata.SelectToken("position.abbreviation")
                        teamName = statsdata.SelectToken("team.name")
                    End If

                    For Each key As String In FieldingStatsKeys.Keys
                        Dim value As String = statsdata.SelectToken($"stat.{key}")
                        If statsDic.ContainsKey(FieldingStatsKeys(key)) Then
                            statsDic(FieldingStatsKeys(key)) = value
                        Else
                            statsDic.Add(FieldingStatsKeys(key), value)
                        End If

                    Next
                    position = $"Season: {position} ({teamName})"
                    Me.mFieldingSeasonStats.Add(position, statsDic)
                    seasonPositions.Add(position)
                Next
            End If
        End If


        ' FiX HERE and BELOW


        'If FieldingCareerStatsData IsNot Nothing Then
        '    Dim stats As JArray = FieldingCareerStatsData.SelectToken("people[0].stats[0].splits")

        '    If stats IsNot Nothing Then
        '        For i = 0 To stats.Count - 1
        '            Dim statsdata As JObject = stats.Item(i)

        '            ' create dic for player stats for each team played on this season
        '            Dim statsDic As Dictionary(Of String, String) = New Dictionary(Of String, String)

        '            Dim position As String
        '            If i = 0 Then
        '                position = FieldingCareerStatsData.SelectToken("people[0].primaryPosition.abbreviation")
        '            Else
        '                position = statsdata.SelectToken("position.abbreviation")
        '            End If


        '            For Each key As String In FieldingStatsKeys.Keys
        '                Dim value As String = statsdata.SelectToken($"stat.{key}")
        '                If statsDic.ContainsKey(FieldingStatsKeys(key)) Then
        '                    statsDic(FieldingStatsKeys(key)) = value
        '                Else
        '                    statsDic.Add(FieldingStatsKeys(key), value)
        '                End If
        '            Next
        '            position = $"Career: {position}"
        '            If Me.mFieldingCareerStats.ContainsKey(position) Then
        '                Trace.WriteLine($"{position} already in fielding career stats: {statsDic}")
        '            Else
        '                Me.mFieldingCareerStats.Add(position, statsDic)
        '            End If

        '            careerPositions.Add(position)
        '        Next
        '    End If
        'End If

        Dim dt As DataTable = InitFieldingGridColumns(gamePositions, seasonPositions, careerPositions)

        ' game data
        For Each position In gamePositions
            For Each key In mFieldingGameStats.Keys
                Dim dr As DataRow = dt.NewRow()
                dr.Item("Stat") = key
                dr.Item(position) = mFieldingGameStats.Item(key)
                dt.Rows.Add(dr)
            Next
        Next

        ' career data
        For Each position In careerPositions
            Dim statsDic = mFieldingCareerStats(position)
            For Each key As String In statsDic.Keys
                Dim dr As DataRow
                If dt.Rows.Count > 0 Then
                    dr = dt.Select($"Stat = '{key}'")(0)
                End If

                If dr Is Nothing Then
                    dr = dt.NewRow()
                    dr.Item(position) = statsDic.Item(key)
                    dt.Rows.Add(dr)
                Else
                    dr.Item(position) = statsDic.Item(key)
                End If
            Next
        Next


        ' season data
        For Each team As String In mFieldingSeasonStats.Keys
            Dim statsDic = mFieldingSeasonStats.Item(team)

            For Each key In statsDic.Keys
                Dim dr As DataRow = dt.Select($"Stat = '{key}'")(0)
                If dr Is Nothing Then
                    dr = dt.NewRow()
                    dr.Item(team) = statsDic.Item(key)
                    dt.Rows.Add(dr)
                Else
                    dr.Item(team) = statsDic.Item(key)
                End If

            Next
        Next

        ' sort by stat
        dt = dt.Select("", "Stat").CopyToDataTable()

        ' set datagrid source
        dgvFieldingStats.DataSource = dt

    End Sub

    Function ProcessSinglePositionGameData(data As JObject, statskeys As SortedDictionary(Of String, String)) As Dictionary(Of String, String)
        Dim dict As New Dictionary(Of String, String)
        If data IsNot Nothing Then
            For Each key As String In statskeys.Keys
                Dim value = ""
                If data.ContainsKey(key) Then
                    value = data(key)
                End If
                dict.Add(statskeys(key), value)
            Next
        End If
    End Function

    Sub ProcessBattingData()

        ' load game batting stats
        If BattingGameStatsData IsNot Nothing Then
            For Each key As String In BattingStatsKeys.Keys
                Dim value = ""
                If BattingGameStatsData.ContainsKey(key) Then
                    value = BattingGameStatsData(key)
                End If
                Me.mBattingGameStats.Add(BattingStatsKeys(key), value)
            Next
        End If

        ' load season batting stats
        If BattingSeasonStatsData IsNot Nothing Then
            Dim stats As JArray = BattingSeasonStatsData.SelectToken("people[0].stats[0].splits")

            If stats IsNot Nothing Then
                For i = 0 To stats.Count - 1
                    Dim statsdata As JObject = stats.Item(i)

                    ' create dic for player stats for each team played on this season
                    Dim statsDic As Dictionary(Of String, String) = New Dictionary(Of String, String)

                    Dim teamName As String
                    Dim numTeams As Integer = Convert.ToInt32(statsdata.SelectToken("numTeams"))
                    If i = 0 Then
                        If numTeams > 1 Then
                            teamName = "Totals"
                        Else
                            teamName = BattingSeasonStatsData.SelectToken("people[0].currentTeam.name")
                        End If
                    Else
                        teamName = statsdata.SelectToken("team.name")
                    End If

                    For Each key As String In BattingStatsKeys.Keys
                        Dim value As String = statsdata.SelectToken($"stat.{key}")
                        If statsDic.ContainsKey(BattingStatsKeys(key)) Then
                            statsDic(BattingStatsKeys(key)) = value
                        Else
                            statsDic.Add(BattingStatsKeys(key), value)
                        End If

                    Next

                    ' if player played for same team twice during season, rename one team
                    Dim dupIndex As Integer = 1
                    While mBattingSeasonStats.Keys.Contains(teamName)
                        teamName = $"{teamName}-{dupIndex}"
                        dupIndex += 1
                    End While
                    Me.mBattingSeasonStats.Add(teamName, statsDic)
                Next
            End If
        End If

        ' load career batting stats
        If BattingCareerStatsData IsNot Nothing Then
            Dim stats As JArray = BattingCareerStatsData.SelectToken("people[0].stats[0].splits")
            If stats IsNot Nothing Then
                For i = 0 To stats.Count - 1
                    Dim statsdata As JObject = stats.Item(i)
                    For Each key As String In BattingStatsKeys.Keys
                        Dim value As String = statsdata.SelectToken($"stat.{key}")
                        ' If mBattingCareerStats.ContainsKey(key) Then
                        'Me.mBattingCareerStats(key) = value
                        'Else
                        Me.mBattingCareerStats.Add(BattingStatsKeys(key), value)
                        'End If
                    Next
                Next
            End If
        End If

        ' load data into datatable
        Dim dt As DataTable = InitBattingAndPitchingGridColumns(mBattingSeasonStats.Keys.ToList())

        ' game data
        For Each key In mBattingGameStats.Keys
            Dim dr As DataRow = dt.NewRow()
            dr.Item("Stat") = key
            dr.Item("Game") = mBattingGameStats.Item(key)
            dt.Rows.Add(dr)
        Next

        ' career data
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

        ' season data
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

        ' sort by stat
        dt = dt.Select("", "Stat").CopyToDataTable()

        ' set datagrid source
        dgvBattingStats.DataSource = dt

    End Sub

    Sub ProcessPitchingData()

        ' load pitching stats data
        If PitchingGameStatsData IsNot Nothing Then
            For Each key As String In PitchingStatsKeys.Keys
                Dim value = ""
                If PitchingGameStatsData.ContainsKey(key) Then
                    value = PitchingGameStatsData(key)
                End If
                Me.mPitchingGameStats.Add(PitchingStatsKeys(key), value)
            Next
        End If

        If PitchingSeasonStatsData IsNot Nothing Then
            Dim stats As JArray = PitchingSeasonStatsData.SelectToken("people[0].stats[0].splits")

            If stats IsNot Nothing Then
                For i = 0 To stats.Count - 1
                    Dim statsdata As JObject = stats.Item(i)

                    ' create dic for player stats for each team played on this season
                    Dim statsDic As Dictionary(Of String, String) = New Dictionary(Of String, String)

                    Dim teamName As String
                    If i = 0 Then
                        teamName = PitchingSeasonStatsData.SelectToken("people[0].currentTeam.name")
                    Else
                        teamName = statsdata.SelectToken("team.name")
                    End If

                    For Each key As String In PitchingStatsKeys.Keys
                        Dim value As String = statsdata.SelectToken($"stat.{key}")
                        If statsDic.ContainsKey(PitchingStatsKeys(key)) Then
                            statsDic(PitchingStatsKeys(key)) = value
                        Else
                            statsDic.Add(PitchingStatsKeys(key), value)
                        End If

                    Next
                    Dim dupIndex As Integer = 1
                    While mPitchingSeasonStats.Keys.Contains(teamName)
                        teamName = $"{teamName}-{dupIndex}"
                        dupIndex += 1
                    End While
                    Me.mPitchingSeasonStats.Add(teamName, statsDic)
                Next
            End If
        End If


        If PitchingCareerStatsData IsNot Nothing Then
            Dim stats As JArray = PitchingCareerStatsData.SelectToken("people[0].stats[0].splits")
            If stats IsNot Nothing Then
                For i = 0 To stats.Count - 1
                    Dim statsdata As JObject = stats.Item(i)
                    For Each key As String In PitchingStatsKeys.Keys
                        Dim value As String = statsdata.SelectToken($"stat.{key}")
                        'If mPitchingCareerStats.ContainsKey(key) Then
                        'Me.mPitchingCareerStats(key) = value
                        'Else
                        Me.mPitchingCareerStats.Add(PitchingStatsKeys(key), value)
                        'End If
                    Next
                Next
            End If
        End If

        ' load data into datatable
        Dim dt As DataTable = InitBattingAndPitchingGridColumns(mPitchingSeasonStats.Keys.ToList)

        ' game data
        For Each key In mPitchingGameStats.Keys
            Dim dr As DataRow = dt.NewRow()
            dr.Item("Stat") = key
            dr.Item("Game") = mPitchingGameStats.Item(key)
            dt.Rows.Add(dr)
        Next

        ' career data
        For Each key In mPitchingCareerStats.Keys
            Dim dr As DataRow = dt.Select($"Stat = '{key}'")(0)
            If dr Is Nothing Then
                dr = dt.NewRow()
                dr.Item("Career") = mPitchingCareerStats.Item(key)
                dt.Rows.Add(dr)
            Else
                dr.Item("Career") = mPitchingCareerStats.Item(key)
            End If
        Next

        ' season data
        For Each team As String In mPitchingSeasonStats.Keys
            Dim statsDic = mPitchingSeasonStats.Item(team)

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

        ' sort by stat
        dt = dt.Select("", "Stat").CopyToDataTable()

        ' set datagrid source
        dgvPitchingStats.DataSource = dt

    End Sub



End Class

'<SDG><

