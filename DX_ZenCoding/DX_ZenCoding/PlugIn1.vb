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
    Private Const STR_VALIDCHARS As String = "$.:>[]*+#1234567890ABCDEFGHIJKLMNOPRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
    Private Const CHAR_ID As Char = "#"c
    Private Const CHAR_Class As Char = "."c
    Private Const CHAR_ContainerSeparator As Char = ">"c
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
            Try
                CodeRush.Source.BeginUpdate()
                Dim Line = CodeRush.Documents.ActiveLine.Trim
                Dim ZenRange = GetZenRange(CodeRush.Documents.ActiveLine, CodeRush.Caret.Line)
                CodeRush.Documents.ActiveTextDocument.DeleteText(ZenRange)
                Call ExpandLevelsAtCaret(ExpressionToLevels(Line))
            Finally
                CodeRush.Source.EndUpdate()
            End Try
        End Using
    End Sub
    Private Function ExpressionToLevels(ByVal Line As String) As LinkedList(Of String)
        Return New LinkedList(Of String)(Line.Split(CHAR_ContainerSeparator))
    End Function
    '''' <summary>Processes a single (sibling) expression</summary>
    'Private Function ProcessExpression(ByVal Expression As String) As SourcePoint
    '    ' Example: div>div+div>div*2+div*4 
    '    ' Each > indicates another level of container.
    '    ' Each container is procesed separately then sub containers are added to it.
    '    Dim OuterContainer = GetOuterContainer(Expression, CHAR_ContainerSeparator)
    '    Dim Content = GetInnerContent(Expression, CHAR_ContainerSeparator)
    '    Dim Siblings As String() = OuterContainer.Split("+"c)
    '    Dim CaretPoint As SourcePoint = Nothing
    '    Dim CurrentSibling As Integer = 0
    '    For Each Sibling As String In Siblings
    '        ' Process each (> seperated) section in turn.
    '        Dim Remainder = If(IsLastItemInList(CurrentSibling, Siblings), Content, "")
    '        ' Expand section into the correct number of containers. 
    '        CaretPoint = ExpandMultiplicity(Sibling, Remainder)
    '        If Siblings.Count > 1 Then
    '            CodeRush.Caret.MoveTo(CaretPoint)
    '        End If
    '        CurrentSibling = CurrentSibling + 1
    '    Next
    '    Return CaretPoint
    'End Function

    Private Function ExpandLevelsAtCaret(ByVal Levels As LinkedList(Of String)) As SourcePoint
        ' Example: div>div+div>div*2+div*4 
        ' Each > indicates another level of container.
        ' Each container is procesed separately then sub containers are added to it.


        Levels = New LinkedList(Of String)(Levels.ToArray)

        Dim Top As String = Levels.First.Value
        Dim TopSiblings As String() = Top.Split("+"c)
        Call Levels.RemoveFirst()
        Dim Remainder = Levels
        Dim CaretPoint As SourcePoint = Nothing
        For Each Sibling As String In TopSiblings
            ' Process each (+ seperated) section in turn.
            ' Expand section into the correct number of containers. 
            CaretPoint = ExpandItem(Sibling, Remainder)
            If TopSiblings.Count > 1 Then
                CodeRush.Caret.MoveTo(CaretPoint)
            End If
        Next
        Return CaretPoint
    End Function

    Private Function IsLastItemInList(ByVal SiblingIndex As Integer, ByVal Siblings As String()) As Boolean
        Return SiblingIndex = Siblings.Length - 1
    End Function
    '''' <summary>
    ''''  Expands Section as many times as multiplicity suggests.
    ''''  Each expansion is also fed child sections
    '''' </summary>
    '''' <param name="Section"></param>
    '''' <param name="Remainder"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Function ExpandMultiplicity(ByVal Section As String, ByVal Remainder As String) As SourcePoint
    '    ' Expands
    '    Dim ParsedSection = ExtractBase(Section)
    '    Dim IsMultiplicity = ParsedSection.Multiplicity > 1
    '    Dim LastPoint As SourcePoint
    '    For Interation = 1 To ParsedSection.Multiplicity
    '        LastPoint = ExpandBasePiece(ParsedSection.Section, Interation).End
    '        Dim SavedPoint As SourcePoint = LastPoint
    '        If Remainder.Length > 0 Then
    '            SavedPoint = SavePoint(LastPoint, CodeRush.Documents.ActiveTextDocument)
    '            ProcessExpression(Remainder)
    '            LastPoint = SavedPoint
    '        End If
    '        If IsMultiplicity Then
    '            CodeRush.Caret.MoveTo(SavedPoint)
    '        End If
    '    Next
    '    Return LastPoint
    'End Function
    Private Function ExpandItem(ByVal Item As String, ByVal Content As LinkedList(Of System.String)) As SourcePoint
        Dim ParsedSection = New ParsedSection(Item)
        ParsedSection.LocateTemplate()
        Dim LastPoint As SourcePoint
        For Interation = 1 To ParsedSection.Multiplicity
            LastPoint = ExpandBasePiece(ParsedSection, Interation).End
            Dim SavedPoint As SourcePoint = LastPoint
            If Content.Count > 0 Then
                SavedPoint = SavePoint(LastPoint, CodeRush.Documents.ActiveTextDocument)
                ExpandLevelsAtCaret(Content)
                LastPoint = SavedPoint
            End If
            If ParsedSection.Multiplicity > 1 Then
                CodeRush.Caret.MoveTo(SavedPoint)
            End If
        Next
        Return LastPoint
    End Function

    Private Function ExpandBasePiece(ByVal Content As ParsedSection, ByVal Iteration As Integer) As SourceRange
        Try
            SetTemplateVariable("ZenCount", If(Iteration > 0, CStr(Iteration), ""))
            'Dim ContentAttribute As ContentAttribute = New ContentAttribute(Content.Base, "")
            'If ContentAttribute.Content.Contains(CHAR_ID) Then
            '    ContentAttribute = ParseIDPiece(ContentAttribute.Content)
            'ElseIf ContentAttribute.Content.Contains(CHAR_Class) Then
            '    ContentAttribute = ParseClassPiece(ContentAttribute.Content)
            'ElseIf ContentAttribute.Content.Contains("["c) Then
            '    ' [ means attributes
            '    Dim Attributes = ContentAttribute.Content.Contents("["c, "]"c)
            '    Dim Base = ContentAttribute.Content.Replace(Attributes, "").Replace("[]", "")
            '    ContentAttribute = New ContentAttribute(Base, Attributes)
            'End If
            Dim ZenAttributes = Content.GetAttributeWithIteration(Iteration)
            SetTemplateVariable("ZenAttribute", If(ZenAttributes = String.Empty, "", ZenAttributes))
            Dim ExpansionRange
            If Content.Template Is Nothing Then
                ExpansionRange = Nothing
            Else
                ExpansionRange = Content.Template.Expand()
            End If
            SetTemplateVariable("ZenAttribute", "")
            SetTemplateVariable("ZenCount", "")
            Return ExpansionRange
        Catch ex As Exception
            Throw
        End Try
    End Function

    'Private Function ParseClassPiece(ByVal Content As String) As ContentAttribute
    '    Dim Attribute As String = String.Empty
    '    Dim OuterContainer = GetOuterContainer(Content, CHAR_Class)
    '    Dim InnerContent = GetInnerContent(Content, CHAR_Class)
    '    If InnerContent <> String.Empty Then
    '        ' Generated a list of classes from Right
    '        Attribute = String.Format("class='{0}'", InnerContent.Replace(".", " "))
    '    End If
    '    'Content = OuterContainer
    '    Return New ContentAttribute(OuterContainer, Attribute)

    'End Function
    'Private Function ParseIDPiece(ByVal Content As String) As ContentAttribute
    '    Dim Attribute As String = String.Empty
    '    Dim OuterContainer = GetOuterContainer(Content, CHAR_ID)
    '    Dim InnerContent = GetInnerContent(Content, CHAR_ID)
    '    If InnerContent <> String.Empty Then
    '        ' somehow allocate an Id to expanded item
    '        Attribute = String.Format("id='{0}'", InnerContent)
    '    End If
    '    'Content = OuterContainer
    '    Return New ContentAttribute(OuterContainer, Attribute)

    'End Function
#End Region

End Class
