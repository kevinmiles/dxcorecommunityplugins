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
        Public Sub X_Click(ByVal Sender As Object, ByVal e As ClickEventArgs)

        End Sub
    End Class
End Namespace

