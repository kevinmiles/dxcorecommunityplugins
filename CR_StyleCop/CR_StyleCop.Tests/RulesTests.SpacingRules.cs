namespace CR_StyleCop.Tests
{
    using System;
    using System.Linq;
    using MbUnit.Framework;

    public partial class RulesTests
    {
        [Test]
        [CodeIssue(27, 13, 27, 16)]
        [CodeIssue(32, 34, 32, 36)]
        [CodeIssue(33, 27, 33, 32)]
        [CodeIssue(34, 27, 34, 32)]
        [CodeIssue(34, 36, 34, 38)]
        [CodeIssue(35, 27, 35, 34)]
        [CodeIssue(37, 27, 37, 33)]
        [CodeIssue(39, 34, 39, 36)]
        [CodeIssue(39, 46, 39, 48)]
        [CodeIssue(39, 63, 39, 69)]
        [CodeIssue(40, 34, 40, 37)]
        [CodeIssue(41, 13, 41, 20)]
        [CodeIssue(45, 13, 45, 18)]
        [CodeIssue(53, 13, 53, 18)]
        [CodeIssue(55, 17, 55, 22)]
        [CodeIssue(60, 17, 60, 22)]
        [CodeIssue(67, 17, 67, 23)]
        [CodeIssue(85, 13, 85, 18)]
        [CodeIssue(87, 17, 87, 22)]
        [CodeIssue(90, 19, 90, 26)]
        [CodeIssue(91, 25, 91, 31)]
        [CodeIssue(92, 24, 92, 30)]
        [CodeIssue(94, 30, 94, 37)]
        [CodeIssue(95, 32, 95, 41)]
        [CodeIssue(96, 13, 96, 19)]
        [CodeIssue(101, 19, 101, 25)]
        public void SA1000_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1000", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(17, 58, 17, 59)]
        [CodeIssue(17, 63, 17, 64)]
        [CodeIssue(18, 35, 18, 36)]
        [CodeIssue(20, 27, 20, 28)]
        [CodeIssue(22, 47, 22, 48)]
        [CodeIssue(24, 30, 24, 31)]
        [CodeIssue(25, 31, 25, 32)]
        [CodeIssue(26, 31, 26, 32)]
        [CodeIssue(27, 30, 27, 31)]
        public void SA1001_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1001", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(9, 5, 9, 6)]
        [CodeIssue(23, 38, 23, 39)]
        [CodeIssue(28, 55, 28, 56)]
        [CodeIssue(29, 60, 29, 61)]
        public void SA1002_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1002", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(22, 32, 22, 33)]
        [CodeIssue(25, 13, 25, 14)]
        [CodeIssue(29, 49, 29, 50)]
        [CodeIssue(31, 24, 31, 25)]
        [CodeIssue(32, 42, 32, 44)]
        [CodeIssue(33, 25, 33, 26)]
        [CodeIssue(34, 24, 34, 25)]
        [CodeIssue(35, 24, 35, 25)]
        [CodeIssue(36, 25, 36, 26)]
        [CodeIssue(37, 24, 37, 25)]
        [CodeIssue(38, 25, 38, 27)]
        [CodeIssue(39, 24, 39, 26)]
        [CodeIssue(40, 20, 40, 22)]
        [CodeIssue(41, 21, 41, 23)]
        [CodeIssue(42, 20, 42, 22)]
        [CodeIssue(43, 21, 43, 23)]
        [CodeIssue(44, 20, 44, 22)]
        [CodeIssue(45, 20, 45, 23)]
        [CodeIssue(46, 20, 46, 23)]
        [CodeIssue(47, 21, 47, 23)]
        [CodeIssue(48, 20, 48, 22)]
        [CodeIssue(49, 20, 49, 22)]
        [CodeIssue(50, 25, 50, 26)]
        [CodeIssue(51, 24, 51, 25)]
        [CodeIssue(52, 24, 52, 25)]
        [CodeIssue(53, 23, 53, 24)]
        [CodeIssue(54, 23, 54, 24)]
        [CodeIssue(56, 28, 56, 29)]
        [CodeIssue(56, 30, 56, 31)]
        [CodeIssue(57, 34, 57, 36)]
        [CodeIssue(59, 29, 59, 31)]
        [CodeIssue(60, 24, 60, 26)]
        [CodeIssue(61, 25, 61, 27)]
        [CodeIssue(62, 24, 62, 26)]
        [CodeIssue(63, 28, 63, 30)]
        [CodeIssue(64, 27, 64, 29)]
        [CodeIssue(65, 23, 65, 24)]
        [CodeIssue(66, 21, 66, 22)]
        [CodeIssue(66, 22, 66, 23)]
        [CodeIssue(67, 23, 67, 24)]
        [CodeIssue(72, 18, 72, 19)]
        public void SA1003_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1003", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(12, 1, 12, 8)]
        [CodeIssue(13, 1, 13, 11)]
        [CodeIssue(14, 1, 14, 8)]
        [CodeIssue(15, 1, 15, 12)]
        [CodeIssue(16, 1, 16, 10)]
        [CodeIssue(17, 1, 17, 7)]
        [CodeIssue(18, 5, 18, 13)]
        [CodeIssue(19, 5, 19, 17)]
        public void SA1006_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1006", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        //[Rule("SA1000")]
        //[Rule("SA1001")]
        //[Rule("SA1002")]
        //[Rule("SA1003")]
        //[Rule("SA1004")]
        //[Rule("SA1005")]
        [Rule("SA1006")]
        public void All_spacing_code_issues_should_be_tested(string ruleCheck)
        {
            this.AssertAllReportedCodeIssuesAreTested(ruleCheck);
        }

        [Test]
        //[Rule("SA1000")]
        //[Rule("SA1001")]
        //[Rule("SA1002")]
        //[Rule("SA1003")]
        //[Rule("SA1004")]
        //[Rule("SA1005")]
        [Rule("SA1006")]
        public void All_spacing_violations_should_have_code_issue(string ruleCheck)
        {
            this.AssertAllStyleCopReportedViolationsHaveCodeIssue(ruleCheck);
        }
    }
}
