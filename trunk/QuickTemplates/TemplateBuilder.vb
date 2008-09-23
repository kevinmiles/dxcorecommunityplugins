Imports System
Imports System.Windows.Forms
Imports System.Environment
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Public Class TemplateBuilder
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
    Friend WithEvents ContextPicker1 As DevExpress.CodeRush.UserControls.ContextPicker
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
        Me.ContextPicker1 = New DevExpress.CodeRush.UserControls.ContextPicker
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
        Me.txtFields.Location = New System.Drawing.Point(4, 28)
        Me.txtFields.Multiline = True
        Me.txtFields.Name = "txtFields"
        Me.txtFields.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtFields.Size = New System.Drawing.Size(188, 152)
        Me.txtFields.TabIndex = 1
        '
        'txtSelection
        '
        Me.txtSelection.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtSelection.HideSelection = False
        Me.txtSelection.Location = New System.Drawing.Point(0, 0)
        Me.txtSelection.Multiline = True
        Me.txtSelection.Name = "txtSelection"
        Me.txtSelection.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtSelection.Size = New System.Drawing.Size(380, 150)
        Me.txtSelection.TabIndex = 2
        Me.txtSelection.WordWrap = False
        '
        'txtTemplateName
        '
        Me.txtTemplateName.Location = New System.Drawing.Point(128, 4)
        Me.txtTemplateName.Name = "txtTemplateName"
        Me.txtTemplateName.Size = New System.Drawing.Size(64, 20)
        Me.txtTemplateName.TabIndex = 0
        '
        'cmdOk
        '
        Me.cmdOk.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOk.Location = New System.Drawing.Point(428, 360)
        Me.cmdOk.Name = "cmdOk"
        Me.cmdOk.Size = New System.Drawing.Size(75, 23)
        Me.cmdOk.TabIndex = 4
        Me.cmdOk.Text = "&Ok"
        '
        'cmdCancel
        '
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.Location = New System.Drawing.Point(508, 360)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(75, 23)
        Me.cmdCancel.TabIndex = 5
        Me.cmdCancel.Text = "C&ancel"
        '
        'txtTemplate
        '
        Me.txtTemplate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtTemplate.Location = New System.Drawing.Point(0, 0)
        Me.txtTemplate.Multiline = True
        Me.txtTemplate.Name = "txtTemplate"
        Me.txtTemplate.ReadOnly = True
        Me.txtTemplate.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtTemplate.Size = New System.Drawing.Size(380, 150)
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
        Me.ContextPicker1.LegendBackground = System.Drawing.SystemColors.Window
        Me.ContextPicker1.LegendFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ContextPicker1.Location = New System.Drawing.Point(196, 212)
        Me.ContextPicker1.Name = "ContextPicker1"
        Me.ContextPicker1.RootContext = ""
        Me.ContextPicker1.ShowHint = True
        Me.ContextPicker1.ShowLegend = True
        Me.ContextPicker1.Size = New System.Drawing.Size(388, 144)
        Me.ContextPicker1.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(76, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 16)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Name"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(196, 4)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(388, 176)
        Me.TabControl1.TabIndex = 11
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.txtSelection)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(380, 150)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Base Text"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.txtTemplate)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Size = New System.Drawing.Size(380, 150)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Preview"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 16)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Fields:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmdAddAsField
        '
        Me.cmdAddAsField.Location = New System.Drawing.Point(196, 184)
        Me.cmdAddAsField.Name = "cmdAddAsField"
        Me.cmdAddAsField.Size = New System.Drawing.Size(100, 23)
        Me.cmdAddAsField.TabIndex = 13
        Me.cmdAddAsField.Text = "<<Add As Field"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 184)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(186, 172)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Help"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Location = New System.Drawing.Point(3, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(39, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Label3"
        '
        'TemplateBuilder
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(588, 386)
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
    Private mLoaded As Boolean = False
#Region "Properties"
    Private ReadOnly Property GetPrimaryField() As String
        Get
            If txtFields.Text = String.Empty Then
                Return String.Empty
            End If
            Return txtFields.Lines(0).Trim
        End Get
    End Property
