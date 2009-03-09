Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Text

Public Class NodeFilter

    Private _members As New List(Of SortMember)

    Public ReadOnly Property Members() As List(Of SortMember)
        Get
            Return _members
        End Get
    End Property
    ''' <summary>
    ''' Initializes a new instance of the NodeFilter class.
    ''' </summary>
    ''' <param name="visibility"></param>
    ''' <param name="nodeType"></param>
    Public Sub New(ByVal visibility As String, ByVal nodeType As String)
        _visibility = visibility
        _nodeType = nodeType
        _memberName = ""
    End Sub

    ''' <summary>
    ''' Initializes a new instance of the NodeFilter class.
    ''' </summary>
    ''' <param name="visibility"></param>
    ''' <param name="nodeType"></param>
    ''' <param name="memberName"></param>
    ''' <param name="specials"></param>
    Public Sub New(ByVal visibility As String, ByVal nodeType As String, ByVal memberName As String, ByVal specials As String)
        _memberName = memberName
        _visibility = visibility
        _nodeType = nodeType
        _specials = specials
    End Sub

    Public Sub AddMember(ByVal theMember As SortMember)
        _members.Add(theMember)
    End Sub


    Private _specials As String
    Public Property Specials() As String
        Get
            Return _specials
        End Get
        Set(ByVal Value As String)
            _specials = Value
        End Set
    End Property
    


    Private _memberName As String
    Public Property MemberName() As String
        Get
            Return _memberName
        End Get
        Set(ByVal Value As String)
            _memberName = Value
        End Set
    End Property


    Private _visibility As String
    Public Property Visibility() As String
        Get
            Return _visibility
        End Get
        Set(ByVal Value As String)
            _visibility = Value
        End Set
    End Property

    Private _nodeType As String
    Public Property NodeType() As String
        Get
            Return _nodeType
        End Get
        Set(ByVal Value As String)
            _nodeType = Value
        End Set
    End Property

    Public Sub GenerateCode(ByVal textDocument As TextDocument, ByVal code As StringBuilder)
        Dim codeLength As Integer = code.Length

        _members.Sort()
        For Each member As SortMember In _members
            Dim memberCode As String = textDocument.GetText(member.theMember.GetFullBlockCutRange())
            code.AppendLine(memberCode)
            code.AppendLine()
        Next
    End Sub





End Class