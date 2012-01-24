Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Public Class PlugIn1

	'DXCore-generated code...
#Region " InitializePlugIn "
	Public Overrides Sub InitializePlugIn()
		MyBase.InitializePlugIn()
        Call CreateRegionTabber()
    End Sub
#End Region
#Region " FinalizePlugIn "
	Public Overrides Sub FinalizePlugIn()
		'TODO: Add your finalization code here.

		MyBase.FinalizePlugIn()
	End Sub
#End Region

    Private Sub CreateRegionTabber()
        Dim RegionTabber = New DevExpress.CodeRush.Core.SearchProvider()
        CType(RegionTabber, System.ComponentModel.ISupportInitialize).BeginInit()
        RegionTabber.Description = "Region Tabber"
        RegionTabber.ProviderName = "Region Tabber" ' Needs to be Unique
        RegionTabber.Register = True
        RegionTabber.UseForNavigation = True
        AddHandler RegionTabber.CheckAvailability, AddressOf RegionTabber_CheckAvailability
        AddHandler RegionTabber.SearchReferences, AddressOf RegionTabber_SearchReferences
        CType(RegionTabber, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub RegionTabber_CheckAvailability(ByVal sender As Object, ByVal ea As CheckSearchAvailabilityEventArgs)
        Dim Region As RegionDirective = Nothing

        Dim RegionPainterPluginExists As Boolean = ActionExists("ToggleRegionPainting")
        ea.Available = (Not RegionPainterPluginExists OrElse Not isRegionPaintingEnabled()) _
            AndAlso IsOnOpenRegionStartOrEnd(Region)
    End Sub

    Private Function isRegionPaintingEnabled() As Boolean
        Return CodeRush.Options.GetStorage("Editor\Painting", "Regions").ReadBoolean("Preferences", "Enabled", False)

    End Function
    Private Function ActionExists(ByVal ActionName As String) As Boolean
        For Each Action In CodeRush.Actions
            If Action.ActionName = ActionName Then
                Return True
            End If
        Next
        Return False
    End Function
    Private Function IsOnOpenRegionStartOrEnd(ByRef Region As RegionDirective) As Boolean
        Region = GetRegionAtCaret()
        If Region Is Nothing Then
            Return False
        End If
        If Region.Collapsed Then
            Region = Nothing
            Return False
        End If
        Return True
    End Function
    Private Shared Function GetRegionAtCaret() As RegionDirective
        Dim ActiveDoc As TextDocument = CodeRush.Documents.ActiveTextDocument
        Dim File = TryCast(ActiveDoc.FileNode, SourceFile)
        Dim FileRegions = File.RegionRootNode.Nodes.OfType(Of RegionDirective)()
        Return FileRegions.Where(Function(r) r.CollapsibleRange.Start.Line = CodeRush.Caret.Line _
                                     OrElse r.CollapsibleRange.End.Line = CodeRush.Caret.Line).FirstOrDefault()
    End Function
    Private Sub RegionTabber_SearchReferences(Sender As Object, ByVal ea As DevExpress.CodeRush.Core.SearchEventArgs)
        Dim FoundRegion As RegionDirective = Nothing
        If Not IsOnOpenRegionStartOrEnd(FoundRegion) Then
            Exit Sub
        End If

        ea.AddRange(FoundRegion.FileNode, GetSourceRangeOfRegionStart(FoundRegion))
        ea.AddRange(FoundRegion.FileNode, GetSourceRangeOfRegionEnd(FoundRegion))
    End Sub
    Private Function GetSourceRangeOfRegionStart(Region As RegionDirective) As SourceRange
        Return GetRangeOftextOnLine(region.StartLine)
    End Function
    Private Function GetSourceRangeOfRegionEnd(ByVal Region As RegionDirective) As SourceRange
        Return GetRangeOftextOnLine(Region.EndLine)
    End Function
    Private Function GetRangeOfTextOnLine(ByVal Line As Integer) As SourceRange
        Dim LineText As String = CodeRush.Documents.ActiveTextDocument.GetLine(Line)
        Dim LengthOfWhiteSpace As Integer = LineText.IndexOf(LineText.TrimStart.Substring(0, 1))
        Dim LengthOfTextOnLine As Integer = LineText.Trim.Length
        Return New SourceRange(Line, 1 + LengthOfWhiteSpace, Line, 1 + LengthOfWhiteSpace + LengthOfTextOnLine)
    End Function
End Class
