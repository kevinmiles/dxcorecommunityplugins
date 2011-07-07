namespace CR_StyleCop.Tests
{
    using System;
    using System.Linq;
    using MbUnit.Framework;

    public partial class RulesTests
    {
        [Test]
        [CodeIssue(15, 33, 15, 34)]
        [CodeIssue(18, 35, 18, 36)]
        [CodeIssue(19, 33, 19, 34)]
        [CodeIssue(25, 27, 25, 28)]
        [CodeIssue(27, 33, 27, 34)]
        [CodeIssue(28, 27, 28, 28)]
        public void SA1500_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1500", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(19, 42, 19, 102)]
        [CodeIssue(21, 25, 21, 48)]
        [CodeIssue(24, 13, 24, 36)]
        public void SA1501_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1501", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(19, 9, 19, 46)]
        [CodeIssue(21, 9, 21, 73)]
        [CodeIssue(25, 9, 25, 38)]
        [CodeIssue(27, 9, 27, 40)]
        [CodeIssue(29, 9, 29, 38)]
        public void SA1502_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1502", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(22, 17, 22, 74)]
        [CodeIssue(25, 17, 25, 36)]
        [CodeIssue(28, 17, 28, 36)]
        [CodeIssue(31, 17, 31, 36)]
        [CodeIssue(34, 17, 34, 36)]
        [CodeIssue(37, 17, 37, 36)]
        [CodeIssue(41, 17, 41, 36)]
        public void SA1503_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1503", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(24, 13, 24, 16)]
        [CodeIssue(38, 13, 38, 19)]
        [CodeIssue(43, 13, 43, 16)]
        [CodeIssue(57, 13, 57, 16)]
        public void SA1504_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1504", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(7, 1, 7, 2)]
        [CodeIssue(17, 5, 17, 6)]
        [CodeIssue(22, 9, 22, 10)]
        [CodeIssue(25, 13, 25, 14)]
        [CodeIssue(31, 13, 31, 14)]
        [CodeIssue(38, 9, 38, 10)]
        [CodeIssue(41, 13, 41, 14)]
        [CodeIssue(47, 13, 47, 14)]
        [CodeIssue(53, 13, 53, 14)]
        [CodeIssue(59, 13, 59, 14)]
        [CodeIssue(65, 13, 65, 14)]
        [CodeIssue(71, 13, 71, 14)]
        [CodeIssue(78, 13, 78, 14)]
        public void SA1505_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1505", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(14, 1, 14, 2)]
        [CodeIssue(21, 1, 21, 9)]
        [CodeIssue(29, 1, 29, 2)]
        [CodeIssue(35, 1, 35, 2)]
        [CodeIssue(45, 1, 45, 2)]
        [CodeIssue(51, 1, 51, 9)]
        public void SA1506_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1506", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(11, 1, 11, 2)]
        [CodeIssue(12, 1, 12, 5)]
        [CodeIssue(21, 1, 21, 2)]
        [CodeIssue(26, 1, 26, 2)]
        public void SA1507_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1507", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [Rule("SA1500")]
        [Rule("SA1501")]
        [Rule("SA1502")]
        //[Rule("SA1503")]
        [Rule("SA1504")]
        [Rule("SA1505")]
        [Rule("SA1506")]
        [Rule("SA1507")]
        public void All_layout_code_issues_should_be_tested(string ruleCheck)
        {
            this.AssertAllReportedCodeIssuesAreTested(ruleCheck);
        }

        [Test]
        [Rule("SA1500")]
        [Rule("SA1501")]
        [Rule("SA1502")]
        //[Rule("SA1503")]
        [Rule("SA1504")]
        [Rule("SA1505")]
        [Rule("SA1506")]
        [Rule("SA1507")]
        public void All_layout_violations_should_have_code_issue(string ruleCheck)
        {
            this.AssertAllStyleCopReportedViolationsHaveCodeIssue(ruleCheck);
        }
    }
}
