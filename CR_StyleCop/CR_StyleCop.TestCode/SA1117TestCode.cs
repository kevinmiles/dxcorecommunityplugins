// <copyright file="SA1117TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Test code for SA1117 rule - parameters must be on the same line or on separate lines each.
    /// </summary>
    public class SA1117TestCode
    {
        private int this[
            int x, int y,
            int z]
        {
            get
            {
                return this[
                    x, y,
                    z];
            }
        }

        private string JoinStrings(
            string x, string y,
            string z)
        {
            return this.JoinStrings(
                x, y,
                z);
        }
    }
}