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
        RegisterIssues()
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
#End Region
    Public Sub RegisterIssues()
        ' SA1300
        Call CreateIssue("Element start with uppercase char", AddressOf ElementsStartWithUpperCase_CheckCodeIssues)
        ' SA1302
        Call CreateIssue("Interfaces start with I", AddressOf InterfacesStartWithI_CheckCodeIssues)
        ' SA1303
        'Call CreateIssue("Field Consts start with uppercase char", AddressOf ConstantFieldsStartWithUpperCase_CheckCodeIssues)

        ' SA1304
        Call CreateIssue("Non Private Fields start with uppercase char", AddressOf NonPrivateReadOnlyFieldsMustStartUppercase_CheckCodeIssues)
        ' SA1307
        Call CreateIssue("Public or Internal Fields start with uppercase char", AddressOf PublicAndInternalFieldsMustStartWithUppercase_CheckCodeIssues)
        ' SA1306
        Call CreateIssue("Variables start with lowercase char", AddressOf FieldsMustStartWithLowercase_CheckCodeIssues)

        ' SA1305
        Call CreateIssue("Variables must not use Hungarian notation", AddressOf VariablesMustNotUseHungarianNotation_CheckCodeIssues)

        ' SA1308
        Call CreateIssue("Field names must not prefixed m_ or s_", AddressOf FieldsMustNotBePrefixedMorS_CheckCodeIssues)

        ' SA1309
        Call CreateIssue("Field names must not be prefixed _ ", AddressOf FieldsMustNotBePrefixedUnderscore_CheckCodeIssues)

        ' SA1310
        Call CreateIssue("Field names must not contain _ ", AddressOf FieldsMustNotContainUnderscore_CheckCodeIssues)


        Call CreateIssue("Locals start with...", AddressOf LocalsStartWithL_CheckCodeIssues)
        Call CreateIssue("Fields start with...", AddressOf FieldsStartWithFieldPrefix_CheckCodeIssues)
        Call CreateIssue("Parameters start with...", AddressOf ParametersStartWithParamPrefix_CheckCodeIssues)
    End Sub
#Region "LocalsStartWith..."
    Private Sub LocalsStartWithL_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If CodeRush.CodeStyle.PrefixLocal = String.Empty Then
            Exit Sub
        End If
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From l In Locals(ea.Scope.ToLE) Where Not l.Name.StartsWith(CodeRush.CodeStyle.PrefixLocal)
            For Each FoundItem In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("Local {0} should start with '{1}'", FoundItem.Name, CodeRush.CodeStyle.PrefixLocal))
            Next
        End If
    End Sub
#End Region
#Region "FieldsStartWith..."
    Public Sub FieldsStartWithFieldPrefix_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If CodeRush.CodeStyle.PrefixField = String.Empty Then
            Exit Sub
        End If
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From f In Fields(ea.Scope.ToLE) Where Not f.Name.StartsWith(CodeRush.CodeStyle.PrefixField)
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("Field {0} should start with '{1}'", FoundItem.Name, CodeRush.CodeStyle.PrefixField))
            Next
        End If
    End Sub
#End Region
#Region "ParametersStartWithParamPrefix"
    Public Sub ParametersStartWithParamPrefix_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If CodeRush.CodeStyle.PrefixParam = String.Empty Then
            Exit Sub
        End If
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From p In Params(ea.Scope.ToLE) Where Not p.Name.StartsWith(CodeRush.CodeStyle.PrefixParam)
            For Each FoundItem As Param In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("Parameter '{0}' does not start with '{1}'", FoundItem.Name, CodeRush.CodeStyle.PrefixParam))
            Next
        End If
    End Sub
#End Region
#Region "InterfacesStartWith..."
    Public Sub InterfacesStartWithI_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From I In Interfaces(ea.Scope.ToLE) Where Not I.Name.StartsWith("I")
            For Each FoundItem As [Interface] In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("Interface {0} should start with 'I'", FoundItem))
            Next
        End If
    End Sub
#End Region
#Region "ElementsStartWithUpperCase"
    Public Sub ElementsStartWithUpperCase_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From e In MainElements(ea.Scope.ToLE) Where StartsLower(e)
            For Each FoundItem As Method In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("Element '{0}' does not start with an uppercase char.", FoundItem.Name))
            Next
        End If
    End Sub
#End Region
#Region "ConstantFieldsStartWithUpperCase"
    'Public Sub ConstantFieldsStartWithUpperCase_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
    '    If ea.Scope.ToLE IsNot Nothing Then
    '        Dim Finder = ConstantFields(ea.Scope.ToLE).WhereNameStartsLower()
    '        For Each FoundItem As [Const] In Finder
    '            ea.AddHint(FoundItem.NameRange, String.Format("Constant Member '{0}' must start with an uppercase char.", FoundItem.Name))
    '        Next
    '    End If
    'End Sub
#End Region
#Region "NonPrivateReadOnlyFieldsMustStartUppercase"
    Public Sub NonPrivateReadOnlyFieldsMustStartUppercase_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From f In Fields(ea.Scope.ToLE) Where isNonPrivate(f) AndAlso f.IsReadOnly
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("Non Private Field '{0}' must start with an uppercase char.", FoundItem.Name))
            Next
        End If
    End Sub
#End Region
#Region "VariablesMustNotUseHungarianNotation"
    Public Sub VariablesMustNotUseHungarianNotation_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = FieldsAndLocals(ea.Scope.ToLE).WhereHungarianNotation()
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("Variable '{0}' must not use Hungarian Notation", FoundItem.Name))
            Next
        End If
    End Sub
#End Region
#Region "FieldsMustStartWithLowercase"
    Public Sub FieldsMustStartWithLowercase_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From f In Fields(ea.Scope.ToLE) Where Not (isPublicOrInternal(f) OrElse (isPrivateReadonly(f)))
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("Field '{0}' must start with a lowercase char.", FoundItem.Name))
            Next
        End If
    End Sub
#End Region
#Region "PublicAndInternalFieldsMustStartWithUppercase"
    Public Sub PublicAndInternalFieldsMustStartWithUppercase_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From f In Fields(ea.Scope.ToLE) Where isPublicOrInternal(f) And StartsUpper(f)
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("Field '{0}' must start with uppercase char.", FoundItem.Name))
            Next
        End If
    End Sub
#End Region
#Region "FieldsMustNotBePrefixed"
    Public Sub FieldsMustNotBePrefixedMorS_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From f In Fields(ea.Scope.ToLE) Where f.Name.StartsWith("s_") OrElse f.Name.StartsWith("m_")
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("Field '{0}' must not be prefixed with m_ or s_", FoundItem.Name))
            Next
        End If
    End Sub
#End Region
#Region "FieldsMustNotBePrefixedUnderscore"
    Public Sub FieldsMustNotBePrefixedUnderscore_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From f In Fields(ea.Scope.ToLE) Where f.Name.StartsWith("_")
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("Field '{0}' must not be prefixed with underscore", FoundItem.Name))

            Next
        End If
    End Sub
#End Region
    Public Sub FieldsMustNotContainUnderscore_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From f In Fields(ea.Scope.ToLE) Where f.Name.Contains("_")
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("Field '{0}' must not contain underscores", FoundItem.Name))

            Next
        End If
    End Sub
    ' Code: Declare Delegate

    ' CreateIssue("Interfaces start with I", AddressOf InterfacesStartWithI_CheckCodeIssues)
    ' Code: Declare as Extension Method
End Class