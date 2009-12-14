Option Strict On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports EnvDTE
Imports System.IO

Public Class ReferenceSmartTagItem
    Inherits SmartTagItem
    Private mReference As Reference
    Public Sub New(ByVal Reference As Reference)
        MyBase.New(String.Format("{0}", Reference.FilenameWithDescription))
        mReference = Reference
    End Sub
    Protected Overrides Sub OnExecute()
        Try
            Call mReference.AddToProjectTestingForGAC(CodeRush.Project.Active)
        Catch ex As Exception

        End Try
    End Sub

End Class