// <copyright file="SA1406TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test code for SA1406 rule - Debug.Fail must provide message.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "*", Justification = "This is about SA1406 rule")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1406 rule.")]
    public class SA1406TestCode
    {
        private const string MessageConstant = "";
        private const string MessageConstant2 = null;

        private void MethodName(bool val)
        {
            Debug.Fail(@"");
            Debug.Fail("");
            Debug.Fail(String.Empty);
            Debug.Fail(System.String.Empty);
            Debug.Fail(global::System.String.Empty);
            Debug.Fail(string.Empty);
            Debug.Fail(null);
            string message = null;
            Debug.Fail(message);
            Debug.Fail(MessageConstant);
            Debug.Fail(MessageConstant2);
        }
    }
}