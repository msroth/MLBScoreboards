

Imports Newtonsoft.Json.Linq

Public Class Player
    Dim mId As String
    Dim mNumber As String
    Dim mFullName As String
    Dim mShortName As String
    Dim mPosition As String
    Dim mShortPosition As String
    Dim mBattingPosition As Integer
    Private mAPI As MLB_API = New MLB_API()
    Private mBattingStats As DataTable = New DataTable("BattingStats")
    Private mPitchingStats As DataTable = New DataTable("PithingStats")


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

    Public Function ConvertToFullObject() As Player
        Return New Player(Me.mId)
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

    End Sub

    Sub New(Id As String, Number As String, Name As String, Position As String)
        ' Create new Player object with known data
        Me.mId = Id
        Me.mNumber = Number
        Me.mFullName = Name
        Me.mShortPosition = Position
    End Sub


End Class
