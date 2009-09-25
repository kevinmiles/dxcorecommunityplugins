Imports System.Runtime.CompilerServices

Public Module CODE
    Private CSharpCode As String = _
    <code>
        public class ScriptClass 
        {
            public void ScriptExecute()
            {
                System.Console.WriteLine(123);
            } 
        }
    </code>.Value
    Public VBMethodWrapper As String = _
    <code>
        Public Class ScriptClass
            {0}
        End Class        
    </code>.Value
    Private x As String = <code> Imports System.ComponentModel
        Imports System.Drawing
        Imports System.Windows.Forms
        Imports DevExpress.CodeRush.Core
        Imports DevExpress.CodeRush.PlugInCore
        Imports DevExpress.CodeRush.StructuralParser
        </code>.Value
    Public VBMethod As String = _
    <code>
        Public Sub ScriptExecute()
            {0}
        End Sub
    </code>.Value
    Public VBActionHandler As String = _
    <code>
        Private Sub ScriptExecute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs)
            {0}
        End Sub
    </code>.Value
    Public Function WrapVBCodeInMethodAndClass(ByVal InternalMethodCode As String) As String
        Return InternalMethodCode.WrapIn(VBMethod).WrapIn(VBMethodWrapper)
    End Function
    Public Function WrapVBCodeInActionAndClass(ByVal InternalMethodCode As String) As String
        Return InternalMethodCode.WrapIn(VBActionHandler).WrapIn(VBMethodWrapper)
    End Function
    Public Function WriteLineReadLineTest() As String
        Return <code>
                    System.Console.WriteLine("Hello World")
                    System.Console.ReadLine()
               </code>.Value
    End Function
    <Extension()> _
    Public Function WrapIn(ByVal Source As String, ByVal StringWithFormatPos As String) As String
        Return String.Format(StringWithFormatPos, Source)
    End Function
End Module
