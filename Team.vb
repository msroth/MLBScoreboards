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
    Private mLineup As List(Of Player) = New List(Of Player)
    Private mRoster As List(Of Player) = New List(Of Player)
    Private mBattingOrder As List(Of Player) = New List(Of Player)
    Private mAPI As MLB_API = New MLB_API()

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

    Public Property Wins() As String
        Set(wins As String)
            Me.mWins = wins
        End Set
        Get
            Return Me.mWins
        End Get
    End Property

    Public Property Loses() As String
        Set(loses As String)
            Me.mLoses = loses
        End Set
        Get
            Return Me.mLoses
        End Get
    End Property

    Public ReadOnly Property Lineup() As List(Of Player)
        Get
            Return Me.mLineup
        End Get
    End Property

    Public ReadOnly Property Roster() As List(Of Player)
        Get
            Return Me.mRoster
        End Get
    End Property

    Public ReadOnly Property BattingOrder() As List(Of Player)
        Get
            Return Me.mBattingOrder
        End Get
    End Property

    Public Sub New(TeamId As Integer)
        Me.mId = TeamId

        Try
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
        Catch ex As Exception
            Trace.WriteLine($"ERROR: New Team - {ex}")
        End Try
    End Sub

    Public Function GetPlayer(Id As String) As Player
        For Each player As Player In mRoster
            If Convert.ToInt32(player.Id()) = Convert.ToInt32(Id) Then
                Return player
            End If
        Next
        Return Nothing
    End Function

    Public Sub LoadPlayerData(ThisGame As Game)
        If ThisGame Is Nothing Or ThisGame.GamePk = 0 Then
            Return
        End If

        Try
            ' determine if team is away or home
            Dim AwayOrHome As String
            If Me.Id = ThisGame.AwayTeam.Id Then
                AwayOrHome = "away"
            Else
                AwayOrHome = "home"
            End If

            ' clear lineup and roster lists before update
            mLineup.Clear()
            mRoster.Clear()
            mBattingOrder.Clear()

            ' use boxscore data to create Player objects
            For Each Player As JProperty In ThisGame.BoxScoreData().SelectToken($"teams.{AwayOrHome}.players")
                Dim pId As String = Player.Value.Item("person").Item("id")
                Dim pNum As String = Player.Value.Item("jerseyNumber")
                Dim pName As String = Player.Value.Item("person").Item("fullName")
                pName = pName.Substring(pName.IndexOf(" ") + 1) + ", " + pName.Substring(0, 1)
                Dim pPosition As String = Player.Value.Item("position").Item("abbreviation")
                Dim aPlayer As Player = New Player(pId, pNum, pName, pPosition)
                'Dim aPlayer As Player = New Player(pId)
                mRoster.Add(aPlayer)

                ' get active batters and pitchers for game i.e. the line up
                For Each Id As String In ThisGame.BoxScoreData().SelectToken($"teams.{AwayOrHome}.batters")
                    ' add player obj to lineup table
                    If Player.Value.Item("person").Item("id").ToString().Equals(Id) Then
                        mLineup.Add(aPlayer)
                    End If
                Next
            Next

            ' load batting order
            Dim BODAta As JArray = ThisGame.BoxScoreData().SelectToken($"teams.{AwayOrHome}.battingOrder")
            Dim i As Integer = 1
            For Each playerId As String In BODAta
                Dim player As Player = GetPlayer(playerId)
                player.BattingPosition = i.ToString()
                mBattingOrder.Append(player)
                i += 1
            Next

        Catch ex As Exception
            Trace.WriteLine($"ERROR: LoadLineupAndRosterData - {ex}")
        End Try
    End Sub

    Public Function ToString() As String
        Dim sb As StringBuilder = New StringBuilder()
        sb.Append(vbCr)
        sb.Append($"Team Id: {Me.Id()}")
        sb.Append(vbCr)
        sb.Append($"Team Full Name: {Me.FullName()}")
        sb.Append(vbCr)
        sb.Append($"Team Short Name: {Me.ShortName()}")
        sb.Append(vbCr)
        sb.Append($"Team Abbr: {Me.Abbr()}")
        sb.Append(vbCr)
        sb.Append("Players:")
        sb.Append(vbCr)
        If mLineup.Count > 0 Then
            For Each Player In mLineup
                sb.Append(vbTab)
                sb.Append(Player.Id())
                sb.Append(vbTab)
                sb.Append(Player.FullName())
                sb.Append(vbTab)
                sb.Append(Player.Number())
                sb.Append(vbTab)
                sb.Append(Player.ShortPosition())
                sb.Append(vbCr)
            Next
        End If
        Return sb.ToString()
    End Function

    Function GetLinupTable() As DataTable
        Dim dt As DataTable = New DataTable("Lineup")

        Try
            dt.Columns.Add("Id")
            dt.Columns.Add("Num")
            dt.Columns.Add("Name")
            dt.Columns.Add("Position")
            dt.Columns.Add("BattingOrder")

            For Each player As Player In mLineup
                Dim row As DataRow = dt.NewRow()
                row("Id") = player.Id()
                row("Num") = player.Number()
                row("Name") = player.FullName()
                row("Position") = player.ShortPosition()
                row("BattingOrder") = player.BattingPosition()
                dt.Rows.Add(row)
            Next
        Catch ex As Exception
            Trace.WriteLine($"ERROR: GetLineup - {ex}")
        End Try
        ' sort by batting order
        dt = dt.Select("", "BattingOrder").CopyToDataTable()
        Return dt
    End Function

    Function GetRosterTable() As DataTable

        ' TODO - will probably want to have more/different data in roster?

        Dim dt As DataTable = New DataTable("Roster")

        Try
            dt.Columns.Add("Id")
            dt.Columns.Add("Num")
            dt.Columns.Add("Name")
            dt.Columns.Add("Position")

            For Each player As Player In mRoster
                Dim row As DataRow = dt.NewRow()
                row("Id") = player.Id()
                row("Num") = player.Number()
                row("Name") = player.FullName()
                row("Position") = player.ShortPosition()
                dt.Rows.Add(row)
            Next

        Catch ex As Exception
            Trace.WriteLine($"ERROR: GetRoster - {ex}")
        End Try
        Return dt
    End Function
End Class
