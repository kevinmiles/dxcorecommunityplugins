// <copyright file="SA1308TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

#pragma warning disable 169

    /// <summary>
    /// Test code for SA1308 rule - fields cannot use m_ or s_ prefixes.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1308 rule.")]
    public class SA1308TestCode
    {
        private static int s_field;
        private int m_field;
    }
}
