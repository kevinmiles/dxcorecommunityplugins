Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices

Public Module LanguageElementExt
    <Extension()> _
    Public Function WhereNotNameStarts(Of T As IElement)(ByVal Source As IEnumerable(Of T), ByVal s As String) As IEnumerable(Of T)
        Return Source.Where(Function(l) Not l.Name.StartsWith(s))
    End Function
    <Extension()> _
    Public Function WhereNameStartsLower(Of T As IElement)(ByVal Source As IEnumerable(Of T)) As IEnumerable(Of T)
        Return Source.Where(Function(l) Char.IsLower(l.Name.First))
    End Function
    <Extension()> _
    Public Function WhereNameStartsUpper(Of T As IElement)(ByVal Source As IEnumerable(Of T)) As IEnumerable(Of T)
        Return Source.Where(Function(l) Char.IsUpper(l.Name.First))
    End Function
    <Extension()> _
    Public Function ToLE(ByVal Source As IElement) As LanguageElement
        Return TryCast(Source, LanguageElement)
    End Function
End Module