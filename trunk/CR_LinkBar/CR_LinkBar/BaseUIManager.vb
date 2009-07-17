Imports System.Drawing
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.Menus
Imports System.IO
Imports DevExpress.CodeRush.StructuralParser
Imports System.Reflection


Public MustInherit Class BaseUIManager
    Implements IUIManager
#Region "Constants"
    Protected Const PNG_FOLDER As String = "Workspace.png"
    Protected Const PNG_SAVEANDCLOSEALL As String = "SaveAndCloseAll.png"
    Protected Const PNG_CREATEWORKSPACE As String = "CreateWorkspace.png"
#End Region
#Region "Fields"
    Protected mEnabled As Boolean = False
    Protected mMenuBar As MenuBar
    Protected mLinkBar As LinkBar
#End Region
#Region "Properties"
    Public Property Enabled() As Boolean
        Get
            Return mEnabled
        End Get
        Set(ByVal Value As Boolean)
            For i As Integer = 0 To mMenuBar.Count
                mMenuBar.Item(i).Enabled = Value
            Next
            mEnabled = Value
        End Set
    End Property
#End Region
#Region "Constructors"
    Public Sub New(ByVal LinkBar As LinkBar)
        mLinkBar = LinkBar
    End Sub
#End Region

#Region "Abstract Methods"
    Public MustOverride Sub Refresh() Implements IUIManager.Refresh
#End Region

    Public Function GetBitmapByName(ByVal BitmapName As String) As Bitmap
        Dim Asm As System.Reflection.Assembly = System.Reflection.Assembly.GetAssembly(Me.GetType)
        Dim stream As IO.Stream = Asm.GetManifestResourceStream(String.Format("CR_LinkBar.{0}", BitmapName))
        Return CType(Bitmap.FromStream(stream), Bitmap)
    End Function
    Protected Sub ResetToolBar()
        If mMenuBar IsNot Nothing Then
            mMenuBar.Delete()
            mMenuBar = Nothing
        End If
        mMenuBar = CodeRush.Menus.Bars.Add("LinkBar")
        mMenuBar.Position = BarPosition.Top
        mMenuBar.Visible = True
    End Sub
    Protected Sub CreateSaveAllAndCloseButton()
        Dim Button = CreateAndAddButton(mMenuBar, "Clear Workspace", "Saves all files and then Closes them.")
        Button.Style = ButtonStyle.Icon
        Button.SetFace(GetBitmapByName(PNG_SAVEANDCLOSEALL))
        AddHandler Button.Click, AddressOf mLinkBar.OnClickSaveAndCloseAll
    End Sub
    Protected Sub CreateCreateNewWorkspaceButton()
        Dim Button As IMenuButton
        Button = mMenuBar.CreateAndAddButton("Create Workspace")
        Button.Style = ButtonStyle.Icon
        Button.SetFace(GetBitmapByName(PNG_CREATEWORKSPACE))
        AddHandler Button.Click, AddressOf mLinkBar.OnClickCreateNewWorkspace
    End Sub


End Class
