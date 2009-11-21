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
        Dim InsertionPoint2 As DevExpress.CodeRush.Core.InsertionPoint = New DevExpress.CodeRush.Core.InsertionPoint
        Me.cmdMoveCodeUp = New DevExpress.CodeRush.Core.Action(Me.components)
        Me.cmdMoveCodeDown = New DevExpress.CodeRush.Core.Action(Me.components)
        Me.cmdMoveCodeLeft = New DevExpress.CodeRush.Core.Action(Me.components)
        Me.cmdMoveCodeRight = New DevExpress.CodeRush.Core.Action(Me.components)
        Me.TargetPicker1 = New DevExpress.CodeRush.PlugInCore.TargetPicker(Me.components)
        CType(Me.cmdMoveCodeUp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdMoveCodeDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdMoveCodeLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdMoveCodeRight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TargetPicker1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        '
        'cmdMoveCodeDown
        '
        Me.cmdMoveCodeDown.ActionName = "MoveCodeDown"
        Me.cmdMoveCodeDown.ButtonText = "Move Code Down"
        Me.cmdMoveCodeDown.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
        Me.cmdMoveCodeDown.Description = "Moves Code Down"
        Me.cmdMoveCodeDown.Image = CType(resources.GetObject("cmdMoveCodeDown.Image"), System.Drawing.Bitmap)
        Me.cmdMoveCodeDown.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))
        '
        'cmdMoveCodeLeft
        '
        Me.cmdMoveCodeLeft.ActionName = "MoveCodeLeft"
        Me.cmdMoveCodeLeft.ButtonText = "Move Code Left"
        Me.cmdMoveCodeLeft.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
        Me.cmdMoveCodeLeft.Description = "Moves Code Left"
        Me.cmdMoveCodeLeft.Image = CType(resources.GetObject("cmdMoveCodeLeft.Image"), System.Drawing.Bitmap)
        Me.cmdMoveCodeLeft.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))
        '
        'cmdMoveCodeRight
        '
        Me.cmdMoveCodeRight.ActionName = "MoveCodeRight"
        Me.cmdMoveCodeRight.ButtonText = "Move Code Right"
        Me.cmdMoveCodeRight.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
        Me.cmdMoveCodeRight.Description = "Moves Code Right"
        Me.cmdMoveCodeRight.Image = CType(resources.GetObject("cmdMoveCodeRight.Image"), System.Drawing.Bitmap)
        Me.cmdMoveCodeRight.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))
        '
        'TargetPicker1
        '
        Me.TargetPicker1.BigHint = Nothing
        Me.TargetPicker1.Code = Nothing
        InsertionPoint2.ArrowFillColor = System.Drawing.Color.Red
        InsertionPoint2.ArrowFillOpacity = 30
        InsertionPoint2.ArrowLineColor = System.Drawing.Color.Red
        InsertionPoint2.LineColor = System.Drawing.Color.Red
        InsertionPoint2.LineOpacity = 200
        Me.TargetPicker1.InsertionPoint = InsertionPoint2
        Me.TargetPicker1.IsModalMode = False
        Me.TargetPicker1.ShortcutsHint = Nothing
        CType(Me.cmdMoveCodeUp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdMoveCodeDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdMoveCodeLeft, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdMoveCodeRight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TargetPicker1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents cmdMoveCodeUp As DevExpress.CodeRush.Core.Action
    Friend WithEvents cmdMoveCodeDown As DevExpress.CodeRush.Core.Action
    Friend WithEvents cmdMoveCodeLeft As DevExpress.CodeRush.Core.Action
    Friend WithEvents cmdMoveCodeRight As DevExpress.CodeRush.Core.Action
    Friend WithEvents TargetPicker1 As DevExpress.CodeRush.PlugInCore.TargetPicker

End Class
