Partial Class CR_Paste
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
        Me.PasteSmartTag = New DevExpress.CodeRush.Core.SmartTagProvider(Me.components)
        CType(Me.PasteSmartTag, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'PasteSmartTag
        '
        Me.PasteSmartTag.Description = "Paste"
        Me.PasteSmartTag.DisplayName = "Paste"
        Me.PasteSmartTag.MenuOrder = 0
        Me.PasteSmartTag.ProviderName = "Paste"
        Me.PasteSmartTag.Register = True
        Me.PasteSmartTag.ShowInContextMenu = True
        Me.PasteSmartTag.ShowInPopupMenu = False
        CType(Me.PasteSmartTag, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents PasteSmartTag As DevExpress.CodeRush.Core.SmartTagProvider

End Class
