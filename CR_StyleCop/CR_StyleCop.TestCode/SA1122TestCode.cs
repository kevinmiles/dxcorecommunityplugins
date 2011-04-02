namespace CR_StyleCop.TestCode
{
    using System;
    using System.ComponentModel;

#pragma warning disable 414

    /// <summary>
    /// Test code for SA1122 rule - string.Empty should be used for empty strings.
    /// </summary>
    public class SA1122TestCode
    {
        [Category("")]
        private string s1 = "";

        [Category(@"")]
        private string s2 = @"";

        private string s3 = string.Empty;
    }
}
