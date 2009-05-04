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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PlugIn1))
        Me.AddEventHandler = New DevExpress.CodeRush.Core.CodeProvider(Me.components)
        CType(Me.AddEventHandler, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'AddEventHandler
        '
        Me.AddEventHandler.ActionHintText = ""
        Me.AddEventHandler.AutoActivate = True
        Me.AddEventHandler.AutoUndo = False
        Me.AddEventHandler.Description = "AddEventHandler"
        Me.AddEventHandler.DisplayName = "Add Event Handler"
        Me.AddEventHandler.Image = CType(resources.GetObject("AddEventHandler.Image"), System.Drawing.Bitmap)
        Me.AddEventHandler.NeedsSelection = False
        Me.AddEventHandler.ProviderName = "AddEventHandler"
        Me.AddEventHandler.Register = True
        Me.AddEventHandler.SupportsAsyncMode = False
        CType(Me.AddEventHandler, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents AddEventHandler As DevExpress.CodeRush.Core.CodeProvider

End Class
