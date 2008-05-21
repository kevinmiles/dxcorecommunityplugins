Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser


Public Class PluginMenuitem
    Inherits MenuItem
    Private _newName As String
    Public Property NewName() As String
        Get
            Return _newName
        End Get
        Set(ByVal Value As String)
            _newName = Value
        End Set
    End Property
    Private _addNameSpace As Boolean
    Public Property AddNameSpace() As Boolean
        Get
            Return _addNameSpace
        End Get
        Set(ByVal Value As Boolean)
            _addNameSpace = Value
        End Set
    End Property
    Private _theNameSpace As String
    Public Property TheNameSpace() As String
        Get
            Return _theNameSpace
        End Get
        Set(ByVal Value As String)
            _theNameSpace = Value
        End Set
    End Property
    Private _theElement As LanguageElement
    Public Property theElement() As LanguageElement
        Get
            Return _theElement
        End Get
        Set(ByVal Value As LanguageElement)
            _theElement = Value
        End Set
    End Property
End Class
