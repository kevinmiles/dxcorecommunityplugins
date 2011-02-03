Imports DevExpress.CodeRush.StructuralParser
Public Module DXtras
    Public Sub SetTemplateVariable(ByVal VarName As String, ByVal VarValue As String)
        DevExpress.CodeRush.Core.CodeRush.Strings.Get("Set", String.Format("{0}, {1}", VarName, VarValue))
    End Sub
    Public Function StartOfLine(ByVal Point As SourcePoint, Optional ByVal LineOffset As Integer = 0) As SourcePoint
        Return New SourcePoint(Point.Line + LineOffset, 1)
    End Function
End Module
