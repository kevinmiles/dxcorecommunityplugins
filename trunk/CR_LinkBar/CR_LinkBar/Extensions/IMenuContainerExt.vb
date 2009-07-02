Imports DevExpress.CodeRush.Menus
Imports System.Runtime.CompilerServices
Imports System.IO


Public Module IMenuContainerExt
    <Extension()> _
    Public Function CreateAndAddButton(ByVal LinkBar As IMenuContainer, ByVal Caption As String, Optional ByVal Description As String = "") As IMenuButton
        If Description = String.Empty Then
            Description = Caption
        End If
        Dim Control As IMenuButton = LinkBar.AddButton()
        Control.Caption = Caption
        Control.Visible = True
        Control.Enabled = True
        Control.DescriptionText = Description
        Return Control
    End Function
    <Extension()> _
    Public Function CreateAndAddDropDownButton(ByVal LinkBar As IMenuContainer, ByVal Caption As String, Optional ByVal Description As String = "") As IMenuPopup
        If Description = String.Empty Then
            Description = Caption
        End If
        Dim Control As IMenuPopup = LinkBar.AddPopup
        Control.Caption = Caption
        Control.Visible = True
        Control.Enabled = True
        Control.DescriptionText = Description
        Return Control
    End Function
End Module
