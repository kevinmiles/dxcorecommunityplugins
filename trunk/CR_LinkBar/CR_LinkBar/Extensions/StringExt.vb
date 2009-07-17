Imports System.Runtime.CompilerServices
Public Module StringExt
    <Extension()> _
    Public Function DefaultTo(ByVal Source As String, ByVal Defaultvalue As String) As String
        Return If(Source = String.Empty, Defaultvalue, Source)
    End Function
End Module