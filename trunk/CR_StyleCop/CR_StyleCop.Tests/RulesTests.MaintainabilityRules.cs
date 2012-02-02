namespace CR_StyleCop.Tests
{
    using System;
    using System.Linq;
    using MbUnit.Framework;

    public partial class RulesTests
    {
        [Test]
        [CodeIssue(21, 34, 21, 41)]
        [CodeIssue(25, 21, 25, 28)]
        [CodeIssue(26, 21, 26, 28)]
        [CodeIssue(28, 17, 28, 24)]
        [CodeIssue(29, 24, 29, 39)]
        [CodeIssue(30, 24, 30, 33)]
        [CodeIssue(31, 25, 31, 32)]
        [CodeIssue(32, 27, 32, 34)]
        [CodeIssue(33, 20, 33, 28)]
        [CodeIssue(38, 26, 38, 34)]
        public void SA1119_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1119", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(15, 11, 15, 25)]
        [CodeIssue(17, 14, 17, 23)]
        [CodeIssue(19, 9, 19, 23)]
        [CodeIssue(23, 28, 23, 33)]
        [CodeIssue(25, 14, 25, 20)]
        [CodeIssue(31, 14, 31, 23)]
        [CodeIssue(37, 13, 37, 25)]
        [CodeIssue(39, 14, 39, 18)]
        [CodeIssue(44, 16, 44, 24)]
        public void SA1400_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1400", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(23, 9, 23, 15)]
        [CodeIssue(24, 9, 24, 15)]
        [CodeIssue(28, 9, 28, 17)]
        [CodeIssue(29, 9, 29, 17)]
        [CodeIssue(33, 9, 33, 27)]
        [CodeIssue(34, 9, 34, 27)]
        [CodeIssue(38, 9, 38, 18)]
        [CodeIssue(39, 9, 39, 18)]
        public void SA1401_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1401", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(20, 18, 20, 33)]
        public void SA1402_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1402", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(18, 1, 18, 31)]
        [CodeIssue(18, 1, 18, 17, "2")]
        [CodeIssue(8, 5, 8, 20, "3")]
        public void SA1403_should_be_reported(int startLine, int startOffset, int endLine, int endOffset, string fileNameSuffix)
        {
            this.AssertSpecificCodeIssueExists("SA1403", startLine, startOffset, endLine, endOffset, fileNameSuffix);
        }

        [Test]
        [CodeIssue(15, 38, 15, 53)]
        [CodeIssue(16, 38, 16, 53)]
        [CodeIssue(17, 38, 17, 53)]
        [CodeIssue(18, 38, 18, 53)]
        [CodeIssue(19, 12, 19, 27)]
        [CodeIssue(20, 12, 20, 27)]
        [CodeIssue(21, 12, 21, 27)]
        [CodeIssue(22, 12, 22, 27)]
        [CodeIssue(23, 6, 23, 21)]
        [CodeIssue(24, 6, 24, 21)]
        [CodeIssue(25, 6, 25, 21)]
        [CodeIssue(26, 6, 26, 21)]
        [CodeIssue(27, 46, 27, 61)]
        [CodeIssue(28, 46, 28, 61)]
        [CodeIssue(29, 46, 29, 61)]
        [CodeIssue(30, 46, 30, 61)]
        public void SA1404_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1404", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(24, 13, 24, 43)]
        [CodeIssue(25, 13, 25, 44)]
        [CodeIssue(26, 13, 26, 53)]
        [CodeIssue(27, 13, 27, 60)]
        [CodeIssue(28, 13, 28, 68)]
        [CodeIssue(29, 13, 29, 53)]
        [CodeIssue(30, 13, 30, 45)]
        [CodeIssue(31, 13, 31, 39)]
        public void SA1405_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1405", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(24, 13, 24, 29)]
        [CodeIssue(25, 13, 25, 28)]
        [CodeIssue(26, 13, 26, 38)]
        [CodeIssue(27, 13, 27, 45)]
        [CodeIssue(28, 13, 28, 53)]
        [CodeIssue(29, 13, 29, 38)]
        [CodeIssue(30, 13, 30, 30)]
        public void SA1406_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1406", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(19, 25, 19, 30)]
        [CodeIssue(20, 17, 20, 22)]
        [CodeIssue(21, 17, 21, 23)]
        [CodeIssue(22, 17, 22, 22)]
        public void SA1407_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1407", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(19, 22, 19, 34)]
        [CodeIssue(20, 25, 20, 38)]
        public void SA1408_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1408", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(17, 16, 17, 30)]
        [CodeIssue(25, 13, 25, 22)]
        [CodeIssue(29, 13, 29, 20)]
        [CodeIssue(33, 13, 33, 24)]
        [CodeIssue(37, 13, 37, 19)]
        [CodeIssue(41, 13, 41, 16)]
        [CodeIssue(44, 13, 44, 20)]
        [CodeIssue(48, 13, 48, 16)]
        [CodeIssue(52, 13, 52, 20)]
        [CodeIssue(64, 13, 64, 16)]
        [CodeIssue(87, 13, 87, 16)]
        [CodeIssue(93, 13, 93, 20)]
        [CodeIssue(104, 13, 104, 20)]
        [CodeIssue(115, 13, 115, 20)]
        [CodeIssue(138, 13, 138, 20)]
        public void SA1409_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1409", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(24, 36, 24, 46)]
        public void SA1410_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1410", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(14, 21, 14, 25)]
        [CodeIssue(15, 21, 15, 34)]
        public void SA1411_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1411", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [Rule("SA1119")]
        [Rule("SA1400")]
        [Rule("SA1401")]
        [Rule("SA1402")]
        [Rule("SA1403")]
        [Rule("SA1404")]
        [Rule("SA1405")]
        [Rule("SA1406")]
        [Rule("SA1407")]
        [Rule("SA1408")]
        [Rule("SA1409")]
        [Rule("SA1410")]
        [Rule("SA1411")]
        public void All_maintainability_code_issues_should_be_tested(string ruleCheck)
        {
            this.AssertAllReportedCodeIssuesAreTested(ruleCheck);
        }

        [Test]
        [Rule("SA1119")]
        [Rule("SA1400")]
        [Rule("SA1401")]
        [Rule("SA1402")]
        [Rule("SA1403")]
        [Rule("SA1404")]
        [Rule("SA1405")]
        [Rule("SA1406")]
        [Rule("SA1407")]
        [Rule("SA1408")]
        [Rule("SA1409")]
        [Rule("SA1410")]
        [Rule("SA1411")]
        public void All_maintainability_violations_should_have_code_issue(string ruleCheck)
        {
            this.AssertAllStyleCopReportedViolationsHaveCodeIssue(ruleCheck);
        }
    }
}
