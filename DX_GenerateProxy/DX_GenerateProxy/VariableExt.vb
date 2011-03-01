Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices

Public Module VariableExt
    <Extension()> _
    Public Function ToPropSpec(ByVal Source As Variable) As PropertySpec
        Return New PropertySpec(Source.Name, _
                                Source.MemberType, _
                                Source.Visibility, _
                                Source.IsStatic, _
                                True, _
                                True)
    End Function
    <Extension()> _
    Public Function ToPropSpec(ByVal Source As [Property]) As PropertySpec
        Return New PropertySpec(Source.Name, _
                                Source.MemberType, _
                                Source.Visibility, _
                                Source.IsStatic, _
                                Source.HasGetter, _
                                Source.HasSetter)
    End Function
End Module