// <copyright file="SA1611TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1611 rule - parameters must be documented.
    /// </summary>
    public class SA1611TestCode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SA1611TestCode"/> class.
        /// </summary>
        public SA1611TestCode(string parameter)
        {
        }

        /// <summary>
        /// Some delegate.
        /// </summary>
        public delegate void MyDelegate(object parameter);

        /// <summary>
        /// Some indexer.
        /// </summary>
        /// <returns>The value 42.</returns>
        public int this[int parameter]
        {
            get { return 42; }
        }

        /// <summary>
        /// Some method.
        /// </summary>
        public void MethodName(string parameter)
        {
        }
    }
}
