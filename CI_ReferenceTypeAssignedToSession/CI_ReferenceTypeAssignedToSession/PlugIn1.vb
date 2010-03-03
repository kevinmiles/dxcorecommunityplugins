Option Infer On
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

    Private Sub ReferenceAssignedToSession_CheckCodeIssues(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CheckCodeIssuesEventArgs) Handles ReferenceAssignedToSession.CheckCodeIssues
        Dim LElement = TryCast(ea.Scope, LanguageElement)
        If LElement IsNot Nothing Then
            Dim IssueQuery = From Assignment In AssignmentsToSession(LElement) _
                                Where Assignment.Expression.IsReferenceType(False)
            For Each Result In IssueQuery
                ea.AddHint(Result.Range, "Reference Type Assigned to Session")
            Next
        End If
    End Sub
#Region "Gather Assignments"
    Private Function AssignmentsToSession(ByVal Scope As LanguageElement) As IEnumerable(Of Assignment)
        Dim AssignmentsToMethodCall = From Assignment In AssignmentEnumerator(Scope) _
                                 Where TypeOf Assignment.LeftSide Is MethodCallExpression
        Return From Assignment In AssignmentsToMethodCall _
               Where CType(Assignment.LeftSide, MethodCallExpression).Name = "Session"
    End Function
    Private Function AssignmentEnumerator(ByVal Scope As LanguageElement) As IEnumerable(Of Assignment)
        Return New ElementEnumerable(Scope, LanguageElementType.Assignment, True).OfType(Of Assignment)()
    End Function
#End Region

End Class

Public Module ExpressionExt
    <Extension()> _
    Public Function GetTypeDeclaration(ByVal Source As Expression) As IElement
        Return Source.Resolve(New SourceTreeResolver).GetDeclaration()
    End Function
    <Extension()> _
    Public Function IsValueType(ByVal Expression As Expression) As Boolean
        Return TryCast(Expression.GetTypeDeclaration(), IStructElement) IsNot Nothing
    End Function
    <Extension()> _
    Public Function IsValueTypeOrString(ByVal Expression As Expression) As Boolean
        Dim Decl As IElement = Expression.GetTypeDeclaration()
        Dim IsValueType As Boolean = TryCast(Decl, IStructElement) IsNot Nothing
        Dim IsString As Boolean = TryCast(Decl, IClassElement).FullName = "System.String"
        Return IsValueType OrElse IsString
    End Function
    <Extension()> _
    Public Function IsReferenceType(ByVal Expression As Expression, Optional ByVal AllowString As Boolean = True) As Boolean
        Dim Decl As IElement = Expression.GetTypeDeclaration()
        If Not AllowString AndAlso Decl.FullName = "System.String" Then
            Return False
        End If
        Return TryCast(Decl, IClassElement) IsNot Nothing
    End Function
End Module