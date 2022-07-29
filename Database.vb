
Imports System.Data.SQLite
Imports Newtonsoft.Json.Linq

Public Class Database

    Dim conn As SQLiteConnection

    Public Sub New()

        ' create connections
        Me.conn = New SQLiteConnection("Data Source=:memory:;Version=3;")
        'Me.conn = New SQLiteConnection("Data Source=scoreboard.db;Version=3;")
        Me.conn.Open()

        ' create tables
        Dim sql As String = "CREATE TABLE IF NOT EXISTS teams(id INTEGER, abbrev TEXT, name TEXT, full_name TEXT);"
        Me.runNonQuery(sql)

        sql = "CREATE TABLE IF NOT EXISTS current_game(gamePk INTEGER, home_abbrev TEXT, away_abbrev TEXT, home_name TEXT, away_name TEXT, game_date TEXT, game_time TEXT);"
        Me.runNonQuery(sql)

        sql = "CREATE TABLE IF NOT EXISTS status(current_inning INTEGER, inning_half TEXT, inning_state TEXT, game_status TEXT);"
        Me.runNonQuery(sql)

        sql = "CREATE TABLE IF NOT EXISTS players(id INTEGER, team_abbrev TEXT, full_name TEXT, last_name TEXT, position TEXT, jersey_num TEXT);"
        Me.runNonQuery(sql)

        sql = "CREATE TABLE IF NOT EXISTS games(id INTEGER, away_abbrev TEXT, away_score TEXT, home_abbrev TEXT, home_score TEXT, status TEXT, inning TEXT, inning_half TEXT);"
        Me.runNonQuery(sql)

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

    'Public Function returnTeamAbbr(team As String) As String
    '    Dim sql As String = ""
    '    If team.Equals("home") Then
    '        sql = "SELECT home_abbrev FROM current_game;"
    '    Else
    '        sql = "SELECT away_abbrev FROM current_game;"
    '    End If
    '    Dim dt As DataTable = Me.runQuery(sql)
    '    Dim abbr As String = dt(0)(0).ToString
    '    Return abbr
    'End Function

    Public Sub insertPlayerDataIntoDB(id As String, teamAbbr As String, fullName As String, position As String, jerseyNumber As String)

        fullName = fullName.Replace("'", "")
        Dim lastName As String = fullName.Split(" ")(1)
        Dim sql As String = String.Format("INSERT INTO players (id, team_abbrev, full_name, last_name, position, jersey_num) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');", id, teamAbbr, fullName, lastName, position, jerseyNumber)
        Me.runNonQuery(sql)
    End Sub

    Public Function returnTeamLineup(teamAbbr As String) As DataTable
        Dim sql As String = String.Format("SELECT id, jersey_num as Num, last_name as Name, position as Position FROM players WHERE team_abbrev = '{0}';", teamAbbr)
        Dim dt As DataTable = Me.runQuery(sql)
        Return dt
    End Function

    Public Sub clearPlayersTable()
        Dim sql As String = "DELETE FROM players;"
        Me.runNonQuery(sql)
    End Sub

    Public Function returnPlayerTeamAbbr(playerId As Integer) As DataTable
        Dim sql As String = String.Format("SELECT team_abbrev FROM players WHERE id = {0}", playerId)
        Dim dt As DataTable = Me.runQuery(sql)
        Return dt
    End Function

    Public Function returnHomeTeamName() As DataTable
        Dim sql As String = "SELECT home_name FROM current_game"
        Dim dt As DataTable = Me.runQuery(sql)
        Return dt
    End Function

    Public Function returnAwayTeamName() As DataTable
        Dim sql As String = "SELECT away_name FROM current_game"
        Dim dt As DataTable = Me.runQuery(sql)
        Return dt
    End Function

    Public Function returnHomeTeamAbbr() As DataTable
        Dim sql As String = "SELECT home_abbrev FROM current_game"
        Dim dt As DataTable = Me.runQuery(sql)
        Return dt
    End Function

    Public Function returnAwayTeamAbbr() As DataTable
        Dim sql As String = "SELECT away_abbrev FROM current_game"
        Dim dt As DataTable = Me.runQuery(sql)
        Return dt
    End Function

    Public Function returnTeamAbbrevFromId(id As String) As DataTable
        Dim sql As String = String.Format("SELECT abbrev FROM teams WHERE id={0};", id)
        Dim dt As DataTable = Me.runQuery(sql)
        Return dt
    End Function

    Sub InsertAllGamesData(gamePk As String, awayName As String, awayScore As String, homeName As String, homeScore As String, status As String, inning As String, inningHalf As String)
        Dim sql As String = String.Format("INSERT INTO games (id, away_abbrev, away_score, home_abbrev, home_score, status, inning, inning_half) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')", gamePk, awayName, awayScore, homeName, homeScore, status, inning, inningHalf)
        Me.runNonQuery(sql)
    End Sub

    Sub ClearAllGamesData()
        Dim sql As String = "DELETE from games"
        Me.runNonQuery(sql)
    End Sub

    Function returnAllGamesData() As DataTable
        Dim sql As String = "SELECT * FROM games;"
        Dim dt As DataTable = Me.runQuery(sql)
        Return dt
    End Function
End Class


