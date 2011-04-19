namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1603 rule - xml doc comment must be valid xml.
    /// <summary>
    public class SA1603TestCode
    {
        /// <summary>
        /// Another invalid tag.
        /// </summa3ry>
        public int PropertyName { get; set; }

        /// <summary>
        /// Invalid character < inside comment.
        /// </summary>
        public int PropertyName2 { get; set; }
    }
}
