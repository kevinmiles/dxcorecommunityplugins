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
        Me.CreateDelegate = New DevExpress.CodeRush.Core.CodeProvider(Me.components)
        CType(Me.CreateDelegate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'CreateDelegate
        '
        Me.CreateDelegate.ActionHintText = ""
        Me.CreateDelegate.AutoActivate = True
        Me.CreateDelegate.AutoUndo = False
        Me.CreateDelegate.Description = "Creates A Delegate declaration from aMethod Signature"
        Me.CreateDelegate.DisplayName = "Create Delegate"
        Me.CreateDelegate.Image = CType(resources.GetObject("CreateDelegate.Image"), System.Drawing.Bitmap)
        Me.CreateDelegate.NeedsSelection = False
        Me.CreateDelegate.ProviderName = "CreateDelegate"
        Me.CreateDelegate.Register = True
        Me.CreateDelegate.SupportsAsyncMode = False
        CType(Me.CreateDelegate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents CreateDelegate As DevExpress.CodeRush.Core.CodeProvider

End Class
