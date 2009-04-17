Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Public Module ElementTypes
    Public VarDeclarationTypes() As LanguageElementType = _
                                         {LanguageElementType.Variable, _
                                          LanguageElementType.Parameter, _
                                          LanguageElementType.InitializedVariable}

    Public ConstDeclarationTypes() As LanguageElementType = {LanguageElementType.Const}

    Public VarAndConstDeclarationTypes() As LanguageElementType = _
                                                {LanguageElementType.Variable, _
                                                 LanguageElementType.Parameter, _
                                                 LanguageElementType.InitializedVariable, _
                                                 LanguageElementType.Const}
    Public MethodDeclarationTypes() As LanguageElementType = {LanguageElementType.Method}
    Public AllDeclarationTypes() As LanguageElementType = {LanguageElementType.Variable, _
                                                        LanguageElementType.Parameter, _
                                                        LanguageElementType.InitializedVariable, _
                                                        LanguageElementType.Const, _
                                                        LanguageElementType.Method, _
                                                        LanguageElementType.Property}

    Public VarReferenceTypes() As LanguageElementType = _
                                         {LanguageElementType.TypeReferenceExpression, _
                                          LanguageElementType.ElementReferenceExpression, _
                                          LanguageElementType.ParameterReference}
    Public MethodReferenceTypes() As LanguageElementType = {LanguageElementType.MethodCall, LanguageElementType.MethodCallExpression}
    Public AllReferenceTypes() As LanguageElementType = _
                                         {LanguageElementType.TypeReferenceExpression, _
                                          LanguageElementType.ElementReferenceExpression, _
                                          LanguageElementType.ParameterReference, _
                                          LanguageElementType.MethodCall, _
                                          LanguageElementType.MethodCallExpression}
    'Public UsableTypes() As LanguageElementType = _
    '                                     {LanguageElementType.Variable, _
    '                                      LanguageElementType.Parameter, _
    '                                      LanguageElementType.TypeReferenceExpression, _
    '                                      LanguageElementType.ElementReferenceExpression, _
    '                                      LanguageElementType.Method}
End Module
