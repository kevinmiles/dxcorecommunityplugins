namespace CR_StyleCop
{
    using System;
    using System.Collections.Generic;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.PlugInCore;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;

    public partial class CR_StyleCopPlugIn : StandardPlugIn
    {
        private bool checkForSuppressions = true;
        private CodeIssueFactory issuesFactory;
        private StyleCopRunner styleCopRunner;

        public IEnumerable<CodeIssue> GetCodeIssuesFor(IElement element)
        {
            this.checkForSuppressions = false;
            try
            {
                return styleCopIssueProvider.GetCodeIssues(element);
            }
            finally
            {
                this.checkForSuppressions = true;
            }
        }

        public IEnumerable<Violation> GetStyleCopViolations(SourceFile file, string ruleCheck)
        {
            return this.styleCopRunner.GetViolations(file.Project, this.SetupSourceCode(file))[ruleCheck];
        }

        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            this.issuesFactory = new CodeIssueFactory();
            this.styleCopRunner = new StyleCopRunner();
        }

        public override void FinalizePlugIn()
        {
            this.styleCopRunner.Dispose();
            this.styleCopRunner = null;

            base.FinalizePlugIn();
        }

        private void StyleCopIssueProvider_LanguageSupported(LanguageSupportedEventArgs ea)
        {
            ea.Handled = ea.LanguageID.ToLower() == "csharp";
        }

        private void StyleCopIssueProvider_CheckCodeIssues(object sender, CheckCodeIssuesEventArgs ea)
        {
            if (this.checkForSuppressions && ea.IsSuppressed(ea.Scope))
            {
                return;
            }

            var scope = ea.Scope as SourceFile;
            if (scope == null)
            {
                return;
            }

            var project = scope.Project as ProjectElement;
            if (project == null)
            {
                return;
            }

            ISourceCode sourceCode = this.SetupSourceCode(scope);
            foreach (var violationList in this.styleCopRunner.GetViolations(project, sourceCode).Values)
            {
                foreach (var violation in violationList)
                {
                    IStyleCopRule rule = this.issuesFactory.GetRuleFor(violation);
                    rule.AddViolationIssue(ea, sourceCode, violation);
                }
            }
        }

        private ISourceCode SetupSourceCode(SourceFile scope)
        {
            if (scope.Document != null)
            {
                return new VSSourceCode(scope);
            }
            else
            {
                return new FileSourceCode(scope.FilePath);
            }
        }
    }
}