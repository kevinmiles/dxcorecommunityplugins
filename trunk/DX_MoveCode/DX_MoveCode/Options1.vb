Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core

<UserLevel(UserLevel.Advanced)> _
Public Class Options1
    Public Const SECTION_MOVE_CODE As String = "MoveCode"
    Public Const SETTING_MOVE_SOURCE As String = "MoveSource"
    Public Const SETTING_SWAP_ELEMENTS As String = "SwapElements"

    'DXCore-generated code...
#Region " Initialize "
    Protected Overrides Sub Initialize()
        MyBase.Initialize()

        'TODO: Add your initialization code here.
    End Sub
#End Region

#Region " GetCategory "
    Public Shared Function GetCategory() As String
        Return "IDE"
    End Function
#End Region
#Region " GetPageName "
    Public Shared Function GetPageName() As String
        Return SECTION_MOVE_CODE
    End Function
#End Region

    Private Sub Options1_RestoreDefaults(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageEventArgs) Handles Me.RestoreDefaults
        optMoveSource.Checked = True
        optSwapElements.Checked = False
    End Sub

    Private Sub Options1_PreparePage(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageStorageEventArgs) Handles Me.PreparePage
        ' Load
        optMoveSource.Checked = ea.Storage.ReadBoolean(SECTION_MOVE_CODE, SETTING_MOVE_SOURCE, True)
        optSwapElements.Checked = ea.Storage.ReadBoolean(SECTION_MOVE_CODE, SETTING_SWAP_ELEMENTS, False)
    End Sub

    Private Sub Options1_CommitChanges(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CommitChangesEventArgs) Handles Me.CommitChanges
        ' Save
        ea.Storage.WriteBoolean(SECTION_MOVE_CODE, SETTING_MOVE_SOURCE, optMoveSource.Checked)
        ea.Storage.WriteBoolean(SECTION_MOVE_CODE, SETTING_SWAP_ELEMENTS, optSwapElements.Checked)
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub
End Class
