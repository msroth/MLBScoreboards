Imports System.IO
Imports System.Text
Imports Newtonsoft.Json.Linq

Public Class Team

    Private mId As Integer
    Private mAbbr As String
    Private mShortName As String
    Private mFullName As String
    Private mLineup As DataTable = New DataTable("LineUp")
    Private mAPI As MLB_API = New MLB_API()
    Private mSBData As ScoreboardData = New ScoreboardData()


    Public ReadOnly Property Id() As Integer
        Get
            Return Me.mId
        End Get
    End Property

    Public ReadOnly Property Abbr() As String
        Get
            Return Me.mAbbr
        End Get
    End Property

    Public ReadOnly Property ShortName() As String
        Get
            Return Me.mShortName
        End Get
    End Property

    Public ReadOnly Property FullName() As String
        Get
            Return Me.mFullName
        End Get
    End Property

    Public ReadOnly Property Lineup() As DataTable
        Get
            Return Me.mLineup
        End Get
    End Property



    Public Sub New(TeamId As Integer)
        Me.mId = TeamId

        ' needs to load all other data from API and JSON data
        Dim Data As JObject = mAPI.returnAllTeamsData()
        Dim TeamsData As JArray = Data.SelectToken("teams")

        For Each Team As JObject In TeamsData
            Dim Id As String = Team.SelectToken("id").ToString()
            If Me.Id() = Convert.ToInt32(Id) Then
                Me.mFullName = Team.SelectToken("name")
                Me.mShortName = Team.SelectToken("teamName")
                Me.mAbbr = Team.SelectToken("abbreviation")

                File.WriteAllText($"c:\\temp\\{mAbbr}-teamdata.json", TeamsData.ToString())

                Exit For
            End If
        Next

    End Sub

    Public Sub LoadLineupData(gamePk As Integer)
        If (gamePk = 0) Then
            Return
        End If

        Dim Data As JObject = mAPI.returnLiveFeedData(gamePk)
        Dim LiveData As JObject = Data.SelectToken("liveData")
        Dim LineData As JObject = LiveData.SelectToken("linescore")
        Dim BoxData As JObject = LiveData.SelectToken("boxscore")



        ' determine if team is away or home
        Dim AwayOrHome As String

        ' test team ids
        Dim awayId As String = BoxData.SelectToken("teams.away.team.id")
        If Convert.ToInt32(awayId) = Me.Id() Then
            AwayOrHome = "away"
        Else
            AwayOrHome = "home"
        End If

        ' clear roster table before update
        'mLineup.Clear()
        mLineup.Reset()

        ' setup columns in datatable
        'If Not mLineup.Columns().Contains("Id") Then
        mLineup.Columns.Add("Id")
        'End If

        'If Not mLineup.Columns().Contains("Num") Then
        mLineup.Columns.Add("Num")
        'End If

        'If Not mLineup.Columns().Contains("Name") Then
        mLineup.Columns.Add("Name")
        'End If

        'If Not mLineup.Columns().Contains("Position") Then
        mLineup.Columns.Add("Position")
        'End If


        ' get active batters and pitchers
        Dim PlayerIds As List(Of String) = New List(Of String)
        For Each Id As String In BoxData.SelectToken($"teams.{AwayOrHome}.batters")
            Dim row As DataRow = mLineup.NewRow()
            row(0) = Id

            For Each Player As JProperty In BoxData.SelectToken($"teams.{AwayOrHome}.players")
                If Player.Value.Item("person").Item("id").ToString().Equals(Id) Then
                    row(1) = Player.Value.Item("jerseyNumber")
                    row(2) = Player.Value.Item("person").Item("fullName")
                    row(3) = Player.Value.Item("position").Item("name")

                    mLineup.Rows.Add(row)
                    Exit For
                End If
            Next
        Next
    End Sub

    Public Function ToString() As String
        Dim sb As StringBuilder = New StringBuilder()
        sb.Append($"Team Id: {Me.Id()}")
        sb.Append(vbCr)
        sb.Append($"Team Full Name: {Me.FullName()}")
        sb.Append(vbCr)
        sb.Append($"Team Short Name: {Me.ShortName()}")
        sb.Append(vbCr)
        sb.Append($"Team Abbr: {Me.Abbr()}")
        sb.Append(vbCr)

        ' flatten line up
        If mLineup.Rows.Count > 0 Then
            For Each row As DataRow In mLineup.Rows
                sb.Append(row(0).ToString())
                sb.Append(vbTab)
                sb.Append(row(1).ToString())
                sb.Append(vbTab)
                sb.Append(row(2).ToString())
                sb.Append(vbTab)
                sb.Append(row(3).ToString())
                sb.Append(vbCr)
            Next
        End If

        Return sb.ToString()
    End Function



End Class
