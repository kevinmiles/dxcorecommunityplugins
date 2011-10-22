// <copyright file="SA1118TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// Test code for SA1118 rule - only first parameter can span multiple lines.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1117 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "*", Justification = "This is about SA1117 rule.")]
    public class SA1118TestCode
    {
        private int this[
            int x,
            int
            y]
        {
            get
            {
                return this[
                    x,
                    y
                    + y];
            }
        }

        private string JoinStrings(
            string x,
            string
            y)
        {
            return this.JoinStrings(
                x,
                y
                + y);
        }
    }
}