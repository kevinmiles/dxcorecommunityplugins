Imports DevExpress.CodeRush.StructuralParser

Public Interface IStatementMover
    Function MoveStatementUp(ByVal FirstNodeOnLine As LanguageElement) As SourceRange
    Function MoveStatementDown(ByVal FirstNodeOnLine As LanguageElement) As SourceRange
    Function MoveStatementLeft(ByVal Statement As LanguageElement) As SourceRange
    Function MoveStatementRight(ByVal Statement As LanguageElement) As SourceRange
End Interface
Public Interface IMemberMover
    Function MoveMemberUp(ByVal Member As LanguageElement) As SourceRange
    Function MoveMemberDown(ByVal Member As LanguageElement) As SourceRange
End Interface
Public Interface ISelectionMover
    Function MoveSelectionDown(ByVal Selection As SourceRange) As SourceRange
    Function MoveSelectionUp(ByVal Selection As SourceRange) As SourceRange
    Function MoveSelectionRight(ByVal Selection As SourceRange) As SourceRange
    Function MoveSelectionLeft(ByVal Selection As SourceRange) As SourceRange
End Interface
