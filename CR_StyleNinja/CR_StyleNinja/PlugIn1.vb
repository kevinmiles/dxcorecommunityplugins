Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
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
    Public Function NamespacesClassesEnumsSructsDelegatesEventsMethodsProperties(ByVal Scope As LanguageElement) As IEnumerable(Of LanguageElement)
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
        Dim Scope = TryCast(ea.Scope, LanguageElement)
        If Scope Is Nothing Then
            Exit Sub
        End If
        Dim Finder = Locals(Scope).Where(Function(l) Not l.Name.StartsWith(CodeRush.CodeStyle.PrefixLocal))
        For Each FoundItem In Finder
            ea.AddHint(FoundItem.NameRange, String.Format("Local {0} should start with '{1}'", FoundItem.Name, CodeRush.CodeStyle.PrefixLocal))
        Next
    End Sub
#End Region
#Region "FieldsStartWith..."
    Public Sub FieldsStartWithFieldPrefix_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        Dim Scope = TryCast(ea.Scope, LanguageElement)
        If Scope Is Nothing Then
            Exit Sub
        End If
        Dim Finder = Fields(Scope).Where(Function(f) Not f.Name.StartsWith(CodeRush.CodeStyle.PrefixField))
        For Each FoundItem As Variable In Finder
            ea.AddHint(FoundItem.NameRange, String.Format("Field {0} should start with '{1}'", FoundItem.Name, CodeRush.CodeStyle.PrefixField))
        Next
    End Sub
#End Region
#Region "InterfacesStartWith..."
    Public Sub InterfacesStartWithI_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        Dim Scope = TryCast(ea.Scope, LanguageElement)
        If Scope Is Nothing Then
            Exit Sub
        End If
        Dim Finder = Interfaces(Scope).Where(Function(I) Not I.Name.StartsWith("I"))
        For Each FoundItem As [Interface] In Finder
            ea.AddHint(FoundItem.NameRange, String.Format("Interface {0} should start with 'I'", FoundItem))
        Next
    End Sub
#End Region
#Region "ElementsStartWithUpperCase"
    Public Sub ElementsStartWithUpperCase_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        Dim Scope = TryCast(ea.Scope, LanguageElement)
        If Scope Is Nothing Then
            Exit Sub
        End If
        Dim Finder = NamespacesClassesEnumsSructsDelegatesEventsMethodsProperties(Scope).Where(Function(e) Char.IsLower(e.Name.First))
        For Each FoundItem As Method In Finder
            ea.AddHint(FoundItem.NameRange, String.Format("Element '{0} does not start with an upper case char.", FoundItem.Name))

        Next
    End Sub
#End Region
#Region "ParametersStartWithParamPrefix"
    Public Sub ParametersStartWithParamPrefix_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        Dim Scope = TryCast(ea.Scope, LanguageElement)
        If Scope Is Nothing Then
            Exit Sub
        End If
        Dim Finder = Params(Scope).Where(Function(p) Not p.Name.StartsWith(CodeRush.CodeStyle.PrefixParam))
        For Each FoundItem As Param In Finder
            ea.AddHint(FoundItem.NameRange, String.Format("Parameter '{0}' does not start with '{1}'", FoundItem.Name, CodeRush.CodeStyle.PrefixParam))
        Next
    End Sub
#End Region

End Class
