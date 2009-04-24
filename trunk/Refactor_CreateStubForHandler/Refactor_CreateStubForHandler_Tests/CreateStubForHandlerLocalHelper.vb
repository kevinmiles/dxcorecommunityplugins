Imports DevExpress.CodeRush.Core
Imports DevExpress.DXCore.Testing
Imports DevExpress.Refactor.Testing
Imports DevExpress.CodeRush.StructuralParser

Public Class CreateStubForHandlerLocalHelper
    Inherits RefactoringHelper
#Region "Consts"
    Private Const PAGE As String = "Editor\Refactoring\CreateStubForHandler"
#End Region
#Region "Fields"
    Private mDefaultPosition As String
    Private mUseTargetPicker As Boolean
#End Region
#Region "Options Control"
    Private Const Section As String = "OptionsCreateStubForHandler"
    Private Const POS_AFTER As String = "After"
    Private Const POS_BEFORE As String = "Before"
    Private Const KEY_DefaultPosition As String = "DefaultPosition"
    Private Const KEY_UseTargetPicker As String = "AllowTargetPicker"

    Private Shared Sub SaveSettings(ByVal Storage As DecoupledStorage, ByVal PositionSetting As String, ByVal AllowUserModifications As Boolean)
        Call Storage.WriteString(Section, KEY_DefaultPosition, PositionSetting)
        Call Storage.WriteBoolean(Section, KEY_UseTargetPicker, AllowUserModifications)
    End Sub

    Private Shared Sub LoadSettings(ByVal Storage As DecoupledStorage, ByRef DefaultPosition As String, ByRef UseTargetPicker As Boolean)
        DefaultPosition = Storage.ReadString(Section, KEY_DefaultPosition, POS_AFTER)
        UseTargetPicker = Storage.ReadBoolean(Section, KEY_UseTargetPicker, False)
    End Sub
#End Region

    Protected Overrides Sub UpdateOptions()
        MyBase.UpdateOptions()
        Using Storage As DecoupledStorage = GetStorage(PAGE)
            ' DisablePicker
            Call LoadSettings(Storage, mDefaultPosition, mUseTargetPicker)
            Call SaveSettings(Storage, "After", False)
        End Using
    End Sub
    Protected Overrides Sub AfterExecute()
        MyBase.AfterExecute()
        Using Storage As DecoupledStorage = GetStorage(PAGE)
            ' Restore Settings
            Call SaveSettings(Storage, mDefaultPosition, mUseTargetPicker)
        End Using
    End Sub






End Class
