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

namespace RedGreen
{
    delegate void TestsStartingHandler(object sender, TestsStartingEventArgs ea);
    delegate void TestCompleteEventHandler(object sender, TestCompleteEventArgs args);
    delegate void AllTestsCompleteEventHandler(object sender, AllTestsCompleteEventArgs args);

    delegate void RunTestSelectorAction(BaseTestRunner runner, string assemblyPath, string assemblyName);

    /// <summary>
    /// Abstract base class to unify the api of the test runners
    /// </summary>
    internal abstract class BaseTestRunner
    {
        /// <summary>
        /// Raised just before the runner starts the tests.
        /// </summary>
        public event TestsStartingHandler TestsStarting;

        /// <summary>
        /// Raised after a test has been run
        /// </summary>
        public event TestCompleteEventHandler TestComplete;

        /// <summary>
        /// Raised after all tests have been run
        /// </summary>
        public event AllTestsCompleteEventHandler AllTestsComplete;

        /// <summary>
        /// Emmit the TestComplete event
        /// </summary>
        /// <param name="raw">result of test in text form</param>
        /// <param name="parsed">result of in type form</param>
        protected void RaiseTestsStarting(string message)
        {
            if (TestsStarting != null)
            {
                TestsStarting(this, new TestsStartingEventArgs(message));
            }
        }

        /// <summary>
        /// Emmit the TestComplete event
        /// </summary>
        /// <param name="raw">result of test in text form</param>
        /// <param name="parsed">result of in type form</param>
        protected void RaiseComplete(string raw, TestResult parsed)
        {
            if (TestComplete != null)
            {
                TestComplete(this, new TestCompleteEventArgs(raw, parsed));
            }
        }

        /// <summary>
        /// Emit the AllTestsComplete event
        /// </summary>
        protected void RaiseAllComplete(SummaryResult result)
        {
            if (AllTestsComplete != null)
            {
                AllTestsComplete(this, new AllTestsCompleteEventArgs(result.PassCount, result.FailCount, result.SkipCount, result.Duration));
            }
        }

        #region Abstract methods
        public abstract void RunTests(string assemblyPath, string assembly);
        public abstract void RunTests(string assemblyPath, string assembly, string type);
        public abstract void RunTests(string assemblyPath, string assembly, string type, string method);

        #endregion

        /// <summary>
        /// Handle wiring up the events and running everything in a try catch block.
        /// </summary>
        /// <param name="runner">Which test runner to use when launching tests </param>
        /// <param name="startingHandler">method to wire up to start event</param>
        /// <param name="testCompleteHandler">method to recieve test complete event</param>
        /// <param name="allCompleteHandler">method to recieve all tests complete event</param>
        /// <param name="assemblyPath">File path to assembly containing tests </param>
        /// <param name="assemblyName">Full name of assembly containing tests </param>
        /// <param name="specificTestAction">Delegate that will call the correct testRunner method and do any other tasks associated with testing</param>
        public static void StandardRunTestBehavior(BaseTestRunner runner, 
            TestsStartingHandler startingHandler, 
            TestCompleteEventHandler testCompleteHandler, 
            AllTestsCompleteEventHandler allCompleteHandler,
            string assemblyPath,
            string assemblyName,
            RunTestSelectorAction specificTestAction)
        {
            try
            {
                runner.TestsStarting += startingHandler;
                runner.TestComplete += testCompleteHandler;
                runner.AllTestsComplete += allCompleteHandler;
                specificTestAction(runner, assemblyPath, assemblyName);
            }
            finally
            {
                runner.TestsStarting -= startingHandler;
                runner.TestComplete -= testCompleteHandler;
                runner.AllTestsComplete -= allCompleteHandler;
            }
        }
    }
}
