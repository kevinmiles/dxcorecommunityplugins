Imports DevExpress.CodeRush.StructuralParser

Public Interface IDXRenderer
    Inherits IDXOperable
    Function Render(ByVal RootNode As LanguageElement) As String
    Function RenderToEditor(ByVal RootNode As LanguageElement) As String
End Interface