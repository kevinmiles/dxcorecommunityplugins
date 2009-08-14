Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices
Imports System.Collections.Generic


Public Module ImpossibleEquality
    Public Function Qualifies(ByVal RO As RelationalOperation) As Boolean
        Select Case True
            Case IsToLower(RO.LeftSide) AndAlso IsNonLowercaseString(RO.RightSide)
                Return True
            Case IsToUpper(RO.LeftSide) AndAlso IsNonUppercaseString(RO.RightSide)
                Return True
            Case Else
                Return False
        End Select
        Return IsToLower(RO.LeftSide) AndAlso IsNonLowercaseString(RO.RightSide)
    End Function

#Region "Utils"
    Private Function isPrimitiveString(ByVal Expression As Expression) As Boolean
        If Not TypeOf Expression Is PrimitiveExpression Then
            Return False
        End If
        ' Is Primitive
        Dim Primitive = CType(Expression, PrimitiveExpression)
        If Not Primitive.ExpressionTypeName = "System.String" Then
            Return False
        End If
        Return True
    End Function
#End Region

#Region "Lowercase?"
    Private Function IsToLower(ByVal Expression As Expression) As Boolean
        Return Expression.Name.EndsWith("ToLower")
    End Function
    Private Function IsNonLowercaseString(ByVal Expression As Expression) As Boolean
        If Not isPrimitiveString(Expression) Then
            Return False
        End If
        Dim Primitive = CType(Expression, PrimitiveExpression)
        If Primitive.Name = Primitive.Name.ToLower Then
            Return False
        End If
        Return True
    End Function
#End Region
#Region "Uppercase?"
    Private Function IsToUpper(ByVal Expression As Expression) As Boolean
        Return Expression.Name.EndsWith("ToUpper")
    End Function
    Private Function IsNonUppercaseString(ByVal Expression As Expression) As Boolean
        If Not isPrimitiveString(Expression) Then
            Return False
        End If
        Dim Primitive = CType(Expression, PrimitiveExpression)
        If Primitive.Name = Primitive.Name.ToUpper Then
            Return False
        End If
        Return True
    End Function
#End Region
End Module