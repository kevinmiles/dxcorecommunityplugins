﻿namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1617 rule - void return value must not be documented.
    /// </summary>
    public class SA1617TestCode
    {
        /// <summary>
        /// Some delegate.
        /// </summary>
        /// <param name="parameter">Some parameter.</param>
        /// <returns></returns>
        public delegate void MyDelegate(object parameter);

        /// <summary>
        /// Some method.
        /// </summary>
        /// <param name="parameter">Some parameter.</param>
        /// <returns></returns>
        public void MethodName(string parameter)
        {
        }
    }
}
