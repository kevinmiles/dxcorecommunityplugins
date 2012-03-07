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
        Me.txtTestClassSuffix = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTestNamespaceSuffix = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtTestClassSuffix
        '
        Me.txtTestClassSuffix.Location = New System.Drawing.Point(155, 64)
        Me.txtTestClassSuffix.Name = "txtTestClassSuffix"
        Me.txtTestClassSuffix.Size = New System.Drawing.Size(221, 20)
        Me.txtTestClassSuffix.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(62, 67)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Test Class Suffix"
        '
        'txtTestNamespaceSuffix
        '
        Me.txtTestNamespaceSuffix.Location = New System.Drawing.Point(155, 90)
        Me.txtTestNamespaceSuffix.Name = "txtTestNamespaceSuffix"
        Me.txtTestNamespaceSuffix.Size = New System.Drawing.Size(221, 20)
        Me.txtTestNamespaceSuffix.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(32, 93)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(117, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Test Namespace Suffix"
        '
        'Options1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtTestNamespaceSuffix)
        Me.Controls.Add(Me.txtTestClassSuffix)
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
    Friend WithEvents txtTestClassSuffix As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtTestNamespaceSuffix As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label

	Public Shared ReadOnly Property FullPath() As String
		Get
			Return GetCategory() + "\" + GetPageName()
		End Get
	End Property

End Class
