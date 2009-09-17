Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.IO

Public Class PlugIn1

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()

        Call StartMonitoring()
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region
    Private Sub StartMonitoring()
        Dim Path As String = If(CodeRush.Options.Paths.SettingsOverrideExists, _
                                CodeRush.Options.Paths.SettingsOverridePath, _
                                CodeRush.Options.Paths.SettingsPreferredPath)
        Dim X As New FileSystemWatcher(Path)
        X.IncludeSubdirectories = True
        AddHandler X.Created, AddressOf OnSettingsFileChanged
        AddHandler X.Changed, AddressOf OnSettingsFileChanged
    End Sub
    Private Sub OnSettingsFileChanged(ByVal sender As Object, ByVal e As FileSystemEventArgs)
        ' Called when settings are changed or created.
        CodeRush.Options.ReloadAll()
    End Sub

End Class
