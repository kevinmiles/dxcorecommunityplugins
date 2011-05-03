Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports System.Linq
Public Class PlugIn1

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        CreateSortRegionContent()
        'CreateSortSelection()
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

    Public Sub CreateSortRegionContent()
        Dim SortRegionContent As New DevExpress.Refactor.Core.RefactoringProvider(components)
        CType(SortRegionContent, System.ComponentModel.ISupportInitialize).BeginInit()
        SortRegionContent.ProviderName = "SortRegionContent" ' Should be Unique
        SortRegionContent.DisplayName = "Sort Region Content"
        AddHandler SortRegionContent.CheckAvailability, AddressOf SortRegionContent_CheckAvailability
        AddHandler SortRegionContent.Apply, AddressOf SortRegionContent_Execute
        CType(SortRegionContent, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
#Region "Sort Region"
    Private Sub SortRegionContent_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        ea.Available = CodeRush.Caret.Location = CaretLocation.InRegionDeclaration
    End Sub
    Private Sub SortRegionContent_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        'Determine list of high level items in Range
        Dim Range = CodeRush.Source.GetRegionAt(CodeRush.Caret.SourcePoint).Range
        SortRangeElements(Range)
    End Sub
#End Region
    'Public Sub CreateSortSelection()
    '    Dim SortSelection As New DevExpress.Refactor.Core.RefactoringProvider(components)
    '    CType(SortSelection, System.ComponentModel.ISupportInitialize).BeginInit()
    '    SortSelection.ProviderName = "SortSelection" ' Should be Unique
    '    SortSelection.DisplayName = "Sort Selection"
    '    AddHandler SortSelection.CheckAvailability, AddressOf SortSelection_CheckAvailability
    '    AddHandler SortSelection.Apply, AddressOf SortSelection_Execute
    '    CType(SortSelection, System.ComponentModel.ISupportInitialize).EndInit()
    'End Sub
    'Private Sub SortSelection_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
    '    ' This method is executed when the system checks the availability of your Refactoring.
    '    If Not CodeRush.Selection.Exists Then
    '        Return
    '    End If
    '    If Not CodeRush.Selection.Height > 1 Then
    '        Return
    '    End If
    '    ea.Available = True
    'End Sub
    'Private Sub SortSelection_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
    '    ' This method is executed when the system executes your refactoring 
    '    Dim Range = GetSelectionRange()
    '    Call SortRangeElements(Range)
    'End Sub
    'Private Shared Function GetSelectionRange() As SourceRange
    '    Dim StartPoint = SourcePoint.Empty
    '    Dim EndPoint = SourcePoint.Empty
    '    If Not CodeRush.Selection.GetPoints(StartPoint, EndPoint) Then
    '        Return Nothing
    '    End If
    '    Return New SourceRange(StartPoint, EndPoint)
    'End Function
    

    Private Shared Sub SortRangeElements(ByVal Range As SourceRange)
        Call SortItemsInRange(Range, LanguageElementType.Method)
        Call SortItemsInRange(Range, LanguageElementType.Property)
        Call SortItemsInRange(Range, LanguageElementType.Variable)
    End Sub
    Private Shared Sub SortItemsInRange(ByVal Range As SourceRange, ByVal ElementType As LanguageElementType)
        Dim ActiveDoc = CodeRush.Documents.ActiveTextDocument
        Dim SortedList As New SortedList(Of String, SourceRange)
        Dim RangeNodes = CodeRush.Source.GetSelectedNodes(ActiveDoc, ActiveDoc.ActiveView, Range).Where(Function(E) E.ElementType = ElementType)
        For Each Item In RangeNodes
            SortedList.Add(Item.Name, Item.GetFullBlockCutRange)
        Next
        Dim CodeAnalysis As String = String.Empty
        For Each RangeKey As String In SortedList.Keys
            ' Gather Code
            Dim ElementRange = SortedList(RangeKey)
            CodeAnalysis &= ActiveDoc.GetText(ElementRange)
        Next
        For Each RangeKey In SortedList.Keys
            Dim ElementRange = SortedList(RangeKey)
            ActiveDoc.QueueDelete(ElementRange)
        Next
        ActiveDoc.QueueInsert(Range.Start.OffsetPoint(1, 0), CodeAnalysis)
        ActiveDoc.ApplyQueuedEdits("Sorted Elements")
    End Sub
    Private Shared Function GetNodesInRange(ByVal ActiveDoc As TextDocument, ByVal Range As SourceRange) As IEnumerable(Of LanguageElement)
        Return CodeRush.Source.GetSelectedNodes(ActiveDoc, ActiveDoc.ActiveView, Range)
    End Function


End Class
