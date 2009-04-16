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

using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core;

namespace RedGreen
{
    /// <summary>
    /// Data that describes a test. 
    /// </summary>
    public class UnitTestDetail : ITestDetail
    {
        /// <summary>
        /// The text string for the Method. Supplied by the test runner and used to later get the DxCore objects which are associated
        /// </summary>
        public string Location { get; private set; }
        private Method _method;
        /// <summary>
        /// Structural parser data about the test method
        /// </summary>
        public Method Method
        {
            get
            {
	            if (_method == null)
			            _method = DxCoreUtil.GetMethod(Location);
	            return _method;
            }
       }
        private Attribute _Attribute;
        /// <summary>
        /// Structural parser data about the test attribute that describes the method as a test.
        /// </summary>
        public Attribute Attribute
        {
            get
            {
                if (_Attribute == null)
                {
                    _Attribute = DxCoreUtil.GetFirstTestAttribute(Method);
                }
                return _Attribute;
            }
            private set { _Attribute = value; }
        }
        /// <summary>
        /// Pass/Fail/Skip data given up by the test runner
        /// </summary>
        public TestResult Result { get; set; }

        /// <summary>
        /// Initializes a new instance of the TestInfo class.
        /// </summary>
        private UnitTestDetail()
        {
            Result = new TestResult();
        }

        /// <summary>
        /// Initializes a new instance of the TestInfo class.
        /// </summary>
        /// <param name="tagProvider"></param>
        /// <param name="method"></param>
        public UnitTestDetail(Method method, SmartTagProvider tagProvider)
            : this()
        {
            _method = method;
            Location = method.Location;
            SmartTagProvider = tagProvider;
        }

        public UnitTestDetail(string location, SmartTagProvider tagProvider)
            : this()
        {
            Location = location;
            try
            {
                _method = DxCoreUtil.GetMethod(location);
            }
            catch
            {// fail silently, try to get it later
            }
        }
        public SmartTagProvider SmartTagProvider { get; private set; }

        public SourcePoint IconCoordinates
        {
            get { return Attribute.Range.Start; }
        }

        public string ClassName
        {
            get { return ((Class)Method.Parent).FullName; }
        }

        public string MethodName
        {
            get{return Method.Name;}
        }
    }
}
