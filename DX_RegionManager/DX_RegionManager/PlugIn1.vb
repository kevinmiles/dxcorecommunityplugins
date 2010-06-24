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
        CreateExpandCurrentDocumentRegions()
        CreateOpenAllRegionsInOpenFiles()
        AddHandler EventNexus.DocumentOpened, AddressOf OpenActiveDocumentRegions
    End Sub
    Private Sub OpenActiveDocumentRegions(ByVal ea As DocumentEventArgs)
        ExpandDocumentRegions(ea.Document)
    End Sub

#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        RemoveHandler Me.DocumentOpened, AddressOf OpenActiveDocumentRegions
        MyBase.FinalizePlugIn()
    End Sub
#End Region
    Private Sub ExpandCurrentDocumentRegions_Execute(ByVal ea As ExecuteEventArgs)
        Call ExpandDocumentRegions(CodeRush.Documents.ActiveTextDocument)
    End Sub

#Region "Initialise Actions"
    Public Sub CreateExpandCurrentDocumentRegions()
        Dim ExpandCurrentDocumentRegions As New DevExpress.CodeRush.Core.Action(components)
        CType(ExpandCurrentDocumentRegions, System.ComponentModel.ISupportInitialize).BeginInit()
        ExpandCurrentDocumentRegions.ActionName = "ExpandCurrentDocumentRegions"
        ExpandCurrentDocumentRegions.RegisterInCR = True
        AddHandler ExpandCurrentDocumentRegions.Execute, AddressOf ExpandCurrentDocumentRegions_Execute
        CType(ExpandCurrentDocumentRegions, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Public Sub CreateOpenAllRegionsInOpenFiles()
        Dim OpenAllRegionsInOpenFiles As New DevExpress.CodeRush.Core.Action(components)
        CType(OpenAllRegionsInOpenFiles, System.ComponentModel.ISupportInitialize).BeginInit()
        OpenAllRegionsInOpenFiles.ActionName = "ExpandAllRegionsInOpenFiles"
        OpenAllRegionsInOpenFiles.RegisterInCR = True
        AddHandler OpenAllRegionsInOpenFiles.Execute, AddressOf OpenAllRegionsInOpenFiles_Execute
        CType(OpenAllRegionsInOpenFiles, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
#End Region


    Private Sub OpenAllRegionsInOpenFiles_Execute(ByVal ea As ExecuteEventArgs)
        For Each Doc In CodeRush.Documents.AllProjectTextDocuments()
            Call ExpandDocumentRegions(Doc)
        Next
    End Sub

    Private Sub ExpandDocumentRegions(ByVal Document As TextDocument)
        For Each Region As RegionDirective In Document.Regions()
            Call Region.ExpandInView(Document.ActiveView, False)
        Next
    End Sub
End Class
