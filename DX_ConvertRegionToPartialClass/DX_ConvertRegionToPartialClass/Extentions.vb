Option Strict On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Imports DevExpress.Refactor
Imports System.Runtime.CompilerServices
Imports DevExpress.Refactor.Core
Imports DevExpress.CodeRush.Interop.OLE.Helpers

Public Module Extentions
    <Extension()> _
    Public Function InnerRange(ByVal Source As RegionDirective) As SourceRange
        Return New SourceRange(Source.StartLine + 1, 1, Source.EndLine, 1)
    End Function
    <Extension()> _
    Public Function OnlyAlphaNumerics(ByVal Source As String) As String
        Dim ValidChars As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890"
        Dim Result = String.Empty
        For Each Character In Source
            If ValidChars.Contains(Character) Then
                Result &= Character
            End If
        Next
        Return Result
    End Function

End Module