Imports System
Public Class ElementNode
    Implements INode

    Public Sub New(ByVal name As String, ByVal type As String, Optional ByVal ID As String = "", Optional ByVal parentID As String = "")
        MyBase.New()

        If ID = "" Then
            Me.ID = Guid.NewGuid()
        Else
            Me.ID = New Guid(ID)
        End If

        Me.Name = name
        Me.Type = type
        If Not parentID = "" Then
            Me.ParentID = New Guid(parentID)
        End If
    End Sub

    Private _iD As Guid
    Public Property ID() As Guid Implements INode.ID
        Get
            Return _iD
        End Get
        Set(ByVal Value As Guid)
            _iD = Value
        End Set
    End Property

    Private _name As String
    Public Property Name() As String Implements INode.Name
        Get
            Return _name
        End Get
        Set(ByVal Value As String)
            _name = Value
        End Set
    End Property

    Private _type As String
    Public Property Type() As String Implements INode.Type
        Get
            Return _type
        End Get
        Set(ByVal Value As String)
            _type = Value
        End Set
    End Property

    Private _parentID As Guid
    Public Property ParentID() As Guid Implements INode.ParentID
        Get
            Return _parentID
        End Get
        Set(ByVal Value As Guid)
            _parentID = Value
        End Set
    End Property

    Public Overrides Function ToString() As String Implements INode.ToString
        Return Me.Type & " '" & Me.Name & "'"
    End Function
End Class
