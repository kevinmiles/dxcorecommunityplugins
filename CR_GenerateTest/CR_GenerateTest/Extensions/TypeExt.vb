Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports System.IO
Imports System.Runtime.CompilerServices
Imports DevExpress.CodeRush.Diagnostics.Core
Imports EnvDTE80


Public Module TypeExt
    <Extension()> _
    Public Sub AddAttribute(ByVal CodeElement As CodeElement, ByVal AttributeName As String)
        Dim Section As AttributeSection
        If CodeElement.AttributeSections.Count = 0 Then
            Section = New AttributeSection()
            CodeElement.AttributeSections.Add(Section)
        Else
            Section = TryCast(CodeElement.AttributeSections(0), AttributeSection)
        End If
        Section.AttributeCollection.Add(New Attribute() With {.Name = AttributeName})
    End Sub

    <Extension()> _
    Public Function GetMethodWithName(ByVal ParentType As TypeDeclaration, ByVal MethodName As String) As Method
        Return ParentType.FirstMethodWhere(Function(M) M.Name = MethodName)
    End Function
    <Extension()> _
    Public Function FirstMethodWhere(ByVal TestType As TypeDeclaration, ByVal Func As Func(Of Method, Boolean)) As Method
        Return TestType.AllMethods.OfType(Of Method).Where(Func).FirstOrDefault
    End Function
    <Extension()> _
    Public Function FirstMethodNameNotInUse(ByVal TestType As TypeDeclaration, ByVal BaseMethodName As String) As String
        Dim Result = String.Empty
        Dim CandidateName As String = BaseMethodName
        Dim Count As Integer = 0
        Do While TestType.AllMethods.OfType(Of Method).Any(Function(m) m.Name = CandidateName)
            Count += 1
            CandidateName = BaseMethodName & CStr(Count)
        Loop
        Return CandidateName
    End Function

End Module