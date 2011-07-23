namespace CR_StyleCop.Tests
{
    using System;
    using System.Linq;
    using MbUnit.Framework;

    public partial class RulesTests
    {
        [Test]
        [CodeIssue(58, 17, 58, 21)]
        [CodeIssue(60, 17, 60, 21)]
        public void SA1100_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1100", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(157, 17, 157, 28)]
        [CodeIssue(158, 17, 158, 30)]
        [CodeIssue(159, 17, 159, 39)]
        [CodeIssue(160, 17, 160, 31)]
        [CodeIssue(161, 17, 161, 29)]
        [CodeIssue(163, 17, 163, 29)]
        [CodeIssue(165, 17, 165, 31)]
        [CodeIssue(166, 17, 166, 33)]
        [CodeIssue(167, 17, 167, 42)]
        [CodeIssue(168, 17, 168, 34)]
        [CodeIssue(169, 17, 169, 32)]
        [CodeIssue(171, 17, 171, 30)]
        [CodeIssue(172, 17, 172, 32)]
        [CodeIssue(173, 17, 173, 41)]
        [CodeIssue(174, 17, 174, 33)]
        [CodeIssue(175, 17, 175, 31)]
        public void SA1101_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1101", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(23, 23, 23, 28)]
        [CodeIssue(29, 23, 29, 28)]
        public void SA1102_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1102", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(26, 24, 26, 29)]
        [CodeIssue(31, 24, 31, 30)]
        public void SA1103_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1103", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(22, 35, 22, 40)]
        [CodeIssue(27, 30, 27, 36)]
        public void SA1104_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1104", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(21, 38, 21, 43)]
        [CodeIssue(25, 56, 25, 62)]
        public void SA1105_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1105", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(25, 21, 25, 22)]
        [CodeIssue(29, 17, 29, 18)]
        public void SA1106_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1106", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(21, 24, 21, 34)]
        [CodeIssue(23, 21, 21, 31)]
        public void SA1107_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1107", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(21, 13, 21, 46)]
        [CodeIssue(25, 13, 25, 48)]
        [CodeIssue(31, 13, 31, 50)]
        public void SA1108_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1108", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(21, 13, 21, 42)]
        [CodeIssue(24, 13, 24, 23)]
        [CodeIssue(26, 13, 26, 50)]
        [CodeIssue(33, 13, 33, 54)]
        public void SA1109_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1109", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(20, 13, 20, 14)]
        [CodeIssue(25, 21, 25, 22)]
        [CodeIssue(30, 13, 30, 14)]
        [CodeIssue(33, 17, 33, 18)]
        public void SA1110_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1110", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(20, 13, 20, 14)]
        [CodeIssue(25, 21, 25, 22)]
        [CodeIssue(30, 13, 30, 14)]
        [CodeIssue(33, 17, 33, 18)]
        public void SA1111_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1111", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(20, 13, 20, 14)]
        [CodeIssue(23, 17, 23, 18)]
        public void SA1112_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1112", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(21, 13, 21, 14)]
        [CodeIssue(27, 21, 27, 22)]
        [CodeIssue(33, 13, 33, 14)]
        [CodeIssue(37, 17, 37, 18)]
        public void SA1113_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1113", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(21, 13, 21, 18)]
        [CodeIssue(27, 21, 27, 22)]
        [CodeIssue(33, 13, 33, 25)]
        [CodeIssue(37, 17, 37, 22)]
        public void SA1114_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1114", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(22, 13, 22, 18)]
        [CodeIssue(29, 21, 29, 22)]
        [CodeIssue(36, 13, 36, 24)]
        [CodeIssue(41, 17, 41, 21)]
        public void SA1115_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1115", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(19, 26, 19, 31)]
        [CodeIssue(24, 29, 24, 30)]
        [CodeIssue(29, 36, 29, 48)]
        [CodeIssue(32, 37, 32, 42)]
        public void SA1116_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1116", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(20, 21, 20, 25)]
        [CodeIssue(26, 24, 26, 28)]
        [CodeIssue(32, 24, 32, 35)]
        [CodeIssue(36, 25, 36, 36)]
        public void SA1117_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1117", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(21, 13, 22, 14)]
        [CodeIssue(28, 21, 29, 24)]
        [CodeIssue(35, 13, 36, 14)]
        [CodeIssue(40, 17, 41, 20)]
        public void SA1118_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1118", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [Rule("SA1100")]
        [Rule("SA1101")]
        [Rule("SA1102")]
        [Rule("SA1103")]
        [Rule("SA1104")]
        [Rule("SA1105")]
        [Rule("SA1106")]
        [Rule("SA1107")]
        [Rule("SA1108")]
        [Rule("SA1109")]
        [Rule("SA1110")]
        [Rule("SA1111")]
        [Rule("SA1112")]
        [Rule("SA1113")]
        [Rule("SA1114")]
        [Rule("SA1115")]
        [Rule("SA1116")]
        [Rule("SA1117")]
        [Rule("SA1118")]
        public void All_readability_code_issues_should_be_tested(string ruleCheck)
        {
            this.AssertAllReportedCodeIssuesAreTested(ruleCheck);
        }

        [Test]
        [Rule("SA1100")]
        [Rule("SA1101")]
        [Rule("SA1102")]
        [Rule("SA1103")]
        [Rule("SA1104")]
        [Rule("SA1105")]
        [Rule("SA1106")]
        [Rule("SA1107")]
        [Rule("SA1108")]
        [Rule("SA1109")]
        [Rule("SA1110")]
        [Rule("SA1111")]
        [Rule("SA1112")]
        [Rule("SA1113")]
        [Rule("SA1114")]
        [Rule("SA1115")]
        [Rule("SA1116")]
        [Rule("SA1117")]
        [Rule("SA1118")]
        public void All_readability_violations_should_have_code_issue(string ruleCheck)
        {
            this.AssertAllStyleCopReportedViolationsHaveCodeIssue(ruleCheck);
        }
    }
}
