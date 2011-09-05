Imports System.IO

Public Module IO
    Public Function GetUniqueFileName(ByVal RootFolder As DirectoryInfo, _
                                      ByVal FileBase As String) As String
        Dim Mantissa As String = Path.GetFileNameWithoutExtension(FileBase)
        Dim Extension As String = Path.GetExtension(FileBase)

        Dim Count As Integer = 0
        Dim FileName As String

        Do
            Count += 1
            FileName = String.Format("{0}{1}{2}", Mantissa, Count, Extension)
        Loop Until RootFolder.GetFiles(FileName).Count = 0
        Return FileName
    End Function

End Module
