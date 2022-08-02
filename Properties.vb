
Imports System.IO

Public Class Properties

    Private m_Properties As New Hashtable

    Public TIMER_KEY As String = "timer"
    Public WEATHER_KEY As String = "weather_key"
    Public FAVORITE_TEAM_KEY As String = "favorite_team"
    Public PROPERTIES_FILE As String = "config.mlb"


    Public Sub New()
        ' open file
        Dim sr As New StreamReader(PROPERTIES_FILE)
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

            Add(key, value)
            Trace.WriteLine($"read prop {key} = {value}")

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
        Trace.WriteLine($"got property {key}={value}")
        Return value
    End Function

    Public Sub Write()
        Dim sw As New StreamWriter(PROPERTIES_FILE)
        For Each key As String In m_Properties.Keys()
            If Not key Is Nothing Then
                sw.WriteLine($"{key}={GetProperty(key)}")
                Trace.WriteLine($"propery {key} saved as {GetProperty(key)}")
            End If
        Next
        sw.Close()
    End Sub
End Class

