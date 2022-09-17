

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
    Private mAPI As MlbApi = New MlbApi()
    Private mProps As SBProperties = New SBProperties()
    'Private mBattingStats As DataTable = New DataTable("BattingStats")
    'Private mPitchingStats As DataTable = New DataTable("PitchingStats")


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