#End Region
#Region "UI Events"
    Private Sub cmdOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOk.Click
        ValidateAndQuit()
    End Sub
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.Close()
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If mLoaded Then
            Call UpdatePreview()
        End If
    End Sub

    Private Sub cmdAddAsField_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddAsField.Click
        If txtSelection.SelectedText.IndexOf(NewLine) > -1 Then
            Exit Sub
        End If
        If txtSelection.SelectedText <> String.Empty Then
            If Not FieldListContains(txtSelection.SelectedText) Then
                Call AddField(txtSelection.SelectedText)
            End If
        End If
    End Sub
    Private Sub AddField(ByVal Field As String)
        txtFields.Text = txtFields.Text.Trim
        If txtFields.Text = String.Empty Then
            txtFields.Text = Field
        Else
            txtFields.Text = txtFields.Text.Trim & NewLine & Field
        End If
    End Sub
    Private Function FieldListContains(ByVal SearchFor As String) As Boolean
        If txtFields.Text.Trim = String.Empty Then
            Return False
        End If
        For CurrentLine As Integer = 0 To txtFields.Lines.GetLength(0)
            If txtFields.Lines(CurrentLine).Trim = SearchFor Then
                Return True
            End If
        Next
        Return False
    End Function
#End Region
#Region "Validation"
    Private Function FormIsValid(ByRef ErrorMessage As String) As Boolean
        If txtTemplateName.Text.Trim = String.Empty Then
            ErrorMessage = "Template has no Name"
            txtTemplateName.Focus()
            Return False
        End If
        If txtSelection.Text.Trim = String.Empty Then
            ErrorMessage = "Template text is empty."
            txtSelection.Focus()
            Return False
        End If

        Return True
    End Function
    Private Sub ValidateAndQuit()
        Call ValidateAndProcess()
        Call CreateTemplate(txtTemplateName.Text, _
                            txtTemplate.Text, _
                            "QuickTemplates", _
                            ContextPicker1.GetData)
        Me.DialogResult = Windows.Forms.DialogResult.OK
    End Sub
    Private Sub ValidateAndProcess()
        ' Check Form Valid
        Dim ErrorMessage As String
        If Not FormIsValid(ErrorMessage) Then
            MessageBox.Show(ErrorMessage)
            Exit Sub
        End If
        Call UpdatePreview()
    End Sub
    Private Sub UpdatePreview()
        txtTemplate.Text = _
            TemplateManipulator.CreateTemplate(txtSelection.Text, _
                                               txtFields.Lines, _
                                               GetPrimaryField)
    End Sub
#End Region
    Private Function CreateTemplate(ByVal TemplateName As String, ByVal Expansion As String, ByVal CategoryName As String, ByVal Context As ContextPickerData) As Boolean
        Dim Template As Template = GetOrCreateTemplateByNameAndCat(TemplateName, CategoryName, CodeRush.Language.Active)
        Dim Data As TemplateData = New TemplateData
        Data.Expansion = Expansion
        Call Data.SetContext(Context)
        Select Case Template.Items.Count
            Case 0
                Template.Items.Add(Data)
            Case 1
                Dim Replace As DialogResult = MessageBox.Show("This Template already exists. Would you like to replace the old Template?", "Template already exists", MessageBoxButtons.YesNoCancel)
                Select Case Replace
                    Case DialogResult.Yes
                        'Replace Existing Template Variations
                        Template.Items.Clear()
                        Template.Items.Add(Data)
                    Case DialogResult.No
                        ' Add this variation to those Already existing.
                        Template.Items.Add(Data)
                    Case DialogResult.Cancel
                        Return False ' Cancel
                End Select
            Case Else
                ' Many alternatives
                MessageBox.Show("Multiple Template expansions found. Aborting.")
                Return False
        End Select
        CodeRush.Templates.Save()
    End Function
    Private Function GetOrCreateTemplateByNameAndCat(ByVal TemplateName As String, ByVal CategoryName As String, ByVal Language As String) As Template
        Dim QTC As TemplateCategory = CodeRush.Templates.FindCategory(CategoryName, Language)
        If QTC Is Nothing Then
            QTC = CodeRush.Templates.AddCategory(CategoryName, "", Language)
        End If
        Dim Template As Template = CodeRush.Templates.FindTemplate(TemplateName, CategoryName, Language)
        If Template Is Nothing Then
            Template = CodeRush.Templates.AddTemplate(TemplateName, CategoryName, Language)
        End If
        Return Template
    End Function
#Region "Shared Methods"
    Public Shared Sub CreateQuickTemplate(ByVal BaseText As String)
        Dim TheForm As New TemplateBuilder
        TheForm.txtSelection.Text = BaseText
        CodeRush.Context.PopulateContextPicker(TheForm.ContextPicker1, CodeRush.Language.Active)
        TheForm.ShowDialog()
    End Sub
#End Region

    Private Sub TemplateBuilder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Need to setup a default context here.
        ' Try to base this on language.
        ' Basic, C# : InMethod, InClass, InInterface

    End Sub

End Class
