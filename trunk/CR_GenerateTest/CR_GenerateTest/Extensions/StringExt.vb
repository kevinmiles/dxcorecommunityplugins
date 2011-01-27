Imports DevExpress.CodeRush.Core
Imports System.Runtime.CompilerServices

Public Module StringExt
    <Extension()> _
    Public Function WrapInField(ByVal Value As String) As String
        Return ExpansionBuilder.GetTextCommand("FieldStart") & Value & ExpansionBuilder.GetTextCommand("FieldEnd")
    End Function
    <Extension()> _
    Public Function WrapInSelection(ByVal Value As String) As String
        Return ExpansionBuilder.Caret & Value & ExpansionBuilder.BlockAnchor
    End Function
    <Extension()> _
    Public Function Add_Test(ByVal Name As String) As String
        Return String.Format("{0}_Test", Name)
    End Function
    <Extension()> _
    Public Function Add_Tests(ByVal Name As String) As String
        Return String.Format("{0}_Tests", Name)
    End Function
End Module