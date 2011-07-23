// <copyright file="SA1114TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// Test code for SA1114 rule - parameters must be on the same or on the next line as opening paren.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1114 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "*", Justification = "This is about SA1114 rule.")]
    public class SA1114TestCode
    {
        private int this[

            int x]
        {
            get
            {
                return this[

                    x];
            }
        }

        private string JoinStrings(

            string first)
        {
            return this.JoinStrings(

                first);
        }
    }
}