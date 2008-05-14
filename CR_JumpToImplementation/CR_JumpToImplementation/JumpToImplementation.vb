Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Public Class JumpToImplementation

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()

        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region
    Private Sub actJumpToImplementation_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles actJumpToImplementation.Execute
        Dim TheInterface As [Interface] = CodeRush.Source.ActiveInterface

        ' Attempt Interface member first 
        Dim InterfaceMember As Member = CType(CodeRush.Source.ActiveMember, Member)
        If InterfaceMember IsNot Nothing Then
            If TypeOf InterfaceMember.Parent Is [Interface] Then
                Dim TheClass As [Class] = GetFirstClassImplementingInterface(TheInterface)
                Call LocateClassMemberImplementation(TheClass, InterfaceMember)
                Exit Sub
            End If
        End If
        ' Use whole interface if nessecary
        If TheInterface IsNot Nothing Then
            Dim TheClass As [Class] = GetFirstClassImplementingInterface(TheInterface)
            If TheClass IsNot Nothing Then
                ' Locate Class
                Call JumpToLangageElement(TheClass)
                Exit Sub
            End If
        End If
    End Sub
    Private Sub LocateClassMemberImplementation(ByVal TheClass As [Class], ByVal InterfaceMember As Member)
        ' find the Member on the whose signature matches
        For Each Node As LanguageElement In TheClass.Nodes
            If TypeOf Node Is Member Then
                Dim ClassMember As Member = CType(Node, Member)
                If ClassMember.Implements.Contains(InterfaceMember.Location) Then
                    Call JumpToLangageElement(ClassMember)
                    Exit For
                End If
            End If
        Next
    End Sub

    Private Sub JumpToLangageElement(ByVal FoundClass As LanguageElement)
        Call FoundClass.Document.Activate()
        Call CodeRush.Documents.ActiveTextView.Selection.Set(FoundClass.NameRange)
    End Sub

    Private Function GetFirstClassImplementingInterface(ByVal TheInterface As [Interface]) As [Class]
        Dim Implementations As ITypeElement() = TheInterface.GetDescendants()
        Dim Found As Boolean = False
        For Each Imp In Implementations
            Dim TheDeclaration As IElement = Imp.GetDeclaration
            If TheDeclaration IsNot Nothing Then
                Return CType(TheDeclaration, [Class])
            End If
        Next
        Return Nothing
    End Function
End Class
