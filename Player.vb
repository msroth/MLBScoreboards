

Imports Newtonsoft.Json.Linq

Public Class Player
    Dim mId As String
    Dim mNumber As String
    Dim mFullName As String
    Dim mPosition As String
    Private mAPI As MLB_API = New MLB_API()

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



    Sub New(Id As String)
        Me.mId = Id

        Dim Data As JObject = mAPI.ReturnPlayerData(Id)
        Dim PlayerData As JProperty = Data.Property("people")
        Dim Player As JObject = PlayerData.Value(0)
        mNumber = Player.SelectToken("primaryNumber")
        mFullName = Player.SelectToken("boxscoreName")
        mPosition = Player.SelectToken("primaryPosition.abbreviation")

    End Sub


    Sub LoadPlayerStats()


    End Sub


End Class
