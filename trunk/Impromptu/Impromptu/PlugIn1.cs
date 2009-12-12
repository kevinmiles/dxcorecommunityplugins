/*
 * Software License Agreement for RedGreen
 * 
 * Copyright (c) 2009 Renaissance Learning, Inc. and James Argeropoulos
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
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace Impromptu
{
	public partial class PlugIn1 : StandardPlugIn
	{
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

		private void PlugIn1_EditorPaintLanguageElement(EditorPaintLanguageElementEventArgs ea)
		{
			LanguageElement element = ea.LanguageElement;
			if (element.ElementType == LanguageElementType.Method)
            {
				Method method = (Method)element;
				if (_DisplayTile == true
					&& method.IsClassOperator == false 
					&& method.IsConstructor == false 
					&& method.IsDestructor == false
					&& method.IsExplicitCast == false
					&& method.IsImplicitCast == false
					&& method.ParameterCount == 0
					&& method.InsideClass == true) // last one is probably not needed, but...
				{
					DrawImpromptuIcon(ea.PaintArgs, method);
				}
            }
		}

		/// <summary>
		/// Draw the icon for the tile
		/// </summary>
		/// <param name="attributes"></param>
		private void DrawImpromptuIcon(EditorPaintEventArgs paintArgs, Method target)
		{
			Rectangle indicator = CreateIndicator(paintArgs, target.Range.Start);
			paintArgs.TextView.AddTile(NewTile(indicator, target));
			try
			{
				paintArgs.TextView.Graphics.DrawIcon(new Icon(GetType(), "SawBlade.ico"), indicator);
			}
			catch
			{// fail silently if icon is missing from the project.
				target = null; // attempt to get rid of coderush warning
			}
		}

		private static Rectangle CreateIndicator(EditorPaintEventArgs paintArgs, SourcePoint referencePoint)
		{
			Point topLeft = paintArgs.TextView.GetPoint(referencePoint.Line, referencePoint.Offset);
			Rectangle indicator = new Rectangle(topLeft.X - 24 - 16, topLeft.Y + 2, 16, 16); //Double subtract to avoid interfering with access modifyer tile. Need to learn how to determine if it is visible.
			return indicator;
		}

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

		private void PlugIn1_TileSetCursor(object sender, TileSetCursorEventArgs ea)
		{
			Cursor.Current = Cursors.Hand;
			ea.SetCursorArgs.Cancel = true;
		}

		private void PlugIn1_TileMouseDown(object sender, TileMouseEventArgs ea)
		{
			if (ea.MouseArgs.Button != MouseButtons.Left)
			{
				return;
			}
			ea.MouseArgs.Cancel = true;

			Method method = (Method)ea.Tile.Object;
			RunMethod(method);
		}

		private static void RunMethod(Method method)
		{
			try
			{
				Cursor.Current = Cursors.WaitCursor;
				if (BuildActiveProject() == true)
				{
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
						string performerPath = LocateExe();
						if (performerPath == String.Empty)
						{
							return;
						}
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
				else
				{
					ShowBuildOutputWindow();
				}
			}
			catch (Exception ex)
			{
				WriteToImpromptuPane(ex.Message);
			}
			finally
			{
				Cursor.Current = Cursors.Default;
				ShowTestOutputWindow();
			}
		}

		private static string LocateExe()
		{
			Microsoft.Win32.RegistryKey communityPathKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Developer Express\\CodeRush for VS\\CommunityPlugIns");
			if (communityPathKey != null)
			{
				string installFolder = communityPathKey.GetValue("InstallFolder").ToString();
				return Path.Combine(installFolder, "Impromptu.Performer.exe");
			}
			return String.Empty;
		}

		private void impromptuActions_Execute(ExecuteEventArgs ea)
		{
			Method method = CodeRush.Source.ActiveMethod;
			if (method != null)
			{
				RunMethod(method);
			}
		}

		private void impromptuActions_CheckAvailability(CheckActionAvailabilityEventArgs ea)
		{
			Method method = CodeRush.Source.ActiveMethod;

			ea.Available = (method != null 
					&& method.IsClassOperator == false 
					&& method.IsConstructor == false 
					&& method.IsDestructor == false
					&& method.IsExplicitCast == false
					&& method.IsImplicitCast == false
					&& method.ParameterCount == 0) ;
		}

		private void PlugIn1_OptionsChanged(OptionsChangedEventArgs ea)
		{
			bool displayTile = LoadSettings();
			if (displayTile != _DisplayTile)
			{
				_DisplayTile = displayTile;
				CodeRush.Source.Active.View.Invalidate();
			}
		}
	}
}