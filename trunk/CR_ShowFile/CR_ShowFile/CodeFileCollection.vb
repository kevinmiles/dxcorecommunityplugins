Imports System.Collections

Public Class CodeFileCollection
    Inherits CollectionBase

    ' public methods...
#Region "Add"
    Public Function Add(ByVal aCodeFile As CodeFile) As Integer
        Return InnerList.Add(aCodeFile)
    End Function
#End Region
#Region "Insert"
    Public Sub Insert(ByVal index As Integer, ByVal aCodeFile As CodeFile)
        InnerList.Insert(index, aCodeFile)
    End Sub
#End Region
#Region "Remove"
    Public Sub Remove(ByVal aCodeFile As CodeFile)
        InnerList.Remove(aCodeFile)
    End Sub
#End Region
#Region "Find"
    Public Function Find(ByVal aCodeFile As CodeFile) As CodeFile
        For Each lCodeFile As CodeFile In Me
            If lCodeFile Is aCodeFile Then   ' Found it
                Return lCodeFile
            End If
        Next
        Return Nothing    ' Not found
    End Function
#End Region
#Region "Contains"
    Public Function Contains(ByVal aCodeFile As CodeFile) As Boolean
        Return Not (Find(aCodeFile) Is Nothing)
    End Function
#End Region

    ' public properties...
#Region "Item(aIndex As Integer)"
    Public ReadOnly Property Item(ByVal aIndex As Integer) As CodeFile
        Get
            Return DirectCast(InnerList(aIndex), CodeFile)
        End Get
    End Property
#End Region
End Class