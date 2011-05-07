namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1615 rule - return value must be documented.
    /// </summary>
    public class SA1615TestCode
    {
        /// <summary>
        /// Some delegate.
        /// </summary>
        /// <param name="parameter">Some parameter.</param>
        public delegate int MyDelegate(object parameter);

        /// <summary>
        /// Some indexer.
        /// </summary>
        /// <param name="parameter">Some parameter.</param>
        public int this[int parameter]
        {
            get { return 42; }
        }

        /// <summary>
        /// Some method.
        /// </summary>
        /// <param name="parameter">Some parameter.</param>
        public int MethodName(string parameter)
        {
            return 42;
        }
    }
}
