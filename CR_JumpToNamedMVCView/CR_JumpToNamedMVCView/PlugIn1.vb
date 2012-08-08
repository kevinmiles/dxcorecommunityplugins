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
        Call CreateJumpToNamedMVCView()
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

    Public Sub CreateJumpToNamedMVCView()
        Dim JumpToNamedMVCView As New DevExpress.CodeRush.Library.NavigationProvider(components)
        CType(JumpToNamedMVCView, System.ComponentModel.ISupportInitialize).BeginInit()
        JumpToNamedMVCView.ProviderName = "JumpToNamedMVCView" ' Should be Unique
        JumpToNamedMVCView.DisplayName = "Named MVC View"
        AddHandler JumpToNamedMVCView.CheckAvailability, AddressOf NavToNamedMVCView_CheckAvailability
        AddHandler JumpToNamedMVCView.Apply, AddressOf NavToNamedMVCView_Apply
        CType(JumpToNamedMVCView, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub NavToNamedMVCView_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        Dim ViewDocument As String = GetNamedMVCViewDocumentPath(ea.CodeActive)
        ea.Available = ViewDocument IsNot String.Empty
    End Sub
    Function GetNamedMVCViewDocumentPath(ByVal Element As LanguageElement) As String
        ' Caret should be on View Or PartialView
        Dim ParentClass = Element.GetParent(LanguageElementType.Class)
        If ParentClass Is Nothing Then
            Return Nothing
        End If
        If Not ParentClass.Name.EndsWith("Controller") Then
            Return Nothing
        End If
        If Element.ElementType <> LanguageElementType.MethodReferenceExpression Then
            Return Nothing
        End If
        Dim MRE = TryCast(Element, MethodReferenceExpression)
        If MRE.Name <> "View" AndAlso MRE.Name <> "PartialView" Then
            Return Nothing
        End If
        Dim MethodCallExpression As LanguageElement = MRE.Parent
        If MethodCallExpression.DetailNodes.Count = 0 Then
            Return Nothing
        End If
        Dim FirstParam As PrimitiveExpression = TryCast(MethodCallExpression.DetailNodes(0), PrimitiveExpression)
        If FirstParam.PrimitiveType <> PrimitiveType.String Then
            Return Nothing
        End If
        Dim ViewName = FirstParam.Name.Trim("""")
        Dim ThisDocPath As String = CodeRush.Documents.ActiveTextDocument.Path.RemoveLast("Controllers\".Length)
        Dim RelativePath = String.Format("Views\{0}\{1}", ParentClass.Name.RemoveLast("Controller".Length), FirstParam.PrimitiveValue)
        Dim HtmlFileType As String = If(CodeRush.Language.Active = "Basic", ".vbhtml", ".cshtml")
        Dim ViewPath As String = System.IO.Path.Combine(ThisDocPath, RelativePath) & HtmlFileType
        Return ViewPath
    End Function

    Private Sub NavToNamedMVCView_Apply(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        ' This method is executed when the system executes your NavigationProvider 
        Dim ViewDocument As String = GetNamedMVCViewDocumentPath(ea.CodeActive)
        If ViewDocument <> String.Empty Then
            CodeRush.Markers.Drop()
            CodeRush.File.Activate(ViewDocument)
        End If
    End Sub

End Class
Public Module ExtensionString
    <Extension()> _
    Function RemoveLast(Source As String, ByVal CharCount As Integer) As String
        Return Source.Substring(0, Source.Length - (CharCount))
    End Function
End Module