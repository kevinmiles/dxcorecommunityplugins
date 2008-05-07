Namespace Test
    Public Class TestClass
        Public Sub TestMethod()
            Dim X As New System.Windows.Forms.Button
            AddHandler X.Click, AddressOf OnClick<<:caret:>>
        End Sub
    End Class
End Namespace