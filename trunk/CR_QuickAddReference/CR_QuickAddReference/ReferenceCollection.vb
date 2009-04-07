Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Core
Imports System.Windows.Forms
Imports System.Runtime.CompilerServices
Imports System.Collections.Generic

Public Class ReferenceCollection
    Implements IEnumerable(Of Reference)
    Private mDict As New Dictionary(Of String, Reference)
    Private mList As New List(Of Reference)
    Public Sub Clear()
        mList.Clear()
        mDict.Clear()
    End Sub
    Public Sub Add(ByVal Reference As Reference)
        If mDict.ContainsValue(Reference) Then
            ' remove from list so that it gets added back in correct location
            mList.Remove(Reference)
        Else
            mDict.Item(Reference.FullName) = Reference
        End If
        mList.Add(Reference)
    End Sub
    Public Sub Add(ByVal References As IEnumerable(Of Reference))
        For Each Reference In References
            Add(Reference)
        Next
    End Sub
    Public Sub Add(ByVal References As IEnumerable(Of String))
        For Each ReferenceString In References
            Call Add(New Reference(ReferenceString))
        Next
    End Sub
    Public Sub Remove(ByVal Reference As Reference)
        mList.Remove(Reference)
        mDict.Remove(Reference.FullName)
    End Sub

    Public Function GetEnumerator() As IEnumerator(Of Reference) Implements IEnumerable(Of Reference).GetEnumerator
        Return mList.GetEnumerator
    End Function

    Public Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
        Return mList.GetEnumerator
    End Function
End Class
