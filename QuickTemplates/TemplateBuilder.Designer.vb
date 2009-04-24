Imports System
Imports System.Windows.Forms
Imports System.Environment
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.UserControls
Imports System.Drawing
Partial Public Class TemplateBuilder
    Inherits System.Windows.Forms.Form
#Region " Windows Form Designer generated code "
    Private Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        mLoaded = True
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents txtSelection As System.Windows.Forms.TextBox
    Friend WithEvents txtTemplateName As System.Windows.Forms.TextBox
    Friend WithEvents cmdOk As System.Windows.Forms.Button
    Friend WithEvents cmdCancel As System.Windows.Forms.Button
    Friend WithEvents txtTemplate As System.Windows.Forms.TextBox
    Friend WithEvents txtFields As System.Windows.Forms.TextBox
    Friend WithEvents ContextPicker1 As ContextPicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmdAddAsField As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtFields = New System.Windows.Forms.TextBox
        Me.txtSelection = New System.Windows.Forms.TextBox
        Me.txtTemplateName = New System.Windows.Forms.TextBox
        Me.cmdOk = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.txtTemplate = New System.Windows.Forms.TextBox
        Me.ContextPicker1 = New ContextPicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmdAddAsField = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtFields
        '
        Me.txtFields.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtFields.Location = New Point(4, 28)
        Me.txtFields.Multiline = True
        Me.txtFields.Name = "txtFields"
        Me.txtFields.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtFields.Size = New Size(188, 152)
        Me.txtFields.TabIndex = 1
        '
        'txtSelection
        '
        Me.txtSelection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSelection.HideSelection = False
        Me.txtSelection.Location = New Point(0, 0)
        Me.txtSelection.Multiline = True
        Me.txtSelection.Name = "txtSelection"
        Me.txtSelection.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtSelection.Size = New Size(380, 150)
        Me.txtSelection.TabIndex = 2
        Me.txtSelection.WordWrap = False
        '
        'txtTemplateName
        '
        Me.txtTemplateName.Location = New Point(128, 4)
        Me.txtTemplateName.Name = "txtTemplateName"
        Me.txtTemplateName.Size = New Size(64, 20)
        Me.txtTemplateName.TabIndex = 0
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOk.Location = New Point(428, 360)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New Size(75, 23)
        Me.cmdOk.TabIndex = 4
        Me.cmdOk.Text = "&Ok"
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.Location = New Point(508, 360)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New Size(75, 23)
        Me.cmdCancel.TabIndex = 5
        Me.cmdCancel.Text = "C&ancel"
        '
        'txtTemplate
        '
        Me.txtTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTemplate.Location = New Point(0, 0)
        Me.txtTemplate.Multiline = True
        Me.txtTemplate.Name = "txtTemplate"
        Me.txtTemplate.ReadOnly = True
        Me.txtTemplate.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtTemplate.Size = New Size(380, 150)
        Me.txtTemplate.TabIndex = 8
        Me.txtTemplate.TabStop = False
        Me.txtTemplate.WordWrap = False
        '
        'ContextPicker1
        '
        Me.ContextPicker1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ContextPicker1.HintDisplayTime = 6000
        Me.ContextPicker1.HintWindowWidth = 200
        Me.ContextPicker1.LegendBackground = SystemColors.Window
        Me.ContextPicker1.LegendFont = New Font("Microsoft Sans Serif", 8.25!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
        Me.ContextPicker1.Location = New Point(196, 212)
        Me.ContextPicker1.Name = "ContextPicker1"
        Me.ContextPicker1.RootContext = ""
        Me.ContextPicker1.ShowHint = True
        Me.ContextPicker1.ShowLegend = True
        Me.ContextPicker1.Size = New Size(388, 144)
        Me.ContextPicker1.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Location = New Point(76, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New Size(48, 16)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Name"
        Me.Label1.TextAlign = ContentAlignment.MiddleRight
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New Point(196, 4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New Size(388, 176)
        Me.TabControl1.TabIndex = 11
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.txtSelection)
        Me.TabPage1.Location = New Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New Size(380, 150)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Base Text"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.txtTemplate)
        Me.TabPage2.Location = New Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New Size(380, 150)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Preview"
        '
        'Label2
        '
        Me.Label2.Location = New Point(8, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New Size(48, 16)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Fields:"
        Me.Label2.TextAlign = ContentAlignment.MiddleLeft
        '
        'cmdAddAsField
        '
        Me.cmdAddAsField.Location = New Point(196, 184)
        Me.cmdAddAsField.Name = "cmdAddAsField"
        Me.cmdAddAsField.Size = New Size(100, 23)
        Me.cmdAddAsField.TabIndex = 13
        Me.cmdAddAsField.Text = "<<Add As Field"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New Point(4, 184)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New Size(186, 172)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Help"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Location = New Point(3, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New Size(39, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Label3"
        '
        'TemplateBuilder
        '
        Me.AutoScaleBaseSize = New Size(5, 13)
        Me.ClientSize = New Size(588, 386)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cmdAddAsField)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ContextPicker1)
        Me.Controls.Add(Me.txtTemplateName)
        Me.Controls.Add(Me.txtFields)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOk)
        Me.Name = "TemplateBuilder"
        Me.Text = "Create Quick Template"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

End Class
