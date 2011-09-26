// <copyright file="SA1115TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// Test code for SA1115 rule - parameter must be on the same or on the next line as previous parameter.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1115 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "*", Justification = "This is about SA1115 rule.")]
    public class SA1115TestCode
    {
        private int this[
            int x, 
            
            int y]
        {
            get
            {
                return this[
                    x, 
                    
                    y];
            }
        }

        private string JoinStrings(
            string first,

            string last)
        {
            return this.JoinStrings(
                first,

                last);
        }
    }
}
