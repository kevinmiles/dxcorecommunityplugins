namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Test code for SA1406 rule - Debug.Fail must provide message.
    /// </summary>
    public class SA1406TestCode
    {
        private const string MessageConstant = "";
        private const string MessageConstant2 = null;

        private void MethodName(bool val)
        {
            Debug.Fail(@"");
            Debug.Fail("");
            Debug.Fail(String.Empty);
            Debug.Fail(string.Empty);
            Debug.Fail(null);
            string message = null;
            Debug.Fail(message);
            Debug.Fail(MessageConstant);
            Debug.Fail(MessageConstant2);
        }
    }
}