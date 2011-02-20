Imports System
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.Interop.OLE.Helpers

Imports DXCoreEngine = DevExpress.CodeRush.Core.CodeRush

Public Class FileActivateRedoUnit
    Inherits UndoUnit
    Private mOriginalFileName As String
    Private mRedoFileName As String

    Public Sub New(ByVal originalFileName As String, ByVal redoFileName As String)

        mOriginalFileName = originalFileName
        mRedoFileName = redoFileName
    End Sub

    Protected Overrides Sub Execute()
        If (DXCoreEngine.StrUtil.IsNullOrEmpty(mRedoFileName)) Then
            Return
        End If

        DXCoreEngine.File.Activate(mRedoFileName)
    End Sub

    Protected Overrides Function ReverseUnit() As UndoUnit

        Return New FileActivateUndoUnit(mOriginalFileName)
    End Function


    Protected Overrides ReadOnly Property Description() As String
        Get
            Return "File activate"
        End Get
    End Property
End Class
