' =========================================================================================================
' (C) 2022 MSRoth
'
' Released under XXX license.
' =========================================================================================================

Imports Newtonsoft.Json.Linq
Imports NLog

Public Class MlbBroadcasters

    Private mGame As MlbGame
    Private mTable As DataTable = New DataTable()
    Private mAPI As MlbApi = New MlbApi

    Private mLanguages As Dictionary(Of String, String) = New Dictionary(Of String, String)

    Shared ReadOnly Logger As Logger = LogManager.GetCurrentClassLogger()

    Public Property Game As MlbGame
        Set(value As MlbGame)
            Me.mGame = value
        End Set
        Get
            Return mGame
        End Get
    End Property

    Private Sub MlbBroadcasters_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Logger.Debug($"Loading {Me.Name}")

        ' abort if pre-reqs not fulfilled
        If Me.mGame Is Nothing Then
            Return
        End If

        Me.Cursor = Cursors.WaitCursor

        ' init data structures
        InitTableCols()
        LoadLanguageDic()

        ' set titles
        Me.lblGameTitle.Text = mGame.GameTitleGamePk()

        Try
            ' get JSON data from API
            Dim gamedate = DateTime.Parse(mGame.GameDateTime()).ToString("MM/dd/yyyy")
            Dim schedule As JObject = Me.mAPI.ReturnScheduleData(gamedate)
            Dim gameDates As JArray = schedule.SelectToken("dates")

            ' process data looking for this game
            For Each gDate As JObject In gameDates
                Dim games As JArray = gDate.SelectToken("games")

                For Each game As JObject In games
                    If Me.mGame.GamePk = Convert.ToInt32(game.SelectToken("gamePk").ToString()) Then
                        Dim Broadcasters As JArray = game.SelectToken("broadcasts")

                        For i As Integer = 0 To Broadcasters.Count - 1
                            Dim broadcast As JObject = Broadcasters.Item(i)

                            Dim name As String = broadcast.SelectToken("name")
                            Dim type As String = broadcast.SelectToken("type")
                            Dim language As String = broadcast.SelectToken("language")
                            Dim side As String = broadcast.SelectToken("homeAway")
                            Dim national As String = broadcast.SelectToken("isNational")
                            Dim callsign As String = broadcast.SelectToken("callSign")

                            Dim row As DataRow = mTable.NewRow()
                            row("Name") = name
                            row("Type") = type.ToUpper()
                            row("Language") = ResolveLanguageCode(language)
                            row("Side") = side.ToUpper()
                            If national Is Nothing Then
                                row("Scope") = "Local"
                            Else
                                row("Scope") = "National"
                            End If
                            row("Call Sign") = callsign

                            mTable.Rows.Add(row)
                        Next
                        Exit For
                    End If
                Next
            Next

        Catch ex As Exception
            Logger.Error(ex)
        End Try

        ' set data on data grid
        dgvBroadcasters.DataSource = mTable
        dgvBroadcasters.ColumnHeadersDefaultCellStyle.Font = New Font(dgvBroadcasters.DefaultFont, FontStyle.Bold)
        dgvBroadcasters.ClearSelection()

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub InitTableCols()
        Dim ColNames As String() = {"Type", "Call Sign", "Name", "Language", "Side", "Scope"}
        For Each name As String In ColNames
            Dim col As DataColumn = New DataColumn()
            col.ColumnName = name
            mTable.Columns.Add(col)
        Next
    End Sub

    Private Sub LoadLanguageDic()
        ' top ten most spoken languages
        mLanguages.Add("ar", "Arabic")
        mLanguages.Add("bn", "Bengali")
        mLanguages.Add("zn", "Chinese")
        mLanguages.Add("en", "English")
        mLanguages.Add("fr", "French")
        mLanguages.Add("hi", "Hindi")
        mLanguages.Add("in", "Indonesian")
        mLanguages.Add("pt", "Portuguese")
        mLanguages.Add("ru", "Russian")
        mLanguages.Add("es", "Spansih")
    End Sub

    Private Function ResolveLanguageCode(code As String) As String
        If mLanguages.ContainsKey(code) Then
            Return mLanguages.Item(code)
        Else
            Return code.ToUpper()
        End If
    End Function

End Class

'<SDG><
