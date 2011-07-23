// <copyright file="SA1112TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// Test code for SA1112 rule - closing paren must be on the same line as opening paren when there are no parameters.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1112 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "*", Justification = "This is about SA1112 rule.")]
    public class SA1112TestCode
    {
        private string JoinStrings(
            )
        {
            return this.JoinStrings(
                );
        }
    }
}
