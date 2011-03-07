Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices

Public Class PlugIn1

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        CreateCloseAllButThisAndTestDocument()
        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region
    Public Sub CreateCloseAllButThisAndTestDocument()
        Dim CloseAllButThisAndTestDocument As New DevExpress.CodeRush.Core.Action(components)
        CType(CloseAllButThisAndTestDocument, System.ComponentModel.ISupportInitialize).BeginInit()
        CloseAllButThisAndTestDocument.ActionName = "CloseAllButThisAndTestDocument"
        CloseAllButThisAndTestDocument.RegisterInCR = True
        AddHandler CloseAllButThisAndTestDocument.Execute, AddressOf CloseAllButThisAndTestDocument_Execute
        CType(CloseAllButThisAndTestDocument, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub CloseAllButThisAndTestDocument_Execute(ByVal ea As ExecuteEventArgs)
        Dim ActiveClass = CodeRush.Source.ActiveClass
        If ActiveClass Is Nothing Then
            Exit Sub
        End If
        Dim ActiveDoc As TextDocument = CodeRush.Documents.ActiveTextDocument
        Dim DocsToClose As New List(Of TextDocument)
        For Each TextDocument As TextDocument In CodeRush.Documents.AllSolutionTextDocuments
            If TextDocument.Name.Substring(0, TextDocument.Name.Length - 3).In(ActiveClass.Name, ActiveClass.Name & "_Tests") Then
                Continue For
            End If
            DocsToClose.Add(TextDocument)
        Next
        For Each Doc In DocsToClose
            Doc.Close()
        Next
    End Sub
End Class
Public Module SomeTypeExt
    <Extension()> _
    Public Function [In](ByVal Source As String, ByVal ParamArray Values() As String) As Boolean
        Return Values.Contains(Source)
    End Function
End Module