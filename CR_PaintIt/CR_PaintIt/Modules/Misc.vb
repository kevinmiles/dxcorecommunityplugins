Option Strict On
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Public Module Misc
    Public Function CollectionContainsElement(ByVal Collection As LanguageElementCollection, ByVal LE As LanguageElement) As Boolean
        For Each Item As LanguageElement In Collection
            If ElementsAreSame(Item, LE) Then
                Return True
            End If
        Next
    End Function
End Module
