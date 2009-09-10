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
        ' Only if Caret on a method in ist's signature
        ea.Available = ea.Element.ElementType = LanguageElementType.Method
    End Sub

    Private Sub CreateDelegate_Apply(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.ApplyContentEventArgs) Handles CreateDelegate.Apply
        ' Create new Delegate Object 
        Dim M As Method = CType(ea.Element, Method)
        Dim b = ea.NewElementBuilder
        Dim D As SP.DelegateDefinition = b.BuildDelegateDefinition(M.Name & "Delegate", M.Parameters)
        D.MemberType = M.MemberType
        ' Insert into Tree
        Dim range = M.GetFullBlockCutRange
        M.Parent.InsertNode(M.Parent.Nodes.IndexOf(M), D)
        ' Render code into TextDocument
        ea.TextDocument.InsertText(range.Start.Line, range.Start.Offset, D.GenerateCode & System.Environment.NewLine)
    End Sub

End Class
