Option Strict On
Imports System
Imports System.Drawing

Namespace Painting
    Public Class PaintColor
        Implements IPaintColor
        Private mBase As Color
        Private mOpacity As Integer
        Public Sub New(ByVal BaseColor As Color, ByVal Opacity As Integer)
            mBase = Basecolor
            mOpacity = Opacity
        End Sub
        Public Sub New(ByVal BaseColor As Color)
            MyClass.New(BaseColor, 255)
        End Sub
        Public Property Base() As System.Drawing.Color Implements IPaintColor.Base
            Get
                Return mBase
            End Get
            Set(ByVal Value As System.Drawing.Color)
                mBase = Value
            End Set
        End Property
        Public Property Opacity() As Integer Implements IPaintColor.Opacity
            Get
                Return mOpacity
            End Get
            Set(ByVal Value As Integer)
                mOpacity = Value
            End Set
        End Property
        Public ReadOnly Property TrueColor() As System.Drawing.Color Implements IPaintColor.TrueColor
            Get
                Return Combine(mOpacity, mBase)
            End Get
        End Property
        Private Function Combine(ByVal Opacity As Integer, ByVal Color As Color) As Color
            If Color.IsEmpty Then
                Return Color.Empty
            Else
                Return Color.FromArgb(Opacity, Color)
            End If
        End Function
    End Class
End Namespace
