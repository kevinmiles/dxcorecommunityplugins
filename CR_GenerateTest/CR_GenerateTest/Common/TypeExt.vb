Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports System.IO
Imports System.Runtime.CompilerServices
Imports DevExpress.CodeRush.Diagnostics.Core
Imports EnvDTE80


Public Module TypeExt
    <Extension()> _
    Public Function GetMethodWithName(ByVal ParentType As TypeDeclaration, ByVal MethodName As String) As Method
        Return ParentType.FirstMethodWhere(Function(M) M.Name = MethodName)
    End Function
    <Extension()> _
    Public Function FirstMethodWhere(ByVal TestType As TypeDeclaration, ByVal Func As Func(Of Method, Boolean)) As Method
        Return TestType.AllMethods.OfType(Of Method).Where(Func).FirstOrDefault
    End Function
End Module
