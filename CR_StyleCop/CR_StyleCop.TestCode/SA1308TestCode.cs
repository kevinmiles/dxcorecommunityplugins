namespace CR_StyleCop.TestCode
{
    using System;

#pragma warning disable 169

    /// <summary>
    /// Test code for SA1308 rule - fields cannot use m_ or s_ prefixes.
    /// </summary>
    public class SA1308TestCode
    {
        private static int s_field;
        private int m_field;
    }
}
