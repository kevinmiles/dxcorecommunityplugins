Imports System
Imports System.Drawing
Imports DevExpress.CodeRush.Common
Imports System.Diagnostics

Namespace Painting
    Public Interface IPaintColor
        Property Base() As Color
        Property Opacity() As Integer
        ReadOnly Property TrueColor() As Color
    End Interface
End Namespace
