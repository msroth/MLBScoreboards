' =========================================================================================================
' (C) 2022 MSRoth
'
' Released under XXX license.
' =========================================================================================================

Imports System.ComponentModel
Imports System.Net
Imports System.Net.Http
Imports Newtonsoft.Json.Linq
Imports NLog

Public Class MlbApi

    Private API_BASE_URL As String = "https://statsapi.mlb.com/api"
    Private API_LIVEFEED_URL As String = API_BASE_URL + "/v1.1/game/{0}/feed/live"
    Private API_ALL_TEAMS_URL As String = API_BASE_URL + "/v1/teams?sportId=1&activeStatus=ACTIVE"
    Private API_TEAM_URL As String = API_ALL_TEAMS_URL + "/v1/teams/{0}"
    Private API_SCHEDULE_URL As String = API_BASE_URL + "/v1/schedule?sportId=1&date={0}&hydrate=broadcasts"
    Private API_MULTIPLE_PERSON_DATA_URL As String = API_BASE_URL + "/v1/people?personIds={0}"
    Private API_SINGLE_PERSON_DATA_URL As String = API_BASE_URL + "/v1/people/{0}"
    Private API_PERSON_STATS_URL As String = API_BASE_URL + "/v1/people/{0}?hydrate=stats(group={1},type={2})"
    Private API_STANDINGS_URL As String = API_BASE_URL + "/v1/standings?leagueId=103,104&season={0}&standingsTypes=regularSeason&hydrate=team(division)&fields=records,standingsType,teamRecords,team,name,division,id,nameShort,abbreviation,divisionRank,gamesBack,wildCardRank,wildCardGamesBack,wildCardEliminationNumber,divisionGamesBack,clinched,eliminationNumber,winningPercentage,type,wins,losses,leagueRank,sportRank"
    Private API_POSTSEASON_STANDINGS_URL As String = API_BASE_URL + "/v1/schedule/postseason/series?season={0}"

    Shared ReadOnly Logger As Logger = LogManager.GetCurrentClassLogger()


    'Shared client As HttpClient = New HttpClient()

    'Private Async Function GetData(url As String) As Task(Of String)
    '    Dim responseBody As String = "No Data"
    '    Try
    '        responseBody = Await client.GetStringAsync(url)
    '        Trace.WriteLine(responseBody)
    '    Catch e As HttpRequestException
    '        Trace.WriteLine(Environment.NewLine & "Exception Caught!")
    '        Trace.WriteLine("Message :{0} ", e.Message)
    '    End Try
    '    Return responseBody
    'End Function

    'Async Function ReturnLiveFeedData(gamePk) As Task(Of JObject)
    '    Dim url As String = String.Format(API_LIVEFEED_URL, gamePk)
    '    Dim r As String = Await GetData(url)
    '    Dim json As JObject = JObject.Parse(r)
    '    Return json
    'End Function

    'Async Function ReturnScheduleData(gameDate) As Task(Of JObject)
    '    Dim url As String = String.Format(API_SCHEDULE_URL, gameDate)
    '    Dim r As String = Await GetData(url)
    '    Dim json As JObject = JObject.Parse(r)
    '    Return json
    'End Function

    'Async Function ReturnAllTeamsData() As Task(Of JObject)
    '    Dim r As String = Await GetData(API_ALL_TEAMS_URL)
    '    Dim json As JObject = JObject.Parse(r)
    '    Return json
    'End Function

    'Async Function ReturnTeamData(TeamId As String) As Task(Of JObject)
    '    Dim url As String = String.Format(API_TEAM_URL, TeamId)
    '    Dim r As String = Await GetData(url)
    '    Dim json As JObject = JObject.Parse(r)
    '    Return json
    'End Function

    'Async Function ReturnMultiplePlayerData(PlayerIds As List(Of String)) As Task(Of JObject)
    '    Dim ids As String = ""
    '    For Each id As String In PlayerIds
    '        id += id + ","
    '    Next
    '    ids = ids.Substring(1, ids.Length - 1)
    '    Dim url As String = String.Format(API_MULTIPLE_PERSON_DATA_URL, ids)
    '    Dim r As String = Await GetData(url)
    '    Dim json As JObject = JObject.Parse(r)
    '    Return json
    'End Function

    'Async Function ReturnPlayerData(PlayerId As String) As Task(Of JObject)
    '    Dim url As String = String.Format(API_SINGLE_PERSON_DATA_URL, PlayerId)
    '    Dim r As String = Await GetData(url)
    '    Dim json As JObject = JObject.Parse(r)
    '    Return json
    'End Function

    'Async Function ReturnPlayerStats(PlayerId As String, GamePk As String) As Task(Of JObject)
    '    Dim url As String = String.Format(API_PERSON_STATS_URL, PlayerId, GamePk)
    '    Dim r As String = Await GetData(url)
    '    Dim json As JObject = JObject.Parse(r)
    '    Return json
    'End Function

    ' ========================

    Function GetData(url)
        Logger.Debug($"API URL: {url}")
        Dim result As String = ""
        Try
            Using client As New WebClient()
                result = client.DownloadString(url)
            End Using
        Catch ex As Exception
            Logger.Error(ex)
        End Try
        Return result
    End Function

    Function ReturnLiveFeedData(gamePk) As JObject
        Dim url As String = String.Format(API_LIVEFEED_URL, gamePk)
        Dim r As String = GetData(url)
        Dim json As JObject = JObject.Parse(r)
        Return json
    End Function

    Function ReturnScheduleData(gameDate) As JObject
        Dim url As String = String.Format(API_SCHEDULE_URL, gameDate)
        Dim r As String = GetData(url)
        Dim json As JObject = JObject.Parse(r)
        Return json
    End Function

    Function ReturnAllTeamsData() As JObject
        Dim r As String = GetData(API_ALL_TEAMS_URL)
        Dim json As JObject = JObject.Parse(r)
        Return json
    End Function

    Function ReturnTeamData(TeamId As String) As JObject
        Dim url As String = String.Format(API_TEAM_URL, TeamId)
        Dim r As String = GetData(url)
        Dim json As JObject = JObject.Parse(r)
        Return json
    End Function

    Function ReturnMultiplePlayerData(PlayerIds As List(Of String)) As JObject
        Dim ids As String = ""
        For Each id As String In PlayerIds
            id += id + ","
        Next
        ids = ids.Substring(1, ids.Length - 1)
        Dim url As String = String.Format(API_MULTIPLE_PERSON_DATA_URL, ids)
        Dim r As String = GetData(url)
        Dim json As JObject = JObject.Parse(r)
        Return json
    End Function

    Function ReturnPlayerData(PlayerId As String) As JObject
        Dim url As String = String.Format(API_SINGLE_PERSON_DATA_URL, PlayerId)
        Dim r As String = GetData(url)
        Dim json As JObject = JObject.Parse(r)
        Return json
    End Function

    Function ReturnPlayerStats(PlayerId As String, period As String, statType As String) As JObject
        Dim url As String = String.Format(API_PERSON_STATS_URL, PlayerId, statType, period)
        Dim r As String = GetData(url)
        Dim json As JObject = JObject.Parse(r)
        Return json
    End Function

    Function ReturnSeasonStandings(year As String) As JObject
        Dim url As String = String.Format(API_STANDINGS_URL, year)
        Dim r As String = GetData(url)
        Dim json As JObject = JObject.Parse(r)
        Return json
    End Function

    Function ReturnPostSeasonStandings(year As String) As JObject
        Dim url As String = String.Format(API_POSTSEASON_STANDINGS_URL, year)
        Dim r As String = GetData(url)
        Dim json As JObject = JObject.Parse(r)
        Return json
    End Function
End Class
