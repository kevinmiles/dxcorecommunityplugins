// <copyright file="SA1008TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// Test code for SA1008 rule - opening paren must be spaced correctly.
    /// </summary>
    [SuppressMessage( "StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1008 rule.")]
    [SuppressMessage ("StyleCop.CSharp.SpacingRules", "SA1003:SymbolsMustBeSpacedCorrectly", Justification = "This is about SA1008 rule.")]
    public class SA1008TestCode
    {
        private int MethodName ()
        {
            int x = 3 * (( 1 + 2) * 5);
            return ( x + 3) *((1 + 2) * 5);
        }
    }
}
