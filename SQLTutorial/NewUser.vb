Public Class NewUser
    Private SQL As New SQLControl
    Private Sub NewUSer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MdiParent = Form1
    End Sub

    Private Sub InsertUser()
        'add sql params & run the command
        SQL.AddParam("@user", txtUser.Text)
        SQL.AddParam("@pass", txtPass.Text)
        SQL.AddParam("@active", cbActive.Checked)
        SQL.AddParam("@admin", cbAdmin.Checked)

        SQL.ExecuteQuery("INSERT INTO members (username, password, active, admin, joindate) " &
                         "VALUES(@user, @pass, @active, @admin, GETDATE());", True)

        'report on errrors
        If SQL.HasException(True) Then Exit Sub

        'getting that last entry ID as key-value in the table after passing above second arg to True
        If SQL.DBDT.Rows.Count > 0 Then
            Dim r As DataRow = SQL.DBDT.Rows(0)
            MsgBox(r("LastID").ToString)
        End If

        MsgBox("User created successfully.")
    End Sub

    Private Sub cmdSubmit_Click(sender As Object, e As EventArgs) Handles cmdSubmit.Click
        InsertUser()

        'clear fields after submit
        txtUser.Clear()
        txtPass.Clear()
        cbActive.Checked = False
        cbAdmin.Checked = False
        cmdSubmit.Enabled = False
    End Sub

    'handles both input fields
    Private Sub txtFields_TextChanged(sender As Object, e As EventArgs) Handles txtUser.TextChanged, txtPass.TextChanged
        'basic validation
        If Not String.IsNullOrWhiteSpace(txtUser.Text) AndAlso Not String.IsNullOrWhiteSpace(txtPass.Text) Then
            'setting button in the form to enables so it's clickable
            cmdSubmit.Enabled = True
        End If
    End Sub
End Class