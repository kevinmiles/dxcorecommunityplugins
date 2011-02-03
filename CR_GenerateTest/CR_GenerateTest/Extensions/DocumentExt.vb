Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices

Public Module DocumentExt
    <Extension()> _
    Public Function AllNamespaces(ByVal Document As TextDocument) As IEnumerable(Of [Namespace])
        Return (New ElementEnumerable(Document.FileNode, True)).OfType(Of [Namespace])()
    End Function
    <Extension()> _
    Public Function GetNamespace(ByVal Document As TextDocument, ByVal NamespaceName As String) As [Namespace]
        ' If namespace does not exist return nothing.
        ' If namespace does exist, then return lowest level namespace of chain.
        ' Get all namespaces. 
        ' Get Leaf namespaces.

        Return Document.AllNamespaces.Where(Function(ns) ns.FullNamespaceName = NamespaceName).FirstOrDefault
    End Function
End Module