namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Test code for SA1405 rule - Debug.Assert must provide message.
    /// </summary>
    public class SA1405TestCode
    {
        private const string MessageConstant = "";
        private const string MessageConstant2 = null;

        private void MethodName(bool val)
        {
            Debug.Assert(val != true, "");
            Debug.Assert(val != true, @"");
            Debug.Assert(val != true, String.Empty);
            Debug.Assert(val != true, string.Empty);
            Debug.Assert(val != true, null);
            Debug.Assert(val != true);
            string message = null;
            Debug.Assert(val != true, message);
            Debug.Assert(val != true, MessageConstant);
            Debug.Assert(val != true, MessageConstant2);
        }
    }
}