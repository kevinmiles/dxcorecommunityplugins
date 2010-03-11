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
    Private NamespacesClassesEnumsSructsDelegatesEventsMethodsPropertiesTypes As LanguageElementType() = {LanguageElementType.Class, LanguageElementType.Struct, LanguageElementType.Enum, LanguageElementType.Namespace, LanguageElementType.Property, LanguageElementType.Method, LanguageElementType.Event, LanguageElementType.Delegate}
    Public Sub CreateIssue(ByVal IssueName As String, ByVal CheckIssues As CheckCodeIssuesDelegate)
        Dim NewIssue As New DevExpress.CodeRush.Core.IssueProvider(components)
        CType(NewIssue, System.ComponentModel.ISupportInitialize).BeginInit()
        NewIssue.ProviderName = IssueName ' Should be Unique
        AddHandler NewIssue.CheckCodeIssues, AddressOf New CheckIssuesDelegateCaller(CheckIssues).Invoke
        CType(NewIssue, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
#End Region
#Region "Enumerators"
    Private Function Locals(ByVal Scope As LanguageElement) As IEnumerable(Of Variable)
        Return Variables(Scope).Where(Function(v) v.IsLocal)
    End Function
    Private Function Fields(ByVal Scope As LanguageElement) As IEnumerable(Of Variable)
        Return Variables(Scope).Where(Function(v) v.IsField)
    End Function
    Private Function Params(ByVal Scope As LanguageElement) As IEnumerable(Of Param)
        Return New ElementEnumerable(Scope, GetType(Param), True).OfType(Of Param)()
    End Function
    Private Function Variables(ByVal Scope As LanguageElement) As IEnumerable(Of Variable)
        Return New ElementEnumerable(Scope, GetType(Variable), True).OfType(Of Variable)()
    End Function
    Private Function Classes(ByVal Scope As LanguageElement) As IEnumerable(Of [Class])
        Return New ElementEnumerable(Scope, GetType([Class]), True).OfType(Of [Class])()
    End Function
    Private Function Interfaces(ByVal Scope As LanguageElement) As IEnumerable(Of [Interface])
        Return New ElementEnumerable(Scope, GetType([Interface]), True).OfType(Of [Interface])()
    End Function
    Public Function MainElements(ByVal Scope As LanguageElement) As IEnumerable(Of LanguageElement)
        Return New ElementEnumerable(Scope, NamespacesClassesEnumsSructsDelegatesEventsMethodsPropertiesTypes, True).OfType(Of LanguageElement)()
    End Function
#End Region
    Public Sub RegisterIssues()
        ' SA1302
        Call CreateIssue("Interfaces start with I", AddressOf InterfacesStartWithI_CheckCodeIssues)
        Call CreateIssue("Element starts with uppercase char", AddressOf ElementsStartWithUpperCase_CheckCodeIssues)

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
            Dim Finder = Locals(ea.Scope.ToLE).WhereNotNameStarts(CodeRush.CodeStyle.PrefixLocal)
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
            Dim Finder = Fields(ea.Scope.ToLE).WhereNotNameStarts(CodeRush.CodeStyle.PrefixField)
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
            Dim Finder = Params(ea.Scope.ToLE).WhereNotNameStarts(CodeRush.CodeStyle.PrefixParam)
            For Each FoundItem As Param In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("Parameter '{0}' does not start with '{1}'", FoundItem.Name, CodeRush.CodeStyle.PrefixParam))
            Next
        End If
    End Sub
#End Region
#Region "InterfacesStartWith..."
    Public Sub InterfacesStartWithI_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = Interfaces(ea.Scope.ToLE).WhereNotNameStarts("I")
            For Each FoundItem As [Interface] In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("Interface {0} should start with 'I'", FoundItem))
            Next
        End If
    End Sub
#End Region
#Region "ElementsStartWithUpperCase"
    Public Sub ElementsStartWithUpperCase_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = MainElements(ea.Scope.ToLE).WhereNameStartsLower()
            For Each FoundItem As Method In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("Element '{0}' does not start with an upper case char.", FoundItem.Name))
            Next
        End If
    End Sub
#End Region

End Class