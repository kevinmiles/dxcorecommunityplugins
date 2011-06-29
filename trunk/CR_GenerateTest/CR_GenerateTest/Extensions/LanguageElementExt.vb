Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices

Public Module LanguageElementExt
    <Extension()> _
    Public Function GenerateCode(ByVal Source As LanguageElement, ByVal Language As String) As String
        Dim Result As String = String.Empty
        Dim CodeElement = TryCast(Source, CodeElement)
        If HasAttributeSections(CodeElement) Then
            Dim Section As AttributeSection = TryCast(CodeElement.AttributeSections(0), AttributeSection)
            'Result &= CodeRush.Language.GenerateElement(Section, Language) & CodeRush.Language.ActiveExtension.LineContinuationCharacter
            Section.SetTarget(Source)
            Result &= CodeRush.CodeMod.GenerateCode(Section, True) '& CodeRush.Language.ActiveExtension.LineContinuationCharacter
            'ShowCode(Section)
        End If
        Result &= CodeRush.Language.GenerateElement(Source, Language)
        Return Result
    End Function
    Private Function ShowCode(ByVal Section As AttributeSection) As String
        Dim Gen As New VB.VBCodeGen()
        Dim SupportGen = New VB.VBSupportElementCodeGen(Gen)
        SupportGen.GenerateAttributes(Section)
        Return Gen.Code.Code
    End Function
    Private Function HasAttributeSections(ByVal CodeElement As CodeElement) As Boolean
        Return CodeElement IsNot Nothing AndAlso CodeElement.AttributeSectionsCount > 0
    End Function
    <Extension()> _
    Public Function GenerateCode(ByVal Source As LanguageElement) As String
        Return GenerateCode(Source, Source.Project.Language)
    End Function
    <Extension()> _
    Public Function AsAssignment(ByVal Element As LanguageElement) As Assignment
        Return TryCast(Element, Assignment)
    End Function

    <Extension()> _
    Public Function AsIEventElement(ByVal Element As IElement) As IEventElement
        Return TryCast(Element, IEventElement)
    End Function
End Module