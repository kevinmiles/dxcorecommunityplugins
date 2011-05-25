// <copyright file="SA1212TestCode.cs" company="ACME">
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
    /// Test code for SA1212 rule - getter must come before setter.
    /// </summary>
    public class SA1212TestCode
    {
        private string validOrder;
        private string invalidOrder;

        private string ValidOrder
        {
            get { return this.validOrder; }
            set { this.validOrder = value; }
        }

        private string InvalidOrder
        {
            set { this.invalidOrder = value; }
            get { return this.invalidOrder; }
        }
    }
}
