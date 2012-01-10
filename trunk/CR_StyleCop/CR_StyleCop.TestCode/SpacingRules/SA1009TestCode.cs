// <copyright file="SA1009TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// Test code for SA1009 rule - closing paren must be spaced correctly.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1009 rule." )]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1017:ClosingAttributeBracketsMustBeSpacedCorrectly", Justification = "This is about SA1009 rule.") ]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1019:MemberAccessSymbolsMustBeSpacedCorrectly", Justification = "This is about SA1009 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1008:OpeningParenthesisMustBeSpacedCorrectly", Justification = "This is about SA1009 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1002:SemicolonsMustBeSpacedCorrectly", Justification = "This is about SA1009 rule.")]
    public class SA1009TestCode
    {
        private int MethodName( )
        {
            int x = 3 * ((1 + 2) * 5 );
            return (x + 3 ) * (5 * (1 + 2) );
        }

        private long MethodName2()
        {
            int x = this.MethodName() .CompareTo(11) ;
            int y = this.MethodName( ).CompareTo(11 );
            long z = (long) x;
            return (long) (z + y);
        }
    }
}
