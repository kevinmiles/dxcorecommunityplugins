Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core

Public Class TemplateCRUD
    Public Sub EnsureTemplateExists(ByVal TemplateName As String, ByVal TemplateText As String)
        Dim Parts = SplitTemplateCategoryAndName(TemplateName)
        Call CreateTemplate(Parts(0), TemplateText, Parts(1), New ContextPickerData())
    End Sub
    Public Function SplitTemplateCategoryAndName(ByRef TemplateName As String) As String()
        If Not TemplateName.Contains("\") Then
            Throw New ApplicationException("Template has no Category")
        End If
        Dim Result As New List(Of String)
        Dim LastSlashPos As Integer = InStrRev(TemplateName, "\"c)
        Result.Add(TemplateName.Substring(LastSlashPos)) ' Name 
        Result.Add(TemplateName.Substring(0, LastSlashPos)) ' Category
        Return Result.ToArray
    End Function
    Private Function CreateTemplate(ByVal TemplateName As String, _
                                    ByVal ExpansionText As String, _
                                    ByVal CategoryName As String, _
                                    ByVal Context As ContextPickerData) As Boolean
        Dim Template As Template = GetOrCreateTemplateByNameAndCat(TemplateName, CategoryName, CodeRush.Language.Active)
        Dim Data As TemplateData = New TemplateData
        Data.Expansion = ExpansionText
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
End Class