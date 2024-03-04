
Imports System.IO
Imports NLog


Public Class MlbScoreboardsProperties

    Public Shared m_Properties As New Hashtable

    Public ReadOnly mGAME_TIMER_KEY As String = "game_refresh"
    Public ReadOnly mSCOREBOARD_TIMER_KEY As String = "scoreboard_refresh"
    Public ReadOnly mFAVORITE_TEAM_KEY As String = "favorite_team"
    Public ReadOnly mKEEP_DATA_FILES_KEY As String = "debug"
    Public ReadOnly mDATA_FILES_PATH_KEY As String = "data_files"
    Public ReadOnly mPROPERTIES_FILE As String = "config.mlb"
    Private mValidPropertyKeys As New List(Of String) From {mGAME_TIMER_KEY, mSCOREBOARD_TIMER_KEY, mFAVORITE_TEAM_KEY, mKEEP_DATA_FILES_KEY, mDATA_FILES_PATH_KEY}

    Shared ReadOnly Logger As Logger = LogManager.GetCurrentClassLogger()

    Public Sub New()
        Try
            If m_Properties.Keys.Count = 0 Then
                ' open file
                Dim fi As FileInfo = New FileInfo(mPROPERTIES_FILE)
                Logger.Debug($"properties file is: {fi.FullName}")

                If Not fi.Exists() Then
                    File.WriteAllText(mPROPERTIES_FILE, $"{mGAME_TIMER_KEY}=30 {vbCr} {mSCOREBOARD_TIMER_KEY}=60 {vbCr} \
                                                  {mFAVORITE_TEAM_KEY}=WSH {vbCr} {mKEEP_DATA_FILES_KEY}=0 {vbCr} \
                                                  {mDATA_FILES_PATH_KEY}=/data")
                End If

                Dim sr As New StreamReader(mPROPERTIES_FILE)

                ' load properties
                Do While sr.Peek <> -1
                    Dim line As String = sr.ReadLine
                    If line = Nothing OrElse line.Length = 0 OrElse line.StartsWith("#") Then
                        Continue Do
                    End If

                    Dim key As String = line.Split("=")(0)
                    Dim value As String = line.Split("=")(1)

                    If mValidPropertyKeys.Contains(key) Then
                        Me.Add(Trim(key), Trim(value))
                        Logger.Debug($"loaded prop {key} = {value}")
                    Else
                        Logger.Error($"encountered invalid property {key} = {value}")
                    End If
                Loop
                sr.Close()
            End If
        Catch ex As Exception
            Logger.Debug(ex)
        End Try
    End Sub

    Public Sub Add(ByVal key As String, ByVal value As String)
        If m_Properties.ContainsKey(key) Then
            m_Properties(key) = value
        Else
            m_Properties.Add(key, value)
        End If
    End Sub

    Public Function GetProperty(ByVal key As String)
        Logger.Debug($"Got property {key} = {GetProperty(key, "")}")
        Return GetProperty(key, "")
    End Function

    Public Function GetProperty(ByVal key As String, ByVal defValue As String) As String

        Dim value As String = m_Properties.Item(key)
        If value Is Nothing Then
            value = defValue
        End If
        Return value
    End Function

    Public Sub Write()
        Try
            Dim sw As New StreamWriter(mPROPERTIES_FILE)
            For Each key As String In m_Properties.Keys()
                If key IsNot Nothing Then
                    sw.WriteLine($"{key}={GetProperty(key)}")
                    Logger.Debug($"propery {key} saved as {GetProperty(key)}")
                End If
            Next
            sw.Close()
        Catch ex As Exception
            Logger.Debug(ex)
        End Try

    End Sub
End Class

'<SDG><
