Option Strict On
Option Explicit On
Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.Refactor.Core
Imports System.IO
Imports DevExpress.CodeRush.Interop.OLE.Helpers
Imports DevExpress.DXCore.TextBuffers
Imports System.Runtime.CompilerServices

Public Module FluentDX
    <Extension()> _
    Public Function AsAssignment(ByVal Element As LanguageElement) As Assignment
        Return TryCast(Element, Assignment)
    End Function

    <Extension()> _
    Public Function AsIEventElement(ByVal Element As IElement) As IEventElement
        Return TryCast(Element, IEventElement)
    End Function
    <Extension()> _
        Public Function GenerateCode(ByVal Source As LanguageElement, ByVal Language As String) As String
        Return CodeRush.Language.GenerateElement(Source, Language)

    End Function
End Module
