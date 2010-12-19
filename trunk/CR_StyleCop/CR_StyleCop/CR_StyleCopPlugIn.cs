namespace CR_StyleCop
{
    using System;
    using System.Collections.Generic;
    using CR_StyleCop.CodeIssues;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.PlugInCore;
    using DevExpress.CodeRush.StructuralParser;
    using Microsoft.StyleCop;

    public partial class CR_StyleCopPlugIn : StandardPlugIn
    {
        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            //
            // TODO: Add your initialization code here.
            //
        }
        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            //
            // TODO: Add your finalization code here.
            //

            base.FinalizePlugIn();
        }
        #endregion

        private Dictionary<string, List<Violation>> violations = new Dictionary<string, List<Violation>>();
        private CodeIssueFactory factory = new CodeIssueFactory();

        private void styleCopIssueProvider_LanguageSupported(LanguageSupportedEventArgs ea)
        {
            ea.Handled = ea.LanguageID.ToLower() == "csharp";
        }

        private void styleCopIssueProvider_CheckCodeIssues(object sender, CheckCodeIssuesEventArgs ea)
        {
            if (ea.IsSuppressed(ea.Scope))
                return;

            var scope = ea.Scope as SourceFile;
            if (scope == null || scope.Document == null)
                return;

            var project = scope.Project as ProjectElement;
            if (project == null)
                return;

            using (StyleCopObjectConsole styleCopConsole = new StyleCopObjectConsole(new ObjectBasedEnvironment(this.CodeFactory, this.SettingsFactory), null, null, true))
            {
                Configuration configuration = new Configuration(new string[] { "DEBUG" });
                CodeProject styleCopProject = new CodeProject(project.FullName.GetHashCode(), project.FullName, configuration);
                string analyzedCode = scope.Document.GetText(scope.StartLine, scope.StartOffset, scope.EndLine, scope.EndOffset);
                styleCopConsole.Core.Environment.AddSourceCode(styleCopProject, scope.FilePath, analyzedCode);
                this.violations.Clear();
                styleCopConsole.ViolationEncountered += this.OnViolationEncountered;
                styleCopConsole.Start(new List<CodeProject> { styleCopProject });
                styleCopConsole.ViolationEncountered -= this.OnViolationEncountered;
            }

            foreach (var violationList in this.violations.Values)
            {
                foreach (var violation in violationList)
                {
                    ICodeIssue issue = this.factory.GetIssueFor(violation);
                    IDocument document = scope.Document;
                    if (document != null)
                    {
                        issue.AddViolationIssue(ea, document, violation);
                    }
                }
            }
        }

        private Microsoft.StyleCop.SourceCode CodeFactory(string path, CodeProject project, SourceParser parser, object context)
        {
            string codeToAnalyze = (string)context;
            return new AnalyzedSourceCode(project, parser, path, codeToAnalyze);
        }

        private Microsoft.StyleCop.Settings SettingsFactory(string path, bool readOnly)
        {
            return null;
        }

        private void OnViolationEncountered(object sender, ViolationEventArgs e)
        {
            List<Violation> list;
            if (!this.violations.TryGetValue(e.Violation.Rule.CheckId, out list))
            {
                list = new List<Violation>();
                this.violations.Add(e.Violation.Rule.CheckId, list);
            }
            list.Add(e.Violation);
        }
    }
}