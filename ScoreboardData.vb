

Imports Newtonsoft.Json.Linq


Public Class ScoreboardData


    Dim API As MLB_API = New MLB_API()
    Dim DB As Database = New Database()
    Dim liveData As JObject
    Dim gamePk As Integer = 0


    Public Sub New()

    End Sub

    Public Function getAPI() As MLB_API
        Return API
    End Function

    Public Function getDB() As Database
        Return DB
    End Function

    Public Sub loadCurrentGameDataIntoDB(liveData, boxData, gameDate, gameTime, gamePk)
        Me.DB.loadCurrentGameDataIntoDB(liveData, boxData, gameDate, gameTime, gamePk)

    End Sub

    Public Function LoadTeamsData() As List(Of Team)
        Dim Teams As List(Of Team) = New List(Of Team)
        Dim Data As JObject = API.ReturnAllTeamsData()
        Dim TeamsData As JArray = Data.SelectToken("teams")

        For Each Team As JObject In TeamsData
            Dim teamId As String = Team.SelectToken("id").ToString()
            Dim oTeam As Team = New Team(Convert.ToInt32(teamId))
            Teams.Add(oTeam)
            'Trace.WriteLine(oTeam.ToString())
        Next
        Return Teams
    End Function


    Public Sub loadTeamsDataIntoDB()
        ' get team data from API
        Dim data As JObject = Me.API.ReturnAllTeamsData()
        DB.loadTeamsDataIntoDB(data)

    End Sub

    'Public Function returnAllTeamNames() As DataTable
    'Return DB.returnAllTeamNames()
    'End Function

    Public Function findGamePk(teamId, gameDate) As Integer
        Dim gamePk As Integer = 0
        Dim schedule As JObject = Me.API.ReturnScheduleData(gameDate)
        Dim gameDates As JArray = schedule.SelectToken("dates")

        For Each gDate As JObject In gameDates
            Dim games As JArray = gDate.SelectToken("games")

            For Each game As JObject In games
                Dim awayId As String = game.SelectToken("teams.away.team.id")
                Dim homeId As String = game.SelectToken("teams.home.team.id")

                If homeId = teamId Or awayId = teamId Then
                    gamePk = game.SelectToken("gamePk")

                End If
            Next

        Next

        Return gamePk
    End Function

    Public Function refreshLiveData(gamePk) As JObject
        Me.liveData = Me.API.ReturnLiveFeedData(gamePk)

        ' TODO update database data

        Return Me.liveData
    End Function

    Public Function getLiveData() As JObject
        Return Me.liveData
    End Function

    Public Function getLineScoreData() As JObject
        Dim data As JObject = Me.liveData.SelectToken("liveData")
        Dim lineData As JObject = data.SelectToken("linescore")
        Return lineData
    End Function

    Public Function getBoxScoreData() As JObject
        Dim data As JObject = Me.liveData.SelectToken("liveData")
        Dim boxData As JObject = data.SelectToken("boxscore")
        Return boxData
    End Function

    Public Function getGameData() As JObject
        Dim data As JObject = Me.liveData.SelectToken("gameData")
        Return data
    End Function

    Public Function getCurrentGameData() As DataTable

        ' TODO - move to Database

        Dim sql As String = "SELECT * FROM current_game"
        Dim dt As DataTable = Me.DB.runQuery(sql)
        Return dt
    End Function

    Public Function getCurrentPlayData() As JObject
        Dim playData As JObject = Me.liveData.SelectToken("liveData.plays.currentPlay")
        Return playData
    End Function

    Public Function getLastPlayData() As JObject
        Dim currentPlayIdx As Integer = Me.liveData.SelectToken("liveData.plays.currentPlay.atBatIndex")
        Dim lastPlayData As JObject = getPlayData(currentPlayIdx - 1)
        Return lastPlayData
    End Function

    Public Function getPlayData(playIdx As Integer) As JObject
        If playIdx < 0 Then
            Return Nothing
        End If

        Dim allPlayData As JArray = Me.liveData.SelectToken("liveData.plays.allPlays")
        Dim playData = allPlayData(playIdx)
        Return playData
    End Function

    Public Function getLastPlayDescription() As String
        Try
            Dim lastPlayData As JObject = Me.getLastPlayData()
            Dim desc As String = lastPlayData.SelectToken("result.description")
            Dim playInning As String = lastPlayData.SelectToken("about.inning")
            Dim playInningHalf As String = lastPlayData.SelectToken("about.halfInning")
            Dim currentInning As String = getCurrentInningNumber()
            Dim currentInningHalf As String = getCurrentInningHalf()
            desc = String.Format("Last play: {0}", desc)
            If Not (playInning.Equals(currentInning)) Or Not (playInningHalf.ToUpper().Equals(currentInningHalf.ToUpper())) Then
                desc = String.Format("{0} {1}: {2}", playInningHalf.ToUpper(), playInning, desc)
            End If
            Return desc
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function getGameStatus() As String
        Dim gameData As JObject = Me.getLiveData()
        Dim gameStatus As String = gameData.SelectToken("gameData.status.detailedState")
        Return gameStatus.ToUpper
    End Function

    Public Function getCurrentInningHalf() As String
        Dim gameData As JObject = Me.getLiveData()
        Dim inningHalf As String = gameData.SelectToken("liveData.linescore.inningHalf")
        Return inningHalf.ToUpper
    End Function

    Public Function getCurrentInningNumber() As String
        Dim gameData As JObject = Me.getLiveData()
        Dim inningHalf As String = gameData.SelectToken("liveData.linescore.currentInning")
        Return inningHalf
    End Function

    Public Function getPlayerTeamAbbr(playerId As Integer) As String
        Dim team As String = String.Empty
        Dim dt As DataTable = DB.returnPlayerTeamAbbr(playerId)
        If dt.Rows.Count > 0 Then
            team = dt.Rows(0).Item(0).ToString
        End If
        Return team
    End Function

    'Public Function getHomeTeamAbbr() As String
    '    Dim team As String = String.Empty
    '    Dim dt As DataTable = DB.returnHomeTeamAbbr()
    '    If dt.Rows.Count > 0 Then
    '        team = dt.Rows(0).Item(0).ToString
    '    End If
    '    Return team
    'End Function

    'Public Function getAwayTeamAbbr() As String
    '    Dim team As String = String.Empty
    '    Dim dt As DataTable = DB.returnAwayTeamAbbr()
    '    If dt.Rows.Count > 0 Then
    '        team = dt.Rows(0).Item(0).ToString
    '    End If
    '    Return team
    'End Function

    Public Function GetHomeTeamId() As String
        Dim data As JObject = getBoxScoreData()
        Dim id As String = data.SelectToken("teams.home.team.id")
        Return id
    End Function

    Public Function GetHomeTeamFullName() As String
        Dim TeamData As Dictionary(Of String, String) = GetTeamData(GetHomeTeamId())
        Return TeamData("fullname")
    End Function

    Public Function GetHomeTeamShortName() As String
        Dim TeamData As Dictionary(Of String, String) = GetTeamData(GetHomeTeamId())
        Return TeamData("shortname")
    End Function

    Public Function GetHomeTeamAbbr() As String
        Dim TeamData As Dictionary(Of String, String) = GetTeamData(GetHomeTeamId())
        Return TeamData("abbr")
    End Function

    Public Function GetTeamData(TeamId As String) As Dictionary(Of String, String)
        Dim TeamData As Dictionary(Of String, String) = New Dictionary(Of String, String)
        Dim data As JObject = Me.API.ReturnAllTeamsData()
        Dim teams As JArray = data.SelectToken("teams")

        For Each team As JObject In teams
            If TeamId.Equals(team.SelectToken("id").ToString()) Then
                TeamData.Add("id", team.SelectToken("id").ToString())
                TeamData.Add("fullname", team.SelectToken("name"))
                TeamData.Add("shortname", team.SelectToken("teamName"))
                TeamData.Add("abbr", team.SelectToken("abbreviation"))
                Exit For
            End If
        Next
        Return TeamData
    End Function

    Public Function GetAwayTeamId() As String
        Dim data As JObject = getBoxScoreData()
        Dim id As String = data.SelectToken("teams.away.team.id")
        Return id
    End Function

    Public Function GetAwayTeamFullName() As String
        Dim TeamData As Dictionary(Of String, String) = GetTeamData(GetAwayTeamId())
        Return TeamData("fullname")
    End Function

    Public Function GetAwayTeamShortName() As String
        Dim TeamData As Dictionary(Of String, String) = GetTeamData(GetAwayTeamId())
        Return TeamData("shortname")
    End Function

    Public Function GetAwayTeamAbbr() As String
        Dim TeamData As Dictionary(Of String, String) = GetTeamData(GetAwayTeamId())
        Return TeamData("abbr")
    End Function

    'Public Function getHomeTeamName() As String
    '    Dim team As String = String.Empty
    '    Dim dt As DataTable = DB.returnHomeTeamName()
    '    If dt.Rows.Count > 0 Then
    '        team = dt.Rows(0).Item(0).ToString
    '    End If
    '    Return team
    'End Function

    'Public Function getAwayTeamName() As String
    '    Dim team As String = String.Empty
    '    Dim dt As DataTable = DB.returnAwayTeamName()
    '    If dt.Rows.Count > 0 Then
    '        team = dt.Rows(0).Item(0).ToString
    '    End If
    '    Return team
    'End Function

    Public Function IsAwayTeamWinner() As Boolean
        Dim lineData As JObject = getLineScoreData()
        Dim homeRuns As String = lineData.SelectToken("teams.home.runs")
        Dim awayRuns As String = lineData.SelectToken("teams.away.runs")

        If Convert.ToInt32(awayRuns) > Convert.ToInt32(homeRuns) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function getPitchingStats(pitcherId, team) As String
        Dim playData As JObject = getCurrentPlayData()
        pitcherId = "ID" + pitcherId.ToString

        Dim pitcherStats As String = String.Empty

        Dim playerData As JObject
        If team = Me.getAwayTeamAbbr() Then
            playerData = getBoxScoreData().SelectToken("teams.away.players")
        Else
            playerData = getBoxScoreData().SelectToken("teams.home.players")
        End If

        For Each player In playerData
            If player.Key = pitcherId Then
                Dim wins As String = player.Value.Item("seasonStats").Item("pitching").Item("wins")
                Dim losses As String = player.Value.Item("seasonStats").Item("pitching").Item("losses")
                Dim era As String = player.Value.Item("seasonStats").Item("pitching").Item("era")
                pitcherStats = String.Format(" ({0}-{1}, {2} ERA)", wins, losses, era)
            End If
        Next
        Return pitcherStats
    End Function

    Public Function getBatterStats(batterId, team) As String
        Dim playData As JObject = getCurrentPlayData()
        batterId = "ID" + batterId

        Dim batterStats As String = String.Empty

        Dim playerData As JObject
        If team = "away" Then
            playerData = getBoxScoreData().SelectToken("teams.away.players")
        Else
            playerData = getBoxScoreData().SelectToken("teams.home.players")
        End If

        For Each player In playerData
            If player.Key = batterId Then
                Dim hits As String = player.Value.Item("stats").Item("batting").Item("hits")
                Dim atBats As String = player.Value.Item("stats").Item("batting").Item("atBats")
                Dim avg As String = player.Value.Item("seasonStats").Item("batting").Item("avg")
                batterStats = String.Format(" ({0}-{1}, {2} AVG)", hits, atBats, avg)
            End If
        Next

        Return batterStats
    End Function

    Function LoadAllGamesData(gameDate As String) As List(Of Game)
        Dim ListOfGames As List(Of Game) = New List(Of Game)
        Dim schedule As JObject = Me.API.ReturnScheduleData(gameDate)
        Dim gameDates As JArray = schedule.SelectToken("dates")

        For Each gDate As JObject In gameDates
            Dim games As JArray = gDate.SelectToken("games")

            For Each game As JObject In games
                Dim gamePk As String = game.SelectToken("gamePk")
                Dim oGame As Game = New Game(gamePk)
                ListOfGames.Add(oGame)
            Next
        Next
        Return ListOfGames
    End Function

    'Sub LoadAllGamesData(gameDate As String)

    '    DB.ClearAllGamesData()

    '    Dim schedule As JObject = Me.API.ReturnScheduleData(gameDate)
    '    Dim gameDates As JArray = schedule.SelectToken("dates")

    '    For Each gDate As JObject In gameDates
    '        Dim games As JArray = gDate.SelectToken("games")

    '        For Each game As JObject In games
    '            Dim gamePk As String = game.SelectToken("gamePk")

    '            Dim awayId As String = game.SelectToken("teams.away.team.id")
    '            Dim homeId As String = game.SelectToken("teams.home.team.id")

    '            ' get abbrevs
    '            Dim dt As DataTable = DB.returnTeamAbbrevFromId(awayId)
    '            Dim awayName As String = dt.Rows(0).Item(0)
    '            dt = DB.returnTeamAbbrevFromId(homeId)
    '            Dim homeName As String = dt.Rows(0).Item(0)

    '            ' get liveData for each game
    '            Dim liveData As JObject = Me.API.ReturnLiveFeedData(gamePk)

    '            ' get status
    '            Dim gameStatus As String = liveData.SelectToken("gameData.status.detailedState")
    '            gameStatus = gameStatus.ToUpper

    '            Dim inning As String = ""
    '            Dim inningHalf As String = ""
    '            Dim awayScore As String = ""
    '            Dim homeScore As String = ""

    '            If gameStatus = "IN PROGRESS" Or gameStatus = "FINAL" Or gameStatus = "COMPLETE" Or gameStatus = "GAME OVER" Then
    '                Dim lineScoreData As JObject = liveData.SelectToken("liveData.linescore")
    '                inning = lineScoreData.SelectToken("currentInning")
    '                inningHalf = lineScoreData.SelectToken("inningHalf")
    '                awayScore = lineScoreData.SelectToken("teams.away.runs")
    '                homeScore = lineScoreData.SelectToken("teams.home.runs")
    '            End If

    '            If gameStatus.ToUpper = "SCHEDULED" Or gameStatus = "WARMUP" Or gameStatus = "PRE-GAME" Then
    '                ' noop
    '            End If

    '            ' insert into database
    '            Me.DB.InsertAllGamesData(gamePk, awayName, awayScore, homeName, homeScore, gameStatus.ToUpper, inning, inningHalf.ToUpper)
    '        Next
    '    Next
    'End Sub

    Function GetAllGamesData() As DataTable
        Dim dt As DataTable = DB.returnAllGamesData()
        Dim dtGames As New DataTable()

        ' add columns to new datatable
        dtGames.Columns.Add(New DataColumn("Id"))
        dtGames.Columns.Add(New DataColumn("Away"))
        dtGames.Columns.Add(New DataColumn("Score"))
        dtGames.Columns.Add(New DataColumn("Home"))
        dtGames.Columns.Add(New DataColumn("Status"))
        dtGames.Columns.Add(New DataColumn("Inning"))

        For Each row As DataRow In dt.Rows
            ' add row to new datatable
            Dim dr As DataRow = dtGames.NewRow
            dr(0) = row.Item("id").ToString
            dr(1) = row.Item("away_abbrev").ToString
            dr(3) = row.Item("home_abbrev").ToString
            dr(2) = row.Item("away_score").ToString + " - " + row.Item("home_score").ToString
            dr(4) = row.Item("status").ToString

            Dim status As String = row.Item("status").ToString.ToUpper
            If status = "FINAL" Or status = "COMPLETE" Or status = "GAME OVER" Then
                If Convert.ToUInt32(row.Item("inning").ToString) > 9 Then
                    dr(5) = row.Item("inning").ToString
                Else
                    dr(5) = ""
                End If
            ElseIf status = "SCHEDULED" Or status = "WARMUP" Or status = "PRE-GAME" Or status.StartsWith("DELAYED") Or status = "POSTPONED" Then
                dr(5) = ""
            Else
                dr(5) = row.Item("inning_half").ToString + " " + row.Item("inning").ToString
            End If
            dtGames.Rows.Add(dr)
        Next

        Return dtGames
    End Function

    Function getTeamLineup(teamAbbr As String) As DataTable
        Dim dt As DataTable = DB.returnTeamLineup(teamAbbr)
        Return dt
    End Function

    Function getCurrentBatterId() As String
        Dim playData As JObject = getCurrentPlayData()
        Dim batterId As String = playData.SelectToken("matchup.batter.id").ToString
        Return batterId
    End Function

    Function getCurrentPitcherId() As String
        Dim playData As JObject = getCurrentPlayData()
        Dim pitcherId As String = playData.SelectToken("matchup.pitcher.id").ToString
        Return pitcherId
    End Function

    Function getCurrentInningState() As String
        Dim gameData As JObject = Me.getLiveData()
        Dim inningState As String = gameData.SelectToken("liveData.linescore.inningState")
        Return inningState.ToUpper
    End Function

    Function getLastOutBatterId() As String

        Dim lastInning As Integer = Me.getCurrentInningNumber() - 2 'zero-indexed
        If lastInning < 0 Then
            lastInning = 0
        End If

        Dim liveData As JObject = Me.getLiveData()
        Dim jsonPath As String
        Dim allPlayData As JObject = liveData.SelectToken("liveData.plays")
        'Dim allPlayDataByInning As JArray = liveData.SelectToken("liveData.plays.playsByInning")

        If Me.getCurrentInningState = "END" Then
            jsonPath = "liveData.plays.playsByInning[" + lastInning.ToString + "].top"
        Else
            jsonPath = "liveData.plays.playsByInning[" + lastInning.ToString + "].bottom"
        End If
        Dim data = liveData.SelectToken(jsonPath)
        Dim lastPlayIndex = data(data.Count - 1)
        jsonPath = "allPlays[" + lastPlayIndex.ToString + "].matchup.batter.id"

        Dim lastBatterId As String = allPlayData.SelectToken(jsonPath)

        Return lastBatterId
    End Function

    Public Function getProperties() As Properties
        Dim props As Properties = New Properties()
        Return props
    End Function
    Public Function getValueFromConfig(key As String, defaultValue As String) As Integer
        Dim props As Properties = New Properties()
        Dim value = props.GetProperty(key, defaultValue)
        Return value
    End Function

    Public Sub addValueToConfig(key As String, value As String)
        Dim props As Properties = New Properties()
        props.Add(key, value)
    End Sub

    Public Sub writeConfig()
        Dim props As Properties = New Properties()
        props.Write()
    End Sub

    Public Function getUpdateSecsFromConfig() As String
        Dim secs As String = getValueFromConfig("timer", "15")
        Dim seconds As Integer = Convert.ToInt32(secs)
        Return seconds * 1000
    End Function

    Public Function getAllTeamAbbrevs() As DataTable
        Dim dt As DataTable = DB.returnAllTeamAbbrevs()
        Return dt
    End Function

    Public Function getVenueWeather() As String
        Dim gameData As JObject = getGameData()
        Dim temp As String = gameData.SelectToken("weather.temp")
        Dim conditions As String = gameData.SelectToken("weather.condition")
        'Dim wind As String = gameData.SelectToken("weather.wind")
        'Return $"Weather: {temp}°F, {conditions}, wind {wind}"
        If temp Is Nothing Or conditions Is Nothing Then
            Return ""
        Else
            Return $"Weather: {temp}°F, {conditions}"
        End If

    End Function
End Class
