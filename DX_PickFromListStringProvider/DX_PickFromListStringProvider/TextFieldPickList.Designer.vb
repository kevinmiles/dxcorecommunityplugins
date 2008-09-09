Partial Class TextFieldPickList
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
        Dim Parameter4 As DevExpress.CodeRush.Core.Parameter = New DevExpress.CodeRush.Core.Parameter(New DevExpress.CodeRush.Core.StringParameterType)
        Dim Parameter5 As DevExpress.CodeRush.Core.Parameter = New DevExpress.CodeRush.Core.Parameter(New DevExpress.CodeRush.Core.StringParameterType)
        Dim Parameter6 As DevExpress.CodeRush.Core.Parameter = New DevExpress.CodeRush.Core.Parameter(New DevExpress.CodeRush.Core.StringParameterType)
        Me.FieldPickList = New DevExpress.CodeRush.Core.TextCommand(Me.components)
        Me.RefactoringProvider1 = New DevExpress.Refactor.Core.RefactoringProvider(Me.components)
        Me.CodeProvider1 = New DevExpress.CodeRush.Core.CodeProvider(Me.components)
        CType(Me.FieldPickList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RefactoringProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CodeProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'FieldPickList
        '
        Me.FieldPickList.CommandName = "FieldPickList"
        Me.FieldPickList.Description = "FieldPickList"
        Parameter4.DefaultValue = ""
        Parameter4.Description = Nothing
        Parameter4.Name = "text"
        Parameter4.Optional = False
        Parameter5.DefaultValue = ""
        Parameter5.Description = Nothing
        Parameter5.Name = "description"
        Parameter5.Optional = False
        Parameter6.DefaultValue = ""
        Parameter6.Description = Nothing
        Parameter6.Name = "items"
        Parameter6.Optional = False
        Me.FieldPickList.Parameters.Add(Parameter4)
        Me.FieldPickList.Parameters.Add(Parameter5)
        Me.FieldPickList.Parameters.Add(Parameter6)
        '
        'RefactoringProvider1
        '
        Me.RefactoringProvider1.ActionHintText = ""
        Me.RefactoringProvider1.AutoActivate = True
        Me.RefactoringProvider1.AutoUndo = False
        Me.RefactoringProvider1.Description = ""
        Me.RefactoringProvider1.DisplayName = ""
        Me.RefactoringProvider1.ProviderName = ""
        Me.RefactoringProvider1.Register = True
		'
        'CodeProvider1
        '
        Me.CodeProvider1.ActionHintText = ""
        Me.CodeProvider1.AutoActivate = True
        Me.CodeProvider1.AutoUndo = False
        Me.CodeProvider1.Description = ""
        Me.CodeProvider1.DisplayName = ""
        Me.CodeProvider1.ProviderName = ""
        Me.CodeProvider1.Register = True
		CType(Me.FieldPickList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RefactoringProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CodeProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents FieldPickList As DevExpress.CodeRush.Core.TextCommand
    Friend WithEvents RefactoringProvider1 As DevExpress.Refactor.Core.RefactoringProvider
    Friend WithEvents CodeProvider1 As DevExpress.CodeRush.Core.CodeProvider

End Class
