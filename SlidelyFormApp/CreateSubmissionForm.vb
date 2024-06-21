Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json

Public Class CreateSubmissionForm
    Private stopwatch As Stopwatch = New Stopwatch()

    Private Sub btnToggleStopwatch_Click(sender As Object, e As EventArgs) Handles btnToggleStopwatch.Click
        If stopwatch.IsRunning Then
            stopwatch.Stop()
            btnToggleStopwatch.Text = "RESUME STOPWATCH (CTRL + T)"
        Else
            stopwatch.Start()
            btnToggleStopwatch.Text = "PAUSE STOPWATCH (CTRL + T)"
        End If
        UpdateStopwatchLabel()
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        SubmitForm()
    End Sub

    Private Sub UpdateStopwatchLabel()
        lblStopwatch.Text = String.Format("{0:hh\:mm\:ss}", stopwatch.Elapsed)
    End Sub

    Private Async Sub SubmitForm()
        Dim submission = New With {
            .name = txtName.Text,
            .email = txtEmail.Text,
            .phone = txtPhoneNum.Text,
            .github_link = txtGithubLink.Text,
            .stopwatch_time = lblStopwatch.Text
        }
        Dim json = JsonConvert.SerializeObject(submission)
        Dim content = New StringContent(json, Encoding.UTF8, "application/json")

        Using client As New HttpClient()
            Dim response = Await client.PostAsync("http://localhost:3000/submit", content)
            If response.IsSuccessStatusCode Then
                MessageBox.Show("Submission saved successfully!")
                Me.Close()
            Else
                MessageBox.Show("Error saving submission!")
            End If
        End Using
    End Sub
End Class
