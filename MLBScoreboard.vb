
Imports Newtonsoft.Json.Linq

Public Class MLBScoreboard

    Dim SBData As ScoreboardData = New ScoreboardData()
    Dim gamePk As Integer = 0
    Dim dtInnings As DataTable
    Dim dtRHE As DataTable


    Private Sub QuitToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles QuitToolStripMenuItem1.Click
        Application.Exit()
    End Sub

    Private Sub AboutToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem1.Click
        Dim frmAbout = AboutBox1
        frmAbout.ShowDialog()
    End Sub

    Private Sub setupRHEDataTable()
        Me.dtRHE = New DataTable("RHE")

        Dim columnR As DataColumn = New DataColumn()
        columnR.DataType = System.Type.GetType("System.String")
        columnR.ColumnName = "R"
        Me.dtRHE.Columns.Add(columnR)

        Dim columnH As DataColumn = New DataColumn()
        columnH.DataType = System.Type.GetType("System.String")
        columnH.ColumnName = "H"
        Me.dtRHE.Columns.Add(columnH)

        Dim columnE As DataColumn = New DataColumn()
        columnE.DataType = System.Type.GetType("System.String")
        columnE.ColumnName = "E"
        Me.dtRHE.Columns.Add(columnE)

        Me.dtRHE.Rows.Add()
        Me.dtRHE.Rows.Add()

    End Sub

    Private Sub setupInningsDataTable()
        ' setup Innings datatable
        Me.dtInnings = New DataTable("Innings")

        Dim column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = ""
        Me.dtInnings.Columns.Add(column)

        ' add first 9 innings
        For i = 1 To 9
            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = i
            Me.dtInnings.Columns.Add(column)
        Next

        ' add RHE columns
        'column = New DataColumn
        'column.ColumnName = "R"
        'dtInnings.Columns.Add(column)
        'column = New DataColumn
        'column.ColumnName = "H"
        'dtInnings.Columns.Add(column)
        'column = New DataColumn
        'column.ColumnName = "E"
        'dtInnings.Columns.Add(column)

        ' add rows for Away and Home teams
        Me.dtInnings.Rows.Add()
        Me.dtInnings.Rows.Add()

    End Sub
    Private Sub MLBScoreboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.setupRHEDataTable()
        Me.setupInningsDataTable()

        ' set tables as datasource for grid views
        Me.dgvInnings.DataSource = Me.dtInnings
        Me.dgvRHE.DataSource = Me.dtRHE
        Me.dgvInnings.ClearSelection()
        Me.dgvRHE.ClearSelection()

        ' load teams into database
        SBData.loadTeamsDataintoDB()

        ' load teams from database into CBX control
        Dim dt As DataTable = SBData.returnAllTeamNames()
        With Me.cbxTeam
            .DisplayMember = "full_name"
            .ValueMember = "id"
            .DataSource = dt
        End With
        Me.cbxTeam.SelectedIndex = 0


    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        Dim teamId As String = Me.cbxTeam.SelectedValue.ToString
        Dim teamName As String = Me.cbxTeam.Items(Me.cbxTeam.SelectedIndex)(3).ToString
        Dim gameDate As String = Me.calDatePicker.Value.ToString("MM/dd/yyyy")

        ' TODO - clear the dtInning datatable columns

        ' stop the timer refreshing the current scoreboard
        Timer1.Stop()

        ' get game data
        Me.gamePk = Me.SBData.findGamePk(teamId, gameDate)

        If Me.gamePk = 0 Then
            MsgBox(String.Format("{0} has no scheduled games {1}.", teamName, gameDate))
        Else
            ' run the scoreboard
            Me.runScoreboard()
            Timer1.Interval = 15000  ' 15 sec
            Timer1.Start()
        End If
    End Sub

    Private Sub setGameStatus()
        Dim gameStatus As String = Me.SBData.getLiveData().SelectToken("gameData.status.detailedState")
        Me.lblStatus.Text = gameStatus
    End Sub

    Private Sub setGameTitle()
        Dim result As DataTable = Me.SBData.getCurrentGameData()
        Me.lblGameTitle.Text = String.Format("{0} @ {1} - {2} (Game: {3})", result.Rows(0).Field(Of String)(4),
                                             result.Rows(0).Field(Of String)(3), result.Rows(0).Field(Of String)(5),
                                             Me.gamePk)
    End Sub

    Private Sub updateLastPlay()
        Dim play = Me.SBData.getLastPlayDescription()
        'TODO - format with inning number if not current inning
        Me.txtCommentary.Text = play
    End Sub

    Private Sub runScoreboard()

        ' get game date
        Dim gameDate As String = Me.calDatePicker.Value.ToString("MM/dd/yyyy")

        ' update status bar
        Me.ToolStripStatusLabel1.Text = "Updating data ... "

        ' get live data
        Me.SBData.refreshLiveData(gamePk)

        ' load current game data into database
        Me.SBData.loadCurrentGameDataIntoDB(Me.SBData.getLiveData(), Me.SBData.getBoxScoreData(), gameDate, Me.gamePk)

        ' update status bar
        Me.ToolStripStatusLabel1.Text = "Updated " + Date.Now

        ' update game label
        Me.setGameTitle()

        ' update game status
        Me.setGameStatus()

        'update innings datatable
        Me.updateInnings()

        ' update runs, hits, errors datatable
        Me.updateRHE()

        ' clear inning label
        dtInnings.Columns(0).ColumnName = " "

        If Me.SBData.getGameStatus().ToUpper() = "IN PROGRESS" Then

            ' update inning
            Dim inningLabel As String = SBData.getCurrentInningHalf() + " " + SBData.getCurrentInningNumber()
            dtInnings.Columns(0).ColumnName = inningLabel

            ' update Balls, Strikes, Outs
            Me.updateBSO()

            ' TODO update pitcher-batter matchup


            ' TODO update last play
            Me.updateLastPlay()

            ' TODO update last pitch data

            'update base runners image
            Me.updateBaseRunners()
        Else
            txtCommentary.Enabled = False
            txtPitch.Enabled = False
        End If

    End Sub

    Private Sub updateRHE()

        ' clear all data
        For Each row As DataRow In Me.dtRHE.Rows
            For Each col As DataColumn In Me.dtRHE.Columns
                row(col) = ""
            Next
        Next

        Dim lineScore As JObject = Me.SBData.getLineScoreData()

        ' Get R - H - E for away team
        Me.dtRHE.Rows(0).Item("R") = lineScore.SelectToken("teams.away.runs")
        Me.dtRHE.Rows(0).Item("H") = lineScore.SelectToken("teams.away.hits")
        Me.dtRHE.Rows(0).Item("E") = lineScore.SelectToken("teams.away.errors")

        ' Get R - H - E for home team
        Me.dtRHE.Rows(1).Item("R") = lineScore.SelectToken("teams.home.runs")
        Me.dtRHE.Rows(1).Item("H") = lineScore.SelectToken("teams.home.hits")
        Me.dtRHE.Rows(1).Item("E") = lineScore.SelectToken("teams.home.errors")

    End Sub

    Private Sub updateBSO()
        Dim playData = Me.SBData.getCurrentPlayData()
        Me.lblBalls.Text = String.Format("Balls: {0}", playData.SelectToken("count.balls"))
        Me.lblStrikes.Text = String.Format("Strikes: {0}", playData.SelectToken("count.strikes"))
        Me.lblOuts.Text = String.Format("Outs: {0}", playData.SelectToken("count.outs"))

    End Sub
    Private Sub updateInnings()

        ' clear all data
        For Each row As DataRow In Me.dtInnings.Rows
            For Each col As DataColumn In Me.dtInnings.Columns
                row(col) = ""
            Next
        Next

        ' add team names with records
        Dim awayWins As String = Me.SBData.getLiveData().SelectToken("gameData.teams.away.record.wins")
        Dim awayLoses As String = Me.SBData.getLiveData().SelectToken("gameData.teams.away.record.losses")
        Dim awayAbbrev As String = Me.SBData.getLiveData().SelectToken("gameData.teams.away.abbreviation")
        Dim awayNameAndRecord As String = String.Format("{0} ({1}-{2})", awayAbbrev, awayWins, awayLoses)
        Me.dtInnings.Rows(0).Item(0) = awayNameAndRecord

        Dim homeWins As String = Me.SBData.getLiveData().SelectToken("gameData.teams.home.record.wins")
        Dim homeLoses As String = Me.SBData.getLiveData().SelectToken("gameData.teams.home.record.losses")
        Dim homeAbbrev As String = Me.SBData.getLiveData().SelectToken("gameData.teams.home.abbreviation")
        Dim homeNameAndRecord As String = String.Format("{0} ({1}-{2})", homeAbbrev, homeWins, homeLoses)
        Me.dtInnings.Rows(1).Item(0) = homeNameAndRecord

        ' fill innings
        Dim lineScore As JObject = Me.SBData.getLineScoreData
        Dim innings As JArray = lineScore.SelectToken("innings")

        'if innings > 9, add columns to datatable before proceeding
        ' Test:  Nats 7/4/22
        If (innings.Count > dtInnings.Columns.Count - 1) Then    ' minus one for team name column
            Dim i As Integer = 1
            While innings.Count >= dtInnings.Columns.Count
                Dim dc = New DataColumn
                dc.ColumnName = (9 + i).ToString
                dtInnings.Columns.Add(dc)
                i = i + 1
            End While
        End If

        ' fill inning data
        For Each inning As JObject In innings

            ' away data
            Dim runs As String = inning.SelectToken("away.runs")
            If runs Is Nothing Then
                runs = "  "
            End If
            Me.dtInnings.Rows(0).Item(Integer.Parse(inning.SelectToken("num"))) = runs

            ' Get R - H - E for away team
            'Me.dtInnings.Rows(0).Item("R") = lineScore.SelectToken("teams.away.runs")
            'Me.dtInnings.Rows(0).Item("H") = lineScore.SelectToken("teams.away.hits")
            'Me.dtInnings.Rows(0).Item("E") = lineScore.SelectToken("teams.away.errors")

            ' home data
            runs = inning.SelectToken("home.runs")
            If runs Is Nothing Then
                runs = "  "
            End If
            Me.dtInnings.Rows(1).Item(Integer.Parse(inning.SelectToken("num"))) = runs

            ' Get R - H - E for home team
            'Me.dtInnings.Rows(1).Item("R") = lineScore.SelectToken("teams.home.runs")
            'Me.dtInnings.Rows(1).Item("H") = lineScore.SelectToken("teams.home.hits")
            'Me.dtInnings.Rows(1).Item("E") = lineScore.SelectToken("teams.home.errors")
        Next

    End Sub

    Private Sub updateBaseRunners()
        Dim lineData As JObject = Me.SBData.getLineScoreData()
        Dim offenseData As JObject = lineData.SelectToken("offense")
        Dim first As Boolean = False
        Dim second As Boolean = False
        Dim third As Boolean = False

        If offenseData.ContainsKey("first") Then
            first = True
        End If

        If offenseData.ContainsKey("second") Then
            second = True
        End If

        If offenseData.ContainsKey("third") Then
            third = True
        End If

        If first = False And second = False And third = False Then
            Me.imgDiamond.Image = My.Resources.diamond
        End If

        If first = True And second = False And third = False Then
            Me.imgDiamond.Image = My.Resources.diamond1
        End If

        If first = False And second = True And third = False Then
            Me.imgDiamond.Image = My.Resources.diamond2
        End If

        If first = False And second = False And third = True Then
            Me.imgDiamond.Image = My.Resources.diamond3
        End If

        If first = True And second = True And third = False Then
            Me.imgDiamond.Image = My.Resources.diamond1_2
        End If

        If first = False And second = True And third = True Then
            Me.imgDiamond.Image = My.Resources.diamond2_3
        End If

        If first = True And second = True And third = True Then
            Me.imgDiamond.Image = My.Resources.diamond1_2_3
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.runScoreboard()

    End Sub
End Class
