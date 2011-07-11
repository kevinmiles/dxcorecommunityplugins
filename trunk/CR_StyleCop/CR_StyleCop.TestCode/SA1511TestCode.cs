// <copyright file="SA1511TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test code for SA1511 rule - do while must not be separated by blank line.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1511 rule.")]
    public class SA1511TestCode
    {
        private void MethodName(bool paramName)
        {
            int varName = 42;

            do
            {
                varName++;
            }

            while (paramName);
        }
    }
}
