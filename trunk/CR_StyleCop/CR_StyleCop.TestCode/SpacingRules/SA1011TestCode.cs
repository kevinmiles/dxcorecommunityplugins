// <copyright file="SA1011TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

#pragma warning disable 168

    /// <summary>
    /// Test code for SA1011 rule - closing square bracket must be spaced correctly.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1010 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1010:OpeningSquareBracketsMustBeSpacedCorrectly", Justification = "This is about SA1010 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1012:OpeningCurlyBracketsMustBeSpacedCorrectly", Justification = "This is about SA1010 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1002:SemicolonsMustBeSpacedCorrectly", Justification = "This is about SA1010 rule.")]
    public class SA1011TestCode
    {
        private string[ ] s 
            = new[ ] { "a" };

        private string[]t 
            = new[]{ "a" };

        private int this[int index ]
        {
            get { return 42; }
        }

        private string MethodName()
        {
            string[]xt;
            return this.s[1 ]
                + this.t[0] ;
        }
    }
}
