Partial Class PlugIn1
	Inherits DevExpress.CodeRush.PlugInCore.StandardPlugIn

	<System.Diagnostics.DebuggerNonUserCode()> _
	Public Sub New()
		MyBase.New()

		'This call is required by the Component Designer.
		InitializeComponent()

	End Sub

	'Component overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PlugIn1))
        Me.GenerateTest = New DevExpress.CodeRush.Core.CodeProvider(Me.components)
        Me.Action1 = New DevExpress.CodeRush.Core.Action(Me.components)
        CType(Me.GenerateTest, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Action1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'GenerateTest
        '
        Me.GenerateTest.ActionHintText = ""
        Me.GenerateTest.AutoActivate = True
        Me.GenerateTest.AutoUndo = False
        Me.GenerateTest.CodeIssueMessage = Nothing
        Me.GenerateTest.Description = ""
        Me.GenerateTest.DisplayName = "Generate Test"
        Me.GenerateTest.ExclusiveAvailabilityBehavior = DevExpress.CodeRush.Core.ExclusiveAvailabilityBehavior.ShowMenu
        Me.GenerateTest.Image = CType(resources.GetObject("GenerateTest.Image"), System.Drawing.Bitmap)
        Me.GenerateTest.NeedsSelection = False
        Me.GenerateTest.ProviderName = "GenerateTest"
        Me.GenerateTest.Register = True
        Me.GenerateTest.SupportsAsyncMode = False
        '
        'Action1
        '
        Me.Action1.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
        Me.Action1.Image = CType(resources.GetObject("Action1.Image"), System.Drawing.Bitmap)
        Me.Action1.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Action1.ToolbarItem.ButtonIsPressed = False
        Me.Action1.ToolbarItem.Caption = Nothing
        Me.Action1.ToolbarItem.Image = Nothing
        CType(Me.GenerateTest, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Action1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents GenerateTest As DevExpress.CodeRush.Core.CodeProvider
    Friend WithEvents Action1 As DevExpress.CodeRush.Core.Action

End Class
