Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Core
Imports System.Windows.Forms
Imports System.Runtime.CompilerServices
Imports DevExpress.CodeRush.PlugInCore
Imports System.Linq
Imports System.Linq.Expressions

Module AssemblyReferenceExt
    <Extension()> _
    Public Function Key(ByVal Reference As AssemblyReference) As String
        Return Reference.FilePath
    End Function
End Module
