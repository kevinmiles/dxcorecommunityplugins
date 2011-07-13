﻿// <copyright file="SA1206TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;

#pragma warning disable 169

    /// <summary>
    /// Test code for SA1206 rule - declaration keywords must be ordered correctly.
    /// </summary>
    public class SA1206TestCode
    {
        private static readonly int aaaa;
        static private readonly int bbbb;
        readonly static private int cccc;
        private readonly static int dddd;
        static readonly private int eeee;
        readonly private static int ffff;
    }
}