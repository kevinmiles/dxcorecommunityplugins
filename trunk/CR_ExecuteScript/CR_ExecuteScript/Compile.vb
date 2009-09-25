Imports System
Imports System.IO
Imports Specialized = System.Collections.Specialized
Imports Reflection = System.Reflection
Imports CodeDom = System.CodeDom.Compiler
Imports System.Runtime.CompilerServices
Imports DX = DevExpress.CodeRush.Core
Public NotInheritable Class Compiler
    Public Function Compile(ByVal provider As CodeDom.CodeDomProvider, ByVal CodeCombo As CodeCombo) As Result(Of Object)
        Dim Param = New CodeDom.CompilerParameters()
        Param.GenerateExecutable = False
        Param.IncludeDebugInformation = False
        Param.GenerateInMemory = True

        Call SetupReferences(CodeCombo, Param)

        Dim Source = CodeCombo.Imports & Environment.NewLine & CODE.WrapVBCodeInActionAndClass(CodeCombo.Code)
        Dim cc As CodeDom.ICodeCompiler = provider.CreateCompiler()
        Dim cr As CodeDom.CompilerResults = cc.CompileAssemblyFromSource(Param, Source)

        Dim output As Specialized.StringCollection = cr.Output
        If Not cr.Errors.HasErrors Then
            Return New Result(Of Object)(cr.CompiledAssembly.CreateInstance("ScriptClass"), True, Nothing)
        Else
            Return New Result(Of Object)(Nothing, False, ReportErrors(cr))
        End If
    End Function
    Private Shared Sub SetupReferences(ByVal CodeCombo As CodeCombo, ByVal Param As CodeDom.CompilerParameters)
        Dim ProgramDir = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) & "\"
        Param.ReferencedAssemblies.Add("System.dll")
        Param.ReferencedAssemblies.Add("System.Data.dll")
        Param.ReferencedAssemblies.Add("System.Windows.Forms.dll")
        Param.ReferencedAssemblies.Add("System.Xml.dll")
        Param.ReferencedAssemblies.Add(Path.Combine(ProgramDir, "Reference Assemblies\Microsoft\Framework\v3.5\System.Core.dll"))
        Param.ReferencedAssemblies.Add(Path.Combine(ProgramDir, "Reference Assemblies\Microsoft\Framework\v3.5\System.Data.DataSetExtensions.dll"))
        Param.ReferencedAssemblies.Add(Path.Combine(ProgramDir, "Reference Assemblies\Microsoft\Framework\v3.5\System.Xml.Linq.dll"))

        Dim DXKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\DevExpress\IDETools\V9.2\")
        Dim RootPath As String = CStr(DXKey.GetValue("RootDirectory", ""))
        Param.ReferencedAssemblies.Add(Path.Combine(RootPath, "IDETools\System\DXCore\BIN\DevExpress.CodeRush.Common.dll"))
        Param.ReferencedAssemblies.Add(Path.Combine(RootPath, "IDETools\System\DXCore\BIN\DevExpress.CodeRush.Controls.dll"))
        Param.ReferencedAssemblies.Add(Path.Combine(RootPath, "IDETools\System\DXCore\BIN\DevExpress.CodeRush.Core.dll"))
        Param.ReferencedAssemblies.Add(Path.Combine(RootPath, "IDETools\System\DXCore\BIN\DevExpress.CodeRush.Extensions.dll"))
        Param.ReferencedAssemblies.Add(Path.Combine(RootPath, "IDETools\System\DXCore\BIN\DevExpress.CodeRush.StructuralParser.dll"))
        ' Add Custom References
        For Each Reference In Split(CodeCombo.References, Environment.NewLine)
            If Not Param.ReferencedAssemblies.Contains(Reference) _
            AndAlso Reference.Trim <> String.Empty _
            AndAlso File.Exists(Reference) Then
                Param.ReferencedAssemblies.Add(Reference)
            End If
        Next
        'For Each Ass In System.Reflection.Assembly.GetExecutingAssembly().GetReferencedAssemblies.Take(1)
        '    If Ass.Name <> "mscorlib" Then
        '        If Not TryAddPath(param, Ass, Path.Combine(ProgramFiles, "Reference Assemblies\Microsoft\Framework\v3.0\" & Ass.Name & ".dll")) Then
        '            If Not TryAddPath(param, Ass, Path.Combine(ProgramFiles, "Reference Assemblies\Microsoft\Framework\v3.5\" & Ass.Name & ".dll")) Then
        '                Call param.ReferencedAssemblies.Add(Ass.Name & ".dll")
        '            End If
        '        End If
        '    End If
        'Next
    End Sub
    Private Shared Function TryAddPath(ByVal param As CodeDom.CompilerParameters, ByVal Ass As System.Reflection.AssemblyName, ByVal TryPath As String) As Boolean
        If File.Exists(TryPath) Then
            param.ReferencedAssemblies.Add(Ass.Name & ".dll")
            Return True
        End If
    End Function

    Private Shared Function ReportErrors(ByVal cr As CodeDom.CompilerResults) As List(Of String)
        Dim Errors As New List(Of String)
        Dim es As CodeDom.CompilerErrorCollection = cr.Errors
        For Each s As CodeDom.CompilerError In es
            Errors.Add(s.ErrorText)
        Next
        Return Errors
    End Function
End Class
