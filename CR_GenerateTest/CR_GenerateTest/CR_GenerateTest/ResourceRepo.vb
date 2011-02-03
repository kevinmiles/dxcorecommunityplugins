Option Infer On
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Imports EnvDTE80
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Reflection

Public Class ResourceRepo
    Public Function GetStringResource(ByVal Filename As String) As String
        Dim Asm As Assembly = Assembly.GetAssembly(Me.GetType)
        Dim stream As IO.Stream = Asm.GetManifestResourceStream(String.Format("CR_GenerateTest.{0}", Filename))
        Using Reader As StreamReader = New IO.StreamReader(stream)
            Return Reader.ReadToEnd()
        End Using
    End Function
End Class