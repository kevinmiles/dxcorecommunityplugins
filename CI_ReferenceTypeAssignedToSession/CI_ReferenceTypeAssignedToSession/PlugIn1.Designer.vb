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
        Me.ReferenceAssignedToSession = New DevExpress.CodeRush.Core.IssueProvider(Me.components)
        CType(Me.ReferenceAssignedToSession, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'ReferenceAssignedToSession
        '
        Me.ReferenceAssignedToSession.Description = "Reference Type Assigned to Session"
        Me.ReferenceAssignedToSession.DisplayName = "Reference Type Assigned to Session"
        Me.ReferenceAssignedToSession.ProviderName = "ReferenceTypeAssignedToSession"
        Me.ReferenceAssignedToSession.Register = True
        CType(Me.ReferenceAssignedToSession, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents ReferenceAssignedToSession As DevExpress.CodeRush.Core.IssueProvider

End Class