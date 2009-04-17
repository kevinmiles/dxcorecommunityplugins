Option Strict On
Imports System
Imports System.Drawing
Imports DevExpress.CodeRush.Common
Imports System.Diagnostics
Namespace Painting
    Public Interface IPaintStyle
        Property Decoration() As PaintRequestEnum
        Property Color1() As IPaintColor
        Property Color2() As IPaintColor
    End Interface
End Namespace
