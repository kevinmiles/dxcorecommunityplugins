Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices

Public Module NamespaceExt
    <Extension()> _
    Public Function FullNamespaceName(ByVal Source As [Namespace]) As String
        Dim Result As String = String.Empty
        If Source Is Nothing Then
            Return Result
        End If
        Dim NextNamespace = Source
        Do While NextNamespace IsNot Nothing
            Result = NextNamespace.Name & "." & Result
            NextNamespace = TryCast(NextNamespace.Parent, [Namespace])
        Loop
        Return Result.Substring(0, Result.Length - 1)
    End Function
End Module