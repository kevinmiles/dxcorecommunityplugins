Option Strict On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Imports DevExpress.Refactor
Imports System.Runtime.CompilerServices
Imports DevExpress.Refactor.Core
Imports DevExpress.CodeRush.Interop.OLE.Helpers

Public Class PlugIn1

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        CreateConvertRegionToPartialClass()
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

    Public Sub CreateConvertRegionToPartialClass()
        Dim ConvertRegionToPartialClass As New DevExpress.Refactor.Core.RefactoringProvider(components)
        CType(ConvertRegionToPartialClass, System.ComponentModel.ISupportInitialize).BeginInit()
        ConvertRegionToPartialClass.ProviderName = "ConvertRegionToPartialClass" ' Should be Unique
        ConvertRegionToPartialClass.DisplayName = "Convert Region to Partial Class"
        AddHandler ConvertRegionToPartialClass.CheckAvailability, AddressOf ConvertRegionToPartialClass_CheckAvailability
        AddHandler ConvertRegionToPartialClass.Apply, AddressOf ConvertRegionToPartialClass_Execute
        CType(ConvertRegionToPartialClass, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub ConvertRegionToPartialClass_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        ' Require Region
        Dim CaretRegion As RegionDirective = RegionAtCaret(TryCast(CodeRush.Documents.ActiveTextDocument.FileNode, SourceFile))
        If CaretRegion Is Nothing Then
            Exit Sub
        End If
        ' Region is directly inside Class
        If Not ContainedByAClass(CaretRegion) Then
            Exit Sub
        End If
        ea.Available = True
    End Sub

    Private Sub ConvertRegionToPartialClass_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        CodeRush.Markers.Drop()
        ' Gather references
        Dim ActiveDoc As TextDocument = CodeRush.Documents.ActiveTextDocument
        Dim SourceClass = TryCast(CodeRush.Source.Active, [Class])

        Dim Region = RegionAtCaret(TryCast(ActiveDoc.FileNode, SourceFile))
        Dim FilePathAndName As String = GetFilePathAndName(ActiveDoc, SourceClass.Name, Region)
        EnsurePartialClassFileExists(CodeRush.Source.ActiveProject, FilePathAndName, SourceClass)

        Dim PartialDoc = TryCast(CodeRush.File.Activate(FilePathAndName), TextDocument)
        Dim PartialClass = LocateClassInFile(TryCast(PartialDoc.FileNode, SourceFile), SourceClass.Name) ' Locate Class Called ClassName in File Filename
        Dim MovingText = ActiveDoc.GetText(Region.InnerRange)
        PartialDoc.InsertText(PartialClass.BlockEnd.Start, MovingText)
        ActiveDoc.DeleteText(Region.GetFullBlockCutRange)
    End Sub

    Private Function ContainedByAClass(ByVal CaretRegion As RegionDirective) As Boolean
        Return GetClassesInSource(CaretRegion.FileNode).Any(Function(c) CaretRegion.ContainedIn(c.Range))
    End Function

    Private Shared Function GetFilePathAndName(ByVal ActiveDoc As TextDocument, ByVal ClassName As String, ByVal Region As RegionDirective) As String
        Dim PlugIn1 As New PlugIn1()
        Dim Folder = New System.IO.FileInfo(ActiveDoc.FileNode.Name).DirectoryName & "\"
        Return Folder & PlugIn1.GetPartialFilename(ClassName, Region.Name.OnlyAlphaNumerics())
    End Function

    Private Shared Function RegionAtCaret(ByVal File As SourceFile) As RegionDirective
        Dim FileRegions = File.RegionRootNode.Nodes.OfType(Of RegionDirective)()
        Return FileRegions.Where(Function(r) r.CollapsibleRange.Start.Line = CodeRush.Caret.Line).FirstOrDefault()
    End Function

    Private Sub CreateFileWithRootElement(ByVal FilePathAndName As String, ByVal Element As LanguageElement)
        Dim Code = CodeRush.CodeMod.GenerateCode(Element)
        My.Computer.FileSystem.WriteAllText(FilePathAndName, Code, False)
        CodeRush.UndoStack.Add(New CreatedFileUndoUnit(FilePathAndName, Code))
    End Sub

    Private Sub EnsurePartialClassFileExists(ByVal Project As ProjectElement, ByVal FilePathAndName As String, ByVal SourceClass As [Class])
        Dim PlugIn1 As New PlugIn1()
        If Project.AllFiles.Contains(FilePathAndName) Then
            Return
        End If
        PlugIn1.DeleteFileIfNeeded(FilePathAndName)
        Dim SourceFile As New SourceFile
        SourceFile.AddNodes(PlugIn1.CloneNamespaceReferences(SourceClass))
        SourceFile.AddNode(CloneHierarchy(SourceClass))
        Call CreateFileWithRootElement(FilePathAndName, SourceFile)
        CodeRush.Solution.AddFileToProject(Project.Name, FilePathAndName)
        CodeRush.UndoStack.Add(New AddedProjectFileUndoUnit(Project.Name, FilePathAndName))
    End Sub
    Private Sub DeleteFileIfNeeded(ByVal FilePathAndName As String)
        If System.IO.File.Exists(FilePathAndName) Then
            Dim DeletedFileText = System.IO.File.ReadAllText(FilePathAndName)
            System.IO.File.Delete(FilePathAndName)
            CodeRush.UndoStack.Add(New DeletedFileUndoUnit(FilePathAndName, DeletedFileText))
        End If
    End Sub
    Private Function CloneNamespaceReferences(ByVal SourceClass As [Class]) As LanguageElementCollectionBase
        Dim SourceFile As SourceFile = SourceClass.GetSourceFile
        Dim NamespaceReferences = SourceFile.Nodes.OfType(Of NamespaceReference)()
        Dim Result As New LanguageElementCollectionBase
        For Each NSR As NamespaceReference In NamespaceReferences
            Result.Add(New NamespaceReference(NSR.Name))
        Next
        Return Result
    End Function

    Private Function GetClassesInSource(ByVal Scope As LanguageElement) As IEnumerable(Of SP.Class)
        Return New ElementEnumerable(Scope, New ElementTypeFilter(GetType(SP.Class)), True).OfType(Of SP.Class)()
        'Return SourceModelUtils.GetElementEnumerator(Scope, New ElementTypeFilter(GetType(SP.Class))).OfType(Of SP.Class)()
    End Function
    Private Function LocateClassInFile(ByVal File As SourceFile, ByVal ClassName As String) As [Class]
        Return GetClassesInSource(File).Where(Function(c) c.Name = ClassName).FirstOrDefault
    End Function

    Private Function CloneHierarchy(ByVal SourceClass As [Class]) As LanguageElement
        Dim NewClass = New [Class](SourceClass.Name) With {.IsPartial = True}
        Dim ReadPoint As LanguageElement = SourceClass
        Dim WritePoint As LanguageElement = NewClass
        Do Until TryCast(ReadPoint.Parent, [Namespace]) Is Nothing
            Dim ThisNamespace = New [Namespace](ReadPoint.Parent.Name)
            ThisNamespace.AddNode(WritePoint)
            WritePoint = ThisNamespace
            ReadPoint = ReadPoint.Parent
        Loop
        Return WritePoint
    End Function

    Private Function GetPartialFilename(ByVal ClassName As String, ByVal Suffix As String) As String
        Return String.Format("{0}_{1}", ClassName, Suffix) & CodeRush.Language.SupportedFileExtensions
    End Function
End Class