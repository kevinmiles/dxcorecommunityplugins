Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Module FieldHelper
    Public Function CreateCustomField(ByVal TextDocument As TextDocument, _
                                      ByVal InsertionPoint As EditPoint, _
                                      ByVal Text As String, _
                                      ByVal Description As String) As PickListField
        ' Default the insertion point.
        If InsertionPoint Is Nothing Then
            InsertionPoint = CreateEditPoint(TextDocument, CodeRush.Caret.SourcePoint)
        End If

        ' Store Current Sourcepoint.

        Dim CurrentPoint As SourcePoint = InsertionPoint.ToSourcePoint()
        If Not CodeRush.StrUtil.IsNullOrEmpty(Text) Then
            InsertionPoint.Insert(Text)
        End If

        Dim StartPoint As EditPoint = CreateEditPoint(TextDocument, CurrentPoint, "CustomFieldStart")
        StartPoint.IsPushable = False

        Dim EndPoint As EditPoint = CreateEditPoint(TextDocument, InsertionPoint.ToSourcePoint(), "CustomFieldEnd")
        EndPoint.IsPushable = False

        If CodeRush.StrUtil.IsNullOrEmpty(Text) Then
            StartPoint.IsAnchorable = True
            EndPoint.IsAnchorable = True
        End If

        Dim NewField As PickListField = New PickListField(StartPoint, EndPoint, Description)
        TextDocument.TextFields.Add(NewField)
        Return NewField
    End Function
    Private Function CreateEditPoint(ByVal TextDocument As TextDocument, ByVal Pos As SourcePoint, Optional ByVal Name As String = "") As EditPoint
        Dim virtualSpaces As Integer
        Return TextDocument.CreateEditPoint(Pos.Line, Pos.Offset, Name, virtualSpaces)
    End Function
End Module