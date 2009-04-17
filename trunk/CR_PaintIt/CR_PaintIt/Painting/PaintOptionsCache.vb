Imports CR_PaintIt.Painting
Imports System.Collections
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Imports System

Public Class PaintOptionsCache
    Implements IEnumerable
    Private Cache As New Hashtable
    Public Delegate Function GetPaintOptionsProc(ByVal Element As LanguageElement) As PaintOptions
    Public Sub Clear()
        Cache.Clear()
    End Sub
    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Return Cache.GetEnumerator
    End Function
    Public Function Contains(ByVal Element As LanguageElement) As Boolean
        Dim TheKey As String
        ElementKey(Element, TheKey)
        Return Cache.ContainsKey(TheKey)
    End Function
    Public Sub Add(ByVal Element As LanguageElement, ByVal Options As PaintOptions)
        If Element Is Nothing Then
            Throw New ArgumentNullException("Element is null.", "Element")
        End If
        If Options Is Nothing Then
            Exit Sub ' No Options ---> No painting ----> so don't fill cache
        End If
        Dim Key As String = String.Empty
        If ElementKey(Element, Key) Then
            Cache.Item(Key) = New LanguageElementPaintOptions(Element, Options)
        End If
    End Sub
    Public Sub AddByDelegate(ByVal SourceNode As LanguageElement, _
                             ByVal EvaluatorProc As LanguageElementEvaluatorDelegate, _
                             ByVal PaintOptionsProc As GetPaintOptionsProc)
        Dim Wrapper As New LanguageElementIterator(SourceNode, EvaluatorProc)
        For Each LE As LanguageElement In Wrapper
            Call Add(LE, PaintOptionsProc.Invoke(LE))
        Next
    End Sub
    'Public Sub RefreshNode(ByVal Element As LanguageElement)
    '    Cache.Item(ElementKey(Element)) = 
    'End Sub
    Public Sub Remove(ByVal Element As LanguageElement)
        Dim Key As String = String.Empty
        If ElementKey(Element, Key) Then
            Cache.Remove(Key)
        End If
    End Sub

    Private Function ElementKey(ByVal Element As LanguageElement, ByRef Key As String) As Boolean
        If Element.Name = String.Empty Then
            Return False
        End If
        Key = Element.Name
        Return True
    End Function
    Default Public ReadOnly Property Item(ByVal Element As LanguageElement) As LanguageElementPaintOptions
        Get
            Dim Key As String = String.Empty
            If ElementKey(Element, Key) Then
                Return CType(Cache.Item(Key), LanguageElementPaintOptions)
            End If
        End Get
    End Property
End Class