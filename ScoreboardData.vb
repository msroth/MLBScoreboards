

Imports Newtonsoft.Json.Linq


Public Class ScoreboardData

    Dim API As MlbApi = New MlbApi()
    Dim DB As Database = New Database()
    Dim liveData As JObject
    Dim gamePk As Integer = 0


    Public Sub New()

    End Sub

    'Public Function getAPI() As MLB_API
    '    Return API
    'End Function

    'Public Function getDB() As Database
    '    Return DB
    'End Function

    'Public Sub loadCurrentGameDataIntoDB(liveData, boxData, gameDate, gameTime, gamePk)
    '    Me.DB.loadCurrentGameDataIntoDB(liveData, boxData, gameDate, gameTime, gamePk)

    'End Sub

    'Public Function LoadTeamsData() As Dictionary(Of String, Team)
    '    Dim Teams As Dictionary(Of String, Team) = New Dictionary(Of String, Team)
    '    Try
    '        Dim Data As JObject = API.ReturnAllTeamsData()
    '        Dim TeamsData As JArray = Data.SelectToken("teams")

    '        For Each Team As JObject In TeamsData
    '            Dim teamId As String = Team.SelectToken("id").ToString()
    '            Dim oTeam As Team = New Team(Convert.ToInt32(teamId))
    '            Teams.Add(teamId, oTeam)
    '            Trace.WriteLine($"Loading team {oTeam.Id} - {oTeam.Abbr} data")
    '        Next
    '    Catch ex As Exception
    '        Trace.WriteLine($"ERROR: LoadTeamsData - {ex}")
    '    End Try
    '    Return Teams
    'End Function

    'Public Function getLiveData() As JObject
    '    Return Me.liveData
    'End Function

    'Public Function getLineScoreData() As JObject
    '    Dim data As JObject = Me.liveData.SelectToken("liveData")
    '    Dim lineData As JObject = data.SelectToken("linescore")
    '    Return lineData
    'End Function

    'Public Function getBoxScoreData() As JObject
    '    Dim data As JObject = Me.liveData.SelectToken("liveData")
    '    Dim boxData As JObject = data.SelectToken("boxscore")
    '    Return boxData
    'End Function

    'Public Function getGameData() As JObject
    ' Dim data As JObject = Me.liveData.SelectToken("gameData")
    ' Return data
    'End Function

    'Public Function getCurrentPlayData(ThisGame As Game) As JObject
    '    Dim playData As JObject = ThisGame.LiveData.SelectToken("plays.currentPlay")
    '    Return playData
    'End Function

    'Public Function getLastPlayData(ThisGame As Game) As JObject
    '    Dim currentPlayIdx As Integer = ThisGame.LiveData.SelectToken("plays.currentPlay.atBatIndex")
    '    Dim lastPlayData As JObject = getPlayData(ThisGame, currentPlayIdx - 1)
    '    Return lastPlayData
    'End Function

    'Public Function getPlayData(ThisGame As Game, playIdx As Integer) As JObject
    '    If playIdx <= 0 Then
    '        Return Nothing
    '    End If

    '    Dim allPlayData As JArray = ThisGame.LiveData.SelectToken("plays.allPlays")
    '    Dim playData = allPlayData(playIdx)
    '    Return playData
    'End Function

    'Public Function GetLastPitchDescription(ThisGame As Game) As String
    '    Dim desc As String

    '    Try
    '        'Dim playData As JObject = ThisGame.LiveData.SelectToken("plays.currentPlay")
    '        'Dim currentPlayData As JObject = Me.getCurrentPlayData(ThisGame)

    '        Dim currentPlayData As JObject = ThisGame.CurrentPlayData()
    '        Dim playEvents As JArray = currentPlayData.SelectToken("playEvents")
    '        Dim lastEventIdx As Int32 = playEvents.Count - 1
    '        If lastEventIdx < 0 Then
    '            Return String.Empty
    '        End If
    '        Dim pitchResults = playEvents.Item(lastEventIdx)
    '        Dim pitchType = pitchResults.SelectToken("details.type.description")
    '        Dim pitchCall As String = pitchResults.SelectToken("details.description")
    '        Dim startSpeed As String = pitchResults.SelectToken("pitchData.startSpeed")
    '        Dim endSpeed As String = pitchResults.SelectToken("pitchData.endSpeed")
    '        Dim pitchNum As String = pitchResults.SelectToken("pitchNumber")

    '        If pitchType = String.Empty Then
    '            desc = String.Empty
    '        ElseIf startSpeed = "" Then
    '            desc = String.Format("{0}", pitchCall)
    '        Else
    '            desc = String.Format("Pitch #{0}: {1} - {4}  (Start {2}mph, End {3}mph)", pitchNum, pitchType, startSpeed, endSpeed, pitchCall)
    '        End If
    '    Catch ex As Exception
    '        Trace.WriteLine($"ERROR: GetLastPitchDescription - {ex}")
    '    End Try
    '    Return desc
    'End Function

    'Public Function GetLastPlayDescription(ThisGame As Game) As String
    '    Dim desc As String
    '    Try
    '        Dim currentPlayIdx As Integer = ThisGame.LiveData.SelectToken("plays.currentPlay.atBatIndex")
    '        If currentPlayIdx = 0 Then
    '            Return ""
    '        End If
    '        Dim lastPlayData As JObject = getPlayData(ThisGame, currentPlayIdx - 1)
    '        Dim allPlayData As JArray = ThisGame.LiveData.SelectToken("plays.allPlays")
    '        desc = lastPlayData.SelectToken("result.description")
    '        Dim playInning As String = lastPlayData.SelectToken("about.inning")
    '        Dim playInningHalf As String = lastPlayData.SelectToken("about.halfInning")

    '        desc = String.Format("Last play: {0}", desc)
    '        If Not (playInning.Equals(ThisGame.CurrentInning)) Or
    '            Not (playInningHalf.ToUpper().Equals(ThisGame.CurrentInningHalf.ToUpper())) Then
    '            desc = String.Format("{0} {1}: {2}", playInningHalf.ToUpper(), playInning, desc)
    '        End If

    '    Catch ex As Exception
    '        Trace.WriteLine($"ERROR: GetLastPlayDescription - {ex}")
    '    End Try
    '    Return desc

    'End Function

    'Public Function getCurrentInningNumber() As String
    '    Dim gameData As JObject = Me.getLiveData()
    '    Dim inningHalf As String = gameData.SelectToken("liveData.linescore.currentInning")
    '    Return inningHalf
    'End Function

    'Public Function getPlayerTeamAbbr(playerId As Integer) As String
    '    Dim team As String = String.Empty
    '    Dim dt As DataTable = DB.returnPlayerTeamAbbr(playerId)
    '    If dt.Rows.Count > 0 Then
    '        team = dt.Rows(0).Item(0).ToString
    '    End If
    '    Return team
    'End Function

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

    'Public Function GetHomeTeamId() As String
    '    Dim data As JObject = getBoxScoreData()
    '    Dim id As String = data.SelectToken("teams.home.team.id")
    '    Return id
    'End Function

    'Public Function GetHomeTeamFullName() As String
    '    Dim TeamData As Dictionary(Of String, String) = GetTeamData(GetHomeTeamId())
    '    Return TeamData("fullname")
    'End Function

    'Public Function GetHomeTeamShortName() As String
    '    Dim TeamData As Dictionary(Of String, String) = GetTeamData(GetHomeTeamId())
    '    Return TeamData("shortname")
    'End Function

    'Public Function GetHomeTeamAbbr() As String
    '    Dim TeamData As Dictionary(Of String, String) = GetTeamData(GetHomeTeamId())
    '    Return TeamData("abbr")
    'End Function

    'Public Function GetTeamData(TeamId As String) As Dictionary(Of String, String)
    '    Dim TeamData As Dictionary(Of String, String) = New Dictionary(Of String, String)

    '    Try
    '        Dim data As JObject = Me.API.ReturnAllTeamsData()
    '        Dim teams As JArray = data.SelectToken("teams")

    '        For Each team As JObject In teams
    '            If TeamId.Equals(team.SelectToken("id").ToString()) Then
    '                TeamData.Add("id", team.SelectToken("id").ToString())
    '                TeamData.Add("fullname", team.SelectToken("name"))
    '                TeamData.Add("shortname", team.SelectToken("teamName"))
    '                TeamData.Add("abbr", team.SelectToken("abbreviation"))
    '                Exit For
    '            End If
    '        Next
    '    Catch ex As Exception
    '        Trace.WriteLine($"ERROR: GetTeamData - {ex}")
    '    End Try
    '    Return TeamData
    'End Function

    'Public Function GetAwayTeamId() As String
    '    Dim data As JObject = getBoxScoreData()
    '    Dim id As String = data.SelectToken("teams.away.team.id")
    '    Return id
    'End Function

    'Public Function GetAwayTeamFullName() As String
    '    Dim TeamData As Dictionary(Of String, String) = GetTeamData(GetAwayTeamId())
    '    Return TeamData("fullname")
    'End Function

    'Public Function GetAwayTeamShortName() As String
    '    Dim TeamData As Dictionary(Of String, String) = GetTeamData(GetAwayTeamId())
    '    Return TeamData("shortname")
    'End Function

    'Public Function GetAwayTeamAbbr() As String
    '    Dim TeamData As Dictionary(Of String, String) = GetTeamData(GetAwayTeamId())
    '    Return TeamData("abbr")
    'End Function

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

    'Public Function IsAwayTeamWinner() As Boolean
    '    Dim lineData As JObject = getLineScoreData()
    '    Dim homeRuns As String = lineData.SelectToken("teams.home.runs")
    '    Dim awayRuns As String = lineData.SelectToken("teams.away.runs")

    '    If Convert.ToInt32(awayRuns) > Convert.ToInt32(homeRuns) Then
    '        Return True
    '    Else
    '        Return False
    '    End If
    'End Function

    'Public Function GetPitcherMatchup(ThisGame As Game) As String
    '    Dim matchup As String = ""
    '    Dim awayPitcherId As String = ThisGame.AwayScheduledPitcherId()
    '    Dim homePitcherId As String = ThisGame.HomeScheduledPitcherId()

    '    Dim awayPitcherName As String
    '    If ThisGame.AwayTeam.GetPlayerData(awayPitcherId) Is Nothing Then
    '        awayPitcherName = "TBD"
    '    Else
    '        awayPitcherName = ThisGame.AwayTeam.GetPlayerData(awayPitcherId).Name
    '    End If

    '    'Dim awayPitcherName As String = ThisGame.AwayTeam.GetPlayerData(awayPitcherId).Name

    '    Dim homePitcherName As String
    '    If ThisGame.HomeTeam.GetPlayerData(homePitcherId) Is Nothing Then
    '        homePitcherName = "TBD"
    '    Else
    '        homePitcherName = ThisGame.HomeTeam.GetPlayerData(homePitcherId).Name
    '    End If

    '    'Dim homePitcherName As String = ThisGame.HomeTeam.GetPlayerData(homePitcherId).Name

    '    'Dim awayPitchingStats As String = getPitchingStats(awayPitcherId, ThisGame.AwayTeam.Abbr)
    '    'Dim homePitchingStats As String = getPitchingStats(homePitcherId, ThisGame.HomeTeam.Abbr)
    '    Dim awayPitchingStats As String = getPitchingStats(awayPitcherId, ThisGame)
    '    Dim homePitchingStats As String = getPitchingStats(homePitcherId, ThisGame)
    '    matchup = String.Format("Preview: {0} {1} vs. {2} {3}", awayPitcherName, awayPitchingStats, homePitcherName, homePitchingStats)
    '    Return matchup
    'End Function

    'Public Function getPitchingStats(pitcherId As Integer, ThisGame As Game) As String
    '    'Dim playData As JObject = getCurrentPlayData()
    '    Dim pitcherStats As String = String.Empty

    '    'TODO - get these stats from player object
    '    Dim playerData As JObject
    '    'If teamAbbr = Me.GetAwayTeamAbbr() Then
    '    If ThisGame.AwayTeam.GetPlayerData(pitcherId) IsNot Nothing Then
    '        playerData = getBoxScoreData().SelectToken("teams.away.players")
    '    Else
    '        playerData = getBoxScoreData().SelectToken("teams.home.players")
    '    End If

    '    Dim pId As String = "ID" + pitcherId.ToString()
    '    For Each player In playerData
    '        If player.Key = pId Then
    '            Dim wins As String = player.Value.Item("seasonStats").Item("pitching").Item("wins")
    '            Dim losses As String = player.Value.Item("seasonStats").Item("pitching").Item("losses")
    '            Dim era As String = player.Value.Item("seasonStats").Item("pitching").Item("era")
    '            pitcherStats = String.Format(" ({0}-{1}, {2} ERA)", wins, losses, era)
    '            Exit For
    '        End If
    '    Next
    '    Return pitcherStats
    'End Function

    'Public Function getBatterStats(batterId As Integer, ThisGame As Game) As String
    '    'Dim playData As JObject = getCurrentPlayData()

    '    Dim batterStats As String = String.Empty

    '    'TODO - get these stats from player object
    '    Dim playerData As JObject
    '    'If team = "away" Then
    '    If ThisGame.AwayTeam.GetPlayer(batterId) IsNot Nothing Then
    '        playerData = getBoxScoreData().SelectToken("teams.away.players")
    '    Else
    '        playerData = getBoxScoreData().SelectToken("teams.home.players")
    '    End If

    '    Dim bId As String = "ID" + batterId.ToString
    '    For Each player In playerData
    '        If player.Key = bId Then
    '            Dim hits As String = player.Value.Item("stats").Item("batting").Item("hits")
    '            Dim atBats As String = player.Value.Item("stats").Item("batting").Item("atBats")
    '            Dim avg As String = player.Value.Item("seasonStats").Item("batting").Item("avg")
    '            batterStats = String.Format(" ({0}-{1}, {2} AVG)", hits, atBats, avg)
    '        End If
    '    Next

    '    Return batterStats
    'End Function

    'Function LoadAllGamesData(gameDate As String) As Dictionary(Of String, Game)
    '    Dim ListOfGames As Dictionary(Of String, Game) = New Dictionary(Of String, Game)

    '    Try
    '        Dim schedule As JObject = Me.API.ReturnScheduleData(gameDate)
    '        Dim gameDates As JArray = schedule.SelectToken("dates")

    '        For Each gDate As JObject In gameDates
    '            Dim games As JArray = gDate.SelectToken("games")

    '            For Each game As JObject In games
    '                Dim gamePk As String = game.SelectToken("gamePk")
    '                Dim oGame As Game = New Game(gamePk)
    '                ListOfGames.Add(gamePk, oGame)
    '            Next
    '        Next
    '    Catch ex As Exception
    '        Trace.WriteLine($"ERROR: LoadAllTeamsData - {ex}")
    '    End Try
    '    Return ListOfGames
    'End Function

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

    'Function GetAllGamesData() As DataTable
    '    Dim dt As DataTable = DB.returnAllGamesData()
    '    Dim dtGames As New DataTable()

    '    ' add columns to new datatable
    '    dtGames.Columns.Add(New DataColumn("Id"))
    '    dtGames.Columns.Add(New DataColumn("Away"))
    '    dtGames.Columns.Add(New DataColumn("Score"))
    '    dtGames.Columns.Add(New DataColumn("Home"))
    '    dtGames.Columns.Add(New DataColumn("Status"))
    '    dtGames.Columns.Add(New DataColumn("Inning"))

    '    For Each row As DataRow In dt.Rows
    '        ' add row to new datatable
    '        Dim dr As DataRow = dtGames.NewRow
    '        dr(0) = row.Item("id").ToString
    '        dr(1) = row.Item("away_abbrev").ToString
    '        dr(3) = row.Item("home_abbrev").ToString
    '        dr(2) = row.Item("away_score").ToString + " - " + row.Item("home_score").ToString
    '        dr(4) = row.Item("status").ToString

    '        Dim status As String = row.Item("status").ToString.ToUpper
    '        If status = "FINAL" Or status = "COMPLETE" Or status = "GAME OVER" Then
    '            If Convert.ToUInt32(row.Item("inning").ToString) > 9 Then
    '                dr(5) = row.Item("inning").ToString
    '            Else
    '                dr(5) = ""
    '            End If
    '        ElseIf status = "SCHEDULED" Or status = "WARMUP" Or status = "PRE-GAME" Or status.StartsWith("DELAYED") Or status = "POSTPONED" Then
    '            dr(5) = ""
    '        Else
    '            dr(5) = row.Item("inning_half").ToString + " " + row.Item("inning").ToString
    '        End If
    '        dtGames.Rows.Add(dr)
    '    Next

    '    Return dtGames
    'End Function

    'Function getTeamLineup(teamAbbr As String) As DataTable
    '    Dim dt As DataTable = DB.returnTeamLineup(teamAbbr)
    '    Return dt
    'End Function

    'Function getCurrentBatterId(ThisGame As Game) As String
    '    Dim playData As JObject = getCurrentPlayData(ThisGame)
    '    Dim batterId As String = playData.SelectToken("matchup.batter.id").ToString
    '    Return batterId
    'End Function

    'Function getCurrentPitcherId(ThisGame As Game) As String
    '    Dim playData As JObject = getCurrentPlayData(ThisGame)
    '    Dim pitcherId As String = playData.SelectToken("matchup.pitcher.id").ToString
    '    Return pitcherId
    'End Function

    'Function GetLastOutBatterId(ThisGame As Game) As String
    '    Dim lastBatterId As String = 0
    '    Try
    '        Dim lastInning As Integer = ThisGame.CurrentInning - 1
    '        If lastInning < 0 Then
    '            lastInning = 0
    '        End If

    '        Dim jsonPath As String
    '        Dim allPlayData As JObject = ThisGame.LiveData.SelectToken("plays")

    '        If ThisGame.CurrentInningState = "END" Then
    '            jsonPath = "plays.playsByInning[" + lastInning.ToString + "].top"
    '        Else
    '            jsonPath = "plays.playsByInning[" + lastInning.ToString + "].bottom"
    '        End If

    '        Dim data = ThisGame.LiveData.SelectToken(jsonPath)
    '        Dim lastPlayIndex = data(data.Count - 1)
    '        jsonPath = "allPlays[" + lastPlayIndex.ToString + "].matchup.batter.id"

    '        lastBatterId = allPlayData.SelectToken(jsonPath)
    '    Catch ex As Exception
    '        Trace.WriteLine($"ERROR: getLastBatterId - {ex}")
    '    End Try
    '    Return lastBatterId
    'End Function

    'Public Function GetProperties() As Properties
    '    Dim props As Properties = New Properties()
    '    Return props
    'End Function

    'Public Function getValueFromConfig(key As String, defaultValue As String) As Integer
    '    Dim props As SBProperties = New SBProperties()
    '    Dim value = props.GetProperty(key, defaultValue)
    '    Return value
    'End Function

    'Public Sub addValueToConfig(key As String, value As String)
    '    Dim props As Properties = New Properties()
    '    props.Add(key, value)
    'End Sub

    'Public Sub writeConfig()
    '    Dim props As Properties = New Properties()
    '    props.Write()
    'End Sub

    'Public Function getUpdateSecsFromConfig() As String
    '    Dim secs As String = getValueFromConfig("timer", "15")
    '    Dim seconds As Integer = Convert.ToInt32(secs)
    '    Return seconds * 1000
    'End Function

    'Public Function getAllTeamAbbrevs() As DataTable
    '    Dim dt As DataTable = DB.returnAllTeamAbbrevs()
    '    Return dt
    'End Function

    'Public Function getVenueWeather() As String
    '    Dim gameData As JObject = getGameData()
    '    Dim temp As String = gameData.SelectToken("weather.temp")
    '    Dim conditions As String = gameData.SelectToken("weather.condition")
    '    'Dim wind As String = gameData.SelectToken("weather.wind")
    '    'Return $"Weather: {temp}°F, {conditions}, wind {wind}"
    '    If temp Is Nothing Or conditions Is Nothing Then
    '        Return ""
    '    Else
    '        Return $"Weather: {temp}°F, {conditions}"
    '    End If

    'End Function

    'Public Function GetDueUpBatters(ThisGame As Game) As List(Of Player)
    '    Dim DueUpBatters As List(Of Player) = New List(Of Player)


    '    ' get last batter
    '    Dim lastBatterId As String = 0
    '    Try
    '        Dim lastInning As Integer = ThisGame.CurrentInning - 1
    '        If lastInning <= 0 Then
    '            lastInning = 0
    '        End If

    '        ' if last inning = 0, 1st inning and no batters yet
    '        ' TODO - get batting order and return top three ids

    '        Dim jsonPath As String
    '        Dim allPlayData As JObject = ThisGame.LiveData.SelectToken("plays")

    '        If ThisGame.CurrentInningState = "END" Then
    '            jsonPath = $"playsByInning[{lastInning}].top"
    '        Else
    '            jsonPath = $"playsByInning[{lastInning}].bottom"
    '        End If

    '        Dim data As JArray = allPlayData.SelectToken(jsonPath)
    '        Dim lastPlayIndex = data.Item(data.Count() - 1)  ' zero based
    '        jsonPath = "allPlays[" + lastPlayIndex.ToString() + "].matchup.batter.id"

    '        lastBatterId = allPlayData.SelectToken(jsonPath)


    '        ' figure out next three batters
    '        Dim lastBatter As Player
    '        Dim lineup As DataTable
    '        Dim lastBattingPosition As Integer

    '        ' Away Team
    '        If ThisGame.CurrentInningState = "END" Then
    '            lastBatter = ThisGame.AwayTeam.GetPlayer(lastBatterId)
    '            lineup = ThisGame.AwayTeam.GetLinupTable()
    '            lastBattingPosition = lastBatter.BattingPosition()
    '            Dim battingPosition As Integer = lastBattingPosition
    '            For i As Integer = 1 To 3
    '                battingPosition = battingPosition + 1
    '                If battingPosition > 9 Then
    '                    battingPosition = battingPosition - 9
    '                End If

    '                For Each row As DataRow In lineup.Rows()
    '                    If battingPosition = row("BattingOrder") Then
    '                        DueUpBatters.Add(ThisGame.AwayTeam.GetPlayer(row("Id")))
    '                    End If
    '                Next
    '            Next
    '        Else
    '            ' Home Team
    '            lastBatter = ThisGame.HomeTeam.GetPlayer(lastBatterId)
    '            lineup = ThisGame.AwayTeam.GetLinupTable()
    '            lastBattingPosition = lastBatter.BattingPosition()
    '            Dim battingPosition As Integer = lastBattingPosition
    '            For i As Integer = 1 To 3
    '                battingPosition = battingPosition + 1

    '                If battingPosition > 9 Then
    '                    battingPosition = battingPosition - 9
    '                End If

    '                For Each row As DataRow In lineup.Rows()
    '                    If battingPosition = row("BattingOrder") Then
    '                        DueUpBatters.Add(ThisGame.HomeTeam.GetPlayer(row("Id")))
    '                    End If
    '                Next
    '            Next



    '        End If

    '    Catch ex As Exception
    '        Trace.WriteLine($"ERROR: GetDueUpBatters - get last batter Id - {ex}")
    '    End Try



    '    Return DueUpBatters
    'End Function


End Class
