// <copyright file="SA1023TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

#pragma warning disable 219
#pragma warning disable 414

    /// <summary>
    /// Test code for SA1023 rule - dereference symbol must be spaced correctly.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1023 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1003:SymbolsMustBeSpacedCorrectly", Justification = "This is about SA1023 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1008:OpeningParenthesisMustBeSpacedCorrectly", Justification = "This is about SA1023 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1010:OpeningSquareBracketsMustBeSpacedCorrectly", Justification = "This is about SA1023 rule.")]
    public unsafe class SA1023TestCode
    {
        private int
            * x1 = null;

        private int * x2 = null;
        private int*x3 = null; // BUGBUG
        private int* [] y4 = null; // BUGBUG

        private void MethodName(int p)
        {
            int
                * y1 = null;
            int z1 = *
                y1;

            int * y2 = null;
            int z2 = * y2;
            int*y3 = null; // BUGBUG
            int z3 =*y3; // BUGBUG
            int* [] y4 = null; // BUGBUG
            int[] zz = null;
            int z4 = zz[ *y3]; // BUGBUG
            this.MethodName( *y3); // BUGBUG

            DateTime * date = null;
            int day = ( *date).Day; // BUGBUG
        }
    }
}
