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
        Me.Label1 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.lblMax = New System.Windows.Forms.Label
        Me.chkEnabled = New System.Windows.Forms.CheckBox
        Me.Boundary3 = New System.Windows.Forms.TrackBar
        Me.Boundary2 = New System.Windows.Forms.TrackBar
        Me.Boundary1 = New System.Windows.Forms.TrackBar
        Me.lblPCent1 = New System.Windows.Forms.Label
        Me.lblPCent2 = New System.Windows.Forms.Label
        Me.lblPCent3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.ColorPicker3 = New CR_MetricShader.ColorPicker
        Me.ColorPicker2 = New CR_MetricShader.ColorPicker
        Me.ColorPicker1 = New CR_MetricShader.ColorPicker
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        CType(Me.Boundary3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Boundary2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Boundary1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cbxMetric
        '
        Me.cbxMetric.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbxMetric.FormattingEnabled = True
        Me.cbxMetric.Location = New System.Drawing.Point(74, 44)
        Me.cbxMetric.Name = "cbxMetric"
        Me.cbxMetric.Size = New System.Drawing.Size(260, 21)
        Me.cbxMetric.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(32, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Metric"
        '
        'Panel1
        '
        Me.Panel1.Location = New System.Drawing.Point(35, 386)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(453, 72)
        Me.Panel1.TabIndex = 5
        '
        'lblMax
        '
        Me.lblMax.AutoSize = True
        Me.lblMax.Location = New System.Drawing.Point(341, 47)
        Me.lblMax.Name = "lblMax"
        Me.lblMax.Size = New System.Drawing.Size(39, 13)
        Me.lblMax.TabIndex = 6
        Me.lblMax.Text = "Label7"
        '
        'chkEnabled
        '
        Me.chkEnabled.AutoSize = True
        Me.chkEnabled.Checked = True
        Me.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkEnabled.Location = New System.Drawing.Point(74, 21)
        Me.chkEnabled.Name = "chkEnabled"
        Me.chkEnabled.Size = New System.Drawing.Size(65, 17)
        Me.chkEnabled.TabIndex = 8
        Me.chkEnabled.Text = "Enabled"
        Me.chkEnabled.UseVisualStyleBackColor = True
        '
        'Boundary3
        '
        Me.Boundary3.LargeChange = 10
        Me.Boundary3.Location = New System.Drawing.Point(284, 205)
        Me.Boundary3.Maximum = 100
        Me.Boundary3.Minimum = 1
        Me.Boundary3.Name = "Boundary3"
        Me.Boundary3.Size = New System.Drawing.Size(164, 45)
        Me.Boundary3.SmallChange = 5
        Me.Boundary3.TabIndex = 3
        Me.Boundary3.TickFrequency = 10
        Me.Boundary3.Value = 1
        '
        'Boundary2
        '
        Me.Boundary2.LargeChange = 10
        Me.Boundary2.Location = New System.Drawing.Point(284, 165)
        Me.Boundary2.Maximum = 100
        Me.Boundary2.Minimum = 1
        Me.Boundary2.Name = "Boundary2"
        Me.Boundary2.Size = New System.Drawing.Size(164, 45)
        Me.Boundary2.SmallChange = 5
        Me.Boundary2.TabIndex = 3
        Me.Boundary2.TickFrequency = 10
        Me.Boundary2.Value = 1
        '
        'Boundary1
        '
        Me.Boundary1.LargeChange = 10
        Me.Boundary1.Location = New System.Drawing.Point(284, 121)
        Me.Boundary1.Maximum = 100
        Me.Boundary1.Minimum = 1
        Me.Boundary1.Name = "Boundary1"
        Me.Boundary1.Size = New System.Drawing.Size(164, 45)
        Me.Boundary1.SmallChange = 5
        Me.Boundary1.TabIndex = 3
        Me.Boundary1.TickFrequency = 10
        Me.Boundary1.Value = 1
        '
        'lblPCent1
        '
        Me.lblPCent1.AutoSize = True
        Me.lblPCent1.Location = New System.Drawing.Point(451, 132)
        Me.lblPCent1.Name = "lblPCent1"
        Me.lblPCent1.Size = New System.Drawing.Size(13, 13)
        Me.lblPCent1.TabIndex = 10
        Me.lblPCent1.Text = "1"
        '
        'lblPCent2
        '
        Me.lblPCent2.AutoSize = True
        Me.lblPCent2.Location = New System.Drawing.Point(451, 169)
        Me.lblPCent2.Name = "lblPCent2"
        Me.lblPCent2.Size = New System.Drawing.Size(13, 13)
        Me.lblPCent2.TabIndex = 10
        Me.lblPCent2.Text = "2"
        '
        'lblPCent3
        '
        Me.lblPCent3.AutoSize = True
        Me.lblPCent3.Location = New System.Drawing.Point(451, 205)
        Me.lblPCent3.Name = "lblPCent3"
        Me.lblPCent3.Size = New System.Drawing.Size(13, 13)
        Me.lblPCent3.TabIndex = 10
        Me.lblPCent3.Text = "3"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(82, 90)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(31, 13)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "Color"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(168, 90)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Opacity"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(341, 90)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Percentage"
        '
        'ColorPicker3
        '
        Me.ColorPicker3.AllowOpacity = True
        Me.ColorPicker3.ColorBase = System.Drawing.Color.Empty
        Me.ColorPicker3.LabelWidth = 104
        Me.ColorPicker3.Location = New System.Drawing.Point(3, 205)
        Me.ColorPicker3.Name = "ColorPicker3"
        Me.ColorPicker3.Opacity = 255
        Me.ColorPicker3.Size = New System.Drawing.Size(264, 31)
        Me.ColorPicker3.TabIndex = 9
        Me.ColorPicker3.Text = "Color3"
        '
        'ColorPicker2
        '
        Me.ColorPicker2.AllowOpacity = True
        Me.ColorPicker2.ColorBase = System.Drawing.Color.Empty
        Me.ColorPicker2.LabelWidth = 104
        Me.ColorPicker2.Location = New System.Drawing.Point(3, 165)
        Me.ColorPicker2.Name = "ColorPicker2"
        Me.ColorPicker2.Opacity = 255
        Me.ColorPicker2.Size = New System.Drawing.Size(264, 31)
        Me.ColorPicker2.TabIndex = 9
        Me.ColorPicker2.Text = "Color2"
        '
        'ColorPicker1
        '
        Me.ColorPicker1.AllowOpacity = True
        Me.ColorPicker1.ColorBase = System.Drawing.Color.Black
        Me.ColorPicker1.LabelWidth = 104
        Me.ColorPicker1.Location = New System.Drawing.Point(3, 121)
        Me.ColorPicker1.Name = "ColorPicker1"
        Me.ColorPicker1.Opacity = 255
        Me.ColorPicker1.Size = New System.Drawing.Size(264, 31)
        Me.ColorPicker1.TabIndex = 9
        Me.ColorPicker1.Text = "Color1"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(45, 253)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(163, 13)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Please note: For sanity reasons..."
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(45, 277)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(423, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Please keep Color1, Color2 and Color3's percentage value in ascending numerical o" & _
            "rder."
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(226, 299)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 13)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "m'Kay?"
        '
        'Options1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblPCent3)
        Me.Controls.Add(Me.lblPCent2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblPCent1)
        Me.Controls.Add(Me.ColorPicker3)
        Me.Controls.Add(Me.ColorPicker2)
        Me.Controls.Add(Me.ColorPicker1)
        Me.Controls.Add(Me.chkEnabled)
        Me.Controls.Add(Me.lblMax)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Boundary3)
        Me.Controls.Add(Me.Boundary2)
        Me.Controls.Add(Me.Boundary1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbxMetric)
        Me.Name = "Options1"
        CType(Me.Boundary3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Boundary2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Boundary1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblMax As System.Windows.Forms.Label
    Friend WithEvents chkEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents Boundary3 As System.Windows.Forms.TrackBar
    Friend WithEvents Boundary2 As System.Windows.Forms.TrackBar
    Friend WithEvents Boundary1 As System.Windows.Forms.TrackBar
    Friend WithEvents ColorPicker1 As CR_MetricShader.ColorPicker
    Friend WithEvents ColorPicker2 As CR_MetricShader.ColorPicker
    Friend WithEvents ColorPicker3 As CR_MetricShader.ColorPicker
    Friend WithEvents lblPCent1 As System.Windows.Forms.Label
    Friend WithEvents lblPCent2 As System.Windows.Forms.Label
    Friend WithEvents lblPCent3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label

	Public Shared ReadOnly Property FullPath() As String
		Get
			Return GetCategory() + "\" + GetPageName()
		End Get
	End Property

End Class
