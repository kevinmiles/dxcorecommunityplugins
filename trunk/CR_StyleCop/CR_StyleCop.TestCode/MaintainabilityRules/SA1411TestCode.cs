// <copyright file="SA1411TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test code for SA1411 rule - redundant parens from attributes.
    /// </summary>
    [SA1411TestCode.My()]
    [SA1411TestCode.MyAttribute()]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1411 rule.")]
    internal class SA1411TestCode
    {
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
        internal class MyAttribute : Attribute
        {
        }
    }
}