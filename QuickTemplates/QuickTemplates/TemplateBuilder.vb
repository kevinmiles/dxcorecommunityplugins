Imports System
Imports System.Windows.Forms
Imports System.Environment
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.UserControls
Imports System.Drawing

Partial Public Class TemplateBuilder
    Inherits Form
    Private mLoaded As Boolean = False
#Region "Utils"
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
        Dim ErrorMessage As String = String.Empty
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
                    Case Else
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
