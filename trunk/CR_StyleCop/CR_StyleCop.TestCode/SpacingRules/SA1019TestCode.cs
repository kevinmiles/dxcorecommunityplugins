// <copyright file="SA1019TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using global ::System;
    using global:: System.Diagnostics.CodeAnalysis;
    using global::System .Linq;

    /// <summary>
    /// Test code for SA1019 rule - member access symbol must be spaced correctly.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1019 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1009:ClosingParenthesisMustBeSpacedCorrectly", Justification = "This is about SA1015 rule.")]
    public class SA1019TestCode
    {
        private void MethodName()
        {
            this .MethodName();
            this. MethodName();
        }

        private unsafe int CalculateInt()
        {
            int x = this.CalculateInt() .GetHashCode();
            x = this.CalculateInt(). GetHashCode();
            x = this.CalculateInt().
                GetHashCode();
            x = this.CalculateInt()
                .GetHashCode();

            DateTime* date = null;
            int day2 = date ->Day;
            int day3 = date-> Day;
            int day4 = date->
                Day;
            int day5 = date
                ->Day;

            return x + day2 + day3 + day4 + day5;
        }

        private int CalculateInt2()
        {
            return this.CalculateInt2()
                .GetHashCode().
                GetHashCode();
        }
    }
}
