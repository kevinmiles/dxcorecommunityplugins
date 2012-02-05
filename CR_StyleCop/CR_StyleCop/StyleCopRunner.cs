namespace CR_StyleCop
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using DevExpress.CodeRush.Diagnostics.General;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;

    internal class StyleCopRunner : IDisposable
    {
        private Dictionary<string, List<Violation>> violations;
        private ObjectBasedEnvironment environment;
        private Configuration configuration;
        private StyleCopObjectConsole styleCopConsole;

        public StyleCopRunner()
        {
            this.violations = new Dictionary<string, List<Violation>>();
            this.environment = new ObjectBasedEnvironment(this.SourceCodeFactory, this.SettingsFactory);
            this.configuration = new Configuration(new[] { "DEBUG", "TRACE" });
            this.styleCopConsole = new StyleCopObjectConsole(this.environment, null, null, true);
            this.styleCopConsole.ViolationEncountered += this.OnViolationEncountered;
        }

        public IDictionary<string, List<Violation>> GetViolations(IProjectElement project, ISourceCode sourceCode)
        {
            var styleCopProject = new CodeProject(
                project.FullName.GetHashCode(), 
                this.GetProjectFolder(project), 
                this.configuration);

            this.violations.Clear();
            this.environment.AddSourceCode(styleCopProject, sourceCode.FilePath, sourceCode);
            this.styleCopConsole.Start(new List<CodeProject> { styleCopProject });
            return new Dictionary<string, List<Violation>>(this.violations);
        }

        public void Dispose()
        {
            this.styleCopConsole.ViolationEncountered -= this.OnViolationEncountered;
            this.styleCopConsole = null;
            this.environment = null;
            this.configuration = null;
            this.violations = null;
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

        private string GetProjectFolder(IProjectElement project)
        {
            return Path.GetDirectoryName(project.FilePath);
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
                string settingsPath = path.EndsWith(Settings.DefaultFileName) || path.EndsWith(Settings.AlternateFileName)
                    ? path
                    : Path.Combine(path, Settings.DefaultFileName);
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
    }
}
