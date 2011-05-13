// <copyright file="SA1511TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1511 rule - do while must not be separated by blank line.
    /// </summary>
    public class SA1511TestCode
    {
        private void MethodName(bool paramName)
        {
            int varName = 42;

            do
            {
                varName++;
            }

            while (paramName);
        }
    }
}
