// <copyright file="SA1509TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode

{
    using System;

    /// <summary>
    /// Test code for SA1509 rule - opening curly bracket must not be preceded by blank line.
    /// </summary>
    public class SA1509TestCode

    {
        private object syncRoot = new object();

        private object SyncRoot
        
        {
            get
        
            {
                return this.syncRoot;
            }

            set

            {
                this.syncRoot = value;
            }
        }

        private void MethodName(string x)

        {
            if (string.IsNullOrEmpty(x))

            {
                throw new ArgumentException("x is null or empty.", " x");
            }

            while (string.IsNullOrEmpty(x))

            {
                x = x.Substring(1);
            }

            lock (this.syncRoot)
            
            {
                x = x.Substring(1);
            }

            for (int i = 0; i < 5; i++)
            
            {
                x = x.Substring(1);
            }

            foreach (int s in new int[] { 1, 2 })
            
            {
                x = x.Substring(1);
            }

            do
            
            {
                x = x.Substring(1);
            }
            while (string.IsNullOrEmpty(x));

            using (new System.IO.StreamReader("aaa"))
            
            {
                x = x.Substring(1);
            }
        }
    }
}
