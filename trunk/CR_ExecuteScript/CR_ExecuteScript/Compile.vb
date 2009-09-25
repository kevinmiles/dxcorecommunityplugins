Imports System
Imports System.IO
Imports Specialized = System.Collections.Specialized
Imports Reflection = System.Reflection
Imports CodeDom = System.CodeDom.Compiler
Imports System.Runtime.CompilerServices

Public NotInheritable Class Compiler
    Public Function Compile(ByVal provider As CodeDom.CodeDomProvider, ByVal source As String) As Object
        Dim param As CodeDom.CompilerParameters = New CodeDom.CompilerParameters()
        param.GenerateExecutable = False
        param.IncludeDebugInformation = False
        param.GenerateInMemory = True
        Dim ProgramFiles = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) & "\"

        For Each Ass In System.Reflection.Assembly.GetExecutingAssembly().GetReferencedAssemblies.Take(1)
            If Ass.Name <> "mscorlib" Then
                If Not TryAddPath(param, Ass, Path.Combine(ProgramFiles, "Reference Assemblies\Microsoft\Framework\v3.0\" & Ass.Name & ".dll")) Then
                    If Not TryAddPath(param, Ass, Path.Combine(ProgramFiles, "Reference Assemblies\Microsoft\Framework\v3.5\" & Ass.Name & ".dll")) Then
                        Call param.ReferencedAssemblies.Add(Ass.Name & ".dll")
                    End If
                End If
            End If
        Next

        Dim cc As CodeDom.ICodeCompiler = provider.CreateCompiler()
        Dim cr As CodeDom.CompilerResults = cc.CompileAssemblyFromSource(param, source)

        Dim output As Specialized.StringCollection = cr.Output
        If Not cr.Errors.HasErrors Then
            Return cr.CompiledAssembly.CreateInstance("ScriptClass")
        End If
        Call ReportErrors(cr)
        Return Nothing
    End Function
    Private Shared Function TryAddPath(ByVal param As CodeDom.CompilerParameters, ByVal Ass As System.Reflection.AssemblyName, ByVal TryPath As String) As Boolean
        If File.Exists(TryPath) Then
            param.ReferencedAssemblies.Add(Ass.Name & ".dll")
            Return True
        End If
    End Function

    Private Shared Sub ReportErrors(ByVal cr As CodeDom.CompilerResults)
        System.Console.WriteLine("Error invoking scripts.")
        Dim es As CodeDom.CompilerErrorCollection = cr.Errors
        For Each s As CodeDom.CompilerError In es
            System.Console.WriteLine(s.ErrorText)
        Next
        System.Console.ReadLine()
    End Sub
End Class
