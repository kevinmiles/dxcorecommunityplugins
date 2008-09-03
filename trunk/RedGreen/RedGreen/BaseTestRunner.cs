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
using DevExpress.CodeRush.StructuralParser;

namespace RedGreen
{
    /// <summary>
    /// Provides many commonly used functionality used by the test runners created to date
    /// </summary>
    abstract class BaseTestRunner
    {
        public event TestCompleteEventHandler TestComplete;
        public event AllTestsCompleteEventHandler AllTestsComplete;

        /// <summary>
        /// Returns true if the namespace is one of the supported frameworks
        /// </summary>
        /// <param name="framework">A namespace of a potential supported framework</param>
        public bool RunsTestsForNamespace(string framework)
        {
            return SupportedFrameworks.Contains(framework);
        }

        /// <summary>
        /// Determines if a method has a test attribute 
        /// </summary>
        /// <param name="potentialTest">Attribute source</param>
        /// <returns>True if the method is decorated with a test attribute</returns>
        public bool IsTest(Method potentialTest)
        {
            if (potentialTest != null)
            {
                foreach (object attribute in potentialTest.Attributes)
                {
                    if (SupportedAttributes.Contains(attribute.ToString()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        /// <summary>
        /// Determines if an attribute is one this plug-in considers a test
        /// </summary>
        /// <param name="element">The attribute to validate</param>
        /// <returns>True if the method is in the list of supported attributes</returns>
        public bool IsTestAttribute(LanguageElement element)
        {
            return element.ElementType == LanguageElementType.Attribute && SupportedAttributes.Contains(element.ToString());
        }

        /// <summary>
        /// Add to the list of frameworks the plug-in has the ability to run.
        /// </summary>
        /// <param name="testFrameworks">namespace of framework</param>
        protected void AddFrameworks(params string[] testFrameworks)
        {
            _supportedFrameworks.AddRange(testFrameworks);
        }

        /// <summary>
        /// Add to the list of attributes that can decorate a test method
        /// </summary>
        /// <param name="testAttributes">Attribrute name as used, not the attribute class name</param>
        protected void AddAttributes(params string[] testAttributes)
        {
            _supportedAttributes.AddRange(testAttributes);
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
        /// <param name="passed">number of tests passed</param>
        /// <param name="failed">number of tests failed</param>
        /// <param name="skipped">number of tests skipped</param>
        /// <param name="duration">time elapsed to run tests</param>
        protected void RaiseAllComplete(string passed, string failed, string skipped, string duration)
        {
            if (AllTestsComplete != null)
            {
                AllTestsComplete(this, new AllTestsCompleteEventArgs(passed, failed, skipped, duration));
            }
        }

        #region Properties
        private readonly List<string> _supportedFrameworks = new List<string>();
        public List<string> SupportedFrameworks
        {
            get { return _supportedFrameworks; }
        }

        private readonly List<string> _supportedAttributes = new List<string>();
        public List<string> SupportedAttributes
        {
            get { return _supportedAttributes; }
        }

        #endregion
    }
}
