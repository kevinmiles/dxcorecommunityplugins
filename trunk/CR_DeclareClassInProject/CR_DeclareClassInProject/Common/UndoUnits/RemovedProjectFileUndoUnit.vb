Imports System
Imports System.IO
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.Interop.OLE.Helpers
Imports DevExpress.Refactor.Diagnostics

Imports DXCoreEngine = DevExpress.CodeRush.Core.CodeRush

Public Class RemovedProjectFileUndoUnit
    Inherits UndoUnit
    Private mProjectName As String
    Private mFilePath As String

    Public Sub New(ByVal projectName As String, ByVal filePath As String)
        mProjectName = projectName
        mFilePath = filePath
    End Sub

    Protected Overrides Sub Execute()
        DXCoreEngine.Solution.AddFileToProject(mProjectName, mFilePath)
    End Sub

    Protected Overrides Function ReverseUnit() As UndoUnit
        Return New AddedProjectFileUndoUnit(mProjectName, mFilePath)
    End Function

    Protected Overrides ReadOnly Property Description() As String
        Get
            Return String.Format("Removed {0} from {1}", Path.GetFileName(mFilePath), mProjectName)
        End Get
    End Property
End Class