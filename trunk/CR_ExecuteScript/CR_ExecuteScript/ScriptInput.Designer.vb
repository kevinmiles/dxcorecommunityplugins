<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ScriptInput
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.txtSource = New System.Windows.Forms.TextBox
        Me.cmdOk = New System.Windows.Forms.Button
        Me.TabPages = New System.Windows.Forms.TabControl
        Me.tabSource = New System.Windows.Forms.TabPage
        Me.tabImports = New System.Windows.Forms.TabPage
        Me.txtImports = New System.Windows.Forms.TextBox
        Me.tabReferences = New System.Windows.Forms.TabPage
        Me.txtReferences = New System.Windows.Forms.TextBox
        Me.TabPages.SuspendLayout()
        Me.tabSource.SuspendLayout()
        Me.tabImports.SuspendLayout()
        Me.tabReferences.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtSource
        '
        Me.txtSource.Location = New System.Drawing.Point(6, 6)
        Me.txtSource.Multiline = True
        Me.txtSource.Name = "txtSource"
        Me.txtSource.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtSource.Size = New System.Drawing.Size(489, 207)
        Me.txtSource.TabIndex = 0
        Me.txtSource.Text = "' Action Code"
        '
        'cmdOk
        '
        Me.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdOk.Location = New System.Drawing.Point(485, 294)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(75, 23)
        Me.cmdOk.TabIndex = 1
        Me.cmdOk.Text = "Ok"
        Me.cmdOk.UseVisualStyleBackColor = True
        '
        'TabPages
        '
        Me.TabPages.Controls.Add(Me.tabSource)
        Me.TabPages.Controls.Add(Me.tabImports)
        Me.TabPages.Controls.Add(Me.tabReferences)
        Me.TabPages.Location = New System.Drawing.Point(32, 24)
        Me.TabPages.Name = "TabPages"
        Me.TabPages.SelectedIndex = 0
        Me.TabPages.Size = New System.Drawing.Size(509, 245)
        Me.TabPages.TabIndex = 2
        '
        'tabSource
        '
        Me.tabSource.Controls.Add(Me.txtSource)
        Me.tabSource.Location = New System.Drawing.Point(4, 22)
        Me.tabSource.Name = "tabSource"
        Me.tabSource.Padding = New System.Windows.Forms.Padding(3)
        Me.tabSource.Size = New System.Drawing.Size(501, 219)
        Me.tabSource.TabIndex = 0
        Me.tabSource.Text = "Source"
        Me.tabSource.UseVisualStyleBackColor = True
        '
        'tabImports
        '
        Me.tabImports.Controls.Add(Me.txtImports)
        Me.tabImports.Location = New System.Drawing.Point(4, 22)
        Me.tabImports.Name = "tabImports"
        Me.tabImports.Padding = New System.Windows.Forms.Padding(3)
        Me.tabImports.Size = New System.Drawing.Size(501, 219)
        Me.tabImports.TabIndex = 1
        Me.tabImports.Text = "Imports"
        Me.tabImports.UseVisualStyleBackColor = True
        '
        'txtImports
        '
        Me.txtImports.Location = New System.Drawing.Point(6, 6)
        Me.txtImports.Multiline = True
        Me.txtImports.Name = "txtImports"
        Me.txtImports.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtImports.Size = New System.Drawing.Size(489, 207)
        Me.txtImports.TabIndex = 1
        '
        'tabReferences
        '
        Me.tabReferences.Controls.Add(Me.txtReferences)
        Me.tabReferences.Location = New System.Drawing.Point(4, 22)
        Me.tabReferences.Name = "tabReferences"
        Me.tabReferences.Padding = New System.Windows.Forms.Padding(3)
        Me.tabReferences.Size = New System.Drawing.Size(501, 219)
        Me.tabReferences.TabIndex = 2
        Me.tabReferences.Text = "References"
        Me.tabReferences.UseVisualStyleBackColor = True
        '
        'txtReferences
        '
        Me.txtReferences.Location = New System.Drawing.Point(6, 6)
        Me.txtReferences.Multiline = True
        Me.txtReferences.Name = "txtReferences"
        Me.txtReferences.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtReferences.Size = New System.Drawing.Size(489, 207)
        Me.txtReferences.TabIndex = 1
        '
        'ScriptInput
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(572, 329)
        Me.Controls.Add(Me.TabPages)
        Me.Controls.Add(Me.cmdOk)
        Me.Name = "ScriptInput"
        Me.Text = "ScriptInput"
        Me.TabPages.ResumeLayout(False)
        Me.tabSource.ResumeLayout(False)
        Me.tabSource.PerformLayout()
        Me.tabImports.ResumeLayout(False)
        Me.tabImports.PerformLayout()
        Me.tabReferences.ResumeLayout(False)
        Me.tabReferences.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtSource As System.Windows.Forms.TextBox
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents TabPages As System.Windows.Forms.TabControl
    Friend WithEvents tabSource As System.Windows.Forms.TabPage
    Friend WithEvents tabImports As System.Windows.Forms.TabPage
    Friend WithEvents txtImports As System.Windows.Forms.TextBox
    Friend WithEvents tabReferences As System.Windows.Forms.TabPage
    Friend WithEvents txtReferences As System.Windows.Forms.TextBox
End Class
