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
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;

namespace RedGreen
{
    /// <summary>
    /// Details for an ad hoc class. The interface allows one api to be used for either ad hoc or unit tests.
    /// This class is a smart tag factory and contains details about where the test is. Once created we have enough information to run a test if requested.
    /// </summary>
    class AdHocDetail : ITestDetail
    {
        private Method _method;

        private AdHocDetail()
        {
        }

        /// <summary>
        /// Create the detail class.
        /// </summary>
        /// <param name="test"></param>
        public AdHocDetail(Method test, SmartTagProvider tagProvider)
        {
            _method = test;
            SmartTagProvider = tagProvider;
        }

        /// <summary>
        /// Supplies the smart tag provider. It should be one for ad hoc tests if the object is constructed correctly
        /// </summary>
        public SmartTagProvider SmartTagProvider { get; private set; }

        /// <summary>
        /// Where the test icon should live, for ad hoc tests the icon is associated with the method signature
        /// </summary>
        public SourcePoint IconCoordinates
        {
            get { return _method.Range.Start; }
        }

        /// <summary>
        /// The full name of the class so that the testing engines can reference the test with reflection.
        /// </summary>
        public string ClassName
        {
            get { return ((Class)_method.Parent).FullName; }
        }

        /// <summary>
        /// The name of the method that is a test which will be run.
        /// </summary>
        public string MethodName
        {
            get { return _method.Name; }
        }
    }
}
