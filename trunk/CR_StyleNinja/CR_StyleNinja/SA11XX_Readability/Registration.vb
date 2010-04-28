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
            C.CreateIssue(Message_SA1100, AddressOf Qualifies_SA1100, GetType(BaseReferenceExpression))
            C.CreateIssue(Message_SA1101, AddressOf Qualifies_SA1101, SourceTypeEnum.MethodCall)
        End Sub
        Private Sub RegisterFixes(ByVal C As IContainer)
            ' SA1100
            C.CreateRefactoring("Remove Base Reference",
                                "Remove Base Reference",
                                AddressOf Fix_SA1100,
                                AddressOf Available_SA1100).SolvedIssues.Add(Message_SA1100)

            ' SA1100
            C.CreateRefactoring("Add self Reference",
                                "Add self Reference",
                                AddressOf Fix_SA1100,
                                AddressOf Available_SA1100).SolvedIssues.Add(Message_SA1100)

        End Sub
    End Module
End Namespace
