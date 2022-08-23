

Imports Newtonsoft.Json.Linq

Public Class Player
    Dim mId As String
    Dim mNumber As String
    Dim mFullName As String
    Dim mPosition As String
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

    Public ReadOnly Property Name() As String
        Get
            Return Me.mFullName
        End Get
    End Property

    Public ReadOnly Property Position() As String
        Get
            Return Me.mPosition
        End Get
    End Property

    Public ReadOnly Property BattingStats() As DataTable
        Get
            If mBattingStats.Rows.Count = 0 Then
                LoadBattingStats()
            End If
            Return Me.mBattingStats
        End Get
    End Property

    Public ReadOnly Property PitchingStats() As DataTable
        Get
            If mPitchingStats.Rows.Count = 0 Then
                LoadPitchingStats()
            End If
            Return Me.mPitchingStats
        End Get
    End Property


    Sub New(Id As String)
        ' Create new Player object by querying API

        Me.mId = Id
        Dim Data As JObject = mAPI.ReturnPlayerData(Id)
        Dim PlayerData As JProperty = Data.Property("people")
        Dim ThisPlayer As JObject = PlayerData.Value(0)
        Me.mNumber = ThisPlayer.SelectToken("primaryNumber")
        Me.mFullName = ThisPlayer.SelectToken("boxscoreName")
        Me.mPosition = ThisPlayer.SelectToken("primaryPosition.abbreviation")

    End Sub

    Sub New(Id As String, Number As String, Name As String, Position As String)
        ' Create new Player object with known data
        Me.mId = Id
        Me.mNumber = Number
        Me.mFullName = Name
        Me.mPosition = Position
    End Sub


    Private Sub LoadBattingStats()

        ' http://statsapi.mlb.com/api/v1/stats?stats=season&group=hitting&personId=602104
        ' http://statsapi.mlb.com/api/v1/stats?stats=career&group=hitting&personId=602104

        ' personID is ignored
        ' must loop through data looking for this player id

    End Sub

    Private Sub LoadPitchingStats()


    End Sub
End Class
