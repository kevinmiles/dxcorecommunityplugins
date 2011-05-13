// <copyright file="SA1500TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1500 rule - curly brackets must be on own line.
    /// </summary>
    public class SA1500TestCode {
        private int i = 10;

        private void MethodName() {
            while (this.i < 20) {
                this.i++;
            }

            while (this.i < 30)
            {
                this.i++; }

            while (this.i < 40) {
                this.i++; }
        }
    }
}
