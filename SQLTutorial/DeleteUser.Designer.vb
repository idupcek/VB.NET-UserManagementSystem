<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class DeleteUser
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.txtFilter = New System.Windows.Forms.TextBox()
        Me.clbUsers = New System.Windows.Forms.CheckedListBox()
        Me.cmdDelte = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtFilter
        '
        Me.txtFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFilter.Location = New System.Drawing.Point(38, 27)
        Me.txtFilter.Name = "txtFilter"
        Me.txtFilter.Size = New System.Drawing.Size(161, 22)
        Me.txtFilter.TabIndex = 0
        '
        'clbUsers
        '
        Me.clbUsers.FormattingEnabled = True
        Me.clbUsers.Location = New System.Drawing.Point(38, 69)
        Me.clbUsers.Name = "clbUsers"
        Me.clbUsers.Size = New System.Drawing.Size(161, 225)
        Me.clbUsers.TabIndex = 1
        '
        'cmdDelte
        '
        Me.cmdDelte.Location = New System.Drawing.Point(38, 321)
        Me.cmdDelte.Name = "cmdDelte"
        Me.cmdDelte.Size = New System.Drawing.Size(161, 23)
        Me.cmdDelte.TabIndex = 2
        Me.cmdDelte.Text = "Delete"
        Me.cmdDelte.UseVisualStyleBackColor = True
        '
        'DeleteUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(366, 459)
        Me.Controls.Add(Me.cmdDelte)
        Me.Controls.Add(Me.clbUsers)
        Me.Controls.Add(Me.txtFilter)
        Me.Name = "DeleteUser"
        Me.Text = "DeleteUser"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtFilter As TextBox
    Friend WithEvents clbUsers As CheckedListBox
    Friend WithEvents cmdDelte As Button
End Class
