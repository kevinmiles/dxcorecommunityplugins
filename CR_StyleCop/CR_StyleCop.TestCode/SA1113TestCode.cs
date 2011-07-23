// <copyright file="SA1113TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    /// <summary>
    /// Test code for SA1113 rule - comma must follow previous parameter.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1113 rule.")]
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "*", Justification = "This is about SA1113 rule.")]
    public class SA1113TestCode
    {
        private int this[
            int x
            , int y]
        {
            get
            {
                return this[
                    x
                    , y];
            }
        }

        private string JoinStrings(
            string first
            , string last)
        {
            return this.JoinStrings(
                first
                , last);
        }
    }
}