namespace CR_StyleCop.TestCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Test code for SA1201 rule - elements must be ordered correctly.
    /// </summary>
    public class SA1201TestCode
    {
    }

    /// <summary>
    /// Test code for SA1201 rule - elements must be ordered correctly.
    /// </summary>
    public struct MyStruct
    {
    }

    /// <summary>
    /// Test code for SA1201 rule - elements must be ordered correctly.
    /// </summary>
    public struct MyStruct2
    {
    }

    /// <summary>
    /// Test code for SA1201 rule - elements must be ordered correctly.
    /// </summary>
    public interface IMyInterface
    {
    }

    /// <summary>
    /// Test code for SA1201 rule - elements must be ordered correctly.
    /// </summary>
    public enum MyEnum
    {
        /// <summary>
        /// Test code for SA1201 rule - elements must be ordered correctly.
        /// </summary>
        FirstElement,
    }

    /// <summary>
    /// Test code for SA1201 rule - elements must be ordered correctly.
    /// </summary>
    /// <param name="sender">Sender parameter.</param>
    /// <param name="ea">Event args parameter.</param>
    public delegate void MyEventHandler(object sender, EventArgs ea);

    namespace Inner
    {
    }
}
