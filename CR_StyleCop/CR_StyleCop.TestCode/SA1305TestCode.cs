// <copyright file="SA1305TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

#pragma warning disable 169

    /// <summary>
    /// Test code for SA1305 rule - fields can not use hungarian notation.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1305 rule.")]
    public class SA1305TestCode
    {
        private int isValid;

        private int bInvalid;

        private string sInvalid;
    }
}
