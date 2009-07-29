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
        Me.IssueProvider1 = New DevExpress.CodeRush.Core.IssueProvider(Me.components)
        CType(Me.IssueProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'IssueProvider1
        '
        Me.IssueProvider1.Description = ""
        Me.IssueProvider1.ProviderName = ""
        Me.IssueProvider1.Register = True
        CType(Me.IssueProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents IssueProvider1 As DevExpress.CodeRush.Core.IssueProvider

End Class
