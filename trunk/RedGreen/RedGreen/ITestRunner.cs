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
using System.Text;
using DevExpress.CodeRush.StructuralParser;

namespace RedGreen
{
    delegate void TestCompleteEventHandler(object sender, TestCompleteEventArgs args);
    delegate void AllTestsCompleteEventHandler(object sender, AllTestsCompleteEventArgs args);

    interface ITestRunner
    {
        /// <summary>
        /// Raised after a test has been run
        /// </summary>
        event TestCompleteEventHandler TestComplete;

        /// <summary>
        /// Raised after all tests have been run
        /// </summary>
        event AllTestsCompleteEventHandler AllTestsComplete;

        /// <summary>
        /// Returns true if the namespace is one of the supported frameworks
        /// </summary>
        /// <param name="framework">A namespace of a potential supported framework</param>
        bool RunsTestsForNamespace(string framework);
        
        /// <summary>
        /// Determine if this method has a test attribute this plug-in supports
        /// </summary>
        /// <param name="potentialTest">Attribute source</param>
        /// <returns>True if the attribute has an attribute the plug-in supports</returns>
        bool IsTest(Method potentialTest);
        
        /// <summary>
        /// Determine if this attribute is one supported by this plug-in
        /// </summary>
        /// <param name="element">Attribute</param>
        /// <returns></returns>
        bool IsTestAttribute(LanguageElement element);
        
        /// <summary>
        /// Run a single unit test
        /// </summary>
        /// <param name="assemblyPath">The assembly that contains the unit test.</param>
        /// <param name="className">The full name of the class that contains the unit test.</param>
        /// <param name="methodName">The name of the unit test method</param>
        void RunMethod(string assemblyPath, string assemblyName, string className, string methodName);
        
        /// <summary>
        /// Run all the unit tests in the class
        /// </summary>
        /// <param name="assemblyPath">The assembly that contains the unit test.</param>
        /// <param name="assemblyName">The name of the assembly to test.</param>
        /// <param name="className">The full name of the class that contains the unit test.</param>
        void RunClass(string assemblyPath, string assemblyName, string className);
    }
}
