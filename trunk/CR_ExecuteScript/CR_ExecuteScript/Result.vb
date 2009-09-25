Imports System
Imports System.IO
Imports Specialized = System.Collections.Specialized
Imports Reflection = System.Reflection
Imports CodeDom = System.CodeDom.Compiler
Imports System.Runtime.CompilerServices
Imports DX = DevExpress.CodeRush.Core



Public Class Result(Of T)
    Private mValue As T
    Private mSuccess As Boolean
    Private mErrors As List(Of String)
    ''' <summary>
    ''' Initializes a new instance of the  class.
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <param name="Success"></param>
    ''' <param name="Errors"></param>
    Public Sub New(ByVal Value As T, ByVal Success As Boolean, ByVal Errors As List(Of String))
        mValue = Value
        mSuccess = Success
        mErrors = Errors
    End Sub
    Public ReadOnly Property Value() As T
        Get
            Return mValue
        End Get
    End Property
    Public ReadOnly Property Success() As Boolean
        Get
            Return mSuccess
        End Get
    End Property
    Public ReadOnly Property Errors() As List(Of String)
        Get
            Return mErrors
        End Get
    End Property
End Class
