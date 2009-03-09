Imports System

Public Interface INode
    Property ID() As Guid
    Property ParentID() As Guid
    Property Name() As String
    Property Type() As String
    Function ToString() As String
End Interface
