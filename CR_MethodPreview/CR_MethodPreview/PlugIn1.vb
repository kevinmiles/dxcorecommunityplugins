Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.UserControls

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

#Region "Fields"
    Private mPreviewWindow As CodePreviewWindow
    Private mShowingPreview As Boolean = False
#End Region
#Region "Utils"
    Private Function FirstXLines(ByVal Code As String, ByVal LineCount As Integer) As String
        ' Count newlines
        Dim Lines As String() = Split(Code, System.Environment.NewLine)
        Dim XLines As String() = Lines.Take(LineCount).ToArray
        Return Join(XLines, System.Environment.NewLine)
    End Function
    Private Function GetMethod(ByVal Element As LanguageElement) As Method
        Dim Method As Method = Nothing
        Select Case Element.ElementType
            Case LanguageElementType.MethodCall
                Method = TryCast(Element, MethodCall).GetDeclaration
            Case LanguageElementType.MethodReferenceExpression
                Method = TryCast(Element, MethodReferenceExpression).GetDeclaration
        End Select
        Return Method
    End Function
#End Region
#Region "Show and hide Methods "
    Private Sub ShowMethodPreview(ByVal Method As Method, ByVal SourcePoint As SourcePoint)
        Dim TheCode As String = CodeRush.CodeMod.GenerateCode(Method)
        Call ShowCodePreview(TheCode, SourcePoint)
    End Sub
    Private Sub ShowCodePreview(ByVal Code As String, ByVal insertionPoint As SourcePoint)
        mPreviewWindow = New CodePreviewWindow(CodeRush.Documents.ActiveTextView, insertionPoint)
        'Code = FirstXLines(Code, 20)
        mPreviewWindow.AddCode(Code)
        mPreviewWindow.ShowPreview()
    End Sub
    Private Sub HideCodePreview()
        mPreviewWindow.HidePreview()
        mShowingPreview = False
    End Sub
#End Region
    Private Sub ToggleMethodPreview_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles ToggleMethodPreview.Execute
        If mShowingPreview Then
            Call HideCodePreview()
        Else
            Dim Method As Method = GetMethod(CodeRush.Source.GetNodeAt(CodeRush.Caret.SourcePoint))
            If Not Method Is Nothing Then
                Call ShowMethodPreview(Method, CodeRush.Caret.SourcePoint)
                mShowingPreview = True
            End If
        End If
    End Sub
End Class
