

Imports Newtonsoft.Json.Linq

Public Class Player
    Dim mId As String
    Dim mNumber As String
    Dim mFullName As String
    Dim mPosition As String
    Private mAPI As MLB_API = New MLB_API()
    Private mSeasonStats As DataTable = New DataTable("SeasonStats")
    Private mCareerStats As DataTable = New DataTable("CareerStats")


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

    Public ReadOnly Property SeasonStats() As DataTable
        Get
            If mSeasonStats.Rows.Count = 0 Then
                LoadSeasonStats()
            End If
            Return Me.mSeasonStats
        End Get
    End Property

    Public ReadOnly Property CareerStats() As DataTable
        Get
            If mCareerStats.Rows.Count = 0 Then
                LoadCareerStats()
            End If
            Return Me.mCareerStats
        End Get
    End Property


    Sub New(Id As String)
        ' Create new Player object by querying API

        Me.mId = Id
        Dim Data As JObject = mAPI.ReturnPlayerData(Id)
        Dim PlayerData As JProperty = Data.Property("people")
        Dim Player As JObject = PlayerData.Value(0)
        Me.mNumber = Player.SelectToken("primaryNumber")
        Me.mFullName = Player.SelectToken("boxscoreName")
        Me.mPosition = Player.SelectToken("primaryPosition.abbreviation")

    End Sub

    Sub New(Id As String, Number As String, Name As String, Position As String)
        ' Create new Player object with known data
        Me.mId = Id
        Me.mNumber = Number
        Me.mFullName = Name
        Me.mPosition = Position
    End Sub


    Private Sub LoadSeasonStats()


    End Sub

    Private Sub LoadCareerStats()


    End Sub
End Class
