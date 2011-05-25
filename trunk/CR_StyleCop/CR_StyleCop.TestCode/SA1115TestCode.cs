// <copyright file="SA1115TestCode.cs" company="ACME">
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
    /// Test code for SA1115 rule - parameter must be on the same or on the next line as previous parameter.
    /// </summary>
    public class SA1115TestCode
    {
        private int this[
            int x, 
            
            int y]
        {
            get
            {
                return this[
                    x, 
                    
                    y];
            }
        }

        private string JoinStrings(
            string first,

            string last)
        {
            return this.JoinStrings(
                first,

                last);
        }
    }
}
