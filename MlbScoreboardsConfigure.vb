Imports System.IO
Imports Newtonsoft.Json.Linq
Imports NLog

Public Class MlbScoreboardsConfigure
    Private mMLB_API As MlbApi = New MlbApi()
    Private mProps As MlbScoreboardsProperties = New MlbScoreboardsProperties()
    Private mAllTeams As Dictionary(Of String, MlbTeam)

    Shared ReadOnly Logger As Logger = LogManager.GetCurrentClassLogger()


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
        Try
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
            Logger.Debug($"Wrote: {mProps.mSCOREBOARD_TIMER_KEY} = {mProps.GetProperty(mProps.mSCOREBOARD_TIMER_KEY)}")
            Logger.Debug($"Wrote: {mProps.mGAME_TIMER_KEY} = {mProps.GetProperty(mProps.mGAME_TIMER_KEY)}")
            Logger.Debug($"Wrote: {mProps.mFAVORITE_TEAM_KEY} = {mProps.GetProperty(mProps.mFAVORITE_TEAM_KEY)}")
            Logger.Debug($"Wrote: {mProps.mKEEP_DATA_FILES_KEY} = {mProps.GetProperty(mProps.mKEEP_DATA_FILES_KEY)}")
            Logger.Debug($"Wrote: {mProps.mDATA_FILES_PATH_KEY} = {mProps.GetProperty(mProps.mDATA_FILES_PATH_KEY)}")

        Catch ex As Exception
            Logger.Debug(ex)
        End Try

        Close()
    End Sub

    Private Sub Configure_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Logger.Debug($"Loading {Me.Name}")

        Try
            Me.Cursor = Cursors.WaitCursor

            mAllTeams = Me.LoadTeamsData()
            LoadTeamsComboBox()
            numSBRefreshTime.Value = Convert.ToInt32(mProps.GetProperty(mProps.mSCOREBOARD_TIMER_KEY, "60"))
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

            Logger.Debug($"Read: {mProps.mSCOREBOARD_TIMER_KEY} = {mProps.GetProperty(mProps.mSCOREBOARD_TIMER_KEY)}")
            Logger.Debug($"Read: {mProps.mGAME_TIMER_KEY} = {mProps.GetProperty(mProps.mGAME_TIMER_KEY)}")
            Logger.Debug($"Read: {mProps.mFAVORITE_TEAM_KEY} = {mProps.GetProperty(mProps.mFAVORITE_TEAM_KEY)}")
            Logger.Debug($"Read: {mProps.mKEEP_DATA_FILES_KEY} = {mProps.GetProperty(mProps.mKEEP_DATA_FILES_KEY)}")
            Logger.Debug($"Read: {mProps.mDATA_FILES_PATH_KEY} = {mProps.GetProperty(mProps.mDATA_FILES_PATH_KEY)}")

            Me.Cursor = Cursors.Default

        Catch ex As Exception
            Logger.Debug(ex)
        End Try

    End Sub

    Private Sub LoadTeamsComboBox()
        Try
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
        Catch ex As Exception
            Logger.Debug(ex)
        End Try

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
            Logger.Debug(ex)
        End Try

        Return Teams
    End Function

    Private Function BuildDataPath() As String
        Try
            Dim DataRoot As String = $"{Application.StartupPath()}\data"
            Dim di As DirectoryInfo = New DirectoryInfo(DataRoot)
            If di.Exists = True Then
                Return di.FullName()
            Else
                di.Create()
                Return di.FullName()
            End If
        Catch ex As Exception
            Logger.Debug(ex)
        End Try

    End Function

    Private Sub chxWriteFiles_CheckedChanged(sender As Object, e As EventArgs) Handles chxWriteFiles.CheckedChanged
        If chxWriteFiles.Checked = True Then
            lblData.Visible = True
            lblData.Text = $"Data Path: {Me.BuildDataPath()}"
        Else
            lblData.Visible = False
        End If
    End Sub

    Private Sub chxWriteFiles_Click(sender As Object, e As EventArgs) Handles chxWriteFiles.Click
        If chxWriteFiles.Checked = True Then
            MessageBox.Show("Enabling data file writes can effect performance.", "Warning")
        End If

    End Sub
End Class

'<SDG><
