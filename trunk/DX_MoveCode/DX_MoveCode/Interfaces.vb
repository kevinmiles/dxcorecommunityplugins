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
