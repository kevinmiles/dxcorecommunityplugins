Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.Refactor

Public Class PlugIn1

	'DXCore-generated code...
#Region " InitializePlugIn "
	Public Overrides Sub InitializePlugIn()
		MyBase.InitializePlugIn()
        CreateRemoveRegions()
    End Sub
#End Region
#Region " FinalizePlugIn "
	Public Overrides Sub FinalizePlugIn()
		'TODO: Add your finalization code here.

		MyBase.FinalizePlugIn()
	End Sub
#End Region

    Public Sub CreateRemoveRegions()
        Dim RemoveRegions As New DevExpress.Refactor.Core.RefactoringProvider(components)
        CType(RemoveRegions, System.ComponentModel.ISupportInitialize).BeginInit()
        RemoveRegions.ProviderName = "RemoveFileRegions" ' Should be Unique
        RemoveRegions.DisplayName = "Remove File Regions"
        AddHandler RemoveRegions.CheckAvailability, AddressOf RemoveRegions_CheckAvailability
        AddHandler RemoveRegions.Apply, AddressOf RemoveRegions_Execute
        CType(RemoveRegions, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub RemoveRegions_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        ea.Available = OnARegionDirective(ea)
    End Sub
    Private Function OnARegionDirective(ByVal ea As CheckContentAvailabilityEventArgs) As Boolean
        Dim CaretLine As String = ea.TextDocument.GetLine(ea.Caret.Line).Trim.ToLower
        Return CaretLine.StartsWith("#region") OrElse CaretLine.StartsWith("#end region")
    End Function
    Private Sub RemoveRegions_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        Dim FileNode As SourceFile = If(ea.CodeActive.ElementType = LanguageElementType.SourceFile, ea.CodeActive, ea.CodeActive.FileNode)
        Dim RegionsInfile = FileNode.Regions
        For Each MyRegion As RegionDirective In RegionsInfile
            ea.TextDocument.QueueDelete(WholeLine(MyRegion.Range.Start.Line))
            ea.TextDocument.QueueDelete(WholeLine(MyRegion.Range.End.Line))
        Next
        ea.TextDocument.ApplyQueuedEdits("Regions Removed")
    End Sub
    Private Function WholeLine(ByVal Line As Integer) As SourceRange
        Return New SourceRange(Line, 1, Line + 1, 0)
    End Function

End Class
