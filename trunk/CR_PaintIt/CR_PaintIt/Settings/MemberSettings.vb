Imports DevExpress.Coderush.StructuralParser
Imports Painting

Public Class MemberSettings
    Public [Public] As New PaintOptions
    Public [Protected] As New PaintOptions
    Public [ProtectedInternal] As New PaintOptions
    Public [Internal] As New PaintOptions
    Public [Private] As New PaintOptions

    Public Overridable Function GetScopedOptions(ByVal Member As IMemberElement) As PaintOptions
        If Member Is Nothing Then
            Return Nothing
        End If
        Select Case Member.Visibility
            Case MemberVisibility.Public
                Return [Public]
            Case MemberVisibility.ProtectedInternal
                Return [ProtectedInternal]
            Case MemberVisibility.Internal
                Return [Internal]
            Case MemberVisibility.Protected
                Return [Protected]
            Case MemberVisibility.Private
                Return [Private]
        End Select
    End Function
End Class
