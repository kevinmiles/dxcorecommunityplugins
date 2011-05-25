// <copyright file="SA1614TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1614 rule - parameter documentation must be filled with text.
    /// </summary>
    public class SA1614TestCode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SA1614TestCode"/> class.
        /// </summary>
        /// <param name="parameter"></param>
        public SA1614TestCode(string parameter)
        {
        }

        /// <summary>
        /// Some delegate.
        /// </summary>
        /// <param name="parameter"></param>
        public delegate void MyDelegate(object parameter);

        /// <summary>
        /// Some indexer.
        /// </summary>
        /// <returns>The value 42.</returns>
        /// <param name="parameter"></param>
        public int this[int parameter]
        {
            get { return 42; }
        }

        /// <summary>
        /// Some method.
        /// </summary>
        /// <param name="parameter"></param>
        public void MethodName(string parameter)
        {
        }
    }
}
