<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FilePicker
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtFilename = New System.Windows.Forms.TextBox
        Me.cmdPickFromFS = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.SuspendLayout()
        '
        'txtFilename
        '
        Me.txtFilename.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFilename.Enabled = False
        Me.txtFilename.Location = New System.Drawing.Point(0, 0)
        Me.txtFilename.Name = "txtFilename"
        Me.txtFilename.Size = New System.Drawing.Size(200, 20)
        Me.txtFilename.TabIndex = 3
        '
        'cmdPickFromFS
        '
        Me.cmdPickFromFS.Dock = System.Windows.Forms.DockStyle.Right
        Me.cmdPickFromFS.Location = New System.Drawing.Point(200, 0)
        Me.cmdPickFromFS.Name = "cmdPickFromFS"
        Me.cmdPickFromFS.Size = New System.Drawing.Size(26, 22)
        Me.cmdPickFromFS.TabIndex = 4
        Me.cmdPickFromFS.Text = "..."
        Me.cmdPickFromFS.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'FilePicker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.cmdPickFromFS)
        Me.Controls.Add(Me.txtFilename)
        Me.Name = "FilePicker"
        Me.Size = New System.Drawing.Size(226, 22)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtFilename As System.Windows.Forms.TextBox
    Friend WithEvents cmdPickFromFS As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog

End Class
