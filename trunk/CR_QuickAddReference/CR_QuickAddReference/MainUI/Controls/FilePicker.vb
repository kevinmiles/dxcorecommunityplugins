Public Class FilePicker

    Property Filename() As String
        Get
            Return txtFilename.Text
        End Get
        Set(ByVal value As String)
            txtFilename.Text = value
        End Set
    End Property
    Private Sub cmdPickFromFS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPickFromFS.Click
        OpenFileDialog1.Filter = "References|*.dll"
        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
            txtFilename.Text = OpenFileDialog1.FileName
        End If
    End Sub
End Class
