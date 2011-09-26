// <copyright file="SA1515TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test code for SA1515 rule - single line comments must be preceded by blank line.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1515 rule.")]
    public class SA1515TestCode
    {
        // Valid comment.
        // Another valid comment.
        private int MethodName()
        {
            int varName = 42;
            // Invalid comment.
            return varName;
        }
        // Another invalid comment.
    }
}
