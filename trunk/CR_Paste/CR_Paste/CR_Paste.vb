Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.Refactor.Core

Public Class CR_Paste

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

    Private Sub PasteSmartTag_GetSmartTagItems(ByVal sender As Object, ByVal ea As GetSmartTagItemsEventArgs) Handles PasteSmartTag.GetSmartTagItems
        Dim mnuSmartPaste As New SmartTagItem("Smart Paste")
        Dim mnuCSharpToVBNet As New SmartTagItem("CSharp -> VBNet")
        Dim mnuVBNetToCSharp As New SmartTagItem("VBNet -> CSharp")
        ea.Add(mnuSmartPaste)
        ea.Add(mnuCSharpToVBNet)
        ea.Add(mnuVBNetToCSharp)
        AddHandler mnuSmartPaste.Execute, AddressOf PasteCSharpAsVBNet
        AddHandler mnuCSharpToVBNet.Execute, AddressOf PasteCSharpAsVBNet
        AddHandler mnuVBNetToCSharp.Execute, AddressOf PasteVBNetAsCSharp
    End Sub
    Private Sub SmartPaste(ByVal sender As Object, ByVal e As EventArgs)
        Call New GenericDXTranslator(CodeRush.Documents.ActiveLanguage).Paste()
    End Sub
    Private Sub PasteCSharpAsVBNet(ByVal sender As Object, ByVal e As EventArgs)
        Call New GenericDXTranslator("Basic", "CSharp").Paste()
    End Sub
    Private Sub PasteVBNetAsCSharp(ByVal sender As Object, ByVal e As EventArgs)
        Call New GenericDXTranslator("CSharp", "Basic").Paste()
    End Sub
End Class

