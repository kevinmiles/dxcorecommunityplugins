Imports System.Linq
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices

Public Module UnTestable
    ' Typically because they rely directly on DXCore 
#Region "TemplateOps"
    ''' <summary>Locates First Template with given Name</summary>
    Public Function GetFirstTemplateWithName(ByVal Part As String) As Template
        Dim TemplateList = CodeRush.Templates.Find(Part, False)
        If TemplateList.Count = 0 Then
            Return Nothing
        End If
        Return TemplateList(0)
    End Function
    Public Sub SetTemplateVariable(ByVal VarName As String, ByVal VarValue As String)
        DevExpress.CodeRush.Core.CodeRush.Strings.Get("Set", String.Format("{0},{1}", VarName, VarValue))
    End Sub
#End Region
#Region "SourcePointOps"
    Public Function RestorePoint(ByVal SavedPoint As SourcePoint) As SourcePoint
        CodeRush.Caret.MoveTo(SavedPoint)
        Return SavedPoint
    End Function
    Public Function SavePoint(ByVal Point As SourcePoint, ByVal Document As TextDocument) As SourcePoint
        Dim NewPoint As SourcePoint = New SourcePoint(Point)
        NewPoint.BindToCode(Document, True)
        Return NewPoint
    End Function
#End Region
End Module