// <copyright file="SA1601TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;

#pragma warning disable 1591

    public partial class SA1601TestCode
    {
        partial void MethodName();
    }

    public partial class SA1601TestCode
    {
        partial void MethodName()
        {
        }
    }
}
