namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1622 rule - generic parameter documentation must have text.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SA1622TestCode<T>
    {
        /// <summary>
        /// Some method.
        /// </summary>
        /// <typeparam name="TM"></typeparam>
        public void MethodName<TM>()
        {
        }
    }
}
