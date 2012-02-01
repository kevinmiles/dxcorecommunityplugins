namespace CR_StyleCop.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CR_StyleCop.Tests.Helpers;
    using DevExpress.CodeRush.StructuralParser;
    using MbUnit.Framework;
    using StyleCop;

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
            this.AssertSpecificCodeIssueExists(ruleCheck, startLine, startOffset, endLine, endOffset, string.Empty);
        }

        private void AssertSpecificCodeIssueExists(string ruleCheck, int startLine, int startOffset, int endLine, int endOffset, string fileNameSuffix)
        {
            foreach (var file in files.Where(x => Path.GetFileName(x.Name) == string.Format("{0}TestCode{1}.cs", ruleCheck, fileNameSuffix)))
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

        private void AssertAllReportedCodeIssuesAreTested(string ruleCheck)
        {
            var method = typeof(RulesTests).GetMethod(ruleCheck + "_should_be_reported");
            if (method == null)
            {
                return;
            }

            var coveredCodeIssues = method.GetCustomAttributes(typeof(CodeIssueAttribute), false).Cast<CodeIssueAttribute>();
            foreach (var file in files.Where(x => x.Name.StartsWith(string.Format("{0}TestCode", ruleCheck))))
            {
                var codeIssues = plugin.GetCodeIssuesFor(file);
                foreach (var codeIssue in codeIssues.Where(x => x.Message.StartsWith(ruleCheck)))
                {
                    Assert.Exists(
                        coveredCodeIssues,
                        x => x.StartLine == codeIssue.Range.Start.Line
                            && x.StartOffset == codeIssue.Range.Start.Offset
                            && x.EndLine == codeIssue.Range.End.Line
                            && x.EndOffset == codeIssue.Range.End.Offset,
                        "Not all occurrences of {0} are covered with tests. Issue from line {1}, col {2} to line {3}, col {4} is not covered.",
                        ruleCheck,
                        codeIssue.Range.Start.Line,
                        codeIssue.Range.Start.Offset,
                        codeIssue.Range.End.Line,
                        codeIssue.Range.End.Offset);
                }
            }
        }

        private void AssertAllStyleCopReportedViolationsHaveCodeIssue(string ruleCheck)
        {
            var comparer = new ViolationComparer();
            foreach (var file in files.Where(x => x.Name.StartsWith(string.Format("{0}TestCode", ruleCheck))))
            {
                var codeIssues = plugin.GetCodeIssuesFor(file);
                var violations = plugin.GetStyleCopViolations(file, ruleCheck);
                foreach (var violation in violations)
                {
                    Assert.Exists(
                        codeIssues, 
                        x => comparer.Equals(violation, x.Data as Violation), 
                        "{0} violation is not reported as code issue: Ln {1}", 
                        ruleCheck, 
                        violation.Line);
                }
            }
        }
    }
}
