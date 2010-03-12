Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Public Module Naming

#Region "LocalsStartWith..."
    Friend Sub LocalsStartWithL_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
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
    Friend Sub FieldsStartWithFieldPrefix_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
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
    Friend Sub ParametersStartWithParamPrefix_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
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
#Region "SA1300: ElementsStartWithUpperCase"
    Friend Const Message_SA1300 As String = "SA1300: Element does not start with an uppercase char."
    Friend Sub SA1300_ElementsStartWithUpperCase_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From e In MainElements(ea.Scope.ToLE) Where StartsLower(e)
            For Each FoundItem As Method In Finder
                ea.AddHint(FoundItem.NameRange, Message_SA1300)
            Next
        End If
    End Sub
#End Region
#Region "SA1302: InterfacesStartWith..."
    Friend Sub SA1302_InterfacesStartWithI_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From I In Interfaces(ea.Scope.ToLE) Where Not I.Name.StartsWith("I")
            For Each FoundItem As [Interface] In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("SA1302: Interface {0} should start with 'I'", FoundItem))
            Next
        End If
    End Sub
#End Region
#Region "SA1303: ConstantFieldsStartWithUpperCase"
    'Friend Sub ConstantFieldsStartWithUpperCase_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
    '    If ea.Scope.ToLE IsNot Nothing Then
    '        Dim Finder = ConstantFields(ea.Scope.ToLE).WhereNameStartsLower()
    '        For Each FoundItem As [Const] In Finder
    '            ea.AddHint(FoundItem.NameRange, String.Format("Constant Member '{0}' must start with an uppercase char.", FoundItem.Name))
    '        Next
    '    End If
    'End Sub
#End Region
#Region "SA1304: NonPrivateReadOnlyFieldsMustStartUppercase"
    Friend Sub SA1304_NonPrivateReadOnlyFieldsMustStartUppercase_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From f In Fields(ea.Scope.ToLE) Where isNonPrivate(f) AndAlso f.IsReadOnly AndAlso StartsLower(f)
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("SA1304: Non Private Field '{0}' must start with an uppercase char.", FoundItem.Name))
            Next
        End If
    End Sub
#End Region
#Region "SA1305: VariablesMustNotUseHungarianNotation"
    Friend Sub SA1305_VariablesMustNotUseHungarianNotation_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = FieldsAndLocals(ea.Scope.ToLE).WhereHungarianNotation()
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("SA1305: Variable '{0}' must not use Hungarian Notation", FoundItem.Name))
            Next
        End If
    End Sub
#End Region
#Region "SA1306: FieldsMustStartWithLowercase"
    Friend Sub SA1306_FieldsMustStartWithLowercase_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From f In Fields(ea.Scope.ToLE) Where Not ((isPublicOrInternal(f) OrElse (isPrivateReadonly(f)))) AndAlso StartsUpper(f)
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("SA1306: Field '{0}' must start with a lowercase char.", FoundItem.Name))
            Next
        End If
    End Sub
#End Region
#Region "SA1307: PublicAndInternalFieldsMustStartWithUppercase"
    Friend Sub SA1307_PublicAndInternalFieldsMustStartWithUppercase_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From f In Fields(ea.Scope.ToLE) Where isPublicOrInternal(f) AndAlso StartsLower(f)
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("SA1307: Field '{0}' must start with uppercase char.", FoundItem.Name))
            Next
        End If
    End Sub
#End Region
#Region "SA1308: FieldsMustNotBePrefixed"
    Friend Sub SA1308_FieldsMustNotBePrefixedMorS_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From f In Fields(ea.Scope.ToLE) Where f.Name.StartsWith("s_") OrElse f.Name.StartsWith("m_")
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("SA1308: Field '{0}' must not be prefixed with m_ or s_", FoundItem.Name))
            Next
        End If
    End Sub
#End Region
#Region "SA1309: FieldsMustNotBePrefixedUnderscore"
    Friend Sub SA1309_FieldsMustNotBePrefixedUnderscore_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From f In Fields(ea.Scope.ToLE) Where f.Name.StartsWith("_")
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("SA1309: Field '{0}' must not be prefixed with underscore", FoundItem.Name))

            Next
        End If
    End Sub
#End Region
#Region "SA1310: FieldsMustNotContainUnderscore"
    Friend Sub SA1310_FieldsMustNotContainUnderscore_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From f In Fields(ea.Scope.ToLE) Where f.Name.Contains("_")
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, String.Format("SA1310: Field '{0}' must not contain underscores", FoundItem.Name))

            Next
        End If
    End Sub
#End Region
End Module
