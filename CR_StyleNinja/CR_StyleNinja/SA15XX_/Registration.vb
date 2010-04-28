Imports System.ComponentModel
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Core
Namespace SA15XX
    Module Registration
        Public Sub RegisterRulesAndFixes(ByVal C As IContainer)
            RegisterRules(C)
            RegisterFixes(C)
        End Sub
        Private Sub RegisterRules(ByVal C As IContainer)
            'C.CreateIssue(Message_SA1507, AddressOf Qualifies_SA1507, GetType(Line))
        End Sub
        Private Sub RegisterFixes(ByVal C As IContainer)
            ' SA1507
            'C.CreateRefactoring("Remove Blank Line",
            '                    "Remove Blank Line",
            '                    AddressOf Fix_SA1507,
            '                    AddressOf Available_SA1507).SolvedIssues.Add(Message_SA1507)

        End Sub
    End Module
End Namespace
