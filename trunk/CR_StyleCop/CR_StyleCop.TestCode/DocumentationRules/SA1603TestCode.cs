// <copyright file="SA1603TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;

#pragma warning disable 1570

    /// <summary>
    /// Test code for SA1603 rule - xml doc comment must be valid xml.
    /// <summary>
    public class SA1603TestCode
    {
        /// <summary>
        /// Another invalid tag.
        /// </summa3ry>
        public int PropertyName { get; set; }

        /// <summary>
        /// Invalid character < inside comment.
        /// </summary>
        public int PropertyName2 { get; set; }
    }
}
