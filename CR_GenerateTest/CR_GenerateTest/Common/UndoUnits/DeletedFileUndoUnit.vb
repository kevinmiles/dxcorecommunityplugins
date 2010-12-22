Imports System
Imports System.IO
Imports DevExpress.CodeRush.Interop.OLE.Helpers

Public Class DeletedFileUndoUnit
    Inherits UndoUnit

    Private mFilePath As String
    Private mFileText As String

    Public Sub New(ByVal filePath As String, ByVal fileText As String)
        mFilePath = filePath
        mFileText = fileText
    End Sub

    Protected Overrides Sub Execute() ' run the undo operation, which rewrites the file.
        If (Not File.Exists(mFilePath)) Then
            Using writer As New StreamWriter(mFilePath)
                writer.Write(mFileText)
                writer.Flush()
            End Using
        End If
    End Sub

    Protected Overrides Function ReverseUnit() As UndoUnit
        Return New CreatedFileUndoUnit(mFilePath, mFileText)
    End Function

    Protected Overrides ReadOnly Property Description() As String
        Get
            Return String.Format("Deleted {0}", Path.GetFileName(mFilePath))
        End Get
    End Property
End Class
