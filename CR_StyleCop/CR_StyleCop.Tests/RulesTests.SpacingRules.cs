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
