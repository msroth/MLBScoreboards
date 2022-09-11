
Imports Newtonsoft.Json.Linq

Public Class Standings
    Private mAPI As MLB_API = New MLB_API()
    Private mYear As String
    Private mLeagueNames As New Dictionary(Of String, String) From {{"200", "American League West"},
                                                                  {"201", "American League East"},
                                                                  {"202", "American League West"},
                                                                  {"203", "National League Central"},
                                                                  {"204", "National League East"},
                                                                  {"205", "National League Central"}}
 
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
        Dim data As JObject = Me.mAPI.ReturnSeasonStandings(Year)

        Dim dtDivisionStandings As DataTable
        For i = 0 To data.SelectToken("records").Count - 1

            ' create dt for this division
            dtDivisionStandings = initDivStandingDataTable()

            ' get divison data
            Dim DivisionData As JObject = data.SelectToken("records").Item(i)
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

End Class