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
    /// <summary>
    /// What happened when a test was run.
    /// </summary>
    public class TestResult
    {
        private string _MethodLocation;
        /// <summary>
        /// The full name of the test method run
        /// </summary>
        public string MethodLocation
        {
            get { return _MethodLocation; }
            set 
            {
                _MethodLocation = value;
            }
        }

        private string _AssertLocation;
        /// <summary>
        /// The full path to the assert that failed. 
        /// </summary>
        /// <remarks>Null unless the result reports a failure.</remarks>
        public string AssertLocation
        {
            get { return _AssertLocation; }
        }
        
        private TestStatus _Result;
        /// <summary>
        /// Pass/Fail/Skip status
        /// </summary>
        public TestStatus Result
        {
            get { return _Result; }
            set { _Result = value; }
        }

        private int _Position;
        /// <summary>
        /// The string offset where the expected result began to be different from the actual. Only filled in when a failure is reported.
        /// </summary>
        public int Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        private string _Expected;
        /// <summary>
        /// If available, the expected value
        /// </summary>
        public string Expected
        {
            get { return _Expected; }
            set { _Expected = value; }
        }

        private string _Actual;
        /// <summary>
        /// If available, the actual value the code under test supplied. 
        /// </summary>
        public string Actual
        {
            get { return _Actual; }
            set { _Actual = value; }
        }

        private string _Durration;
        /// <summary>
        /// How long it took to run the test
        /// </summary>
        public string Durration
        {
            get { return _Durration; }
            set { _Durration = value; }
        }
        
        private int _FailAtLine;
        /// <summary>
        /// The line number where the test failed. Only non-zero for failures
        /// </summary>
        public int FailAtLine
        {
            get { return _FailAtLine; }
            set 
            {
                _FailAtLine = value;
                _AssertLocation = MethodLocation;
                LanguageElement method = DxCoreUtil.GetTestMethod(MethodLocation);
                if (method != null && value > 0)
                {// Associate with assert 
                    LanguageElement statement = method.FirstChild;
                    while (statement.StartLine != FailAtLine)
                    {
                        statement = statement.NextCodeSibling;
                    }
                    _AssertLocation = statement.Location;
                }
            }
        }

    }
}
