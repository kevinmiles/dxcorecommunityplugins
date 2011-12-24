// <copyright file="SA1005TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    //Uh oh
    //  Uh oh

    /// <summary>
    /// Test code for SA1005 - single line comments must start with single space.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "*", Justification = "This is about SA1005 rule.")]
    public class SA1005TestCode
    {
        //Uh oh
        //  Uh oh
        private void MethodName()
        {
            //Uh oh
            //  Uh oh
        }
    }
}
