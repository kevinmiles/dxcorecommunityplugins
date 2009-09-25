Public Class ScriptInput

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim Script = TextBox1.Text

    End Sub
    Public Shared Function GetCode() As String
        Return CODE.WriteLineReadLineTest()
    End Function
End Class
