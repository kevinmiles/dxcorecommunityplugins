<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class XPO_EasyFields_Options
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.lblRegion = New System.Windows.Forms.Label
        Me.txtRegionName = New System.Windows.Forms.TextBox
        Me.chkRegion = New System.Windows.Forms.CheckBox
        Me.lblRegionName = New System.Windows.Forms.Label
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.Controls.Add(Me.lblRegionName, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblRegion, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtRegionName, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.chkRegion, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(530, 480)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'lblRegion
        '
        Me.lblRegion.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRegion.Location = New System.Drawing.Point(3, 0)
        Me.lblRegion.Name = "lblRegion"
        Me.lblRegion.Size = New System.Drawing.Size(145, 23)
        Me.lblRegion.TabIndex = 0
        Me.lblRegion.Text = "Embed Class within Region:"
        Me.lblRegion.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRegionName
        '
        Me.txtRegionName.Location = New System.Drawing.Point(154, 26)
        Me.txtRegionName.Name = "txtRegionName"
        Me.txtRegionName.Size = New System.Drawing.Size(196, 20)
        Me.txtRegionName.TabIndex = 1
        '
        'chkRegion
        '
        Me.chkRegion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkRegion.AutoSize = True
        Me.chkRegion.Location = New System.Drawing.Point(154, 3)
        Me.chkRegion.Name = "chkRegion"
        Me.chkRegion.Size = New System.Drawing.Size(15, 17)
        Me.chkRegion.TabIndex = 2
        Me.chkRegion.UseVisualStyleBackColor = True
        '
        'lblRegionName
        '
        Me.lblRegionName.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRegionName.Location = New System.Drawing.Point(3, 23)
        Me.lblRegionName.Name = "lblRegionName"
        Me.lblRegionName.Size = New System.Drawing.Size(145, 26)
        Me.lblRegionName.TabIndex = 3
        Me.lblRegionName.Text = "Region Name:"
        Me.lblRegionName.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'XPO_EasyFields_Options
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "XPO_EasyFields_Options"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Public Shared ReadOnly Property Storage() As DevExpress.CodeRush.Core.DecoupledStorage
        Get
            Return DevExpress.CodeRush.Core.CodeRush.Options.GetStorage(GetCategory(), GetPageName())
        End Get
    End Property

    Public Overrides ReadOnly Property Category() As String
        Get
            Return XPO_EasyFields_Options.GetCategory()
        End Get
    End Property

    Public Overrides ReadOnly Property PageName() As String
        Get
            Return XPO_EasyFields_Options.GetPageName()
        End Get
    End Property

    Public Shared Shadows Sub Show()
        DevExpress.CodeRush.Core.CodeRush.Command.Execute("Options", FullPath)
    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblRegion As System.Windows.Forms.Label
    Friend WithEvents txtRegionName As System.Windows.Forms.TextBox
    Friend WithEvents chkRegion As System.Windows.Forms.CheckBox
    Friend WithEvents lblRegionName As System.Windows.Forms.Label

	Public Shared ReadOnly Property FullPath() As String
		Get
			Return GetCategory() + "\" + GetPageName()
		End Get
	End Property

End Class
