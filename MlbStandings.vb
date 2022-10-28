﻿
Imports System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder
Imports Newtonsoft.Json.Linq

Public Class MlbStandings
    Private mAPI As MlbApi = New MlbApi()
    Private mYear As String
    Private mData As JObject

    Property Year As String
        Set(Year As String)
            Me.mYear = Year
        End Set
        Get
            Return Me.mYear
        End Get
    End Property


    Private Sub Standings_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' get standings data
        Me.mData = Me.mAPI.ReturnSeasonStandings(Year)

        LoadLeagueStandings()
        LoadPostSeasonStandings()
        LoadWS()
    End Sub

    Private Sub LoadLeagueStandings()

        Dim dtDivisionStandings As DataTable
        For i = 0 To Me.mData.SelectToken("records").Count - 1

            ' create dt for this division
            dtDivisionStandings = initDivStandingDataTable()

            ' get divison data
            Dim DivisionData As JObject = Me.mData.SelectToken("records").Item(i)
            Dim DivisionId As String = DivisionData.SelectToken("division.id")

            ' get records for each team in division
            For j = 0 To DivisionData.SelectToken("teamRecords").Count - 1
                Dim row As DataRow = dtDivisionStandings.NewRow()
                Dim TeamRecord As JObject = DivisionData.SelectToken("teamRecords").Item(j)
                row.Item("Rank") = TeamRecord.SelectToken("divisionRank")
                row.Item("Team") = TeamRecord.SelectToken("team.name")
                row.Item("W") = TeamRecord.SelectToken("wins")
                row.Item("L") = TeamRecord.SelectToken("losses")
                row.Item("W%") = TeamRecord.SelectToken("winningPercentage")
                row.Item("GB") = TeamRecord.SelectToken("divisionGamesBack")
                row.Item("E#") = TeamRecord.SelectToken("eliminationNumber")
                row.Item("WC_Rank") = TeamRecord.SelectToken("wildCardRank")
                row.Item("WC_GB") = TeamRecord.SelectToken("wildCardGamesBack")
                row.Item("WC_E#") = TeamRecord.SelectToken("wildCardEliminationNumber")

                ' add team record to division standings table
                dtDivisionStandings.Rows.Add(row)
            Next

            If DivisionId = "201" Then
                ' AL East
                Me.dgvALEast.DataSource = dtDivisionStandings
                Me.dgvALEast.ClearSelection()
            ElseIf DivisionId = "202" Then
                ' AL Central
                Me.dgvALCentral.DataSource = dtDivisionStandings
                Me.dgvALCentral.ClearSelection()
            ElseIf DivisionId = "200" Then
                ' AL West
                Me.dgvALWest.DataSource = dtDivisionStandings
                Me.dgvALWest.ClearSelection()
            ElseIf DivisionId = "204" Then
                ' NL East
                Me.dgvNLEast.DataSource = dtDivisionStandings
                Me.dgvNLEast.ClearSelection()
            ElseIf DivisionId = "205" Then
                ' NL Central
                Me.dgvNLCentral.DataSource = dtDivisionStandings
                Me.dgvNLCentral.ClearSelection()
            ElseIf DivisionId = "203" Then
                ' NL West
                Me.dgvNLWest.DataSource = dtDivisionStandings
                Me.dgvNLWest.ClearSelection()
            End If
        Next

        ' format data grids
        dgvALEast.ColumnHeadersDefaultCellStyle.Font = New Font(dgvALEast.DefaultFont, FontStyle.Bold)
        dgvALCentral.ColumnHeadersDefaultCellStyle.Font = New Font(dgvALCentral.DefaultFont, FontStyle.Bold)
        dgvALWest.ColumnHeadersDefaultCellStyle.Font = New Font(dgvALWest.DefaultFont, FontStyle.Bold)
        dgvNLEast.ColumnHeadersDefaultCellStyle.Font = New Font(dgvNLEast.DefaultFont, FontStyle.Bold)
        dgvNLCentral.ColumnHeadersDefaultCellStyle.Font = New Font(dgvNLCentral.DefaultFont, FontStyle.Bold)
        dgvNLWest.ColumnHeadersDefaultCellStyle.Font = New Font(dgvNLWest.DefaultFont, FontStyle.Bold)

    End Sub

    Function initDivStandingDataTable() As DataTable

        Dim dt As New DataTable()
        Dim col As New DataColumn()
        col.ColumnName = "Rank"
        dt.Columns.Add(col)

        col = New DataColumn()
        col.ColumnName = "Team"
        dt.Columns.Add(col)

        col = New DataColumn()
        col.ColumnName = "W"
        dt.Columns.Add(col)

        col = New DataColumn()
        col.ColumnName = "L"
        dt.Columns.Add(col)

        col = New DataColumn()
        col.ColumnName = "W%"
        dt.Columns.Add(col)

        col = New DataColumn()
        col.ColumnName = "GB"
        dt.Columns.Add(col)

        col = New DataColumn()
        col.ColumnName = "E#"
        dt.Columns.Add(col)

        col = New DataColumn()
        col.ColumnName = "WC_Rank"
        dt.Columns.Add(col)

        col = New DataColumn()
        col.ColumnName = "WC_GB"
        dt.Columns.Add(col)

        col = New DataColumn()
        col.ColumnName = "WC_E#"
        dt.Columns.Add(col)

        Return dt

    End Function

    Function initPostSeasonStandingDataTable() As DataTable

        Dim dt As New DataTable()
        Dim col As New DataColumn()
        col.ColumnName = "Game"
        dt.Columns.Add(col)

        col = New DataColumn()
        col.ColumnName = "Away"
        dt.Columns.Add(col)

        col = New DataColumn()
        col.ColumnName = "Score"
        dt.Columns.Add(col)

        col = New DataColumn()
        col.ColumnName = "Home"
        dt.Columns.Add(col)

        col = New DataColumn()
        col.ColumnName = "Winner"
        dt.Columns.Add(col)

        col = New DataColumn()
        col.ColumnName = "Venue"
        dt.Columns.Add(col)

        Return dt
    End Function


    Private Sub LoadPostSeasonStandings()

        Dim alwc As DataTable = initPostSeasonStandingDataTable()
        Dim nlwc As DataTable = initPostSeasonStandingDataTable()
        Dim alds As DataTable = initPostSeasonStandingDataTable()
        Dim nlds As DataTable = initPostSeasonStandingDataTable()
        Dim alcs As DataTable = initPostSeasonStandingDataTable()
        Dim nlcs As DataTable = initPostSeasonStandingDataTable()


        Dim PostSeasonData As JObject = mAPI.ReturnPostSeasonStandings(mYear)
        Dim totalItems As String = PostSeasonData.SelectToken("totalItems")

        If Convert.ToInt32(totalItems) = 0 Then
            Me.TabControl1.TabPages.Remove(TabPage2)
            Me.TabControl1.TabPages.Remove(TabPage3)
            Return
        End If

        Dim seriesData As JArray = PostSeasonData.SelectToken("series")
        For i As Integer = 0 To seriesData.Count - 1
            Dim series As JObject = seriesData.Item(i)
            Dim seriesId As String = series.SelectToken("series.id")
            Dim row As DataRow
            Dim games As JArray

            ' AL Wild Card Series
            If seriesId = "F_1" Or seriesId = "F_2" Then
                games = series.SelectToken("games")
                dgvALWC.DataSource = ProcessPostSeasonGameData(games, alwc)
                dgvALWC.ClearSelection()
                ' NL Wild Card 3-6
            ElseIf seriesId = "F_3" Or seriesId = "F_4" Then
                games = series.SelectToken("games")
                dgvNLWC.DataSource = ProcessPostSeasonGameData(games, nlwc)
                dgvNLWC.ClearSelection()
                ' AL Division Series
            ElseIf seriesId = "D_1" Or seriesId = "D_2" Then
                games = series.SelectToken("games")
                dgvALDS.DataSource = ProcessPostSeasonGameData(games, alds)
                dgvALDS.ClearSelection()
                ' NL Division Series
            ElseIf seriesId = "D_3" Or seriesId = "D_4" Then
                games = series.SelectToken("games")
                dgvNLDS.DataSource = ProcessPostSeasonGameData(games, nlds)
                dgvNLDS.ClearSelection()
                'AL Conference Series
            ElseIf seriesId = "L_1" Then
                games = series.SelectToken("games")
                dgvALCS.DataSource = ProcessPostSeasonGameData(games, alcs)
                dgvALCS.ClearSelection()
                'NL Conferense Series
            ElseIf seriesId = "L_2" Or seriesId = "D_4" Then
                games = series.SelectToken("games")
                dgvNLCS.DataSource = ProcessPostSeasonGameData(games, nlcs)
                dgvNLCS.ClearSelection()
            End If

        Next

        ' format data grids
        dgvALWC.ColumnHeadersDefaultCellStyle.Font = New Font(dgvALWC.DefaultFont, FontStyle.Bold)
        dgvNLWC.ColumnHeadersDefaultCellStyle.Font = New Font(dgvNLWC.DefaultFont, FontStyle.Bold)
        dgvALDS.ColumnHeadersDefaultCellStyle.Font = New Font(dgvALDS.DefaultFont, FontStyle.Bold)
        dgvNLDS.ColumnHeadersDefaultCellStyle.Font = New Font(dgvNLDS.DefaultFont, FontStyle.Bold)
        dgvALCS.ColumnHeadersDefaultCellStyle.Font = New Font(dgvALCS.DefaultFont, FontStyle.Bold)
        dgvNLCS.ColumnHeadersDefaultCellStyle.Font = New Font(dgvNLCS.DefaultFont, FontStyle.Bold)

    End Sub

    Function ProcessPostSeasonGameData(games As JArray, dt As DataTable) As DataTable

        For j As Integer = 0 To games.Count - 1
            Dim game As JObject = games.Item(j)
            Dim gameState As String = game.SelectToken("status.detailedState")

            If gameState.ToUpper() = "FINAL" Or gameState.ToUpper() = "SCHEDULED" Then
                Dim row As DataRow = dt.NewRow()
                row("Game") = game.SelectToken("description")
                row("Home") = game.SelectToken("teams.home.team.name")
                row("Away") = game.SelectToken("teams.away.team.name")

                If gameState.ToUpper() = "FINAL" Then
                    row("Score") = game.SelectToken("teams.away.score").ToString() + " - " + game.SelectToken("teams.home.score").ToString()
                    If game.SelectToken("teams.away.isWinner").ToString().ToUpper() = "TRUE" Then
                        row("Winner") = game.SelectToken("teams.away.team.name")
                    Else
                        row("Winner") = game.SelectToken("teams.home.team.name")
                    End If
                Else
                    row("Score") = gameState
                End If

                row("Venue") = game.SelectToken("venue.name")
                dt.Rows.Add(row)
            End If

        Next
        Return dt
    End Function

    Private Sub LoadWS()

        Dim ws As DataTable = initPostSeasonStandingDataTable()
        Dim PostSeasonData As JObject = mAPI.ReturnPostSeasonStandings(mYear)
        Dim seriesData As JArray = PostSeasonData.SelectToken("series")
        For i As Integer = 0 To seriesData.Count - 1
            Dim series As JObject = seriesData.Item(i)
            Dim seriesId As String = series.SelectToken("series.id")
            Dim games As JArray

            ' World Series
            If seriesId = "W_1" Then
                games = series.SelectToken("games")
                dgvWS.DataSource = ProcessPostSeasonGameData(games, ws)
                dgvWS.ClearSelection()
            End If

        Next

        dgvWS.ColumnHeadersDefaultCellStyle.Font = New Font(dgvWS.DefaultFont, FontStyle.Bold)
    End Sub

End Class

'<SDG><
