Imports DevExpress.CodeRush.StructuralParser

Public Module DXtras
    Public Function StartOfLine(ByVal Point As SourcePoint, Optional ByVal LineOffset As Integer = 0) As SourcePoint
        Return New SourcePoint(Point.Line + LineOffset, 1)
    End Function
End Module
