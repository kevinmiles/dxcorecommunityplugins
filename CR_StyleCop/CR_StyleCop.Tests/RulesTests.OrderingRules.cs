namespace CR_StyleCop.Tests
{
    using System;
    using System.Linq;
    using MbUnit.Framework;

    public partial class RulesTests
    {
        [Test]
        [CodeIssue(6, 1, 6, 14)]
        [CodeIssue(7, 1, 7, 19)]
        public void SA1200_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1200", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(23, 19, 23, 27)]
        [CodeIssue(30, 19, 30, 28)]
        [CodeIssue(37, 22, 37, 34)]
        [CodeIssue(44, 17, 44, 23)]
        [CodeIssue(57, 26, 57, 40)]
        [CodeIssue(59, 15, 59, 20)]
        [CodeIssue(19, 22, 19, 32, "2")]
        [CodeIssue(23, 23, 23, 34, "2")]
        [CodeIssue(27, 22, 27, 32, "2")]
        [CodeIssue(32, 20, 32, 24, "2")]
        [CodeIssue(48, 26, 48, 40, "2")]
        [CodeIssue(52, 21, 52, 30, "2")]
        [CodeIssue(57, 35, 57, 44, "2")]
        [CodeIssue(59, 30, 59, 44, "2")]
        [CodeIssue(61, 10, 61, 25, "2")]
        [CodeIssue(65, 16, 65, 31, "2")]
        [CodeIssue(71, 21, 71, 33, "2")]
        public void SA1201_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1201", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(19, 22, 19, 33)]
        [CodeIssue(31, 33, 31, 44)]
        [CodeIssue(35, 23, 35, 34)]
        [CodeIssue(39, 21, 39, 32)]
        public void SA1202_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1202", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(19, 24, 19, 31)]
        [CodeIssue(22, 30, 22, 38)]
        public void SA1203_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1203", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(19, 24, 19, 39)]
        [CodeIssue(26, 16, 26, 30)]
        [CodeIssue(33, 42, 33, 53)]
        [CodeIssue(41, 30, 41, 44)]
        [CodeIssue(60, 28, 60, 38)]
        [CodeIssue(64, 28, 60, 41)]
        public void SA1204_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1204", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(20, 23, 20, 35)]
        public void SA1205_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1205", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(20, 9, 20, 32)]
        [CodeIssue(21, 9, 21, 32)]
        [CodeIssue(22, 9, 22, 32)]
        [CodeIssue(23, 9, 23, 32)]
        [CodeIssue(24, 9, 24, 32)]
        public void SA1206_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1206", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(23, 9, 23, 27)]
        [CodeIssue(27, 9, 27, 34)]
        [CodeIssue(31, 9, 31, 34)]
        public void SA1207_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1207", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(11, 5, 11, 23)]
        public void SA1208_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1208", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(9, 5, 9, 30)]
        public void SA1209_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1209", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(9, 5, 9, 23)]
        [CodeIssue(10, 5, 10, 23)]
        public void SA1210_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1210", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(10, 5, 10, 29)]
        public void SA1211_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1211", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(29, 13, 29, 16)]
        public void SA1212_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1212", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(28, 13, 28, 19)]
        public void SA1213_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1213", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [Rule("SA1200")]
        [Rule("SA1201")]
        [Rule("SA1202")]
        [Rule("SA1203")]
        [Rule("SA1204")]
        [Rule("SA1205")]
        [Rule("SA1206")]
        [Rule("SA1207")]
        [Rule("SA1208")]
        [Rule("SA1209")]
        [Rule("SA1210")]
        [Rule("SA1211")]
        [Rule("SA1212")]
        [Rule("SA1213")]
        public void All_ordering_code_issues_should_be_tested(string ruleCheck)
        {
            this.AssertAllReportedCodeIssuesAreTested(ruleCheck);
        }

        [Test]
        [Rule("SA1200")]
        [Rule("SA1201")]
        [Rule("SA1202")]
        [Rule("SA1203")]
        [Rule("SA1204")]
        [Rule("SA1205")]
        [Rule("SA1206")]
        [Rule("SA1207")]
        [Rule("SA1208")]
        [Rule("SA1209")]
        [Rule("SA1210")]
        [Rule("SA1211")]
        [Rule("SA1212")]
        [Rule("SA1213")]
        public void All_ordering_violations_should_have_code_issue(string ruleCheck)
        {
            this.AssertAllStyleCopReportedViolationsHaveCodeIssue(ruleCheck);
        }
    }
}
