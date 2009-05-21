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

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace RedGreen
{
    /// <summary>
    /// This plug-in
    /// </summary>
    internal partial class PlugIn1 : StandardPlugIn
    {
        private const string kRunTestMenuItem = "Run Test";
        private const string kRunClassMenuItem = "Run All Tests in Class";
        private const string kRunAssemblyMenuItem = "Run All Tests in Assembly";
        private const string kNextFailedTestMenuItem = "Go to Next Failed Test";
        private const int kDefaultCurrentFailure = -1;
        private const string kTestingStartedMessage = "Testing started...";
        private List<UnitTestDetail> _Tests = new List<UnitTestDetail>();
        private List<UnitTestDetail> _Failures = new List<UnitTestDetail>();
        private int _currentFailure = kDefaultCurrentFailure;
        private ITestDetail _hoveredTest;
        static readonly TestPopupMenuColors sTestMenuColors = new TestPopupMenuColors();

        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
        }
        #endregion

        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            base.FinalizePlugIn();

        }
        #endregion

        #region Build Project
        /// <summary>
        /// Build the current project.
        /// </summary>
        /// <returns>
        /// returns true if build passed 
        /// </returns>
        private static bool BuildActiveProject()
        {
            EnvDTE.Solution sol = CodeRush.Solution.Active;
            //EnvDTE.Project project = GetActiveProject();
            sol.SolutionBuild.Build(true); // We build the whole solution becuase some projects are not configured to build the projects they depend upon first.
            //sol.SolutionBuild.BuildProject(project.ConfigurationManager.ActiveConfiguration.ConfigurationName, project.UniqueName, true);
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
        private static void ShowTestOutputWindow()
        {
            ShowOutputWindowPane("Testing");
        }

        private static void ShowOutputWindowPane(string paneName)
        {
            EnvDTE.OutputWindow outputWindow = CodeRush.Windows.Active.DTE.Windows.Item(EnvDTE.Constants.vsWindowKindOutput).Object as EnvDTE.OutputWindow;
            EnvDTE.OutputWindowPane buildPane = outputWindow.OutputWindowPanes.Item(paneName);
            System.Diagnostics.Debug.Assert(buildPane != null);
            buildPane.Activate();
            outputWindow.Parent.SetFocus();
        }

        /// <summary>
        /// Clear the prior results because a build invalidates our knowledge
        /// </summary>
        private void PlugIn1_BuildDone(EnvDTE.vsBuildScope scope, EnvDTE.vsBuildAction action)
        {
            ResetTestResults();
        }
        #endregion

        #region Run Test Tile Interaction
        /// <summary>
        /// Force a tile redraw when the cursor enters a tile
        /// </summary>
        private void PlugIn1_TileMouseEnter(object sender, TileEventArgs ea)
        {
            ea.Tile.Invalidate();
        }

        /// <summary>
        /// Force a redraw when the cursor leaves. Also clean up anythine we were doing with the tile
        /// </summary>
        private void PlugIn1_TileMouseLeave(object sender, TileEventArgs ea)
        {
            ea.Tile.Invalidate();
            _hoveredTest = null;
            if(CodeRush.SmartTags.IsSmartTagVisible)
            {
                CodeRush.SmartTags.HidePopupMenu();
            }
        }

        /// <summary>
        /// Change the cursor when it enters a tile
        /// </summary>
        private static void PlugIn1_TileSetCursor(object sender, TileSetCursorEventArgs ea)
        {
            Cursor.Current = Cursors.Hand;
            ea.SetCursorArgs.Cancel = true;
        }

        /// <summary>
        /// When the user hovers, save the tile and present a menu of options
        /// </summary>
        /// <param name="ea"></param>
        private void PlugIn1_EditorMouseHover(EditorEventArgs ea)
        {
            Tile tile = ea.TextView.ActiveTile;
            ShowTestPopupMenu(ea.TextView, tile);
        }

        private void ShowTestPopupMenu(TextView textView, Tile tile)
        {
            if (TileIsOurs(tile) && _hoveredTest == null)
            {
                _hoveredTest = tile.Object as ITestDetail;
                Point tilePoint = new Point(tile.Bounds.Left, tile.Bounds.Bottom);
                Point menuPoint = textView.ToScreenPoint(tilePoint);
                CodeRush.SmartTags.ShowPopupMenu(menuPoint, _hoveredTest.SmartTagProvider);
            }
        }
        /// <summary>
        /// Provide the list of unit test menu options
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ea"></param>
        private void testActions_GetSmartTagItems(object sender, GetSmartTagItemsEventArgs ea)
        {
            AddSmartTagItem(ea, kRunTestMenuItem, PlugIn1_RunTest);
            AddSmartTagItem(ea, kRunClassMenuItem, PlugIn1_RunTest);
            AddSmartTagItem(ea, kRunAssemblyMenuItem, PlugIn1_RunTest);

            if (_Failures.Count > 0)
            {
                AddSmartTagItem(ea, kNextFailedTestMenuItem, MoveToNextFailure);
            }
        }

        /// <summary>
        /// Do all the busy work to add a menuitem to a smart tag
        /// </summary>
        /// <param name="ea">Where to put new menu item</param>
        /// <param name="menuText">What to put into the smartTagItem</param>
        /// <param name="handler">What will respond to the user selecting the smart tag item</param>
        private static void AddSmartTagItem(GetSmartTagItemsEventArgs ea, string menuText, System.EventHandler handler)
        {
            SmartTagItem menuItem = new SmartTagItem(menuText);
            menuItem.Execute += handler;
            ea.Add(menuItem);
        }


        /// <summary>
        /// Set the color scheme of the smartTag
        /// </summary>
        private void testActions_GetSmartTagItemColors(object sender, GetSmartTagItemColorsEventArgs ea)
        {
            ea.PopupMenuColors = sTestMenuColors; 
        }

        /// <summary>
        /// Tells the system if a smartTag is needed
        /// </summary>
        private bool testActions_CheckSmartTagAvailability(object sender, System.EventArgs ea)
        {
            return _hoveredTest != null;
        }
        #endregion

        #region Run Tests
        /// <summary>
        /// Respond to a SmartTagMenuItem selection and run the appropriate set of tests
        /// </summary>
        private void PlugIn1_RunTest(object sender, System.EventArgs ea)
        {
            ResetTestResults();

            switch (((SmartTagItem)sender).Caption)
            {
                case kRunAssemblyMenuItem:
                    StandardRunTestBehavior(new GallioRunner(), (run, assemblyPath, assemblyName) => run.RunTests(assemblyPath, assemblyName));
                    break;

                case kRunClassMenuItem:
                    StandardRunTestBehavior(new GallioRunner(), (run, assemblyPath, assemblyName) => run.RunTests(assemblyPath, assemblyName, _hoveredTest.ClassName));
                    break;

                case kRunTestMenuItem:
                    StandardRunTestBehavior(new GallioRunner(), (run, assemblyPath, assemblyName) => run.RunTests(assemblyPath, assemblyName, _hoveredTest.ClassName, _hoveredTest.MethodName));
                    break;
            }
        }

        /// <summary>
        /// Handle the Tests starting message
        /// </summary>
        private void runner_TestsStarting(object sender, TestsStartingEventArgs args)
        {
            WriteToTestPane(args.Message);
        }

        /// <summary>
        /// Handle the a test is complete event
        /// </summary>
        private void runner_TestComplete(object sender, TestCompleteEventArgs args)
        {
            WriteToTestPane(args.RawResult, true);
            if (args.Result != null && args.Result.Location != null)
            {// Only UnitTests have a result.
                UnitTestDetail testData = _Tests.Find(test => test.Method.RootNamespaceLocation == args.Result.Location);
                if (testData == null)
                {
                    testData = new UnitTestDetail(args.Result.Location, testActions);
                    _Tests.Add(testData);
                }
                testData.Result = args.Result;
            }
        }

        /// <summary>
        /// Write a line of text to the test pane
        /// </summary>
        internal static void WriteToTestPane(string text)
        {
            WriteToTestPane(text, false);
        }

        /// <summary>
        /// Write a line of text to the test pane
        /// </summary>
        internal static void WriteToTestPane(string text, bool appendLineFeed)
        {
            if (!string.IsNullOrEmpty(text))
            {
                EnvDTE.OutputWindowPane testPane = GetTestingOutputPane();
                if (testPane != null)
                {
                    testPane.OutputString(text);
                    if (appendLineFeed)
                    {
                        testPane.OutputString(System.Environment.NewLine);
                        testPane.OutputString(System.Environment.NewLine);
                    }
                }
            }
        }

        /// <summary>
        /// Handle the end of tests event
        /// </summary>
        private void runner_AllTestsComplete(object sender, AllTestsCompleteEventArgs args)
        {
            string overview = string.Format("\n{0} passed, {1} failed, {2} skipped, duration: {3} seconds\n",
                                                                                          args.PassCount,
                                                                                          args.FailCount,
                                                                                          args.SkipCount,
                                                                                          args.Duration);
            WriteToTestPane(overview);
            CodeRush.Windows.Active.DTE.StatusBar.Text = overview;

            CreateFailedTestList();
            DxCoreUtil.Invalidate(CodeRush.Source.ActiveClass);
        }

        /// <summary>
        /// Find the tests that failed and resort them into file/line order
        /// </summary>
        private void CreateFailedTestList()
        {
            _Failures = _Tests.FindAll(test => test.Result.Status == TestStatus.Failed);
            _Failures.Sort((lhs, rhs) =>
            {
                int locationResult = GetTestPath(lhs).CompareTo(GetTestPath(rhs));
                if (locationResult == 0)
                    return lhs.Method.StartLine - rhs.Method.StartLine;
                return locationResult;
            });
        }

        /// <summary>
        /// Fetches the document name or the class name if the document name is not available.
        /// </summary>
        static string GetTestPath(UnitTestDetail test)
        {
            return test.Method.Document != null ? test.Method.Document.FullName : test.ClassName;
        }

        /// <summary>
        /// Fetch the test output window
        /// </summary>
        /// <returns></returns>
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
            catch
            {
                testPane = outputWindow.OutputWindowPanes.Add(kTestsPane);
            }
            System.Diagnostics.Debug.Assert(testPane != null);
            testPane.Activate();
            return testPane;
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
        /// Get the assembly path for a project
        /// </summary>
        private static string GetAssemblyPath(EnvDTE.Project vsProject)
        {
            if (vsProject != null)
            {
                string outputDir = Path.Combine(GetPropertyValue(vsProject.Properties, "FullPath"),
                    GetPropertyValue(vsProject.ConfigurationManager.ActiveConfiguration.Properties, "OutputPath"));
                return Path.Combine(outputDir, GetPropertyValue(vsProject.Properties, "OutputFileName"));
            }
            return string.Empty;
        }

        /// <summary>
        /// Helper function to get the text from a project item
        /// </summary>
        private static string GetPropertyValue(EnvDTE.Properties source, string name)
        {
            if (source == null || System.String.IsNullOrEmpty(name))
                return string.Empty;

            EnvDTE.Property property = source.Item(name);
            if (property != null)
            {
                return property.Value.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// Get the assembly name of a project.
        /// </summary>
        /// <param name="vsProject"></param>
        /// <returns></returns>
        static string GetAssemblyName(EnvDTE.Project vsProject)
        {
            return GetPropertyValue(vsProject.Properties, "AssemblyName");
        }
        #endregion

        #region Interact with Results
        /// <summary>
        /// Clear all the result information we know anything about
        /// </summary>
        private void ResetTestResults()
        {
            _currentFailure = kDefaultCurrentFailure;
            _Failures = new List<UnitTestDetail>();
            _Tests.ForEach(test => test.Result = new TestResult());
        }

        /// <summary>
        /// Move the cursor to the next error and use a beacon to highlight the move 
        /// </summary>
        private void MoveToNextFailure(object sender, System.EventArgs ea)
        {
            UnitTestDetail nextLocation = LocateNextTest();
            MoveToTest(nextLocation.Method);
        }

        /// <summary>
        /// Determine what the next failed test is
        /// </summary>
        private UnitTestDetail LocateNextTest()
        {
            int offset = -1;
            LanguageElement currentMethod = CodeRush.Source.ActiveMethod;
            if (currentMethod != null)
            {
                string currentLocation = currentMethod.RootNamespaceLocation;
                offset = _Failures.FindIndex(test => test.Location == currentLocation && test.Result.Status == TestStatus.Failed);
            }
            if (offset >= 0)
            {// Within in a test move from here
                _currentFailure = (++offset) % _Failures.Count;
            }
            else
            {// Not in a test, go to the next one in our list from the last spot
                _currentFailure = (++_currentFailure) % _Failures.Count;
            }
            return _Failures[_currentFailure];
        }

        /// <summary>
        /// Put the cursor on the next given element and use a beacon
        /// </summary>
        private void MoveToTest(LanguageElement dxCoreElement)
        {
            if (dxCoreElement != null)
            {
                CodeRush.File.Activate(dxCoreElement.FileNode.Name);
                CodeRush.Caret.MoveTo(dxCoreElement.StartLine, dxCoreElement.StartOffset);
                TextView view = (TextView)CodeRush.Source.Active.View;
                using (LocatorBeacon beacon = new LocatorBeacon { Color = FailedColor })
                {
                    beacon.Start(view, dxCoreElement.StartLine, dxCoreElement.StartOffset);
                }
            }
        }
        #endregion

        #region Draw test results
        private readonly Color PassedColor = Color.FromArgb(157, 254, 133);
        private readonly Color SkippedColor = Color.FromArgb(255, 255, 70);
        private readonly Color FailedColor = Color.FromArgb(255, 140, 140);
        private readonly Color UnknownColor = Color.White;
        private readonly Color TileBackgroundFillColor = Color.FromArgb(255, 253, 75);
        private readonly Color TileBorderColor = Color.FromArgb(233, 210, 33);

        /// <summary>
        /// Put up the action tile
        /// redraw the test attribute
        /// Draw the parsed error text if we can
        /// </summary>
        private void PlugIn1_EditorPaintLanguageElement(EditorPaintLanguageElementEventArgs ea)
        {
            Attribute testAttribute = GetTestAttributeForLanguageElement(ea.LanguageElement);
            if (testAttribute != null)
            {
                UnitTestDetail testData = FindDataForTest(testAttribute);
                if (testData != null)
                {
                    if (ea.LanguageElement.ElementType == LanguageElementType.Attribute)
                    {
                        DrawTestRunnerIcon(ea.PaintArgs, testData);
                        RedrawTestAttribute(ea.PaintArgs, testData);
                    }
                    else
                    {
                        DrawError(ea, testData.Result);
                    }
                }
            }
            else if (ea.LanguageElement.ElementType == LanguageElementType.Method)
            {// Potential adHocTest
                Method method = (Method)ea.LanguageElement;
                if ( DxCoreUtil.GetFirstTestAttribute(ea.LanguageElement) == null
                    && IsAdHoc(method))
                {
                    DrawTestRunnerIcon(ea.PaintArgs, new AdHocDetail(method, adHocActions));
                }
            }
        }

        private static bool IsAdHoc(Method method)
        {
            return method.Parameters.Count == 0
                    && method.IsGeneric == false
                    && method.IsConstructor == false;
        }

        /// <summary>
        /// Attempt to locate the Test attribute for the given language element
        /// </summary>                                                              
        private static Attribute GetTestAttributeForLanguageElement(LanguageElement languageElement)
        {       
            if (languageElement.ElementType == LanguageElementType.Attribute && DxCoreUtil.IsTest((Attribute)languageElement))
            {                     
                return (Attribute)languageElement;
            }        
            else if (languageElement.ElementType == LanguageElementType.MethodCall)
            {                                           
                return DxCoreUtil.GetFirstTestAttribute(languageElement);
            }
            return null;
        }

        /// <summary>
        /// Look for information about the the attribute. Create and add an information point if none exists and this is a test attribute
        /// </summary>
        private UnitTestDetail FindDataForTest(Attribute testAttribute)
        {
            UnitTestDetail testData = _Tests.Find(test => test.Method.RootNamespaceLocation == testAttribute.TargetNode.RootNamespaceLocation);
            if (testData == null)
            {
                if (DxCoreUtil.IsTest(testAttribute))  // probably not needed because we can't get here unless GetFirstTestAttribute already performed the test, but not a bad safeguard.
                {
                    testData = new UnitTestDetail(DxCoreUtil.GetMethod(testAttribute.TargetNode), testActions);
                    _Tests.Add(testData);
                };
            }
            return testData;
        }

        /// <summary>
        /// Draw the icon for the tile
        /// </summary>
        /// <param name="attributes"></param>
        private void DrawTestRunnerIcon(EditorPaintEventArgs paintArgs, ITestDetail attributes)
        {
            Rectangle indicator = CreateIndicator(paintArgs, attributes.IconCoordinates);
            paintArgs.TextView.AddTile(NewTile(indicator, attributes));
            try
            {
                paintArgs.TextView.Graphics.DrawIcon(new Icon(GetType(), "TestIndicator.ico"), indicator);
            }
            catch
            {// fail silently if icon is missing from the project.
            }
        }

        private Rectangle CreateIndicator(EditorPaintEventArgs paintArgs, SourcePoint referencePoint)
        {
            Point topLeft = paintArgs.TextView.GetPoint(referencePoint.Line, referencePoint.Offset);
            Rectangle indicator = new Rectangle(topLeft.X - 24, topLeft.Y + 2, 16, 16);

            Tile tile = ((TextView)CodeRush.Source.Active.View).ActiveTile;
            if (TileIsOurs(tile))
            {// cursor is over tile give a hint
                paintArgs.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                using (SolidBrush backgroundFill = new SolidBrush(TileBackgroundFillColor))
                {
                    paintArgs.Graphics.FillRectangle(backgroundFill, indicator);
                }
                using (Pen borderHighlight = new Pen(TileBorderColor))
                {
                    paintArgs.Graphics.DrawRectangle(borderHighlight, indicator.X, indicator.Y, indicator.Width - 1, indicator.Width - 1);
                }
            }
            return indicator;
        }

        /// <summary>
        /// Draw all the test attributes the same and put the status color in the background
        /// </summary>
        private void RedrawTestAttribute(EditorPaintEventArgs paintArgs, UnitTestDetail testData)
        {
            if (paintArgs.LineInView(testData.Attribute.StartLine) && ShouldPaintTestAttribute(paintArgs, testData.Attribute))
            {
                string displayText = GetDisplayText(testData.Attribute, testData.Method);
                paintArgs.OverlayText(displayText,
                    testData.Attribute.StartLine,
                    testData.Attribute.StartOffset - 1,
                    Color.Black,
                    GetBackgroundColor(testData.Result.Status));
            }
        }

        private void PlugIn1_EditorValidateLanguageElementClipRegion(EditorValidateLanguageElementClipRegionEventArgs ea)
        {
            Attribute testAttribute = ea.LanguageElement as Attribute;
            if (testAttribute == null || false == DxCoreUtil.IsTest(testAttribute))
            {// Nothing to do
                return;
            }

            if(!ShouldPaintTestAttribute(ea.ValidateClipRegionArgs, testAttribute))
            {
                return;
            }

            Point start;
            if (!ea.ValidateClipRegionArgs.GetPoint(testAttribute.Range.Start, out start))
            {
                return;
            }

            Point stop;
            int textLength = GetDisplayText(testAttribute, testAttribute.TargetNode).Length - testAttribute.ToString().Length;
            if (!ea.ValidateClipRegionArgs.GetPoint(new SourcePoint (testAttribute.Range.End.Line, (testAttribute.Range.Start.Offset + textLength)), out stop))
            {
                return;
            }

            if (stop.X < start.X)
            {
                stop.X = start.X;
            }
            int attributeWidth = stop.X - start.X;
            int attributeHeight = stop.Y - start.Y + ea.ValidateClipRegionArgs.LineHeight;
            ea.ValidateClipRegionArgs.ValidateRectangle(new Rectangle(start.X, start.Y, attributeWidth, attributeHeight));
        }

        private static bool ShouldPaintTestAttribute(BaseEditorPaintEventArgs paintArgs, Attribute testAttribute)
        {
            return (!paintArgs.TextViewIsActive ||
                ((paintArgs.CaretLine < testAttribute.StartLine ||
                paintArgs.CaretLine > testAttribute.EndLine) ||
                (paintArgs.CaretLine == testAttribute.StartLine &&
                paintArgs.CaretOffset < testAttribute.StartOffset)));
        }

        /// <summary>
        /// Draw the parsed error text at the end of the method causing the test failure 
        /// </summary>
        private void DrawError(EditorPaintLanguageElementEventArgs ea, TestResult testResult)
        {
            if (testResult.Status == TestStatus.Failed)
            {
                FailureData failure = testResult.Failure;
                if (failure.FailingStatement != null)
                {
                    int errorTextStartCol = failure.FailingStatement.EndOffset + 5;
                    if (string.IsNullOrEmpty(failure.Expected))
                    {// not an equal comparison
                        ea.PaintArgs.OverlayText("<------- Test failed here",
                            failure.FailingStatement.StartLine,
                            errorTextStartCol,
                            FailedColor);

                    }
                    else if (failure.ActualDiffersAt < 0)
                    {
                        ea.PaintArgs.OverlayText(string.Format("Expected: {0} Actual: {1}", failure.Expected, failure.Actual),
                            failure.FailingStatement.StartLine,
                            errorTextStartCol,
                            FailedColor);
                    }
                    else
                    {
                        int start = errorTextStartCol;
                        string correctPortion = string.Format("Expected: {0} Actual: {1}", failure.Expected, failure.Actual.Substring(0, failure.ActualDiffersAt));
                        ea.PaintArgs.OverlayText(correctPortion,
                            failure.FailingStatement.StartLine,
                            start,
                            FailedColor);
                        ea.PaintArgs.OverlayText(failure.Actual.Substring(failure.ActualDiffersAt),
                            failure.FailingStatement.StartLine,
                            start + correctPortion.Length,
                            Color.Red);
                    }
                }
            }
        }

        /// <summary>
        /// Look up the method location in the test results. If found use the test results to pick the background color
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns> 
        private Color GetBackgroundColor(TestStatus status)
        {
            switch (status)
            {
                case TestStatus.Unknown:
                default:
                    return CodeRush.VSSettings.BackgroundColor;
                case TestStatus.Skipped:
                    return SkippedColor;
                case TestStatus.Passed:
                    return PassedColor;
                case TestStatus.Failed:
                    return FailedColor;
            }
        }

        /// <summary>
        /// Use the width of the method the attribute is attached to to determine how much 
        /// trailing whitespace is needed for the colored bar.
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns>Test plus enough whitespace to make the colored bar extent one char past the end of the closing paren</returns>
        private static string GetDisplayText(LanguageElement attribute, LanguageElement method)
        {
            TextView view = CodeRush.Source.Active.View as TextView;

            StringBuilder displayText = new StringBuilder(" Test");
            int attributeOffset = attribute.StartOffset; // Doesn't include the opening square bracket
            int lineLength = view.LengthOfLine(method.StartLine);
            displayText.Append(' ', lineLength - attributeOffset - 3); // -3 to account for addition of " test" and yet cover past the parens too.
            return displayText.ToString();
        }
        #endregion

        #region Build Action
        /// <summary>
        /// Respond to build menu item
        /// </summary>
        /// <param name="ea"></param>
        private void actBuildProject_Execute(ExecuteEventArgs ea)
        {
            bool buildPassed = BuildActiveProject();

            if (!buildPassed)
            {
                ShowBuildOutputWindow();
            }
            CodeRush.Windows.Active.DTE.StatusBar.Text = "Build succeded";
        }
        #endregion

        /// <summary>
        /// Reset what we know
        /// </summary>
        private void PlugIn1_SolutionOpened()
        {
            _Tests.Clear();
            _Failures.Clear();
        }

        UnitTestDetail _currentTestData;
        private void PlugIn1_LanguageElementActivated(LanguageElementActivatedEventArgs ea)
        {
            if (ea.Element.InsideMethod)
            {
                Method method = DxCoreUtil.GetMethod(ea.Element);
                _currentTestData = _Tests.Find(test => test.Method == method);
            }
        }

        private void PlugIn1_TextChanged(TextChangedEventArgs ea)
        {
            if (_currentTestData != null && _currentTestData.Result.Status != TestStatus.Unknown)
            {
            	_currentTestData.Result = new TestResult();
                DxCoreUtil.Invalidate(_currentTestData.Method);
            }
        }

        private void actRunTests_Execute(ExecuteEventArgs ea)
        {
            ResetTestResults();

            string className = CodeRush.Source.ActiveClass.FullName;
            if (CodeRush.Source.ActiveMethod != null)
            {// Handle trigger in method
                string methodName = CodeRush.Source.ActiveMethod.Name;
                if (DxCoreUtil.GetFirstTestAttribute(CodeRush.Source.ActiveMethod) != null)
                {
                    StandardRunTestBehavior(new GallioRunner(), 
                        (run, assemblyPath, assemblyName) => run.RunTests(assemblyPath, assemblyName, className, methodName));
                }
                else if (IsAdHoc(CodeRush.Source.ActiveMethod))
                {
                    StandardRunTestBehavior(new AdHocRunner(),
                        (run, assemblyPath, assemblyName) =>
                        {
                            run.RunTests(assemblyPath, assemblyName, className, methodName);
                            ShowTestOutputWindow();
                        });
                }
            }
            else
            {// Handle trigger in class
                StandardRunTestBehavior(new GallioRunner(), 
                    (run, assemblyPath, assemblyName) => run.RunTests(assemblyPath, assemblyName, className));
            }
        }

        /// <summary>
        /// Respond to the RunAllTests action by launching all the tests in the assembly.
        /// </summary>
        /// <param name="ea"></param>
        private void actRunAllTests_Execute(ExecuteEventArgs ea)
        {
            ResetTestResults();
            StandardRunTestBehavior(new GallioRunner(), (run, assemblyPath, assemblyName) => run.RunTests(assemblyPath, assemblyName));
        }

        /// <summary>
        /// Add the extra common things here that we can't do in the BaseTestRunner without adding too much coupling.
        /// </summary>
        /// <param name="runner">What runner to use</param>
        /// <param name="specificTestAction">What test action to take with the runner </param>
        private void StandardRunTestBehavior(BaseTestRunner runner, RunTestSelectorAction specificTestAction)
        {
            bool buildPassed = BuildActiveProject();

            if (buildPassed)
            {
                try
                {
                    EnvDTE.Project activeProject = GetActiveProject();
                    string assemblyPath = GetAssemblyPath(activeProject);
                    string assemblyName = GetAssemblyName(activeProject);
                    CodeRush.Windows.Active.DTE.StatusBar.Text = kTestingStartedMessage;
                    BaseTestRunner.StandardRunTestBehavior(runner,
                        runner_TestsStarting,
                        runner_TestComplete,
                        runner_AllTestsComplete,
                        assemblyPath,
                        assemblyName,
                        specificTestAction);
                }
                catch (System.Exception ex)
                {
                    WriteToTestPane("-->RedGreen Program Failure<--");
                    WriteToTestPane(ex.Message);
                    WriteToTestPane(ex.StackTrace);
                    throw;
                }
            }
            else
            {
                ShowBuildOutputWindow();
            }

        }

        private void PlugIn1_EditorMouseUp(EditorMouseEventArgs ea)
        {
            //Not working well enough to commit yet.
            //Tile tile = ea.TextView.ActiveTile;
            //ShowTestPopupMenu(ea.TextView, tile);
        }

        private void attachDebugger_Execute(ExecuteEventArgs ea)
        {
            AttachToDebugger();
        }

        public static bool AttachToDebugger()
        {
            bool alreadyAttached = false;
            const string kGallioHost = "Gallio.Host";

            foreach (EnvDTE.Process attachedProcess in CodeRush.Solution.Active.DTE.Debugger.DebuggedProcesses)
            {
                if (attachedProcess.Name.Contains(kGallioHost))
                {
                    alreadyAttached = true;
                    break;
                }
            }

            if (false == alreadyAttached)
            {
                foreach (EnvDTE.Process process in CodeRush.Solution.Active.DTE.Debugger.LocalProcesses)
                {
                    if (process.Name.Contains(kGallioHost))
                    {
                        process.Attach();
                        return true;
                    }
                }
            }
            return false;
        }

        #region AdHoc Smart Menu
        private bool adHocActions_CheckSmartTagAvailability(object sender, System.EventArgs ea)
        {
            return default(bool);
        }

        private void adHocActions_GetSmartTagItemColors(object sender, GetSmartTagItemColorsEventArgs ea)
        {
            ea.PopupMenuColors = sTestMenuColors; 
        }

        private void adHocActions_GetSmartTagItems(object sender, GetSmartTagItemsEventArgs ea)
        {
            AddSmartTagItem(ea, kRunTestMenuItem, PlugIn1_RunAdHocTest);
        }

        private void PlugIn1_RunAdHocTest(object sender, System.EventArgs ea)
        {
            StandardRunTestBehavior(new AdHocRunner(),
                                (run, assemblyPath, assemblyName) =>
                                {
                                    run.RunTests(assemblyPath, assemblyName, _hoveredTest.ClassName, _hoveredTest.MethodName);
                                    ShowTestOutputWindow();
                                });
        }
        #endregion
    }


}
