/*
 * Software License Agreement for RedGreen
 * 
 * Copyright (c) 2010 Renaissance Learning, Inc. and James Argeropoulos
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace Impromptu
{
    public partial class PlugIn1 : StandardPlugIn
    {
        private static string performerPath = LocateExe();
        private bool _DisplayTile;

        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            bool displayTile = LoadSettings();
            _DisplayTile = displayTile;
        }
        private static bool LoadSettings()
        {
            using (DecoupledStorage storage = ImpromptuOptions.Storage)
            {
                return ImpromptuOptions.ReadDisplayIcon(storage);
            }
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

        #region Read Assembly Path from Project
        /// <summary>
        /// Get the assembly path for a project
        /// </summary>
        private static string GetAssemblyPath()
        {
            EnvDTE.Project vsProject = GetActiveProject();
            if (vsProject != null)
            {
                string outputDir = Path.Combine(GetPropertyValue(vsProject.Properties, "FullPath"),
                    GetPropertyValue(vsProject.ConfigurationManager.ActiveConfiguration.Properties, "OutputPath"));
                return Path.Combine(outputDir, GetPropertyValue(vsProject.Properties, "OutputFileName"));
            }
            return string.Empty;
        }

        /// <summary>
        /// Get the VS project object for the current project
        /// </summary>
        /// <remarks>Old code from when I first started could use a review for better practice</remarks>
        private static EnvDTE.Project GetActiveProject()
        {
            // try to get the current active project
            ProjectElement activeProj = CodeRush.Source.ActiveProject;
            if (activeProj != null)
                return CodeRush.Solution.FindEnvDTEProject(activeProj.Name);
            else
            {  // if no active project found...
                // see if there is just one project in the solution, and if so, return that
                EnvDTE.Solution solution = CodeRush.ApplicationObject.Solution;

                if (solution != null || solution.Projects.Count == 1)
                {
                    return solution.Projects.Item(1);// One based so that the system works with VBA
                }
            }
            return null;
        }

        /// <summary>
        /// Helper function to get the text from a project item
        /// </summary>
        private static string GetPropertyValue(EnvDTE.Properties source, string name)
        {
            if (source == null || string.IsNullOrEmpty(name))
                return string.Empty;

            EnvDTE.Property property = source.Item(name);
            return property != null ? property.Value.ToString() : String.Empty;
        }
        #endregion

        #region Write to output pane
        /// <summary>
        /// Fetch the test output window
        /// </summary>
        /// <returns></returns>
        private static EnvDTE.OutputWindowPane GetImpromptuOutputPane()
        {
            EnvDTE.OutputWindow outputWindow = CodeRush.Windows.Active.DTE.Windows.Item(EnvDTE.Constants.vsWindowKindOutput).Object as EnvDTE.OutputWindow;
            const string kTestsPane = "Impromptu";
            EnvDTE.OutputWindowPane ourPane;
            try
            {
                ourPane = outputWindow.OutputWindowPanes.Item(kTestsPane);
                ourPane.Clear();
            }
            catch
            {
                ourPane = outputWindow.OutputWindowPanes.Add(kTestsPane);
            }
            System.Diagnostics.Debug.Assert(ourPane != null);
            ourPane.Activate();
            return ourPane;
        }

        /// <summary>
        /// Write a line of text to the test pane
        /// </summary>
        internal static void WriteToImpromptuPane(string text)
        {
            WriteToImpromptuPane(text, null);
        }
        internal static void WriteToImpromptuPane(string text, EnvDTE.OutputWindowPane destination)
        {
            if (!string.IsNullOrEmpty(text))
            {
                destination = destination ?? GetImpromptuOutputPane();
                if (destination != null)
                {
                    destination.OutputString(text);
                }
            }
        }
        #endregion

        private static bool BuildActiveProject()
        {
            EnvDTE.Solution sol = CodeRush.Solution.Active;
            sol.SolutionBuild.Build(true); // We build the whole solution becuase some projects are not configured to build the projects they depend upon first.
            return sol.SolutionBuild.LastBuildInfo == 0;
        }

        /// <summary>
        /// Opens the output window if needed and sets the focus to it.
        /// </summary>
        private static void ShowBuildOutputWindow()
        {
            ShowOutputWindowPane("Build");
        }

        /// <summary>
        /// Opens the output window if needed and sets the focus to it.
        /// </summary>
        private static void ShowImpromptuOutputWindow()
        {
            ShowOutputWindowPane("Impromptu");
        }

        private static void ShowOutputWindowPane(string paneName)
        {
            EnvDTE.OutputWindow outputWindow = CodeRush.Windows.Active.DTE.Windows.Item(EnvDTE.Constants.vsWindowKindOutput).Object as EnvDTE.OutputWindow;
            EnvDTE.OutputWindowPane buildPane = outputWindow.OutputWindowPanes.Item(paneName);
            System.Diagnostics.Debug.Assert(buildPane != null);
            buildPane.Activate();
            outputWindow.Parent.SetFocus();
        }

        internal static void RunMethod(Method method)
        {
            if (performerPath == String.Empty)
            {
                return;
            }
            if (BuildActiveProject() == false)
            {
                ShowBuildOutputWindow();
                return;
            }
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                string assemblyPath = GetAssemblyPath();
                string typeName = ((Class)method.Parent).FullName;
                string methodName = method.Name;
                EnvDTE.OutputWindowPane resultPane = GetImpromptuOutputPane();
                WriteToImpromptuPane(
                    string.Format(
                        "Running Impromptu test for Assembly: {0}, Type: {1}, Method: {2}\r\n",
                        Path.GetFileName(assemblyPath),
                        typeName,
                        methodName),
                    resultPane);

                StreamReader sr = null;
                using (Process p = new Process())
                {
                    p.StartInfo = new ProcessStartInfo(performerPath);
                    p.StartInfo.Arguments = string.Format("/assemblyPath:\"{0}\" /TypeName:{1} /MethodName:{2}", assemblyPath, typeName, methodName);
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    sr = p.StandardOutput;
                }
                string line = sr.ReadLine();
                while (line != null)
                {
                    WriteToImpromptuPane(string.Format("{0}\n", line), resultPane);
                    line = sr.ReadLine();
                }
            }
            catch (Exception ex)
            {
                WriteToImpromptuPane(ex.Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                ShowImpromptuOutputWindow();
            }
        }

        private static string LocateExe()
        {
            string installFolder;
            GetInstallFolderFromRegistry();            
            // Okay, no registry data. See if we can find it by using our assembly location
            installFolder = Path.GetDirectoryName(typeof(MyClass).Assembly.Location);

            string exePath = Path.Combine(installFolder, "Impromptu.Performer.exe");
            if (File.Exists(exePath))
                return exePath;
            WriteToImpromptuPane("Unable to locate Impromptu.Performer.Exe. Please ensure that it is in your Community Plugins folder with the Impromptu plugin.");
            ShowImpromptuOutputWindow();
            return String.Empty;
        }

        private static void GetInstallFolderFromRegistry()
        {
            string installFolder;
            Microsoft.Win32.RegistryKey communityPathKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Developer Express\\CodeRush for VS\\CommunityPlugIns");
            if (communityPathKey == null)
            {// Yet another Location to choose from. I don't like it when people push key's around!
                communityPathKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Developer Express\\CodeRush for VS\\Community");
            }
            if (communityPathKey != null)
            {
                installFolder = communityPathKey.GetValue("InstallFolder").ToString();
                if (false == installFolder.EndsWith("plugins"))
                {
                    installFolder = Path.Combine(installFolder, "Plugins");
                }
            }
        }
        private void impromptuActions_Execute(ExecuteEventArgs ea)
        {
            Method method = CodeRush.Source.ActiveMethod;
            if (method != null)
                if (CanBeRun(method))
                {
                    RunMethod(method);
                }
                else
                {
                    WriteToImpromptuPane(string.Format("Impromptu can run {0}. It will only run methods with no parmeters.", method.Name));
                    ShowImpromptuOutputWindow();
                }
        }
        private static bool CanBeRun(Method method)
        {
            return (method != null
                    && method.IsClassOperator == false
                    && method.IsConstructor == false
                    && method.IsDestructor == false
                    && method.IsExplicitCast == false
                    && method.IsImplicitCast == false
                    && method.ParameterCount == 0);
        }

        private void PlugIn1_DecorateLanguageElement(object sender, DecorateLanguageElementEventArgs args)
        {
            LanguageElement element = args.LanguageElement;
            if (_DisplayTile && element.ElementType == LanguageElementType.Method && CanBeRun((Method)element))
            {
                DocPoint start = new DocPoint(element.StartLine, element.StartOffset);
                args.AddAdornment(new RunMethodTileDocumentAdornment(start, start, this, (Method)element));
            }
        }

        private void PlugIn1_OptionsChanged(OptionsChangedEventArgs ea)
        {
            _DisplayTile = LoadSettings();
        }
    }
    /// <summary>
    /// A dummy class just used to get a reference to this assembly
    /// </summary>
    internal class MyClass
    {
        
        public MyClass()
        {
            
        }
    }
}