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
        Dim Parameter1 As DevExpress.CodeRush.Core.Parameter = New DevExpress.CodeRush.Core.Parameter(New DevExpress.CodeRush.Core.StringParameterType)
        Dim Parameter2 As DevExpress.CodeRush.Core.Parameter = New DevExpress.CodeRush.Core.Parameter(New DevExpress.CodeRush.Core.StringParameterType)
        Dim Parameter3 As DevExpress.CodeRush.Core.Parameter = New DevExpress.CodeRush.Core.Parameter(New DevExpress.CodeRush.Core.BooleanParameterType)
        Dim Parameter4 As DevExpress.CodeRush.Core.Parameter = New DevExpress.CodeRush.Core.Parameter(New DevExpress.CodeRush.Core.StringParameterType)
        Dim Parameter5 As DevExpress.CodeRush.Core.Parameter = New DevExpress.CodeRush.Core.Parameter(New DevExpress.CodeRush.Core.StringParameterType)
        Me.ExpandTemplateToFile = New DevExpress.CodeRush.Core.TextCommand(Me.components)
        Me.UnusedFilename = New DevExpress.CodeRush.Extensions.StringProvider(Me.components)
        CType(Me.ExpandTemplateToFile, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UnusedFilename, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'ExpandTemplateToFile
        '
        Me.ExpandTemplateToFile.CommandName = "ExpandTemplateToFile"
        Me.ExpandTemplateToFile.Description = "Expands the given template into the passed File (Creates the File if needed)"
        Parameter1.DefaultValue = ""
        Parameter1.Description = "Name of Template"
        Parameter1.Name = "TemplateName"
        Parameter1.Optional = False
        Parameter2.DefaultValue = ""
        Parameter2.Description = "Name of File"
        Parameter2.Name = "Filename"
        Parameter2.Optional = False
        Parameter3.DefaultValue = False
        Parameter3.Description = "ActivateNewFile"
        Parameter3.Name = "ActivateNewFile"
        Parameter3.Optional = True
        Me.ExpandTemplateToFile.Parameters.Add(Parameter1)
        Me.ExpandTemplateToFile.Parameters.Add(Parameter2)
        Me.ExpandTemplateToFile.Parameters.Add(Parameter3)
        '
        'UnusedFilename
        '
        Me.UnusedFilename.Description = ""
        Me.UnusedFilename.DisplayName = "Unused Filename"
        Parameter4.DefaultValue = "Class"
        Parameter4.Description = "FileRoot"
        Parameter4.Name = "FileRoot"
        Parameter4.Optional = False
        Parameter5.DefaultValue = ""
        Parameter5.Description = "FileExt"
        Parameter5.Name = "FileExt"
        Parameter5.Optional = False
        Me.UnusedFilename.Parameters.Add(Parameter4)
        Me.UnusedFilename.Parameters.Add(Parameter5)
        Me.UnusedFilename.ProviderName = "UnusedFilename"
        Me.UnusedFilename.Register = True
        CType(Me.ExpandTemplateToFile, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UnusedFilename, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents ExpandTemplateToFile As DevExpress.CodeRush.Core.TextCommand
    Friend WithEvents UnusedFilename As DevExpress.CodeRush.Extensions.StringProvider

End Class
