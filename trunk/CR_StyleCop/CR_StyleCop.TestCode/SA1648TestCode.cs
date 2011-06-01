// <copyright file="SA1648TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Linq;

    /// <summary>
    /// Test code for SA1648 rule - inheritdoc tag must be on element where doc can be inherited.
    /// </summary>
    public class SA1648TestCode : IMyInterface
    {
        /// <inheritdoc />
        public interface IMyInterface
        {
            /// <inheritdoc />
            void MethodName();
        }

        /// <inheritdoc />
        public void MethodName()
        {
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return base.ToString();
        }

        /// <inheritdoc />
        public void Invalid()
        {
            // this should fire SA1648, but it is outside of stylecop possibility to check this (don't know whether method is in some interface)
        }
    }
}
