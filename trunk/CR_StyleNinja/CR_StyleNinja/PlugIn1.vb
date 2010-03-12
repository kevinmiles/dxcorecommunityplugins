Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices


Public Class PlugIn1

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        RegisterRules()
        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

#Region "Utility"
    Public Function CreateIssue(ByVal IssueName As String, ByVal CheckIssues As CheckCodeIssuesDelegate) As IssueProvider
        Dim NewIssue As New DevExpress.CodeRush.Core.IssueProvider(components)
        CType(NewIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        NewIssue.ProviderName = IssueName ' Should be Unique
        AddHandler NewIssue.CheckCodeIssues, AddressOf New CheckIssuesDelegateCaller(CheckIssues).Invoke
        CType(NewIssue, System.ComponentModel.ISupportInitialize).EndInit()
        Return NewIssue
    End Function
    Public Function CreateRefactoring(ByVal IssueName As String, ByVal Apply As ApplyDelegate, Optional ByVal CheckAvailability As CheckAvailabilityDelegate = Nothing, Optional ByVal CodeIssueMessage As String = "") As DevExpress.Refactor.Core.RefactoringProvider
        Dim NewRefactoring As New DevExpress.Refactor.Core.RefactoringProvider(components)
        CType(NewRefactoring, System.ComponentModel.ISupportInitialize).BeginInit()
        NewRefactoring.ProviderName = IssueName ' Should be Unique
        NewRefactoring.CodeIssueMessage = CodeIssueMessage
        AddHandler NewRefactoring.CheckAvailability, AddressOf New CheckAvailabilityDelegateCaller(CheckAvailability).Invoke
        AddHandler NewRefactoring.Apply, AddressOf New ApplyDelegateCaller(Apply).Invoke
        CType(NewRefactoring, System.ComponentModel.ISupportInitialize).EndInit()
        Return NewRefactoring
    End Function
    Public Function CreateCodeProvider(ByVal IssueName As String, ByVal DisplayName As String, ByVal Apply As ApplyDelegate, Optional ByVal CheckAvailability As CheckAvailabilityDelegate = Nothing, Optional ByVal CodeIssueMessage As String = "") As CodeProvider
        Dim NewCodeProvider As New DevExpress.CodeRush.Core.CodeProvider(components)
        CType(NewCodeProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        NewCodeProvider.ProviderName = IssueName ' Should be Unique
        NewCodeProvider.DisplayName = DisplayName
        NewCodeProvider.CodeIssueMessage = CodeIssueMessage
        AddHandler NewCodeProvider.CheckAvailability, AddressOf New CheckAvailabilityDelegateCaller(CheckAvailability).Invoke
        AddHandler NewCodeProvider.Apply, AddressOf New ApplyDelegateCaller(Apply).Invoke
        CType(NewCodeProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Return NewCodeProvider
    End Function
#End Region
#Region "Rule Registration"
    Private Sub RegisterRules()
        NamingRules()
        RegisterCodeProviders()
    End Sub
    Public Sub RegisterCodeProviders()
        Dim CodeProvider = CreateCodeProvider("UpperCaseFirstChar", "Change First Character to Uppercase", AddressOf UppercaseFirstChar, Function() True)
        CodeProvider.SolvedIssues.Add(Message_SA1300)
    End Sub
    Public Sub UppercaseFirstChar(ByVal sender As Object, ByVal ea As ApplyContentEventArgs)
        CodeRush.Refactoring.Get("Rename").Activate()
        'RenameElement(ea.CodeActive, ChangeFirstCharToUpper(ea.CodeActive.Name))
    End Sub
    Public Sub RenameElement(ByVal LE As LanguageElement, ByVal NewName As String)
        LE.Document.QueueReplace(LE.NameRange, NewName)
    End Sub
    Public Function ChangeFirstCharToUpper(ByVal Name As String) As String
        Return Char.ToUpper(Name.First) & Name.Substring(1)
    End Function
    Public Function ChangeFirstCharToLower(ByVal Name As String) As String
        Return Char.ToLower(Name.First) & Name.Substring(1)
    End Function
    Private Sub NamingRules()
        Call CreateIssue("SA1300", AddressOf SA1300_ElementsStartWithUpperCase_CheckCodeIssues)
        Call CreateIssue("SA1302", AddressOf SA1302_InterfacesStartWithI_CheckCodeIssues)
        'Call CreateIssue("SA1303", AddressOf SA1303_ConstantFieldsStartWithUpperCase_CheckCodeIssues)

        Call CreateIssue("SA1304", AddressOf SA1304_NonPrivateReadOnlyFieldsMustStartUppercase_CheckCodeIssues)
        Call CreateIssue("SA1307", AddressOf SA1307_PublicAndInternalFieldsMustStartWithUppercase_CheckCodeIssues)
        Call CreateIssue("SA1306", AddressOf SA1306_FieldsMustStartWithLowercase_CheckCodeIssues)

        Call CreateIssue("SA1305", AddressOf SA1305_VariablesMustNotUseHungarianNotation_CheckCodeIssues)

        Call CreateIssue("SA1308", AddressOf SA1308_FieldsMustNotBePrefixedMorS_CheckCodeIssues)
        Call CreateIssue("SA1309", AddressOf SA1309_FieldsMustNotBePrefixedUnderscore_CheckCodeIssues)
        Call CreateIssue("SA1310", AddressOf SA1310_FieldsMustNotContainUnderscore_CheckCodeIssues)


        Call CreateIssue("Locals start with...", AddressOf LocalsStartWithL_CheckCodeIssues)
        Call CreateIssue("Fields start with...", AddressOf FieldsStartWithFieldPrefix_CheckCodeIssues)
        Call CreateIssue("Parameters start with...", AddressOf ParametersStartWithParamPrefix_CheckCodeIssues)
    End Sub
#End Region

    ' Code: Declare Delegate

    ' CreateIssue("Interfaces start with I", AddressOf InterfacesStartWithI_CheckCodeIssues)
    ' Code: Declare as Extension Method
End Class