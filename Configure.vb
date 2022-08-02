Public Class Configure
    Private SBData As ScoreboardData = New ScoreboardData()
    Private Props As Properties = SBData.getProperties()

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Props.Add(Props.TIMER_KEY, numUpdateTime.Value.ToString())
        Props.Add(Props.FAVORITE_TEAM_KEY, cbxTeams.SelectedValue.ToString())
        Props.Write()
        Close()
    End Sub

    Private Sub Configure_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadTeamsComboBox()
        numUpdateTime.Value = Convert.ToInt32(Props.GetProperty(Props.TIMER_KEY, "15"))
        cbxTeams.Text = Props.GetProperty(Props.FAVORITE_TEAM_KEY)
    End Sub

    Private Sub LoadTeamsComboBox()
        Dim dt As DataTable = SBData.getAllTeamAbbrevs()
        cbxTeams.DataSource = dt
        cbxTeams.DisplayMember = dt.Columns(0).ColumnName
        cbxTeams.ValueMember = dt.Columns(0).ColumnName
    End Sub
End Class