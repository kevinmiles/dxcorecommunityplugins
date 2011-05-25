// <copyright file="SA1619TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1619 rule - generic parameters must be documented for partial classes.
    /// </summary>
    public partial class SA1619TestCode<T>
    {
        /// <summary>
        /// Implementation of method.
        /// </summary>
        partial void Method<TT>()
        {
        }
    }

    /// <content>
    /// Content description.
    /// </content>
    public partial class SA1619TestCode<T>
    {
        /// <content>
        /// Declaration of method.
        /// </content>
        partial void Method<TT>();
    }
}
