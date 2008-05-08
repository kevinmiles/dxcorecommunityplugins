Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Public MustInherit Class BaseDXRenderer
    Implements IDXRenderer
    Public MustOverride ReadOnly Property LanguageID() As String Implements IDXOperable.LanguageID
    Public Function Render(ByVal RootNode As LanguageElement) As String Implements IDXRenderer.Render
        Return CodeRush.Language.GenerateElement(RootNode, LanguageID)
    End Function

    Public Function RenderToEditor(ByVal RootNode As LanguageElement) As String Implements IDXRenderer.RenderToEditor
        Dim Code As String = Render(RootNode)
        CodeRush.Documents.ActiveTextView.Selection.Text = Code
        Return Code
    End Function
End Class
