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
        Me.LocalVarsProvider = New DevExpress.CodeRush.Core.SmartTagProvider(Me.components)
        CType(Me.LocalVarsProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'LocalVarsProvider
        '
        Me.LocalVarsProvider.Description = "Local Vars"
        Me.LocalVarsProvider.DisplayName = "Local Vars"
        Me.LocalVarsProvider.Image = CType(resources.GetObject("LocalVarsProvider.Image"), System.Drawing.Bitmap)
        Me.LocalVarsProvider.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LocalVarsProvider.MenuOrder = 0
        Me.LocalVarsProvider.ProviderName = "LocalVars"
        Me.LocalVarsProvider.Register = True
        Me.LocalVarsProvider.ShowInContextMenu = True
        Me.LocalVarsProvider.ShowInPopupMenu = True
        CType(Me.LocalVarsProvider, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents LocalVarsProvider As DevExpress.CodeRush.Core.SmartTagProvider

End Class
