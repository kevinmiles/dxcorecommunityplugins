namespace CR_StyleCop.Tests
{
    using System;
    using System.Linq;
    using MbUnit.Framework;

    public partial class RulesTests
    {
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
