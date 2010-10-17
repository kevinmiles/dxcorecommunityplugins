Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports System.Xml

<UserLevel(UserLevel.NewUser)> _
Public Class EasyFields_Options

    'DXCore-generated code...
#Region " Initialize "
    Protected Overrides Sub Initialize()
        MyBase.Initialize()

        'TODO: Add your initialization code here.
    End Sub
#End Region

#Region " GetCategory "
    Public Shared Function GetCategory() As String
        Return "Editor\XPO"
    End Function
#End Region
#Region " GetPageName "
    Public Shared Function GetPageName() As String
        Return "EasyFields"
    End Function
#End Region

    Private Sub XPO_EasyFields_Options_CommitChanges(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CommitChangesEventArgs) Handles Me.CommitChanges
        Helpers.Settings.AvailableEntireClass = chkAvailableWithinEntireClass.Checked
        Helpers.Settings.UpdateOnSave = chkUpdateOnDocumentSave.Checked
        Helpers.Settings.ReplaceClassOnly = chkReplaceClassOnly.Checked
        Helpers.Settings.CommentFormat = txtCommentFormat.Text
        Helpers.Settings.UseComment = chkUseComment.Checked
        Helpers.Settings.FieldsClassVariableName = txtVariableName.Text
        Helpers.Settings.IncludeFieldConstants = chkIncludeFieldConstants.Checked
        Helpers.Settings.IncludeNonPersistent = chkIncludeNonPersistent.Checked
        Helpers.Settings.UseCollectionFieldsClass = chkUseCollectionsFieldsClass.Checked
        Helpers.Settings.IncludeInheritedMembers = chkIncludedInheritedMembers.Checked
    End Sub

    Private Sub XPO_EasyFields_Options_PreparePage(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageStorageEventArgs) Handles Me.PreparePage
        chkAvailableWithinEntireClass.Checked = Helpers.Settings.AvailableEntireClass
        chkReplaceClassOnly.Checked = Helpers.Settings.ReplaceClassOnly
        chkUpdateOnDocumentSave.Checked = Helpers.Settings.UpdateOnSave
        chkUseComment.Checked = Helpers.Settings.UseComment
        chkIncludeFieldConstants.Checked = Helpers.Settings.IncludeFieldConstants
        txtCommentFormat.Text = Helpers.Settings.CommentFormat
        txtVariableName.Text = Helpers.Settings.FieldsClassVariableName
        chkIncludeNonPersistent.Checked = Helpers.Settings.IncludeNonPersistent
        chkUseCollectionsFieldsClass.Checked = Helpers.Settings.UseCollectionFieldsClass
        chkIncludedInheritedMembers.Checked = Helpers.Settings.IncludeInheritedMembers
    End Sub

    Private Sub XPO_EasyFields_Options_RestoreDefaults(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageEventArgs) Handles Me.RestoreDefaults
        chkAvailableWithinEntireClass.Checked = False
        chkReplaceClassOnly.Checked = True
        chkUpdateOnDocumentSave.Checked = False
        chkUseComment.Checked = True
        txtCommentFormat.Text = Helpers.Settings.sDefaultCommentFormat
        chkIncludeNonPersistent.Checked = False
        chkIncludedInheritedMembers.Checked = False
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Process.Start("http://code.google.com/p/dxcorecommunityplugins/wiki/XPO_EasyFields")
    End Sub

    Private Sub chkUseCollectionsFieldsClass_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkUseCollectionsFieldsClass.CheckedChanged
        If chkUseCollectionsFieldsClass.Checked = True Then
            If MessageBox.Show("Are you sure you wish to enable this? this feature is not finished and has some major drawbacks, this is for testing only" & Environment.NewLine & Environment.NewLine & "Are you sure you wish to enable this?", "WARNING TEST FEATURE ONLY, NOT FOR PUBLIC USE", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) = DialogResult.No Then
                chkUseCollectionsFieldsClass.Checked = False
            End If
        End If
    End Sub
End Class
