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
        Dim InsertionPoint1 As DevExpress.CodeRush.Core.InsertionPoint = New DevExpress.CodeRush.Core.InsertionPoint
        Me.cmdMoveCodeUp = New DevExpress.CodeRush.Core.Action(Me.components)
        Me.cmdMoveCodeDown = New DevExpress.CodeRush.Core.Action(Me.components)
        Me.cmdMoveCodeLeft = New DevExpress.CodeRush.Core.Action(Me.components)
        Me.cmdMoveCodeRight = New DevExpress.CodeRush.Core.Action(Me.components)
        Me.TargetPicker1 = New DevExpress.CodeRush.PlugInCore.TargetPicker(Me.components)
        Me.cmdMoveCaretUp = New DevExpress.CodeRush.Core.Action(Me.components)
        Me.cmdMoveCaretDown = New DevExpress.CodeRush.Core.Action(Me.components)
        Me.cmdMoveCaretLeft = New DevExpress.CodeRush.Core.Action(Me.components)
        Me.cmdMoveCaretRight = New DevExpress.CodeRush.Core.Action(Me.components)
        CType(Me.cmdMoveCodeUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdMoveCodeDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdMoveCodeLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdMoveCodeRight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TargetPicker1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdMoveCaretUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdMoveCaretDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdMoveCaretLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdMoveCaretRight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'cmdMoveCodeUp
        '
        Me.cmdMoveCodeUp.ActionName = "MoveCodeUp"
        Me.cmdMoveCodeUp.ButtonText = "Move Code Up"
        Me.cmdMoveCodeUp.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
        Me.cmdMoveCodeUp.Description = "Moves Code Up"
        Me.cmdMoveCodeUp.Image = CType(resources.GetObject("cmdMoveCodeUp.Image"), System.Drawing.Bitmap)
        Me.cmdMoveCodeUp.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdMoveCodeUp.RegisterInVS = True
        '
        'cmdMoveCodeDown
        '
        Me.cmdMoveCodeDown.ActionName = "MoveCodeDown"
        Me.cmdMoveCodeDown.ButtonText = "Move Code Down"
        Me.cmdMoveCodeDown.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
        Me.cmdMoveCodeDown.Description = "Moves Code Down"
        Me.cmdMoveCodeDown.Image = CType(resources.GetObject("cmdMoveCodeDown.Image"), System.Drawing.Bitmap)
        Me.cmdMoveCodeDown.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdMoveCodeDown.RegisterInVS = True
        '
        'cmdMoveCodeLeft
        '
        Me.cmdMoveCodeLeft.ActionName = "MoveCodeLeft"
        Me.cmdMoveCodeLeft.ButtonText = "Move Code Left"
        Me.cmdMoveCodeLeft.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
        Me.cmdMoveCodeLeft.Description = "Moves Code Left"
        Me.cmdMoveCodeLeft.Image = CType(resources.GetObject("cmdMoveCodeLeft.Image"), System.Drawing.Bitmap)
        Me.cmdMoveCodeLeft.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdMoveCodeLeft.RegisterInVS = True
        '
        'cmdMoveCodeRight
        '
        Me.cmdMoveCodeRight.ActionName = "MoveCodeRight"
        Me.cmdMoveCodeRight.ButtonText = "Move Code Right"
        Me.cmdMoveCodeRight.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
        Me.cmdMoveCodeRight.Description = "Moves Code Right"
        Me.cmdMoveCodeRight.Image = CType(resources.GetObject("cmdMoveCodeRight.Image"), System.Drawing.Bitmap)
        Me.cmdMoveCodeRight.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.cmdMoveCodeRight.RegisterInVS = True
        '
        'TargetPicker1
        '
        Me.TargetPicker1.BigHint = Nothing
        Me.TargetPicker1.Code = Nothing
        InsertionPoint1.ArrowFillColor = System.Drawing.Color.Red
        InsertionPoint1.ArrowFillOpacity = 30
        InsertionPoint1.ArrowLineColor = System.Drawing.Color.Red
        InsertionPoint1.LineColor = System.Drawing.Color.Red
        InsertionPoint1.LineOpacity = 200
        Me.TargetPicker1.InsertionPoint = InsertionPoint1
        Me.TargetPicker1.IsModalMode = False
        Me.TargetPicker1.ShortcutsHint = Nothing
        '
        'cmdMoveCaretUp
        '
        Me.cmdMoveCaretUp.ActionName = "MoveCaretUp"
        Me.cmdMoveCaretUp.ButtonText = "Move Caret Up"
        Me.cmdMoveCaretUp.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
        Me.cmdMoveCaretUp.Description = "Moves Caret Up"
        Me.cmdMoveCaretUp.Image = CType(resources.GetObject("cmdMoveCaretUp.Image"), System.Drawing.Bitmap)
        Me.cmdMoveCaretUp.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))
        '
        'cmdMoveCaretDown
        '
        Me.cmdMoveCaretDown.ActionName = "MoveCaretDown"
        Me.cmdMoveCaretDown.ButtonText = "Move Caret Down"
        Me.cmdMoveCaretDown.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
        Me.cmdMoveCaretDown.Description = "Movess Caret Down"
        Me.cmdMoveCaretDown.Image = CType(resources.GetObject("cmdMoveCaretDown.Image"), System.Drawing.Bitmap)
        Me.cmdMoveCaretDown.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))
        '
        'cmdMoveCaretLeft
        '
        Me.cmdMoveCaretLeft.ActionName = "MoveCaretLeft"
        Me.cmdMoveCaretLeft.ButtonText = "Move Caret Left"
        Me.cmdMoveCaretLeft.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
        Me.cmdMoveCaretLeft.Description = "Moves Caret Left"
        Me.cmdMoveCaretLeft.Image = CType(resources.GetObject("cmdMoveCaretLeft.Image"), System.Drawing.Bitmap)
        Me.cmdMoveCaretLeft.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))
        '
        'cmdMoveCaretRight
        '
        Me.cmdMoveCaretRight.ActionName = "MoveCaretRight"
        Me.cmdMoveCaretRight.ButtonText = "Move Caret Right"
        Me.cmdMoveCaretRight.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
        Me.cmdMoveCaretRight.Description = "Moves Caret Right"
        Me.cmdMoveCaretRight.Image = CType(resources.GetObject("cmdMoveCaretRight.Image"), System.Drawing.Bitmap)
        Me.cmdMoveCaretRight.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))
        '
        'PlugIn1
        '
        CType(Me.cmdMoveCodeUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdMoveCodeDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdMoveCodeLeft, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdMoveCodeRight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TargetPicker1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdMoveCaretUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdMoveCaretDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdMoveCaretLeft, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdMoveCaretRight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents cmdMoveCodeUp As DevExpress.CodeRush.Core.Action
    Friend WithEvents cmdMoveCodeDown As DevExpress.CodeRush.Core.Action
    Friend WithEvents cmdMoveCodeLeft As DevExpress.CodeRush.Core.Action
    Friend WithEvents cmdMoveCodeRight As DevExpress.CodeRush.Core.Action
    Friend WithEvents TargetPicker1 As DevExpress.CodeRush.PlugInCore.TargetPicker
    Friend WithEvents cmdMoveCaretUp As DevExpress.CodeRush.Core.Action
    Friend WithEvents cmdMoveCaretDown As DevExpress.CodeRush.Core.Action
    Friend WithEvents cmdMoveCaretLeft As DevExpress.CodeRush.Core.Action
    Friend WithEvents cmdMoveCaretRight As DevExpress.CodeRush.Core.Action

End Class
