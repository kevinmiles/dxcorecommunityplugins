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
using System.IO;
using System.Text;
using Gallio.Framework;
using Gallio.Framework.Utilities;
using Gallio.Model;
using Gallio.Model.Execution;
using Gallio.Model.Filters;
using Gallio.Runner;
using Gallio.Runtime;

namespace RedGreen
{
    /// <summary>
    /// Runs tests for Gallio supported testing frameworks. 
    /// </summary>
    class GallioRunner : BaseTestRunner , ITestRunner
    {
        public GallioRunner()
        {
            AddAttributes("Test", "Fact");
            AddFrameworks("MbUnit.Framework", "NUnit.Framework", "Xunit");
        }
        
        /// <summary>
        /// Run all the tests in a class
        /// </summary>
        /// <param name="assemblyPath">Where the class lives physically on the disk</param>
        /// <param name="assemblyName">The full name of the assembly that contains the class.</param>
        /// <param name="className">Full name of the class that has the tests to run.</param>
        public void RunClass(string assemblyPath, string assemblyName, string className)
        {
            RunTests(assemblyPath,
                new AssemblyFilter<ITest>(new EqualityFilter<string>(assemblyName)),
                new TypeFilter<ITest>(new EqualityFilter<string>(className), false));
        }

        /// <summary>
        /// Run a test
        /// </summary>
        /// <param name="assemblyPath">Where the class lives physically on the disk</param>
        /// <param name="assemblyName">The full name of the assembly that contains the class.</param>
        /// <param name="className">Full name of the class that has the tests to run.</param>
        /// <param name="methodName">The specific method name of the test to run.</param>
        public void RunMethod(string assemblyPath, string assemblyName, string className, string methodName)
        {
            RunTests(assemblyPath, 
                new AssemblyFilter<ITest>(new EqualityFilter<string>(assemblyName)), 
                new TypeFilter<ITest>(new EqualityFilter<string>(className), false), 
                new MemberFilter<ITest>(new EqualityFilter<string>(methodName)));
        }

        /// <summary>
        /// Do the real work of running some tests
        /// </summary>
        /// <param name="assemblyPath">Where the class lives physically on the disk</param>
        /// <param name="filters">A set of filters to narrow the tests that should be run</param>
        private void RunTests(string assemblyPath, params Filter<ITest>[] filters)
        {
            TestLauncher launcher = new TestLauncher();

            //launcher.Logger = new RedGreenLogger(); // provide your own ILogger implementation if you like... this is optional


            // Set the installation path explicitly to ensure that we do not encounter problems
            // when the test assembly contains a local copy of the primary runtime assemblies
            // which will confuse the runtime into searching in the wrong place for plugins.
            launcher.RuntimeSetup = new RuntimeSetup();
            Microsoft.Win32.RegistryKey gallioKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("Gallio");
            if (gallioKey == null)
            {
                System.Windows.Forms.MessageBox.Show(
                    @"Gallio not installed (registry key HKLM\SOFTWARE\Gallio\(Default) not found).\r\n"
                    + "Please download from http://www.gallio.org/",
                    "RedGreen Run Tests",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            string gallioPath = gallioKey.GetValue("").ToString();

            launcher.RuntimeSetup.PluginDirectories.Add(gallioPath);
            launcher.RuntimeSetup.InstallationPath = Path.GetDirectoryName(typeof(GallioRunner).Assembly.Location); 
            //On 9/7/08, the InstallationFolder was not yet available in a packaged version of Gallio. When it is, the below should be used in place of the above three lines!
            //launcher.RuntimeSetup.InstallationConfiguration = InstallationConfiguration.LoadFromRegistry();
            //launcher.RuntimeSetup.InstallationPath = launcher.RuntimeSetup.InstallationConfiguration.InstallationFolder;
            
            launcher.TestExecutionOptions.Filter = new AndFilter<ITest>(filters);
            
            launcher.TestPackageConfig.HostSetup.ShadowCopy = true;
            launcher.TestPackageConfig.HostSetup.ApplicationBaseDirectory = Path.GetDirectoryName(assemblyPath);
            launcher.TestPackageConfig.HostSetup.WorkingDirectory = Path.GetDirectoryName(assemblyPath);


            launcher.TestPackageConfig.AssemblyFiles.Add(assemblyPath);


            GallioLogExtension testCompleteReciever = new GallioLogExtension();
            testCompleteReciever.TestComplete += new TestCompleteEventHandler(testCompleteReciever_TestComplete);
            launcher.TestRunnerExtensions.Add(testCompleteReciever);


            TestLauncherResult result = launcher.Run();


            RaiseAllComplete(result.Statistics.PassedCount.ToString(),

                result.Statistics.FailedCount.ToString(),
                result.Statistics.SkippedCount.ToString(),
                result.Statistics.Duration.ToString());
        }

        /// <summary>
        /// Re-raise event as though the runner were the source.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void testCompleteReciever_TestComplete(object sender, TestCompleteEventArgs args)
        {
            RaiseComplete(args.RawResult, args.Result);
        }
    }
}
