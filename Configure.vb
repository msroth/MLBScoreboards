Public Class Configure
    Private mSBData As ScoreboardData = New ScoreboardData()
    Private mProps As Properties = mSBData.getProperties()
    Private mAllTeams As Dictionary(Of String, Team)

    Public Property AllTeams() As Dictionary(Of String, Team)
        Set(teams As Dictionary(Of String, Team))
            Me.mAllTeams = teams
        End Set
        Get
            Return Me.mAllTeams
        End Get
    End Property


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        mProps.Add(mProps.TIMER_KEY, numUpdateTime.Value.ToString())
        mProps.Add(mProps.FAVORITE_TEAM_KEY, cbxTeams.SelectedValue.ToString())
        mProps.Write()
        Close()
    End Sub

    Private Sub Configure_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTeamsComboBox()
        numUpdateTime.Value = Convert.ToInt32(mProps.GetProperty(mProps.TIMER_KEY, "15"))
        cbxTeams.SelectedValue = mProps.GetProperty(mProps.FAVORITE_TEAM_KEY)
        'Dim dt As DataTable = cbxTeams.DataSource
        'For i As Integer = 0 To dt.Rows.Count - 1
        '    If dt.Rows(i)(cbxTeams.ValueMember) = mProps.GetProperty(mProps.FAVORITE_TEAM_KEY) Then
        '        cbxTeams.SelectedIndex = i
        '        Exit For
        '    End If
        'Next
        ' cbxTeams.Text = mProps.GetProperty(mProps.FAVORITE_TEAM_KEY)
    End Sub

    Private Sub LoadTeamsComboBox()
        'Dim dt As DataTable = mSBData.getAllTeamAbbrevs()
        Dim dt As DataTable = New DataTable("Teams")
        dt.Columns.Add("Abbr")
        dt.Columns.Add("Name")
        For Each Team In mAllTeams.Values
            Dim name As String = $"{Team.FullName} ({Team.Abbr})"
            dt.Rows.Add(Team.Abbr, name)
        Next
        dt = dt.Select("", "Name").CopyToDataTable()
        cbxTeams.DataSource = dt
        cbxTeams.DisplayMember = dt.Columns("Name").ColumnName
        cbxTeams.ValueMember = dt.Columns("Abbr").ColumnName
    End Sub
End Class