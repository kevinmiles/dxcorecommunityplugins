<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionsCreateStubForHandler
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
        Me.chkAllowuserToModifyDefaultLocationWithPicker = New System.Windows.Forms.CheckBox
        Me.optBeforeSourceMethod = New System.Windows.Forms.RadioButton
        Me.optAfterSourceMethod = New System.Windows.Forms.RadioButton
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'chkAllowuserToModifyDefaultLocationWithPicker
        '
        Me.chkAllowuserToModifyDefaultLocationWithPicker.AutoSize = True
        Me.chkAllowuserToModifyDefaultLocationWithPicker.Location = New System.Drawing.Point(48, 94)
        Me.chkAllowuserToModifyDefaultLocationWithPicker.Name = "chkAllowuserToModifyDefaultLocationWithPicker"
        Me.chkAllowuserToModifyDefaultLocationWithPicker.Size = New System.Drawing.Size(163, 17)
        Me.chkAllowuserToModifyDefaultLocationWithPicker.TabIndex = 0
        Me.chkAllowuserToModifyDefaultLocationWithPicker.Text = "Allow user to position handler"
        Me.chkAllowuserToModifyDefaultLocationWithPicker.UseVisualStyleBackColor = True
        '
        'optBeforeSourceMethod
        '
        Me.optBeforeSourceMethod.AutoSize = True
        Me.optBeforeSourceMethod.Location = New System.Drawing.Point(48, 48)
        Me.optBeforeSourceMethod.Name = "optBeforeSourceMethod"
        Me.optBeforeSourceMethod.Size = New System.Drawing.Size(211, 17)
        Me.optBeforeSourceMethod.TabIndex = 1
        Me.optBeforeSourceMethod.TabStop = True
        Me.optBeforeSourceMethod.Text = "Position handler 'Above' source method"
        Me.optBeforeSourceMethod.UseVisualStyleBackColor = True
        '
        'optAfterSourceMethod
        '
        Me.optAfterSourceMethod.AutoSize = True
        Me.optAfterSourceMethod.Checked = True
        Me.optAfterSourceMethod.Location = New System.Drawing.Point(48, 71)
        Me.optAfterSourceMethod.Name = "optAfterSourceMethod"
        Me.optAfterSourceMethod.Size = New System.Drawing.Size(209, 17)
        Me.optAfterSourceMethod.TabIndex = 1
        Me.optAfterSourceMethod.TabStop = True
        Me.optAfterSourceMethod.Text = "Position handler 'Below' source method"
        Me.optAfterSourceMethod.UseVisualStyleBackColor = True
        '
        'OptionsCreateStubForHandler
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.optAfterSourceMethod)
        Me.Controls.Add(Me.optBeforeSourceMethod)
        Me.Controls.Add(Me.chkAllowuserToModifyDefaultLocationWithPicker)
        Me.Name = "OptionsCreateStubForHandler"
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
            Return OptionsCreateStubForHandler.GetCategory()
        End Get
    End Property

    Public Overrides ReadOnly Property PageName() As String
        Get
            Return OptionsCreateStubForHandler.GetPageName()
        End Get
    End Property

    Public Shared Shadows Sub Show()
        DevExpress.CodeRush.Core.CodeRush.Command.Execute("Options", FullPath)
    End Sub
    Friend WithEvents chkAllowuserToModifyDefaultLocationWithPicker As System.Windows.Forms.CheckBox
    Friend WithEvents optBeforeSourceMethod As System.Windows.Forms.RadioButton
    Friend WithEvents optAfterSourceMethod As System.Windows.Forms.RadioButton

	Public Shared ReadOnly Property FullPath() As String
		Get
			Return GetCategory() + "\" + GetPageName()
		End Get
	End Property

End Class
