// <copyright file="SA1117TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// Test code for SA1117 rule - parameters must be on the same line or on separate lines each.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1117 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "*", Justification = "This is about SA1117 rule.")]
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1115:ParameterMustFollowComma", Justification = "This is about SA1117 rule.")]
    public class SA1117TestCode
    {
        private int this[
            int x, int y,
            int z]
        {
            get
            {
                return this[
                    x, y,
                    z];
            }
        }

        private string JoinStrings(
            string x, string y,
            string z)
        {
            return this.JoinStrings(
                x, y,
                z);
        }
    }
}