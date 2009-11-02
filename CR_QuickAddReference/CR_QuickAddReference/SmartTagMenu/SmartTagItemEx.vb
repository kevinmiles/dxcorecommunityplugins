Option Strict On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports EnvDTE
Imports System.IO
Imports System.Runtime.CompilerServices


Public Class SmartTagItemEx
    Inherits SmartTagItem

    Private mMethod As MethodInvoker
    Public Sub New(ByVal Name As String, ByVal Method As MethodInvoker)
        mMethod = Method
        Me.Caption = Name
    End Sub
    Protected Overrides Sub OnExecute()
        mMethod.Invoke()
    End Sub
End Class
