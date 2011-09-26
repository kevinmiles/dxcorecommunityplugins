// <copyright file="SA1122TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;

#pragma warning disable 414

    /// <summary>
    /// Test code for SA1122 rule - string.Empty should be used for empty strings.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1121 rule.")]
    public class SA1122TestCode
    {
        [Category("")]
        private string s1 = "";

        [Category(@"")]
        private string s2 = @"";

        private string s3 = string.Empty;
    }
}
