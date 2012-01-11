Imports DevExpress.CodeRush.Core

Public Class ActionExecutor
    ReadOnly mAction As Action
    ReadOnly mParamlist As String = String.Empty

    Public Sub New(ByVal Action As Action, ByVal Paramlist As String)
        mAction = Action
        mParamlist = Paramlist
    End Sub
    Public Sub ExecuteAction(ByVal sender As Object, ByVal e As DevExpress.CodeRush.Menus.MenuButtonClickEventArgs)
        mAction.DoExecute(mParamlist)
    End Sub

End Class
