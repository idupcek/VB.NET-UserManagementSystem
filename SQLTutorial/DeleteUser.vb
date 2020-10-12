Public Class DeleteUser
    Private SQL As New SQLControl
    Private Sub DeleteUser_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MdiParent = Form1

        FetchUsers()
    End Sub

    Private Sub FetchUsers()
        'refresh user list
        clbUsers.Items.Clear()

        'add params & run query
        SQL.AddParam("@users", "%" & txtFilter.Text & "%")
        SQL.ExecuteQuery("SELECT username " &
                         "FROM members " &
                         "WHERE username LIKE @users " &
                         "ORDER BY username ASC;")

        'loop rows & Return Users
        For Each r As DataRow In SQL.DBDT.Rows
            clbUsers.Items.Add(r("username"))
        Next
    End Sub

    Private Sub DeleteUsers()
        If MsgBox("The selected users will be deleted. Are you sure you wish to continue?", MsgBoxStyle.YesNo, "Delete User?") = MsgBoxResult.Yes Then
            'generate mass delete command
            Dim c As Integer 'unique ID for auto generated numbers
            Dim DelString As String = "" 'query string builder

            For Each i As String In clbUsers.CheckedItems
                SQL.AddParam("@user" & c, i) 'add user1 as i(user object)
                DelString += "DELETE FROM members WHERE username=@user" & c & ";"
                c += 1
            Next

            SQL.ExecuteQuery(DelString)

            'report & abort on errors
            If SQL.HasException(True) Then Exit Sub

            'report success
            MsgBox("The selected users have been deleted successfuly.")

            'refresh user list
            FetchUsers()
        End If
    End Sub

    Private Sub txtFilter_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFilter.KeyDown
        If e.KeyCode = Keys.Enter Then
            FetchUsers()

            'supress default sounds
            e.Handled = True
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub cmdDelte_Click(sender As Object, e As EventArgs) Handles cmdDelte.Click
        If clbUsers.CheckedItems.Count > 0 Then DeleteUsers()
    End Sub
End Class