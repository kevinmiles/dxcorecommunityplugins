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
        Me.cbxMetric = New System.Windows.Forms.ComboBox
        Me.optPCent = New System.Windows.Forms.RadioButton
        Me.RadioButton2 = New System.Windows.Forms.RadioButton
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.numMax = New System.Windows.Forms.NumericUpDown
        Me.HScrollBar4 = New System.Windows.Forms.HScrollBar
        Me.HScrollBar5 = New System.Windows.Forms.HScrollBar
        Me.HScrollBar6 = New System.Windows.Forms.HScrollBar
        Me.HScrollBar7 = New System.Windows.Forms.HScrollBar
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblMax = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.Label11 = New System.Windows.Forms.Label
        CType(Me.numMax, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cbxMetric
        '
        Me.cbxMetric.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxMetric.FormattingEnabled = True
        Me.cbxMetric.Location = New System.Drawing.Point(82, 29)
        Me.cbxMetric.Name = "cbxMetric"
        Me.cbxMetric.Size = New System.Drawing.Size(260, 21)
        Me.cbxMetric.TabIndex = 0
        '
        'optPCent
        '
        Me.optPCent.AutoSize = True
        Me.optPCent.Checked = True
        Me.optPCent.Location = New System.Drawing.Point(43, 144)
        Me.optPCent.Name = "optPCent"
        Me.optPCent.Size = New System.Drawing.Size(85, 17)
        Me.optPCent.TabIndex = 1
        Me.optPCent.TabStop = True
        Me.optPCent.Text = "Percentages"
        Me.optPCent.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(43, 185)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(90, 17)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.Text = "RadioButton1"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(40, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Metric"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Enabled = False
        Me.Label2.Location = New System.Drawing.Point(59, 293)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Green"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Enabled = False
        Me.Label3.Location = New System.Drawing.Point(59, 333)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Orange"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Enabled = False
        Me.Label4.Location = New System.Drawing.Point(58, 373)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(27, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Red"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Enabled = False
        Me.Label5.Location = New System.Drawing.Point(59, 253)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "White"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Enabled = False
        Me.Label6.Location = New System.Drawing.Point(58, 221)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Max Value"
        '
        'numMax
        '
        Me.numMax.Location = New System.Drawing.Point(122, 219)
        Me.numMax.Name = "numMax"
        Me.numMax.Size = New System.Drawing.Size(120, 20)
        Me.numMax.TabIndex = 4
        '
        'HScrollBar4
        '
        Me.HScrollBar4.Enabled = False
        Me.HScrollBar4.Location = New System.Drawing.Point(122, 289)
        Me.HScrollBar4.Name = "HScrollBar4"
        Me.HScrollBar4.Size = New System.Drawing.Size(325, 17)
        Me.HScrollBar4.TabIndex = 3
        '
        'HScrollBar5
        '
        Me.HScrollBar5.Enabled = False
        Me.HScrollBar5.Location = New System.Drawing.Point(122, 249)
        Me.HScrollBar5.Name = "HScrollBar5"
        Me.HScrollBar5.Size = New System.Drawing.Size(325, 17)
        Me.HScrollBar5.TabIndex = 3
        '
        'HScrollBar6
        '
        Me.HScrollBar6.Enabled = False
        Me.HScrollBar6.Location = New System.Drawing.Point(122, 329)
        Me.HScrollBar6.Name = "HScrollBar6"
        Me.HScrollBar6.Size = New System.Drawing.Size(325, 17)
        Me.HScrollBar6.TabIndex = 3
        '
        'HScrollBar7
        '
        Me.HScrollBar7.Enabled = False
        Me.HScrollBar7.Location = New System.Drawing.Point(122, 369)
        Me.HScrollBar7.Name = "HScrollBar7"
        Me.HScrollBar7.Size = New System.Drawing.Size(325, 17)
        Me.HScrollBar7.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.Location = New System.Drawing.Point(19, 144)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(453, 287)
        Me.Panel1.TabIndex = 5
        '
        'lblMax
        '
        Me.lblMax.AutoSize = True
        Me.lblMax.Location = New System.Drawing.Point(349, 32)
        Me.lblMax.Name = "lblMax"
        Me.lblMax.Size = New System.Drawing.Size(39, 13)
        Me.lblMax.TabIndex = 6
        Me.lblMax.Text = "Label7"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(178, 81)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(114, 13)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "White at <25% of Max."
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(178, 94)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(121, 13)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Green at < 50% of Max. "
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(178, 107)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(127, 13)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Orange at < 75% of Max. "
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(178, 120)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(50, 13)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "Else Red"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(89, 68)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(122, 13)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "Methods will be Colored:"
        '
        'Options1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.lblMax)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.numMax)
        Me.Controls.Add(Me.HScrollBar7)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.HScrollBar6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.HScrollBar5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.HScrollBar4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RadioButton2)
        Me.Controls.Add(Me.optPCent)
        Me.Controls.Add(Me.cbxMetric)
        Me.Name = "Options1"
        CType(Me.numMax, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents cbxMetric As System.Windows.Forms.ComboBox
    Friend WithEvents optPCent As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents numMax As System.Windows.Forms.NumericUpDown
    Friend WithEvents HScrollBar4 As System.Windows.Forms.HScrollBar
    Friend WithEvents HScrollBar5 As System.Windows.Forms.HScrollBar
    Friend WithEvents HScrollBar6 As System.Windows.Forms.HScrollBar
    Friend WithEvents HScrollBar7 As System.Windows.Forms.HScrollBar
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblMax As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label

	Public Shared ReadOnly Property FullPath() As String
		Get
			Return GetCategory() + "\" + GetPageName()
		End Get
	End Property

End Class
