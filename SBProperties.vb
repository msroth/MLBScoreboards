
Imports System.IO

Public Class SBProperties

    Private m_Properties As New Hashtable
    Public mGAME_TIMER_KEY As String = "game_refresh"
    Public mSCOREBOARD_TIMER_KEY As String = "scoreboard_refresh"
    Public mFAVORITE_TEAM_KEY As String = "favorite_team"
    Public mKEEP_DATA_FILES_KEY As String = "debug"
    Public mDATA_FILES_PATH_KEY As String = "data_files"
    Public mPROPERTIES_FILE As String = "config.mlb"

    Public Sub New()
        ' open file
        Dim fi As FileInfo = New FileInfo(mPROPERTIES_FILE)
        If Not fi.Exists() Then
            File.WriteAllText(mPROPERTIES_FILE, $"{mGAME_TIMER_KEY}=30 {vbCr} {mSCOREBOARD_TIMER_KEY}=60 {vbCr} \
                                                  {mFAVORITE_TEAM_KEY}=WSH {vbCr} {mKEEP_DATA_FILES_KEY}=0 {vbCr} \
                                                  {mDATA_FILES_PATH_KEY}=/data")
        End If

        Dim sr As New StreamReader(mPROPERTIES_FILE)

        ' load properties
        Load(sr)
        sr.Close()
    End Sub

    Public Sub Add(ByVal key As String, ByVal value As String)
        If m_Properties.ContainsKey(key) Then
            m_Properties(key) = value
        Else
            m_Properties.Add(key, value)
        End If
    End Sub

    Public Sub Load(ByRef sr As StreamReader)

        Dim line As String
        Dim key As String
        Dim value As String

        Do While sr.Peek <> -1
            line = sr.ReadLine
            If line = Nothing OrElse line.Length = 0 OrElse line.StartsWith("#") Then
                Continue Do
            End If

            key = line.Split("=")(0)
            value = line.Split("=")(1)

            Me.Add(key, value)
            'Trace.WriteLine($"read prop {key} = {value}")
        Loop

    End Sub

    Public Function GetProperty(ByVal key As String)
        Return GetProperty(key, "")
    End Function

    Public Function GetProperty(ByVal key As String, ByVal defValue As String) As String

        Dim value As String = m_Properties.Item(key)
        If value Is Nothing Then
            value = defValue
        End If
        'Trace.WriteLine($"got property {key}={value}")
        Return value
    End Function

    Public Sub Write()
        Dim sw As New StreamWriter(mPROPERTIES_FILE)
        For Each key As String In m_Properties.Keys()
            If Not key Is Nothing Then
                sw.WriteLine($"{key}={GetProperty(key)}")
                Trace.WriteLine($"propery {key} saved as {GetProperty(key)}")
            End If
        Next
        sw.Close()
    End Sub
End Class

