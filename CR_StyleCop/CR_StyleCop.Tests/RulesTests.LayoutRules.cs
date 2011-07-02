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
        [Rule("SA1500")]
        //[Rule("SA1501")]
        //[Rule("SA1502")]
        //[Rule("SA1503")]
        //[Rule("SA1504")]
        //[Rule("SA1505")]
        //[Rule("SA1506")]
        public void All_layout_code_issues_should_be_tested(string ruleCheck)
        {
            this.AssertAllReportedCodeIssuesAreTested(ruleCheck);
        }

        [Test]
        [Rule("SA1500")]
        [Rule("SA1501")]
        [Rule("SA1502")]
        [Rule("SA1503")]
        [Rule("SA1504")]
        [Rule("SA1505")]
        [Rule("SA1506")]
        public void All_layout_violations_should_have_code_issue(string ruleCheck)
        {
            this.AssertAllStyleCopReportedViolationsHaveCodeIssue(ruleCheck);
        }
    }
}
