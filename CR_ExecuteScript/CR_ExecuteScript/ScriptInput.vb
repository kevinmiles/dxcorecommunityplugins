Public Class ScriptInput

#Region "Fields"
    Private mCodeCombo As CodeCombo
#End Region
#Region "Simple Properties"
    Public ReadOnly Property CodeCombo() As CodeCombo
        Get
            Return mCodeCombo
        End Get
    End Property
#End Region
    Public Shared Function GetCode() As CodeCombo
        Using Screen As New ScriptInput
            If Screen.ShowDialog <> Windows.Forms.DialogResult.OK Then
                Return Nothing
            End If
            Return New CodeCombo(Screen.txtImports.Text, Screen.txtSource.Text, Screen.txtReferences.Text)
        End Using
    End Function
End Class
