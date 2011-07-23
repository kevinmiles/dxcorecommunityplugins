// <copyright file="SA1207TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

#pragma warning disable 1591

    /// <summary>
    /// Test code for SA1207 rule - protected must come before internal.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1207 rule.")]
    public class SA1207TestCode
    {
        protected internal static void MethodName()
        {
        }

        internal protected static void MethodName2()
        {
        }

        internal static protected void MethodName3()
        {
        }

        protected static internal void MethodName4()
        {
        }
    }
}
