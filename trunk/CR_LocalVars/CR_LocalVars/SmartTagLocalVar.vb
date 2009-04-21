Imports DevExpress.CodeRush.Core

Public Class SmartTagLocalVar
    Inherits SmartTagItem
    Private mVarName As String
    Public Sub New(ByVal VarName As String, ByVal Ordinal As Integer)
        mVarName = VarName
        Caption = String.Format("&{0}.> {1}", _
                                Ordinal, _
                                VarName)
    End Sub

    Private Sub SmartTagLocalVar_Execute(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Execute
        CodeRush.Documents.ActiveTextDocument.InsertText(CodeRush.Caret.SourcePoint, mVarName)
    End Sub
End Class