// <copyright file="SA1113TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Test code for SA1113 rule - comma must follow previous parameter.
    /// </summary>
    public class SA1113TestCode
    {
        private int this[
            int x
            , int y]
        {
            get
            {
                return this[
                    x
                    , y];
            }
        }

        private string JoinStrings(
            string first
            , string last)
        {
            return this.JoinStrings(
                first
                , last);
        }
    }
}