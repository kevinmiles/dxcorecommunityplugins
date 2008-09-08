/*
 * Software License Agreement for RedGreen
 * 
 * Copyright (c) 2008 Renaissance Learning, Inc. and James Argeropoulos
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Xml;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace RedGreen
{
    /// <summary>
    /// This plug-in
    /// </summary>
    public partial class PlugIn1 : StandardPlugIn
    {
        private List<ITestRunner> _testRunners = new List<ITestRunner>(new ITestRunner[] { /*new XunitRunner(),*/ new GallioRunner() });
        private List<TestResult> _testResults = new List<TestResult>();

        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            foreach (ITestRunner runner in _testRunners)
            {
                runner.TestComplete += new TestCompleteEventHandler(runner_TestComplete);
                runner.AllTestsComplete += new AllTestsCompleteEventHandler(runner_AllTestsComplete);
            }
        }
        #endregion

        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            base.FinalizePlugIn();
        }
        #endregion

        #region Run Tests
        private void actRunTest_Execute(ExecuteEventArgs ea)
        {
            ClearPriorResults();

            Class selectedClass = CodeRush.Source.ActiveClass;
            Method selectedMethod = CodeRush.Source.ActiveMethod;
            string assemblyPath = GetAssemblyPath(GetActiveProject());
            string assemblyName = GetAssemblyName(GetActiveProject());
            if (selectedMethod != null)
            {
                TestOneMethod(assemblyPath, assemblyName, selectedClass, selectedMethod);
            }
            else if (selectedClass != null)
            {
                TestOneClass(assemblyPath, assemblyName, selectedClass);
            }
        }

        private void TestOneClass(string assemblyPath, string assemblyName, Class selectedClass)
        {
            BuildActiveProject();

            string testFramework = GetTestFramework();
            foreach (ITestRunner runner in _testRunners)
            {
                if (runner.RunsTestsForNamespace(testFramework))
                {
                    runner.RunClass(assemblyPath, assemblyName, selectedClass.FullName);
                    break;
                }
            }
        }

        private void TestOneMethod(string assemblyPath, string assemblyName, Class selectedClass, Method selectedMethod)
        {
            string testFramework = GetTestFramework();
            foreach (ITestRunner runner in _testRunners)
            {
                if (runner.RunsTestsForNamespace(testFramework))
                {
                    if (runner.IsTest(selectedMethod))
                    {
                        BuildActiveProject();
                        runner.RunMethod(assemblyPath, assemblyName, selectedClass.FullName, selectedMethod.Name);
                    }
                }
            }
        }

        void runner_TestComplete(object sender, TestCompleteEventArgs args)
        {
            EnvDTE.OutputWindowPane testPane = GetTestingOutputPane();
            if (testPane == null)
                return;

            if (args.RawResult.Length > 0)
            {
                testPane.OutputString(args.RawResult);
                testPane.OutputString(Environment.NewLine);
                testPane.OutputString(Environment.NewLine);
            }
            _testResults.Add(args.Result);
        }

        void runner_AllTestsComplete(object sender, AllTestsCompleteEventArgs args)
        {
            string overview = string.Format("\n{0} passed, {1} failed, {2} skipped, duration: {3} seconds\n",
                                                                                          args.PassCount,
                                                                                          args.FailCount,
                                                                                          args.SkipCount,
                                                                                          args.Duration);
            EnvDTE.OutputWindowPane resultsPane = GetTestingOutputPane();
            if (resultsPane == null)
                return;

            resultsPane.OutputString(overview);
            CodeRush.Windows.Active.DTE.StatusBar.Text = overview;

            DxCoreUtil.Invalidate(CodeRush.Source.ActiveClass);
        }

        private static EnvDTE.OutputWindowPane GetTestingOutputPane()
        {
            EnvDTE.OutputWindow outputWindow = CodeRush.Windows.Active.DTE.Windows.Item(EnvDTE.Constants.vsWindowKindOutput).Object as EnvDTE.OutputWindow;
            const string kTestsPane = "Testing";
            EnvDTE.OutputWindowPane testPane;
            try
            {
                testPane = outputWindow.OutputWindowPanes.Item(kTestsPane);
                testPane.Clear();
            }
            catch (ArgumentException /*ex*/)
            {
                testPane = outputWindow.OutputWindowPanes.Add(kTestsPane);
            }
            System.Diagnostics.Debug.Assert(testPane != null);
            testPane.Activate();
            return testPane;
        }

        private string GetTestFramework()
        {
            foreach (System.Collections.DictionaryEntry reference in CodeRush.Source.NamespaceReferences)
            {
                string referenceName = reference.Value.ToString();
                foreach (ITestRunner runner in _testRunners)
                {
                    if (runner.RunsTestsForNamespace(referenceName))
                        return referenceName;
                }
            }
            return String.Empty;
        }

        private EnvDTE.Project GetActiveProject()
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
                    return solution.Projects.Item(0);
                }
            }
            return null;
        }

        static string GetAssemblyPath(EnvDTE.Project vsProject)
        {
            if (vsProject != null)
            {
                string fullPath = vsProject.Properties.Item("FullPath").Value.ToString();
                string outputPath = vsProject.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value.ToString();
                string outputDir = Path.Combine(fullPath, outputPath);
                return Path.Combine(outputDir, vsProject.Properties.Item("OutputFileName").Value.ToString());
            }
            return String.Empty;
        }

        static string GetAssemblyName(EnvDTE.Project vsProject)
        {
            if (vsProject != null)
            {
                return vsProject.Properties.Item("AssemblyName").Value.ToString();
            }
            return String.Empty;
        }

        private static void BuildActiveProject()
        {
            EnvDTE.Solution sol = CodeRush.Solution.Active;
            sol.SolutionBuild.Build(true);
        }
        #endregion

        #region Redraw test attribute
        private readonly Color Passed = Color.FromArgb(157, 254, 133);
        private readonly Color Skipped = Color.FromArgb(255, 255, 70);
        private readonly Color Failed = Color.FromArgb(255, 140, 140);
        private Color Unknown = Color.White;

        private void ClearPriorResults()
        {
            List<TestResult> temp = new List<TestResult>(_testResults);
            _testResults.Clear();

            if (temp.Count > 0)
            {
                DxCoreUtil.Invalidate(CodeRush.Source.ActiveClass);
            }
        }

        private void PlugIn1_EditorPaintLanguageElement(EditorPaintLanguageElementEventArgs ea)
        {
            try
            {
                RedrawTestAttribute(ea);
                DrawError(ea);
            }
            finally
            {

            }
        }

        private void RedrawTestAttribute(EditorPaintLanguageElementEventArgs ea)
        {
            if (IsTestAttribute(ea.LanguageElement))
            {
                if (!ea.LanguageElement.Range.Contains(new SourcePoint(ea.PaintArgs.CaretLine, ea.PaintArgs.CaretOffset)))
                {
                    LanguageElement attribute = ea.LanguageElement;
                    if (ea.PaintArgs.LineInView(attribute.StartLine))
                    {
                        LanguageElement method = GetMethodAttachedTo(attribute);
                        StringBuilder displayText = GetDisplayText(attribute, method);
                        ea.PaintArgs.OverlayText(displayText.ToString(),
                            attribute.StartLine,
                            attribute.StartOffset - 1,
                            Color.Black,
                            GetBackgroundColor(method.Location));
                    }
                }
            }
        }

        private bool IsTestAttribute(LanguageElement potentialTest)
        {
            foreach (ITestRunner runner in _testRunners)
            {
                if (runner.IsTestAttribute(potentialTest))
                {
                    return true;
                }
            }
            return false;
        }

        private void DrawError(EditorPaintLanguageElementEventArgs ea)
        {
            TestResult currentResult = GetResultForLocation(ea.LanguageElement.Location);
            if (currentResult != null && currentResult.Result == TestStatus.Failed)
            {
                LanguageElement statement = DxCoreUtil.GetStatement(currentResult.AssertLocation, currentResult.FailAtLine);
                if (statement != null)
                {
                    int errorTextStartCol = statement.EndOffset + 5;
                    if (string.IsNullOrEmpty(currentResult.Expected))
                    {// not an equal comparison
                        ea.PaintArgs.OverlayText("<------- Test failed here",
                            currentResult.FailAtLine,
                            errorTextStartCol,
                            Failed);

                    }
                    else if (currentResult.Position < 0)
                    {
                        ea.PaintArgs.OverlayText(string.Format("Expected: {0} Actual: {1}", currentResult.Expected, currentResult.Actual),
                            currentResult.FailAtLine,
                            errorTextStartCol,
                            Failed);
                    }
                    else
                    {
                        int start = errorTextStartCol;
                        string correctPortion = string.Format("Expected: {0} Actual: {1}", currentResult.Expected, currentResult.Actual.Substring(0, currentResult.Position));
                        ea.PaintArgs.OverlayText(correctPortion,
                            currentResult.FailAtLine,
                            start,
                            Failed);
                        ea.PaintArgs.OverlayText(currentResult.Actual.Substring(currentResult.Position),
                            currentResult.FailAtLine,
                            start + correctPortion.Length,
                            Color.Red);
                    }
                }
            }
        }

        /// <summary>
        /// Walk the node tree down to the method that this attribute decorates
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private LanguageElement GetMethodAttachedTo(LanguageElement node)
        {
            System.Diagnostics.Debug.Assert(node.ElementType == LanguageElementType.Attribute);

            LanguageElement method = node.NextNode;
            while (method != null)
            {
                if (method.ElementType == LanguageElementType.Method)
                {
                    return method;
                }
                method = method.NextNode;
            }
            return node;
        }

        /// <summary>
        /// Look up the method location in the test results. If found use the test results to pick the background color
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        private Color GetBackgroundColor(string location)
        {
            if (!string.IsNullOrEmpty(location))
            {
                TestResult result = GetResultForLocation(location);
                if (result != null)
                {
                    switch (result.Result)
                    {
                        case TestStatus.Unknown:
                        default:
                            return Unknown;
                        case TestStatus.Skipped:
                            return Skipped;
                        case TestStatus.Passed:
                            return Passed;
                        case TestStatus.Failed:
                            return Failed;
                    }
                }
            }
            return Unknown;
        }

        /// <summary>
        /// Find a test result for the given location 
        /// </summary>
        /// <param name="location">What result to find</param>
        /// <returns>A result or null</returns>
        private TestResult GetResultForLocation(string location)
        {
            return _testResults.Find(delegate(TestResult testResult)
                                {
                                    return (LocationMatchesFull(testResult.MethodLocation, location)
                                          || LocationMatchesAfterRootNs(testResult.MethodLocation, location));
                                });
        }

        /// <summary>
        /// Check to see if both locations match
        /// </summary>
        /// <param name="resultLocation">The method location reported by the test framework</param>
        /// <param name="iteratingLocation">The method location given when iterating to redraw a method attribute</param>
        /// <returns>True if 100% match</returns>
        private static bool LocationMatchesFull(string resultLocation, string iteratingLocation)
        {
            return resultLocation == iteratingLocation;
        }

        /// <summary>
        /// Check to see if both match when the test result has the root namespace removed
        /// </summary>
        /// <param name="resultLocation">The method location reported by the test framework</param>
        /// <param name="iteratingLocation">The method location given when iterating to redraw a method attribute</param>
        /// <returns>True if the locations match after the root namespace is removed from the test location</returns>
        /// <remarks>This is the case for VB. I am not sure why. It may be I just don't know enough about VB.</remarks>
        private static bool LocationMatchesAfterRootNs(string resultLocation, string iteratingLocation)
        {
            return resultLocation.Substring(resultLocation.IndexOf(".") + 1) == iteratingLocation;
        }

        /// <summary>
        /// Use the width of the method the attribute is attached to to determine how much 
        /// trailing whitespace is needed for the colored bar.
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns>Test plus enough whitespace to make the colored bar extent one char past the end of the closing paren</returns>
        private static StringBuilder GetDisplayText(LanguageElement attribute, LanguageElement method)
        {
            TextView view = method.View as TextView;

            StringBuilder displayText = new StringBuilder(" Test");
            int attributeOffset = attribute.StartOffset; // Doesn't include the opening square bracket
            int lineLength = view.LengthOfLine(method.StartLine);
            displayText.Append(' ', lineLength - attributeOffset - 3); // -3 to account for addition of " test" and yet cover past the parens too.
            return displayText;
        }
#endregion

        private void PlugIn1_SolutionOpened()
        {
            _testResults.Clear();
        }

        private void actNextError_Execute(ExecuteEventArgs ea)
        {
            // Move to next failed test
        }
    }
}