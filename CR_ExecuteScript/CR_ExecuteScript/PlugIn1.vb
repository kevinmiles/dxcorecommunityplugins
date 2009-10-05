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

    Private Sub ExecuteScript_Execute(ByVal ea As ExecuteEventArgs) Handles ExecuteScript.Execute
        Dim Combo = ScriptInput.GetCode()
        If Not Combo Is Nothing Then
            Dim Compiler As Compiler = New Compiler()
            Dim CompilerGen = Compiler.Compile(New VBCodeProvider, Combo)
            If CompilerGen.Success Then
                Call ExecuteHandleAction(CompilerGen, ea)
            Else
                MsgBox(Microsoft.VisualBasic.Join(CompilerGen.Errors.ToArray, Environment.NewLine))
            End If
        End If
    End Sub
    Public Sub ExecuteHandleAction(ByVal Source As Object, ByVal ea As ExecuteEventArgs)
        Dim type As System.Type = Source.GetType()
        type.InvokeMember("ScriptExecute", _
                          Reflection.BindingFlags.InvokeMethod Or Reflection.BindingFlags.Default, _
                          Nothing, Source, New Object() {ea})
    End Sub

End Class
