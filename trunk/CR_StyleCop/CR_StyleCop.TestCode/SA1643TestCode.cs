// <copyright file="SA1643TestCode.cs" company="ACME">
// Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Test code for SA1643 rule - destructor summary must start with standard text.
    /// </summary>
    public class SA1643TestCode
    {
        /// <summary>
        /// Non standard text.
        /// </summary>
        ~SA1643TestCode()
        {
        }
    }
}
