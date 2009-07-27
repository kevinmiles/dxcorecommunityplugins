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
        Dim Parameter1 As DevExpress.CodeRush.Core.Parameter = New DevExpress.CodeRush.Core.Parameter(New DevExpress.CodeRush.Core.StringParameterType)
        Dim Parameter2 As DevExpress.CodeRush.Core.Parameter = New DevExpress.CodeRush.Core.Parameter(New DevExpress.CodeRush.Core.StringParameterType)
        Me.actExpandHeader = New DevExpress.CodeRush.Core.Action(Me.components)
        CType(Me.actExpandHeader, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'actExpandHeader
        '
        Me.actExpandHeader.ActionName = "ExpandHeader"
        Me.actExpandHeader.ButtonText = "Expand Header"
        Me.actExpandHeader.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
        Me.actExpandHeader.Description = "Expand Header"
        Me.actExpandHeader.Image = CType(resources.GetObject("actExpandHeader.Image"), System.Drawing.Bitmap)
        Me.actExpandHeader.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))
        Parameter1.DefaultValue = ""
        Parameter1.Description = "Path to Template"
        Parameter1.Name = "Template"
        Parameter1.Optional = False
        Parameter2.DefaultValue = "File"
        Parameter2.Description = "Type of Header(File, Type or Member)"
        Parameter2.Name = "HeaderType"
        Parameter2.Optional = True
        Me.actExpandHeader.Parameters.Add(Parameter1)
        Me.actExpandHeader.Parameters.Add(Parameter2)
        CType(Me.actExpandHeader, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents actExpandHeader As DevExpress.CodeRush.Core.Action

End Class
