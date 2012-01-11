<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Options1
	Inherits DevExpress.CodeRush.Core.OptionsPage

	<System.Diagnostics.DebuggerNonUserCode()> _
	Public Sub New()
		MyBase.New()

		'This call is required by the Component Designer.
		InitializeComponent()

	End Sub

	'OptionsPage overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso components IsNot Nothing Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Options1))
        Me.optDevExpress = New System.Windows.Forms.RadioButton()
        Me.optDXV2 = New System.Windows.Forms.RadioButton()
        Me.optOther = New System.Windows.Forms.RadioButton()
        Me.txtMenuName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'optDevExpress
        '
        Me.optDevExpress.AutoSize = True
        Me.optDevExpress.Location = New System.Drawing.Point(157, 47)
        Me.optDevExpress.Name = "optDevExpress"
        Me.optDevExpress.Size = New System.Drawing.Size(132, 17)
        Me.optDevExpress.TabIndex = 0
        Me.optDevExpress.Text = "Use DevExpress menu"
        Me.optDevExpress.UseVisualStyleBackColor = True
        '
        'optDXV2
        '
        Me.optDXV2.AutoSize = True
        Me.optDXV2.Checked = True
        Me.optDXV2.Location = New System.Drawing.Point(157, 70)
        Me.optDXV2.Name = "optDXV2"
        Me.optDXV2.Size = New System.Drawing.Size(115, 17)
        Me.optDXV2.TabIndex = 0
        Me.optDXV2.TabStop = True
        Me.optDXV2.Text = "Create DXV2 menu"
        Me.optDXV2.UseVisualStyleBackColor = True
        '
        'optOther
        '
        Me.optOther.AutoSize = True
        Me.optOther.Location = New System.Drawing.Point(157, 93)
        Me.optOther.Name = "optOther"
        Me.optOther.Size = New System.Drawing.Size(65, 17)
        Me.optOther.TabIndex = 0
        Me.optOther.TabStop = True
        Me.optOther.Text = "Other..."
        Me.optOther.UseVisualStyleBackColor = True
        '
        'txtMenuName
        '
        Me.txtMenuName.Location = New System.Drawing.Point(206, 116)
        Me.txtMenuName.Name = "txtMenuName"
        Me.txtMenuName.Size = New System.Drawing.Size(100, 20)
        Me.txtMenuName.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(41, 145)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(423, 41)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = resources.GetString("Label1.Text")
        '
        'Options1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtMenuName)
        Me.Controls.Add(Me.optOther)
        Me.Controls.Add(Me.optDXV2)
        Me.Controls.Add(Me.optDevExpress)
        Me.Name = "Options1"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

	Public Shared ReadOnly Property Storage() As DevExpress.CodeRush.Core.DecoupledStorage
		Get
			Return DevExpress.CodeRush.Core.CodeRush.Options.GetStorage(GetCategory(), GetPageName())
		End Get
	End Property

	Public Overrides ReadOnly Property Category() As String
		Get
			Return Options1.GetCategory()
		End Get
	End Property

	Public Overrides ReadOnly Property PageName() As String
		Get
			Return Options1.GetPageName()
		End Get
	End Property

	Public Shared Shadows Sub Show()
		DevExpress.CodeRush.Core.CodeRush.Command.Execute("Options", FullPath)
    End Sub
    Friend WithEvents optDevExpress As System.Windows.Forms.RadioButton
    Friend WithEvents optDXV2 As System.Windows.Forms.RadioButton
    Friend WithEvents optOther As System.Windows.Forms.RadioButton
    Friend WithEvents txtMenuName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label

	Public Shared ReadOnly Property FullPath() As String
		Get
			Return GetCategory() + "\" + GetPageName()
		End Get
	End Property

End Class
