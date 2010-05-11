Imports System.Linq
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices

Public Module StringExt
    <Extension()> _
    Public Function Contents(ByVal Source As String, ByVal StartChar As String, ByVal EndChar As String) As String
        Dim StartPoint As Integer = Source.IndexOf(StartChar) + 1
        Dim EndPoint As Integer = Source.IndexOf(EndChar) - 1
        Return Source.Substring(StartPoint, EndPoint - StartPoint)
    End Function
End Module