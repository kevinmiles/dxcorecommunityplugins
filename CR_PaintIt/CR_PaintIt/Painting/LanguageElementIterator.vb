Imports CR_PaintIt.Painting
Imports System.Collections
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Imports System

Friend Class LanguageElementIterator
    Implements IEnumerator
    Implements IEnumerable
#Region "Fields"
    Private mPositionStack As New Stack
    Private mStartElement As LanguageElement
    Private mEvaluator As LanguageElementEvaluatorDelegate
#End Region
#Region "Utils"
    <System.Diagnostics.DebuggerStepThrough()> Private Function TopStackElement() As ElementChildMarker
        Return CType(mPositionStack.Peek(), ElementChildMarker)
    End Function
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Function CurrentElementTestsPositive() As Boolean
        Return (Not mEvaluator Is Nothing) AndAlso mEvaluator.Invoke(CType(Current, LanguageElement))
    End Function
    Private Sub AddElementToStack(ByVal Element As LanguageElement)
        If Not StackEmpty Then
            TopStackElement.MarkCurrentChildVisited()
        End If
        mPositionStack.Push(New ElementChildMarker(Element))
    End Sub
    Private ReadOnly Property StackEmpty() As Boolean
        Get
            Return mPositionStack.Count = 0
        End Get
    End Property
    Private Function PassedLastElement() As Boolean
        Return Current Is Nothing
    End Function
#End Region
    Public Sub New(ByVal StartElement As LanguageElement, Optional ByVal Evaluator As LanguageElementEvaluatorDelegate = Nothing)
        mStartElement = StartElement
        mEvaluator = Evaluator
        If mEvaluator Is Nothing Then
            mEvaluator = AddressOf AllElements
        End If
        Call Reset()
    End Sub
    Private Function AllElements(ByVal Element As LanguageElement) As Boolean
        Return True
    End Function
    Public Sub Reset() Implements System.Collections.IEnumerator.Reset
        mPositionStack = New Stack
        Call AddElementToStack(mStartElement)
    End Sub
    Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
        Do
            If TopStackElement.AllChildrenVisited Then
                'Pop this Child Off the stack and call moveNext again.
                Call mPositionStack.Pop()
                If StackEmpty Then
                    Return False
                End If
                Return MoveNext()
            Else
                'Pick the Next UnvisitedChild 
                'Push that onto the stack marking the child as visited.
                AddElementToStack(TopStackElement.NextUnvisitedChild)
                If CurrentElementTestsPositive() Then
                    Exit Do
                End If
                If PassedLastElement() Then
                    Exit Do
                End If
            End If
        Loop
        Return Not PassedLastElement()
    End Function
    Public ReadOnly Property Current() As Object Implements System.Collections.IEnumerator.Current
        Get
            If mPositionStack.Count > 0 Then
                Return TopStackElement().Element
            Else
                Return Nothing
            End If
        End Get
    End Property
    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return Me
    End Function
#Region "ElementChildMarker"
    Private Class ElementChildMarker
#Region "Fields"
        Public Element As LanguageElement
        Public LastVisitedNode As Integer
        Public LastVisitedDetailNode As Integer
#End Region
#Region "Utility"
        Public Function AllChildrenVisited() As Boolean
            Return (AllNodesVisited() AndAlso AllDetailNodesVisited())
        End Function
        Private Function AllNodesVisited() As Boolean
            Return Me.LastVisitedNode >= Element.NodeCount - 1
        End Function
        Private Function AllDetailNodesVisited() As Boolean
            Return Me.LastVisitedDetailNode >= Element.DetailNodeCount - 1
        End Function
#End Region
        Public Sub New(ByVal Element As LanguageElement)
            Me.Element = Element
            Me.LastVisitedNode = -1
            Me.LastVisitedDetailNode = -1
        End Sub
        <System.Diagnostics.DebuggerStepThrough()> Public Function NextUnvisitedChild() As LanguageElement
            If AllNodesVisited() Then
                Return CType(Element.DetailNodes.Item(LastVisitedDetailNode + 1), LanguageElement)
            Else
                Return CType(Element.Nodes.Item(LastVisitedNode + 1), LanguageElement)
            End If
        End Function
        Public Sub MarkCurrentChildVisited()
            If Not AllNodesVisited() Then
                LastVisitedNode += 1
            Else
                LastVisitedDetailNode += 1
            End If
        End Sub
    End Class
#End Region
End Class