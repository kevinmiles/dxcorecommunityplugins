// <copyright file="SA1110TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// Test code for SA1110 rule - opening paren must be on declaration line.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1110 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "*", Justification = "This is about SA1110 rule.")]
    public class SA1110TestCode
    {
        private int this
            [int x]
        {
            get
            {
                return this
                    [x];
            }
        }

        private string JoinStrings
            (string first, string last)
        {
            return this.JoinStrings
                (first, last);
        }
    }
}