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
    Public Function CreateRefactoring(ByVal IssueName As String, ByVal DisplayName As String, ByVal Apply As ApplyDelegate, ByVal CheckContentAvailability As CheckContentAvailabilityDelegate) As DevExpress.Refactor.Core.RefactoringProvider
        Dim NewRefactoring As New DevExpress.Refactor.Core.RefactoringProvider(components)
        CType(NewRefactoring, System.ComponentModel.ISupportInitialize).BeginInit()
        NewRefactoring.ProviderName = IssueName ' Should be Unique
        NewRefactoring.DisplayName = DisplayName
        AddHandler NewRefactoring.CheckAvailability, AddressOf New CheckContentAvailabilityDelegateCaller(CheckContentAvailability).Invoke
        AddHandler NewRefactoring.Apply, AddressOf New ApplyDelegateCaller(Apply).Invoke

        CType(NewRefactoring, System.ComponentModel.ISupportInitialize).EndInit()
        Return NewRefactoring
    End Function
    Public Function CreateCodeProvider(ByVal IssueName As String, ByVal DisplayName As String, ByVal Apply As ApplyDelegate, ByVal CheckContentAvailability As CheckContentAvailabilityDelegate) As CodeProvider
        Dim NewCodeProvider As New CodeProvider(components)
        CType(NewCodeProvider, System.ComponentModel.ISupportInitialize).BeginInit()
        NewCodeProvider.ProviderName = IssueName ' Should be Unique
        NewCodeProvider.DisplayName = DisplayName
        AddHandler NewCodeProvider.CheckAvailability, AddressOf New CheckContentAvailabilityDelegateCaller(CheckContentAvailability).Invoke
        AddHandler NewCodeProvider.Apply, AddressOf New ApplyDelegateCaller(Apply).Invoke
        CType(NewCodeProvider, System.ComponentModel.ISupportInitialize).EndInit()
        Return NewCodeProvider
    End Function
#End Region
#Region "Rule Registration"
    Private Sub RegisterRules()
        RegisterNamingRules()
        RegisterRefactorings()
    End Sub
    Friend Sub RegisterNamingRules()
        Call CreateIssue("SA1302", AddressOf Checker.InterfaceChecker(AddressOf Qualifies_SA1302, Message_SA1302).CheckCodeIssues)
        'Call CreateIssue("SA1303", AddressOf SA1303_ConstantFieldsStartWithUpperCase_CheckCodeIssues)

        ' Rules: Require Uppercase 
        Call CreateIssue("SA1300", AddressOf Checker.MainElementChecker(AddressOf Qualifies_SA1300, Message_SA1300).CheckCodeIssues)
        Call CreateIssue("SA1304", AddressOf Checker.FieldChecker(AddressOf Qualifies_SA1304, Message_SA1304).CheckCodeIssues)
        Call CreateIssue("SA1307", AddressOf Checker.FieldChecker(AddressOf Qualifies_SA1307, Message_SA1307).CheckCodeIssues)

        ' Rules: Require Lowercase
        Call CreateIssue("SA1306", AddressOf Checker.FieldChecker(AddressOf Qualifies_SA1306, Message_SA1306).CheckCodeIssues)

        Call CreateIssue("SA1305", AddressOf Checker.VariableChecker(AddressOf Qualifies_SA1305, Message_SA1305).CheckCodeIssues)


        'Rules: Prefixes and Underscores.
        Call CreateIssue("SA1308", AddressOf Checker.FieldChecker(AddressOf Qualifies_SA1308, Message_SA1308).CheckCodeIssues)
        Call CreateIssue("SA1309", AddressOf Checker.FieldChecker(AddressOf Qualifies_SA1309, Message_SA1309).CheckCodeIssues)
        Call CreateIssue("SA1310", AddressOf Checker.FieldChecker(AddressOf Qualifies_SA1310, Message_SA1310).CheckCodeIssues)


        Call CreateIssue("Locals start with...", AddressOf Checker.LocalChecker(AddressOf Qualifies_LocalWithPoorPrefix, Message_LocalsShouldStart, AddressOf LocalPrefixNotSet).CheckCodeIssues)
        Call CreateIssue("Fields start with...", AddressOf Checker.FieldChecker(AddressOf Qualifies_FieldWithPoorPrefix, Message_FieldsShouldStart, AddressOf FieldPrefixNotSet).CheckCodeIssues)
        Call CreateIssue("Parameters start with...", AddressOf Checker.ParamChecker(AddressOf Qualifies_ParamWithPoorPrefix, Message_ParamsShouldStart, AddressOf ParamPrefixNotSet).CheckCodeIssues)
    End Sub

    Public Sub RegisterRefactorings()
        Dim UppercaseInitial = CreateRefactoring("UppercaseFirstChar", "Change first Character to Uppercase", AddressOf UppercaseFirstChar_Apply, AddressOf ShouldBeUppercased_Check)
        UppercaseInitial.SolvedIssues.Add(Message_SA1300) ' MainElement and InitialLower
        UppercaseInitial.SolvedIssues.Add(Message_SA1304) ' NonPrivateField and InitialLower
        UppercaseInitial.SolvedIssues.Add(Message_SA1307) ' PublicOrInternalField and InitialLower

        '-----------------------------------------
        Dim LowercaseInitial = CreateRefactoring("LowercaseFirstChar", "Change first Character to Lowercase", AddressOf LowercaseFirstChar_Apply, AddressOf ShouldBeLowercased_Check)
        LowercaseInitial.SolvedIssues.Add(Message_SA1306) ' Not (NonPrivateField) and InitialUpper

        '-----------------------------------------
        Dim PrefixNameWithI = CreateRefactoring("PrefixNameWithI", "Prefix name with 'I'", AddressOf PrefixNameWithI_Apply, AddressOf InterfaceNotPrefixedI_Check)
        PrefixNameWithI.SolvedIssues.Add(Message_SA1302)

        '-----------------------------------------
        Dim RemoveHungarianPrefix = CreateRefactoring("RemoveHungarianPrefix", "Remove Hungarian Prefix", AddressOf RemoveHungarianPrefix_Apply, AddressOf PrefixedWithHungarian_Check)
        RemoveHungarianPrefix.SolvedIssues.Add(Message_SA1305)

        '-----------------------------------------
        Dim RemoveUnderscores = CreateRefactoring("RemoveUnderscores", "Remove Underscores", AddressOf RemoveUnderscores_Apply, AddressOf ContainsUnderscores_Check)
        RemoveUnderscores.SolvedIssues.Add(Message_SA1309)
        RemoveUnderscores.SolvedIssues.Add(Message_SA1310)

    End Sub

#End Region

    ' Code: Declare Delegate

    ' CreateIssue("Interfaces start with I", AddressOf InterfacesStartWithI_CheckCodeIssues)
    ' Code: Declare as Extension Method
End Class
