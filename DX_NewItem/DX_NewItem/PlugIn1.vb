Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports EnvDTE80
Imports System.IO
Imports System.Runtime.CompilerServices
Public Class PlugIn1

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        Call CreateNewItem()
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region
    Public Sub CreateNewItem()
        Dim NewItem As New DevExpress.CodeRush.Core.TextCommand(components)
        CType(NewItem, System.ComponentModel.ISupportInitialize).BeginInit()
        NewItem.CommandName = "NewItem"
        AddHandler NewItem.Execute, AddressOf NewItem_Execute
        CType(NewItem, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub NewItem_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteTextCommandEventArgs)
        Dim TemplateName As String
        Dim TemplatePath As String
        Dim FilenamePattern As String
        Try
            FilenamePattern = ea.GetParameterValue(0).Split("|"c)(1)
        Catch ex As Exception
            FilenamePattern = String.Empty
        End Try
        'Try
        Dim Param As String = String.Format("{0}/{1}", CodeRush.Documents.ActiveLanguageVS, _
                                            ea.GetParameterValue(0).Split("|"c)(0))
        Dim Pos = Param.LastIndexOf("/"c)
        TemplateName = Param.Substring(Pos + 1)
        TemplatePath = Param.Substring(0, Pos)
        'Catch ex As Exception
        '    Exit Sub
        'End Try

        If FilenamePattern = String.Empty Then
            FilenamePattern = TemplateName.GetLastPart("/"c) & CodeRush.Language.ActiveExtension.SupportedFileExtensions
        End If

        Dim RootFolder = New FileInfo(TryCast(CodeRush.Source.ActiveFileNode, SourceFile).FilePath).Directory
        Dim FileName As String = GetUniqueFileName(RootFolder, FilenamePattern)
        Dim TemplateFullName As String = GetVSTemplatePath(TemplateName & ".zip", TemplatePath)
        CodeRush.ProjectItems.Active.ProjectItems.AddFromTemplate(TemplateFullName, FileName)
    End Sub


End Class
Public Module StringExt
    <Extension()> _
    Public Function GetLastPart(ByVal Source As String, ByVal Seperator As String) As String
        Dim Pos As Integer = Source.LastIndexOf(Seperator)
        'If Pos = -1  Then
        '    Return Source
        'End If
        Return Source.Substring(Pos+1)
    End Function
End Module