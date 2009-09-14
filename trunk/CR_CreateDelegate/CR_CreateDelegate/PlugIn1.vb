Option Strict On
Option Explicit On
Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser

Public Class PlugIn1

	'DXCore-generated code...
#Region " InitializePlugIn "
	Public Overrides Sub InitializePlugIn()
		MyBase.InitializePlugIn()

		'TODO: Add your initialization code here.
	End Sub
#End Region
#Region " FinalizePlugIn "
	Public Overrides Sub FinalizePlugIn()
		'TODO: Add your finalization code here.

		MyBase.FinalizePlugIn()
	End Sub
#End Region

    Private Sub CreateDelegate_CheckAvailability(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CheckContentAvailabilityEventArgs) Handles CreateDelegate.CheckAvailability
        ' Only if Caret on a method 
        ea.Available = ea.Element.ElementType = LanguageElementType.Method
    End Sub

    Private Sub CreateDelegate_Apply(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.ApplyContentEventArgs) Handles CreateDelegate.Apply
        ' Get reference to Method
        Dim Method = CType(ea.Element, Method)
        ' Create new Delegate Object 
        Dim NewDelegate As SP.DelegateDefinition
        NewDelegate = ea.NewElementBuilder.BuildDelegateDefinition(Method.Name & "Delegate", Method.Parameters)
        NewDelegate.MemberType = Method.MemberType
        NewDelegate.Visibility = Method.Visibility
        ' Store current location
        Dim Range = Method.GetFullBlockCutRange
        ' Insert into Tree
        Method.Parent.InsertNode(Method.Parent.Nodes.IndexOf(Method), NewDelegate)
        ' Render code into TextDocument
        Dim NewCode As String = NewDelegate.GenerateCode & System.Environment.NewLine
        ea.TextDocument.InsertText(Range.Start.Line, Range.Start.Offset, NewCode)
    End Sub

End Class
