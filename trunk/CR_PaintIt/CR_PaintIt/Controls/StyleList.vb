Imports Painting
Imports System

Namespace Controls
    Public Class StyleList
        Inherits System.Windows.Forms.ComboBox
        Public Sub New()
            MyBase.New()
            Me.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.Items.Clear()
            For Each Value As PaintRequestEnum In System.Enum.GetValues(GetType(PaintRequestEnum))
                Me.Items.Add(Value)
            Next
            CurrentStyle = PaintRequestEnum.TextHighlight
        End Sub
        Public Property CurrentStyle() As PaintRequestEnum
            Get
                Return CType(Me.SelectedItem, PaintRequestEnum)
            End Get
            Set(ByVal Value As PaintRequestEnum)
                SelectedItem = Value
            End Set
        End Property
        Private Function StringToPaintStyle(ByVal PaintStyle As String) As PaintRequestEnum
            Try
                Return CType(System.Enum.Parse(GetType(PaintRequestEnum), RemoveSpaces(PaintStyle)), PaintRequestEnum)
            Catch ex As Exception
                Return PaintRequestEnum.BrushStroke
            End Try
        End Function
        Private Function PaintStyleToString(ByVal PaintStyle As PaintRequestEnum) As String
            Return SeperateWords(PaintStyle.ToString)
        End Function
    End Class
End Namespace

