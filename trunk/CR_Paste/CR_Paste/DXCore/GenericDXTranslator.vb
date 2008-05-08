Imports System.Windows.Forms
Public Class GenericDXTranslator
    Implements IPaster
    Protected Loader As IDXLoader
    Protected Renderer As IDXRenderer
    Public Sub New(ByVal RendererLanguageID As String, ByVal LoaderLanguageID As String)
        Me.Loader = New GenericDXLoader(LoaderLanguageID)
        Me.Renderer = New GenericDXRenderer(RendererLanguageID)
    End Sub
    Public Sub Paste() Implements IPaster.Paste
        If Not Clipboard.ContainsText() Then
            Exit Sub
        End If
        ' Assume Text in Clipboard is CSharp
        ' Load Text as CSharp into A ParseTree
        Call Loader.Load(Clipboard.GetText())
        ' Pass ParseTree Through PreProcessor
        ' Render ParseTree as VBNet
        Call Renderer.RenderToEditor(Loader.TreeRoot)
    End Sub
End Class
