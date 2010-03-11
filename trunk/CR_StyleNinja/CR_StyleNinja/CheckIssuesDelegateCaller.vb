Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser


Public Delegate Sub CheckCodeIssuesDelegate(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
Public Class CheckIssuesDelegateCaller
    Private mCheckIssues As CheckCodeIssuesDelegate
    Public Sub New(ByVal CheckIssues As CheckCodeIssuesDelegate)
        mCheckIssues = CheckIssues

    End Sub
    Public Sub Invoke(ByVal sender As Object, ByVal ea As CheckCodeIssuesEventArgs)
        mCheckIssues.Invoke(sender, ea)
    End Sub
End Class
