// <copyright file="SA1510TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test code for SA1510 rule - chained elements must not be separated with blank line.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1510 rule.")]
    public class SA1510TestCode
    {
        private int MethodName(bool parameter)
        {
            int varName = 1;
            if (parameter)
            {
                varName++;
            }

            else
            {
                varName--;
            }

            try
            {
                varName++;
            }

            catch (NullReferenceException)
            {
                return varName;
            }

            catch (ArgumentNullException)
            {
                return varName;
            }

            finally
            {
                varName++;
            }

            return varName;
        }
    }
}
