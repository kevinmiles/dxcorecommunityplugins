Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices

Public Module LanguageElementExt
    Private ClassOrStruct As LanguageElementType() = New LanguageElementType() {LanguageElementType.Class, LanguageElementType.Struct}
    <Extension()> _
    Public Function ToLE(ByVal Source As IElement) As LanguageElement
        Return TryCast(Source, LanguageElement)
    End Function

    <Extension()> _
    Friend Function WhereHungarianNotation(Of T As IElement)(ByVal Source As IEnumerable(Of T)) As IEnumerable(Of T)
        Return Source.Where(Function(e) e.Name.IsHungarian)
    End Function
    <Extension()> _
    Friend Function IsHungarian(ByVal Source As String) As Boolean
        Return (Char.IsLower(Source.First) AndAlso Char.IsUpper(Source.Skip(1).First)) _
        OrElse (Char.IsLower(Source.First) AndAlso Char.IsLower(Source.Skip(1).First) AndAlso Char.IsUpper(Source.Skip(2).First))
    End Function


    Friend Function isPrivateReadonly(ByVal e As IMemberElement) As Boolean
        Return e.Visibility = MemberVisibility.Private AndAlso e.IsReadOnly
    End Function
    Friend Function isPublicOrInternal(ByVal e As IMemberElement) As Boolean
        Return e.Visibility = MemberVisibility.Public _
                                         OrElse e.Visibility = MemberVisibility.Friend
    End Function
    Friend Function isNonPrivate(ByVal e As IMemberElement) As Boolean
        Return e.Visibility <> MemberVisibility.Private
    End Function

    Friend Function StartsLower(ByVal V As IElement) As Boolean
        Return Char.IsLower(V.Name.First)
    End Function
    Friend Function StartsUpper(ByVal V As IElement) As Boolean
        Return Char.IsUpper(V.Name.First)
    End Function




End Module