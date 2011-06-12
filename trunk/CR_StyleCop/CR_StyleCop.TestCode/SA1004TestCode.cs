// <copyright file="SA1004TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    ///  <summary>
    ///  Test code for SA1004 - documentation line must start with single space.
    ///  </summary>
    ///  <typeparam name="T">Description of type.</typeparam>
    ///  <exception cref="NullReferenceException">Exception condition.</exception>
    ///  <permission cref="System.Security.PermissionSet">Description of permission.</permission>
    ///  <remarks>Some remarks to class.</remarks>
    ///  <example>
    ///  <code>
    ///  namespace xxx
    ///  {
    ///  }
    ///  </code>
    ///  </example>
    public class SA1004TestCode<T>
    {
        ///  <summary>
        ///  No capital letter.
        ///  </summary>
        ///  <permission cref="System.Security.PermissionSet">Description of permission.</permission>
        ///  <remarks>Some remarks to field.</remarks>
        ///  <example>
        ///  <code>
        ///  namespace xxx
        ///  {
        ///  }
        ///  </code>
        ///  </example>
        public const int InvalidField = 14;

        ///  <summary>
        ///  Initializes a new instance of the SA1004TestCode class.
        ///  </summary>
        ///  <param name="parameter">Input parameter for method.</param>
        ///  <exception cref="NullReferenceException">Exception condition.</exception>
        ///  <permission cref="System.Security.PermissionSet">Description of permission.</permission>
        ///  <remarks>Some remarks to class.</remarks>
        ///  <example>
        ///  <code>
        ///  namespace xxx
        ///  {
        ///  }
        ///  </code>
        ///  </example>
        public SA1004TestCode(int parameter)
        {
        }

        ///  <summary>
        ///  Finalizes an instance of the SA1004TestCode class.
        ///  </summary>
        ///  <remarks>Some remarks to destructor.</remarks>
        ~SA1004TestCode()
        {
        }

        ///  <summary>
        ///  Summary for delegate.
        ///  </summary>
        ///  <typeparam name="TD">Generic parameter for delegate.</typeparam>
        ///  <param name="ea">Argument for delegate.</param>
        ///  <returns>Something not useful at all.</returns>
        ///  <exception cref="NullReferenceException">Exception condition.</exception>
        ///  <permission cref="System.Security.PermissionSet">Description of permission.</permission>
        ///  <remarks>Some remarks to class.</remarks>
        ///  <example>
        ///  <code>
        ///  namespace xxx
        ///  {
        ///  }
        ///  </code>
        ///  </example>
        public delegate int MyEventHandler<TD>(EventArgs ea);

        ///  <summary>
        ///  This event is never fired.
        ///  </summary>
        ///  <permission cref="System.Security.PermissionSet">Description of permission.</permission>
        ///  <remarks>Some remarks to class.</remarks>
        ///  <example>
        ///  <code>
        ///  namespace xxx
        ///  {
        ///  }
        ///  </code>
        ///  </example>
        public event EventHandler EventName;

        ///  <summary>
        ///  Summary for interface.
        ///  </summary>
        ///  <typeparam name="TI">Generic parameter for interface.</typeparam>
        ///  <exception cref="NullReferenceException">Exception condition.</exception>
        ///  <permission cref="System.Security.PermissionSet">Description of permission.</permission>
        ///  <remarks>Some remarks to interface.</remarks>
        ///  <example>
        ///  <code>
        ///  namespace xxx
        ///  {
        ///  }
        ///  </code>
        ///  </example>
        public interface IMyInterface<TI>
        {
        }

        ///  <summary>
        ///  Gets or sets some integer.
        ///  </summary>
        ///  <value>The integer.</value>
        ///  <exception cref="NullReferenceException">Exception condition.</exception>
        ///  <permission cref="System.Security.PermissionSet">Description of permission.</permission>
        ///  <remarks>Some remarks to property.</remarks>
        ///  <example>
        ///  <code>
        ///  namespace xxx
        ///  {
        ///  }
        ///  </code>
        ///  </example>
        public int Property { get; set; }

        ///  <summary>
        ///  Summary for indexer.
        ///  </summary>
        ///  <param name="index">Index parameter is ignored.</param>
        ///  <returns>Magic number of 42.</returns>
        ///  <exception cref="NullReferenceException">Exception condition.</exception>
        ///  <permission cref="System.Security.PermissionSet">Description of permission.</permission>
        ///  <remarks>Some remarks to indexer.</remarks>
        ///  <example>
        ///  <code>
        ///  namespace xxx
        ///  {
        ///  }
        ///  </code>
        ///  </example>
        public int this[int index]
        {
            get { return 42; }
        }

        ///  <summary>
        ///  Description of method.
        ///  </summary>
        ///  <typeparam name="TT">Generic parameter for method.</typeparam>
        ///  <param name="parameter">Input parameter for method.</param>
        ///  <returns>Something not useful at all.</returns>
        ///  <exception cref="NullReferenceException">Exception condition.</exception>
        ///  <permission cref="System.Security.PermissionSet">Description of permission.</permission>
        ///  <remarks>Some remarks to method.</remarks>
        ///  <example>
        ///  <code>
        ///  namespace xxx
        ///  {
        ///  }
        ///  </code>
        ///  </example>
        public int Method<TT>(int parameter)
        {
            return parameter;
        }

        ///  <summary>
        ///  Summary for struct.
        ///  </summary>
        ///  <typeparam name="TS">Generic parameter for struct.</typeparam>
        ///  <exception cref="NullReferenceException">Exception condition.</exception>
        ///  <permission cref="System.Security.PermissionSet">Description of permission.</permission>
        ///  <remarks>Some remarks to struct.</remarks>
        ///  <example>
        ///  <code>
        ///  namespace xxx
        ///  {
        ///  }
        ///  </code>
        ///  </example>
        public struct MyStruct<TS>
        {
        }
    }
}
