Imports System
Imports System.IO
Imports DevExpress.CodeRush.Interop.OLE.Helpers
Imports DXCoreEngine = DevExpress.CodeRush.Core.CodeRush
Public Class AddedProjectFileUndoUnit
    Inherits UndoUnit
    Private mProjectName As String
    Private mFilePath As String
    Public Sub New(ByVal ProjectName As String, ByVal FilePath As String)
        mProjectName = ProjectName
        mFilePath = FilePath
    End Sub

    Protected Overrides ReadOnly Property Description() As String
        Get
            Return String.Format("Added {0} to {1}", Path.GetFileName(mFilePath), mProjectName)
        End Get
    End Property

    Protected Overrides Sub Execute()
		DXCoreEngine.Solution.RemoveFileFromProject(mProjectName, mFilePath)
    End Sub

    Protected Overrides Function ReverseUnit() As UndoUnit
        Return New RemovedProjectFileUndoUnit(mProjectName, mFilePath)
    End Function

End Class
