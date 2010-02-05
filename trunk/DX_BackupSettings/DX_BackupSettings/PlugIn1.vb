Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports Ionic.Zip

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
    Private Sub actBackupSettings_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles actBackupSettings.Execute
        Using SaveAs As New System.Windows.Forms.SaveFileDialog
            SaveAs.Filter = "*.zip|*.Zip"
            If SaveAs.ShowDialog() = DialogResult.OK Then
                Using zip As ZipFile = New ZipFile
                    zip.AddDirectory(CodeRush.Options.Paths.SettingsPreferredPath, "Settings.Xml")
                    zip.Save(SaveAs.FileName)
                End Using
            End If
        End Using
    End Sub
End Class
