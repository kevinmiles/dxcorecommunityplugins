namespace CR_StyleCop
{
    using System;
    using System.Collections.Generic;
    using CR_StyleCop.CodeIssues;
    using Microsoft.StyleCop;

    internal class CodeIssueFactory
    {
        private Dictionary<string, ICodeIssue> handlers = new Dictionary<string, ICodeIssue>();
        private ICodeIssue nullHandler = new NullCodeIssue();

        public CodeIssueFactory()
        {
            this.handlers.Add("SA1600", new SA1600_ElementsMustBeDocumented()); // OK
            this.handlers.Add("SA1601", new SA1601_PartialElementsMustBeDocumented()); // OK
            this.handlers.Add("SA1602", new SA1602_EnumerationItemsMustBeDocumented()); // OK
            this.handlers.Add("SA1603", new SA1603_DocumentationMustContainValidXml()); // OK
            this.handlers.Add("SA1604", new SA1604_ElementDocumentationMustHaveSummary()); // OK
            this.handlers.Add("SA1605", new SA1605_PartialElementDocumentationMustHaveSummary()); // Cannot get this violation with stylecop. Sample needed
            this.handlers.Add("SA1606", new SA1606_ElementDocumentationMustHaveSummaryText()); // OK
            this.handlers.Add("SA1607", new SA1607_PartialElementDocumentationMustHaveSummaryText()); // OK
            this.handlers.Add("SA1608", new SA1608_ElementDocumentationMustNotHaveDefaultSummary()); // OK
            this.handlers.Add("SA1609", new SA1609_PropertyDocumentationMustHaveValue()); // OK
            this.handlers.Add("SA1610", new SA1610_PropertyDocumentationMustHaveValueText()); // Cannot get this violation with stylecop. Sample needed
            this.handlers.Add("SA1611", new SA1611_ElementParametersMustBeDocumented()); // OK
            this.handlers.Add("SA1612", new SA1612_ElementParameterDocumentationMustMatchElementParameters()); // OK
            this.handlers.Add("SA1613", new SA1613_ElementParameterDocumentationMustDeclareParameterName()); // OK
            this.handlers.Add("SA1614", new SA1614_ElementParameterDocumentationMustHaveText()); // OK
            this.handlers.Add("SA1615", new SA1615_ElementReturnValueMustBeDocumented()); // OK
            this.handlers.Add("SA1616", new SA1616_ElementReturnValueDocumentationMustHaveText()); // OK
            this.handlers.Add("SA1617", new SA1617_VoidReturnValueMustNotBeDocumented()); // OK
            this.handlers.Add("SA1618", new SA1618_GenericTypeParametersMustBeDocumented()); // OK
            this.handlers.Add("SA1619", new SA1619_GenericTypeParametersMustBeDocumentedPartialClass()); // OK
            this.handlers.Add("SA1620", new SA1620_GenericTypeParameterDocumentationMustMatchTypeParameters()); // OK
            this.handlers.Add("SA1621", new SA1621_GenericTypeParameterDocumentationMustDeclareParameterName()); // OK
            this.handlers.Add("SA1622", new SA1622_GenericTypeParameterDocumentationMustHaveText()); // OK
            this.handlers.Add("SA1623", new SA1623_PropertySummaryDocumentationMustMatchAccessors()); // OK
            this.handlers.Add("SA1624", new SA1624_PropertySummaryDocumentationMustOmitSetAccessorWithRestrictedAccess()); // OK
            this.handlers.Add("SA1625", new SA1625_ElementDocumentationMustNotBeCopiedAndPasted()); // OK
            this.handlers.Add("SA1626", new SA1626_SingleLineCommentsMustNotUseDocumentationStyleSlashes()); // OK
            this.handlers.Add("SA1627", new SA1627_DocumentationTextMustNotBeEmpty()); // Cannot get this violation with stylecop. Sample needed
            this.handlers.Add("SA1628", new SA1628_DocumentationTextMustBeginWithACapitalLetter()); // OK
            this.handlers.Add("SA1629", new SA1629_DocumentationTextMustEndWithAPeriod()); // OK
            this.handlers.Add("SA1630", new SA1630_DocumentationTextMustContainWhitespace()); // OK
            this.handlers.Add("SA1631", new SA1631_DocumentationTextMustMeetCharacterPercentage()); // OK
            this.handlers.Add("SA1632", new SA1632_DocumentationTextMustMeetMinimumCharacterLength()); // OK
            this.handlers.Add("SA1633", nullHandler); // TODO
            this.handlers.Add("SA1634", nullHandler); // TODO
            this.handlers.Add("SA1635", nullHandler); // TODO
            this.handlers.Add("SA1636", nullHandler); // TODO
            this.handlers.Add("SA1637", nullHandler); // TODO
            this.handlers.Add("SA1638", nullHandler); // TODO
            this.handlers.Add("SA1639", nullHandler); // TODO
            this.handlers.Add("SA1640", nullHandler); // TODO
            this.handlers.Add("SA1641", nullHandler); // TODO
            this.handlers.Add("SA1642", new SA1642_ConstructorSummaryDocumentationMustBeginWithStandardText()); // OK
            this.handlers.Add("SA1643", new SA1643_DestructorSummaryDocumentationMustBeginWithStandardText()); // OK
            this.handlers.Add("SA1644", new SA1644_DocumentationHeadersMustNotContainBlankLines()); // OK
            this.handlers.Add("SA1645", new SA1645_IncludedDocumentationFileDoesNotExist()); // OK
            this.handlers.Add("SA1646", new SA1646_IncludedDocumentationXPathDoesNotExist()); // OK
            this.handlers.Add("SA1647", new SA1647_IncludeNodeDoesNotContainValidFileAndPath()); // OK
            this.handlers.Add("SA1500", new SA1500_CurlyBracketsForMultiLineStatementsMustNotShareLine()); // OK
            this.handlers.Add("SA1501", new SA1501_StatementMustNotBeOnASingleLine()); // OK
            this.handlers.Add("SA1502", new SA1502_ElementMustNotBeOnASingleLine()); // OK
            this.handlers.Add("SA1503", new SA1503_CurlyBracketsMustNotBeOmitted()); // TODO
            this.handlers.Add("SA1504", new SA1504_AllAccessorsMustBeSingleLineOrMultiLine()); // BUG
            this.handlers.Add("SA1505", new SA1505_OpeningCurlyBracketsMustNotBeFollowedByBlankLine()); // OK
            this.handlers.Add("SA1506", new SA1506_ElementDocumentationHeadersMustNotBeFollowedByBlankLine()); // DxCore bug
            this.handlers.Add("SA1507", new SA1507_CodeMustNotContainMultipleBlankLinesInARow()); // OK
            this.handlers.Add("SA1508", new SA1508_ClosingCurlyBracketsMustNotBePrecededByBlankLine()); // OK
            this.handlers.Add("SA1509", new SA1509_OpeningCurlyBracketsMustNotBePrecededByBlankLine()); // OK
            this.handlers.Add("SA1510", new SA1510_ChainedStatementBlocksMustNotBePrecededByBlankLine()); // OK
            this.handlers.Add("SA1511", new SA1511_WhileDoFooterMustNotBePrecededByBlankLine()); // OK
            this.handlers.Add("SA1512", new SA1512_SingleLineCommentsMustNotBeFollowedByBlankLine()); // OK
            this.handlers.Add("SA1513", new SA1513_ClosingCurlyBracketsMustNotBeFollowedByBlankLine()); // Cannot get this violation with stylecop. Sample needed
            this.handlers.Add("SA1514", new SA1514_ElementDocumentationHeadersMustBePrecededByBlankLine()); // TO Check when debugging will be available
            this.handlers.Add("SA1515", new SA1515_SingleLineCommentsMustBePrecededByBlankLine()); // OK
            this.handlers.Add("SA1516", new SA1516_ElementsMustBeSeparatedByBlankLine()); // OK
            this.handlers.Add("SA1400", new SA1400_AccessModifierMustBeDeclared()); // OK
            this.handlers.Add("SA1401", new SA1401_FieldsMustBePrivate()); // OK
            this.handlers.Add("SA1402", new SA1402_FileMayOnlyContainASingleClass()); // OK
            this.handlers.Add("SA1403", new SA1403_FileMayOnlyContainASingleNamespace()); // OK
            this.handlers.Add("SA1404", new SA1404_CodeAnalysisSuppressionMustHaveJustification()); // OK
            this.handlers.Add("SA1405", new SA1405_DebugAssertMustProvideMessageText()); // OK
            this.handlers.Add("SA1406", new SA1406_DebugFailMustProvideMessageText()); // OK
            this.handlers.Add("SA1300", new SA1300_ElementMustBeginWithUpperCaseLetter()); // OK
            this.handlers.Add("SA1301", new SA1301_ElementMustBeginWithLowerCaseLetter()); // Never reported according to StyleCop help
            this.handlers.Add("SA1302", new SA1302_InterfaceNamesMustBeginWithI()); // OK
            this.handlers.Add("SA1303", new SA1303_ConstFieldNamesMustBeginWithUpperCaseLetter()); // OK
            this.handlers.Add("SA1304", new SA1304_NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter()); // OK
            this.handlers.Add("SA1305", new SA1305_FieldNamesMustNotUseHungarianNotation()); // OK
            this.handlers.Add("SA1306", new SA1306_FieldNamesMustBeginWithLowerCaseLetter()); // OK
            this.handlers.Add("SA1307", new SA1307_AccessibleFieldsMustBeginWithUpperCaseLetter()); // OK
            this.handlers.Add("SA1308", new SA1308_VariableNamesMustNotBePrefixed()); // OK
            this.handlers.Add("SA1309", new SA1309_FieldNamesMustNotBeginWithUnderscore()); // OK
            this.handlers.Add("SA1310", new SA1310_FieldNamesMustNotContainUnderscore()); // OK
            this.handlers.Add("SA1119", new SA1119_StatementMustNotUseUnnecessaryParenthesis());
        }

        public ICodeIssue GetIssueFor(Violation violation)
        {
            ICodeIssue handler = null;
            this.handlers.TryGetValue(violation.Rule.CheckId, out handler);
            return handler ?? this.nullHandler;
        }
    }
}
