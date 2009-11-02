<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QuickAddReference
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
        Me.components = New System.ComponentModel.Container
        Me.Tabs = New System.Windows.Forms.TabControl
        Me.cmdClose = New System.Windows.Forms.Button
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.cmdRefreshTab = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.ColumnHeader11 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader12 = New System.Windows.Forms.ColumnHeader
        Me.Label8 = New System.Windows.Forms.Label
        Me.cmdBrowse = New System.Windows.Forms.Button
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmdOptions = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Tabs
        '
        Me.Tabs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Tabs.Location = New System.Drawing.Point(12, 12)
        Me.Tabs.Name = "Tabs"
        Me.Tabs.SelectedIndex = 0
        Me.Tabs.Size = New System.Drawing.Size(616, 355)
        Me.Tabs.TabIndex = 0
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.Location = New System.Drawing.Point(553, 373)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(75, 23)
        Me.cmdClose.TabIndex = 1
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'cmdAdd
        '
        Me.cmdAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAdd.Location = New System.Drawing.Point(472, 373)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(75, 23)
        Me.cmdAdd.TabIndex = 1
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'cmdRefreshTab
        '
        Me.cmdRefreshTab.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdRefreshTab.Location = New System.Drawing.Point(12, 373)
        Me.cmdRefreshTab.Name = "cmdRefreshTab"
        Me.cmdRefreshTab.Size = New System.Drawing.Size(75, 23)
        Me.cmdRefreshTab.TabIndex = 1
        Me.cmdRefreshTab.Text = "Refresh Tab"
        Me.cmdRefreshTab.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(238, 146)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Not Available yet."
        '
        'Label6
        '
        Me.Label6.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(238, 146)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(90, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Not Available yet."
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "Name"
        Me.ColumnHeader11.Width = 202
        '
        'ColumnHeader12
        '
        Me.ColumnHeader12.Text = "Location"
        Me.ColumnHeader12.Width = -2
        '
        'Label8
        '
        Me.Label8.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(238, 146)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(90, 13)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Not Available yet."
        '
        'cmdBrowse
        '
        Me.cmdBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdBrowse.Location = New System.Drawing.Point(351, 373)
        Me.cmdBrowse.Name = "cmdBrowse"
        Me.cmdBrowse.Size = New System.Drawing.Size(115, 23)
        Me.cmdBrowse.TabIndex = 2
        Me.cmdBrowse.Text = "Add via Browse..."
        Me.ToolTip1.SetToolTip(Me.cmdBrowse, "This will add a reference to your project and to the Recent Tab. (but not to any " & _
                "other tab.)")
        Me.cmdBrowse.UseVisualStyleBackColor = True
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.Filter = "Assemblies|*.dll|Executables|*.exe|All files|*.*"
        Me.OpenFileDialog.InitialDirectory = "%WinDir%\Microsoft.Net\Framework\"
        Me.OpenFileDialog.Multiselect = True
        '
        'cmdOptions
        '
        Me.cmdOptions.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdOptions.Location = New System.Drawing.Point(93, 373)
        Me.cmdOptions.Name = "cmdOptions"
        Me.cmdOptions.Size = New System.Drawing.Size(75, 23)
        Me.cmdOptions.TabIndex = 3
        Me.cmdOptions.Text = "Options..."
        Me.cmdOptions.UseVisualStyleBackColor = True
        '
        'QuickAddReference
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdClose
        Me.ClientSize = New System.Drawing.Size(640, 408)
        Me.Controls.Add(Me.cmdOptions)
        Me.Controls.Add(Me.cmdBrowse)
        Me.Controls.Add(Me.cmdRefreshTab)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.Tabs)
        Me.Name = "QuickAddReference"
        Me.Text = "QuickAddReference"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Tabs As System.Windows.Forms.TabControl
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents cmdRefreshTab As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ColumnHeader11 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader12 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmdBrowse As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cmdOptions As System.Windows.Forms.Button
End Class
