Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Text


Public Class SortMember
    Implements IComparable


    Private _theMember As Member
    Public Property theMember() As Member
        Get
            Return _theMember
        End Get
        Set(ByVal Value As Member)
            _theMember = Value
        End Set
    End Property

    ''' <summary>
    ''' Initializes a new instance of the SortMember class.
    ''' </summary>
    ''' <param name="theMember"></param>
    Public Sub New(ByVal theMember As Member)
        _theMember = theMember
    End Sub


    Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
        If obj Is Nothing Then
            Return 0
        End If
        Return Me.theMember.Name.CompareTo(DirectCast(obj, SortMember).theMember.Name)
    End Function
End Class
