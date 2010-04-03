Imports System.ComponentModel
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Core
Namespace SA11XX
    Module Registration
        'Public Sub RegisterRulesAndFixes(ByVal C As IContainer)
        '    RegisterRules(C)
        '    RegisterFixes(C)
        'End Sub
        'Private Sub RegisterRules(ByVal C As IContainer)
        '    C.CreateIssue("SA1400 - Must Declare Access Modifier.", AddressOf Qualifies_SA1400, Message_SA1400, SourceTypeEnum.VisibleItems)
        '    C.CreateIssue("SA1401 - Field Must be Private.", AddressOf Qualifies_SA1401, Message_SA1401, SourceTypeEnum.Field)
        '    C.CreateIssue("SA1402 - One Class to a File.", AddressOf Qualifies_SA1402, Message_SA1402, SourceTypeEnum.Class)
        '    C.CreateIssue("SA1403 - One Namespace to a File.", AddressOf Qualifies_SA1403, Message_SA1403, SourceTypeEnum.Attribute)
        '    C.CreateIssue("SA1404 - Must Justify Suppression.", AddressOf Qualifies_SA1404, Message_SA1404, SourceTypeEnum.Attribute)
        '    C.CreateIssue("SA1405 - Must Provide Descriptive Message.", AddressOf Qualifies_SA1405, Message_SA1405, SourceTypeEnum.MethodCall)
        '    C.CreateIssue("SA1409 - Unnecessary Try..X Construct", AddressOf Qualifies_SA1409, Message_SA1409, SourceTypeEnum.Try)
        'End Sub
        'Private Sub RegisterFixes(ByVal C As IContainer)
        '    ' SA1400
        '    C.CreateRefactoring("MakeVisibilityExplicit", "Make Visibility Explicit", AddressOf Fix_SA1400, AddressOf SA1400_Available).SolvedIssues.Add(Message_SA1400)
        '    ' SA1401
        '    C.CreateCodeProvider("MakeFieldPrivate", "Make Field Private", AddressOf Fix_SA1401, AddressOf Available_SA1401).SolvedIssues.Add(Message_SA1401)
        '    ' SA1409
        '    C.CreateCodeProvider("RemoveTryX", "Remove Try..X", AddressOf Fix_SA1409, AddressOf Available_SA1409).SolvedIssues.Add(Message_SA1409)
        'End Sub
    End Module
End Namespace
