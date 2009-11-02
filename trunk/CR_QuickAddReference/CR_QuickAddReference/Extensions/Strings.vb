Imports System.Runtime.CompilerServices
Public Module Strings
    <Extension()> _
    Public Function [In](ByVal Source As String, ByVal ParamArray Strings() As String) As Boolean
        Return Strings.Contains(Source)
    End Function
End Module
