Imports DevExpress.CodeRush.Diagnostics.Core

Public Module Logging
    Public Sub LogReferenceAddFailure(ByVal Reference As Reference)
        Dim LogMessage As String = String.Format("Failed to Add Reference '{0}', '{1}'", _
                                                                Reference.FullName, _
                                                                Reference.FileName)
        Log.SendMsg(LogMessage)
    End Sub

End Module
