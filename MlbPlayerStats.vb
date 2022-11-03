' =========================================================================================================
' (C) 2022 MSRoth
'
' Released under XXX license.
' =========================================================================================================

Imports System.IO
Imports Newtonsoft.Json.Linq
Imports NLog

Public Class MlbPlayerStats

    Private mThisPlayer As MlbPlayer = Nothing
    Private mThisGame As MlbGame
    Private mAwayOrHome As String = ""

    Shared ReadOnly Logger As Logger = LogManager.GetCurrentClassLogger()

    ' == keys ==
    Private mFieldingStatsKeys As New SortedDictionary(Of String, String)
    Private mBattingStatsKeys As New SortedDictionary(Of String, String)
    Private mPitchingStatsKeys As New SortedDictionary(Of String, String)

    ' == dictionaries ==
    Private mBattingGameStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mBattingSeasonStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mBattingCareerStats As Dictionary(Of String, String) = New Dictionary(Of String, String)

    Private mPitchingGameStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mPitchingSeasonStats As Dictionary(Of String, String) = New Dictionary(Of String, String)
    Private mPitchingCareerStats As Dictionary(Of String, String) = New Dictionary(Of String, String)

    Private mFieldingGameStats As Dictionary(Of String, Dictionary(Of String, String)) = New Dictionary(Of String, Dictionary(Of String, String))
    Private mFieldingSeasonStats As Dictionary(Of String, Dictionary(Of String, String)) = New Dictionary(Of String, Dictionary(Of String, String))
    Private mFieldingCareerStats As Dictionary(Of String, Dictionary(Of String, String)) = New Dictionary(Of String, Dictionary(Of String, String))

    Property Player As MlbPlayer
        Set(ThisPlayer As MlbPlayer)
            Me.mThisPlayer = ThisPlayer
        End Set
        Get
            Return Me.mThisPlayer
        End Get
    End Property

    Property Game As MlbGame
        Set(Game As MlbGame)
            Me.mThisGame = Game
        End Set
        Get
            Return Me.mThisGame
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

        Logger.Debug($"Loading {Me.Name}")

        If Player Is Nothing Or Game Is Nothing Or AwayOrHome = "" Then
            Logger.Debug($"ERROR - called PlayerStats form with no args")
            Close()
        End If

        Me.Cursor = Cursors.WaitCursor

        ' set labels
        Me.lblPlayer.Text = $"{Me.mThisPlayer.FullName} (#{Me.mThisPlayer.Number})"
        Me.lblPosition.Text = Me.mThisPlayer.FullPosition
        Me.lblPlayerID.Text = $"ID: {Me.mThisPlayer.Id}"

        LoadStatsDataKeys()

        ' load pitching data
        Dim PitchingCareerStats As Dictionary(Of String, String) = Me.mThisPlayer.GetCareerPitchingStats()
        Dim PitchingSeasonStats As Dictionary(Of String, String) = Me.mThisPlayer.GetSeasonPitchingStats()
        Dim PitchingGameStats As Dictionary(Of String, String) = Me.mThisGame.GetPlayerGamePitchingStats(mThisPlayer.Id, mAwayOrHome.ToLower())
        dgvPitchingStats.DataSource = InitPitchingGrid()
        ProcessPitchingData(PitchingGameStats, PitchingSeasonStats, PitchingCareerStats)

        ' load fielding data
        Dim FieldingCareerStats As Dictionary(Of String, Dictionary(Of String, String)) = Me.mThisPlayer.GetCareerFieldingStats()
        Dim FieldingSeasonStats As Dictionary(Of String, Dictionary(Of String, String)) = Me.mThisPlayer.GetSeasonFieldingStats()
        Dim FieldingGameStats As Dictionary(Of String, Dictionary(Of String, String)) = Me.mThisGame.GetPlayerGameFieldingStats(mThisPlayer.Id, mAwayOrHome.ToLower())
        dgvFieldingStats.DataSource = InitFieldingGrid(FieldingGameStats.Keys().ToArray(), FieldingSeasonStats.Keys().ToArray(), FieldingCareerStats.Keys().ToArray())
        ProcessFieldingData(FieldingGameStats, FieldingSeasonStats, FieldingCareerStats)

        ' load batting data
        Dim BattingCareerStats As Dictionary(Of String, String) = Me.mThisPlayer.GetCareerBattingStats()
        Dim BattingSeasonStats As Dictionary(Of String, String) = Me.mThisPlayer.GetSeasonBattingStats()
        Dim BattingGameStats As Dictionary(Of String, String) = Me.mThisGame.GetPlayerGameBattingStats(mThisPlayer.Id, mAwayOrHome.ToLower())
        dgvBattingStats.DataSource = InitBattingGrid()
        ProcessBattingData(BattingGameStats, BattingSeasonStats, BattingCareerStats)

        ' tweak tables
        dgvBattingStats.Columns("key").Visible = False
        dgvFieldingStats.Columns("key").Visible = False
        dgvPitchingStats.Columns("key").Visible = False
        dgvBattingStats.ColumnHeadersDefaultCellStyle.Font = New Font(dgvBattingStats.DefaultFont, FontStyle.Bold)
        dgvFieldingStats.ColumnHeadersDefaultCellStyle.Font = New Font(dgvFieldingStats.DefaultFont, FontStyle.Bold)
        dgvPitchingStats.ColumnHeadersDefaultCellStyle.Font = New Font(dgvPitchingStats.DefaultFont, FontStyle.Bold)

        Me.Cursor = Cursors.Default
    End Sub

    Private Function InitBattingGrid() As DataTable
        Dim dt As DataTable = New DataTable()
        Dim ColNames As String() = {"key", "Stat", "Game", "Season", "Career"}
        For Each name As String In ColNames
            Dim col As DataColumn = New DataColumn()
            col.ColumnName = name
            dt.Columns.Add(col)
        Next

        For Each key As String In mBattingStatsKeys.Keys()
            Dim row As DataRow = dt.NewRow()
            row("key") = key
            row("Stat") = mBattingStatsKeys(key)
            dt.Rows.Add(row)
        Next

        Return dt
    End Function

    Private Function InitPitchingGrid() As DataTable
        Dim dt As DataTable = New DataTable()
        Dim ColNames As String() = {"key", "Stat", "Game", "Season", "Career"}
        For Each name As String In ColNames
            Dim col As DataColumn = New DataColumn()
            col.ColumnName = name
            dt.Columns.Add(col)
        Next

        For Each key As String In mPitchingStatsKeys.Keys()
            Dim row As DataRow = dt.NewRow()
            row("key") = key
            row("Stat") = mPitchingStatsKeys(key)
            dt.Rows.Add(row)
        Next
        Return dt
    End Function

    Private Function InitFieldingGrid(gamePositions As String(), seasonPositions As String(), careerPositions As String()) As DataTable
        Dim dt As DataTable = New DataTable()

        Dim col As New DataColumn
        col.ColumnName = "key"
        dt.Columns.Add(col)

        col = New DataColumn
        col.ColumnName = "Stat"
        dt.Columns.Add(col)

        ' game may not have started yet, thus no positions listed yet
        If gamePositions.Length > 0 Then
            For Each position As String In gamePositions
                col = New DataColumn
                col.ColumnName = $"Game: {position}"
                dt.Columns.Add(col)
            Next
        Else
            col = New DataColumn
            col.ColumnName = "Game"
            dt.Columns.Add(col)
        End If

        For Each position As String In seasonPositions
            col = New DataColumn
            col.ColumnName = $"Season: {position}"
            dt.Columns.Add(col)
        Next

        For Each position As String In careerPositions
            col = New DataColumn
            col.ColumnName = $"Career: {position}"
            dt.Columns.Add(col)
        Next

        For Each key As String In mFieldingStatsKeys.Keys()
            Dim row As DataRow = dt.NewRow()
            row("key") = key
            row("Stat") = mFieldingStatsKeys(key)
            dt.Rows.Add(row)
        Next

        Return dt
    End Function

    Sub LoadStatsDataKeys()

        ' fielding stats
        mFieldingStatsKeys.Add("gamesPlayed", "Games Played")
        mFieldingStatsKeys.Add("gamesStarted", "Games Started")
        mFieldingStatsKeys.Add("assists", "Assists")
        mFieldingStatsKeys.Add("putOuts", "Put Outs")
        mFieldingStatsKeys.Add("errors", "Errors")
        mFieldingStatsKeys.Add("chances", "Chances")
        mFieldingStatsKeys.Add("fielding", "Fielding")
        mFieldingStatsKeys.Add("innings", "Innings Played")
        mFieldingStatsKeys.Add("doublePlays", "Double Plays")
        mFieldingStatsKeys.Add("triplePlays", "Triple Plays")
        mFieldingStatsKeys.Add("throwingErrors", "Throwing Errors")

        ' batting stats
        mBattingStatsKeys.Add("runs", "Runs")
        mBattingStatsKeys.Add("doubles", "Doubles")
        mBattingStatsKeys.Add("triples", "Triples")
        mBattingStatsKeys.Add("homeRuns", "Home Runs")
        mBattingStatsKeys.Add("strikeOuts", "Strike Outs")
        mBattingStatsKeys.Add("baseOnBalls", "Walks")
        mBattingStatsKeys.Add("hits", "Hits")
        mBattingStatsKeys.Add("hitByPitch", "Hit By Pitch")
        mBattingStatsKeys.Add("avg", "Batting Average")
        mBattingStatsKeys.Add("atBats", "At Bats")
        mBattingStatsKeys.Add("obp", "On Base Percentage (OBP)")
        mBattingStatsKeys.Add("slg", "Slugging")
        mBattingStatsKeys.Add("ops", "OBP + Slugging (OPS)")
        mBattingStatsKeys.Add("caughtStealing", "Caught Stealing")
        mBattingStatsKeys.Add("stolenBases", "Stolen Bases")
        mBattingStatsKeys.Add("stolenBasePercentage", "Stolen Base Percentage")
        mBattingStatsKeys.Add("plateAppearances", "Plate Appearances")
        mBattingStatsKeys.Add("totalBases", "Total Bases")
        mBattingStatsKeys.Add("rbi", "RBIs")
        mBattingStatsKeys.Add("sacBunts", "Sacrifice Bunts")
        mBattingStatsKeys.Add("atBatsPerHomeRun", "ABs / Home Runs")

        ' pitching stats
        mPitchingStatsKeys.Add("gamesStarted", "Games Started")
        mPitchingStatsKeys.Add("groundOuts", "Ground Outs")
        mPitchingStatsKeys.Add("airOuts", "Air Outs")
        mPitchingStatsKeys.Add("intentionalWalks", "Intentional Walks")
        mPitchingStatsKeys.Add("numberOfPitches", "Number of Pitches")
        mPitchingStatsKeys.Add("era", "ERA")
        mPitchingStatsKeys.Add("inningsPitched", "Innings Pitched")
        mPitchingStatsKeys.Add("wins", "Wins")
        mPitchingStatsKeys.Add("losses", "Losses")
        mPitchingStatsKeys.Add("saves", "Saves")
        mPitchingStatsKeys.Add("holds", "Holds")
        mPitchingStatsKeys.Add("blownSaves", "Blown Saves")
        mPitchingStatsKeys.Add("earnedRuns", "Earned Runs")
        mPitchingStatsKeys.Add("whip", "WHIP")
        mPitchingStatsKeys.Add("battersFaced", "Batters Faced")
        mPitchingStatsKeys.Add("outs", "Outs")
        mPitchingStatsKeys.Add("gamesPitched", "Games Pitched")
        mPitchingStatsKeys.Add("completeGames", "Complete Games")
        mPitchingStatsKeys.Add("shutouts", "Shutouts")
        mPitchingStatsKeys.Add("pitchesThrown", "Pitches Thrown")
        mPitchingStatsKeys.Add("balls", "Balls")
        mPitchingStatsKeys.Add("strikes", "Strikes")
        mPitchingStatsKeys.Add("strikePercentage", "Percent Strikes")
        mPitchingStatsKeys.Add("hitBatsmen", "Hit Batters")
        mPitchingStatsKeys.Add("balks", "Balks")
        mPitchingStatsKeys.Add("wildPitches", "Wild Pitches")
        mPitchingStatsKeys.Add("pickoffs", "Pickoffs")
        mPitchingStatsKeys.Add("groundOutsToAirouts", "GO to AO Ration")
        mPitchingStatsKeys.Add("rbi", "RBIs")
        mPitchingStatsKeys.Add("winPercentage", "Win Percentage")
        mPitchingStatsKeys.Add("pitchesPerInning", "Pitches per Inning")
        mPitchingStatsKeys.Add("gamesFinished", "Games Finished")
        mPitchingStatsKeys.Add("strikeoutWalkRatio", "SO to Walk Ratio")
        mPitchingStatsKeys.Add("strikeoutsPer9Inn", "SO per 9 Innings")
        mPitchingStatsKeys.Add("walksPer9Inn", "Walks per 9 Innings")
        mPitchingStatsKeys.Add("hitsPer9Inn", "Hits per 9 Innings")
        mPitchingStatsKeys.Add("runsScoredPer9", "Runs per 9 Iniings")
        mPitchingStatsKeys.Add("homeRunsPer9", "HRs per 9 Innings")
        mPitchingStatsKeys.Add("inheritedRunners", "Inherited Runners")
        mPitchingStatsKeys.Add("inheritedRunnersScored", "Inherited Runners Scored")
        mPitchingStatsKeys.Add("sacBunts", "Sacrifice Bunts")
        mPitchingStatsKeys.Add("sacFlies", "Sacrifice Flies")
    End Sub

    Sub ProcessFieldingData(GameData As Dictionary(Of String, Dictionary(Of String, String)),
                            SeasonData As Dictionary(Of String, Dictionary(Of String, String)),
                            CareerData As Dictionary(Of String, Dictionary(Of String, String)))

        Dim dt As DataTable = Me.dgvFieldingStats.DataSource()
        Try
            ' load game stats
            If GameData IsNot Nothing Then
                For Each position As String In GameData.Keys()
                    Dim statsDic = GameData(position)
                    For Each row As DataRow In dt.Rows()
                        Dim key As String = row("key")
                        If statsDic.Keys.Contains(key) Then
                            row($"Game: {position}") = statsDic(key)
                        End If
                    Next
                Next
            End If

            ' load season stats
            If SeasonData IsNot Nothing Then
                For Each position As String In SeasonData.Keys()
                    Dim statsDic = SeasonData(position)
                    For Each row As DataRow In dt.Rows()
                        Dim key As String = row("key")
                        If statsDic.Keys.Contains(key) Then
                            row($"Season: {position}") = statsDic(key)
                        End If
                    Next
                Next
            End If

            ' load career stats
            If CareerData IsNot Nothing Then
                For Each position As String In CareerData.Keys()
                    Dim statsDic = CareerData(position)
                    For Each row As DataRow In dt.Rows()
                        Dim key As String = row("key")
                        If statsDic.Keys.Contains(key) Then
                            row($"Career: {position}") = statsDic(key)
                        End If
                    Next
                Next
            End If

        Catch ex As Exception
            Logger.Error(ex)
        End Try

        ' sort by stat
        dt = dt.Select("", "Stat").CopyToDataTable()

        ' set datagrid source
        dgvFieldingStats.DataSource = dt

    End Sub

    Private Sub ProcessBattingData(GameData As Dictionary(Of String, String),
                                   SeasonData As Dictionary(Of String, String),
                                   CareerData As Dictionary(Of String, String))

        Dim dt As DataTable = Me.dgvBattingStats.DataSource()

        Try
            ' load game stats
            If GameData IsNot Nothing Then
                For Each row As DataRow In dt.Rows()
                    Dim key As String = row("key")
                    If GameData.Keys.Contains(key) Then
                        row("Game") = GameData(key)
                    End If
                Next
            End If

            ' load season stats
            If SeasonData IsNot Nothing Then
                For Each row As DataRow In dt.Rows()
                    Dim key As String = row("key")
                    If SeasonData.Keys.Contains(key) Then
                        row("Season") = SeasonData(key)
                    End If
                Next
            End If

            ' load career stats
            If CareerData IsNot Nothing Then
                For Each row As DataRow In dt.Rows()
                    Dim key As String = row("key")
                    If CareerData.Keys.Contains(key) Then
                        row("Career") = CareerData(key)
                    End If
                Next
            End If

        Catch ex As Exception
            Logger.Error(ex)
        End Try

        ' sort by stat
        dt = dt.Select("", "Stat").CopyToDataTable()

        ' set datagrid source
        dgvBattingStats.DataSource = dt

    End Sub

    Private Sub ProcessPitchingData(GameData As Dictionary(Of String, String),
                                    SeasonData As Dictionary(Of String, String),
                                    CareerData As Dictionary(Of String, String))

        Dim dt As DataTable = Me.dgvPitchingStats.DataSource()

        Try
            ' load game stats
            If GameData IsNot Nothing Then
                For Each row As DataRow In dt.Rows()
                    Dim key As String = row("key")
                    If GameData.Keys.Contains(key) Then
                        row("Game") = GameData(key)
                    End If
                Next
            End If

            ' load season stats
            If SeasonData IsNot Nothing Then
                For Each row As DataRow In dt.Rows()
                    Dim key As String = row("key")
                    If SeasonData.Keys.Contains(key) Then
                        row("Season") = SeasonData(key)
                    End If
                Next
            End If

            ' load career stats
            If CareerData IsNot Nothing Then
                For Each row As DataRow In dt.Rows()
                    Dim key As String = row("key")
                    If CareerData.Keys.Contains(key) Then
                        row("Career") = CareerData(key)
                    End If
                Next
            End If

        Catch ex As Exception
            Logger.Error(ex)
        End Try

        ' sort by stat
        dt = dt.Select("", "Stat").CopyToDataTable()

        ' set datagrid source
        dgvPitchingStats.DataSource = dt

    End Sub

End Class

'<SDG><

