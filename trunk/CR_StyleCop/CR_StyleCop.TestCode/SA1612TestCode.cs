// <copyright file="SA1612TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1612 rule - comments must match parameters list.
    /// </summary>
    public class SA1612TestCode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SA1612TestCode"/> class.
        /// </summary>
        /// <param name="parameter2">Not existing parameter.</param>
        public SA1612TestCode(string parameter)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SA1612TestCode"/> class.
        /// </summary>
        /// <param name="intParameter">Should be second.</param>
        /// <param name="parameter">Should be first.</param>
        public SA1612TestCode(string parameter, int intParameter)
        {
        }

        /// <summary>
        /// Some delegate.
        /// </summary>
        /// <param name="parameter2">Not existing parameter.</param>
        public delegate void MyDelegate(object parameter);

        /// <summary>
        /// Some delegate.
        /// </summary>
        /// <param name="intParameter">Should be second.</param>
        /// <param name="parameter">Should be first.</param>
        public delegate void MyDelegate2(object parameter, int intParameter);

        /// <summary>
        /// Some indexer.
        /// </summary>
        /// <param name="parameter2">Not existing parameter.</param>
        /// <returns>The value 42.</returns>
        public int this[int parameter]
        {
            get { return 42; }
        }

        /// <summary>
        /// Some indexer.
        /// </summary>
        /// <param name="intParameter">Should be second.</param>
        /// <param name="parameter">Should be first.</param>
        /// <returns>The value 42.</returns>
        public int this[int parameter, int intParameter]
        {
            get { return 42; }
        }

        /// <summary>
        /// Some method.
        /// </summary>
        /// <param name="parameter2">Not existing parameter.</param>
        public void MethodName(string parameter)
        {
        }

        /// <summary>
        /// Some method.
        /// </summary>
        /// <param name="intParameter">Should be second.</param>
        /// <param name="parameter">Should be first.</param>
        public void MethodName(string parameter, int intParameter)
        {
        }
    }
}
