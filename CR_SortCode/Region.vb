Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser


Public Class LayoutNode

    Public Function HasCode() As Boolean
        Dim res As Boolean
        res = False
        For Each item As Object In Me.LayoutList
            If TypeOf item Is LayoutNode Then
                res = DirectCast(item, LayoutNode).HasCode
                If res Then
                    Return True
                End If
            ElseIf TypeOf item Is NodeFilter Then
                res = DirectCast(item, NodeFilter).Members.Count > 0
                If res = True Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

    Public Shared _CommentList As New List(Of String)
    ''' <summary>
    ''' Initializes a new instance of the Region class.
    ''' </summary>
    ''' <param name="iD"></param>
    ''' <param name="description"></param>
    Public Sub New(ByVal iD As String, ByVal description As String)
        _iD = iD
        _description = description
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the Region class.
    ''' </summary>
    ''' <param name="iD"></param>
    ''' <param name="description"></param>
    ''' <param name="isComment"></param>
    Public Sub New(ByVal iD As String, ByVal description As String, ByVal isComment As Boolean)
        _isComment = isComment
        _iD = iD
        _description = description
        If isComment AndAlso Not _CommentList.Contains(description) Then
            _CommentList.Add(description)
        End If
    End Sub

    Private _isComment As Boolean
    Public Property IsComment() As Boolean
        Get
            Return _isComment
        End Get
        Set(ByVal Value As Boolean)
            _isComment = Value
        End Set
    End Property



    Private _iD As String
    Public Property ID() As String
        Get
            Return _iD
        End Get
        Set(ByVal Value As String)
            _iD = Value
        End Set
    End Property

    Private _description As String
    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal Value As String)
            _description = Value
        End Set
    End Property

    Private _layoutList As New List(Of Object)
    Public Property LayoutList() As List(Of Object)
        Get
            Return _layoutList
        End Get
        Set(ByVal Value As List(Of Object))
            _layoutList = Value
        End Set
    End Property

End Class
