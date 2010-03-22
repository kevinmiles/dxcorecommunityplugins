<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class XPO_EasyFields_Options
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
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'XPO_EasyFields_Options
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Name = "XPO_EasyFields_Options"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

	Public Shared ReadOnly Property Storage() As DevExpress.CodeRush.Core.DecoupledStorage
		Get
			Return DevExpress.CodeRush.Core.CodeRush.Options.GetStorage(GetCategory(), GetPageName())
		End Get
	End Property

	Public Overrides ReadOnly Property Category() As String
		Get
			Return XPO_EasyFields_Options.GetCategory()
		End Get
	End Property

	Public Overrides ReadOnly Property PageName() As String
		Get
			Return XPO_EasyFields_Options.GetPageName()
		End Get
	End Property

	Public Shared Shadows Sub Show()
		DevExpress.CodeRush.Core.CodeRush.Command.Execute("Options", FullPath)
	End Sub

	Public Shared ReadOnly Property FullPath() As String
		Get
			Return GetCategory() + "\" + GetPageName()
		End Get
	End Property

End Class
