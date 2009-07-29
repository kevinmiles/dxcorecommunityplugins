Imports System
Imports System.IO
Public Module IOHelpers
    Public Sub SetTextInFile(ByVal Name As String, ByVal FileText As String)
        Dim WriteStream As FileStream = New FileStream(Name, FileMode.Create, FileAccess.Write, FileShare.Write)
        Dim TextWriter As New StreamWriter(WriteStream)
        TextWriter.Write(FileText.ToCharArray)
        TextWriter.Flush()
        TextWriter.Close()
        WriteStream.Close()
    End Sub
    Public Function GetTextInFile(ByVal Name As String) As String
        Dim ReadStream As FileStream = New FileStream(Name, FileMode.Open, FileAccess.Read, FileShare.Read)
        Dim TextReader As New StreamReader(ReadStream)
        GetTextInFile = TextReader.ReadToEnd
        TextReader.Close()
        ReadStream.Close()
    End Function

End Module
