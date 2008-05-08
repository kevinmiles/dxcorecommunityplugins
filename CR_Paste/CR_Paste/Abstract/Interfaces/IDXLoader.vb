Imports DevExpress.CodeRush.StructuralParser
Public Interface IDXLoader
    Inherits IDXOperable
    ReadOnly Property TreeRoot() As LanguageElement
    Sub Load(ByVal Text As String)
End Interface