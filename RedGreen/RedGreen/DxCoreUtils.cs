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
using DevExpress.CodeRush.Core;

namespace RedGreen
{
    /// <summary>
    /// As much as possible, keeps the knowledge of how to navigate DxCore internal structures in one spot
    /// </summary>
    internal static class DxCoreUtil
    {
        /// <summary>
        /// Get a language element from a location string.
        /// </summary>
        /// <param name="location">The location string for a method</param>
        /// <returns>A LanguageElement with a matching location string and a LanguageElementType equal to Method or null </returns>
        internal static LanguageElement GetTestMethod(string location)
        {
            try
            {
                if (CodeRush.Source.ActiveClass != null)
                {
                    LanguageElement testMethod = CodeRush.Source.ActiveClass.FirstChild;
                    while (testMethod != null)
                    {
                        if (testMethod.ElementType == LanguageElementType.Method && testMethod.Location == location)
                        {
                            return testMethod;
                        }
                        testMethod = testMethod.NextCodeSibling;
                    }
                }
            }
            catch
            {// fail siliently
            }
            return null;
        }

        /* Ended up not using this because invalidating the test attribute wasn't working reliably 
        internal static LanguageElement GetTestAttribute(List<BaseTestRunner> runners, string location)
        {
            return GetTestAttribute(runners, GetTestMethod(location));
        }

        internal static LanguageElement GetTestAttribute(List<BaseTestRunner> runners, LanguageElement method)
        {
            System.Diagnostics.Debug.Assert(method.ElementType == LanguageElementType.Method);
            LanguageElement testAttribute = method.PreviousNode;
            while (testAttribute != null)
            {
                if (IsTestAttribute(runners, testAttribute))
                {
                    return testAttribute;
                }
            }
            return null;
        }
        */

        /// <summary>
        /// Get a statement language element that matches the location and the line number
        /// </summary>
        /// <param name="location">What location to look for</param>
        /// <param name="failAtLine">The line that the location should be on</param>
        /// <returns></returns>
        /// <remarks>Assumes that the location is in the active file.</remarks>
        internal static LanguageElement GetStatement(string location, int failAtLine)
        {
            if (CodeRush.Source.ActiveSourceFile == null)
            {
                return null;
            }
            LanguageElement node = CodeRush.Source.ActiveSourceFile.GetNodeAt(new SourcePoint(failAtLine, 0));
            if (node != null)
            {//Checks full location first and then strips the root namespace and checks again, the latter is a VB work around.
                if (MatchesAssertLocationOrMethod(location, node.Location) || MatchesAssertLocationOrMethod(location.Substring(location.IndexOf(".") + 1), node.Location))
                {
                    LanguageElement statement = node.FirstChild;
                    while (statement != null)
                    {
                        if (statement.StartLine == failAtLine)
                        {
                            return statement;
                        }
                        statement = statement.NextCodeSibling;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Looks for an exact match (the assert location), or a partial match (the method location)
        /// </summary>
        /// <param name="desiredLocation"></param>
        /// <param name="nodeLocation"></param>
        /// <returns></returns>
        private static bool MatchesAssertLocationOrMethod(string desiredLocation, string nodeLocation)
        {
            return desiredLocation == nodeLocation || desiredLocation.StartsWith(nodeLocation);
        }
        /// <summary>
        /// Force a redraw of the class which contains the given language element
        /// </summary>
        /// <param name="element">what to invalidate</param>
        internal static void Invalidate(LanguageElement element)
        {
            TextView view = element.View as TextView;
            view.Invalidate(element);//(element.StartLine - 1, 0, element.EndLine + 1, 0);
        }
    }
}
