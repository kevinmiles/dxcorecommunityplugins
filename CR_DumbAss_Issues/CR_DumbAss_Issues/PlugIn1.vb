Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices
Imports System.Collections.Generic

Public Enum StyleEnum
    Hint
    Warning
    [Error]
End Enum
Public Class VarStartsWith
    Private mBadToStartWith As String
    Public Sub New(ByVal BadToStartWith As String)
        mBadToStartWith = BadToStartWith
    End Sub
    Public Function StartsWith(ByVal Element As LanguageElement) As Boolean
        Return Element.Name.StartsWith(mBadToStartWith)
    End Function
End Class
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


    Private Sub ImpossibleStringComparison_CheckCodeIssues(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs) Handles ImpossibleStringComparison.CheckCodeIssues
        Call ea.AddHint(LanguageElementType.RelationalOperation, AddressOf ImpossibleEquality.Qualifies, "Can never equate.")
        Call ea.AddHint(LanguageElementType.Variable, AddressOf New VarStartsWith("Fred").StartsWith, "Fred is Bad")
    End Sub
End Class
