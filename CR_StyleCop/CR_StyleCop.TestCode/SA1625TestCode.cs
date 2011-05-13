// <copyright file="SA1625TestCode.cs" company="ACME">
//     Copyright (c) 2011. All rights reserved.
// </copyright>

namespace CR_StyleCop.TestCode
{
    using System;

    /// <summary>
    /// Test code for SA1625 rule - documentation text must not be the same for two tags.
    /// </summary>
    /// <typeparam name="T">Test code for SA1625 rule - documentation text must not be the same for two tags.</typeparam>
    public class SA1625TestCode<T>
    {
        // BUGBUG: SA1625 not reported for fields.

        /// <summary>
        /// Duplicated text.
        /// </summary>
        /// <remarks>Duplicated text.</remarks>
        public const string Constant = "Constant";

        /// <summary>
        /// Duplicated text.
        /// </summary>
        /// <remarks>Duplicated text.</remarks>
        private int anything;

        /// <summary>
        /// Initializes a new instance of the <see cref="SA1625TestCode{T}"/> class.
        /// </summary>
        /// <param name="anything">Duplicated text.</param>
        /// <param name="something">Duplicated text.</param>
        public SA1625TestCode(int anything, int something)
        {
            this.anything = anything;
            this.Something = something;
        }

        // BUGBUG: SA1625 not reported for destructors.
        
        /// <summary>
        /// Finalizes an instance of the <see cref="SA1625TestCode{T}"/> class.
        /// </summary>
        /// <remarks>Finalizes an instance of the <see cref="SA1625TestCode{T}"/> class.</remarks>
        ~SA1625TestCode()
        {
        }

        /// <summary>
        /// Duplicated text.
        /// </summary>
        /// <param name="sender">Duplicated text.</param>
        /// <param name="ea">Duplicated text.</param>
        public delegate void MyDelegate(object sender, EventArgs ea);

        // BUGBUG: SA1625 not reported for events.
        
        /// <summary>
        /// Duplicated text.
        /// </summary>
        /// <remarks>Duplicated text.</remarks>
        public event EventHandler Event;

        /// <summary>
        /// Duplicated text.
        /// </summary>
        /// <typeparam name="TI">Duplicated text.</typeparam>
        public interface IInnerInterface<TI>
        {
        }

        // BUGBUG: SA1625 not reported for properties.

        /// <summary>
        /// Gets or sets something.
        /// </summary>
        /// <remarks>Gets or sets something.</remarks>
        /// <value>Gets or sets something.</value>
        public int Something { get; set; }

        /// <summary>
        /// Gets or sets anything.
        /// </summary>
        /// <remarks>Gets or sets anything.</remarks>
        /// <value>Gets or sets anything.</value>
        public int Anything
        {
            get { return this.anything; }
            set { this.anything = value; }
        }

        /// <summary>
        /// Duplicated text.
        /// </summary>
        /// <param name="index">Duplicated text.</param>
        /// <returns>Duplicated text.</returns>
        public int this[int index]
        {
            get { return 42; }
            set { }
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="firstName">Part of the name.</param>
        /// <param name="lastName">Part of the name.</param>
        /// <returns>The joined names.</returns>
        public string JoinNames(string firstName, string lastName)
        {
            return firstName + " " + lastName;
        }

        /// <summary>
        /// Joins a first name and a last name together into a single string.
        /// </summary>
        /// <param name="firstName">The parameter is not used.</param>
        /// <param name="lastName">The parameter is not used.</param>
        /// <returns>The joined names.</returns>
        public string JoinNames(string firstName, string lastName)
        {
            return "Something";
        }

        /// <summary>
        /// Duplicated text.
        /// </summary>
        /// <param name="firstName">Duplicated text.</param>
        /// <param name="lastName">Part of the name.</param>
        /// <returns>The joined names.</returns>
        public string JoinNames(string firstName, string lastName)
        {
            return firstName + " " + lastName;
        }

        /// <summary>
        /// Duplicated text.
        /// </summary>
        /// <typeparam name="TS">Duplicated text.</typeparam>
        public struct InnerStruct<TS>
        {
        }
    }
}
