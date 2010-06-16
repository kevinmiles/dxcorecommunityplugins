'Imports System.ComponentModel
'Imports System.Drawing
'Imports System.Windows.Forms
'Imports DevExpress.CodeRush.Core
'Imports DevExpress.CodeRush.PlugInCore
'Imports DevExpress.CodeRush.StructuralParser
'Public Class PlugIn1

'    'DXCore-generated code...
'#Region " InitializePlugIn "
'    Public Overrides Sub InitializePlugIn()
'        MyBase.InitializePlugIn()

'        'TODO: Add your initialization code here.
'    End Sub
'#End Region
'#Region " FinalizePlugIn "
'    Public Overrides Sub FinalizePlugIn()
'        'TODO: Add your finalization code here.

'        MyBase.FinalizePlugIn()
'    End Sub
'#End Region
'    Private Sub Testing()
'        Me.components.CreateAction("MyAction", "MyActionMenu", AddressOf MyAction_Execute)
'        Me.components.CreateIssue("IssueName", "DisplayName", AddressOf MyIssue_CheckCodeIssues)

'    End Sub
'    Private Sub MyAction_Execute(ByVal ea As ExecuteEventArgs)
'        ' This method is executed when your action is called.
'        ' Remember you must bind your action to a shortcut in order to use it.
'        ' Shortcuts are created\altered using the IDE\Shortcuts option page 
'    End Sub
'    Private Sub MyIssue_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
'        ' This method is executed when the system checks for your issue.
'        Dim Scope = TryCast(ea.Scope, LanguageElement)
'        If Scope Is Nothing Then
'            Exit Sub
'        End If
'        Dim Finder As New ElementEnumerable(Scope, GetType(DevExpress.CodeRush.StructuralParser.Method), True)
'        For Each FoundItem As DevExpress.CodeRush.StructuralParser.Method In Finder
'            ' ea.AddError(FoundItem.NameRange, "Provide Error Detail here.")
'            ' ea.AddHint(FoundItem.NameRange, "Provide hint here.")
'            ' You get the general drift. :)

'        Next
'    End Sub
'End Class
