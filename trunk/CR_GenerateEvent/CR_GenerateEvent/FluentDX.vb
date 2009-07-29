Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices

Public Module FluentDX
    <Extension()> _
    Public Function AsAssignment(ByVal LE As LanguageElement) As Assignment
        Return TryCast(LE, Assignment)
    End Function

    <Extension()> _
    Public Function AsIEventElement(ByVal LE As IElement) As IEventElement
        Return TryCast(LE, IEventElement)
    End Function
    <Extension()> _
    Public Function GenerateCode(ByVal LE As LanguageElement, Optional ByVal SurroundWithCRLF As Boolean = False) As String
        Return String.Format("{0}{1}{0}", _
                             If(SurroundWithCRLF, System.Environment.NewLine, String.Empty), _
                             CodeRush.CodeMod.GenerateCode(LE))
    End Function
End Module