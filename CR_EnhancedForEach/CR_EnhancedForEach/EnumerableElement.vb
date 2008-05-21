Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Collections
Imports System.Diagnostics
Imports System.Reflection

Public Class EnumerableElement
    Implements IComparable

    Private _Name As String
    Private _ReturnType As LanguageElement
    Private _Local As Boolean
    Private _ReturnTypeName As String
    private _element as LanguageElement

    ''' <summary>
    ''' Initializes a new instance of the EnumerableElement class.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="returnType"></param>
    ''' <param name="local"></param>
    Public Sub New(ByVal name As String, theElement as LanguageElement, ByVal returnType As LanguageElement, ByVal local As Boolean)
        _Name = name
        _ReturnType = returnType
        ReturnTypeName = ""
        _Local = local
        _element=theElement
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the EnumerableElement class.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="local"></param>
    Public Sub New(ByVal name As String, ByVal local As Boolean)
        _Name = name
        _ReturnType = Nothing
        ReturnTypeName = ""
        _Local = local
        _element = Nothing
    End Sub
    Public Sub New(ByVal name As String, ByVal returnTypeName As String, ByVal local As Boolean)
        _Name = name
        _ReturnTypename = returnTypename
        ReturnType = Nothing
        _Local = local
    End Sub

    Public Property Element() As LanguageElement
        Get
            Return _element
        End Get
        Set(ByVal Value As LanguageElement)
            _element = Value
        End Set
    End Property

    Public Property ReturnTypeName() As String
        Get
            Return _ReturnTypeName
        End Get
        Set(ByVal Value As String)
            If _ReturnTypeName = Value Then
                Return
            End If
            _ReturnTypeName = Value
        End Set
    End Property

    Public Property Local() As Boolean
        Get
            Return _Local
        End Get
        Set(ByVal Value As Boolean)
            If _Local = Value Then
                Return
            End If
            _Local = Value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return _Name
        End Get
        Set(ByVal Value As String)
            If _Name = Value Then
                Return
            End If
            _Name = Value
        End Set
    End Property
    Public Property ReturnType() As LanguageElement
        Get
            Return _ReturnType
        End Get
        Set(ByVal Value As LanguageElement)

            _ReturnType = Value
        End Set
    End Property

    Overrides Function ToString() As String
        If Local Then
            Return Name & " (local)"
        Else
            Return Name
        End If
    End Function

    Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
        If obj.ToString = Me.ToString Then
            Return 0
        ElseIf obj.ToString > Me.ToString Then
            Return -1
        Else
            Return 1
        End If
    End Function
End Class