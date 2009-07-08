Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Menus
Imports System.Runtime.CompilerServices
Imports System.IO

Public Class PlugIn1

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        If DevExpress.CodeRush.Core.CodeRush.Solution.Active.Count > 0 Then
            LinkBar.LoadWorkspaces()
        End If
        Call LinkBar.Refresh()
        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.
        MyBase.FinalizePlugIn()
    End Sub
#End Region
#Region " Fields "
    Private LinkBar As New LinkBar
#End Region

    Private Sub PlugIn1_SolutionOpened() Handles Me.SolutionOpened
        'LinkBar.Enabled = True
        LinkBar.LoadWorkspaces()
    End Sub

    Private Sub PlugIn1_BeforeClosingSolution() Handles Me.BeforeClosingSolution
        LinkBar.SaveWorkspaces()
    End Sub

    Private Sub PlugIn1_AfterClosingSolution() Handles Me.AfterClosingSolution
        LinkBar.ClearWorkSpaces()
        LinkBar.Refresh()
        'LinkBar.Enabled = False
    End Sub
End Class