Namespace Test
    Public Class TestClass
        Public Sub TestMethod()
            Dim X As New System.Windows.Forms.Button
            AddHandler X.Click, AddressOf X_Click<<:caret:>>
        End Sub
    End Class
End Namespace

Namespace Result
    Public Class TestClass
        Public Sub TestMethod()
            Dim X As New System.Windows.Forms.Button
            AddHandler X.Click, AddressOf X_Click
        End Sub

        Private Sub X_Click(ByVal sender As Object, ByVal e As System.EventArgs)
            Throw New System.NotImplementedException()
        End Sub
    End Class
End Namespace

