// <copyright file="SA1021TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

#pragma warning disable 414
#pragma warning disable 1591

    /// <summary>
    /// Test code for SA1021 rule - negative sign must be spaced correctly.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1021 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1000:KeywordsMustBeSpacedCorrectly", Justification = "This is about SA1021 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1003:SymbolsMustBeSpacedCorrectly", Justification = "This is about SA1021 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1008:OpeningParenthesisMustBeSpacedCorrectly", Justification = "This is about SA1021 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1010:OpeningSquareBracketsMustBeSpacedCorrectly", Justification = "This is about SA1021 rule.")]
    public class SA1021TestCode
    {
        private int intVar = -
            42;

        private int intVar2 = - 42;
        private int intVar3 =-42;

        public int PropertyName
        {
            get { return-42; }
        }

        private int MethodName(int paramName)
        {
            // BUGBUG: 
            List<int> list = new List<int>(this.MethodName( -42));
            return list[ -42];
        }
    }
}
