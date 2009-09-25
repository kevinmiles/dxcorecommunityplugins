Option Strict On
Option Explicit On
Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices

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

    Private Sub ExecuteScript_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles ExecuteScript.Execute
        Dim BaseCode As String = "System.Windows.Forms.MessageBox.Show(""Hello World"")"
        Dim ClassWithAction = CODE.WrapVBCodeInActionAndClass(BaseCode)
        Dim Compiler As Compiler = New Compiler()
        Dim TheClass As Object = Compiler.Compile(New VBCodeProvider, ClassWithAction)
        Call ExecuteHandleAction(TheClass, ea)
    End Sub
    Public Sub ExecuteHandleAction(ByVal Source As Object, ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs)
        Dim type As System.Type = Source.GetType()
        type.InvokeMember("HandleAction", _
                          Reflection.BindingFlags.InvokeMethod Or Reflection.BindingFlags.Default, _
                          Nothing, Source, Nothing)
    End Sub
End Class
