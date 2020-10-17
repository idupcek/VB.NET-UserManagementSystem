Public Class UserLogin
    Private SQL As New SQLControl
    Private AuthUser As String
    Private Sub UserLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MdiParent = Form1

    End Sub

    Private Sub cmdLogin_Click(sender As Object, e As EventArgs) Handles cmdLogin.Click
        If SQL.HasConnection = True Then
            If IsAuthenticated() = True Then
                AuthUser = txtUser.Text
                GetUserInfo()
                MsgBox("Login successful")
            End If
        End If
    End Sub

    Private Function IsAuthenticated() As Boolean
        'clear existing records
        If SQL.DBDS IsNot Nothing Then
            SQL.DBDS.Clear()
        End If

        'check the fields against db (pass will be case sensitive)
        'returns 1 for a successful find (should not be more then 1 user-pass combo)
        SQL.RunQuery("SELECT Count(username) As UserCount " &
                         "FROM members " &
                         "WHERE username='" & txtUser.Text & "' " &
                         " AND password='" & txtPass.Text & "' COLLATE SQL_Latin1_General_CP1_CS_AS ")

        MsgBox("SELECT Count(username) As UserCount " &
                         "FROM members " &
                         "WHERE username='" & txtUser.Text & "' " &
                         " AND password='" & txtPass.Text & "' COLLATE SQL_Latin1_General_CP1_CS_AS ")

        'test sql injection
        MsgBox(SQL.DBDS.Tables(0).Rows(0).Item("UserCount"))

        If SQL.DBDS.Tables(0).Rows(0).Item("UserCount") = 1 Then
            Return True
        End If

        MsgBox("Invalid user credentials.", MsgBoxStyle.Critical, "LOGIN FAILED")
        Return False
    End Function

    Private Sub GetUserInfo()
        SQL.RunQuery("SELECT username, joindate " &
                     "FROM members " &
                     "WHERE username='" & AuthUser & "' ")

        For Each i As Object In SQL.DBDS.Tables(0).Rows
            txtUserInfo.Text = "Member: " & i.Item("username") & vbCrLf &
                               "Date joined: " & i.Item("joindate")
        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SQL.AddParam("@user", txtUser.Text, DbType.String)
        SQL.AddParam("@pass", txtPass.Text, DbType.String)

        SQL.ParamQuery("SELECT username FROM members WHERE username = @user AND password = @pass", SQLControl.Collation.CaseSensitive)
    End Sub
End Class