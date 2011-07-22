namespace CR_StyleCop.Tests
{
    using System;
    using System.Linq;
    using MbUnit.Framework;

    public partial class RulesTests
    {
        [Test]
        [CodeIssue(6, 23, 6, 31)]
        [CodeIssue(22, 30, 22, 44)]
        [CodeIssue(24, 35, 24, 44)]
        [CodeIssue(26, 21, 26, 27)]
        [CodeIssue(31, 20, 31, 32)]
        [CodeIssue(37, 20, 37, 33)]
        [CodeIssue(39, 22, 39, 32)]
        [CodeIssue(43, 23, 43, 31)]
        [CodeIssue(47, 22, 47, 29)]
        public void SA1300_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1300", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(19, 26, 19, 38)]
        [CodeIssue(23, 26, 23, 37)]
        [CodeIssue(27, 26, 27, 37)]
        public void SA1302_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1302", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(19, 26, 19, 41)]
        [CodeIssue(21, 28, 21, 43)]
        [CodeIssue(23, 38, 23, 53)]
        [CodeIssue(25, 29, 25, 44)]
        [CodeIssue(27, 27, 27, 41)]
        public void SA1303_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1303", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(21, 29, 21, 44)]
        [CodeIssue(23, 31, 23, 46)]
        [CodeIssue(25, 41, 25, 56)]
        [CodeIssue(27, 32, 27, 47)]
        public void SA1304_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1304", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(21, 21, 21, 29)]
        [CodeIssue(23, 24, 23, 32)]
        public void SA1305_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1305", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(40, 23, 40, 31)]
        [CodeIssue(41, 21, 41, 29)]
        public void SA1306_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1306", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(20, 29, 20, 37)]
        [CodeIssue(21, 27, 21, 35)]
        [CodeIssue(22, 20, 22, 29)]
        [CodeIssue(25, 31, 25, 39)]
        [CodeIssue(26, 29, 26, 37)]
        [CodeIssue(27, 22, 27, 31)]
        [CodeIssue(30, 41, 30, 49)]
        [CodeIssue(31, 39, 31, 47)]
        [CodeIssue(32, 32, 32, 41)]
        public void SA1307_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1307", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(19, 28, 19, 35)]
        [CodeIssue(20, 21, 20, 28)]
        public void SA1308_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1308", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(19, 21, 19, 31)]
        public void SA1309_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1309", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(19, 21, 19, 31)]
        public void SA1310_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1310", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [Rule("SA1300")]
        [Rule("SA1301")]
        [Rule("SA1302")]
        [Rule("SA1303")]
        [Rule("SA1304")]
        [Rule("SA1305")]
        [Rule("SA1306")]
        [Rule("SA1307")]
        [Rule("SA1308")]
        [Rule("SA1309")]
        [Rule("SA1310")]
        public void All_naming_code_issues_should_be_tested(string ruleCheck)
        {
            this.AssertAllReportedCodeIssuesAreTested(ruleCheck);
        }

        [Test]
        [Rule("SA1300")]
        [Rule("SA1301")]
        [Rule("SA1302")]
        [Rule("SA1303")]
        [Rule("SA1304")]
        [Rule("SA1305")]
        [Rule("SA1306")]
        [Rule("SA1307")]
        [Rule("SA1308")]
        [Rule("SA1309")]
        [Rule("SA1310")]
        public void All_naming_violations_should_have_code_issue(string ruleCheck)
        {
            this.AssertAllStyleCopReportedViolationsHaveCodeIssue(ruleCheck);
        }
    }
}
