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
        Me.DeclareStructInProject = New DevExpress.CodeRush.Core.CodeProvider(Me.components)
        Me.DeclareInterfaceInProject = New DevExpress.CodeRush.Core.CodeProvider(Me.components)
        CType(Me.DeclareClassInProject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DeclareStructInProject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DeclareInterfaceInProject, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'DeclareClassInProject
        '
        Me.DeclareClassInProject.ActionHintText = ""
        Me.DeclareClassInProject.AutoActivate = True
        Me.DeclareClassInProject.AutoUndo = False
        Me.DeclareClassInProject.CodeIssueMessage = Nothing
        Me.DeclareClassInProject.Description = "Declares a class in a specified project"
        Me.DeclareClassInProject.DisplayName = "Declare Class In Project"
        Me.DeclareClassInProject.Image = CType(resources.GetObject("DeclareClassInProject.Image"), System.Drawing.Bitmap)
        Me.DeclareClassInProject.NeedsSelection = False
        Me.DeclareClassInProject.ProviderName = "DeclareClassInProject"
        Me.DeclareClassInProject.Register = True
        Me.DeclareClassInProject.SupportsAsyncMode = False
        '
        'DeclareStructInProject
        '
        Me.DeclareStructInProject.ActionHintText = ""
        Me.DeclareStructInProject.AutoActivate = True
        Me.DeclareStructInProject.AutoUndo = False
        Me.DeclareStructInProject.CodeIssueMessage = Nothing
        Me.DeclareStructInProject.Description = "Declare Structure in Project"
        Me.DeclareStructInProject.DisplayName = "Declare Structure in Project"
        Me.DeclareStructInProject.Image = CType(resources.GetObject("DeclareStructInProject.Image"), System.Drawing.Bitmap)
        Me.DeclareStructInProject.NeedsSelection = False
        Me.DeclareStructInProject.ProviderName = "DeclareStructInProject"
        Me.DeclareStructInProject.Register = True
        Me.DeclareStructInProject.SupportsAsyncMode = False
        '
        'DeclareInterfaceInProject
        '
        Me.DeclareInterfaceInProject.ActionHintText = ""
        Me.DeclareInterfaceInProject.AutoActivate = True
        Me.DeclareInterfaceInProject.AutoUndo = False
        Me.DeclareInterfaceInProject.CodeIssueMessage = Nothing
        Me.DeclareInterfaceInProject.Description = "Declare Interface in Project"
        Me.DeclareInterfaceInProject.DisplayName = "Declare Interface in Project"
        Me.DeclareInterfaceInProject.Image = CType(resources.GetObject("DeclareInterfaceInProject.Image"), System.Drawing.Bitmap)
        Me.DeclareInterfaceInProject.NeedsSelection = False
        Me.DeclareInterfaceInProject.ProviderName = "DeclareInterfaceInProject"
        Me.DeclareInterfaceInProject.Register = True
        Me.DeclareInterfaceInProject.SupportsAsyncMode = False
        CType(Me.DeclareClassInProject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DeclareStructInProject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DeclareInterfaceInProject, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents DeclareClassInProject As DevExpress.CodeRush.Core.CodeProvider
    Friend WithEvents DeclareStructInProject As DevExpress.CodeRush.Core.CodeProvider
    Friend WithEvents DeclareInterfaceInProject As DevExpress.CodeRush.Core.CodeProvider

End Class
