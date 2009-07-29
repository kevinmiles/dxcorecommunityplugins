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
        Dim mnuSmartPaste As New SmartTagItemEx("Smart Paste", AddressOf SmartPaste)
        Dim mnuCSharpToVBNet As New SmartTagItemEx("CSharp -> VBNet", AddressOf PasteCSharpAsVBNet)
        Dim mnuVBNetToCSharp As New SmartTagItemEx("VBNet -> CSharp", AddressOf PasteVBNetAsCSharp)
        ea.Add(mnuSmartPaste)
        ea.Add(mnuCSharpToVBNet)
        ea.Add(mnuVBNetToCSharp)
        'AddHandler mnuSmartPaste.Execute, AddressOf SmartPaste
        'AddHandler mnuCSharpToVBNet.Execute, AddressOf PasteCSharpAsVBNet
        'AddHandler mnuVBNetToCSharp.Execute, AddressOf PasteVBNetAsCSharp
    End Sub
    Private Sub SmartPaste()
        Call New GenericDXTranslator(CodeRush.Documents.ActiveLanguage).Paste()
    End Sub
    Private Sub PasteCSharpAsVBNet()
        Call New GenericDXTranslator("Basic", "CSharp").Paste()
    End Sub
    Private Sub PasteVBNetAsCSharp()
        Call New GenericDXTranslator("CSharp", "Basic").Paste()
    End Sub
End Class
Public Class SmartTagItemEx
    Inherits SmartTagItem
    Private mExecute As MethodInvoker
    Public Sub New(ByVal Caption As String, ByVal OnExecute As MethodInvoker)
        MyBase.Caption = Caption
        mExecute = OnExecute
    End Sub
    Private Sub SmartTagItemEx_Execute(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Execute
        mExecute.Invoke()
    End Sub
End Class
