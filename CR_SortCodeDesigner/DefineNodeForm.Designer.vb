<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DefineNodeForm
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
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtName = New System.Windows.Forms.TextBox
        Me.cboType = New System.Windows.Forms.ComboBox
        Me.cboVisibility = New System.Windows.Forms.ComboBox
        Me.OKButton = New System.Windows.Forms.Button
        Me.CancelButton = New System.Windows.Forms.Button
        Me.chkAbstract = New System.Windows.Forms.CheckBox
        Me.chkExtern = New System.Windows.Forms.CheckBox
        Me.chkOverloads = New System.Windows.Forms.CheckBox
        Me.chkOverrides = New System.Windows.Forms.CheckBox
        Me.chkVitual = New System.Windows.Forms.CheckBox
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 82)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Visibility:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Name:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Type:"
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(79, 47)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(177, 20)
        Me.txtName.TabIndex = 1
        '
        'cboType
        '
        Me.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboType.Location = New System.Drawing.Point(79, 15)
        Me.cboType.Name = "cboType"
        Me.cboType.Size = New System.Drawing.Size(177, 21)
        Me.cboType.TabIndex = 0
        '
        'cboVisibility
        '
        Me.cboVisibility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVisibility.Location = New System.Drawing.Point(79, 79)
        Me.cboVisibility.Name = "cboVisibility"
        Me.cboVisibility.Size = New System.Drawing.Size(177, 21)
        Me.cboVisibility.TabIndex = 2
        '
        'OKButton
        '
        Me.OKButton.Location = New System.Drawing.Point(79, 152)
        Me.OKButton.Name = "OKButton"
        Me.OKButton.Size = New System.Drawing.Size(75, 58)
        Me.OKButton.TabIndex = 8
        Me.OKButton.Text = "OK"
        Me.OKButton.UseVisualStyleBackColor = True
        '
        'CancelButton
        '
        Me.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelButton.Location = New System.Drawing.Point(160, 152)
        Me.CancelButton.Name = "CancelButton"
        Me.CancelButton.Size = New System.Drawing.Size(75, 58)
        Me.CancelButton.TabIndex = 9
        Me.CancelButton.Text = "Cancel"
        Me.CancelButton.UseVisualStyleBackColor = True
        '
        'chkAbstract
        '
        Me.chkAbstract.AutoSize = True
        Me.chkAbstract.Location = New System.Drawing.Point(21, 106)
        Me.chkAbstract.Name = "chkAbstract"
        Me.chkAbstract.Size = New System.Drawing.Size(65, 17)
        Me.chkAbstract.TabIndex = 3
        Me.chkAbstract.Text = "Abstract"
        Me.chkAbstract.UseVisualStyleBackColor = True
        Me.chkAbstract.UseWaitCursor = True
        '
        'chkExtern
        '
        Me.chkExtern.AutoSize = True
        Me.chkExtern.Location = New System.Drawing.Point(116, 106)
        Me.chkExtern.Name = "chkExtern"
        Me.chkExtern.Size = New System.Drawing.Size(56, 17)
        Me.chkExtern.TabIndex = 4
        Me.chkExtern.Text = "Extern"
        Me.chkExtern.UseVisualStyleBackColor = True
        Me.chkExtern.UseWaitCursor = True
        '
        'chkOverloads
        '
        Me.chkOverloads.AutoSize = True
        Me.chkOverloads.Location = New System.Drawing.Point(21, 129)
        Me.chkOverloads.Name = "chkOverloads"
        Me.chkOverloads.Size = New System.Drawing.Size(74, 17)
        Me.chkOverloads.TabIndex = 6
        Me.chkOverloads.Text = "Overloads"
        Me.chkOverloads.UseVisualStyleBackColor = True
        Me.chkOverloads.UseWaitCursor = True
        '
        'chkOverrides
        '
        Me.chkOverrides.AutoSize = True
        Me.chkOverrides.Location = New System.Drawing.Point(116, 129)
        Me.chkOverrides.Name = "chkOverrides"
        Me.chkOverrides.Size = New System.Drawing.Size(66, 17)
        Me.chkOverrides.TabIndex = 7
        Me.chkOverrides.Text = "Override"
        Me.chkOverrides.UseVisualStyleBackColor = True
        Me.chkOverrides.UseWaitCursor = True
        '
        'chkVitual
        '
        Me.chkVitual.AutoSize = True
        Me.chkVitual.Location = New System.Drawing.Point(201, 106)
        Me.chkVitual.Name = "chkVitual"
        Me.chkVitual.Size = New System.Drawing.Size(55, 17)
        Me.chkVitual.TabIndex = 5
        Me.chkVitual.Text = "Virtual"
        Me.chkVitual.UseVisualStyleBackColor = True
        Me.chkVitual.UseWaitCursor = True
        '
        'DefineNodeForm
        '
        Me.AcceptButton = Me.OKButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(280, 216)
        Me.Controls.Add(Me.chkVitual)
        Me.Controls.Add(Me.chkOverrides)
        Me.Controls.Add(Me.chkOverloads)
        Me.Controls.Add(Me.chkExtern)
        Me.Controls.Add(Me.chkAbstract)
        Me.Controls.Add(Me.CancelButton)
        Me.Controls.Add(Me.OKButton)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.cboType)
        Me.Controls.Add(Me.cboVisibility)
        Me.Name = "DefineNodeForm"
        Me.Text = "Add node"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents cboType As System.Windows.Forms.ComboBox
    Friend WithEvents cboVisibility As System.Windows.Forms.ComboBox
    Friend WithEvents OKButton As System.Windows.Forms.Button
    Friend WithEvents CancelButton As System.Windows.Forms.Button
    Friend WithEvents chkAbstract As System.Windows.Forms.CheckBox
    Friend WithEvents chkExtern As System.Windows.Forms.CheckBox
    Friend WithEvents chkOverloads As System.Windows.Forms.CheckBox
    Friend WithEvents chkOverrides As System.Windows.Forms.CheckBox
    Friend WithEvents chkVitual As System.Windows.Forms.CheckBox
End Class
