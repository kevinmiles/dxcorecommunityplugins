namespace CR_StyleCop.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CR_StyleCop.Tests.Helpers;
    using DevExpress.CodeRush.StructuralParser;
    using MbUnit.Framework;
    using System.Reflection;

    [TestFixture]
    public partial class RulesTests
    {
        private IEnumerable<SourceFile> files;
        private CR_StyleCopPlugIn plugin;

        [FixtureSetUp]
        public void SetUp()
        {
            ParserHelper.RegisterParserServices();
            var solutionParser = new SolutionParser(Path.GetFullPath("..\\..\\..\\CR_StyleCop.TestCode.sln"));
            var solution = solutionParser.GetParsedSolution();
            var project = solution.AllProjects.Cast<ProjectElement>().First();
            this.files = project.AllFiles.Cast<SourceFile>();
            this.plugin = new CR_StyleCopPlugIn();
        }

        [FixtureTearDown]
        public void TearDown()
        {
            ParserHelper.UnRegisterParserServices();
            this.plugin.FinalizePlugIn();
            this.plugin = null;
            this.files = null;
        }

        private void AssertSpecificCodeIssueExists(string ruleCheck, int startLine, int startOffset, int endLine, int endOffset)
        {
            foreach (SourceFile file in files.Where(x => x.Name.EndsWith(string.Format("{0}TestCode.cs", ruleCheck))))
            {
                var codeIssues = plugin.GetCodeIssuesFor(file);
                Assert.Exists(
                    codeIssues,
                    x => x.Message.StartsWith(ruleCheck)
                        && x.Range.Start.Line == startLine
                        && x.Range.Start.Offset == startOffset
                        && x.Range.End.Line == endLine
                        && x.Range.End.Offset == endOffset,
                    "{0} is not reported from line {1}, col {2} to line {3}, col {4}",
                    new object[] { ruleCheck, startLine, startOffset, endLine, endOffset });
            }
        }

        private void AssertAllSpecificCodeIssuesAreTested(string ruleCheck)
        {
            var method = typeof(RulesTests).GetMethod(ruleCheck + "_should_be_reported");
            var coveredCodeIssues = method.GetCustomAttributes(typeof(RowAttribute), false);
            foreach (SourceFile file in files.Where(x => x.Name.EndsWith(string.Format("{0}TestCode.cs", ruleCheck))))
            {
                var codeIssues = plugin.GetCodeIssuesFor(file);
                Assert.AreEqual(
                    coveredCodeIssues.Length,
                    codeIssues.Count(),
                    "Not all occurances of {0} are covered with tests",
                    ruleCheck);
            }

        }
    }
}
