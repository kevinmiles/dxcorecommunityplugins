Imports System.Text
Imports System.IO
Public Module Text
    Public Function SetTextinFile(ByVal File As FileInfo, ByVal [String] As String, ByVal Encoding As System.Text.Encoding) As Boolean
        Dim Stream1 As FileStream = File.Open(System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.Write)
        Dim sw As New StreamWriter(Stream1, Encoding)
        Dim Result As Boolean
        Try
            sw.Write([String])
            Result = True
        Catch
            Result = False
        Finally
            sw.Flush()
            sw.Close()
        End Try
        Return Result
    End Function
    Public Function GetTextinFile(ByVal File As FileInfo) As String
        Dim sr As StreamReader = File.OpenText()
        Dim Result As String
        Result = sr.ReadToEnd()
        sr.Close()
        Return Result
    End Function

End Module
