Public Class DefaultReferences
    Private Shared mDefaultReferences As New Dictionary(Of String, List(Of Reference))
    Shared Sub New()
        mDefaultReferences("Common") = DefaultReferencesCommon()
        mDefaultReferences("Win") = DefaultReferencesWin()
        mDefaultReferences("Web") = DefaultReferencesWeb()
    End Sub
    Public Shared Function GetTabDefaults(ByVal TabName As String) As List(Of Reference)
        mDefaultReferences("Common") = DefaultReferencesCommon()
        Return mDefaultReferences(TabName)
    End Function

    Public Const v35 As String = "%ProgramFiles%\Reference Assemblies\Microsoft\Framework\v3.5\"
    Public Const v20 As String = "%WinDir%\Microsoft.Net\Framework\v2.0.50727\"
    Public Shared Function DefaultReferencesCommon() As List(Of Reference)
        Dim Result As New List(Of Reference)
        'Result.Add(New Reference(v20 & "System.dll"))
        Result.Add(New Reference(v20 & "System.Configuration.dll"))
        Result.Add(New Reference(v20 & "System.Data.dll"))
        Result.Add(New Reference(v20 & "System.Xml.dll"))
        Result.Add(New Reference(v35 & "System.Data.DataSetExtensions.dll"))
        Result.Add(New Reference(v35 & "System.Xml.Linq.dll"))
        Return Result
    End Function
    Public Shared Function DefaultReferencesWin() As List(Of Reference)
        Dim Result As New List(Of Reference)
        'Result.Add(New Reference(v20 & "System.dll"))
        Result.Add(New Reference(v20 & "System.Drawing.dll"))
        Result.Add(New Reference(v20 & "System.Windows.Forms.dll"))
        Return Result
    End Function
    Public Shared Function DefaultReferencesWeb() As List(Of Reference)
        Dim Result As New List(Of Reference)
        'Result.Add(New Reference(v20 & "System.dll"))
        Result.Add(New Reference(v20 & "System.Web.dll"))
        Result.Add(New Reference(v35 & "System.Net.dll"))
        Result.Add(New Reference(v35 & "System.Web.Extensions.dll"))
        Result.Add(New Reference(v20 & "System.Web.Services.dll"))
        Return Result
    End Function
End Class
