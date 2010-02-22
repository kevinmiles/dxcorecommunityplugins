Imports DevExpress.CodeRush.StructuralParser

Public Interface IStatementMover
    Sub MoveStatementUp(ByVal FirstNodeOnLine As LanguageElement)
    Sub MoveStatementDown(ByVal FirstNodeOnLine As LanguageElement)
    Sub MoveStatementLeft(ByVal Statement As LanguageElement)
    Sub MoveStatementRight(ByVal Statement As LanguageElement)
End Interface
Public Interface IMemberMover
    Sub MoveMemberUp(ByVal Member As LanguageElement)
    Sub MoveMemberDown(ByVal Member As LanguageElement)
End Interface
Public Interface ISelectionMover
    Sub MoveSelectionUp(ByVal Selection As SourceRange)
    Sub MoveSelectionDown(ByVal Selection As SourceRange)
    Sub MoveSelectionLeft(ByVal Selection As SourceRange)
    Sub MoveSelectionRight(ByVal Selection As SourceRange)
End Interface
