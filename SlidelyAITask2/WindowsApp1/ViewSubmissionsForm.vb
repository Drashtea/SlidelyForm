Imports System.Net.Http
Imports Newtonsoft.Json.Linq ' Ensure Newtonsoft.Json package is installed

Public Class ViewSubmissionsForm
    Inherits Form

    ' Fields for storing submission data and current index
    Private submissions As List(Of Submission)
    Private currentIndex As Integer = 0

    ' UI Controls
    Private WithEvents btnPrevious As Button
    Private WithEvents btnNext As Button
    Private lblName As Label
    Private lblEmail As Label
    Private lblPhoneNumber As Label
    Private lblGitHubRepoLink As Label

    ' Constructor
    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()

        ' Initialize UI controls
        InitializeControls()

        ' Initialize submissions list
        submissions = New List(Of Submission)()

        ' Fetch submissions from API
        FetchSubmissions()
    End Sub

    ' Method to initialize UI controls
    Private Sub InitializeControls()
        ' Initialize buttons
        btnPrevious = New Button()
        btnPrevious.Location = New System.Drawing.Point(50, 180)
        btnPrevious.Name = "btnPrevious"
        btnPrevious.Size = New System.Drawing.Size(75, 23)
        btnPrevious.Text = "Previous"
        btnPrevious.UseVisualStyleBackColor = True

        btnNext = New Button()
        btnNext.Location = New System.Drawing.Point(150, 180)
        btnNext.Name = "btnNext"
        btnNext.Size = New System.Drawing.Size(75, 23)
        btnNext.Text = "Next"
        btnNext.UseVisualStyleBackColor = True

        ' Initialize labels
        lblName = New Label()
        lblName.AutoSize = True
        lblName.Location = New System.Drawing.Point(50, 50)
        lblName.Name = "lblName"
        lblName.Size = New System.Drawing.Size(46, 17)
        lblName.Text = "Name"

        lblEmail = New Label()
        lblEmail.AutoSize = True
        lblEmail.Location = New System.Drawing.Point(50, 80)
        lblEmail.Name = "lblEmail"
        lblEmail.Size = New System.Drawing.Size(46, 17)
        lblEmail.Text = "Email"

        lblPhoneNumber = New Label()
        lblPhoneNumber.AutoSize = True
        lblPhoneNumber.Location = New System.Drawing.Point(50, 110)
        lblPhoneNumber.Name = "lblPhoneNumber"
        lblPhoneNumber.Size = New System.Drawing.Size(106, 17)
        lblPhoneNumber.Text = "Phone Number"

        lblGitHubRepoLink = New Label()
        lblGitHubRepoLink.AutoSize = True
        lblGitHubRepoLink.Location = New System.Drawing.Point(50, 140)
        lblGitHubRepoLink.Name = "lblGitHubRepoLink"
        lblGitHubRepoLink.Size = New System.Drawing.Size(123, 17)
        lblGitHubRepoLink.Text = "GitHub Repo Link"

        ' Add controls to form
        Me.Controls.Add(btnPrevious)
        Me.Controls.Add(btnNext)
        Me.Controls.Add(lblName)
        Me.Controls.Add(lblEmail)
        Me.Controls.Add(lblPhoneNumber)
        Me.Controls.Add(lblGitHubRepoLink)

        ' Add event handlers
        AddHandler btnPrevious.Click, AddressOf btnPrevious_Click
        AddHandler btnNext.Click, AddressOf btnNext_Click
    End Sub

    ' Method to fetch submissions from API
    Private Async Sub FetchSubmissions()
        Try
            Dim client As New HttpClient()
            Dim response As HttpResponseMessage = Await client.GetAsync("http://localhost:3000/api/read")

            If response.IsSuccessStatusCode Then
                Dim json As String = Await response.Content.ReadAsStringAsync()
                Dim jsonArray As JArray = JArray.Parse(json)

                For Each item As JObject In jsonArray
                    Dim submission As New Submission()
                    submission.Name = item("name").ToString()
                    submission.Email = item("email").ToString()
                    submission.PhoneNumber = item("phone").ToString()
                    submission.GitHubRepoLink = item("github_link").ToString()

                    submissions.Add(submission)
                Next

                ' Display the first submission
                DisplaySubmission(currentIndex)
            Else
                MessageBox.Show("Failed to fetch submissions: " & response.ReasonPhrase, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred while fetching submissions: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Method to display submission details based on index
    Private Sub DisplaySubmission(index As Integer)
        ' Check if submissions list is not null and index is within bounds
        If submissions IsNot Nothing AndAlso index >= 0 AndAlso index < submissions.Count Then
            Dim submission = submissions(index)
            lblName.Text = "Name: " & submission.Name
            lblEmail.Text = "Email: " & submission.Email
            lblPhoneNumber.Text = "Phone Number: " & submission.PhoneNumber
            lblGitHubRepoLink.Text = "GitHub Repo Link: " & submission.GitHubRepoLink

            ' Update UI based on current index
            btnPrevious.Enabled = (index > 0)
            btnNext.Enabled = (index < submissions.Count - 1)
        Else
            ' Handle case where submissions list is null or index is out of bounds
            MessageBox.Show("No submissions available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' Event handler for Previous button click
    Private Sub btnPrevious_Click(sender As Object, e As EventArgs)
        ' Decrement index and display previous submission
        If currentIndex > 0 Then
            currentIndex -= 1
            DisplaySubmission(currentIndex)
        End If
    End Sub

    ' Event handler for Next button click
    Private Sub btnNext_Click(sender As Object, e As EventArgs)
        ' Increment index and display next submission
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            DisplaySubmission(currentIndex)
        End If
    End Sub
End Class
