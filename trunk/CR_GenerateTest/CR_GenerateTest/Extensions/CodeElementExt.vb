Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices

Public Module CodeElementExt
    <Extension()> _
    Public Function HasAttribute(ByVal Element As CodeElement, ByVal AttributeName As String) As Boolean
        If Element.AttributeSections.Count = 0 Then
            Return False
        End If
        Dim Section = TryCast(Element.AttributeSections(0), AttributeSection)
        Return Section.AttributeCollection.OfType(Of Attribute).Any(Function(Att) Att.Name = AttributeName)
    End Function
    <Extension()> _
    Public Sub AddAttribute(ByVal Element As CodeElement, ByVal AttributeName As String)
        Dim Section As AttributeSection
        If Element.AttributeSections.Count = 0 Then
            Section = New AttributeSection()
            Element.AttributeSections.Add(Section)
        Else
            Section = TryCast(Element.AttributeSections(0), AttributeSection)
        End If
        Section.AttributeCollection.Add(New Attribute() With {.Name = AttributeName})
    End Sub

End Module