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
        Me.actQuickAddReference = New DevExpress.CodeRush.Core.Action(Me.components)
        CType(Me.actQuickAddReference, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'actQuickAddReference
        '
        Me.actQuickAddReference.ActionName = "QuickAddReference"
        Me.actQuickAddReference.ButtonText = "Quick Add Reference"
        Me.actQuickAddReference.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.EditorContext
        Me.actQuickAddReference.Description = "Launches the 'Quick Add Reference' Dialog... Like the 'Add Reference' dialog only" & _
            " Quicker :)"
        Me.actQuickAddReference.Image = CType(resources.GetObject("actQuickAddReference.Image"), System.Drawing.Bitmap)
        Me.actQuickAddReference.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))
        CType(Me.actQuickAddReference, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents actQuickAddReference As DevExpress.CodeRush.Core.Action

End Class
