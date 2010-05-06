Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices
Imports CR = DevExpress.CodeRush.Core.CodeRush


Public Class PlugIn1

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()

        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

    Private Sub actCustomCollapse_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles actCustomCollapse.Execute
        Dim InclusionStrings = CStr(ea.VarIn).Split(","c).Select(Function(s) Normalise(s)).ToList
        Dim Exclusions As LanguageElementType() = {LanguageElementType.Namespace, LanguageElementType.Class, LanguageElementType.Struct, LanguageElementType.Region}

        For Each Node In GetElements(CR.Source.ActiveFileNode, _
                                     Function(e) e.IsCollapsible _
                                     AndAlso InclusionStrings.Contains(Normalise(e.ElementType.ToString)))

            Node.Collapse()
        Next
    End Sub
    Private Function Normalise(ByVal S As String) As String
        Return S.LastPart("."c).Trim.ToLower
    End Function
End Class

Public Module GeneralExt
    <Extension()> _
    Public Function [In](ByVal Source As LanguageElementType, ByVal List As IEnumerable(Of LanguageElementType)) As Boolean
        Return List.Any(Function(e) e = Source)
    End Function
    <Extension()> _
    Public Function LastPart(ByVal Source As String, ByVal PartSpecifier As String) As String
        Return Source.Substring(Source.LastIndexOf(PartSpecifier) + 1)
    End Function
End Module