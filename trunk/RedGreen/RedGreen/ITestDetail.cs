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
    /// Implementations provide just enough detail about a test to be able to 
    /// display icons and run tests with runners.
    /// </summary>
    interface ITestDetail
    {
        /// <summary>
        /// Supplies the actions available when the icon/tile is activated in the UI
        /// </summary>
        SmartTagProvider SmartTagProvider { get; }

        /// <summary>
        /// Where the icon should be displayed.
        /// </summary>
        SourcePoint IconCoordinates { get; }

        /// <summary>
        /// The full name of the class so that test runners can correctly reference the test
        /// </summary>
        string ClassName { get; }

        /// <summary>
        /// The test name, used when a specific test will be run. 
        /// </summary>
        string MethodName { get; }
    }
}
