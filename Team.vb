Imports System.IO
Imports System.Text
Imports Newtonsoft.Json.Linq

Public Class Team

    Private mId As Integer
    Private mAbbr As String
    Private mShortName As String
    Private mFullName As String
    Private mWins As String
    Private mLoses As String
    'Private mLineup As DataTable = New DataTable("LineUp")
    Private mLineup As List(Of Player) = New List(Of Player)
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

    Public ReadOnly Property Wins() As String
        Get
            Return Me.mWins
        End Get
    End Property

    Public ReadOnly Property Loses() As String
        Get
            Return Me.mLoses
        End Get
    End Property

    'Public ReadOnly Property Lineup() As DataTable
    '    Get
    '        Return Me.mLineup
    '    End Get
    'End Property

    Public ReadOnly Property Lineup() As List(Of Player)
        Get
            Return Me.mLineup
        End Get
    End Property

    Public Sub New(TeamId As Integer)
        Me.mId = TeamId

        ' needs to load all other data from API and JSON data
        Dim Data As JObject = mAPI.ReturnAllTeamsData()
        Dim TeamsData As JArray = Data.SelectToken("teams")

        For Each Team As JObject In TeamsData
            Dim Id As String = Team.SelectToken("id").ToString()
            If Me.Id() = Convert.ToInt32(Id) Then
                Me.mFullName = Team.SelectToken("name")
                Me.mShortName = Team.SelectToken("teamName")
                Me.mAbbr = Team.SelectToken("abbreviation")

                File.WriteAllText($"c:\\temp\\{mAbbr}-teamdata.json", Team.ToString())

                Exit For
            End If
        Next

    End Sub

    'Public Function GetPlayerData(Id As String) As DataRow
    '    For Each row As DataRow In mLineup.Rows()
    '        If row("Id") = Id Then
    '            Return row
    '        End If
    '    Next
    '    Return Nothing
    'End Function

    Public Function GetPlayerData(Id As String) As Player
        For Each Player In mLineup
            If Player.Id() = Id Then
                Return Player
            End If
        Next
        Return Nothing
    End Function

    Public Sub LoadLineupData(gamePk As Integer)
        If (gamePk = 0) Then
            Return
        End If

        Dim Data As JObject = mAPI.ReturnLiveFeedData(gamePk)
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
        'mLineup.Reset()
        mLineup.Clear()

        '' setup columns in datatable
        'mLineup.Columns.Add("Id")
        'mLineup.Columns.Add("Num")
        'mLineup.Columns.Add("Name")
        'mLineup.Columns.Add("Position")

        ' get active batters and pitchers
        Dim PlayerIds As List(Of String) = New List(Of String)
        For Each Id As String In BoxData.SelectToken($"teams.{AwayOrHome}.batters")
            mLineup.Add(New Player(Id))

            '    Dim row As DataRow = mLineup.NewRow()
            '    row("Id") = Id

            '    For Each Player As JProperty In BoxData.SelectToken($"teams.{AwayOrHome}.players")
            '        If Player.Value.Item("person").Item("id").ToString().Equals(Id) Then
            '            row("Num") = Player.Value.Item("jerseyNumber")
            '            row("Name") = Player.Value.Item("person").Item("fullName")
            '            row("Position") = Player.Value.Item("position").Item("name")

            '            mLineup.Rows.Add(row)
            '            Exit For
            '        End If
            '    Next
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
        'If mLineup.Rows.Count > 0 Then
        '    For Each row As DataRow In mLineup.Rows
        '        sb.Append(row(0).ToString())
        '        sb.Append(vbTab)
        '        sb.Append(row(1).ToString())
        '        sb.Append(vbTab)
        '        sb.Append(row(2).ToString())
        '        sb.Append(vbTab)
        '        sb.Append(row(3).ToString())
        '        sb.Append(vbCr)
        '    Next
        'End If

        If mLineup.Count > 0 Then
            For Each Player In mLineup
                sb.Append(Player.Id())
                sb.Append(vbCr)
                sb.Append(Player.Name())
                sb.Append(vbCr)
                sb.Append(Player.Number())
                sb.Append(vbCr)
                sb.Append(Player.Position())
                sb.Append(vbCr)
            Next
        End If

        Return sb.ToString()
    End Function

    Function GetLinup() As DataTable
        Dim dt As DataTable = New DataTable("Lineup")
        dt.Columns.Add("Id")
        dt.Columns.Add("Num")
        dt.Columns.Add("Name")
        dt.Columns.Add("Position")

        For Each player As Player In mLineup
            Dim row As DataRow = dt.NewRow()
            row("Id") = player.Id()
            row("Num") = player.Number()
            row("name") = player.Name()
            row("Position") = player.Position()
            dt.Rows.Add(row)
        Next
        Return dt
    End Function

End Class
