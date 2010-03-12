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
    Public Sub CreateIssue(ByVal IssueName As String, ByVal CheckIssues As CheckCodeIssuesDelegate)
        Dim NewIssue As New DevExpress.CodeRush.Core.IssueProvider(components)
        CType(NewIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        NewIssue.ProviderName = IssueName ' Should be Unique
        AddHandler NewIssue.CheckCodeIssues, AddressOf New CheckIssuesDelegateCaller(CheckIssues).Invoke
        CType(NewIssue, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Public Sub CreateRefactoring(ByVal IssueName As String, ByVal Apply As ApplyDelegate, Optional ByVal CheckAvailability As CheckAvailabilityDelegate = Nothing, Optional ByVal CodeIssueMessage As String = "")
        Dim NewRefactoring As New DevExpress.Refactor.Core.RefactoringProvider(components)
        CType(NewRefactoring, System.ComponentModel.ISupportInitialize).BeginInit()
        NewRefactoring.ProviderName = IssueName ' Should be Unique
        NewRefactoring.CodeIssueMessage = CodeIssueMessage
        AddHandler NewRefactoring.CheckAvailability, AddressOf New CheckAvailabilityDelegateCaller(CheckAvailability).Invoke
        AddHandler NewRefactoring.Apply, AddressOf New ApplyDelegateCaller(Apply).Invoke
        CType(NewRefactoring, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Public Sub CreateCodeProvider(ByVal IssueName As String, ByVal Apply As ApplyDelegate, Optional ByVal CheckAvailability As CheckAvailabilityDelegate = Nothing, Optional ByVal CodeIssueMessage As String = "")
        Dim NewCodeProvider As New DevExpress.CodeRush.Core.CodeProvider(components)
        CType(NewCodeProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        NewCodeProvider.ProviderName = IssueName ' Should be Unique
        NewCodeProvider.CodeIssueMessage = CodeIssueMessage
        AddHandler NewCodeProvider.CheckAvailability, AddressOf New CheckAvailabilityDelegateCaller(CheckAvailability).Invoke
        AddHandler NewCodeProvider.Apply, AddressOf New ApplyDelegateCaller(Apply).Invoke
        CType(NewCodeProvider, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
#End Region
#Region "Rule Registration"
    Private Sub RegisterRules()
        RegisterCodeProviders()
        NamingRules()
    End Sub
    Public Sub RegisterCodeProviders()
    End Sub
    Private Sub NamingRules()
        Call CreateIssue("Element start with uppercase char", AddressOf SA1300_ElementsStartWithUpperCase_CheckCodeIssues)
        Call CreateIssue("Interfaces start with I", AddressOf SA1302_InterfacesStartWithI_CheckCodeIssues)
        'Call CreateCodeProvider("Prefix Var with 'I'",AddressOf PrefixVarWithI,Nothing, "
        'Call CreateIssue("Field Consts start with uppercase char", AddressOf SA1303_ConstantFieldsStartWithUpperCase_CheckCodeIssues)

        Call CreateIssue("Non Private Fields start with uppercase char", AddressOf SA1304_NonPrivateReadOnlyFieldsMustStartUppercase_CheckCodeIssues)
        Call CreateIssue("Public or Internal Fields start with uppercase char", AddressOf SA1307_PublicAndInternalFieldsMustStartWithUppercase_CheckCodeIssues)
        Call CreateIssue("Variables start with lowercase char", AddressOf SA1306_FieldsMustStartWithLowercase_CheckCodeIssues)

        Call CreateIssue("Variables must not use Hungarian notation", AddressOf SA1305_VariablesMustNotUseHungarianNotation_CheckCodeIssues)

        Call CreateIssue("Field names must not prefixed m_ or s_", AddressOf SA1308_FieldsMustNotBePrefixedMorS_CheckCodeIssues)
        Call CreateIssue("Field names must not be prefixed _ ", AddressOf SA1309_FieldsMustNotBePrefixedUnderscore_CheckCodeIssues)
        Call CreateIssue("Field names must not contain _ ", AddressOf SA1310_FieldsMustNotContainUnderscore_CheckCodeIssues)


        Call CreateIssue("Locals start with...", AddressOf LocalsStartWithL_CheckCodeIssues)
        Call CreateIssue("Fields start with...", AddressOf FieldsStartWithFieldPrefix_CheckCodeIssues)
        Call CreateIssue("Parameters start with...", AddressOf ParametersStartWithParamPrefix_CheckCodeIssues)
    End Sub
#End Region

    ' Code: Declare Delegate

    ' CreateIssue("Interfaces start with I", AddressOf InterfacesStartWithI_CheckCodeIssues)
    ' Code: Declare as Extension Method
End Class