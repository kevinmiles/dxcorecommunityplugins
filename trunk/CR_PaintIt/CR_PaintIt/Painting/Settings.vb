Imports System
Imports DevExpress.CodeRush.Common
Imports System.Diagnostics

Namespace Painting
    Public Module Settings
        Const STR_Decoration As String = "Decoration"
        Const STR_Color1 As String = "Color1"
        Const STR_Color2 As String = "Color2"
        Const STR_Base As String = "Base"
        Const STR_Opacity As String = "Opacity"
        Public Sub Copy(ByVal Source As IPaintStyle, ByVal Dest As IPaintStyle)
            Dest.Decoration = Source.Decoration
            Call Copy(Source.Color1, Dest.Color1)
            Call Copy(Source.Color2, Dest.Color2)
        End Sub
        Public Sub Copy(ByVal Source As IPaintColor, ByVal Dest As IPaintColor)
            Dest.Base = Source.Base
            Dest.Opacity = Source.Opacity
        End Sub
        Public Sub SaveStyle(ByVal Storage As IDecoupledStorage, ByVal Section As String, ByVal Prefix As String, ByVal PaintOptions As IPaintStyle)
            Try
                Storage.WriteEnum(Section, Prefix & STR_Decoration, PaintOptions.Decoration)
                Call SaveIColor(Storage, Section, Prefix & STR_Color1, PaintOptions.Color1)
                Call SaveIColor(Storage, Section, Prefix & STR_Color2, PaintOptions.Color2)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Throw
            End Try
        End Sub
        Public Sub LoadStyle(ByVal Storage As IDecoupledStorage, ByVal Section As String, ByVal Prefix As String, ByVal PaintStyle As IPaintStyle, ByVal DefaultPaintStyle As IPaintStyle)
            PaintStyle.Decoration = CType(Storage.ReadEnum(Section, Prefix & STR_Decoration, GetType(PaintRequestEnum), DefaultPaintStyle.Decoration), PaintRequestEnum)
            LoadIColor(Storage, Section, Prefix & STR_Color1, PaintStyle.Color1, DefaultPaintStyle.Color1)
            LoadIColor(Storage, Section, Prefix & STR_Color2, PaintStyle.Color2, DefaultPaintStyle.Color2)
        End Sub
        Public Sub SaveIColor(ByVal Storage As IDecoupledStorage, ByVal Section As String, ByVal Prefix As String, ByVal Color As IPaintColor)
            Try
                Storage.WriteColor(Section, Prefix & STR_Base, Color.Base)
                Storage.WriteInt32(Section, Prefix & STR_Opacity, Color.Opacity)
            Catch ex As Exception
                Debug.WriteLine(ex.Message)
                Throw
            End Try
        End Sub
        Public Sub LoadIColor(ByVal Storage As IDecoupledStorage, ByVal Section As String, ByVal Prefix As String, ByVal Color As IPaintColor, ByVal DefaultColor As IPaintColor)
            Color.Base = Storage.ReadColor(Section, Prefix & STR_Base, DefaultColor.Base)
            Color.Opacity = Storage.ReadInt32(Section, Prefix & STR_Opacity, DefaultColor.Opacity)
        End Sub
    End Module
End Namespace
