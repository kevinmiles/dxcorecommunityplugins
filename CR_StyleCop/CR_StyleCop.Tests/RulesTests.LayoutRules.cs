namespace CR_StyleCop.Tests
{
    using System;
    using System.Linq;
    using MbUnit.Framework;

    public partial class RulesTests
    {
        [Test]
        [Row(15, 33, 15, 34)]
        [Row(18, 35, 18, 36)]
        [Row(19, 33, 19, 34)]
        [Row(25, 27, 25, 28)]
        [Row(27, 33, 27, 34)]
        [Row(28, 27, 28, 28)]
        public void SA1500_should_be_reported(int startLine, int startOffset, int endLine, int endOffset)
        {
            this.AssertSpecificCodeIssueExists("SA1500", startLine, startOffset, endLine, endOffset);
        }

        [Test]
        public void All_SA1500_should_be_tested()
        {
            this.AssertAllSpecificCodeIssuesAreTested("SA1500");
        }
    }
}
