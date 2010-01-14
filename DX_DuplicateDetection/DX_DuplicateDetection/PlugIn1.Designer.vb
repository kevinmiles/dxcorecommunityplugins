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
        Me.UniquenessHash = New DevExpress.CodeRush.Extensions.CodeMetricProvider(Me.components)
        Me.FindDuplicateMethods = New DevExpress.CodeRush.Core.Action(Me.components)
        CType(Me.UniquenessHash, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FindDuplicateMethods, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'UniquenessHash
        '
        Me.UniquenessHash.CalculateOption = DevExpress.CodeRush.Core.CalculateOption.AfterParse
        Me.UniquenessHash.Description = "UniquenessHash"
        Me.UniquenessHash.MetricGoal = DevExpress.CodeRush.Core.MetricGoal.Members
        Me.UniquenessHash.ProviderName = "UniquenessHash"
        Me.UniquenessHash.Register = True
        Me.UniquenessHash.WarningValue = 0
        '
        'FindDuplicateMethods
        '
        Me.FindDuplicateMethods.ActionName = "FindDuplicateMethods"
        Me.FindDuplicateMethods.ButtonText = "Find Duplicate Methods"
        Me.FindDuplicateMethods.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
        Me.FindDuplicateMethods.Description = "Find Duplicate Methods"
        Me.FindDuplicateMethods.Image = CType(resources.GetObject("FindDuplicateMethods.Image"), System.Drawing.Bitmap)
        Me.FindDuplicateMethods.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.FindDuplicateMethods.ToolbarItem.Caption = Nothing
        Me.FindDuplicateMethods.ToolbarItem.Image = Nothing
        CType(Me.UniquenessHash, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FindDuplicateMethods, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents UniquenessHash As DevExpress.CodeRush.Extensions.CodeMetricProvider
    Friend WithEvents FindDuplicateMethods As DevExpress.CodeRush.Core.Action

End Class
