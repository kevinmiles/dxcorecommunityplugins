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
    Private Const STR_VALIDCHARS As String = "$.:>*+#1234567890ABCDEFGHIJKLMNOPRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
    Private Const CHAR_ID As Char = "#"c
    Private Const CHAR_Class As Char = "."c
    Private Const CHAR_LevelIndicator As Char = ">"c
    Private Const CHAR_SiblingExpression As Char = "+"c
#End Region
#Region "Usual DXCore magic"
    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        CreateZenExpand()
        CreateZenExpressionContext()
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
#Region "ZenExpandContext"
    Private Sub CreateZenExpressionContext()
        Dim ZenContext As New DevExpress.CodeRush.Extensions.ContextProvider(components)
        CType(ZenContext, System.ComponentModel.ISupportInitialize).BeginInit()
        'ZenContext.Categor "Zen"
        ZenContext.ProviderName = "ZenContext"
        ZenContext.DisplayName = "Is Zen Expression"
        ZenContext.Description = "Returns true if the caret is to the right of a Zen Expression"
        ZenContext.ProviderName = "IsZenExpression"
        ZenContext.Register = True

        AddHandler ZenContext.ContextSatisfied, AddressOf ZenExpand_ContextSatisfied

        CType(ZenContext, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub ZenExpand_ContextSatisfied(ByVal ea As ContextSatisfiedEventArgs)
        Dim Line = CodeRush.Documents.ActiveLine.Trim
        ea.Satisfied = Not Line.Contains(" "c) AndAlso (Line.Contains(">") OrElse Line.Contains("+"))
    End Sub
#End Region
#Region "ZenExpandAction"
    ''' <summary>Create the ZenExpand action and add it to this plugin's components</summary>
    Public Sub CreateZenExpand()
        Dim ZenExpand As New DevExpress.CodeRush.Core.Action(components)
        CType(ZenExpand, System.ComponentModel.ISupportInitialize).BeginInit()
        ZenExpand.ActionName = "ZenExpand"
        ZenExpand.RegisterInCR = True
        AddHandler ZenExpand.Execute, AddressOf ZenExpand_Execute
        CType(ZenExpand, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub ZenExpand_Execute(ByVal ea As ExecuteEventArgs)
        Using Action = CodeRush.Documents.ActiveTextDocument.NewCompoundAction("Expand Zen Expression")
            Dim Line As String = CodeRush.Documents.ActiveLine.Trim
            CodeRush.Documents.ActiveTextDocument.DeleteText(ZenRange)
            Call ProcessExpression(Line)
        End Using
    End Sub
    ''' <summary>Processes a single (sibling) expression</summary>
    Private Function ProcessExpression(ByVal Expression As String) As SourcePoint
        ' div>div+div>div*2+div*4 <-these are levels
        ' Break out first Level
        Dim TopLevel = GetTopmostLevel(Expression, CHAR_LevelIndicator)
        Dim RemainingLevels = GetRightPiece(Expression, CHAR_LevelIndicator)
        Dim Siblings As String() = TopLevel.Split("+"c)
        Dim SiblingEndPoint As SourcePoint
        For S As Integer = 0 To Siblings.Length - 1 Step 1
            Dim Remainder = If(S < Siblings.Length - 1, "", RemainingLevels)
            SiblingEndPoint = ExpandMultiplicity(Siblings(S), Remainder)
            If Siblings.Count > 1 Then
                CodeRush.Caret.MoveTo(SiblingEndPoint)
            End If
        Next
        Return SiblingEndPoint
    End Function
    Private Function ExpandMultiplicity(ByVal TopLevel As String, ByVal RemainingLevels As String) As SourcePoint
        Dim RepeatCount As Integer
        Dim BasePiece = ExtractBase(TopLevel, RepeatCount)
        Dim IsMultiplicity As Boolean = RepeatCount > 1
        Dim LastPoint As SourcePoint
        For x = 1 To RepeatCount
            LastPoint = ExpandBasePiece(BasePiece, x).End
            Dim SavedPoint As SourcePoint = LastPoint
            If RemainingLevels.Length > 0 Then
                SavedPoint = SavePoint(LastPoint, CodeRush.Documents.ActiveTextDocument)
                ProcessExpression(RemainingLevels)
                LastPoint = SavedPoint
            End If
            If IsMultiplicity Then
                CodeRush.Caret.MoveTo(SavedPoint)
            End If
        Next
        Return LastPoint
    End Function
    Private Function ExtractBase(ByVal Source As String, ByRef Count As Integer) As String
        Dim Piece As String
        If Source.Contains("*"c) Then
            Piece = Source.Split("*"c)(0)
            Count = Source.Split("*"c)(1)
        Else
            Piece = Source
            Count = 1
        End If
        Return Piece
    End Function
    Private Function ExpandBasePiece(ByVal Piece As String, ByVal Iteration As Integer) As SourceRange
        SetTemplateVariable("ZenCount", If(Iteration > 0, CStr(Iteration), ""))
        Dim Attribute As String = String.Empty
        If Piece.Contains(CHAR_ID) Then
            Dim LeftPiece = GetTopmostLevel(Piece, CHAR_ID)
            Dim RightPiece = GetRightPiece(Piece, CHAR_ID)
            If RightPiece <> String.Empty Then
                ' somehow allocate an Id to expanded item
                Attribute = String.Format("id='{0}'", RightPiece)
            End If
            Piece = LeftPiece
        ElseIf Piece.Contains(CHAR_Class) Then
            Dim LeftPiece = GetTopmostLevel(Piece, CHAR_Class)
            Dim RightPiece = GetRightPiece(Piece, CHAR_Class)
            If RightPiece <> String.Empty Then
                ' Generated a list of classes from Right
                Attribute = String.Format("class='{0}'", RightPiece.Replace(".", " "))
            End If
            Piece = LeftPiece
        ElseIf Piece.Contains("["c) Then
            Attribute = Piece.Contents("["c, "]"c)
        End If
        For Count = 5 To 1 Step -1
            Attribute = Attribute.Replace(New String("$", Count), Iteration.ToString("D" & Count))
        Next
        SetTemplateVariable("ZenAttribute", Attribute)
        Dim Found As Template = GetFirstTemplateWithName(Piece)
        Dim ExpansionRange As SourceRange
        If Found IsNot Nothing Then
            ExpansionRange = Found.Expand()
        End If
        SetTemplateVariable("ZenAttribute", "")
        SetTemplateVariable("ZenCount", "")
        Return ExpansionRange
    End Function
#End Region
#Region "Utils"
#Region "ExpressionBreaking"
    Private Function GetTopmostLevel(ByVal Expression As String, ByVal Delimiter As Char) As String
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
        Dim TemplateList = CodeRush.Templates.Find(Part, False)
        If TemplateList.Count = 0 Then
            Return Nothing
        End If
        Return TemplateList(0)
    End Function
#End Region
    Private Sub SetTemplateVariable(ByVal VarName As String, ByVal VarValue As String)
        DevExpress.CodeRush.Core.CodeRush.Strings.Get("Set", String.Format("{0}, {1}", VarName, VarValue))
    End Sub
#End Region
End Class