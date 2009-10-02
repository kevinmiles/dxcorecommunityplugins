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
        /// Get a statement language element that matches the location and the line number
        /// </summary>
        /// <param name="location">What location to look for</param>
        /// <param name="failAtLine">The line that the location should be on</param>
        /// <returns></returns>
        /// <remarks>Assumes that the location is in the active file.</remarks>
        internal static LanguageElement GetStatement(string location, int failAtLine)
        {
            try
            {
                if (CodeRush.Source.ActiveSourceFile == null)
                {
                    return null;
                }
                LanguageElement node = CodeRush.Source.ActiveSourceFile.GetNodeAt(new SourcePoint(failAtLine, 0));
                if (node != null)
                {
                    if (location == node.RootNamespaceLocation || location.StartsWith(node.RootNamespaceLocation))
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
            }
            catch
            {// eat exceptions
            }
            return null;
        }

        /// <summary>
        /// Force a redraw of the class which contains the given language element
        /// </summary>
        /// <param name="element">what to invalidate</param>
        internal static void Invalidate(LanguageElement element)
        {
            if (element != null)
            {
                TextView view = element.View as TextView;
                view.Invalidate(element);//(element.StartLine - 1, 0, element.EndLine + 1, 0);
            }
            else
            {
                CodeRush.TextViews.Active.Invalidate();
            }
        }

        /// <summary>
        /// Iterate through the known methods looking for one with the given location
        /// </summary>
        static public Method GetMethod(string location)
        { //I am sure there is a better way, but this is as good as I have gotten so far.
            foreach (SourceFile file in CodeRush.Source.ActiveSolution.AllFiles)
            {
                foreach (LanguageElement t in file.AllTypes)
                {
                    if (location.StartsWith(t.RootNamespaceLocation))
                    {
                        LanguageElement methodElement = t.FindChildByElementType(LanguageElementType.Method);
                        while (methodElement != null)
                        {
                            if (methodElement.RootNamespaceLocation == location)
                            {
                                return (Method)methodElement;
                            }
                            methodElement = methodElement.NextCodeSibling;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Attempts to get the attribute attached the the parent method that is a test attribute
        /// </summary>
        public static Attribute GetFirstTestAttribute(LanguageElement element)
        {
			Method method = GetMethod(element);
            if (method != null && method.AttributeCount > 0)
            {
                foreach (Attribute attribute in method.Attributes)
                {
                    if (IsTest(attribute))
                    {
                        return attribute;
                    }
                }
            }
            return null;
        }

        static readonly List<string> supportedAttributes = new List<string>(new string[] { "Test", "Fact", "TestMethod" });

        /// <summary>
        /// Determines if the attribute is a known test attribute
        /// </summary>
        public static bool IsTest(Attribute attribute)
        {
            return supportedAttributes.Contains(attribute.ToString()) && attribute.TargetNode.ElementType == LanguageElementType.Method;
        }

        /// <summary>
        /// Get the method that contains the given LanguageElement
        /// </summary>
        public static Method GetMethod(LanguageElement element)
        {
            if (element.ElementType == LanguageElementType.Method)
            {
                return (Method)element;
            }
            if (element.InsideMethod)
            {
                LanguageElement method = element;
                while (method.ElementType != LanguageElementType.Method)
                {
                    method = method.Parent;
                }
                return (Method)method;
            }
            return null;
        }
    }
}
