Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpo.Helpers

Public Class PersistentObject1
	Inherits XPObject

	Public Sub New()
		MyBase.New()
		' This constructor is used when an object is loaded from a persistent storage.
		' Do not place any code here.			
	End Sub

	Public Sub New(ByVal session As Session)
		MyBase.New(session)
		' This constructor is used when an object is loaded from a persistent storage.
		' Do not place any code here.			
	End Sub

	Public Overrides Sub AfterConstruction()
		MyBase.AfterConstruction()
		' Place here your initialization code.
    End Sub

    Private _persistentTestProperty As String
    Public Property PersistentTestProperty As String
        Get
            Return _persistentTestProperty
        End Get
        Set(ByVal Value As String)
            SetPropertyValue("PersistentTestProperty", _persistentTestProperty, Value)
        End Set
    End Property

    Private _nonPersistentTestProperty As String
    <DevExpress.Xpo.NonPersistent()> _
    Public Property NonPersistentTestProperty As String
        Get
            Return _nonPersistentTestProperty
        End Get
        Set(ByVal Value As String)
            _nonPersistentTestProperty = Value
        End Set
    End Property

    <Persistent("PersistentVariable")> _
    Private _persistentVariable As String
    Protected Overridable Sub SetPersistentVariable(ByVal Value As String)
        SetPropertyValue("PersistentVariable", _persistentVariable, Value)
    End Sub
    <PersistentAlias("_persistentVariable")> _
    Public ReadOnly Property PersistentVariable As String
        Get
            Return _persistentVariable
        End Get
    End Property

    Private _test As Date
    Public Property Test As Date
        Get
            Return _test
        End Get
        Set(ByVal Value As Date)
            SetPropertyValue("Test", _test, Value)
        End Set
    End Property


#Region "XPO nested fields class - don't edit manually"
    'Created/Updated: Fri 19-Mar-2010 13:21:33
    Public Shadows Class FieldsClass
        Inherits XPObject.FieldsClass
        Public Sub New()
            MyBase.New()
        End Sub
        Public Sub New(ByVal propertyName As String)
            MyBase.New(propertyName)
        End Sub
        Public ReadOnly Property PersistentTestProperty() As DevExpress.Data.Filtering.OperandProperty
            Get
                Return New DevExpress.Data.Filtering.OperandProperty(GetNestedName("PersistentTestProperty"))
            End Get
        End Property
        Public ReadOnly Property PersistentVariable() As DevExpress.Data.Filtering.OperandProperty
            Get
                Return New DevExpress.Data.Filtering.OperandProperty(GetNestedName("PersistentVariable"))
            End Get
        End Property
        Public ReadOnly Property Test() As DevExpress.Data.Filtering.OperandProperty
            Get
                Return New DevExpress.Data.Filtering.OperandProperty(GetNestedName("Test"))
            End Get
        End Property
        Public ReadOnly Property Fields() As DevExpress.Data.Filtering.OperandProperty
            Get
                Return New DevExpress.Data.Filtering.OperandProperty(GetNestedName("Fields"))
            End Get
        End Property
    End Class
    Private Shared _fields As FieldsClass
    Public Shared Shadows ReadOnly Property Fields() As FieldsClass
        Get
            If ReferenceEquals(_fields, Nothing) Then
                _fields = New FieldsClass()
            End If
            Return _fields
        End Get
    End Property
#End Region
End Class
