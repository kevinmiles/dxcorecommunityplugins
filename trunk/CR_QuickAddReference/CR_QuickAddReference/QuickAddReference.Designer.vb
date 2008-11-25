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
        Me.Tabs = New System.Windows.Forms.TabControl
        Me.TabSolution = New System.Windows.Forms.TabPage
        Me.lstSolution = New System.Windows.Forms.ListView
        Me.ColName = New System.Windows.Forms.ColumnHeader
        Me.ColLocation = New System.Windows.Forms.ColumnHeader
        Me.TabRecent = New System.Windows.Forms.TabPage
        Me.lstRecent = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.cmdClose = New System.Windows.Forms.Button
        Me.cmdAdd = New System.Windows.Forms.Button
        Me.cmdRefreshTab = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Tabs.SuspendLayout()
        Me.TabSolution.SuspendLayout()
        Me.TabRecent.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Tabs
        '
        Me.Tabs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Tabs.Controls.Add(Me.TabSolution)
        Me.Tabs.Controls.Add(Me.TabRecent)
        Me.Tabs.Location = New System.Drawing.Point(12, 12)
        Me.Tabs.Name = "Tabs"
        Me.Tabs.SelectedIndex = 0
        Me.Tabs.Size = New System.Drawing.Size(611, 354)
        Me.Tabs.TabIndex = 0
        '
        'TabSolution
        '
        Me.TabSolution.Controls.Add(Me.lstSolution)
        Me.TabSolution.Location = New System.Drawing.Point(4, 22)
        Me.TabSolution.Name = "TabSolution"
        Me.TabSolution.Padding = New System.Windows.Forms.Padding(3)
        Me.TabSolution.Size = New System.Drawing.Size(364, 194)
        Me.TabSolution.TabIndex = 0
        Me.TabSolution.Text = "Solution"
        Me.TabSolution.UseVisualStyleBackColor = True
        '
        'lstSolution
        '
        Me.lstSolution.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstSolution.CheckBoxes = True
        Me.lstSolution.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColName, Me.ColLocation})
        Me.lstSolution.FullRowSelect = True
        Me.lstSolution.Location = New System.Drawing.Point(6, 6)
        Me.lstSolution.Name = "lstSolution"
        Me.lstSolution.Size = New System.Drawing.Size(352, 182)
        Me.lstSolution.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lstSolution.TabIndex = 0
        Me.lstSolution.UseCompatibleStateImageBehavior = False
        Me.lstSolution.View = System.Windows.Forms.View.Details
        '
        'ColName
        '
        Me.ColName.Text = "Name"
        Me.ColName.Width = 202
        '
        'ColLocation
        '
        Me.ColLocation.Text = "Location"
        Me.ColLocation.Width = 138
        '
        'TabRecent
        '
        Me.TabRecent.Controls.Add(Me.Panel1)
        Me.TabRecent.Controls.Add(Me.lstRecent)
        Me.TabRecent.Location = New System.Drawing.Point(4, 22)
        Me.TabRecent.Name = "TabRecent"
        Me.TabRecent.Padding = New System.Windows.Forms.Padding(3)
        Me.TabRecent.Size = New System.Drawing.Size(603, 328)
        Me.TabRecent.TabIndex = 1
        Me.TabRecent.Text = "Recent"
        Me.TabRecent.UseVisualStyleBackColor = True
        '
        'lstRecent
        '
        Me.lstRecent.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstRecent.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2})
        Me.lstRecent.FullRowSelect = True
        Me.lstRecent.Location = New System.Drawing.Point(6, 6)
        Me.lstRecent.Name = "lstRecent"
        Me.lstRecent.Size = New System.Drawing.Size(591, 316)
        Me.lstRecent.TabIndex = 1
        Me.lstRecent.UseCompatibleStateImageBehavior = False
        Me.lstRecent.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Name"
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Location"
        '
        'cmdClose
        '
        Me.cmdClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClose.Location = New System.Drawing.Point(548, 372)
        Me.cmdClose.Name = "cmdClose"
        Me.cmdClose.Size = New System.Drawing.Size(75, 23)
        Me.cmdClose.TabIndex = 1
        Me.cmdClose.Text = "Close"
        Me.cmdClose.UseVisualStyleBackColor = True
        '
        'cmdAdd
        '
        Me.cmdAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdAdd.Location = New System.Drawing.Point(467, 372)
        Me.cmdAdd.Name = "cmdAdd"
        Me.cmdAdd.Size = New System.Drawing.Size(75, 23)
        Me.cmdAdd.TabIndex = 1
        Me.cmdAdd.Text = "Add"
        Me.cmdAdd.UseVisualStyleBackColor = True
        '
        'cmdRefreshTab
        '
        Me.cmdRefreshTab.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmdRefreshTab.Location = New System.Drawing.Point(12, 372)
        Me.cmdRefreshTab.Name = "cmdRefreshTab"
        Me.cmdRefreshTab.Size = New System.Drawing.Size(75, 23)
        Me.cmdRefreshTab.TabIndex = 1
        Me.cmdRefreshTab.Text = "Refresh Tab"
        Me.cmdRefreshTab.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(600, 325)
        Me.Panel1.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(259, 154)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Not Available yet."
        '
        'QuickAddReference
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(635, 407)
        Me.Controls.Add(Me.cmdRefreshTab)
        Me.Controls.Add(Me.cmdAdd)
        Me.Controls.Add(Me.cmdClose)
        Me.Controls.Add(Me.Tabs)
        Me.Name = "QuickAddReference"
        Me.Text = "QuickAddReference"
        Me.Tabs.ResumeLayout(False)
        Me.TabSolution.ResumeLayout(False)
        Me.TabRecent.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Tabs As System.Windows.Forms.TabControl
    Friend WithEvents TabSolution As System.Windows.Forms.TabPage
    Friend WithEvents TabRecent As System.Windows.Forms.TabPage
    Friend WithEvents lstSolution As System.Windows.Forms.ListView
    Friend WithEvents cmdClose As System.Windows.Forms.Button
    Friend WithEvents cmdAdd As System.Windows.Forms.Button
    Friend WithEvents ColName As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColLocation As System.Windows.Forms.ColumnHeader
    Friend WithEvents lstRecent As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmdRefreshTab As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
