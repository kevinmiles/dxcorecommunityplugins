'Imports DevExpress.CodeRush.StructuralParser
'Imports System.Runtime.CompilerServices

'Public Module VariableExt
'    <Extension()> _
'    Public Function ToPropSpec(ByVal Source As IFieldElement) As PropertySpec
'        Return New PropertySpec(Source.Name, _
'                                Source.Type.Name, _
'                                Source.Visibility, _
'                                Source.IsStatic, _
'                                True, _
'                                True)
'    End Function
'    <Extension()> _
'    Public Function ToPropSpec(ByVal Source As IPropertyElement) As PropertySpec
'        Return New PropertySpec(Source.Name, _
'                                Source.Type.Name, _
'                                Source.Visibility, _
'                                Source.IsStatic, _
'                                Source.HasGetter, _
'                                Source.HasSetter)
'    End Function
'    <Extension()> _
'    Public Function HasGetter(ByVal Source As IPropertyElement) As Boolean
'        Return Source.Children.Any(Function(c) c.ElementType = LanguageElementType.PropertyAccessorGet)

'    End Function
'    <Extension()> _
'    Public Function HasSetter(ByVal Source As IPropertyElement) As Boolean
'        Return Source.Children.Any(Function(c) c.ElementType = LanguageElementType.PropertyAccessorSet)
'    End Function
'End Module