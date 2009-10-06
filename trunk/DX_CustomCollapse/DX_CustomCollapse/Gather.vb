Imports DevExpress.CodeRush.StructuralParser
Public Module Gather
    Friend Function GetElements(ByVal Scope As LanguageElement, Optional ByVal Proc As System.Func(Of LanguageElement, Boolean) = Nothing) As IEnumerable(Of LanguageElement)
        Dim Enumerator = New ElementEnumerable(Scope, True).OfType(Of LanguageElement)()
        If Proc Is Nothing Then
            Return Enumerator
        End If
        Return Enumerator.Where(Proc)
    End Function
    Friend Function GetElements(ByVal Scope As LanguageElement, ByVal ElementType() As LanguageElementType, Optional ByVal Proc As System.Func(Of LanguageElement, Boolean) = Nothing) As IEnumerable(Of LanguageElement)
        Dim Enumerator = New ElementEnumerable(Scope, New ElementTypeFilter(ElementType), True).OfType(Of LanguageElement)()
        If Proc Is Nothing Then
            Return Enumerator
        End If
        Return Enumerator.Where(Proc)
    End Function
End Module
