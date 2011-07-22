// <copyright file="SA1310TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

#pragma warning disable 169

    /// <summary>
    /// Test code for SA1310 rule - field name must not contain _.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1310 rule.")]
    public class SA1310TestCode
    {
        private int field_name;

        // BUGBUG: SA1310 is not fired for parameters and local variables.
        private int MethodName(int param_name)
        { 
            int var_name = param_name;
            return var_name;
        }
    }
}
