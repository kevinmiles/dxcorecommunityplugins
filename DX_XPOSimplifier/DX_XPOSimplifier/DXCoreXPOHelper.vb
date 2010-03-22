Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.Xpo

Public Class DXCoreXPOHelper

    Public Shared Function IsPersistentMember(ByVal foundProperty As MemberWithParameters) As Boolean

        For Each Attr As Attribute In foundProperty.Attributes
            'If Not TypeOf Attr Is ITypeReferenceExpression Then
            '    Continue For
            'End If

            Dim xpoElement As New DXCoreXPOHelper.XPOElement(Attr.GetDeclaration)
            If xpoElement.IsPersistentAttribute Then
                Return True
            ElseIf xpoElement.IsPersistentAliasAttribute Then
                Return True
            ElseIf xpoElement.IsNonPersistentAttribute Then
                Return False
            End If
        Next
        If foundProperty.Visibility = MemberVisibility.Public Then
            Return True
        End If
        Return False
    End Function

    Public Class XPOElement
        Private _Element As ITypeElement
        Private _Resolver As New SourceTreeResolver

        Public Sub New(ByVal baseElement As ITypeElement)
            Element = baseElement
        End Sub

        Private Function Check(Of T)() As Boolean
            Return Check(Of T)(_Element)
        End Function

        Public Shared Function Check(Of T)(ByVal checkElement As ITypeElement) As Boolean
            Return Check(GetType(T), checkElement)
        End Function

        Public Shared Function Check(ByVal checkType As Type, ByVal checkElement As ITypeElement) As Boolean
            Dim Resolver As New SourceTreeResolver
            Try
                Dim DeclaredType As ITypeElement
                If TypeOf checkElement Is ITypeElement Then
                    DeclaredType = checkElement
                Else
                    DeclaredType = checkElement.GetDeclaration
                End If

                If DeclaredType Is Nothing Then
                    Return False
                End If

                If DeclaredType.DescendsFrom(checkType.FullName) Then
                    Return True
                End If
            Catch ex As Exception

            End Try
            Return False
        End Function

        Public Property Element As ITypeElement
            Get
                Return _Element
            End Get
            Set(ByVal Value As ITypeElement)
                _Element = Value
                _isPersistentAttribute = Check(Of DevExpress.Xpo.PersistentAttribute)()
                _isPersistentAliasAttribute = Check(Of DevExpress.Xpo.PersistentAliasAttribute)()
                _isNonPersistentAttribute = Check(Of DevExpress.Xpo.NonPersistentAttribute)()
                _isPersistentClass = Check(Of DevExpress.Xpo.PersistentBase)()
                _isXPCollection = Check(Of DevExpress.Xpo.XPCollection)()
            End Set
        End Property


        Private _isPersistentAttribute As Boolean
        Public ReadOnly Property IsPersistentAttribute As Boolean
            Get
                Return _isPersistentAttribute
            End Get
        End Property

        Private _isPersistentAliasAttribute As Boolean
        Public ReadOnly Property IsPersistentAliasAttribute As Boolean
            Get
                Return _isPersistentAliasAttribute
            End Get
        End Property

        Private _isNonPersistentAttribute As Boolean
        Public ReadOnly Property IsNonPersistentAttribute As Boolean
            Get
                Return _isNonPersistentAttribute
            End Get
        End Property

        Private _isPersistentClass As Boolean
        Public ReadOnly Property IsPersistentClass As Boolean
            Get
                Return _isPersistentClass
            End Get
        End Property

        Private _isXPCollection As Boolean
        Public ReadOnly Property IsXPCollection As Boolean
            Get
                Return _isXPCollection
            End Get
        End Property

    End Class
End Class
