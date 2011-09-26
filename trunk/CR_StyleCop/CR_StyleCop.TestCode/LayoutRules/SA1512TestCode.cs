// <copyright file="SA1512TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test code for SA1512 rule - Single line comments must not be followed by blank line.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1512 rule.")]
    public class SA1512TestCode
    {
        // This is invalid comment.

        private void MethodName()
        {
            // This is valid comment.

            // This is continuation of valid comment.
        }
    }
}
