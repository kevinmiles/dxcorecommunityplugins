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
        Me.lblClassNameOnly = New System.Windows.Forms.Label
        Me.chkClassNameOnly = New System.Windows.Forms.CheckBox
        Me.chkUpdateOnSave = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkReplaceClassOnly = New System.Windows.Forms.CheckBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.Controls.Add(Me.lblClassNameOnly, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.chkClassNameOnly, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.chkUpdateOnSave, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.chkReplaceClassOnly, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 3)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(530, 480)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'lblClassNameOnly
        '
        Me.lblClassNameOnly.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblClassNameOnly.Location = New System.Drawing.Point(3, 0)
        Me.lblClassNameOnly.Name = "lblClassNameOnly"
        Me.lblClassNameOnly.Size = New System.Drawing.Size(166, 62)
        Me.lblClassNameOnly.TabIndex = 0
        Me.lblClassNameOnly.Text = "Update from Class Name Only?"
        Me.lblClassNameOnly.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkClassNameOnly
        '
        Me.chkClassNameOnly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkClassNameOnly.AutoSize = True
        Me.chkClassNameOnly.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkClassNameOnly.Location = New System.Drawing.Point(175, 3)
        Me.chkClassNameOnly.Name = "chkClassNameOnly"
        Me.chkClassNameOnly.Size = New System.Drawing.Size(278, 56)
        Me.chkClassNameOnly.TabIndex = 2
        Me.chkClassNameOnly.Text = "By default if you popup the Refactor window within" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "the PersistentClass the Updat" & _
            "e XPO FieldsClass will" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "be available, use this setting to make it only available" & _
            " " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "when the caret is on the Class name"
        Me.chkClassNameOnly.UseVisualStyleBackColor = True
        '
        'chkUpdateOnSave
        '
        Me.chkUpdateOnSave.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkUpdateOnSave.AutoSize = True
        Me.chkUpdateOnSave.Location = New System.Drawing.Point(175, 65)
        Me.chkUpdateOnSave.Name = "chkUpdateOnSave"
        Me.chkUpdateOnSave.Size = New System.Drawing.Size(15, 17)
        Me.chkUpdateOnSave.TabIndex = 4
        Me.chkUpdateOnSave.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Location = New System.Drawing.Point(3, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(166, 23)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Perform Update on Save?"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkReplaceClassOnly
        '
        Me.chkReplaceClassOnly.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkReplaceClassOnly.AutoSize = True
        Me.chkReplaceClassOnly.CheckAlign = System.Drawing.ContentAlignment.TopLeft
        Me.chkReplaceClassOnly.Location = New System.Drawing.Point(175, 88)
        Me.chkReplaceClassOnly.Name = "chkReplaceClassOnly"
        Me.chkReplaceClassOnly.Size = New System.Drawing.Size(242, 43)
        Me.chkReplaceClassOnly.TabIndex = 6
        Me.chkReplaceClassOnly.Text = "When updating the class if the " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Fields property and/or _fields variable is found" & _
            "" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "don't preemptively replace them"
        Me.chkReplaceClassOnly.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.Location = New System.Drawing.Point(3, 85)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(166, 49)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Don't replace Property/Variable?"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
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
    Friend WithEvents lblClassNameOnly As System.Windows.Forms.Label
    Friend WithEvents chkClassNameOnly As System.Windows.Forms.CheckBox
    Friend WithEvents chkUpdateOnSave As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkReplaceClassOnly As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label

	Public Shared ReadOnly Property FullPath() As String
		Get
			Return GetCategory() + "\" + GetPageName()
		End Get
	End Property

End Class
