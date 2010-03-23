Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Xpo.Helpers

Public Class MyAttribute
    Inherits Attribute
    Public Sub New()

    End Sub
End Class



Public Class RelationType
    Public Sub New()

    End Sub
End Class
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
    <MyAttribute()> _
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

    Private _persistentReferenceProperty As PersistentObject1
    Public Property PersistentReferenceProperty As PersistentObject1
        Get
            Return _persistentReferenceProperty
        End Get
        Set(ByVal Value As PersistentObject1)
            SetPropertyValue("PersistentReferenceProperty", _persistentReferenceProperty, Value)
        End Set
    End Property



    Private _persistentProperty As String
    <Persistent()> _
    Public Property PersistentProperty As String
        Get
            Return _persistentProperty
        End Get
        Set(ByVal Value As String)
            SetPropertyValue("PersistentProperty", _persistentProperty, Value)
        End Set
    End Property


    <Association("PersistentObject1-Relations")> _
    Public ReadOnly Property Relations() As XPCollection(Of RelationType)
        Get
            Return GetCollection(Of RelationType)("Relations")
        End Get
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

    'Created/Updated: Wed 24-Mar-2010 09:20:14
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
        Public ReadOnly Property NonPersistentTestProperty() As DevExpress.Data.Filtering.OperandProperty
            Get
                Return New DevExpress.Data.Filtering.OperandProperty(GetNestedName("NonPersistentTestProperty"))
            End Get
        End Property
        Public ReadOnly Property PersistentReferenceProperty() As DXCore_TestApp.PersistentObject1.FieldsClass
            Get
                Return New DXCore_TestApp.PersistentObject1.FieldsClass(GetNestedName("PersistentReferenceProperty"))
            End Get
        End Property
        Public ReadOnly Property PersistentProperty() As DevExpress.Data.Filtering.OperandProperty
            Get
                Return New DevExpress.Data.Filtering.OperandProperty(GetNestedName("PersistentProperty"))
            End Get
        End Property
        Public ReadOnly Property Relations() As DevExpress.Data.Filtering.OperandProperty
            Get
                Return New DevExpress.Data.Filtering.OperandProperty(GetNestedName("Relations"))
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
    End Class
    Public Shared Shadows ReadOnly Property Fields() As FieldsClass
        Get
            If ReferenceEquals(_fields, Nothing) Then
                _fields = New FieldsClass()
            End If
            Return _fields
        End Get
    End Property
    Private Shared _fields As FieldsClass
End Class
