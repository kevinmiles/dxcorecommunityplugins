Imports System.ComponentModel
Imports CR_StyleNinja.Checker
Imports DevExpress.CodeRush.Core
Imports System.Runtime.CompilerServices

Friend Module Components
#Region "Create Components"
    <Extension()> _
    Public Function CreateIssue(ByVal Components As IContainer, _
                                ByVal IssueMessage As String, _
                                ByVal Qualifies As QualifiesDelegate, _
                                Optional ByVal SourceType As SourceTypeEnum = SourceTypeEnum.Unknown) As IssueProvider
        Return CreateIssue(Components, IssueMessage, AddressOf New Checker(SourceType, Qualifies, IssueMessage).CheckCodeIssues)
    End Function
    <Extension()> _
    Public Function CreateIssue(ByVal Components As IContainer, _
                                ByVal IssueMessage As String, _
                                ByVal Qualifies As QualifiesDelegate, _
                                ByVal Type As Type) As IssueProvider
        Return CreateIssue(Components, IssueMessage, AddressOf New Checker(Type, Qualifies, IssueMessage).CheckCodeIssues)
    End Function
    <Extension()> _
    Public Function CreateIssue(ByVal Components As IContainer,
                                ByVal IssueName As String,
                                ByVal CheckIssues As CheckCodeIssuesDelegate) As IssueProvider
        Dim NewIssue As New IssueProvider(Components)
        CType(NewIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        NewIssue.ProviderName = IssueName ' Should be Unique
        AddHandler NewIssue.CheckCodeIssues, AddressOf New CheckIssuesDelegateCaller(CheckIssues).Invoke
        CType(NewIssue, System.ComponentModel.ISupportInitialize).EndInit()
        Return NewIssue
    End Function
    <Extension()> _
    Public Function CreateRefactoring(ByVal Components As IContainer, ByVal IssueName As String, ByVal DisplayName As String, ByVal Apply As ApplyDelegate, ByVal CheckContentAvailability As CheckContentAvailabilityDelegate) As DevExpress.Refactor.Core.RefactoringProvider
        Dim NewRefactoring As New DevExpress.Refactor.Core.RefactoringProvider(Components)
        CType(NewRefactoring, System.ComponentModel.ISupportInitialize).BeginInit()
        NewRefactoring.ProviderName = IssueName ' Should be Unique
        NewRefactoring.DisplayName = DisplayName
        AddHandler NewRefactoring.CheckAvailability, AddressOf New CheckContentAvailabilityDelegateCaller(CheckContentAvailability).Invoke
        AddHandler NewRefactoring.Apply, AddressOf New ApplyDelegateCaller(Apply).Invoke

        CType(NewRefactoring, System.ComponentModel.ISupportInitialize).EndInit()
        Return NewRefactoring
    End Function
    <Extension()> _
    Public Function CreateCodeProvider(ByVal Components As IContainer, ByVal IssueName As String, ByVal DisplayName As String, ByVal Apply As ApplyDelegate, ByVal CheckContentAvailability As CheckContentAvailabilityDelegate) As CodeProvider
        Dim NewCodeProvider As New CodeProvider(Components)
        CType(NewCodeProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        NewCodeProvider.ProviderName = IssueName ' Should be Unique
        NewCodeProvider.DisplayName = DisplayName
        AddHandler NewCodeProvider.CheckAvailability, AddressOf New CheckContentAvailabilityDelegateCaller(CheckContentAvailability).Invoke
        AddHandler NewCodeProvider.Apply, AddressOf New ApplyDelegateCaller(Apply).Invoke
        CType(NewCodeProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Return NewCodeProvider
    End Function
#End Region

End Module
