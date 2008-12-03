Public Class ReferenceListView
#Region "Fields"
    Private mSaveKey As String
#End Region
#Region "Properties"
    Public Property SaveKey() As String
        Get
            Return mSaveKey
        End Get
        Set(ByVal Value As String)
            mSaveKey = Value
        End Set
    End Property
#End Region
    Private Sub ListView_ColumnWidthChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnWidthChangedEventArgs) Handles ListView.ColumnWidthChanged
        UpdateColWidths()
    End Sub

    Private Sub ListView_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView.Resize
        UpdateColWidths()
    End Sub

    Private Sub UpdateColWidths()
        If colName.Width + colLocation.Width < ListView.Width Then
            'colLocation.Width = -2
        End If
    End Sub

End Class
