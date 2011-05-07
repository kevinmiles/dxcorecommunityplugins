namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1616 rule - returns tag must be filled with text.
    /// </summary>
    public class SA1616TestCode
    {
        /// <summary>
        /// Some delegate.
        /// </summary>
        /// <param name="parameter">Some parameter.</param>
        /// <returns></returns>
        public delegate int MyDelegate(object parameter);

        /// <summary>
        /// Some indexer.
        /// </summary>
        /// <param name="parameter">Some parameter.</param>
        /// <returns></returns>
        public int this[int parameter]
        {
            get { return 42; }
        }

        /// <summary>
        /// Some method.
        /// </summary>
        /// <param name="parameter">Some parameter.</param>
        /// <returns></returns>
        public int MethodName(string parameter)
        {
            return 42;
        }
    }
}
