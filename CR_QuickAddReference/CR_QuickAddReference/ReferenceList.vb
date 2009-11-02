Option Strict On
Public Class ReferenceList
    Protected mName As String
    Private mDefaultFunction As Func(Of List(Of Reference))
    Public Sub New(ByVal Name As String, ByVal DefaultFunction As Func(Of List(Of Reference)))
        mName = Name
        mDefaultFunction = DefaultFunction
    End Sub
    Public ReadOnly Property Name() As String
        Get
            Return mName
        End Get
    End Property
    Public Function Defaults() As IEnumerable(Of Reference)
        Return mDefaultFunction.Invoke
    End Function
    Public Overridable Function References() As IEnumerable(Of Reference)
        Return Defaults()
    End Function
End Class