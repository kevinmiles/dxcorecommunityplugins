Imports DevExpress.CodeRush.StructuralParser
Public Interface IDXLoader
    Inherits IDXOperable
    ReadOnly Property TreeRoot() As LanguageElement
    Function Load(ByVal Text As String) As Boolean
End Interface