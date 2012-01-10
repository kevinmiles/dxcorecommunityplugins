namespace CR_StyleCop
{
    using System;
    using System.Collections.Generic;
    using CR_StyleCop.CodeIssues;
    using StyleCop;

    internal class CodeIssueFactory
    {
        private Dictionary<string, IStyleCopRule> handlers = new Dictionary<string, IStyleCopRule>();
        private IStyleCopRule emptyRule = new StyleCopRule(new NullIssueLocator());

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
            this.handlers.Add("SA1633", emptyRule); // TODO
            this.handlers.Add("SA1634", emptyRule); // TODO
            this.handlers.Add("SA1635", emptyRule); // TODO
            this.handlers.Add("SA1636", emptyRule); // TODO
            this.handlers.Add("SA1637", emptyRule); // TODO
            this.handlers.Add("SA1638", emptyRule); // TODO
            this.handlers.Add("SA1639", emptyRule); // TODO
            this.handlers.Add("SA1640", emptyRule); // TODO
            this.handlers.Add("SA1641", emptyRule); // TODO
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
            this.handlers.Add("SA1407", new SA1407_ArithmeticExpressionsMustDeclarePrecedence()); // OK
            this.handlers.Add("SA1408", new SA1408_ConditionalExpressionsMustDeclarePrecedence()); // OK
            this.handlers.Add("SA1409", new SA1409_RemoveUnnecessaryCode()); // problem with 2 use cases
            this.handlers.Add("SA1410", new SA1410_RemoveDelegateParenthesisWhenPossible()); // OK

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

            this.handlers.Add("SA1200", new SA1200_UsingDirectivesMustBePlacedWithinNamespace()); // OK
            this.handlers.Add("SA1201", new SA1201_ElementsMustAppearInTheCorrectOrder()); // OK
            this.handlers.Add("SA1202", new SA1202_ElementsMustBeOrderedByAccess()); // OK
            this.handlers.Add("SA1203", new SA1203_ConstantsMustAppearBeforeFields()); // OK
            this.handlers.Add("SA1204", new SA1204_StaticElementsMustAppearBeforeInstanceElements()); // OK
            this.handlers.Add("SA1205", new SA1205_PartialElementsMustDeclareAccess()); // OK
            this.handlers.Add("SA1206", new SA1206_DeclarationKeywordsMustFollowOrder()); // OK
            this.handlers.Add("SA1207", new SA1207_ProtectedMustComeBeforeInternal()); // OK
            this.handlers.Add("SA1208", new SA1208_SystemUsingDirectivesMustBePlacedBeforeOtherUsingDirectives()); // OK
            this.handlers.Add("SA1209", new SA1209_UsingAliasDirectivesMustBePlacedAfterOtherUsingDirectives()); // OK
            this.handlers.Add("SA1210", new SA1210_UsingDirectivesMustBeOrderedAlphabeticallyByNamespace()); // OK
            this.handlers.Add("SA1211", new SA1211_UsingAliasDirectivesMustBeOrderedAlphabeticallyByAliasName()); // OK
            this.handlers.Add("SA1212", new SA1212_PropertyAccessorsMustFollowOrder()); // OK
            this.handlers.Add("SA1213", new SA1213_EventAccessorsMustFollowOrder()); // OK

            this.handlers.Add("SA1100", new SA1100_DoNotPrefixCallsWithBaseUnlessLocalImplementationExists());
            this.handlers.Add("SA1101", new SA1101_PrefixLocalCallsWithThis());
            this.handlers.Add("SA1102", new SA1102_QueryClauseMustFollowPreviousClause());
            this.handlers.Add("SA1103", new SA1103_QueryClausesMustBeOnSeparateLinesOrAllOnOneLine());
            this.handlers.Add("SA1104", new SA1104_QueryClauseMustBeginOnNewLineWhenPreviousClauseSpansMultipleLines());
            this.handlers.Add("SA1105", new SA1105_QueryClausesSpanningMultipleLinesMustBeginOnOwnLine());
            this.handlers.Add("SA1106", new SA1106_CodeMustNotContainEmptyStatements());
            this.handlers.Add("SA1107", emptyRule);
            this.handlers.Add("SA1108", new SA1108_BlockStatementsMustNotContainEmbeddedComments());
            this.handlers.Add("SA1109", new SA1109_BlockStatementsMustNotContainEmbeddedRegions());
            this.handlers.Add("SA1110", new SA1110_OpeningParenthesisMustBeOnDeclarationLine());
            this.handlers.Add("SA1111", new SA1111_ClosingParenthesisMustBeOnLineOfLastParameter());
            this.handlers.Add("SA1112", new SA1112_ClosingParenthesisMustBeOnLineOfOpeningParenthesis());
            this.handlers.Add("SA1113", new SA1113_CommaMustBeOnSameLineAsPreviousParameter());
            this.handlers.Add("SA1114", new SA1114_ParameterListMustFollowDeclaration());
            this.handlers.Add("SA1115", new SA1115_ParameterMustFollowComma());
            this.handlers.Add("SA1116", new SA1116_SplitParametersMustStartOnLineAfterDeclaration());

            this.handlers.Add("SA1119", new SA1119_StatementMustNotUseUnnecessaryParenthesis());

            this.handlers.Add("SA1000", new SA1000_KeywordsMustBeSpacedCorrectly());
            this.handlers.Add("SA1001", new SA1001_CommasMustBeSpacedCorrectly());
            this.handlers.Add("SA1002", new SA1002_SemicolonsMustBeSpacedCorrectly());
            this.handlers.Add("SA1003", new SA1003_SymbolsMustBeSpacedCorrectly());
            this.handlers.Add("SA1004", new SA1004_DocumentationLinesMustBeginWithSingleSpace());
            this.handlers.Add("SA1005", new SA1005_SingleLineCommentsMustBeginWithSingleSpace());
            this.handlers.Add("SA1006", new SA1006_PreprocessorKeywordsMustNotBePrecededBySpace());
            this.handlers.Add("SA1007", new SA1007_OperatorKeywordMustBeFollowedBySpace());
            this.handlers.Add("SA1008", new SA1008_OpeningParenthesisMustBeSpacedCorrectly());
            this.handlers.Add("SA1009", new SA1009_ClosingParenthesisMustBeSpacedCorrectly());
            this.handlers.Add("SA1018", new SA1018_NullableTypeSymbolsMustNotBePrecededBySpace());
        }

        public IStyleCopRule GetRuleFor(Violation violation)
        {
            IStyleCopRule rule = null;
            this.handlers.TryGetValue(violation.Rule.CheckId, out rule);
            return rule ?? this.emptyRule;
        }
    }
}
