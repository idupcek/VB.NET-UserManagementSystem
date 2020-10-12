Public Class Form1
    Private Sub miInventory_Click(sender As Object, e As EventArgs) Handles miInventory.Click
        Inventory.Show()
    End Sub

    Private Sub msiNewUser_Click(sender As Object, e As EventArgs) Handles msiNewUser.Click
        NewUser.Show()
    End Sub

    Private Sub msiEditUser_Click(sender As Object, e As EventArgs) Handles msiEditUser.Click
        EditUser.Show()
    End Sub

    Private Sub msiDeleteUser_Click(sender As Object, e As EventArgs) Handles msiDeleteUser.Click
        DeleteUser.Show()
    End Sub
End Class
