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
        Me.DeclareClassInProject = New DevExpress.CodeRush.Core.CodeProvider(Me.components)
        CType(Me.DeclareClassInProject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'DeclareClassInProject
        '
        Me.DeclareClassInProject.ActionHintText = ""
        Me.DeclareClassInProject.AutoActivate = True
        Me.DeclareClassInProject.AutoUndo = False
        Me.DeclareClassInProject.Description = "Declares a class in a specified project"
        Me.DeclareClassInProject.DisplayName = "Declare Class In Project"
        Me.DeclareClassInProject.Image = CType(resources.GetObject("DeclareClassInProject.Image"), System.Drawing.Bitmap)
        Me.DeclareClassInProject.NeedsSelection = False
        Me.DeclareClassInProject.ProviderName = "DeclareClassInProject"
        Me.DeclareClassInProject.Register = True
        Me.DeclareClassInProject.SupportsAsyncMode = False
        CType(Me.DeclareClassInProject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents DeclareClassInProject As DevExpress.CodeRush.Core.CodeProvider

End Class
