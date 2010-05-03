Imports System
Imports System.Text
Imports System.Drawing
Imports System.Windows.Forms

Public Class GrowLabel
    Inherits System.Windows.Forms.Label
    Private mGrowing As Boolean

    Public Sub New()
        Me.AutoSize = False
    End Sub
    Private Sub resizeLabel()
        If mGrowing Then
            Exit Sub
        End If
        Try
            mGrowing = True
            Dim sz As New Size(Me.Width, Int32.MaxValue)
            sz = TextRenderer.MeasureText(Me.Text, Me.Font, sz, TextFormatFlags.WordBreak)
            Me.Height = sz.Height + HeightPadding
        Finally
            mGrowing = False
        End Try
    End Sub

    Private _heightPadding As Integer
    Public Property HeightPadding() As Integer
        Get
            Return _heightPadding
        End Get
        Set(ByVal Value As Integer)
            _heightPadding = Value
            resizeLabel()
        End Set
    End Property

    Protected Overloads Overrides Sub OnTextChanged(ByVal e As EventArgs)
        MyBase.OnTextChanged(e)
        resizeLabel()
    End Sub
    Protected Overloads Overrides Sub OnFontChanged(ByVal e As EventArgs)
        MyBase.OnFontChanged(e)
        resizeLabel()
    End Sub
    Protected Overloads Overrides Sub OnSizeChanged(ByVal e As EventArgs)
        MyBase.OnSizeChanged(e)
        resizeLabel()
    End Sub
End Class


