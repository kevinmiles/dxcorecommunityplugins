Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Menus
Imports System.Runtime.CompilerServices


Public Class UrlWrapper
    Private mUrl As String
    Public Sub New(ByVal Url As String)
        mUrl = Url
    End Sub
    Public Sub Visit()
        Process.Start(mUrl)
    End Sub
End Class
