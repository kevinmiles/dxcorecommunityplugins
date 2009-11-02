Option Strict On
Imports DevExpress.CodeRush.Core
Imports System.Runtime.CompilerServices

Public Module GetSmartTagItemsEventArgsExt
    <Extension()> _
    Public Sub CreateSubMenu(ByVal ea As GetSmartTagItemsEventArgs, ByVal MenuName As String, ByVal References As IEnumerable(Of Reference))
        Dim TopLevel As SmartTagItem = New SmartTagItem(MenuName)
        For Each Reference In References
            If Not Reference.IsReferencedByProject(CodeRush.Project.Active) Then
                TopLevel.AddItem(New ReferenceSmartTagItem(Reference))
            End If
        Next
        If TopLevel.Items.Count > 0 Then
            ea.Add(TopLevel)
        End If
    End Sub
End Module
