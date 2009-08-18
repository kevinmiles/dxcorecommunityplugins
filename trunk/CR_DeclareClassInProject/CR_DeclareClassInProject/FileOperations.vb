Imports System.IO
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser

Public Module FileOperations
    Public Sub CreateEmptyFileWithUndo(ByVal FileAndPath As String)
        If File.Exists(FileAndPath) Then
            Call DeleteFile(FileAndPath)
        End If
        ' Create new file 
        Call File.WriteAllText(FileAndPath, "")
        CodeRush.UndoStack.Add(New CreatedFileUndoUnit(FileAndPath, ""))
    End Sub
    Public Sub InsertTextWithUndo(ByVal Code As String)
        CodeRush.Documents.ActiveTextDocument.QueueInsert(New SourcePoint(1, 1), Code)
        CodeRush.Documents.ActiveTextDocument.ApplyQueuedEdits("Insert Text")
    End Sub
    Public Sub InsertElementWithUndo(ByVal Element As LanguageElement, ByVal Language As String)
        Call InsertTextWithUndo(CodeRush.Language.GenerateElement(Element, Language))
    End Sub
    Public Sub DeleteFile(ByVal FileAndPath As String)
        If File.Exists(FileAndPath) Then
            CodeRush.UndoStack.Add(New CreatedFileUndoUnit(FileAndPath, IO.File.ReadAllText(FileAndPath)))
            Call File.Delete(FileAndPath)
        End If
    End Sub
    Public Sub ParseFile(ByVal FileAndPath As String)
        Call CodeRush.Language.Parse(FileAndPath)
    End Sub
    Public Sub JumpToFileWithUndo(ByVal FileAndPath As String)
        Dim OriginalFilename As String = CodeRush.File.Active
        Call CodeRush.File.Activate(FileAndPath)
        Call CodeRush.UndoStack.Add(New FileActivateUndoUnit(OriginalFilename))
    End Sub

End Module
