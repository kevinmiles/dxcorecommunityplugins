Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Core
Imports System.Windows.Forms
Imports System.IO
Imports System.Runtime.CompilerServices

Public Class ReferenceListViewItem
    Inherits ListViewItem
    Private mReference As Reference
    Public Property Reference() As Reference
        Get
            Return mReference
        End Get
        Set(ByVal value As Reference)
            mReference = value
            SubItems.Clear()
            Me.Text = mReference.FileName
            Me.SubItems.Add(New ListViewSubItem(Me, mReference.Location))
        End Set
    End Property
    Public Shared Function [Of](ByVal Reference As Reference) As ReferenceListViewItem
        Return New ReferenceListViewItem With {.Reference = Reference}
    End Function
    Public Shared Function [Of](ByVal Reference As String) As ReferenceListViewItem
        Return New ReferenceListViewItem With {.Reference = New Reference(Reference)}
    End Function
End Class
Module StringExt
    <Extension()> _
    Public Function ToFileinfo(ByVal Source As String) As FileInfo
        Return New FileInfo(Source)
    End Function
End Module