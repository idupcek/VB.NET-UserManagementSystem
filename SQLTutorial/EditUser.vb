Public Class EditUser
    Private SQL As New SQLControl

    Private Sub FetchUsers()
        'refresh user list
        lbUsers.Items.Clear()

        'add params & run query
        SQL.AddParam("@users", "%" & txtFilter.Text & "%")
        SQL.ExecuteQuery("SELECT username " &
                         "FROM members " &
                         "WHERE username LIKE @users " &
                         "ORDER BY username ASC;")

        'report & abort on errors
        If SQL.HasException(True) Then Exit Sub

        'loop rows and return users to list
        For Each r As DataRow In SQL.DBDT.Rows
            lbUsers.Items.Add(r("username"))
        Next

    End Sub

    Private Sub GetUserDetails(Username As String)
        SQL.AddParam("@user", Username)
        'grab only a single record, the first one it finds
        SQL.ExecuteQuery("SELECT TOP 1 * " &
                         "FROM members " &
                         "WHERE username = @user;")
        'safeguard if no user was found
        If SQL.RecordCount < 1 Then Exit Sub

        For Each r As DataRow In SQL.DBDT.Rows
            txtID.Text = r("ID")
            txtUser.Text = r("username")
            txtPass.Text = r("password")
            cbActive.Checked = r("active")
            cbAdmin.Checked = r("admin")
        Next
    End Sub

    Private Sub UpdateUser()
        SQL.AddParam("@pass", txtPass.Text)
        SQL.AddParam("@active", cbActive.Checked)
        SQL.AddParam("@admin", cbAdmin.Checked)
        SQL.AddParam("@id", txtID.Text)

        SQL.ExecuteQuery("UPDATE members " &
                         "SET password=@pass, active=@active, admin=@admin " &
                         "WHERE ID=@id;")

        If SQL.HasException(True) Then Exit Sub

        MsgBox("User has been updated.")
        cmdSave.Enabled = False
    End Sub

    Private Sub EditUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MdiParent = Form1
        FetchUsers()

    End Sub


    Private Sub txtFilter_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFilter.KeyDown
        'if we press enter, do a search 
        If e.KeyCode = Keys.Enter Then
            FetchUsers()
            'below disabling weird default behaviours and sounds
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub lbUsers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lbUsers.SelectedIndexChanged
        'note: double click on listbox gives this function where lbusers.text is value of clicked item
        'passes selected user we clicked on
        GetUserDetails(lbUsers.Text)
    End Sub

    Private Sub cmdSave_Click(sender As Object, e As EventArgs) Handles cmdSave.Click
        UpdateUser()
    End Sub
End Class