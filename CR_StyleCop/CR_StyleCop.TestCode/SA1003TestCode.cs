// <copyright file="SA1003TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

#pragma warning disable 1591

    /// <summary>
    /// Test code for SA1003 rule - operators must be spaced correctly.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1003 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1008:OpeningParenthesisMustBeSpacedCorrectly", Justification = "This is about SA1003 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1009:ClosingParenthesisMustBeSpacedCorrectly", Justification = "This is about SA1003 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1012:OpeningCurlyBracketsMustBeSpacedCorrectly", Justification = "This is about SA1003 rule.")]
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1119:StatementMustNotUseUnnecessaryParenthesis", Justification = "This is about SA1003 rule.")]
    public class SA1003TestCode:object
    {
        public SA1003TestCode()
            :base()
        {
        }

        private void MethodName<T>(T x) where T :class
        {
            int varName=2;
            EventHandler handler = (o, s)=>{ };
            varName = 2 +3;
            varName = 2- 3;
            varName = 2*3;
            varName = 2 %3;
            varName = 2/ 3;
            varName = 2 >>3;
            varName = 2<< 3;
            varName+= 1;
            varName -=1;
            varName*=1;
            varName /=1;
            varName%= 1;
            varName>>=1;
            varName<<= 1;
            varName &=1;
            varName|= 1;
            varName^=1;
            varName = 1 &2;
            varName = 1| 2;
            varName = 1^2;
            varName = ~ 1;
            varName = true ?1: 2;
            varName = (int?)null ??1;

            bool boolVar = 2==3;
            boolVar = 2<= 3;
            boolVar = 2 >=3;
            boolVar = 2!=3;
            boolVar = true &&false;
            boolVar = true|| false;
            boolVar = ! true;
            boolVar =!true;
            boolVar = (x)is bool; // BUGBUG
            object oo = (x)as object; // BUGBUG

            if ( !boolVar)
            {
            }
        }
    }
}
