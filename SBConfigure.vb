Imports System.IO
Imports Newtonsoft.Json.Linq

Public Class SBConfigure
    Private mMLB_API As MlbApi = New MlbApi()
    Private mProps As SBProperties = New SBProperties()
    Private mAllTeams As Dictionary(Of String, MlbTeam)

    Public Property AllTeams() As Dictionary(Of String, MlbTeam)
        Set(teams As Dictionary(Of String, MlbTeam))
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
        mProps.Add(mProps.mSCOREBOARD_TIMER_KEY, numSBRefreshTime.Value.ToString())
        mProps.Add(mProps.mGAME_TIMER_KEY, numGameRefreshTime.Value.ToString())
        mProps.Add(mProps.mFAVORITE_TEAM_KEY, cbxTeams.SelectedValue.ToString())

        If chxWriteFiles.Checked Then
            mProps.Add(mProps.mKEEP_DATA_FILES_KEY, "1")
        Else
            mProps.Add(mProps.mKEEP_DATA_FILES_KEY, "0")
        End If

        If chxWriteFiles.Checked = True Then
            mProps.Add(mProps.mDATA_FILES_PATH_KEY, Me.BuildDataPath())
        End If

        mProps.Write()
        Close()
    End Sub

    Private Sub Configure_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mAllTeams = Me.LoadTeamsData()
        LoadTeamsComboBox()
        numSBRefreshTime.Value = Convert.ToInt32(mProps.GetProperty(mProps.mSCOREBOARD_TIMER_KEY, "30"))
        numGameRefreshTime.Value = Convert.ToInt32(mProps.GetProperty(mProps.mGAME_TIMER_KEY, "20"))
        cbxTeams.SelectedValue = mProps.GetProperty(mProps.mFAVORITE_TEAM_KEY, "WSH")

        If mProps.GetProperty(mProps.mKEEP_DATA_FILES_KEY, "1") Then
            chxWriteFiles.Checked = True
        Else
            chxWriteFiles.Checked = False
        End If

        If chxWriteFiles.Checked = True Then
            lblData.Text = $"Data Path: {mProps.GetProperty(mProps.mDATA_FILES_PATH_KEY, Me.BuildDataPath)}"
            lblData.Visible = True
        Else
            lblData.Visible = False
        End If
    End Sub

    Private Sub LoadTeamsComboBox()
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

    Public Function LoadTeamsData() As Dictionary(Of String, MlbTeam)
        Dim Teams As Dictionary(Of String, MlbTeam) = New Dictionary(Of String, MlbTeam)
        Try
            Dim Data As JObject = mMLB_API.ReturnAllTeamsData()
            Dim TeamsData As JArray = Data.SelectToken("teams")

            For Each Team As JObject In TeamsData
                Dim teamId As String = Team.SelectToken("id").ToString()
                Dim oTeam As MlbTeam = New MlbTeam(Convert.ToInt32(teamId))
                Teams.Add(teamId, oTeam)
                'Trace.WriteLine($"Loading team {oTeam.Id} - {oTeam.Abbr} data")
            Next
        Catch ex As Exception
            Trace.WriteLine($"ERROR: LoadTeamsData - {ex}")
        End Try
        Return Teams
    End Function

    Private Function BuildDataPath() As String
        Dim DataRoot As String = $"{Application.StartupPath()}\data"
        Dim di As DirectoryInfo = New DirectoryInfo(DataRoot)
        If di.Exists = True Then
            Return di.FullName()
        Else
            di.Create()
            Return di.FullName()
        End If

    End Function

    Private Sub chxWriteFiles_CheckedChanged(sender As Object, e As EventArgs) Handles chxWriteFiles.CheckedChanged
        If chxWriteFiles.Checked = True Then
            lblData.Visible = True
            lblData.Text = $"Data Path: {Me.BuildDataPath()}"
        Else
            lblData.Visible = False
        End If
    End Sub

End Class

'<SDG><
