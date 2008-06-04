Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core

<UserLevel(UserLevel.NewUser)> _
Public Class OptionsCreateStubForHandler

	'DXCore-generated code...
#Region " Initialize "
	Protected Overrides Sub Initialize()
		MyBase.Initialize()

		'TODO: Add your initialization code here.
	End Sub
#End Region

#Region " GetCategory "
	Public Shared Function GetCategory() As String
		Return "Editor\Refactoring"
	End Function
#End Region
#Region " GetPageName "
	Public Shared Function GetPageName() As String
		Return "CreateStubForHandler"
	End Function
#End Region
    Private Const Section As String = "OptionsCreateStubForHandler"
    Private Const POS_AFTER As String = "After"
    Private Const POS_BEFORE As String = "Before"
    Private Const KEY_DefaultPosition As String = "DefaultPosition"
    Private Const KEY_UseTargetPicker As String = "AllowTargetPicker"
    Private Sub OptionsCreateStubForHandler_PreparePage(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageStorageEventArgs) Handles Me.PreparePage
        ' Load Options 
        Dim DefaultPosition As String = String.Empty
        Dim UseTargetPicker As Boolean
        Call LoadOptions(ea.Storage, DefaultPosition, UseTargetPicker)
        optAfterSourceMethod.Checked = DefaultPosition = POS_AFTER
        optBeforeSourceMethod.Checked = Not optAfterSourceMethod.Checked
        chkAllowuserToModifyDefaultLocationWithPicker.Checked = UseTargetPicker
    End Sub

    Private Sub OptionsCreateStubForHandler_CommitChanges(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageStorageEventArgs) Handles Me.CommitChanges
        ' Save Options
        Call SaveSettings(ea.Storage, _
                          If(optAfterSourceMethod.Checked, POS_AFTER, POS_BEFORE), _
                          chkAllowuserToModifyDefaultLocationWithPicker.Checked)
    End Sub

    Public Shared Sub SaveSettings(ByVal Storage As DecoupledStorage, ByVal PositionSetting As String, ByVal AllowUserModifications As Boolean)
        Call Storage.WriteString(Section, KEY_DefaultPosition, PositionSetting)
        Call Storage.WriteBoolean(Section, KEY_UseTargetPicker, AllowUserModifications)
    End Sub

    Public Shared Sub LoadOptions(ByVal Storage As DecoupledStorage, ByRef DefaultPosition As String, ByRef UseTargetPicker As Boolean)
        DefaultPosition = Storage.ReadString(Section, KEY_DefaultPosition, POS_AFTER)
        UseTargetPicker = Storage.ReadBoolean(Section, KEY_UseTargetPicker, False)
    End Sub

    'Private Sub OptionsCreateStubForHandler_RestoreDefaults(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.OptionsPageEventArgs) Handles Me.RestoreDefaults
    '    ' Set Default Options

    'End Sub
End Class
