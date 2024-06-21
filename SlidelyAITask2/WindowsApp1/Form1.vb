Imports System.Windows.Forms

Public Class Form1

    Private WithEvents btnViewSubmissions As Button
    Private WithEvents btnCreateSubmission As Button
    Private pnlHomePage As Panel ' Panel for home page content
    Private pnlViewSubmissionsPage As Panel ' Panel for view submissions page content

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeUI()
    End Sub

    Private Sub InitializeUI()
        Me.KeyPreview = True

        ' Create and configure TableLayoutPanel
        Dim tableLayoutPanel As New TableLayoutPanel()
        tableLayoutPanel.Dock = DockStyle.Fill
        tableLayoutPanel.RowCount = 5
        tableLayoutPanel.ColumnCount = 1
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 40.0F)) ' Top spacer
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.AutoSize)) ' Label
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.AutoSize)) ' Button 1
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.AutoSize)) ' Button 2
        tableLayoutPanel.RowStyles.Add(New RowStyle(SizeType.Percent, 40.0F)) ' Bottom spacer

        ' Create and configure Label
        Dim label As New Label()
        label.Text = "Welcome to Home Page"
        label.AutoSize = True
        label.Margin = New Padding(0, 20, 0, 40)
        label.Anchor = AnchorStyles.None
        label.TextAlign = ContentAlignment.MiddleCenter
        label.Font = New Font("Arial", 14, FontStyle.Bold)
        tableLayoutPanel.Controls.Add(label, 0, 1)

        ' Create and configure Button1
        btnViewSubmissions = New Button()
        btnViewSubmissions.Text = "View Submissions (ctrl+V)"
        btnViewSubmissions.Size = New Size(280, 50)
        btnViewSubmissions.Anchor = AnchorStyles.None
        btnViewSubmissions.BackColor = Color.FromArgb(173, 216, 230)
        btnViewSubmissions.Margin = New Padding(0, 10, 0, 35)
        btnViewSubmissions.Name = "btnViewSubmissions"
        btnViewSubmissions.Font = New Font("Arial", 12, FontStyle.Bold)
        tableLayoutPanel.Controls.Add(btnViewSubmissions, 0, 2)

        ' Create and configure Button2
        btnCreateSubmission = New Button()
        btnCreateSubmission.Text = "Create New Submission (ctrl+N)"
        btnCreateSubmission.Size = New Size(280, 50)
        btnCreateSubmission.Anchor = AnchorStyles.None
        btnCreateSubmission.BackColor = Color.FromArgb(255, 255, 224)
        btnCreateSubmission.Margin = New Padding(0, 10, 0, 25)
        btnCreateSubmission.Name = "btnCreateSubmission"
        btnCreateSubmission.Font = New Font("Arial", 12, FontStyle.Bold)
        tableLayoutPanel.Controls.Add(btnCreateSubmission, 0, 3)

        ' Create Panel for home page content
        pnlHomePage = New Panel()
        pnlHomePage.Dock = DockStyle.Fill
        tableLayoutPanel.Controls.Add(pnlHomePage, 0, 4)

        ' Initialize home page content
        Dim homeLabel As New Label()
        homeLabel.Text = "Welcome to Home Page"
        homeLabel.AutoSize = True
        homeLabel.Location = New Point(50, 50)
        pnlHomePage.Controls.Add(homeLabel)

        ' Create Panel for view submissions page content
        pnlViewSubmissionsPage = New Panel()
        pnlViewSubmissionsPage.Dock = DockStyle.Fill

        ' Add TableLayoutPanel to the form
        Me.Controls.Add(tableLayoutPanel)

        ' Add event handlers
        AddHandler btnViewSubmissions.Click, AddressOf btnViewSubmissions_Click
        AddHandler btnCreateSubmission.Click, AddressOf btnCreateSubmission_Click
        AddHandler Me.KeyDown, AddressOf Form1_KeyDown
    End Sub

    Private Sub btnViewSubmissions_Click(sender As Object, e As EventArgs) Handles btnViewSubmissions.Click
        ' Create and show ViewSubmissionsForm as a dialog
        Dim viewSubmissionsForm As New ViewSubmissionsForm()
        viewSubmissionsForm.ShowDialog()
    End Sub



    Private Sub btnCreateSubmission_Click(sender As Object, e As EventArgs)
        ' Create and show CreateSubmissionForm as a dialog
        Dim createForm As New CreateSubmissionForm()
        createForm.ShowDialog()
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs)
        If e.Control AndAlso e.KeyCode = Keys.V Then
            btnViewSubmissions_Click(Nothing, Nothing)
        ElseIf e.Control AndAlso e.KeyCode = Keys.N Then
            btnCreateSubmission_Click(Nothing, Nothing)
        End If
    End Sub

End Class
