Public Class Submission
    Public Property Name As String
    Public Property Email As String
    Public Property PhoneNumber As String
    Public Property GitHubRepoLink As String

    Public Sub New(name As String, email As String, phoneNumber As String, gitHubRepoLink As String)
        Me.Name = name
        Me.Email = email
        Me.PhoneNumber = phoneNumber
        Me.GitHubRepoLink = gitHubRepoLink
    End Sub
End Class
