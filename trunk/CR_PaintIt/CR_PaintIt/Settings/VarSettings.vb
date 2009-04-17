Imports System
Imports System.Drawing
Imports DevExpress.Coderush.StructuralParser
Imports CR_PaintIt.Painting
Public Class VarSettings
    Inherits MemberSettings

    Public [Local] As New PaintOptions
    Public [ParamIn] As New PaintOptions
    Public [ParamOut] As New PaintOptions
    Public [ParamArray] As New PaintOptions
    Public [ParamRef] As New PaintOptions
    Public Enum ParamQualifier
        [In]
        [Out]
        [Ref]
    End Enum
    Public Overrides Function GetScopedOptions(ByVal Member As IMemberElement) As PaintOptions
        If Member Is Nothing Then
            Return Nothing
        End If
        If Member.Visibility <> MemberVisibility.Local Then
            Return MyBase.GetScopedOptions(Member)
        End If
        If Not (TypeOf Member Is Param) Then
            Return [Local]
        End If
        Select Case CType(Member, Param).Direction
            Case ArgumentDirection.In
                Return ParamIn
            Case ArgumentDirection.Out
                Return ParamOut
            Case ArgumentDirection.ParamArray
                Return [ParamArray]
            Case ArgumentDirection.Ref
                Return ParamRef
        End Select
    End Function
End Class
