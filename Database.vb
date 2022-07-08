
Imports System.Data.SQLite


Public Class Database

    Dim conn As SqliteConnection

    Public Sub New()

        ' create connections
        Me.conn = New SQLiteConnection("Data Source=:memory:;Version=3;")
        ' Me.conn = New SQLiteConnection("Data Source=scoreboard.db;Version=3;")
        Me.conn.Open()

        ' create tables
        Dim table_team As String = "CREATE TABLE IF NOT EXISTS teams(id INTEGER, abbrev TEXT, name TEXT, full_name TEXT);"
        Me.runNonQuery(table_team)

        Dim table_current_game As String = "CREATE TABLE IF NOT EXISTS current_game(gamePk INTEGER, home_abbrev TEXT, away_abbrev TEXT, home_name TEXT, away_name TEXT, game_date TEXT);"
        Me.runNonQuery(table_current_game)

        Dim table_status As String = "CREATE TABLE IF NOT EXISTS status(current_inning INTEGER, inning_half TEXT, game_status TEXT);"
        Me.runNonQuery(table_status)

        ' TODO - create players table to support ROSTER widget

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



End Class
