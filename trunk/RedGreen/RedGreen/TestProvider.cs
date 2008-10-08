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
//using System.IO;
//using Gallio.Model;
//using Gallio.Reflection;
//using Gallio.Runtime;
//using Gallio.Runtime.Logging;
using DevExpress.CodeRush.StructuralParser;

namespace RedGreen
{
    /// <summary>
    /// Was supposed to use Gallio to fetch the tests. But there were some issues in that route, so I just iterate DxCore's knowledge of the project.
    /// </summary>
    public class TestProvider
    {
        /// <summary>
        /// Walk the list of methods in DxCore and return a list of those which are test methods
        /// </summary>
        static public List<LanguageElement> GetTestsInProject(ProjectElement project)
        {
            List<LanguageElement> tests = new List<LanguageElement>();
            foreach (LanguageElement t in project.AllTypes)
            {
                LanguageElement methodElement = t.FindChildByElementType(LanguageElementType.Method);
                while (methodElement != null)
                {
                    if (DxCoreUtil.GetFirstTestAttribute(methodElement) != null)
                    {
                        tests.Add(methodElement);
                    }
                }
            }
            return tests;
        }
        //static public List<string> GetTestsInAssembly(string assemblyPath)
        //{
        //    List<string> testnames = new List<string>();
        //    try
        //    {
        //        //if (RuntimeAccessor.IsInitialized == false)
        //        {
        //            string gallioPath = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Gallio").GetValue("").ToString();
        //            RuntimeSetup setup = new RuntimeSetup();
        //            setup.PluginDirectories.Add(gallioPath);
        //            setup.InstallationPath = Path.GetDirectoryName(typeof(PlugIn1).Assembly.Location);
        //            //On 9/7/08, the InstallationFolder was not yet available in a packaged version of Gallio. When it is, the below should be used in place of the above three lines!
        //            //launcher.RuntimeSetup.InstallationConfiguration = InstallationConfiguration.LoadFromRegistry();
        //            //launcher.RuntimeSetup.InstallationPath = launcher.RuntimeSetup.InstallationConfiguration.InstallationFolder;
        //            RuntimeBootstrap.Initialize(setup, NullLogger.Instance);
        //        }

        //        if (string.IsNullOrEmpty(assemblyPath) == false)
        //        {
        //            IReflectionPolicy reflectionPolicy = Reflector.NativeReflectionPolicy;
        //            ITestExplorer explorer = GetTestExplorer(reflectionPolicy);
        //            if (explorer != null)
        //            {
        //                explorer.ExploreAssembly(GetAssemblyInfo(assemblyPath, reflectionPolicy), null);
        //                explorer.FinishModel();
        //                foreach (ITest test in explorer.TestModel.AllTests)
        //                {
        //                    if (test.ToString().StartsWith("[Test") && !string.IsNullOrEmpty(test.FullName))
        //                    {
        //                        testnames.Add(ConvertFullNameToLocation(test.FullName));
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        RuntimeBootstrap.Shutdown();
        //    }

        //    return testnames;
        //}

        //private static ITestExplorer GetTestExplorer(IReflectionPolicy reflectionPolicy)
        //{
        //    ITestPackageExplorerFactory explorerFactory = RuntimeAccessor.Instance.Resolve<ITestPackageExplorerFactory>();
        //    return explorerFactory.CreateTestExplorer(new TestPackageConfig(), reflectionPolicy);
        //}

        //private static IAssemblyInfo GetAssemblyInfo(string assemblyPath, IReflectionPolicy reflectionPolicy)
        //{
        //    return reflectionPolicy.LoadAssemblyFrom(assemblyPath);
        //}

        //public static string ConvertFullNameToLocation(string testFullName)
        //{
        //    return testFullName.Substring(testFullName.IndexOf("/") + 1).Replace('/', '.');
        //}
    }
}
