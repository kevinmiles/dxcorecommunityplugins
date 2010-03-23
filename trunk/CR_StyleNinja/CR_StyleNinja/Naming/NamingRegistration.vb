Imports System.ComponentModel

Module NamingRegistration
    Public Sub RegisterNamingRulesAndFixes(ByVal c As IContainer)
        RegisterNamingRules(c)
        RegisterRefactorings(c)
    End Sub
    Friend Sub RegisterNamingRules(ByVal c As IContainer)
        c.CreateIssue("SA1302", AddressOf Qualifies_SA1302, Message_SA1302, SourceTypeEnum.Interface)
        'Call CreateIssue("SA1303", AddressOf SA1303_ConstantFieldsStartWithUpperCase_CheckCodeIssues)

        ' Rules: Require Uppercase 
        c.CreateIssue("SA1300", AddressOf Qualifies_SA1300, Message_SA1300, SourceTypeEnum.MainElement)
        c.CreateIssue("SA1304", AddressOf Qualifies_SA1304, Message_SA1304, SourceTypeEnum.Field)
        c.CreateIssue("SA1307", AddressOf Qualifies_SA1307, Message_SA1307, SourceTypeEnum.Field)

        ' Rules: Require Lowercase
        c.CreateIssue("SA1306", AddressOf Qualifies_SA1306, Message_SA1306, SourceTypeEnum.Field)

        c.CreateIssue("SA1305", AddressOf Qualifies_SA1305, Message_SA1305, SourceTypeEnum.Variable)


        'Rules: Prefixes and Underscores.
        c.CreateIssue("SA1308", AddressOf Qualifies_SA1308, Message_SA1308, SourceTypeEnum.Field)
        c.CreateIssue("SA1309", AddressOf Qualifies_SA1309, Message_SA1309, SourceTypeEnum.Field)
        c.CreateIssue("SA1310", AddressOf Qualifies_SA1310, Message_SA1310, SourceTypeEnum.Field)


        c.CreateIssue("Locals start with...", AddressOf Qualifies_LocalWithPoorPrefix, Message_LocalsShouldStart, SourceTypeEnum.Local)
        c.CreateIssue("Fields start with...", AddressOf Qualifies_FieldWithPoorPrefix, Message_FieldsShouldStart, SourceTypeEnum.Field)
        c.CreateIssue("Parameters start with...", AddressOf Qualifies_ParamWithPoorPrefix, Message_ParamsShouldStart, SourceTypeEnum.Param)
    End Sub

End Module
