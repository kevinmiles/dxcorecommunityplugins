Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices
Imports System.Collections.Generic

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

    Private Sub ImpossibleLowercaseEquality_CheckCodeIssues(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CheckCodeIssuesEventArgs) Handles ImpossibleLowercaseEquality.CheckCodeIssues
        Call ea.AddHint(LanguageElementType.RelationalOperation, AddressOf WarnEqualityImpossible.Qualifies, "Can never equate.")
        LowercaseString.CodeIssueMessage = ImpossibleLowercaseEquality.ProviderName
    End Sub

    Private Sub LowercaseString_CheckAvailability(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CheckContentAvailabilityEventArgs) Handles LowercaseString.CheckAvailability
        ea.Available = ea.Element.ElementType = LanguageElementType.RelationalOperation
    End Sub

    Private Sub LowercaseString_Apply(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.ApplyContentEventArgs) Handles LowercaseString.Apply
        Dim RO As RelationalOperation = CType(ea.Element, RelationalOperation)
        Dim StringPrimative = TryCast(RO.RightSide, PrimitiveExpression)
        ea.TextDocument.Replace(StringPrimative.Range, StringPrimative.Name.ToLower, "Lowercase String")
    End Sub

End Class
