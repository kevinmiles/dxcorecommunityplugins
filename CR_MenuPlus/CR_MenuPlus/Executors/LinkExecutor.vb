
Public Class LinkExecutor
    ReadOnly mLink As String
    Public Sub New(ByVal Link As String)
        mLink = Link
    End Sub
    Public Sub Launch(ByVal sender As Object, ByVal e As DevExpress.CodeRush.Menus.MenuButtonClickEventArgs)
        Process.Start(mLink)
    End Sub
End Class
