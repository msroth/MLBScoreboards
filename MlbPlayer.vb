' =========================================================================================================
' (C) 2022 MSRoth
'
' Released under XXX license.
' =========================================================================================================

Imports System.IO
Imports System.Text
Imports Newtonsoft.Json.Linq
Imports NLog

Public Class MlbPlayer
    Dim mId As String
    Dim mNumber As String
    Dim mFullName As String
    Dim mShortName As String
    Dim mPosition As String
    Dim mShortPosition As String
    Dim mAvg As String  ' season
    Dim mOps As String  ' season
    Dim mObp As String  ' season
    Dim mEra As String  ' season

    Private mAPI As MlbApi = New MlbApi()
    Private mProps As SBProperties = New SBProperties()
    Private mProperties As SBProperties = New SBProperties()

    Shared ReadOnly Logger As Logger = LogManager.GetCurrentClassLogger()

    Public ReadOnly Property Id() As String
        Get
            Return Me.mId
        End Get
    End Property

    Public ReadOnly Property Number() As String
        Get
            Return Me.mNumber
        End Get
    End Property

    Public ReadOnly Property FullName() As String
        Get
            Return Me.mFullName
        End Get
    End Property

    Public ReadOnly Property ShortName() As String
        Get
            Return Me.mShortName
        End Get
    End Property

    Public ReadOnly Property ShortPosition() As String
        Get
            Return Me.mShortPosition
        End Get
    End Property

    Public ReadOnly Property FullPosition() As String
        Get
            Return Me.mPosition
        End Get
    End Property

    Public ReadOnly Property Avg() As String
        Get
            Return Me.mAvg
        End Get
    End Property

    Public ReadOnly Property ERA() As String
        Get
            Return Me.mEra
        End Get
    End Property

    Public ReadOnly Property OPS() As String
        Get
            Return Me.mOps
        End Get
    End Property

    Public ReadOnly Property OBP() As String
        Get
            Return Me.mObp
        End Get
    End Property


    Public Function ConvertToFullObject() As MlbPlayer
        ' convert a 'lite' player object to one with full data
        Return New MlbPlayer(Me.mId)
    End Function

    Sub New(PlayerId As String)
        ' Create new Player object by querying API

        Me.mId = PlayerId
        Try
            ' get data
            Dim Data As JObject = mAPI.ReturnPlayerData(PlayerId)
            Dim PlayerData As JProperty = Data.Property("people")
            Dim ThisPlayer As JObject = PlayerData.Value(0)

            Me.mNumber = ThisPlayer.SelectToken("primaryNumber")
            Me.mFullName = ThisPlayer.SelectToken("fullName")
            Me.mShortName = ThisPlayer.SelectToken("lastInitName")
            Me.mShortPosition = ThisPlayer.SelectToken("primaryPosition.abbreviation")
            Me.mPosition = ThisPlayer.SelectToken("primaryPosition.name")

            ' batting stats
            Dim BattingSeasonStatsData As JObject = Me.mAPI.ReturnPlayerStats(Me.mId, "season", "hitting")
            Dim stats As JArray = BattingSeasonStatsData.SelectToken("people[0].stats[0].splits")
            If stats IsNot Nothing Then
                For i = 0 To stats.Count - 1
                    Dim statsdata As JObject = stats.Item(i)
                    Me.mAvg = statsdata.SelectToken("stat.avg")
                    Me.mObp = statsdata.SelectToken("stat.obp")
                    Me.mOps = statsdata.SelectToken("stat.ops")
                Next
            End If

            ' pitching stats
            Dim PitchingSeasonStatsData As JObject = Me.mAPI.ReturnPlayerStats(Me.mId, "season", "pitching")
            stats = PitchingSeasonStatsData.SelectToken("people[0].stats[0].splits")
            If stats IsNot Nothing Then
                For i = 0 To stats.Count - 1
                    Dim statsdata As JObject = stats.Item(i)
                    Me.mEra = statsdata.SelectToken("stat.era")
                Next
            End If

            ' write data file
            If mProps.GetProperty(mProps.mKEEP_DATA_FILES_KEY) = 1 Then
                Dim DataRoot As String = mProps.GetProperty(mProps.mDATA_FILES_PATH_KEY)
                File.WriteAllText($"{DataRoot}\\{Me.Id}-{Me.ShortName}_playerdata.json", ThisPlayer.ToString())
                Logger.Info($"Writing data file: {DataRoot}\\{Me.Id}-{Me.ShortName}_playerdata.json")
            End If

            Logger.Debug($"New Player object created for id={PlayerId}")
        Catch ex As Exception
            Logger.Error(ex)
        End Try
    End Sub

    Sub New(PlayerId As String, Number As String, Name As String, Position As String)
        ' Create new 'lite' Player object with known data
        Me.mId = PlayerId
        Me.mNumber = Number
        Me.mFullName = Name
        Me.mShortPosition = Position
        Logger.Debug($"New Lite Player object created for id={PlayerId}")
    End Sub

    Public Overrides Function ToString() As String
        Dim sb As StringBuilder = New StringBuilder()
        sb.Append(Id)
        sb.Append(vbTab)
        sb.Append(FullName)
        sb.Append(vbTab)
        sb.Append(Number)
        sb.Append(vbTab)
        sb.Append(ShortPosition)
        Return sb.ToString()
    End Function

    Public Function GetCareerBattingStats() As Dictionary(Of String, String)
        Return Me.GetPlayerHittingAndPitchingStats("hitting", "career")
    End Function

    Public Function GetCareerPitchingStats() As Dictionary(Of String, String)
        Return Me.GetPlayerHittingAndPitchingStats("pitching", "career")
    End Function

    Public Function GetSeasonBattingStats() As Dictionary(Of String, String)
        Return Me.GetPlayerHittingAndPitchingStats("hitting", "season")
    End Function

    Public Function GetSeasonPitchingStats() As Dictionary(Of String, String)
        Return Me.GetPlayerHittingAndPitchingStats("pitching", "season")
    End Function

    Private Function GetPlayerHittingAndPitchingStats(group As String, type As String) As Dictionary(Of String, String)
        ' group = hitting, fielding, pitching
        ' type = season, career
        Dim stats As New Dictionary(Of String, String)
        Try
            Dim StatsData As JObject = Me.mAPI.ReturnPlayerStats(Me.Id, type, group)

            ' save data for debugging?
            If mProperties.GetProperty(mProperties.mKEEP_DATA_FILES_KEY, "0") = "1" Then
                Dim DataRoot As String = mProperties.GetProperty(mProperties.mDATA_FILES_PATH_KEY)
                If StatsData IsNot Nothing Then
                    File.WriteAllText($"{DataRoot}\\{Me.Id}-{Me.FullName}_{group}_{type}_stats_data.json", StatsData.ToString())
                    Logger.Info($"Writing data file: {DataRoot}\\{Me.Id}-{Me.FullName}_{group}_{type}_stats_data.json")
                End If
            End If

            If StatsData IsNot Nothing Then
                Dim PlayerStats As JObject = StatsData.SelectToken("people[0].stats[0].splits[0].stat")
                If PlayerStats IsNot Nothing Then
                    For Each prop As JProperty In PlayerStats.Properties
                        stats.Add(prop.Name.ToString(), prop.Value.ToString())
                    Next
                End If
            End If
        Catch ex As Exception
            Logger.Error(ex)
        End Try

        Return stats
    End Function

    Public Function GetSeasonFieldingStats() As Dictionary(Of String, Dictionary(Of String, String))
        Return GetPlayerFieldingStats("fielding", "season")
    End Function

    Public Function GetCareerFieldingStats() As Dictionary(Of String, Dictionary(Of String, String))
        Return GetPlayerFieldingStats("fielding", "career")
    End Function

    Private Function GetPlayerFieldingStats(group As String, type As String) As Dictionary(Of String, Dictionary(Of String, String))
        Dim stats As New Dictionary(Of String, Dictionary(Of String, String))
        ' group = hitting, fielding, pitching
        ' type = season, career
        Try

            Dim StatsData As JObject = Me.mAPI.ReturnPlayerStats(Me.Id, type, group)

            ' save data for debugging?
            If mProperties.GetProperty(mProperties.mKEEP_DATA_FILES_KEY, "0") = "1" Then
                Dim DataRoot As String = mProperties.GetProperty(mProperties.mDATA_FILES_PATH_KEY)
                If StatsData IsNot Nothing Then
                    File.WriteAllText($"{DataRoot}\\{Me.Id}-{Me.FullName}_{group}_{type}_stats_data.json", StatsData.ToString())
                    Logger.Info($" Writing data file: {DataRoot}\\{Me.Id}-{Me.FullName}_{group}_{type}_stats_data.json")
                End If
            End If

            Dim StatsSplits As JArray = StatsData.SelectToken("people[0].stats[0].splits")

            If StatsSplits IsNot Nothing Then
                For i = 0 To StatsSplits.Count - 1
                    Dim SplitsStatData As JObject = StatsSplits.Item(i).SelectToken("stat")

                    If SplitsStatData IsNot Nothing Then
                        ' create dic for player stats for each position played
                        Dim statsDic As Dictionary(Of String, String) = New Dictionary(Of String, String)

                        Dim numTeams As Integer = Convert.ToInt32(SplitsStatData.SelectToken("numTeams"))
                        If numTeams > 1 Then
                            Continue For  ' this is a summary stat
                        End If

                        Dim position As String = SplitsStatData.SelectToken("position.abbreviation")
                        If position = "DH" Then
                            Continue For ' IDK why DH is in fielding data
                        End If

                        For Each prop As JProperty In SplitsStatData.Properties()
                            statsDic.Add(prop.Name.ToString(), prop.Value.ToString())
                        Next

                        stats.Add(position, statsDic)
                    End If

                Next
            End If

        Catch ex As Exception
            Logger.Error(ex)
        End Try

        Return stats

    End Function


End Class

'<SDG><
