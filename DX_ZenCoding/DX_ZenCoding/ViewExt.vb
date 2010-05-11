Imports System.Linq
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices

Public Module ViewExt
    ''' <summary>Treat Source as a Template name and expand it into the active view</summary>
    <Extension()> _
    Public Function Expand(ByVal Source As Template) As SourceRange
        Dim View = CodeRush.TextViews.Active
        Return View.TextDocument.ExpandText(View.Caret.SourcePoint, Source.FirstItemInContext.Expansion)
    End Function
End Module
