Partial Class ImplementBaseConstructors
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
        Me.CODE_ImplementBaseConstructors = New DevExpress.CodeRush.Core.CodeProvider(Me.components)
        CType(Me.CODE_ImplementBaseConstructors, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'CODE_ImplementBaseConstructors
        '
        Me.CODE_ImplementBaseConstructors.ActionHintText = "Implement Base Constructors"
        Me.CODE_ImplementBaseConstructors.AutoActivate = True
        Me.CODE_ImplementBaseConstructors.AutoUndo = True
        Me.CODE_ImplementBaseConstructors.Description = "Implement Base Constructors"
        Me.CODE_ImplementBaseConstructors.DisplayName = "Implement Base Constructors"
        Me.CODE_ImplementBaseConstructors.ProviderName = "ImplementBaseConstructors"
        Me.CODE_ImplementBaseConstructors.Register = True
        CType(Me.CODE_ImplementBaseConstructors, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents CODE_ImplementBaseConstructors As DevExpress.CodeRush.Core.CodeProvider

End Class
