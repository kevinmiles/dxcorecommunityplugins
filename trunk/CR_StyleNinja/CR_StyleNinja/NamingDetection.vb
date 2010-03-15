Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Core

Module NamingQualification
#Region "Utility"
    Private Function isMainElement(ByVal E As IElement) As Boolean
        Return NamespacesClassesEnumsStructsDelegatesEventsMethodsPropertiesTypes.Contains(E.ElementType)
    End Function

    Private Function isPrivateReadonly(ByVal e As IMemberElement) As Boolean
        Return e.Visibility = MemberVisibility.Private AndAlso e.IsReadOnly
    End Function
    Private Function isPublicOrInternal(ByVal e As IMemberElement) As Boolean
        Return e.Visibility = MemberVisibility.Public _
                                         OrElse e.Visibility = MemberVisibility.Friend
    End Function
    Private Function isNonPrivate(ByVal e As IMemberElement) As Boolean
        Return e.Visibility <> MemberVisibility.Private
    End Function

    Private Function StartsLower(ByVal V As IElement) As Boolean
        Return Char.IsLower(V.Name.First)
    End Function
    Private Function StartsUpper(ByVal V As IElement) As Boolean
        Return Char.IsUpper(V.Name.First)
    End Function
#End Region
#Region "CodeRush Style Rule Enforcement"
    Friend Function LocalWithPoorPrefix(ByVal Element As IElement) As Boolean
        Dim Variable = TryCast(Element, Variable)
        If Variable Is Nothing Then
            Return False
        End If

        Return Variable.IsLocal _
        AndAlso Not Element.Name.StartsWith(CodeRush.CodeStyle.PrefixLocal)
    End Function
    Friend Function FieldWithPoorPrefix(ByVal Element As IElement) As Boolean
        Dim Variable = TryCast(Element, Variable)
        If Variable Is Nothing Then
            Return False
        End If

        Return Variable.IsField _
        AndAlso Not Element.Name.StartsWith(CodeRush.CodeStyle.PrefixParam)
    End Function
    Friend Function ParamWithPoorPrefix(ByVal Element As IElement) As Boolean
        Return Element.ElementType = LanguageElementType.Parameter _
        AndAlso Not Element.Name.StartsWith(CodeRush.CodeStyle.PrefixParam)
    End Function
#End Region

#Region ""
    Friend Function Qualifies_SA1300(ByVal Element As IElement) As Boolean
        Return isMainElement(Element) AndAlso StartsLower(Element)
    End Function
    Friend Function Qualifies_SA1302(ByVal Element As IElement) As Boolean
        Return Element.ElementType = LanguageElementType.Interface AndAlso Not Element.Name.StartsWith("I")
    End Function
    Friend Function Qualifies_SA1304(ByVal Element As IElement) As Boolean
        Dim Variable = TryCast(Element, Variable)
        If Variable Is Nothing Then
            Return False
        End If
        Return isNonPrivate(Variable) AndAlso Variable.IsReadOnly AndAlso StartsLower(Variable)
    End Function
    Friend Function Qualifies_SA1305(ByVal Element As IElement) As Boolean
        Dim Variable = TryCast(Element, Variable)
        If Variable Is Nothing Then
            Return False
        End If
        Return (Variable.IsField OrElse Variable.IsLocal) AndAlso Variable.Name.IsHungarian
    End Function
    Friend Function Qualifies_SA1306(ByVal Element As IElement) As Boolean
        Dim Variable = TryCast(Element, Variable)
        If Variable Is Nothing Then
            Return False
        End If
        Return Not ((isPublicOrInternal(Variable) OrElse (isPrivateReadonly(Variable)))) AndAlso StartsUpper(Variable)
    End Function
    Friend Function Qualifies_SA1307(ByVal Element As IElement) As Boolean
        Dim Variable = TryCast(Element, Variable)
        If Variable Is Nothing Then
            Return False
        End If
        Return Variable.IsField AndAlso isPublicOrInternal(Variable) AndAlso StartsLower(Variable)
    End Function
    Friend Function Qualifies_SA1308(ByVal Element As IElement) As Boolean
        Dim Variable = TryCast(Element, Variable)
        If Variable Is Nothing Then
            Return False
        End If
        Return Variable.IsField AndAlso Variable.Name.StartsWith("s_") OrElse Variable.Name.StartsWith("m_")
    End Function
    Friend Function Qualifies_SA1309(ByVal Element As IElement) As Boolean
        Dim Variable = TryCast(Element, Variable)
        If Variable Is Nothing Then
            Return False
        End If
        Return Variable.IsField AndAlso Variable.Name.StartsWith("_")
    End Function
    Friend Function Qualifies_SA1310(ByVal Element As IElement) As Boolean
        Dim Variable = TryCast(Element, Variable)
        If Variable Is Nothing Then
            Return False
        End If
        Return Variable.IsField AndAlso Variable.Name.Contains("_")
    End Function
#End Region

End Module
