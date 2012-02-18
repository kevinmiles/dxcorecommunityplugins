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
        [CodeIssue(26, 9, 26, 46)]
        [CodeIssue(28, 9, 28, 73)]
        [CodeIssue(34, 9, 34, 38)]
        [CodeIssue(36, 9, 36, 40)]
        [CodeIssue(38, 9, 38, 38)]
        public void SA1502_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1502", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(22, 17, 22, 73)]
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
        [CodeIssue(13, 5, 13, 19)]
        [CodeIssue(20, 9, 20, 23)]
        [CodeIssue(28, 9, 28, 23)]
        [CodeIssue(34, 9, 34, 23)]
        [CodeIssue(44, 9, 44, 23)]
        [CodeIssue(50, 9, 50, 23)]
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
        [CodeIssue(26, 13, 26, 14)]
        [CodeIssue(32, 13, 32, 14)]
        [CodeIssue(34, 9, 34, 10)]
        [CodeIssue(42, 13, 42, 14)]
        [CodeIssue(48, 13, 48, 14)]
        [CodeIssue(54, 13, 54, 14)]
        [CodeIssue(60, 13, 60, 14)]
        [CodeIssue(66, 13, 66, 14)]
        [CodeIssue(72, 13, 72, 14)]
        [CodeIssue(79, 13, 79, 14)]
        [CodeIssue(81, 9, 81, 10)]
        [CodeIssue(83, 5, 83, 6)]
        [CodeIssue(85, 1, 85, 2)]
        public void SA1508_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1508", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(8, 1, 8, 2)]
        [CodeIssue(18, 5, 18, 6)]
        [CodeIssue(23, 9, 23, 10)]
        [CodeIssue(26, 13, 26, 14)]
        [CodeIssue(32, 13, 32, 14)]
        [CodeIssue(39, 9, 39, 10)]
        [CodeIssue(42, 13, 42, 14)]
        [CodeIssue(48, 13, 48, 14)]
        [CodeIssue(54, 13, 54, 14)]
        [CodeIssue(60, 13, 60, 14)]
        [CodeIssue(66, 13, 66, 14)]
        [CodeIssue(72, 13, 72, 14)]
        [CodeIssue(79, 13, 79, 14)]
        public void SA1509_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1509", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(25, 13, 25, 17)]
        [CodeIssue(35, 13, 35, 18)]
        [CodeIssue(40, 13, 40, 18)]
        [CodeIssue(45, 13, 45, 20)]
        public void SA1510_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1510", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(26, 13, 26, 18)]
        public void SA1511_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1511", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(17, 9, 17, 36)]
        public void SA1512_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1512", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(27, 13, 27, 14)]
        [CodeIssue(32, 9, 32, 10)]
        [CodeIssue(38, 13, 38, 14)]
        [CodeIssue(43, 9, 43, 10)]
        [CodeIssue(49, 13, 49, 14)]
        [CodeIssue(53, 13, 53, 14)]
        [CodeIssue(57, 13, 57, 14)]
        [CodeIssue(61, 13, 61, 14)]
        [CodeIssue(65, 13, 65, 14)]
        [CodeIssue(69, 13, 69, 14)]
        [CodeIssue(75, 9, 75, 10)]
        public void SA1513_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1513", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(24, 9, 24, 22)]
        [CodeIssue(28, 9, 28, 22)]
        [CodeIssue(36, 9, 36, 22)]
        [CodeIssue(40, 9, 40, 22)]
        public void SA1514_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1514", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(22, 13, 22, 32)]
        [CodeIssue(25, 9, 25, 36)]
        public void SA1515_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1515", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(19, 17, 19, 31)]
        [CodeIssue(22, 36, 22, 45)]
        [CodeIssue(27, 13, 27, 19)]
        [CodeIssue(31, 27, 31, 39)]
        [CodeIssue(34, 21, 34, 33)]
        [CodeIssue(35, 21, 35, 34)]
        [CodeIssue(41, 13, 41, 16)]
        [CodeIssue(46, 22, 46, 32)]
        [CodeIssue(49, 24, 49, 32)]
        [CodeIssue(52, 23, 52, 30)]
        public void SA1516_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1516", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(1, 1, 1, 2)]
        public void SA1517_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1517", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(18, 1, 18, 2)]
        public void SA1518_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1518", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [Rule("SA1500")]
        [Rule("SA1501")]
        [Rule("SA1502")]
        [Rule("SA1503")]
        [Rule("SA1504")]
        [Rule("SA1505")]
        [Rule("SA1506")]
        [Rule("SA1507")]
        [Rule("SA1508")]
        [Rule("SA1509")]
        [Rule("SA1510")]
        [Rule("SA1511")]
        [Rule("SA1512")]
        [Rule("SA1513")]
        [Rule("SA1514")]
        [Rule("SA1515")]
        [Rule("SA1516")]
        [Rule("SA1517")]
        [Rule("SA1518")]
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
        [Rule("SA1507")]
        [Rule("SA1508")]
        [Rule("SA1509")]
        [Rule("SA1510")]
        [Rule("SA1511")]
        [Rule("SA1512")]
        [Rule("SA1513")]
        [Rule("SA1514")]
        [Rule("SA1515")]
        [Rule("SA1516")]
        [Rule("SA1517")]
        [Rule("SA1518")]
        public void All_layout_violations_should_have_code_issue(string ruleCheck)
        {
            this.AssertAllStyleCopReportedViolationsHaveCodeIssue(ruleCheck);
        }
    }
}
