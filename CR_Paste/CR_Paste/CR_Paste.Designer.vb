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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CR_Paste))
        Me.PasteSmartTag = New DevExpress.CodeRush.Core.SmartTagProvider(Me.components)
        Me.PasteAction = New DevExpress.CodeRush.Core.Action(Me.components)
        Me.RefactoringProvider1 = New DevExpress.Refactor.Core.RefactoringProvider(Me.components)
        CType(Me.PasteSmartTag, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PasteAction, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RefactoringProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        '
        'PasteAction
        '
        Me.PasteAction.ActionName = "PasteAction"
        Me.PasteAction.ButtonText = "Paste"
        Me.PasteAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
        Me.PasteAction.Image = CType(resources.GetObject("PasteAction.Image"), System.Drawing.Bitmap)
        Me.PasteAction.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))
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
        CType(Me.PasteSmartTag, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PasteAction, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RefactoringProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents PasteSmartTag As DevExpress.CodeRush.Core.SmartTagProvider
    Friend WithEvents PasteAction As DevExpress.CodeRush.Core.Action
    Friend WithEvents RefactoringProvider1 As DevExpress.Refactor.Core.RefactoringProvider

End Class
