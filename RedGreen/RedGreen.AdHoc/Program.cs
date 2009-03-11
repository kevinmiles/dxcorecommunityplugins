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
using System.IO;
using System.Reflection;

namespace RedGreen.AdHoc
{
    /// <summary>
    /// Use reflection to and late binding to run parameterless methods
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string assembly = ParseAssemblyArgument(args);
            if (string.IsNullOrEmpty(assembly))
            {
                Console.WriteLine("No /Assembly argument.");
                return;
            }
            if (File.Exists(assembly) == false)
            {
                Console.WriteLine("File not found: {0}", assembly);
            }

            Assembly a = null;
            try
            {
                a = Assembly.LoadFrom(assembly);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            string type = ParseTypeArgument(args);
            if (string.IsNullOrEmpty(type))
            {
                Console.WriteLine("No /Type argument.");
                return;
            }
            Type fixtureType = a.GetType(type);
            if (fixtureType == null)
            {
                Console.WriteLine("Unable to load type from assembly");
                return;
            }
            object fixture = Activator.CreateInstance(fixtureType);
            
            string method = ParseMemberArgument(args);
            if (string.IsNullOrEmpty(type))
            {
                Console.WriteLine("No /Member argument.");
                return;
            }

            MethodInfo testInfo = fixtureType.GetMethod(method, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.InvokeMethod);
            if (testInfo == null)
            {
                Console.WriteLine("Method not found");
                return;
            }
            try
            {
                object result = testInfo.Invoke(fixture, null);
                if (result != null)
                {
                    Console.WriteLine(result);
                }
            }
            catch (System.Reflection.TargetInvocationException ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
        }

        private static string ParseAssemblyArgument(string[] args)
        {
            return GetSwithchContent(args, "assembly");
        }

        private static string ParseTypeArgument(string[] args)
        {
            return GetSwithchContent(args, "type");
        }

        private static string ParseMemberArgument(string[] args)
        {
            return GetSwithchContent(args, "member");
        }

        private static string GetSwithchContent(string[] args, string assemblySwitch)
        {
            const string switchFormat = "/{0}:";
            string abreviated = string.Format(switchFormat, assemblySwitch.Substring(0, 1));
            string full = string.Format(switchFormat, assemblySwitch);
            foreach (string argument in args)
            {
                string a = argument.ToLower();
                if (argument.StartsWith(abreviated) || argument.StartsWith(full))
                {
                    return argument.Substring(argument.IndexOf(":") + 1);
                }
            }
            return String.Empty;
        }
    }
}
