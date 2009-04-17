Option Strict On
Public Module Strings
    Private Const ATOZ As String = "ABCDEFGHIJKLMNOPRSTUVWXYZ"
    Private ReadOnly ATOZChars() As Char = ATOZ.ToCharArray
    Public Function SplitOnCase(ByVal VarName As String) As String()
        For Each Character As Char In ATOZChars
            VarName = VarName.Replace(Character, " " & Character)
        Next
        Return VarName.Split(" "c)
    End Function
    Public Function RemoveSpaces(ByVal Text As String) As String
        Return Text.Replace(" "c, "")
    End Function
    Public Function SeperateWords(ByVal Text As String) As String
        Dim Words() As String = SplitOnCase(Text)
        Return Microsoft.VisualBasic.Join(Words, " ")
    End Function

End Module
