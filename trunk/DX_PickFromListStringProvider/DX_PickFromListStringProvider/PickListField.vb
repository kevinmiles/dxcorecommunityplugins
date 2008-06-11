Imports DevExpress.CodeRush.Core

Public Class PickListField
    Inherits TextField
#Region "Fields"
    Public mPickList As New List(Of String)
#End Region
#Region "Properties"
    Public ReadOnly Property PickList() As List(Of String)
        Get
            Return mPickList
        End Get
    End Property
#End Region
#Region "Constructors"
    Public Sub New(ByVal start As EditPoint, ByVal [end] As EditPoint)
        MyBase.New(start, [end])
    End Sub

    Public Sub New(ByVal start As EditPoint, ByVal [end] As EditPoint, ByVal description As String)
        MyBase.New(start, [end], description)
    End Sub

    Public Sub New(ByVal start As EditPoint, ByVal [end] As EditPoint, ByVal description As String, ByVal textFieldType As TextFieldType)
        MyBase.New(start, [end], description, textFieldType)
    End Sub

    Public Sub New(ByVal start As EditPoint, ByVal [end] As EditPoint, ByVal description As String, ByVal textFieldType As TextFieldType, ByVal clearOnFirstActivation As Boolean)
        MyBase.New(start, [end], description, textFieldType, clearOnFirstActivation)
    End Sub
#End Region
End Class