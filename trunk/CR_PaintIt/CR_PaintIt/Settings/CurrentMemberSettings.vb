Imports System
Imports System.Drawing
Imports CR_PaintIt.Painting

Public Class CurrentMemberSettings
    Public Enabled As Boolean
    Public DetectionMethod As DetectionMethod
    Public CurrentMemberExactOptions As New PaintOptions(PaintRequestEnum.BrushStroke, Color.Blue, 100)
    Public CurrentMemberSiblingOptions As New PaintOptions(PaintRequestEnum.BrushStroke, Color.Blue, 255, Color.Blue, 100)
End Class
