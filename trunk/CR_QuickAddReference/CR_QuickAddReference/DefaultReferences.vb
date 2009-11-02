Imports System.IO
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Diagnostics.Core

Public Class DefaultReferences
#Region "Constants"
    Private Const AssembliesCommon As String = "%ProgramFiles%\Common Files\Microsoft Shared\MSEnv\PublicAssemblies\"
    Private Const AssembliesV35 As String = "%ProgramFiles%\Reference Assemblies\Microsoft\Framework\v3.5\"
    Private Const AssembliesV20 As String = "%WinDir%\Microsoft.Net\Framework\v2.0.50727\"
    Private Const AssembliesDXCore As String = "%ProgramFiles%\DevExpress 2009.1\IDETools\System\DXCore\BIN\"
    Private Const AssembliesMVC1 As String = "%ProgramFiles%\Microsoft ASP.NET\ASP.NET MVC 1.0\Assemblies\"
    Private Const AssembliesMVC2 As String = "%ProgramFiles%\Microsoft ASP.NET\ASP.NET MVC 2\Assemblies\"
#End Region
    Private Shared mDefaultReferences As New Dictionary(Of String, List(Of Reference))
    Shared Sub New()
        mDefaultReferences("Common") = DefaultReferencesCommon()
        mDefaultReferences("Win") = DefaultReferencesWin()
        mDefaultReferences("Web") = DefaultReferencesWeb()
        mDefaultReferences("DXCore") = DefaultReferencesDXCore()
    End Sub
    Public Shared Function GetTabReferenceDefaults(ByVal TabName As String) As List(Of Reference)
        Dim Defaults As List(Of Reference) = mDefaultReferences(TabName)
        If Defaults Is Nothing Then
            Defaults = New List(Of Reference)
        End If
        Return Defaults
    End Function
    Public Shared Function GACFolders() As List(Of DirectoryInfo)
        Dim Result As New List(Of DirectoryInfo)
        Result.Add(New DirectoryInfo(AssembliesV35))
        Result.Add(New DirectoryInfo(AssembliesV20))
        Return Result
    End Function

#Region "DefaultReferencesCommon"
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
#End Region
#Region "DefaultReferencesMVC"
    Public Shared Function DefaultReferencesMVC() As List(Of Reference)
        Dim Result As New List(Of Reference)
        Result.Add(New Reference(AssembliesMVC1 & "System.MVC.dll", "System.MVC.dll (v1.0)"))
        Result.Add(New Reference(AssembliesMVC2 & "System.MVC.dll", "System.MVC.dll (v2)"))
        Return Result
    End Function
#End Region
#Region "DefaultReferencesWeb"
    Public Shared Function DefaultReferencesWeb() As List(Of Reference)
        Dim Result As New List(Of Reference)
        'Result.Add(New Reference(v20 & "System.dll"))
        Result.Add(New Reference(AssembliesV20 & "System.Web.dll"))
        Result.Add(New Reference(AssembliesV35 & "System.Net.dll"))
        Result.Add(New Reference(AssembliesV35 & "System.Web.Extensions.dll"))
        Result.Add(New Reference(AssembliesV20 & "System.Web.Services.dll"))
        Return Result
    End Function
#End Region
#Region "DefaultReferencesWin"
    Public Shared Function DefaultReferencesWin() As List(Of Reference)
        Dim Result As New List(Of Reference)
        'Result.Add(New Reference(v20 & "System.dll"))
        Result.Add(New Reference(AssembliesV20 & "System.Drawing.dll"))
        Result.Add(New Reference(AssembliesV20 & "System.Windows.Forms.dll"))
        Return Result
    End Function
#End Region
#Region "DefaultReferencesDXCore"
    Public Shared Function DefaultReferencesDXCore() As List(Of Reference)
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
#End Region
#Region "GetSolutionReferences"
    Public Shared Function GetSolutionReferences() As List(Of Reference)
        Dim References As New List(Of Reference)
        For Each Project As ProjectElement In CodeRush.Source.ActiveSolution.AllProjects
            For Each AssemblyReference As AssemblyReference In Project.AssemblyReferences
                Dim Reference As Reference = New Reference(AssemblyReference)
                Try
                    References.Add(Reference)
                Catch ex As Exception
                    LogReferenceAddFailure(Reference)
                End Try
            Next
        Next
        Return References
    End Function
#End Region
#Region "GetRecentReferences"
    'Public Shared Function GetRecentReferences() As List(Of Reference)
    '    Dim References As New List(Of Reference)
    '    For Each item In OptionsQuickAddReference.Storage.ReadStrings(OptionsQuickAddReference.SECTION_QUICKADD, "MRU")
    '        References.Add(New Reference(item))
    '    Next
    '    Return References
    'End Function
    Public Shared Function GetNoReferences() As List(Of Reference)
        Return New List(Of Reference)
    End Function
#End Region

End Class
