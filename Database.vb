
Imports System.Data.SQLite
Imports Newtonsoft.Json.Linq

Public Class Database

    Dim conn As SQLiteConnection

    Public Sub New()

        ' create connections
        Me.conn = New SQLiteConnection("Data Source=:memory:;Version=3;")
        ' Me.conn = New SQLiteConnection("Data Source=scoreboard.db;Version=3;")
        Me.conn.Open()

        ' create tables
        Dim table_team As String = "CREATE TABLE IF NOT EXISTS teams(id INTEGER, abbrev TEXT, name TEXT, full_name TEXT);"
        Me.runNonQuery(table_team)

        Dim table_current_game As String = "CREATE TABLE IF NOT EXISTS current_game(gamePk INTEGER, home_abbrev TEXT, away_abbrev TEXT, home_name TEXT, away_name TEXT, game_date TEXT, game_time TEXT);"
        Me.runNonQuery(table_current_game)

        Dim table_status As String = "CREATE TABLE IF NOT EXISTS status(current_inning INTEGER, inning_half TEXT, game_status TEXT);"
        Me.runNonQuery(table_status)

        Dim table_players As String = "CREATE TABLE IF NOT EXISTS players(id INTEGER, team_abbr TEXT, full_name TEXT, last_name TEXT, position TEXT, jersey_num TEXT);"
        Me.runNonQuery(table_players)

    End Sub

    Public Function runQuery(sql) As DataTable

        Dim cmd As SQLiteCommand = New SQLiteCommand(sql, Me.conn)
        Dim da As New SQLiteDataAdapter()
        da.SelectCommand = cmd
        Dim dt As New DataTable()
        da.Fill(dt)
        Return dt

    End Function

    Public Sub runNonQuery(sql)

        Dim cmd As SQLiteCommand = Me.conn.CreateCommand()
        cmd.CommandText = sql
        cmd.ExecuteNonQuery()

    End Sub

    Public Sub loadCurrentGameDataIntoDB(liveData, boxData, gameDate, gameTime, gamePk)

        ' TODO - move to Database becuase it uses SQL

        ' get team ids
        Dim awayId As String = boxData.SelectToken("teams.away.team.id")
        Dim homeId As String = boxData.SelectToken("teams.home.team.id")

        ' drop any data currently in table
        Dim sql As String = "DELETE FROM current_game"
        Me.runNonQuery(sql)

        ' get away team data
        sql = String.Format("SELECT * from teams where id='{0}';", awayId)
        Dim awayData As DataTable = Me.runQuery(sql)

        ' get home team data
        sql = String.Format("SELECT * from teams where id='{0}';", homeId)
        Dim homeData As DataTable = Me.runQuery(sql)

        ' insert data into database
        sql = String.Format("INSERT INTO current_game (gamePk, home_abbrev, away_abbrev, home_name, away_name, game_date, game_time) VALUES ({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');",
                            gamePk, homeData.Rows(0).Field(Of String)(1), awayData.Rows(0).Field(Of String)(1), homeData.Rows(0).Field(Of String)(3),
                            awayData.Rows(0).Field(Of String)(3), gameDate, gameTime)
        Me.runNonQuery(sql)

    End Sub

    Public Sub loadTeamsDataIntoDB(data As JObject)

        ' get team data from JSON
        Dim teams As JArray = data.SelectToken("teams")

        For Each team As JObject In teams
            Dim teamId As String = team.SelectToken("id").ToString()
            Dim teamFullName As String = team.SelectToken("name")
            Dim teamName As String = team.SelectToken("teamName")
            Dim teamAbbrev As String = team.SelectToken("abbreviation")

            ' create SQL insert string
            Dim sql As String = String.Format("INSERT INTO teams (id, abbrev, name, full_name) VALUES ({0}, '{1}', '{2}', '{3}');", teamId, teamAbbrev, teamName, teamFullName)

            ' insert into database
            Me.runNonQuery(sql)
        Next
    End Sub

    Public Function returnAllTeamNames() As DataTable
        Dim sql As String = "SELECT * FROM teams ORDER BY full_name"
        Dim dt As DataTable = Me.runQuery(sql)
        Return dt
    End Function

    Public Function returnTeamAbbr(team As String) As String
        Dim sql As String = ""
        If team.Equals("home") Then
            sql = "SELECT home_abbrev FROM current_game;"
        Else
            sql = "SELECT away_abbrev FROM current_game;"
        End If
        Dim dt As DataTable = Me.runQuery(sql)
        Dim abbr As String = dt(0)(0).ToString
        Return abbr
    End Function

    Public Sub insertPlayerDataIntoDB(playerData As JObject, team As String)

        Dim teamAbbr = returnTeamAbbr(team)

        For Each player In playerData

            Dim id As String = player.Value.Item("person").Item("id")
            Dim fullName As String = player.Value.Item("person").Item("fullName")
            fullName = fullName.Replace("'", "")
            Dim lastName As String = fullName.Split(" ")(1)
            Dim jerseyNum As String = player.Value.Item("jeresyNumber")
            Dim position = player.Value.Item("position").Item("name")

            Dim sql As String = String.Format("INSERT INTO players (id, team_abbr, full_name, last_name, position, jersey_num) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');", id, teamAbbr, fullName, lastName, position, jerseyNum)
            Me.runNonQuery(Sql)

        Next

    End Sub

    Public Function returnTeamRoster(team As String) As DataTable
        Dim sql As String = ""
        Dim teamAbbr = ""
        If team.Equals("home") Then
            teamAbbr = returnTeamAbbr("home")
            sql = String.Format("SELECT jersey_num, last_name, position FROM players WHERE team_abbr = '{0}' ORDER BY last_name;", teamAbbr)
        Else
            teamAbbr = returnTeamAbbr("away")
            sql = String.Format("SELECT jersey_num, last_name, position FROM players WHERE team_abbr = '{0}' ORDER BY last_name;", teamAbbr)
        End If
        Dim dt As DataTable = Me.runQuery(sql)
        Return dt
    End Function

End Class
