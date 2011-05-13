// <copyright file="SA1613TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1613 rule - param tag must specify parameter name.
    /// </summary>
    public class SA1613TestCode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SA1613TestCode"/> class.
        /// </summary>
        /// <param>Not existing parameter.</param>
        public SA1613TestCode(string parameter)
        {
        }

        /// <summary>
        /// Some delegate.
        /// </summary>
        /// <param>Not existing parameter.</param>
        public delegate void MyDelegate(object parameter);

        /// <summary>
        /// Some indexer.
        /// </summary>
        /// <param>Not existing parameter.</param>
        /// <returns>The value 42.</returns>
        public int this[int parameter]
        {
            get { return 42; }
        }

        /// <summary>
        /// Some method.
        /// </summary>
        /// <param>Not existing parameter.</param>
        public void MethodName(string parameter)
        {
        }
    }
}
