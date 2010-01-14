Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Text
Imports System.Security.Cryptography


Public Class HashList
    Inherits List(Of String)
    Public Function EqualsOtherHashList(ByVal OtherHashList As HashList) As Boolean
        If OtherHashList Is Nothing Then
            Return False
        End If
        If OtherHashList.Count <> Me.Count Then
            Return False
        End If

        For index As Integer = 0 To Me.Count - 1
            If Item(index) <> OtherHashList.Item(index) Then
                Return False
            End If
        Next
        Return True
    End Function
End Class