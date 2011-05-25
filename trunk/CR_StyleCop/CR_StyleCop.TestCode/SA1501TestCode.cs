// <copyright file="SA1501TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1501 rule - statements must not be on single line with curly brackets.
    /// </summary>
    public class SA1501TestCode
    {
        private void MethodName(string x)
        {
            if (string.IsNullOrEmpty(x)) { throw new ArgumentException("x is null or empty.", "x"); }

            lock (this) { x = x.Substring(0); }

            while (string.IsNullOrEmpty(x)) 
            { x = x.Substring(1); }
        }
    }
}
