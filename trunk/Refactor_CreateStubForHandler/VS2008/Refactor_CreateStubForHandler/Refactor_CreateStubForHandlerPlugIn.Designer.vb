Partial Class Refactor_CreateStubForHandlerPlugIn
	Inherits DevExpress.CodeRush.PlugInCore.StandardPlugIn

	<System.Diagnostics.DebuggerNonUserCode()> _
	Public Sub New()
		MyBase.New()

		'This call is required by the Component Designer.
		InitializeComponent()

	End Sub

	'StandardPlugIn overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
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
	<System.Diagnostics.DebuggerStepThrough()> _
  	Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Refactor_CreateStubForHandlerPlugIn))
        Dim InsertionPoint1 As DevExpress.CodeRush.Core.InsertionPoint = New DevExpress.CodeRush.Core.InsertionPoint
        Me.CreateHandlerStubAction = New DevExpress.CodeRush.Core.Action(Me.components)
        Me.CreateHandlerStubCodeProvider = New DevExpress.CodeRush.Core.CodeProvider(Me.components)
        Me.Picker = New DevExpress.CodeRush.PlugInCore.TargetPicker(Me.components)
        CType(Me.CreateHandlerStubAction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CreateHandlerStubCodeProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Picker, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'CreateHandlerStubAction
        '
        Me.CreateHandlerStubAction.ActionName = "AddHandler completion"
        Me.CreateHandlerStubAction.ButtonText = "AddHandler completion"
        Me.CreateHandlerStubAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
        Me.CreateHandlerStubAction.Description = "Complete the addhandler statement"
        Me.CreateHandlerStubAction.Image = CType(resources.GetObject("CreateHandlerStubAction.Image"), System.Drawing.Bitmap)
        Me.CreateHandlerStubAction.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))
        '
        'CreateHandlerStubCodeProvider
        '
        Me.CreateHandlerStubCodeProvider.ActionHintText = ""
        Me.CreateHandlerStubCodeProvider.AutoActivate = True
        Me.CreateHandlerStubCodeProvider.AutoUndo = False
        Me.CreateHandlerStubCodeProvider.Description = "Create a Stub method for this handler"
        Me.CreateHandlerStubCodeProvider.DisplayName = "Create Handler Stub"
        Me.CreateHandlerStubCodeProvider.ProviderName = "Create Handler Stub"
        Me.CreateHandlerStubCodeProvider.Register = True
        '
        'Picker
        '
        Me.Picker.BigHint = Nothing
        InsertionPoint1.ArrowFillColor = System.Drawing.Color.Red
        InsertionPoint1.ArrowFillOpacity = 30
        InsertionPoint1.ArrowLineColor = System.Drawing.Color.Red
        InsertionPoint1.LineColor = System.Drawing.Color.Red
        InsertionPoint1.LineOpacity = 200
        Me.Picker.InsertionPoint = InsertionPoint1
        Me.Picker.IsModalMode = False
        Me.Picker.ShortcutsHint = Nothing
        CType(Me.CreateHandlerStubAction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CreateHandlerStubCodeProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Picker, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents CreateHandlerStubAction As DevExpress.CodeRush.Core.Action
    Friend WithEvents CreateHandlerStubCodeProvider As DevExpress.CodeRush.Core.CodeProvider
    Friend WithEvents Picker As DevExpress.CodeRush.PlugInCore.TargetPicker

End Class
