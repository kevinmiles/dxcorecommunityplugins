Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Public Class TextFieldPickList

	'DXCore-generated code...
#Region " InitializePlugIn "
	Public Overrides Sub InitializePlugIn()
		MyBase.InitializePlugIn()
        AddHandler EventNexus.TextFieldActivated, AddressOf OnTextFieldActivated
        AddHandler EventNexus.TextFieldDeactivated, AddressOf OnTextFieldDeactivated
    End Sub
#End Region
#Region " FinalizePlugIn "
	Public Overrides Sub FinalizePlugIn()
        RemoveHandler EventNexus.TextFieldActivated, AddressOf OnTextFieldActivated
        RemoveHandler EventNexus.TextFieldDeactivated, AddressOf OnTextFieldDeactivated
        MyBase.FinalizePlugIn()
    End Sub
#End Region

    Private CurrentSelectForm As SelectForm = Nothing
    Private CurrentPickListField As PickListField

    Private Sub OnTextFieldActivated(ByVal ea As TextFieldEventArgs)
        If Not TypeOf ea.TextField Is PickListField Then
            Exit Sub
        End If
        If ea.TextField Is CurrentPickListField Then
            ' TextField Not changed
            Exit Sub
        End If
        CurrentPickListField = CType(ea.TextField, PickListField)
        ' Create a New List 
        Call ShowList(CurrentPickListField.PickList.ToArray)

    End Sub
    Private Sub OnTextFieldDeactivated(ByVal ea As TextFieldEventArgs)
        CurrentPickListField = Nothing
        Call HideList()
    End Sub
    Private Sub ShowList(ByVal ParamArray Parameters As String())
        Dim SelectForm As New SelectForm
        SelectForm.LoadItems(Parameters)
        Dim ShowPoint As Point
        CodeRush.Documents.ActiveTextView.GetPoint(PointUnderRange(CurrentPickListField.Range), ShowPoint)
        Call ShowPoint.Offset(CodeRush.Documents.ActiveTextView.ScreenBounds.Location)
        Call SelectForm.Show()
        SelectForm.Location = ShowPoint
        CurrentSelectForm = SelectForm
        AddHandler CurrentSelectForm.lstItems.SelectedIndexChanged, AddressOf SelectedItemChanged
    End Sub
    Private Function PointUnderRange(ByVal Range As SourceRange) As SourcePoint
        ' Return the SourcePoint under Range
        Dim ReturnPoint As SourcePoint = New SourcePoint(Range.End.Line + 1, Range.Start.Offset)
        Return ReturnPoint
    End Function
    Private Sub SelectedItemChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim CurentDoc As TextDocument = CodeRush.Documents.ActiveTextDocument
        Call CurentDoc.SetText(CurrentPickListField.Range, _
                               CurrentSelectForm.lstItems.SelectedItem.ToString)
    End Sub
    Private Sub HideList()
        If CurrentSelectForm Is Nothing Then
            Exit Sub
        End If
        RemoveHandler CurrentSelectForm.lstItems.SelectedIndexChanged, AddressOf SelectedItemChanged
        CurrentSelectForm.Hide()
        CurrentSelectForm.Dispose()
        CurrentSelectForm = Nothing
    End Sub


    Private Sub FieldEx_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteTextCommandEventArgs) Handles FieldPickList.Execute
        Dim Text As String = FieldPickList.Parameters.GetString("text")
        Dim Description As String = FieldPickList.Parameters.GetString("description")
        Dim CustomField As PickListField = FieldHelper.CreateCustomField(ea.TextDocument, ea.InsertionPoint, Text, Description)
        Dim ItemString As String = FieldPickList.Parameters.GetString("items")
        Dim Params = ItemString.Split("|"c).ToList
        CustomField.PickList.AddRange(PArams) 
    End Sub
End Class

