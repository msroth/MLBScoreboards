

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

    Public Sub loadCurrentGameDataIntoDB(liveData, boxData, gameDate, gamePk)

        ' TODO - move to Database becuase it uses SQL

        ' get team ids
        Dim awayId As String = boxData.SelectToken("teams.away.team.id")
        Dim homeId As String = boxData.SelectToken("teams.home.team.id")

        ' drop any data currently in table
        Dim sql As String = "DELETE FROM current_game"
        Me.DB.runNonQuery(sql)

        ' get away team data
        sql = String.Format("SELECT * from teams where id='{0}';", awayId)
        Dim awayData As DataTable = Me.DB.runQuery(sql)

        ' get home team data
        sql = String.Format("SELECT * from teams where id='{0}';", homeId)
        Dim homeData As DataTable = Me.DB.runQuery(sql)

        ' insert data into database
        sql = String.Format("INSERT INTO current_game (gamePk, home_abbrev, away_abbrev, home_name, away_name, game_date) VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}');",
                            gamePk, homeData.Rows(0).Field(Of String)(1), awayData.Rows(0).Field(Of String)(1), homeData.Rows(0).Field(Of String)(3),
                            awayData.Rows(0).Field(Of String)(3), gameDate)
        Me.DB.runNonQuery(sql)

    End Sub

    Public Sub loadTeamsDataintoDB()

        ' TODO - move to Database becuase it uses SQL

        ' get team data from API
        Dim data As JObject = Me.API.returnTeamsData()
        Dim teams As JArray = data.SelectToken("teams")

        For Each team As JObject In teams
            Dim teamId As String = team.SelectToken("id").ToString()
            Dim teamFullName As String = team.SelectToken("name")
            Dim teamName As String = team.SelectToken("teamName")
            Dim teamAbbrev As String = team.SelectToken("abbreviation")

            ' create SQL insert string
            Dim sql As String = String.Format("INSERT INTO teams (id, abbrev, name, full_name) VALUES ({0}, '{1}', '{2}', '{3}');", teamId, teamAbbrev, teamName, teamFullName)

            ' insert into database
            Me.DB.runNonQuery(sql)

        Next

    End Sub

    Public Function returnAllTeamNames() As DataTable

        ' TODO - move to Database becuase it uses SQL

        Dim sql As String = "SELECT * FROM teams ORDER BY full_name"
        Dim dt As DataTable = Me.DB.runQuery(sql)
        Return dt
    End Function

    Public Function findGamePk(teamId, gameDate) As Integer
        Dim gamePk As Integer = 0
        Dim schedule As JObject = Me.API.returnScheduleData(gameDate)
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
        Me.liveData = Me.API.returnLiveFeedData(gamePk)

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
        Return boxdata
    End Function

    Public Function getCurrentGameData() As DataTable
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
        Dim allPlayData As JArray = Me.liveData.SelectToken("liveData.plays.allPlays")
        Dim playData = allPlayData(playIdx)
        Return playData
    End Function

    Public Function getLastPlayDescription() As String
        Dim lastPlayData As JObject = Me.getLastPlayData()
        Dim desc As String = lastPlayData.SelectToken("result.description")
        Return desc
    End Function

    Public Function getGameStatus() As String
        Dim gameData As JObject = Me.getLiveData()
        Dim gameStatus As String = gameData.SelectToken("gameData.status.detailedState")
        Return gameStatus
    End Function

    Public Function getCurrentInningHalf() As String
        Dim gameData As JObject = Me.getLiveData()
        Dim inningHalf As String = gameData.SelectToken("liveData.linescore.inningHalf")
        Return inningHalf
    End Function

    Public Function getCurrentInningNumber() As String
        Dim gameData As JObject = Me.getLiveData()
        Dim inningHalf As String = gameData.SelectToken("liveData.linescore.currentInning")
        Return inningHalf
    End Function




End Class
