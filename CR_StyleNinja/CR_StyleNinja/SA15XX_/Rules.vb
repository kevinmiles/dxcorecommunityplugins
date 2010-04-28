Imports System.ComponentModel
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Core
Imports System.Diagnostics.CodeAnalysis
Namespace SA15XX
    Friend Module Rules
#Region "Utility"
        Private MemberOrType As LanguageElementType() = New LanguageElementType() {LanguageElementType.Method, _
                                                                                   LanguageElementType.Class, _
                                                                                   LanguageElementType.Struct, _
                                                                                   LanguageElementType.Interface}
        Friend Function IsMemberOrType(ByVal CodeActive As LanguageElement) As Boolean
            Return MemberOrType.Contains(CodeActive.ElementType)
        End Function
        Friend Function HasExplicityVisibility(ByVal Element As AccessSpecifiedElement) As Boolean
            Return Element.VisibilityRange <> SourceRange.Empty
        End Function
#End Region

#Region "SA1507"
        Public Const Message_SA1507 As String = "SA1507 - Multiple Blank Lines are Bad."
        Public Sub Available_SA1507(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
            ea.Available = Qualifies_SA1507(ea.CodeActive)
        End Sub
        Public Function Qualifies_SA1507(ByVal Element As IElement) As Boolean
            Dim Expression = TryCast(Element, BaseReferenceExpression)
            If Expression Is Nothing Then
                Return False
            End If
            Dim MethodReference = TryCast(Expression.Parent, MethodReferenceExpression)
            Dim BaseMethod = MethodReference.GetMethod()
            Dim LocalMethods = MethodReference.GetClass.AllMethods().Cast(Of Method)()
            Dim LocalMethod = LocalMethods.Where(Function(m) m.IsOverride _
                                                 AndAlso XOverridesY(m, BaseMethod)).FirstOrDefault
            Return LocalMethod Is Nothing
        End Function
        Private Function XOverridesY(ByVal MethodX As Method, ByVal MethodY As Method) As Boolean
            ' X IsOverride
            If Not MethodX.IsOverride Then
                Return False
            End If
            ' Name of X = Name of Y
            If Not MethodX.Name = MethodY.Name Then
                Return False
            End If
            ' Class Prep
            Dim ClassX As [Class] = MethodX.GetClass
            Dim ClassY As [Class] = MethodY.GetClass
            ' ClassOf X descends from Class of Y
            If Not ClassX.DescendsFrom(ClassY) Then
                Return False
            End If
            ' Return Value of X = Return Value of Y
            If Not MethodX.MemberType = MethodY.MemberType Then
                Return False
            End If
            ' X Param Types = Y Param Types
            If Not ParamStringOf(MethodX) = ParamStringOf(MethodY) Then
                Return False
            End If
            Return True
        End Function
        Private Function ParamStringOf(ByVal Method As Method) As String
            Dim Result As String = String.Empty
            For Each Param As Parameter In Method.Parameters
                Result &= Param.Type.Name & "|"
            Next
            Return Result
        End Function

        Public Sub Fix_SA1507(ByVal sender As Object, ByVal ea As ApplyContentEventArgs)
            ea.TextDocument.DeleteText(ea.CodeActive.Range())
        End Sub
#End Region
    End Module
End Namespace
