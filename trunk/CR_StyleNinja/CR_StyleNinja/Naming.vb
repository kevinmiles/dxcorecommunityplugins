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
            Dim Finder = From e In Elements(ea.Scope.ToLE) Where LocalWithPoorPrefix(e)
            'Dim Finder = From l In Locals(ea.Scope.ToLE) Where Not l.Name.StartsWith(CodeRush.CodeStyle.PrefixLocal)
            For Each FoundItem As Variable In Finder
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
            Dim Finder = From e In Elements(ea.Scope.ToLE) Where FieldWithPoorPrefix(e)
            'Dim Finder = From f In Fields(ea.Scope.ToLE) Where Not f.Name.StartsWith(CodeRush.CodeStyle.PrefixField)
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
            Dim Finder = From e In Elements(ea.Scope.ToLE) Where ParamWithPoorPrefix(e)
            'Dim Finder = From p In Params(ea.Scope.ToLE) Where Not p.Name.StartsWith(CodeRush.CodeStyle.PrefixParam)
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
            Dim Finder = From e In Elements(ea.Scope.ToLE) Where Qualifies_SA1300(e)
            For Each FoundItem As Method In Finder
                ea.AddHint(FoundItem.NameRange, Message_SA1300)
            Next
        End If
    End Sub
#End Region
#Region "SA1302: InterfacesStartWithI"
    Friend Const Message_SA1302 As String = "SA1302: Interface should start with 'I'"
    Friend Sub SA1302_InterfacesStartWithI_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From E In Elements(ea.Scope.ToLE) Where Qualifies_SA1302(E)
            For Each FoundItem As [Interface] In Finder
                ea.AddHint(FoundItem.NameRange, Message_SA1302)
            Next
        End If
    End Sub
#End Region
#Region "SA1303: ConstantFieldsStartWithUpperCase"
    'Friend Sub ConstantFieldsStartWithUpperCase_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
    '    If ea.Scope.ToLE IsNot Nothing Then
    '        Dim Finder = ConstantFields(ea.Scope.ToLE).WhereNameStartsLower()
    '        For Each FoundItem As [Const] In Finder
    '            ea.AddHint(FoundItem.NameRange, "Constant Member must start with an uppercase char.")
    '        Next
    '    End If
    'End Sub
#End Region
#Region "SA1304: NonPrivateReadOnlyFieldsMustStartUppercase"
    Friend Const Message_SA1304 As String = "SA1304: Non Private Field must start with an uppercase char."
    Friend Sub SA1304_NonPrivateReadOnlyFieldsMustStartUppercase_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From E In Elements(ea.Scope.ToLE) Where Qualifies_SA1304(E)
            'Dim Finder = From f In Fields(ea.Scope.ToLE) Where isNonPrivate(f) AndAlso f.IsReadOnly AndAlso StartsLower(f)
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, Message_SA1304)
            Next
        End If
    End Sub
#End Region
#Region "SA1305: VariablesMustNotUseHungarianNotation"
    Friend Const Message_SA1305 As String = "SA1305: Variable must not use Hungarian Notation"
    Friend Sub SA1305_VariablesMustNotUseHungarianNotation_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            ' 
            Dim Finder = From e In Elements(ea.Scope.ToLE) Where Qualifies_SA1305(e)
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, Message_SA1305)
            Next
        End If
    End Sub
#End Region
#Region "SA1306: FieldsMustStartWithLowercase"
    Friend Const Message_SA1306 As String = "SA1306: Field must start with a lowercase char."
    Friend Sub SA1306_FieldsMustStartWithLowercase_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From e In Elements(ea.Scope.ToLE) Where Qualifies_SA1306(e)
            'Dim Finder = From f In Fields(ea.Scope.ToLE) Where Not ((isPublicOrInternal(f) OrElse (isPrivateReadonly(f)))) AndAlso StartsUpper(f)
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, Message_SA1306)
            Next
        End If
    End Sub
#End Region
#Region "SA1307: PublicAndInternalFieldsMustStartWithUppercase"
    Friend Const Message_SA1307 As String = "SA1307: Field must start with uppercase char."
    Friend Sub SA1307_PublicAndInternalFieldsMustStartWithUppercase_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From e In Elements(ea.Scope.ToLE) Where Qualifies_SA1307(e)
            'Dim Finder = From f In Fields(ea.Scope.ToLE) Where isPublicOrInternal(f) AndAlso StartsLower(f)
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, Message_SA1307)
            Next
        End If
    End Sub
#End Region
#Region "SA1308: FieldsMustNotBePrefixed"
    Friend Const Message_SA1308 As String = "SA1308: Field must not be prefixed with m_ or s_"
    Friend Sub SA1308_FieldsMustNotBePrefixedMorS_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From e In Elements(ea.Scope.ToLE) Where Qualifies_SA1308(e)
            'Dim Finder = From f In Fields(ea.Scope.ToLE) Where f.Name.StartsWith("s_") OrElse f.Name.StartsWith("m_")
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, Message_SA1308)
            Next
        End If
    End Sub
#End Region
#Region "SA1309: FieldsMustNotBePrefixedUnderscore"
    Friend Const Message_SA1309 As String = "SA1309: Field must not be prefixed with underscore"
    Friend Sub SA1309_FieldsMustNotBePrefixedUnderscore_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From e In Elements(ea.Scope.ToLE) Where Qualifies_SA1309(e)
            'Dim Finder = From f In Fields(ea.Scope.ToLE) Where f.Name.StartsWith("_")
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, Message_SA1309)

            Next
        End If
    End Sub
#End Region
#Region "SA1310: FieldsMustNotContainUnderscore"
    Friend Const Message_SA1310 As String = "SA1310: Field must not contain underscores"
    Friend Sub SA1310_FieldsMustNotContainUnderscore_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        If ea.Scope.ToLE IsNot Nothing Then
            Dim Finder = From e In Elements(ea.Scope.ToLE) Where Qualifies_SA1310(e)
            'Dim Finder = From f In Fields(ea.Scope.ToLE) Where f.Name.Contains("_")
            For Each FoundItem As Variable In Finder
                ea.AddHint(FoundItem.NameRange, Message_SA1310)
            Next
        End If
    End Sub
#End Region
End Module
