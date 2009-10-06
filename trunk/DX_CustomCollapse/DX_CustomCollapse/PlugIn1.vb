Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices


Public Class PlugIn1

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()

        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

    Private Sub actCustomCollapse_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles actCustomCollapse.Execute
        Dim Exclusions As LanguageElementType() = {LanguageElementType.Namespace, LanguageElementType.Class, LanguageElementType.Struct, LanguageElementType.Region}
        For Each Node In GetElements(CodeRush.Source.ActiveFileNode, Function(e) Not e.ElementType.In(Exclusions) AndAlso e.IsCollapsible)
            Node.Collapse()
        Next
    End Sub
    Private Function GetElements(ByVal Scope As LanguageElement, Optional ByVal Proc As System.Func(Of LanguageElement, Boolean) = Nothing) As IEnumerable(Of LanguageElement)
        Dim Enumerator = New ElementEnumerable(Scope, True).OfType(Of LanguageElement)()
        If Proc Is Nothing Then
            Return Enumerator
        End If
        Return Enumerator.Where(Proc)
    End Function
    Private Function GetElements(ByVal Scope As LanguageElement, ByVal ElementType() As LanguageElementType, Optional ByVal Proc As System.Func(Of LanguageElement, Boolean) = Nothing) As IEnumerable(Of LanguageElement)
        Dim Enumerator = New ElementEnumerable(Scope, New ElementTypeFilter(ElementType), True).OfType(Of LanguageElement)()
        If Proc Is Nothing Then
            Return Enumerator
        End If
        Return Enumerator.Where(Proc)
    End Function


End Class

Public Module GeneralExt
    <Extension()> _
    Public Function [In](ByVal Source As LanguageElementType, ByVal List As IEnumerable(Of LanguageElementType)) As Boolean
        For Each Item In List
            If Item = Source Then
                Return True
            End If
        Next
        Return False
    End Function
End Module