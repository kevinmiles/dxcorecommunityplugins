Imports System
Imports System.IO
Imports DevExpress.CodeRush.Interop.OLE.Helpers

Public Class CreatedFileUndoUnit
    Inherits UndoUnit

    Private mFilePath As String
    Private mFileText As String

    Public Sub New(ByVal filePath As String, ByVal fileText As String)

        mFilePath = filePath
        mFileText = fileText
    End Sub

    Protected Overrides Sub Execute()
        If File.Exists(mFilePath) Then
            File.Delete(mFilePath)
        End If
    End Sub
    Protected Overrides Function ReverseUnit() As UndoUnit
        Return New DeletedFileUndoUnit(mFilePath, mFileText)
    End Function
    Protected Overrides ReadOnly Property Description() As String
        Get
            Return String.Format("Created {0}", Path.GetFileName(mFilePath))
        End Get
    End Property

    Public Sub SetFileText(ByVal fileText As String)
        mFileText = fileText
    End Sub
End Class