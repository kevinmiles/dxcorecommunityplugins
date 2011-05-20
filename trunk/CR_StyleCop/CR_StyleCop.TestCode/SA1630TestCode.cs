// <copyright file="SA1630TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Test_code_for_SA1630_rule_-_documentation_text_must_contain_space.
    /// </summary>
    /// <typeparam name="T">Description_of_type.</typeparam>
    /// <exception cref="NullReferenceException">Exception_condition.</exception>
    /// <permission cref="">Description_of_permission.</permission>
    /// <remarks>Some_remarks_to_class.</remarks>
    /// <example>
    /// <code>
    /// namespace xxx
    /// {
    /// }
    /// </code>
    /// </example>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1642:ConstructorSummaryDocumentationMustBeginWithStandardText", Justification = "This is about SA1630 rule.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1643:DestructorSummaryDocumentationMustBeginWithStandardText", Justification = "This is about SA1630 rule.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "This is about SA1630 rule.")]
    public class SA1630TestCode<T>
    {
        /// <summary>
        /// No_capital_letter.
        /// </summary>
        /// <permission cref="">Description_of_permission.</permission>
        /// <remarks>Some_remarks_to_class.</remarks>
        /// <example>
        /// <code>
        /// namespace xxx
        /// {
        /// }
        /// </code>
        /// </example>
        public const int InvalidField = 14;

        /// <summary>
        /// Initializes_a_new_instance_of_the_<see cref="SA1630TestCode"/>_class.
        /// </summary>
        /// <param name="parameter">Input_parameter_for_method.</param>
        /// <exception cref="NullReferenceException">Exception_condition.</exception>
        /// <permission cref="">Description_of_permission.</permission>
        /// <remarks>Some_remarks_to_class.</remarks>
        /// <example>
        /// <code>
        /// namespace xxx
        /// {
        /// }
        /// </code>
        /// </example>
        public SA1630TestCode(int parameter)
        {
        }

        /// <summary>
        /// Finalizes_an_instance_of_the_<see cref="SA1630TestCode"/>_class.
        /// </summary>
        /// <remarks>Some_remarks_to_destructor.</remarks>
        ~SA1630TestCode()
        {
        }

        /// <summary>
        /// Summary_for_delegate.
        /// </summary>
        /// <typeparam name="TD">Generic_parameter_for_delegate.</typeparam>
        /// <param name="ea">Argument_for_delegate.</param>
        /// <returns>Something_not_useful_at_all.</returns>
        /// <exception cref="NullReferenceException">Exception_condition.</exception>
        /// <permission cref="">Description_of_permission.</permission>
        /// <remarks>Some_remarks_to_class.</remarks>
        /// <example>
        /// <code>
        /// namespace xxx
        /// {
        /// }
        /// </code>
        /// </example>
        public delegate int MyEventHandler<TD>(EventArgs ea);

        /// <summary>
        /// This_event_is_never_fired.
        /// </summary>
        /// <permission cref="">Description_of_permission.</permission>
        /// <remarks>Some_remarks_to_class.</remarks>
        /// <example>
        /// <code>
        /// namespace xxx
        /// {
        /// }
        /// </code>
        /// </example>
        public event EventHandler EventName;

        /// <summary>
        /// Summary_for_interface.
        /// </summary>
        /// <typeparam name="TI">Generic_parameter_for_interface.</typeparam>
        /// <exception cref="NullReferenceException">Exception_condition.</exception>
        /// <permission cref="">Description_of_permission.</permission>
        /// <remarks>Some_remarks_to_interface.</remarks>
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
        /// Gets_or_sets_some_integer.
        /// </summary>
        /// <value>The_integer.</value>
        /// <exception cref="NullReferenceException">Exception_condition.</exception>
        /// <permission cref="">Description_of_permission.</permission>
        /// <remarks>Some_remarks_to_class.</remarks>
        /// <example>
        /// <code>
        /// namespace xxx
        /// {
        /// }
        /// </code>
        /// </example>
        public int Property { get; set; }

        /// <summary>
        /// Summary_for_indexer.
        /// </summary>
        /// <param name="index">Index_parameter_is_ignored.</param>
        /// <returns>Magic_number_of_42.</returns>
        /// <exception cref="NullReferenceException">Exception_condition.</exception>
        /// <permission cref="">Description_of_permission.</permission>
        /// <remarks>Some_remarks_to_class.</remarks>
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
        /// Description_of_method.
        /// </summary>
        /// <typeparam name="TT">Generic_parameter_for_method.</typeparam>
        /// <param name="parameter">Input_parameter_for_method.</param>
        /// <returns>Something_not_useful_at_all.</returns>
        /// <exception cref="NullReferenceException">Exception_condition.</exception>
        /// <permission cref="">Description_of_permission.</permission>
        /// <remarks>Some_remarks_to_class.</remarks>
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
        /// Summary_for_struct.
        /// </summary>
        /// <typeparam name="TS">Generic_parameter_for_struct.</typeparam>
        /// <exception cref="NullReferenceException">Exception_condition.</exception>
        /// <permission cref="">Description_of_permission.</permission>
        /// <remarks>Some_remarks_to_struct.</remarks>
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