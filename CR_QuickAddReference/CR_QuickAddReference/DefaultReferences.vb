Public Class DefaultReferences
    Private Const AssembliesCommon As String = "%ProgramFiles%\Common Files\Microsoft Shared\MSEnv\PublicAssemblies\"
    Private Const AssembliesV35 As String = "%ProgramFiles%\Reference Assemblies\Microsoft\Framework\v3.5\"
    Private Const AssembliesV20 As String = "%WinDir%\Microsoft.Net\Framework\v2.0.50727\"
    Private Const AssembliesDXCore As String = "%ProgramFiles%\DevExpress 2009.1\IDETools\System\DXCore\BIN\"

    Private Shared mDefaultReferences As New Dictionary(Of String, List(Of Reference))
    Shared Sub New()
        mDefaultReferences("Common") = DefaultReferencesCommon()
        mDefaultReferences("Win") = DefaultReferencesWin()
        mDefaultReferences("Web") = DefaultReferencesWeb()
        mDefaultReferences("DXCore") = DefaultReferencesDXCore()
    End Sub
    Public Shared Function GetTabDefaults(ByVal TabName As String) As List(Of Reference)
        Dim Defaults As List(Of Reference) = mDefaultReferences(TabName)
        If Defaults Is Nothing Then
            Defaults = New List(Of Reference)
        End If
        Return Defaults
    End Function
    Public Shared Function DefaultReferencesCommon() As List(Of Reference)
        Dim Result As New List(Of Reference)
        'Result.Add(New Reference(v20 & "System.dll"))
        Result.Add(New Reference(AssembliesV20 & "System.Configuration.dll"))
        Result.Add(New Reference(AssembliesV20 & "System.Data.dll"))
        Result.Add(New Reference(AssembliesV35 & "System.Data.DataSetExtensions.dll"))
        Result.Add(New Reference(AssembliesV20 & "System.Xml.dll"))
        Result.Add(New Reference(AssembliesV35 & "System.Xml.Linq.dll"))
        Return Result
    End Function
    Public Shared Function DefaultReferencesWin() As List(Of Reference)
        Dim Result As New List(Of Reference)
        'Result.Add(New Reference(v20 & "System.dll"))
        Result.Add(New Reference(AssembliesV20 & "System.Drawing.dll"))
        Result.Add(New Reference(AssembliesV20 & "System.Windows.Forms.dll"))
        Return Result
    End Function
    Public Shared Function DefaultReferencesWeb() As List(Of Reference)
        Dim Result As New List(Of Reference)
        'Result.Add(New Reference(v20 & "System.dll"))
        Result.Add(New Reference(AssembliesV20 & "System.Web.dll"))
        Result.Add(New Reference(AssembliesV35 & "System.Net.dll"))
        Result.Add(New Reference(AssembliesV35 & "System.Web.Extensions.dll"))
        Result.Add(New Reference(AssembliesV20 & "System.Web.Services.dll"))
        Return Result
    End Function
    Private Shared Function DefaultReferencesDXCore() As List(Of Reference)
        Dim Result As New List(Of Reference)
        Result.Add(New Reference(AssembliesCommon & "EnvDTE.dll"))
        Result.Add(New Reference(AssembliesCommon & "EnvDTE80.dll"))
        Result.Add(New Reference(AssembliesDXCore & "DevExpress.CodeRush.Common.dll"))
        Result.Add(New Reference(AssembliesDXCore & "DevExpress.CodeRush.Core.dll"))
        Result.Add(New Reference(AssembliesDXCore & "DevExpress.CodeRush.PlugInCore.dll"))
        Result.Add(New Reference(AssembliesDXCore & "DevExpress.CodeRush.StructuralParser.dll"))
        Result.Add(New Reference(AssembliesDXCore & "DevExpress.CodeRush.VSCore.dll"))
        Return Result
    End Function

End Class
