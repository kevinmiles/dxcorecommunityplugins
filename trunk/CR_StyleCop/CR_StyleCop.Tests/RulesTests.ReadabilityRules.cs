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
        [CodeIssue(26, 41, 26, 47)]
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
        [CodeIssue(21, 13, 21, 34)]
        [CodeIssue(23, 13, 23, 31)]
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
        [CodeIssue(21, 13, 21, 20)]
        [CodeIssue(24, 13, 24, 23)]
        [CodeIssue(26, 13, 26, 20)]
        [CodeIssue(33, 13, 33, 20)]
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
        [CodeIssue(18, 9, 18, 13)]
        [CodeIssue(19, 9, 19, 15)]
        [CodeIssue(20, 9, 20, 11)]
        [CodeIssue(28, 9, 28, 11)]
        public void SA1120_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1120", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(39, 17, 25, 24)]
        [CodeIssue(40, 17, 26, 31)]
        [CodeIssue(41, 17, 27, 39)]
        [CodeIssue(42, 17, 28, 33)]
        [CodeIssue(46, 17, 46, 21)]
        [CodeIssue(47, 17, 47, 28)]
        [CodeIssue(48, 17, 48, 36)]
        [CodeIssue(49, 17, 49, 30)]
        [CodeIssue(53, 17, 53, 21)]
        [CodeIssue(54, 17, 54, 28)]
        [CodeIssue(55, 17, 55, 36)]
        [CodeIssue(56, 17, 56, 30)]
        [CodeIssue(60, 17, 60, 24)]
        [CodeIssue(61, 17, 61, 31)]
        [CodeIssue(62, 17, 62, 39)]
        [CodeIssue(63, 17, 63, 33)]
        [CodeIssue(67, 17, 67, 23)]
        [CodeIssue(68, 17, 68, 30)]
        [CodeIssue(69, 17, 69, 38)]
        [CodeIssue(70, 17, 70, 32)]
        [CodeIssue(74, 17, 74, 22)]
        [CodeIssue(75, 17, 75, 29)]
        [CodeIssue(76, 17, 76, 37)]
        [CodeIssue(77, 17, 77, 31)]
        [CodeIssue(81, 17, 81, 22)]
        [CodeIssue(82, 17, 82, 29)]
        [CodeIssue(83, 17, 83, 37)]
        [CodeIssue(84, 17, 84, 31)]
        [CodeIssue(88, 17, 88, 22)]
        [CodeIssue(89, 17, 89, 29)]
        [CodeIssue(90, 17, 90, 37)]
        [CodeIssue(91, 17, 91, 31)]
        [CodeIssue(95, 17, 95, 23)]
        [CodeIssue(96, 17, 96, 30)]
        [CodeIssue(97, 17, 97, 38)]
        [CodeIssue(98, 17, 98, 32)]
        [CodeIssue(102, 17, 102, 22)]
        [CodeIssue(103, 17, 103, 29)]
        [CodeIssue(104, 17, 104, 37)]
        [CodeIssue(105, 17, 105, 31)]
        [CodeIssue(109, 17, 109, 23)]
        [CodeIssue(110, 17, 110, 30)]
        [CodeIssue(111, 17, 111, 38)]
        [CodeIssue(112, 17, 112, 32)]
        [CodeIssue(116, 17, 116, 23)]
        [CodeIssue(117, 17, 117, 30)]
        [CodeIssue(118, 17, 118, 38)]
        [CodeIssue(119, 17, 119, 32)]
        [CodeIssue(123, 17, 123, 23)]
        [CodeIssue(124, 17, 124, 30)]
        [CodeIssue(125, 17, 125, 38)]
        [CodeIssue(126, 17, 126, 32)]
        [CodeIssue(130, 17, 130, 23)]
        [CodeIssue(131, 17, 131, 30)]
        [CodeIssue(132, 17, 132, 38)]
        [CodeIssue(133, 17, 133, 32)]
        [CodeIssue(137, 17, 137, 23)]
        [CodeIssue(138, 17, 138, 30)]
        [CodeIssue(139, 17, 139, 38)]
        [CodeIssue(140, 17, 140, 32)]
        public void SA1121_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1121", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(21, 29, 21, 31)]
        [CodeIssue(24, 29, 24, 32)]
        public void SA1122_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1122", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(26, 13, 26, 25)]
        [CodeIssue(35, 17, 35, 28)]
        [CodeIssue(42, 17, 42, 31)]
        [CodeIssue(52, 17, 52, 31)]
        [CodeIssue(59, 17, 59, 31)]
        [CodeIssue(67, 13, 67, 27)]
        [CodeIssue(70, 17, 70, 27)]
        [CodeIssue(77, 17, 77, 30)]
        [CodeIssue(84, 17, 84, 29)]
        [CodeIssue(91, 17, 91, 28)]
        [CodeIssue(98, 17, 98, 32)]
        [CodeIssue(105, 17, 105, 31)]
        [CodeIssue(107, 21, 107, 33)]
        [CodeIssue(111, 21, 111, 36)]
        public void SA1123_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1123", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(21, 9, 21, 23)]
        [CodeIssue(26, 9, 26, 22)]
        [CodeIssue(33, 9, 33, 23)]
        [CodeIssue(36, 13, 36, 26)]
        [CodeIssue(50, 9, 50, 27)]
        [CodeIssue(53, 13, 53, 29)]
        [CodeIssue(67, 9, 67, 24)]
        public void SA1124_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1124", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(17, 40, 17, 53)]
        [CodeIssue(19, 17, 19, 30)]
        [CodeIssue(21, 17, 21, 35)]
        [CodeIssue(23, 22, 23, 40)]
        [CodeIssue(25, 17, 25, 30)]
        [CodeIssue(31, 33, 31, 47)]
        [CodeIssue(33, 13, 33, 27)]
        public void SA1125_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1125", startLine, startOffset, endLine, endOffset);
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
        [Rule("SA1120")]
        [Rule("SA1121")]
        [Rule("SA1122")]
        [Rule("SA1123")]
        [Rule("SA1124")]
        [Rule("SA1125")]
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
        [Rule("SA1120")]
        [Rule("SA1121")]
        [Rule("SA1122")]
        [Rule("SA1123")]
        [Rule("SA1124")]
        [Rule("SA1125")]
        public void All_readability_violations_should_have_code_issue(string ruleCheck)
        {
            this.AssertAllStyleCopReportedViolationsHaveCodeIssue(ruleCheck);
        }
    }
}
