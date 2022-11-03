' =========================================================================================================
' (C) 2022 MSRoth
'
' Released under XXX license.
' =========================================================================================================

Imports System.IO
Imports System.Numerics
Imports System.Text
Imports Newtonsoft.Json.Linq
Imports NLog

Public Class MlbTeam

    Private mId As Integer
    Private mAbbr As String
    Private mShortName As String
    Private mFullName As String
    Private mWins As String
    Private mLoses As String
    Private mLineup As SortedDictionary(Of Integer, MlbPlayer) = New SortedDictionary(Of Integer, MlbPlayer)
    Private mRoster As List(Of MlbPlayer) = New List(Of MlbPlayer)
    Private mAPI As MlbApi = New MlbApi()
    Private mProps As SBProperties = New SBProperties()

    Shared ReadOnly Logger As Logger = LogManager.GetCurrentClassLogger()

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

    Public ReadOnly Property Lineup() As SortedDictionary(Of Integer, MlbPlayer)
        Get
            Return Me.mLineup
        End Get
    End Property

    Public ReadOnly Property Roster() As List(Of MlbPlayer)
        Get
            Return Me.mRoster
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

                    ' save data for debugging
                    If mProps.GetProperty(mProps.mKEEP_DATA_FILES_KEY) = 1 Then
                        Dim DataRoot = mProps.GetProperty(mProps.mDATA_FILES_PATH_KEY)
                        File.WriteAllText($"{DataRoot}\\{mAbbr}-teamdata.json", Team.ToString())
                        Logger.Info($"Writing data file: {DataRoot}\\{mAbbr}-teamdata.json")
                    End If
                    Exit For
                End If
            Next
            Logger.Debug($"New Team object created for id={TeamId}")
        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Public Function GetPlayer(Id As String) As MlbPlayer
        For Each player As MlbPlayer In mRoster
            If Convert.ToInt32(player.Id()) = Convert.ToInt32(Id) Then
                Return player.ConvertToFullObject()
            End If
        Next
        Return Nothing
    End Function

    Public Sub LoadPlayerData(ThisGame As MlbGame)

        If ThisGame Is Nothing Or ThisGame.GamePk = 0 Then
            Return
        End If

        Me.LoadRoster(ThisGame)
        Me.LoadLineup(ThisGame)

        End Sub

    Private Sub LoadRoster(ThisGame As MlbGame)
        Dim ThisTeam As MlbTeam

        Try
            ' determine if team is away or home
            Dim AwayOrHome As String
            If Me.Id = ThisGame.AwayTeam.Id Then
                AwayOrHome = "away"
                ThisTeam = ThisGame.AwayTeam
            Else
                AwayOrHome = "home"
                ThisTeam = ThisGame.HomeTeam
            End If

            ' clear roster lists before update
            mRoster.Clear()

            ' use boxscore data to create Player objects
            Dim playerData As JObject = ThisGame.BoxScoreData.SelectToken($"teams.{AwayOrHome}.players")

            For Each Player In playerData
                Dim pId As String = Player.Value.Item("person").Item("id")
                Dim pNum As String = Player.Value.Item("jerseyNumber")
                Dim pName As String = Player.Value.Item("person").Item("fullName")
                pName = pName.Substring(pName.IndexOf(" ") + 1) + ", " + pName.Substring(0, 1)
                Dim pPosition As String = Player.Value.Item("position").Item("abbreviation")

                ' load lite player object or refresh takes too long
                Dim aPlayer As MlbPlayer = New MlbPlayer(pId, pNum, pName, pPosition)

                ' add player to roster
                mRoster.Add(aPlayer)
            Next

        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub


    Private Sub LoadLineup(ThisGame As MlbGame)
        Dim ThisTeam As MlbTeam

        Try
            ' determine if team is away or home
            Dim AwayOrHome As String
            If Me.Id = ThisGame.AwayTeam.Id Then
                AwayOrHome = "away"
                ThisTeam = ThisGame.AwayTeam
            Else
                AwayOrHome = "home"
                ThisTeam = ThisGame.HomeTeam
            End If

            ' clear lineup before update
            mLineup.Clear()

            ' load list of players in batting order
            Dim battingOrder As List(Of String) = New List(Of String)
            Dim battingOrderData As JArray = ThisGame.BoxScoreData.SelectToken($"teams.{AwayOrHome}.battingOrder")
            For i As Integer = 0 To battingOrderData.Count - 1
                battingOrder.Add(battingOrderData.Item(i).ToString)
            Next

            ' load list of players in pitching order
            Dim pitchingOrder As List(Of String) = New List(Of String)
            Dim pitchingOrderData As JArray = ThisGame.BoxScoreData.SelectToken($"teams.{AwayOrHome}.pitchers")
            For i As Integer = 0 To pitchingOrderData.Count - 1
                pitchingOrder.Add(pitchingOrderData.Item(i).ToString)
            Next
            Dim currentPitcherId As String
            If pitchingOrder.Count = 0 Then
                currentPitcherId = 0
            Else
                currentPitcherId = pitchingOrder.Item(pitchingOrder.Count - 1)
            End If

            ' load batters - which is everyone in the game
            Dim battersOrder As List(Of String) = New List(Of String)
            Dim battersOrderData As JArray = ThisGame.BoxScoreData.SelectToken($"teams.{AwayOrHome}.batters")
            For i As Integer = 0 To battersOrderData.Count - 1
                battersOrder.Add(battersOrderData.Item(i).ToString)
            Next

            For Each batterId As String In battersOrder
                Dim json As String = $"teams.{AwayOrHome}.players.ID{batterId}"
                Dim pId As String = ThisGame.BoxScoreData.SelectToken($"{json}.person.id")
                Dim pNum As String = ThisGame.BoxScoreData.SelectToken($"{json}.jerseyNumber")
                Dim pName As String = ThisGame.BoxScoreData.SelectToken($"{json}.person.fullName")
                pName = pName.Substring(pName.IndexOf(" ") + 1) + ", " + pName.Substring(0, 1)
                Dim pPosition As String = ThisGame.BoxScoreData.SelectToken($"{json}.position.abbreviation")
                Dim pBattingOrder As String = ThisGame.BoxScoreData.SelectToken($"{json}.battingOrder")

                ' load lite player object of refresh takes too long
                Dim aPlayer As MlbPlayer = New MlbPlayer(pId, pNum, pName, pPosition)

                ' add player to lineup
                Dim pitcherOffset As Integer = 20
                If battingOrder.Contains(pId) Then
                    Dim BattingOrderId As String = pBattingOrder
                    If BattingOrderId IsNot Nothing And BattingOrderId <> "" Then
                        BattingOrderId = BattingOrderId.Substring(0, 1)
                        mLineup.Add(Integer.Parse(BattingOrderId), aPlayer)
                    End If
                ElseIf pId = currentPitcherId Then
                    mLineup.Add(pitcherOffset, aPlayer)
                End If
            Next

        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Public Overrides Function ToString() As String
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
        If mLineup.Count > 0 Then
            sb.Append("Players:")
            sb.Append(vbCr)
            For Each Player In mLineup
                sb.Append(Player.ToString())
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

            For Each key In mLineup.Keys

                Dim player = mLineup.Item(key)
                Dim row As DataRow = dt.NewRow()
                row("Id") = player.Id()
                row("Num") = player.Number()
                row("Name") = player.FullName()
                row("Position") = player.ShortPosition()
                row("BattingOrder") = key
                dt.Rows.Add(row)
            Next
        Catch ex As Exception
            Logger.Error(ex)
        End Try

        Return dt
    End Function

    Function GetRosterTable() As DataTable
        Dim dt As DataTable = New DataTable("Roster")

        Try
            dt.Columns.Add("Id")
            dt.Columns.Add("Num")
            dt.Columns.Add("Name")
            dt.Columns.Add("Position")

            For Each player As MlbPlayer In mRoster
                Dim row As DataRow = dt.NewRow()
                row("Id") = player.Id()
                row("Num") = player.Number()
                row("Name") = player.FullName()
                row("Position") = player.ShortPosition()
                dt.Rows.Add(row)
            Next

        Catch ex As Exception
            Logger.Error(ex)
        End Try

        ' sort table by position
        dt = dt.Select("", "Position").CopyToDataTable()
        Return dt
    End Function

End Class

'<SDG><
