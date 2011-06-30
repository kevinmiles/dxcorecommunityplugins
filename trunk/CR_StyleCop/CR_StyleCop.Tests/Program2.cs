namespace CR_StyleCop
{
    using System;
    using System.Collections.Generic;
    using DevExpress.CodeRush.StructuralParser;
    using DevExpress.CodeRush.Core;
    using System.IO;
    using StyleCop;
    using DevExpress.CodeRush.Diagnostics.General;
    using System.Xml;

    class Program
    {
        private static Dictionary<string, List<Violation>> violations = new Dictionary<string, List<Violation>>();
        private static StyleCopObjectConsole styleCopConsole;

        static void Main(string[] args)
        {
            try
            {
                string path = "..\\..\\..\\CR_StyleCop.TestCode\\SA1000TestCode.cs";
                string code = File.ReadAllText(path);
                var environment = new ObjectBasedEnvironment(Program.SourceCodeFactory, SettingsFactory);
                styleCopConsole = new StyleCopObjectConsole(environment, null, null, true);
                styleCopConsole.ViolationEncountered += OnViolationEncountered;

                string settingsFolder = "..\\..\\..\\CR_StyleCop.TestCode";
                var configuration = new Configuration(new[] { "DEBUG", "TRACE" });
                var styleCopProject = new CodeProject("CR_StyleCop".GetHashCode(), settingsFolder, configuration);
                environment.AddSourceCode(styleCopProject, path, code);
                styleCopConsole.Start(new List<CodeProject> { styleCopProject });

                foreach (var violationList in violations.Values)
                {
                    foreach (var violation in violationList)
                    {
                        Console.WriteLine("Violation detected on line {0}: {1}", violation.Line, violation.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            Console.ReadLine();
        }

        private static SourceCode SourceCodeFactory(string path, CodeProject project, SourceParser parser, object context)
        {
            string codeToAnalyze = context.ToString();
            return new AnalyzedSourceCode(project, parser, path, codeToAnalyze);
        }

        private static Settings SettingsFactory(string path, bool readOnly)
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
                    return new Settings(styleCopConsole.Core, settingsPath, document, writeTime);
                }
                else
                {
                    return new WritableSettings(styleCopConsole.Core, settingsPath, document, writeTime);
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

        private static void OnViolationEncountered(object sender, ViolationEventArgs e)
        {
            List<Violation> list;
            if (!violations.TryGetValue(e.Violation.Rule.CheckId, out list))
            {
                list = new List<Violation>();
                violations.Add(e.Violation.Rule.CheckId, list);
            }

            list.Add(e.Violation);
        }
    }
}
