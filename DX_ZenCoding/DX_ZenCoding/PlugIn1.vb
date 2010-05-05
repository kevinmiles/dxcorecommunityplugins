Imports System.Linq
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices

Public Class PlugIn1
#Region "Constants"
    Private Const STR_VALIDCHARS As String = ":>*+#1234567890ABCDEFGHIJKLMNOPRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
#End Region
#Region "Usual DXCore magic"

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        CreateZenExpand()
        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region
#End Region
#Region "Initialize"
    ''' <summary>Create the ZenExpand action and add it to this plugin's components</summary>
    Public Sub CreateZenExpand()
        Dim ZenExpand As New DevExpress.CodeRush.Core.Action(components)
        CType(ZenExpand, System.ComponentModel.ISupportInitialize).BeginInit()
        ZenExpand.ActionName = "ZenExpand"
        ZenExpand.RegisterInCR = True
        AddHandler ZenExpand.Execute, AddressOf ZenExpand_Execute
        CType(ZenExpand, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
#End Region
#Region "High level ZenExecute"
    Private Sub ZenExpand_Execute(ByVal ea As ExecuteEventArgs)
        Call ZenExpand(CodeRush.Documents.ActiveLine.Trim)
    End Sub
    ''' <summary>Expands a zen expression at the caret</summary>
    Private Sub ZenExpand(ByVal Source As String)
        Using Action = CodeRush.Documents.ActiveTextDocument.NewCompoundAction("Expand Zen Expression")
            Dim Parts = Source.Split("+"c)
            CodeRush.Documents.ActiveTextDocument.DeleteText(ZenRange)
            For Each Part In Parts
                Dim ExpressionPoint = ProcessExpression(Part)
                If Parts.Count > 1 Then
                    CodeRush.Caret.MoveTo(ExpressionPoint)
                End If
            Next
        End Using
    End Sub
#End Region

#Region "Utils"
#Region "ExpressionBreaking"
    Private Function GetLeftPiece(ByVal Expression As String, ByVal Delimiter As Char) As String
        Dim Pos As Integer = Expression.IndexOf(Delimiter)
        Return If(Pos = -1, Expression, Expression.Substring(0, Pos))
    End Function
    Private Function GetRightPiece(ByVal Expression As String, ByVal Delimiter As Char) As String
        Dim Pos As Integer = Expression.IndexOf(Delimiter)
        Return If(Pos = -1, "", Expression.Substring(Pos + 1))
    End Function
#End Region
#Region "SourcePointOps"
    Private Function RestorePoint(ByVal SavedPoint As SourcePoint) As SourcePoint
        CodeRush.Caret.MoveTo(SavedPoint)
        Return SavedPoint
    End Function
    Private Function SavePoint(ByVal Point As SourcePoint, ByVal Document As TextDocument) As SourcePoint
        Dim NewPoint As SourcePoint = New SourcePoint(Point)
        NewPoint.BindToCode(Document, True)
        Return NewPoint
    End Function
#End Region
#Region "Zen Locations"
    ''' <summary>Full range of the Zen Expression</summary>
    Public Function ZenRange() As SourceRange
        Dim StartLine As Integer = CodeRush.Caret.Line
        Dim TheLine As String = CodeRush.Documents.ActiveTextDocument.GetLine(StartLine)
        Dim LeftRight As Integer = LocateZenStart(TheLine)
        Dim StartPoint As SourcePoint = New SourcePoint(StartLine, LeftRight)
        Return New SourceRange(StartPoint, CodeRush.Caret.SourcePoint)
    End Function
    ''' <summary>Locates the Start of the Zen Expression</summary> 
    Private Function LocateZenStart(ByVal Line As String) As Integer
        For Position = Line.Count - 1 To 1 Step -1
            If Not STR_VALIDCHARS.Contains(Line.Substring(Position, 1)) Then
                'Position is the 0 based location of the first character with is illegal Zen
                Return Position + 2 'The 1-based position of the last Zen Legal character.
            End If
        Next
        Return 1
    End Function
