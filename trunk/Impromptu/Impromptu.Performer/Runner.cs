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
using System.Linq;
using System.Reflection;

namespace Impromptu.Performer
{
    public class Runner
	{
		private IOutputWriter output;
		public Runner(IOutputWriter writer)
		{
			output = writer;
		}

		public Runner()
		{
			
		}
		public void ExecuteMethod(string[] args)
		{
			if (output == null)
            {
				output = new ConsoleWriter();
            }
			var parameters = ParameterParser.Parse(args);
			Parameter path = parameters.FirstOrDefault(param => param.Name.ToLower() == "assemblypath");
			Parameter typeName = parameters.FirstOrDefault(param => param.Name.ToLower() == "typename");
			Parameter methodName = parameters.FirstOrDefault(param => param.Name.ToLower() == "methodname");
			if (!ValidatePath(path))
				return;
			if (!ValidateType(typeName))
				return;
			if (!ValidateMethod(methodName))
				return;

			try
			{
				// Load Assembly
				Assembly impromptuAssembly = Assembly.LoadFile(path.Value);
				
				// Create type requested
				Type impromptuType = impromptuAssembly.GetType(typeName.Value);
				if (impromptuType == null)
                {
					output.WriteLine("Could not locate the {0}", typeName.Value);
					return;
                }
				object instance = Activator.CreateInstance(impromptuType);

				// locate requested method 
				MethodInfo impromptuMethod = impromptuType.GetMethod(methodName.Value, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.InvokeMethod);
				if (impromptuMethod == null)
                {
					output.WriteLine("Could not locate {0} in {1}", methodName.Value, typeName.Value);
					return;
                }

				// Execute the method 
				object result = impromptuMethod.Invoke(instance, null);
				if (result != null)
                {
					output.WriteLine(result.ToString());
                }
			}
			catch (MissingMethodException mme)
			{
				output.WriteLine("{0}: {1}", mme.Message, typeName.Value);
			}
			catch (TargetInvocationException te)
			{
				output.WriteLine(te.InnerException.Message);
			}
			catch (Exception ex)
			{
				output.WriteLine(ex.Message);
			}
		}

		private bool ValidatePath(Parameter path)
		{
			if (path == null || string.IsNullOrEmpty(path.Value))
            {
				output.WriteLine("The /AssemblyPath argument is missing or incomplete");
				return false;
            }
			if (File.Exists(path.Value) == false)
            {
				output.WriteLine("Requested assembly not found");
				return false;
            }
			return true;
		}

		private bool ValidateType(Parameter typeName)
		{
			if (typeName == null || string.IsNullOrEmpty(typeName.Value))
            {
				output.WriteLine("The /TypeName argument is missing or incomplete");
				return false;
            }
			return true;
		}

		private bool ValidateMethod(Parameter methodName)
		{
			if (methodName == null || string.IsNullOrEmpty(methodName.Value))
            {
				output.WriteLine("The /MethodName argument is missing or incomplete");
            }
			return true;
		}
	}
}
