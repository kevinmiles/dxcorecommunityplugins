Imports System
Imports System.Drawing


Public Interface IPaintColor
    Property Base() As System.Drawing.Color
    Property Opacity() As Integer
    ReadOnly Property TrueColor() As System.Drawing.Color
End Interface