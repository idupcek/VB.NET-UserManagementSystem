Public Class Inventory
    Public SQL As New SQLControl
    Public Sub LoadGrid(Optional Query As String = "")
        If Query = "" Then
            SQL.ExecuteQuery("SELECT * FROM Products;")
        Else
            SQL.ExecuteQuery(Query)
        End If

        'error handling
        If SQL.HasException(True) Then Exit Sub

        dgvData.DataSource = SQL.DBDT
    End Sub

    'load combo box
    Private Sub LoadCBX()
        'refresh items before new query in combobox
        cbxItems.Items.Clear()

        'run query
        SQL.ExecuteQuery("SELECT username FROM members ORDER BY username ASC;")

        'error check. if any, exit
        If SQL.HasException(True) Then Exit Sub

        'loop row and add to combobox
        For Each r As DataRow In SQL.DBDT.Rows
            cbxItems.Items.Add(r("username").ToString)
        Next
    End Sub

    Private Sub FindItem()
        SQL.AddParam("@item", "%" & txtSearch.Text & "%")
        LoadGrid("SELECT * FROM Products WHERE PartNo LIKE @item;")
    End Sub


    Private Sub Inventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MdiParent = Form1
        LoadGrid()
        LoadCBX()
    End Sub

    Private Sub cmdSearch_Click(sender As Object, e As EventArgs) Handles cmdSearch.Click
        FindItem()
    End Sub
End Class