// <copyright file="SA1026TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// Test code for SA1026 rule - new keyword in array initializer must be spaced correctly.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1026 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1010:OpeningSquareBracketsMustBeSpacedCorrectly", Justification = "This is about SA1026 rule.")]
    public class SA1026TestCode
    {
        private int[] ints = new [] { 1, 10, 100, 1000 };

        private void MethodName()
        {
            var a = new [] { 1, 10, 100, 1000 };
            var b = new
                [] 
                { 
                    1, 10, 100, 1000 
                };
        }
    }
}
