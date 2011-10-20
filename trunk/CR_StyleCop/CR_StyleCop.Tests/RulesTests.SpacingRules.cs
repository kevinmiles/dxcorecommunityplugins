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
        [CodeIssue(14, 5, 14, 19)]
        [CodeIssue(27, 5, 27, 20)]
        [CodeIssue(30, 9, 30, 23)]
        [CodeIssue(41, 9, 41, 24)]
        [CodeIssue(44, 9, 44, 23)]
        [CodeIssue(57, 9, 57, 24)]
        [CodeIssue(62, 9, 62, 23)]
        [CodeIssue(65, 9, 65, 60)]
        [CodeIssue(70, 9, 70, 23)]
        [CodeIssue(85, 9, 85, 24)]
        [CodeIssue(88, 9, 88, 23)]
        [CodeIssue(99, 9, 99, 24)]
        [CodeIssue(102, 9, 102, 23)]
        [CodeIssue(115, 9, 115, 24)]
        [CodeIssue(120, 9, 120, 23)]
        [CodeIssue(133, 9, 133, 24)]
        [CodeIssue(136, 9, 136, 23)]
        [CodeIssue(150, 9, 150, 24)]
        [CodeIssue(156, 9, 156, 23)]
        [CodeIssue(171, 9, 171, 24)]
        [CodeIssue(177, 9, 177, 23)]
        [CodeIssue(190, 9, 190, 24)]
        public void SA1004_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1004", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(14, 5, 14, 17)]
        [CodeIssue(15, 5, 15, 79)]
        [CodeIssue(16, 5, 16, 18)]
        [CodeIssue(17, 5, 17, 60)]
        [CodeIssue(18, 5, 18, 81)]
        [CodeIssue(19, 5, 19, 96)]
        [CodeIssue(20, 5, 20, 49)]
        [CodeIssue(21, 5, 21, 17)]
        [CodeIssue(22, 5, 22, 14)]
        [CodeIssue(23, 5, 23, 21)]
        [CodeIssue(24, 5, 24, 9)]
        [CodeIssue(25, 5, 25, 9)]
        [CodeIssue(26, 5, 26, 15)]
        [CodeIssue(27, 5, 27, 18)]
        [CodeIssue(30, 9, 30, 21)]
        [CodeIssue(31, 9, 31, 30)]
        [CodeIssue(32, 9, 32, 22)]
        [CodeIssue(33, 9, 33, 100)]
        [CodeIssue(34, 9, 34, 53)]
        [CodeIssue(35, 9, 35, 21)]
        [CodeIssue(36, 9, 36, 18)]
        [CodeIssue(37, 9, 37, 25)]
        [CodeIssue(38, 9, 38, 13)]
        [CodeIssue(39, 9, 39, 13)]
        [CodeIssue(40, 9, 40, 19)]
        [CodeIssue(41, 9, 41, 22)]
        [CodeIssue(44, 9, 44, 21)]
        [CodeIssue(45, 9, 45, 68)]
        [CodeIssue(46, 9, 46, 22)]
        [CodeIssue(47, 9, 47, 71)]
        [CodeIssue(48, 9, 48, 85)]
        [CodeIssue(49, 9, 49, 100)]
        [CodeIssue(50, 9, 50, 53)]
        [CodeIssue(51, 9, 51, 21)]
        [CodeIssue(52, 9, 52, 18)]
        [CodeIssue(53, 9, 53, 25)]
        [CodeIssue(54, 9, 54, 13)]
        [CodeIssue(55, 9, 55, 13)]
        [CodeIssue(56, 9, 56, 19)]
        [CodeIssue(57, 9, 57, 22)]
        [CodeIssue(62, 9, 62, 21)]
        [CodeIssue(63, 9, 63, 63)]
        [CodeIssue(64, 9, 64, 22)]
        [CodeIssue(65, 9, 65, 58)]
        [CodeIssue(70, 9, 70, 21)]
        [CodeIssue(71, 9, 71, 33)]
        [CodeIssue(72, 9, 72, 22)]
        [CodeIssue(73, 9, 73, 76)]
        [CodeIssue(74, 9, 74, 59)]
        [CodeIssue(75, 9, 75, 59)]
        [CodeIssue(76, 9, 76, 85)]
        [CodeIssue(77, 9, 77, 100)]
        [CodeIssue(78, 9, 78, 53)]
        [CodeIssue(79, 9, 79, 21)]
        [CodeIssue(80, 9, 80, 18)]
        [CodeIssue(81, 9, 81, 25)]
        [CodeIssue(82, 9, 82, 13)]
        [CodeIssue(83, 9, 83, 13)]
        [CodeIssue(84, 9, 84, 19)]
        [CodeIssue(85, 9, 85, 22)]
        [CodeIssue(88, 9, 88, 21)]
        [CodeIssue(89, 9, 89, 38)]
        [CodeIssue(90, 9, 90, 22)]
        [CodeIssue(91, 9, 91, 100)]
        [CodeIssue(92, 9, 92, 53)]
        [CodeIssue(93, 9, 93, 21)]
        [CodeIssue(94, 9, 94, 18)]
        [CodeIssue(95, 9, 95, 25)]
        [CodeIssue(96, 9, 96, 13)]
        [CodeIssue(97, 9, 97, 13)]
        [CodeIssue(98, 9, 98, 19)]
        [CodeIssue(99, 9, 99, 22)]
        [CodeIssue(102, 9, 102, 21)]
        [CodeIssue(103, 9, 103, 34)]
        [CodeIssue(104, 9, 104, 22)]
        [CodeIssue(105, 9, 105, 77)]
        [CodeIssue(106, 9, 106, 85)]
        [CodeIssue(107, 9, 107, 100)]
        [CodeIssue(108, 9, 108, 57)]
        [CodeIssue(109, 9, 109, 21)]
        [CodeIssue(110, 9, 110, 18)]
        [CodeIssue(111, 9, 111, 25)]
        [CodeIssue(112, 9, 112, 13)]
        [CodeIssue(113, 9, 113, 13)]
        [CodeIssue(114, 9, 114, 19)]
        [CodeIssue(115, 9, 115, 22)]
        [CodeIssue(120, 9, 120, 21)]
        [CodeIssue(121, 9, 121, 38)]
        [CodeIssue(122, 9, 122, 22)]
        [CodeIssue(123, 9, 123, 39)]
        [CodeIssue(124, 9, 124, 85)]
        [CodeIssue(125, 9, 125, 100)]
        [CodeIssue(126, 9, 126, 56)]
        [CodeIssue(127, 9, 127, 21)]
        [CodeIssue(128, 9, 128, 18)]
        [CodeIssue(129, 9, 129, 25)]
        [CodeIssue(130, 9, 130, 13)]
        [CodeIssue(131, 9, 131, 13)]
        [CodeIssue(132, 9, 132, 19)]
        [CodeIssue(133, 9, 133, 22)]
        [CodeIssue(136, 9, 136, 21)]
        [CodeIssue(137, 9, 137, 32)]
        [CodeIssue(138, 9, 138, 22)]
        [CodeIssue(139, 9, 139, 67)]
        [CodeIssue(140, 9, 140, 50)]
        [CodeIssue(141, 9, 141, 85)]
        [CodeIssue(142, 9, 142, 100)]
        [CodeIssue(143, 9, 143, 55)]
        [CodeIssue(144, 9, 144, 21)]
        [CodeIssue(145, 9, 145, 18)]
        [CodeIssue(146, 9, 146, 25)]
        [CodeIssue(147, 9, 147, 13)]
        [CodeIssue(148, 9, 148, 13)]
        [CodeIssue(149, 9, 149, 19)]
        [CodeIssue(150, 9, 150, 22)]
        [CodeIssue(156, 9, 156, 21)]
        [CodeIssue(157, 9, 157, 34)]
        [CodeIssue(158, 9, 158, 22)]
        [CodeIssue(159, 9, 159, 74)]
        [CodeIssue(160, 9, 160, 71)]
        [CodeIssue(161, 9, 161, 59)]
        [CodeIssue(162, 9, 162, 85)]
        [CodeIssue(163, 9, 163, 100)]
        [CodeIssue(164, 9, 164, 54)]
        [CodeIssue(165, 9, 165, 21)]
        [CodeIssue(166, 9, 166, 18)]
        [CodeIssue(167, 9, 167, 25)]
        [CodeIssue(168, 9, 168, 13)]
        [CodeIssue(169, 9, 169, 13)]
        [CodeIssue(170, 9, 170, 19)]
        [CodeIssue(171, 9, 171, 22)]
        [CodeIssue(177, 9, 177, 21)]
        [CodeIssue(178, 9, 178, 31)]
        [CodeIssue(179, 9, 179, 22)]
        [CodeIssue(180, 9, 180, 74)]
        [CodeIssue(181, 9, 181, 85)]
        [CodeIssue(182, 9, 182, 100)]
        [CodeIssue(183, 9, 183, 54)]
        [CodeIssue(184, 9, 184, 21)]
        [CodeIssue(185, 9, 185, 18)]
        [CodeIssue(186, 9, 186, 25)]
        [CodeIssue(187, 9, 187, 13)]
        [CodeIssue(188, 9, 188, 13)]
        [CodeIssue(189, 9, 189, 19)]
        [CodeIssue(190, 9, 190, 22)]
        public void SA1004b_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1004", startLine, startOffset, endLine, endOffset, "2");
        }

        [Test]
        [CodeIssue(12, 5, 12, 12)]
        [CodeIssue(21, 9, 21, 16)]
        [CodeIssue(25, 13, 25, 20)]
        public void SA1005_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1005", startLine, startOffset, endLine, endOffset);
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
        [CodeIssue(22, 28, 22, 38)]
        [CodeIssue(27, 28, 27, 38)]
        [CodeIssue(32, 28, 32, 38)]
        [CodeIssue(37, 28, 37, 38)]
        [CodeIssue(42, 28, 42, 37)]
        [CodeIssue(47, 28, 47, 37)]
        [CodeIssue(52, 38, 52, 47)]
        [CodeIssue(57, 38, 57, 47)]
        [CodeIssue(62, 38, 62, 47)]
        [CodeIssue(67, 38, 67, 47)]
        [CodeIssue(72, 38, 72, 47)]
        [CodeIssue(77, 38, 77, 47)]
        [CodeIssue(82, 38, 82, 47)]
        [CodeIssue(87, 38, 87, 48)]
        [CodeIssue(92, 38, 92, 48)]
        [CodeIssue(97, 38, 97, 47)]
        [CodeIssue(102, 38, 102, 47)]
        [CodeIssue(107, 38, 107, 47)]
        [CodeIssue(112, 38, 112, 47)]
        [CodeIssue(117, 38, 117, 48)]
        [CodeIssue(122, 38, 122, 48)]
        public void SA1007_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1007", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(17, 21, 17, 22)]
        [CodeIssue(18, 22, 18, 23)]
        [CodeIssue(22, 57, 22, 58)]
        [CodeIssue(24, 56, 24, 57)]
        [CodeIssue(26, 32, 26, 33)]
        [CodeIssue(28, 25, 28, 26)]
        [CodeIssue(28, 27, 28, 28)]
        [CodeIssue(33, 20, 33, 21)]
        public void SA1008_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1008", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(15, 110, 15, 111)]
        [CodeIssue(16, 154, 16, 155)]
        [CodeIssue(20, 33, 20, 34)]
        [CodeIssue(22, 38, 22, 39)]
        [CodeIssue(23, 27, 23, 28)]
        [CodeIssue(23, 42, 23, 43)]
        [CodeIssue(23, 44, 23, 44)]
        public void SA1009_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1009", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(22, 23, 22, 24)]
        [CodeIssue(23, 18, 23, 19)]
        [CodeIssue(25, 24, 25, 25)]
        [CodeIssue(26, 19, 26, 20)]
        [CodeIssue(28, 26, 28, 27)]
        [CodeIssue(33, 25, 33, 26)]
        [CodeIssue(40, 20, 40, 21)]
        [CodeIssue(41, 19, 41, 20)]
        [CodeIssue(42, 27, 42, 28)]
        [CodeIssue(43, 25, 43, 26)]
        public void SA1010_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1010", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(23, 25, 23, 26)]
        [CodeIssue(24, 20, 24, 21)]
        [CodeIssue(27, 19, 27, 20)]
        [CodeIssue(29, 36, 29, 37)]
        [CodeIssue(37, 21, 37, 22)]
        [CodeIssue(38, 29, 38, 30)]
        [CodeIssue(39, 27, 39, 28)]
        public void SA1011_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1011", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(23, 56, 23, 57)]
        [CodeIssue(25, 57, 25, 58)]
        [CodeIssue(27, 34, 27, 35)]
        [CodeIssue(28, 41, 28, 42)]
        [CodeIssue(29, 40, 29, 41)]
        public void SA1012_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1012", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(23, 58, 23, 59)]
        [CodeIssue(25, 59, 25, 60)]
        [CodeIssue(29, 65, 29, 66)]
        [CodeIssue(30, 66, 30, 67)]
        [CodeIssue(31, 9, 31, 10)]
        [CodeIssue(31, 10, 31, 11)]
        public void SA1013_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1013", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(21, 32, 21, 33)]
        [CodeIssue(23, 22, 23, 23)]
        [CodeIssue(24, 21, 24, 22)]
        [CodeIssue(26, 33, 26, 34)]
        [CodeIssue(28, 18, 28, 19)]
        [CodeIssue(29, 29, 29, 30)]
        [CodeIssue(32, 33, 32, 34)]
        [CodeIssue(34, 17, 34, 18)]
        [CodeIssue(35, 29, 35, 30)]
        [CodeIssue(38, 32, 38, 33)]
        public void SA1014_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1014", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(25, 26, 25, 27)]
        [CodeIssue(28, 36, 28, 37)]
        [CodeIssue(30, 22, 30, 23)]
        [CodeIssue(31, 32, 31, 33)]
        [CodeIssue(40, 35, 40, 36)]
        public void SA1015_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1015", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(16, 5, 16, 6)]
        [CodeIssue(20, 9, 20, 10)]
        [CodeIssue(21, 32, 21, 33)]
        public void SA1016_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1016", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(16, 111, 16, 112)]
        [CodeIssue(20, 49, 20, 50)]
        [CodeIssue(21, 42, 21, 43)]
        public void SA1017_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1017", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(22, 21, 22, 22)]
        [CodeIssue(23, 26, 23, 27)]
        [CodeIssue(25, 20, 25, 21)]
        [CodeIssue(31, 21, 31, 22)]
        [CodeIssue(33, 18, 33, 19)]
        [CodeIssue(34, 37, 34, 38)]
        [CodeIssue(37, 42, 37, 43)]
        public void SA1018_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1018", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(8, 18, 8, 20)]
        [CodeIssue(9, 17, 9, 18)]
        [CodeIssue(10, 26, 10, 27)]
        [CodeIssue(21, 18, 21, 19)]
        [CodeIssue(22, 17, 22, 18)]
        [CodeIssue(27, 41, 27, 42)]
        [CodeIssue(28, 36, 28, 37)]
        [CodeIssue(29, 36, 29, 37)]
        [CodeIssue(35, 29, 35, 31)]
        [CodeIssue(36, 28, 36, 30)]
        [CodeIssue(37, 28, 37, 30)]
        public void SA1019_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1019", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(22, 13, 22, 15)]
        [CodeIssue(24, 13, 24, 15)]
        public void SA1020_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1020", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(26, 30, 26, 31)]
        [CodeIssue(29, 31, 29, 32)]
        [CodeIssue(30, 30, 30, 31)]
        [CodeIssue(34, 25, 34, 26)]
        public void SA1021_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1021", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(26, 30, 26, 31)]
        [CodeIssue(29, 31, 29, 32)]
        [CodeIssue(30, 30, 30, 31)]
        [CodeIssue(34, 25, 34, 26)]
        public void SA1022_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1022", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(25, 13, 25, 14)]
        [CodeIssue(27, 21, 27, 22)]
        [CodeIssue(34, 17, 34, 18)]
        [CodeIssue(35, 22, 35, 23)]
        [CodeIssue(38, 17, 38, 18)]
        [CodeIssue(39, 22, 39, 23)]
        [CodeIssue(47, 22, 47, 23)]
        public void SA1023_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1023", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(30, 23, 30, 24)]
        [CodeIssue(31, 24, 31, 25)]
        [CodeIssue(32, 24, 32, 25)]
        [CodeIssue(33, 24, 33, 25)]
        [CodeIssue(43, 18, 43, 19)]
        [CodeIssue(46, 18, 46, 19)]
        [CodeIssue(47, 17, 47, 18)]
        [CodeIssue(48, 18, 48, 19)]
        public void SA1024_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1024", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(18, 129, 18, 131)]
        [CodeIssue(24, 14, 24, 16)]
        [CodeIssue(32, 16, 32, 18)]
        [CodeIssue(33, 28, 33, 20)]
        [CodeIssue(33, 33, 33, 35)]
        [CodeIssue(36, 20, 36, 27)]
        public void SA1025_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1025", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(19, 30, 19, 33)]
        [CodeIssue(23, 21, 23, 24)]
        public void SA1026_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1026", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [CodeIssue(2, 3, 2, 5)]
        [CodeIssue(8, 1, 8, 5)]
        [CodeIssue(9, 1, 9, 5)]
        [CodeIssue(10, 1, 10, 5)]
        [CodeIssue(14, 1, 14, 5)]
        [CodeIssue(15, 1, 15, 5)]
        [CodeIssue(16, 1, 16, 5)]
        [CodeIssue(17, 1, 17, 5)]
        [CodeIssue(18, 1, 18, 5)]
        [CodeIssue(19, 1, 19, 5)]
        [CodeIssue(20, 1, 20, 9)]
        [CodeIssue(22, 1, 22, 9)]
        [CodeIssue(23, 1, 23, 9)]
        [CodeIssue(24, 1, 24, 13)]
        [CodeIssue(25, 1, 25, 13)]
        [CodeIssue(26, 1, 26, 17)]
        [CodeIssue(27, 1, 27, 5)]
        [CodeIssue(27, 9, 27, 13)]
        [CodeIssue(29, 1, 29, 13)]
        [CodeIssue(30, 1, 30, 13)]
        [CodeIssue(31, 1, 31, 17)]
        [CodeIssue(32, 1, 32, 13)]
        [CodeIssue(33, 1, 33, 9)]
        [CodeIssue(34, 1, 34, 5)]
        public void SA1027_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1027", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        [Rule("SA1000")]
        [Rule("SA1001")]
        [Rule("SA1002")]
        [Rule("SA1003")]
        [Rule("SA1004")]
        [Rule("SA1005")]
        [Rule("SA1006")]
        [Rule("SA1007")]
        [Rule("SA1008")]
        [Rule("SA1009")]
        [Rule("SA1010")]
        [Rule("SA1011")]
        [Rule("SA1012")]
        [Rule("SA1013")]
        [Rule("SA1014")]
        [Rule("SA1015")]
        [Rule("SA1016")]
        [Rule("SA1017")]
        [Rule("SA1018")]
        [Rule("SA1019")]
        [Rule("SA1020")]
        [Rule("SA1021")]
        [Rule("SA1022")]
        [Rule("SA1023")]
        [Rule("SA1024")]
        [Rule("SA1025")]
        [Rule("SA1026")]
        [Rule("SA1027")]
        public void All_spacing_code_issues_should_be_tested(string ruleCheck)
        {
            this.AssertAllReportedCodeIssuesAreTested(ruleCheck);
        }

        [Test]
        [Rule("SA1000")]
        [Rule("SA1001")]
        [Rule("SA1002")]
        [Rule("SA1003")]
        [Rule("SA1004")]
        [Rule("SA1005")]
        [Rule("SA1006")]
        [Rule("SA1007")]
        [Rule("SA1008")]
        [Rule("SA1009")]
        [Rule("SA1010")]
        [Rule("SA1011")]
        [Rule("SA1012")]
        [Rule("SA1013")]
        [Rule("SA1014")]
        [Rule("SA1015")]
        [Rule("SA1016")]
        [Rule("SA1017")]
        [Rule("SA1018")]
        [Rule("SA1019")]
        [Rule("SA1020")]
        [Rule("SA1021")]
        [Rule("SA1022")]
        [Rule("SA1023")]
        [Rule("SA1024")]
        [Rule("SA1025")]
        [Rule("SA1026")]
        [Rule("SA1027")]
        public void All_spacing_violations_should_have_code_issue(string ruleCheck)
        {
            this.AssertAllStyleCopReportedViolationsHaveCodeIssue(ruleCheck);
        }
    }
}
