

Imports System.IO
Imports Newtonsoft.Json.Linq

Public Class MlbPlayer
    Dim mId As String
    Dim mNumber As String
    Dim mFullName As String
    Dim mShortName As String
    Dim mPosition As String
    Dim mShortPosition As String
    Dim mBattingPosition As Integer
    Dim mAvg As String  ' season
    Dim mOps As String  ' season
    Dim mObp As String  ' season
    Dim mEra As String  ' season

    Private mAPI As MlbApi = New MlbApi()
    Private mProps As SBProperties = New SBProperties()


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
            Return Me.mFullName
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


    Public Property BattingPosition() As Integer
        Get
            Return Me.mBattingPosition
        End Get
        Set(number As Integer)
            Me.mBattingPosition = number
        End Set
    End Property

    Public Function ConvertToFullObject() As MlbPlayer
        Return New MlbPlayer(Me.mId)
    End Function

    Sub New(Id As String)
        ' Create new Player object by querying API

        Me.mId = Id
        Dim Data As JObject = mAPI.ReturnPlayerData(Id)
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
        stats = BattingSeasonStatsData.SelectToken("people[0].stats[0].splits")
        If stats IsNot Nothing Then
            For i = 0 To stats.Count - 1
                Dim statsdata As JObject = stats.Item(i)
                Me.mAvg = statsdata.SelectToken("stat.era")
            Next
        End If

        If mProps.GetProperty(mProps.mKEEP_DATA_FILES_KEY) = 1 Then
            Dim DataRoot As String = mProps.GetProperty(mProps.mDATA_FILES_PATH_KEY)
            File.WriteAllText($"{DataRoot}\\{Me.Id}-{Me.ShortName}_playerdata.json", ThisPlayer.ToString())
        End If
    End Sub

    Sub New(Id As String, Number As String, Name As String, Position As String)
        ' Create new Player object with known data
        Me.mId = Id
        Me.mNumber = Number
        Me.mFullName = Name
        Me.mShortPosition = Position
    End Sub

End Class
