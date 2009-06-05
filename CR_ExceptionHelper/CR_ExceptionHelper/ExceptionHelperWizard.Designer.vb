Partial Class ExceptionHelperWizard
	Inherits DevExpress.CodeRush.PlugInCore.StandardPlugIn

	<System.Diagnostics.DebuggerNonUserCode()> _
	Public Sub New()
		MyBase.New()

		'This call is required by the Component Designer.
		InitializeComponent()

	End Sub

	'Component overrides dispose to clean up the component list.
	<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId:="m_frmExceptions")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2215:DisposeMethodsShouldCallBaseClassDispose")> <System.Diagnostics.DebuggerNonUserCode()> _
 Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso components IsNot Nothing Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	'Required by the Component Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Component Designer
	'It can be modified using the Component Designer.
	'Do not modify it using the code editor.
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ExceptionHelperWizard))
		Me.Action1 = New DevExpress.CodeRush.Core.Action(Me.components)
        Me.HandlersGenerator1 = New TryCatchGenerator(Me.components)
		CType(Me.Action1, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
		'
		'Action1
		'
		Me.Action1.ActionName = "Add Exception Handlers"
		Me.Action1.ButtonText = "Add Exception Handlers"
		Me.Action1.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.EditorContext
		Me.Action1.Description = "Add Exception Handlers"
		Me.Action1.Image = CType(resources.GetObject("Action1.Image"), System.Drawing.Bitmap)
		Me.Action1.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))

		'
		'ExceptionHelperWizard
		'
		CType(Me.Action1, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

	End Sub
	Friend WithEvents Action1 As DevExpress.CodeRush.Core.Action
    Friend WithEvents HandlersGenerator1 As TryCatchGenerator

End Class
