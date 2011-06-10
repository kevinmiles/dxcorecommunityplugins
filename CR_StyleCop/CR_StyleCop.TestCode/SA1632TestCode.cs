// <copyright file="SA1632TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>
// <summary>Summary for the file</summary>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

#pragma warning disable 67

    /// <summary>
    /// Test code for SA1632 rule - documentation text cannot be too short.
    /// </summary>
    /// <typeparam name="T">F b.</typeparam>
    /// <exception cref="NullReferenceException">E c.</exception>
    /// <permission cref="System.Security.PermissionSet">D p.</permission>
    /// <remarks>R c.</remarks>
    /// <example>
    /// E e.
    /// </example>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1642:ConstructorSummaryDocumentationMustBeginWithStandardText", Justification = "This is about SA1632 rule.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1643:DestructorSummaryDocumentationMustBeginWithStandardText", Justification = "This is about SA1632 rule.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "This is about SA1632 rule.")]
    public class SA1632TestCode<T>
    {
        /// <summary>
        /// S t.
        /// </summary>
        /// <permission cref="System.Security.PermissionSet">D p.</permission>
        /// <remarks>R c.</remarks>
        /// <example>
        /// E e.
        /// </example>
        public const int InvalidField = 14;

        /// <summary>
        /// I d.
        /// </summary>
        /// <param name="parameter">P i.</param>
        /// <exception cref="NullReferenceException">E c.</exception>
        /// <permission cref="System.Security.PermissionSet">D p.</permission>
        /// <remarks>R c.</remarks>
        /// <example>
        /// E e.
        /// </example>
        public SA1632TestCode(int parameter)
        {
        }

        /// <summary>
        /// F c.
        /// </summary>
        /// <remarks>R d.</remarks>
        ~SA1632TestCode()
        {
        }

        /// <summary>
        /// S d.
        /// </summary>
        /// <typeparam name="TD">G p.</typeparam>
        /// <param name="ea">P i.</param>
        /// <returns>R v.</returns>
        /// <exception cref="NullReferenceException">E c.</exception>
        /// <permission cref="System.Security.PermissionSet">D p.</permission>
        /// <remarks>R c.</remarks>
        /// <example>
        /// E e.
        /// </example>
        public delegate int MyEventHandler<TD>(EventArgs ea);

        /// <summary>
        /// E s.
        /// </summary>
        /// <permission cref="System.Security.PermissionSet">D p.</permission>
        /// <remarks>R c.</remarks>
        /// <example>
        /// E e.
        /// </example>
        public event EventHandler EventName;

        /// <summary>
        /// S i.
        /// </summary>
        /// <typeparam name="TI">G p.</typeparam>
        /// <exception cref="NullReferenceException">E c.</exception>
        /// <permission cref="System.Security.PermissionSet">D p.</permission>
        /// <remarks>R i.</remarks>
        /// <example>
        /// E e.
        /// </example>
        public interface IMyInterface<TI>
        {
        }

        /// <summary>
        /// P s.
        /// </summary>
        /// <value>V v.</value>
        /// <exception cref="NullReferenceException">E c.</exception>
        /// <permission cref="System.Security.PermissionSet">D p.</permission>
        /// <remarks>R p.</remarks>
        /// <example>
        /// E e.
        /// </example>
        public int Property { get; set; }

        /// <summary>
        /// S i.
        /// </summary>
        /// <param name="index">I p.</param>
        /// <returns>R v.</returns>
        /// <exception cref="NullReferenceException">E c.</exception>
        /// <permission cref="System.Security.PermissionSet">D p.</permission>
        /// <remarks>R i.</remarks>
        /// <example>
        /// E e.
        /// </example>
        public int this[int index]
        {
            get { return 42; }
        }

        /// <summary>
        /// M s.
        /// </summary>
        /// <typeparam name="TT">G p.</typeparam>
        /// <param name="parameter">I p.</param>
        /// <returns>R v.</returns>
        /// <exception cref="NullReferenceException">E c.</exception>
        /// <permission cref="System.Security.PermissionSet">D p.</permission>
        /// <remarks>R m.</remarks>
        /// <example>
        /// E e.
        /// </example>
        public int Method<TT>(int parameter)
        {
            return parameter;
        }

        /// <summary>
        /// S s.
        /// </summary>
        /// <typeparam name="TS">G p.</typeparam>
        /// <exception cref="NullReferenceException">E c.</exception>
        /// <permission cref="System.Security.PermissionSet">D p.</permission>
        /// <remarks>R s.</remarks>
        /// <example>
        /// E e.
        /// </example>
        public struct MyStruct<TS>
        {
        }
    }
}