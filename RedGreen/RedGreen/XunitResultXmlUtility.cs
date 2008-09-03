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
using System.Xml;
using Xunit;

namespace RedGreen
{
    /// <summary>
    /// Utilities used by the xUnit test runner. This is a copy paste of code supplied by xUnit 
    /// </summary>
    public class XunitResultXmlUtility
    {
        public static XmlNode GetResult(XmlNode assemblyOrClassNode)
        {
            return GetResult(assemblyOrClassNode, 0);
        }

        public static XmlNode GetResult(XmlNode assemblyOrClassNode, int testIndex)
        {
            if (assemblyOrClassNode.Name == "assembly")
                return GetResult(assemblyOrClassNode, 0, testIndex);
            return GetResultFromClass(assemblyOrClassNode, testIndex);
        }

        public static XmlNode GetResult(XmlNode assemblyNode, int classIndex, int testIndex)
        {
            XmlNodeList classNodes = assemblyNode.SelectNodes("class");
            if (classNodes.Count <= classIndex)
                throw new ArgumentException("Could not find class item with index " + classIndex + " in XML:\r\n" + assemblyNode.OuterXml);
            return GetResultFromClass(classNodes[classIndex], testIndex);
        }

        public static XmlNode GetResultFromClass(XmlNode classNode, int testIndex)
        {
            XmlNodeList testNodes = classNode.SelectNodes("test");
            if (testNodes.Count <= testIndex)
                throw new ArgumentException("Could not find test item with index " + testIndex + " in XML:\r\n" + classNode.OuterXml);
            return testNodes[testIndex];
        }


    }
}
