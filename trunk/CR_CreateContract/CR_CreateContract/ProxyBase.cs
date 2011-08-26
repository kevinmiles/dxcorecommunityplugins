//-----------------------------------------------------------------------
// <copyright file="ProxyBase.cs" company="Jim Counts">
//     Copyright (c) Jim Counts 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace CR_CreateContract
{
  using System;
  using System.Diagnostics.Contracts;
  using System.Globalization;

  /// <summary>
  /// Provides common methods used to verify properties and raise exceptions.
  /// </summary>
  public abstract class ProxyBase
  {
    /// <summary>
    /// string literal: "Could not proceed because [{0}] was null."
    /// </summary>
    protected const string NullCodeRushProperty = "Could not proceed because [{0}] was null.";

    /// <summary>
    /// Gets the null code rush property message.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns>The null code rush property message.</returns>
    protected static string GetNullCodeRushPropertyMessage(string propertyName)
    {
      Contract.Requires(!string.IsNullOrEmpty(propertyName), "propertyName must not be null or empty.");
      return string.Format(CultureInfo.CurrentCulture, NullCodeRushProperty, propertyName);
    }

    /// <summary>
    /// Gets the verified property.
    /// </summary>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <param name="property">The property to verify.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns>The property, if not null; otherwise an exception is thrown.</returns>
    /// <exception cref="InvalidOperationExeption">This exception is thrown if the property is null.
    /// The message will explain which property was null.</exception>
    protected static T GetNonNull<T>(T property, string propertyName)
    {
      Contract.Requires(!string.IsNullOrEmpty(propertyName), "propertyName must not be null or empty.");
      Contract.Ensures(Contract.Result<T>() != null, "Result will not be null.");
      if (property == null)
      {
        throw new InvalidOperationException(GetNullCodeRushPropertyMessage(propertyName));
      }

      return property;
    }
  }
}