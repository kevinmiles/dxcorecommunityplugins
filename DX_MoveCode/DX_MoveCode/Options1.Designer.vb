<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Options1
	Inherits DevExpress.CodeRush.Core.OptionsPage

	<System.Diagnostics.DebuggerNonUserCode()> _
	Public Sub New()
		MyBase.New()

		'This call is required by the Component Designer.
		InitializeComponent()

	End Sub

	'OptionsPage overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()> _
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing AndAlso components IsNot Nothing Then
			components.Dispose()
		End If
		MyBase.Dispose(disposing)
	End Sub

	'Required by the Windows Form Designer
	Private components As System.ComponentModel.IContainer

	'NOTE: The following procedure is required by the Windows Form Designer
	'It can be modified using the Windows Form Designer.  
	'Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> _
	Private Sub InitializeComponent()
        Me.optMoveSource = New System.Windows.Forms.RadioButton
        Me.optSwapElements = New System.Windows.Forms.RadioButton
        Me.Label1 = New System.Windows.Forms.Label
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'optMoveSource
        '
        Me.optMoveSource.AutoSize = True
        Me.optMoveSource.Checked = True
        Me.optMoveSource.Location = New System.Drawing.Point(61, 62)
        Me.optMoveSource.Name = "optMoveSource"
        Me.optMoveSource.Size = New System.Drawing.Size(130, 17)
        Me.optMoveSource.TabIndex = 0
        Me.optMoveSource.TabStop = True
        Me.optMoveSource.Text = "Move Source Element"
        Me.optMoveSource.UseVisualStyleBackColor = True
        '
        'optSwapElements
        '
        Me.optSwapElements.AutoSize = True
        Me.optSwapElements.Location = New System.Drawing.Point(61, 85)
        Me.optSwapElements.Name = "optSwapElements"
        Me.optSwapElements.Size = New System.Drawing.Size(98, 17)
        Me.optSwapElements.TabIndex = 0
        Me.optSwapElements.Text = "Swap Elements"
        Me.optSwapElements.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(61, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(248, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Which algorithm to use in moving Elements around."
        '
        'Options1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.optSwapElements)
        Me.Controls.Add(Me.optMoveSource)
        Me.Name = "Options1"
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public Shared ReadOnly Property Storage() As DevExpress.CodeRush.Core.DecoupledStorage
        Get
            Return DevExpress.CodeRush.Core.CodeRush.Options.GetStorage(GetCategory(), GetPageName())
        End Get
    End Property

    Public Overrides ReadOnly Property Category() As String
        Get
            Return Options1.GetCategory()
        End Get
    End Property

    Public Overrides ReadOnly Property PageName() As String
        Get
            Return Options1.GetPageName()
        End Get
    End Property

    Public Shared Shadows Sub Show()
        DevExpress.CodeRush.Core.CodeRush.Command.Execute("Options", FullPath)
    End Sub
    Friend WithEvents optMoveSource As System.Windows.Forms.RadioButton
    Friend WithEvents optSwapElements As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label

	Public Shared ReadOnly Property FullPath() As String
		Get
			Return GetCategory() + "\" + GetPageName()
		End Get
	End Property

End Class
