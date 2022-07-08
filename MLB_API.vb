


Imports Newtonsoft.Json.Linq

Public Class MLB_API

    Dim API_BASE_URL As String = "http://statsapi.mlb.com/api"
    Dim API_LIVEFEED_URL As String = API_BASE_URL + "/v1.1/game/{0}/feed/live"
    Dim API_TEAMS_URL As String = API_BASE_URL + "/v1/teams?sportId=1&activeStatus=ACTIVE"
    Dim API_SCHEDULE_URL As String = API_BASE_URL + "/v1/schedule?sportId=1&date={0}"

    Dim apiClient As New System.Net.WebClient()

    Function getData(url)
        Dim result As String = apiClient.DownloadString(url)
        Dim json As JObject = JObject.Parse(result)

        ' https://stackoverflow.com/questions/21676708

        Return json
    End Function

    Function returnLiveFeedData(gamePk)
        Dim url As String = String.Format(API_LIVEFEED_URL, gamePk)
        Return getData(url)
    End Function

    Function returnScheduleData(gameDate)
        Dim url As String = String.Format(API_SCHEDULE_URL, gameDate)
        Return getData(url)
    End Function

    Function returnTeamsData()
        Return getData(API_TEAMS_URL)
    End Function
End Class
