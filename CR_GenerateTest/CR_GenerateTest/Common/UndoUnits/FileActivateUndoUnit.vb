Imports System
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.Interop.OLE.Helpers
Imports DXCoreEngine = DevExpress.CodeRush.Core.CodeRush

Public Class FileActivateUndoUnit
    Inherits UndoUnit
    Private mOriginalFileName As String
    Private mRedoFileName As String

    Public Sub New(ByVal originalFileName As String)
        mOriginalFileName = originalFileName
    End Sub

    Protected Overrides Sub Execute()
        Dim activeDocument As TextDocument = DXCoreEngine.Documents.ActiveTextDocument
        If (activeDocument IsNot Nothing) Then
            mRedoFileName = activeDocument.FullName
        Else
            mRedoFileName = String.Empty

        End If
        DXCoreEngine.File.Activate(mOriginalFileName)
    End Sub
    Protected Overrides Function ReverseUnit() As UndoUnit
        Return New FileActivateRedoUnit(mOriginalFileName, mRedoFileName)
    End Function

    Protected Overrides ReadOnly Property Description() As String
        Get
            Return "File activate"
        End Get
    End Property
End Class
