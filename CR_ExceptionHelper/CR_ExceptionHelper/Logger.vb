Option Infer On

Public Class Logger
    Public Sub Write(ByVal Msg As String)
        DevExpress.CodeRush.Diagnostics.Debug.Write(Msg)
    End Sub
End Class
