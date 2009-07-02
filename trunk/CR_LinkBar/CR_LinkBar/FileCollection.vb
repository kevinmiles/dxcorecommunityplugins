Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Menus

Public Class FileCollection
    Inherits List(Of FileReference)
#Region "Fields"
    Private mName As String
#End Region
#Region "Simple Properties"
    Public Property Name() As String
        Get
            Return mName
        End Get
        Set(ByVal value As String)
            mName = value
        End Set
    End Property
#End Region
#Region "Constructors"
    Public Sub New(ByVal CollectionName As String)
        mName = CollectionName

    End Sub
#End Region
    Public Overloads Sub Add(ByVal Name As String, ByVal FileWithPath As String)
        MyBase.Add(New FileReference(Name, FileWithPath))
    End Sub
End Class
