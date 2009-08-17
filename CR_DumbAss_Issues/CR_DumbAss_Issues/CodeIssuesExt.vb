Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices
Imports System.Collections.Generic
Public Enum StyleEnum
    Hint
    Warning
    [Error]
End Enum

Public Module CodeIssuesExt
    <Extension()> _
    Public Sub AddHint(ByVal Source As CheckCodeIssuesEventArgs, ByVal ElementType As LanguageElementType, ByVal Proc As System.Func(Of LanguageElement, Boolean), ByVal Message As String)
        Call Source.Add(StyleEnum.Hint, ElementType, Proc, Message)
    End Sub
    <Extension()> _
    Private Sub Add(ByVal Source As CheckCodeIssuesEventArgs, ByVal Style As StyleEnum, ByVal ElementType As LanguageElementType, ByVal Proc As System.Func(Of LanguageElement, Boolean), ByVal Message As String)
        Dim Elements = GetElements(Source.Scope, ElementType, Proc)
        For Each element As LanguageElement In Elements
            Select Case Style
                Case StyleEnum.Hint
                    Source.AddHint(element.Range, Message)
                Case StyleEnum.Error
                    Source.AddError(element.Range, Message)
                Case StyleEnum.Warning
                    Source.AddWarning(element.Range, Message)
            End Select
        Next
    End Sub
    Private Function GetElements(ByVal Scope As IElement, ByVal ElementType As LanguageElementType, ByVal Proc As System.Func(Of LanguageElement, Boolean)) As IEnumerable(Of LanguageElement)
        Return New ElementEnumerable(Scope, New ElementTypeFilter(ElementType), True).OfType(Of LanguageElement)().Where(Proc)
    End Function
End Module