// <copyright file="SA1628TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

#pragma warning disable 67

    /// <summary>
    /// test code for SA1628 rule - documentation text must start with capital letter.
    /// </summary>
    /// <typeparam name="T">description of type.</typeparam>
    /// <exception cref="NullReferenceException">exception condition.</exception>
    /// <permission cref="System.Security.PermissionSet">description of permission.</permission>
    /// <remarks>some remarks to class.</remarks>
    /// <example>
    /// <code>
    /// namespace xxx
    /// {
    /// }
    /// </code>
    /// </example>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1642:ConstructorSummaryDocumentationMustBeginWithStandardText", Justification = "This is about SA1628 rule.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1643:DestructorSummaryDocumentationMustBeginWithStandardText", Justification = "This is about SA1628 rule.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "This is about SA1628 rule.")]
    public class SA1628TestCode<T>
    {
        /// <summary>
        /// no capital letter.
        /// </summary>
        /// <permission cref="System.Security.PermissionSet">description of permission.</permission>
        /// <remarks>some remarks to class.</remarks>
        /// <example>
        /// <code>
        /// namespace xxx
        /// {
        /// }
        /// </code>
        /// </example>
        public const int InvalidField = 14;

        /// <summary>
        /// initializes a new instance of the <see cref="T:SA1628TestCode"/> class.
        /// </summary>
        /// <param name="parameter">input parameter for method.</param>
        /// <exception cref="NullReferenceException">exception condition.</exception>
        /// <permission cref="System.Security.PermissionSet">description of permission.</permission>
        /// <remarks>some remarks to class.</remarks>
        /// <example>
        /// <code>
        /// namespace xxx
        /// {
        /// }
        /// </code>
        /// </example>
        public SA1628TestCode(int parameter)
        {
        }

        /// <summary>
        /// finalizes an instance of the <see cref="T:SA1628TestCode"/> class.
        /// </summary>
        /// <remarks>some remarks to destructor.</remarks>
        ~SA1628TestCode()
        {
        }

        /// <summary>
        /// summary for delegate.
        /// </summary>
        /// <typeparam name="TD">generic parameter for delegate.</typeparam>
        /// <param name="ea">argument for delegate.</param>
        /// <returns>something not useful at all.</returns>
        /// <exception cref="NullReferenceException">exception condition.</exception>
        /// <permission cref="System.Security.PermissionSet">description of permission.</permission>
        /// <remarks>some remarks to class.</remarks>
        /// <example>
        /// <code>
        /// namespace xxx
        /// {
        /// }
        /// </code>
        /// </example>
        public delegate int MyEventHandler<TD>(EventArgs ea);

        /// <summary>
        /// this event is never fired.
        /// </summary>
        /// <permission cref="System.Security.PermissionSet">description of permission.</permission>
        /// <remarks>some remarks to class.</remarks>
        /// <example>
        /// <code>
        /// namespace xxx
        /// {
        /// }
        /// </code>
        /// </example>
        public event EventHandler EventName;

        /// <summary>
        /// summary for interface.
        /// </summary>
        /// <typeparam name="TI">generic parameter for interface.</typeparam>
        /// <exception cref="NullReferenceException">exception condition.</exception>
        /// <permission cref="System.Security.PermissionSet">description of permission.</permission>
        /// <remarks>some remarks to interface.</remarks>
        /// <example>
        /// <code>
        /// namespace xxx
        /// {
        /// }
        /// </code>
        /// </example>
        public interface IMyInterface<TI>
        {
        }

        /// <summary>
        /// gets or sets some integer.
        /// </summary>
        /// <value>the integer.</value>
        /// <exception cref="NullReferenceException">exception condition.</exception>
        /// <permission cref="System.Security.PermissionSet">description of permission.</permission>
        /// <remarks>some remarks to class.</remarks>
        /// <example>
        /// <code>
        /// namespace xxx
        /// {
        /// }
        /// </code>
        /// </example>
        public int Property { get; set; }

        /// <summary>
        /// summary for indexer.
        /// </summary>
        /// <param name="index">index parameter is ignored.</param>
        /// <returns>magic number of 42.</returns>
        /// <exception cref="NullReferenceException">exception condition.</exception>
        /// <permission cref="System.Security.PermissionSet">description of permission.</permission>
        /// <remarks>some remarks to class.</remarks>
        /// <example>
        /// <code>
        /// namespace xxx
        /// {
        /// }
        /// </code>
        /// </example>
        public int this[int index]
        {
            get { return 42; }
        }

        /// <summary>
        /// description of method.
        /// </summary>
        /// <typeparam name="TT">generic parameter for method.</typeparam>
        /// <param name="parameter">input parameter for method.</param>
        /// <returns>something not useful at all.</returns>
        /// <exception cref="NullReferenceException">exception condition.</exception>
        /// <permission cref="System.Security.PermissionSet">description of permission.</permission>
        /// <remarks>some remarks to class.</remarks>
        /// <example>
        /// <code>
        /// namespace xxx
        /// {
        /// }
        /// </code>
        /// </example>
        public int Method<TT>(int parameter)
        {
            return parameter;
        }

        /// <summary>
        /// summary for struct.
        /// </summary>
        /// <typeparam name="TS">generic parameter for struct.</typeparam>
        /// <exception cref="NullReferenceException">exception condition.</exception>
        /// <permission cref="System.Security.PermissionSet">description of permission.</permission>
        /// <remarks>some remarks to struct.</remarks>
        /// <example>
        /// <code>
        /// namespace xxx
        /// {
        /// }
        /// </code>
        /// </example>
        public struct MyStruct<TS>
        {
        }
    }
}