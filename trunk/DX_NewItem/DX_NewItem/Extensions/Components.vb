Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices
Imports EnvDTE80

Public Module Components
#Region "Action"
    Delegate Sub ActionExecute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs)
    <Extension()> _
    Public Function CreateAction(ByVal Components As System.ComponentModel.IContainer, _
                                       ByVal ActionName As String, _
                                       ByVal MenuName As String, _
                                       ByVal ActionEvent As ActionExecute) As Action
        Dim MyAction As New DevExpress.CodeRush.Core.Action(Components)
        CType(MyAction, System.ComponentModel.ISupportInitialize).BeginInit()
        MyAction.ActionName = ActionName
        MyAction.ButtonText = MenuName ' Used if button is placed on a menu.
        MyAction.RegisterInCR = True
        AddHandler MyAction.Execute, AddressOf ActionEvent.Invoke
        CType(MyAction, System.ComponentModel.ISupportInitialize).EndInit()
        Return MyAction
    End Function
#End Region
#Region "Issue"
    Delegate Sub CheckIssueExecute(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CheckCodeIssuesEventArgs)
    <Extension()> _
    Public Function CreateIssue(ByVal Components As System.ComponentModel.IContainer, _
                                       ByVal IssueName As String, _
                                       ByVal DisplayName As String, _
                                       ByVal CheckIssueEvent As CheckIssueExecute) As IssueProvider
        Dim MyIssue As New DevExpress.CodeRush.Core.IssueProvider(Components)
        CType(MyIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        MyIssue.ProviderName = IssueName
        MyIssue.DisplayName = DisplayName
        AddHandler MyIssue.CheckCodeIssues, AddressOf CheckIssueEvent.Invoke
        CType(MyIssue, System.ComponentModel.ISupportInitialize).EndInit()
        Return MyIssue
    End Function
#End Region

End Module
