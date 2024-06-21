Imports System.Windows.Forms
Imports System.IO
Imports System.Net.Http

Public Class CreateSubmissionForm

    Private WithEvents btnSubmit As New Button()

    Private Sub CreateSubmissionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeUI()
    End Sub

    Private Sub InitializeUI()
        Me.Text = "Create Submission"
        Me.Size = New Size(400, 250) ' Adjust form size

        Dim yPos As Integer = 20

        ' Labels and textboxes for user input
        AddLabelAndTextBox("Name:", yPos)
        yPos += 35 ' Increase spacing

        AddLabelAndTextBox("Email:", yPos)
        yPos += 35

        AddLabelAndTextBox("Phone Number:", yPos)
        yPos += 35

        AddLabelAndTextBox("GitHub Repo Link:", yPos)
        yPos += 35

        ' Submit button
        btnSubmit.Text = "Submit"
        btnSubmit.Size = New Size(100, 30) ' Adjust button size
        btnSubmit.Location = New Point((Me.ClientSize.Width - btnSubmit.Width) \ 2, yPos) ' Center button horizontally
        AddHandler btnSubmit.Click, AddressOf btnSubmit_Click
        Me.Controls.Add(btnSubmit)

        ' Center form on screen
        Me.StartPosition = FormStartPosition.CenterScreen
    End Sub

    Private Sub AddLabelAndTextBox(labelText As String, yPos As Integer)
        Dim lbl As New Label()
        lbl.Text = labelText
        lbl.AutoSize = True
        lbl.Location = New Point(20, yPos + 5)
        Me.Controls.Add(lbl)

        Dim txtBox As New TextBox()
        txtBox.Location = New Point(120, yPos)
        txtBox.Size = New Size(250, 20) ' Adjust textbox width
        txtBox.BackColor = Color.White
        Me.Controls.Add(txtBox)
    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs)
        Dim name As String = GetTextBoxValue(1)
        Dim email As String = GetTextBoxValue(2)
        Dim phoneNumber As String = GetTextBoxValue(3)
        Dim gitHubRepoLink As String = GetTextBoxValue(4)

        ' Validate input (if needed)

        ' Create a Submission object
        Dim newSubmission As New Submission(name, email, phoneNumber, gitHubRepoLink)

        ' Serialize to JSON
        Dim json As String = $"{{ ""Name"": ""{EscapeString(newSubmission.Name)}"", ""Email"": ""{EscapeString(newSubmission.Email)}"", ""PhoneNumber"": ""{EscapeString(newSubmission.PhoneNumber)}"", ""GitHubRepoLink"": ""{EscapeString(newSubmission.GitHubRepoLink)}"" }}"

        ' Make API call to submit data
        Await SubmitForm(json)

        ' Display success message
        MessageBox.Show("Submission details submitted successfully.")

        ' Clear textboxes after submission
        ClearTextBoxes()
    End Sub

    Private Function GetTextBoxValue(index As Integer) As String
        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is TextBox AndAlso ctrl.TabIndex = index Then
                Return DirectCast(ctrl, TextBox).Text
            End If
        Next
        Return ""
    End Function

    Private Async Function SubmitForm(jsonData As String) As Task
        Dim apiUrl As String = "http://localhost:3000/submit"

        Using client As New HttpClient()
            Dim content As New StringContent(jsonData, System.Text.Encoding.UTF8, "application/json")

            ' Send POST request
            Dim response As HttpResponseMessage = Await client.PostAsync(apiUrl, content)

            ' Ensure success status code
            response.EnsureSuccessStatusCode()
        End Using
    End Function

    Private Function EscapeString(value As String) As String
        ' Simple function to escape double quotes in JSON string
        Return value.Replace("""", "\""")
    End Function

    Private Sub ClearTextBoxes()
        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is TextBox Then
                DirectCast(ctrl, TextBox).Clear()
            End If
        Next
    End Sub
End Class
