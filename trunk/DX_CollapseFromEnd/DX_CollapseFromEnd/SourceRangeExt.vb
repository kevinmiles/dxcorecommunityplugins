'Option Strict On
'Option Explicit On
'Option Infer On
'Imports System.ComponentModel
'Imports System.Drawing
'Imports System.Windows.Forms
'Imports DevExpress.CodeRush.Core
'Imports DevExpress.CodeRush.PlugInCore
'Imports DevExpress.CodeRush.StructuralParser
'Imports System.Runtime.CompilerServices

'Module SourceRangeExt
'    <Extension()> _
'    Public Function Overlaps(ByVal Source As SourceRange, ByVal Dest As SourceRange) As Boolean
'        Return Source.Intersects(Dest) OrElse Source.Contains(Dest) OrElse Dest.Contains(Source)
'    End Function
'    <Extension()> _
'    Public Function OnScreen(ByVal Source As LanguageElement) As Boolean
'        Dim View = CodeRush.Documents.ActiveTextView
'        Dim ViewRange As SourceRange = View.GetRangeFromRectangle(View.Bounds)
'        Return ViewRange.Overlaps(Source.Range)
'    End Function

'    ''' <summary>
'    ''' Method to paint a limited width rectangle over a member.
'    ''' </summary>
'    <Extension()> _
'    Public Sub FillRectangle(ByVal View As TextView, ByVal LanguageElement As LanguageElement, ByVal Color As Color, Optional ByVal MaxWidth As Integer = 0)
'        Dim Rect As Rectangle = View.GetRectangleFromRange(LanguageElement.GetFullBlockRange)
'        If MaxWidth > 0 Then
'            Rect.Width = 50
'        End If
'        View.Graphics.FillRectangle(New SolidBrush(Color), Rect)
'    End Sub
'End Module