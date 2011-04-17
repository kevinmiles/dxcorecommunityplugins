﻿namespace CR_StyleCop.TestCode
{
    using System;


    /// <summary>
    /// Test code for SA1507 rule - multiple blank lines are bad.
    /// </summary>
    public class SA1507TestCode
    {
        private int PropertyName { get; set; }


        private void MethodName()
        {
            int varName = 6;
   

            this.PropertyName = varName;
        }
    }
}
