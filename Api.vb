Imports System.Net
Imports System.Collections.Specialized
Imports System.Configuration

Public Class Api

    Private _url As String = "http://localhost:2410/api/"

    Sub New()

        _url = GetConfigValue("SNSRi.API.URL", "http://localhost:2410/api/")

    End Sub

    Public Sub InvokeChangeValue(referenceId As Integer, status As String, dateOccurred As DateTime)

        Dim changeEventUrl = _url & "ChangeDeviceValue/" & referenceId

        Console.WriteLine("Connecting to '" & changeEventUrl & "' with " & referenceId & ", " & status & ", " & dateOccurred.ToString)

        Try
            Using client As New WebClient
                Dim values As New NameValueCollection
                values("ReferenceId") = referenceId
                values("NewStatus") = status
                values("OccurredOn") = dateOccurred

                'TODO: make async!
                Dim response = client.UploadValues(changeEventUrl, values)
            End Using

        Catch ex As Exception
            Console.WriteLine("An error occurred: " & ex.Message)
        End Try




    End Sub

    Private Function GetConfigValue(appSetting As String, defaultValue As String) As String
        Dim val = ConfigurationManager.AppSettings.Item(appSetting)
        If (String.IsNullOrEmpty(val)) Then
            val = defaultValue
        End If
        Return val
    End Function



End Class
