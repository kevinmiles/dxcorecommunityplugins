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

#Region "Create Components"
    Public Function CreateIssue(ByVal IssueName As String, ByVal CheckIssues As CheckCodeIssuesDelegate) As IssueProvider
        Dim NewIssue As New IssueProvider(components)
        CType(NewIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        NewIssue.ProviderName = IssueName ' Should be Unique
        AddHandler NewIssue.CheckCodeIssues, AddressOf New CheckIssuesDelegateCaller(CheckIssues).Invoke
        CType(NewIssue, System.ComponentModel.ISupportInitialize).EndInit()
        Return NewIssue
    End Function
    Public Function CreateRefactoring(ByVal IssueName As String, ByVal DisplayName As String, ByVal Apply As ApplyDelegate, Optional ByVal CheckContentAvailability As CheckContentAvailabilityDelegate = Nothing, Optional ByVal CodeIssueMessage As String = "") As DevExpress.Refactor.Core.RefactoringProvider
        Dim NewRefactoring As New DevExpress.Refactor.Core.RefactoringProvider(components)
        CType(NewRefactoring, System.ComponentModel.ISupportInitialize).BeginInit()
        NewRefactoring.ProviderName = IssueName ' Should be Unique
        NewRefactoring.CodeIssueMessage = CodeIssueMessage
        NewRefactoring.DisplayName = DisplayName
        NewRefactoring.Register = True
        AddHandler NewRefactoring.CheckAvailability, AddressOf New CheckContentAvailabilityDelegateCaller(CheckContentAvailability).Invoke
        AddHandler NewRefactoring.Apply, AddressOf New ApplyDelegateCaller(Apply).Invoke

        CType(NewRefactoring, System.ComponentModel.ISupportInitialize).EndInit()
        Return NewRefactoring
    End Function
    Public Function CreateCodeProvider(ByVal IssueName As String, ByVal DisplayName As String, ByVal Apply As ApplyDelegate, Optional ByVal CheckContentAvailability As CheckContentAvailabilityDelegate = Nothing, Optional ByVal CodeIssueMessage As String = "") As CodeProvider
        Dim NewCodeProvider As New CodeProvider(components)
        CType(NewCodeProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        NewCodeProvider.ProviderName = IssueName ' Should be Unique
        'NewCodeProvider.SolvedIssues.Add(CodeIssueMessage)
        'NewCodeProvider.CodeIssueMessage = CodeIssueMessage
        NewCodeProvider.DisplayName = DisplayName
        'NewCodeProvider.Register = True
        AddHandler NewCodeProvider.CheckAvailability, AddressOf New CheckContentAvailabilityDelegateCaller(CheckContentAvailability).Invoke
        AddHandler NewCodeProvider.Apply, AddressOf New ApplyDelegateCaller(Apply).Invoke
        CType(NewCodeProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Return NewCodeProvider
    End Function
#End Region
#Region "Rule Registration"
    Private Sub RegisterRules()
        RegisterNamingRules()
        RegisterCodeProviders()
    End Sub
    Friend Sub RegisterNamingRules()
        Call CreateIssue("SA1302", AddressOf SA1302_InterfacesStartWithI_CheckCodeIssues)
        'Call CreateIssue("SA1303", AddressOf SA1303_ConstantFieldsStartWithUpperCase_CheckCodeIssues)

        ' Rules: Require Uppercase 
        Call CreateIssue("SA1300", AddressOf SA1300_ElementsStartWithUpperCase_CheckCodeIssues)
        Call CreateIssue("SA1304", AddressOf SA1304_NonPrivateReadOnlyFieldsMustStartUppercase_CheckCodeIssues)
        Call CreateIssue("SA1307", AddressOf SA1307_PublicAndInternalFieldsMustStartWithUppercase_CheckCodeIssues)

        ' Rules: Require Lowercase
        Call CreateIssue("SA1306", AddressOf SA1306_FieldsMustStartWithLowercase_CheckCodeIssues)

        Call CreateIssue("SA1305", AddressOf SA1305_VariablesMustNotUseHungarianNotation_CheckCodeIssues)

        Call CreateIssue("SA1308", AddressOf SA1308_FieldsMustNotBePrefixedMorS_CheckCodeIssues)

        'Rules: Underscores are bad.
        Call CreateIssue("SA1309", AddressOf SA1309_FieldsMustNotBePrefixedUnderscore_CheckCodeIssues)
        Call CreateIssue("SA1310", AddressOf SA1310_FieldsMustNotContainUnderscore_CheckCodeIssues)


        Call CreateIssue("Locals start with...", AddressOf LocalsStartWithL_CheckCodeIssues)
        Call CreateIssue("Fields start with...", AddressOf FieldsStartWithFieldPrefix_CheckCodeIssues)
        Call CreateIssue("Parameters start with...", AddressOf ParametersStartWithParamPrefix_CheckCodeIssues)
    End Sub

    Public Sub RegisterCodeProviders()
        Dim UppercaseInitial = CreateCodeProvider("UppercaseFirstChar", "Change First Character to Uppercase", _
                                                  AddressOf UppercaseFirstChar_Apply, AddressOf ShouldBeUppercased_Check)
        UppercaseInitial.SolvedIssues.Add(Message_SA1300) ' MainElement and InitialLower
        UppercaseInitial.SolvedIssues.Add(Message_SA1304) ' NonPrivateField and InitialLower
        UppercaseInitial.SolvedIssues.Add(Message_SA1307) ' PublicOrInternalField and InitialLower

        '-----------------------------------------
        Dim LowercaseInitial = CreateCodeProvider("LowercaseFirstChar", "Change First Character to Lowercase", _
                                                  AddressOf LowercaseFirstChar_Apply, AddressOf ShouldBeLowercased_Check)
        LowercaseInitial.SolvedIssues.Add(Message_SA1306) ' Not (NonPrivateField) and InitialUpper

        '-----------------------------------------
        Dim PrefixNameWithI = CreateCodeProvider("PrefixNameWithI", "Prefix name with 'I'", _
                                          AddressOf PrefixNameWithI_Apply, AddressOf InterfaceNotPrefixedI_Check)
        PrefixNameWithI.SolvedIssues.Add(Message_SA1302)

        '-----------------------------------------
        Dim RemoveHungarianPrefix = CreateCodeProvider("RemoveHungarianPrefix", "Remove Hungarian Prefix", _
                                          AddressOf RemoveHungarianPrefix_Apply, AddressOf PrefixedWithHungarian_Check)
        RemoveHungarianPrefix.SolvedIssues.Add(Message_SA1305)

        '-----------------------------------------
        Dim RemoveUnderscores = CreateCodeProvider("RemoveUnderscores", "Remove Underscores", _
                                          AddressOf RemoveUnderscores_Apply, AddressOf ContainsUnderscores_Check)
        RemoveUnderscores.solvedissues.Add(Message_SA1309)
        RemoveUnderscores.solvedissues.Add(Message_SA1310)

    End Sub

#End Region

    ' Code: Declare Delegate

    ' CreateIssue("Interfaces start with I", AddressOf InterfacesStartWithI_CheckCodeIssues)
    ' Code: Declare as Extension Method
End Class
