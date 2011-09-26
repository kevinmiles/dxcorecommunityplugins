// <copyright file="SA1309TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

#pragma warning disable 169

    /// <summary>
    /// Test code for SA1309 rule - field name must not start with _.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1309 rule.")]
    public class SA1309TestCode
    {
        private int _fieldName;
    }
}
