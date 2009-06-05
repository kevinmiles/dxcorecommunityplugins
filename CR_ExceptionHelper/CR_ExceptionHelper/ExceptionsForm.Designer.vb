<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ExceptionsForm
	Inherits System.Windows.Forms.Form

	'Form overrides dispose to clean up the component list.
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
		Me.components = New System.ComponentModel.Container
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExceptionsForm))
		Me.btnOkay = New System.Windows.Forms.Button
		Me.btnCancel = New System.Windows.Forms.Button
		Me.lbExceptions = New System.Windows.Forms.CheckedListBox
		Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
		Me.DxCoreEvents1 = New DevExpress.DXCore.PlugInCore.DXCoreEvents(Me.components)
		CType(Me.DxCoreEvents1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'btnOkay
		'
		resources.ApplyResources(Me.btnOkay, "btnOkay")
		Me.btnOkay.Name = "btnOkay"
		Me.btnOkay.UseVisualStyleBackColor = True
		'
		'btnCancel
		'
		resources.ApplyResources(Me.btnCancel, "btnCancel")
		Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
		Me.btnCancel.Name = "btnCancel"
		Me.btnCancel.UseVisualStyleBackColor = True
		'
		'lbExceptions
		'
		resources.ApplyResources(Me.lbExceptions, "lbExceptions")
		Me.lbExceptions.Name = "lbExceptions"
		Me.lbExceptions.Sorted = True
		'
		'ToolTip1
		'
		Me.ToolTip1.AutoPopDelay = 8000
		Me.ToolTip1.InitialDelay = 500
		Me.ToolTip1.IsBalloon = True
		Me.ToolTip1.ReshowDelay = 100
		'
		'DxCoreEvents1
		'
		'
		'ExceptionsForm
		'
		Me.AcceptButton = Me.btnOkay
		resources.ApplyResources(Me, "$this")
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.CancelButton = Me.btnCancel
		Me.Controls.Add(Me.lbExceptions)
		Me.Controls.Add(Me.btnCancel)
		Me.Controls.Add(Me.btnOkay)
		Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
		Me.Name = "ExceptionsForm"
		Me.ShowIcon = False
		Me.ShowInTaskbar = False
		CType(Me.DxCoreEvents1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub
	Friend WithEvents btnOkay As System.Windows.Forms.Button
	Friend WithEvents btnCancel As System.Windows.Forms.Button
	Friend WithEvents lbExceptions As System.Windows.Forms.CheckedListBox
	Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
	Friend WithEvents DxCoreEvents1 As DevExpress.DXCore.PlugInCore.DXCoreEvents
End Class