#End Region
#Region "TemplateOps"
    ''' <summary>Locates First Template with given Name</summary>
    Private Function GetFirstTemplateWithName(ByVal Part As String) As Template
        Return CodeRush.Templates.Find(Part, False)(0)
    End Function
#End Region
#End Region

    ''' <summary>Processes a single (sibling) expression</summary>
    Private Function ProcessExpression(ByVal Expression As String) As SourcePoint
        ' ProcessFirstPiece Inject code at current location and return a SourceRange.
        ' break expression at first >
        Dim CaretPoint As SourcePoint
        Dim Count As Integer
        Dim Parent As String
        If Expression.Contains(">"c) Then
            ' Break off leftmost piece 
            Dim ParentPiece = GetLeftPiece(Expression, ">"c)
            Count = GetCount(ParentPiece, Parent)
            ' Split Expression
            For x = 1 To Count
                Dim ParentRange = ExpandPiece(Parent)
                Dim ChildExpression = GetRightPiece(Expression, ">"c)
                CaretPoint = ProcessChildExpression(ChildExpression, ParentRange, Count > 1)
            Next
        Else
            Count = GetCount(Expression, Parent)
            For x = 1 To Count
                Dim ExpansionRange = ExpandPiece(Parent)
                CaretPoint = ExpansionRange.End
                If Count > 1 Then
                    CodeRush.Caret.MoveTo(CaretPoint)
                End If
            Next
        End If
        Return CaretPoint
    End Function
    Private Function ProcessChildExpression(ByVal ChildExpression As String, ByVal ParentRange As SourceRange, ByVal RestoreToEnd As Boolean) As SourcePoint
        Dim CaretPoint As SourcePoint
        Dim SavedPoint = SavePoint(ParentRange.End, CodeRush.Documents.ActiveTextDocument)
        Call ProcessExpression(ChildExpression)
        If RestoreToEnd Then
            CodeRush.Caret.MoveTo(ParentRange.End)
        End If
        CaretPoint = RestorePoint(SavedPoint)
        Return CaretPoint
    End Function
    Private Function GetCount(ByVal ParentPiece As String, ByRef Piece As String) As Integer
        Dim Count As Integer
        If ParentPiece.Contains("*"c) Then
            Piece = ParentPiece.Split("*"c)(0)
            Count = ParentPiece.Split("*"c)(1)
        Else
            Piece = ParentPiece
            Count = 1
        End If
        Return Count
    End Function
    Private Function ExpandPiece(ByVal Piece As String) As SourceRange
        Dim Attribute As String
        If Piece.Contains("#"c) Then
            Dim LeftPiece = GetLeftPiece(Piece, "#"c)
            Dim RightPiece = GetRightPiece(Piece, "#"c)
            If RightPiece = String.Empty Then
                ' somehow allocate an Id to expanded item
                Attribute = String.Format("id='{0}'", RightPiece)
            End If
        ElseIf Piece.Contains("."c) Then
            Dim LeftPiece = GetLeftPiece(Piece, "."c)
            Dim RightPiece = GetRightPiece(Piece, "."c)
            If RightPiece = String.Empty Then
                ' Generated a list of classes from Right
                Attribute = String.Format("class='{0}'", RightPiece.Replace(".", " "))
            End If
        ElseIf Piece.Contains("["c) Then
            Attribute = Piece.Contents("["c, "]"c)
        End If

        Dim Found As Template = GetFirstTemplateWithName(Piece)
        Dim ExpansionRange As SourceRange
        If Found IsNot Nothing Then
            ExpansionRange = Found.Expand()
        End If
        Return ExpansionRange
    End Function
    Private Function ProcessPiece(ByVal Expression As String) As SourceRange
        ' Piece defined for now as either div or div*5
        ' if Div then just expand and then call
        Dim LastRange As SourceRange = Nothing
        Dim Parts = Expression.Split("*"c)
        Dim Count As Integer = If(Parts.Count = 1, 1, CInt(Parts(1)))
        Dim Found As Template = GetFirstTemplateWithName(Parts(0))
        For x = 1 To Count
            If Found IsNot Nothing Then
                LastRange = Found.Expand()
                If Count > 1 Then
                    CodeRush.Caret.MoveTo(LastRange.End)
                End If
            End If
        Next
        Return LastRange
    End Function
End Class
Public Module ViewExt
    ''' <summary>Treat Source as a Template name and expand it into the active view</summary>
    <Extension()> _
    Public Function Expand(ByVal Source As Template) As SourceRange
        Dim View = CodeRush.TextViews.Active
        Return View.TextDocument.ExpandText(View.Caret.SourcePoint, Source.FirstItemInContext.Expansion)
    End Function
End Module
Public Module StringExt
    <Extension()> _
    Public Function Contents(ByVal Source As String, ByVal StartChar As String, ByVal EndChar As String) As String
        Dim StartPoint As Integer = Source.IndexOf(StartChar) + 1
        Dim EndPoint As Integer = Source.IndexOf(EndChar) - 1
        Return Source.Substring(StartPoint, EndPoint - StartPoint)
    End Function
End Module