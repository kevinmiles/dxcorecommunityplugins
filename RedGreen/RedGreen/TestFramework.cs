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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using Xunit;
using System.Text.RegularExpressions;

namespace RedGreen
{
    /// <summary>
    /// A description of testing framework.
    /// </summary>
    struct TestFramework
    {
        public string Name;
        public string Attribute;
        public string AlternateAttribute;
        /// <summary>
        /// Initializes a new instance of the TestFramework structure.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="attributeName"></param>
        public TestFramework(string name, string attributeName)
            : this(name, attributeName, String.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TestFramework structure.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="attributeName"></param>
        /// <param name="alternateAttributeName"></param>
        public TestFramework(string name, string attributeName, string alternateAttributeName)
        {
            Name = name;
            Attribute = attributeName;
            AlternateAttribute = alternateAttributeName;
        }

        internal static readonly TestFramework XUnit = new TestFramework("Xunit", "Fact", "Theroy");
        internal static readonly TestFramework NUnit = new TestFramework("NUnit.Framework", "Test");
        internal static readonly TestFramework MbUnit = new TestFramework("MbUnit.Framework", "Test");
        internal static readonly TestFramework MsUnit = new TestFramework("MsUnit", "TestMethod");
    }
}
