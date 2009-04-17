Imports CR_PaintIt.Painting
Imports System.Collections
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Imports System

Public Class LanguageElementPaintOptions
    Public Element As LanguageElement
    Public PaintOptions As PaintOptions
    Public Sub New(ByVal Element As LanguageElement, ByVal PaintOptions As PaintOptions)
        Me.Element = Element
        Me.PaintOptions = PaintOptions
    End Sub
End Class
