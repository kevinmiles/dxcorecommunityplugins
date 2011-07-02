namespace CR_StyleCop.Tests
{
    using System;
    using System.Linq;
    using MbUnit.Framework;

    public partial class RulesTests
    {
        [Test]
        [Row(12, 1, 12, 8)]
        [Row(13, 1, 13, 11)]
        [Row(14, 1, 14, 8)]
        [Row(15, 1, 15, 12)]
        [Row(16, 1, 16, 10)]
        [Row(17, 1, 17, 7)]
        [Row(18, 5, 18, 13)]
        [Row(19, 5, 19, 17)]
        public void SA1006_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1006", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        public void All_SA1006_should_be_tested()
        {
            this.AssertAllSpecificCodeIssuesAreTested("SA1006");
        }
    }
}
