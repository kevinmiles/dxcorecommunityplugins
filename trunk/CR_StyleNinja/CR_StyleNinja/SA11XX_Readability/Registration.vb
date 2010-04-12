Imports System.ComponentModel
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Core
Namespace SA11XX
    Module Registration
        Public Sub RegisterRulesAndFixes(ByVal C As IContainer)
            RegisterRules(C)
            RegisterFixes(C)
        End Sub
        Private Sub RegisterRules(ByVal C As IContainer)
            C.CreateIssue(Message_SA1100, AddressOf Qualifies_SA1100, SourceTypeEnum.VisibleItems)
        End Sub
        Private Sub RegisterFixes(ByVal C As IContainer)
            '    ' SA1400
            '    C.CreateRefactoring("MakeVisibilityExplicit", "Make Visibility Explicit", AddressOf Fix_SA1400, AddressOf SA1400_Available).SolvedIssues.Add(Message_SA1400)
        End Sub
    End Module
End Namespace
