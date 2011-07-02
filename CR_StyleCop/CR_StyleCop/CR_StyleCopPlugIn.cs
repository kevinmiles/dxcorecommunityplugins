namespace CR_StyleCop
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using CR_StyleCop.CodeIssues;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.Diagnostics.General;
    using DevExpress.CodeRush.PlugInCore;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;

    public partial class CR_StyleCopPlugIn : StandardPlugIn
    {
        private bool checkForSuppressions = true;
        private Dictionary<string, List<Violation>> violations;
        private CodeIssueFactory issuesFactory;
        private ObjectBasedEnvironment environment;
        private StyleCopObjectConsole styleCopConsole;

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

        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            this.violations = new Dictionary<string, List<Violation>>();
            this.issuesFactory = new CodeIssueFactory();
            this.environment = new ObjectBasedEnvironment(this.SourceCodeFactory, this.SettingsFactory);
            this.styleCopConsole = new StyleCopObjectConsole(this.environment, null, null, true);
            this.styleCopConsole.ViolationEncountered += this.OnViolationEncountered;
        }

        public override void FinalizePlugIn()
        {
            this.styleCopConsole.ViolationEncountered -= this.OnViolationEncountered;
            this.styleCopConsole.Dispose();
            this.styleCopConsole = null;

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

            this.violations.Clear();

            string settingsFolder = this.GetProjectFolder(project);
            var configuration = new Configuration(new[] { "DEBUG", "TRACE" });
            var styleCopProject = new CodeProject(project.FullName.GetHashCode(), settingsFolder, configuration);
            ISourceCode sourceCode = this.SetupSourceCode(scope);
            this.environment.AddSourceCode(styleCopProject, scope.FilePath, sourceCode);
            this.styleCopConsole.Start(new List<CodeProject> { styleCopProject });

            foreach (var violationList in this.violations.Values)
            {
                foreach (var violation in violationList)
                {
                    IStyleCopRule rule = this.issuesFactory.GetRuleFor(violation);
                    rule.AddViolationIssue(ea, sourceCode, violation);
                }
            }
        }

        private SourceCode SourceCodeFactory(string path, CodeProject project, SourceParser parser, object context)
        {
            string codeToAnalyze = context.ToString();
            return new AnalyzedSourceCode(project, parser, path, codeToAnalyze);
        }

        private Settings SettingsFactory(string path, bool readOnly)
        {
            try
            {
                string settingsPath = Path.Combine(path, "Settings.StyleCop");
                if (!File.Exists(settingsPath))
                {
                    return null;
                }

                var document = new XmlDocument();
                document.Load(settingsPath);
                var writeTime = File.GetLastWriteTime(settingsPath);

                if (readOnly)
                {
                    return new Settings(this.styleCopConsole.Core, settingsPath, document, writeTime);
                }
                else
                {
                    return new WritableSettings(this.styleCopConsole.Core, settingsPath, document, writeTime);
                }
            }
            catch (IOException ioex)
            {
                Log.SendException(ioex);
                return null;
            }
            catch (UnauthorizedAccessException uaex)
            {
                Log.SendException(uaex);
                return null;
            }
            catch (XmlException xmlex)
            {
                Log.SendException(xmlex);
                return null;
            }
        }

        private string GetProjectFolder(ProjectElement project)
        {
            return Path.GetDirectoryName(project.FilePath);
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