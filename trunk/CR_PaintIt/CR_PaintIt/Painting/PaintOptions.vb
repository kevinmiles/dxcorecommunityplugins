Option Strict On
Imports System
Imports System.Drawing
Namespace Painting
    Public Class PaintOptions
        Implements IPaintStyle

        Const DEFAULT_Opacity As Integer = 255
#Region "Fields"
        Private mEnabled As Boolean = True
        Private mDecoration As PaintRequestEnum
        Private mColor1 As IPaintColor = New Painting.PaintColor(Color.Black)
        Private mColor2 As IPaintColor = New Painting.PaintColor(Color.Empty)
#End Region
#Region "Writeable Properties"
        Public Property Color1() As IPaintColor Implements IPaintStyle.Color1
            Get
                Return mColor1
            End Get
            Set(ByVal Value As IPaintColor)
                mColor1 = Value
            End Set
        End Property
        Public Property Color2() As IPaintColor Implements IPaintStyle.Color2
            Get
                Return mColor2
            End Get
            Set(ByVal Value As IPaintColor)
                mColor2 = Value
            End Set
        End Property
        Public Property Enabled() As Boolean
            Get
                Return mEnabled
            End Get
            Set(ByVal Value As Boolean)
                mEnabled = Value
            End Set
        End Property
        Public Property Decoration() As PaintRequestEnum Implements IPaintStyle.Decoration
            Get
                Return mDecoration
            End Get
            Set(ByVal Value As PaintRequestEnum)
                mDecoration = Value
            End Set
        End Property
#End Region
#Region "Constructors"
        Public Sub New()
            MyClass.New(PaintRequestEnum.TextOverlay)
        End Sub
        Public Sub New(ByVal Style As PaintRequestEnum)
            MyClass.New(Style, Color.Black)
        End Sub
        Public Sub New(ByVal Style As PaintRequestEnum, ByVal Color1Base As Color)
            MyClass.New(Style, Color1Base, DEFAULT_Opacity, Color.Empty, DEFAULT_Opacity)
        End Sub
        Public Sub New(ByVal Style As PaintRequestEnum, ByVal Color1Base As Color, ByVal Color1Opacity As Integer)
            MyClass.New(Style, Color1Base, Color1Opacity, Color.Empty, DEFAULT_Opacity)
        End Sub
        Public Sub New(ByVal Style As PaintRequestEnum, ByVal Color1Base As Color, ByVal Color2Base As Color)
            MyClass.New(Style, Color1Base, DEFAULT_Opacity, Color2Base, DEFAULT_Opacity)
        End Sub
        Public Sub New(ByVal Style As PaintRequestEnum, ByVal Color1Base As Color, ByVal Color1Opacity As Integer, ByVal Color2Base As Color, ByVal Color2Opacity As Integer)
            Me.Decoration = Style
            Me.Color1.Base = Color1Base
            Me.Color1.Opacity = Color1Opacity
            Me.Color2.Base = Color2Base
            Me.Color2.Opacity = Color2Opacity
        End Sub
#End Region
    End Class
End Namespace

