//-----------------------------------------------------------------------
// <copyright file="MethodExtensions.cs" company="Jim Counts">
//     Copyright (c) Jim Counts 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace CR_CreateContract
{
  using System;
  using System.Diagnostics.Contracts;
  using DevExpress.CodeRush.StructuralParser;

  /// <summary>
  /// Provides helper methods that act on Method language elements.
  /// </summary>
  public static class MethodExtensions
  {
    /// <summary>
    /// Copies the parameters from one method to another.
    /// </summary>
    /// <param name="method">The method.</param>
    /// <param name="other">The other method.</param>
    public static void CopyParameters(this Method method, Method other)
    {
      if (method == null)
      {
        throw new ArgumentNullException("method", "method is null.");
      }

      if (other == null)
      {
        throw new ArgumentNullException("other", "other is null.");
      }

      Contract.EndContractBlock();
      if (other.ParameterCount > 0)
      {
        method.AddParameters(other.Parameters);
      }
    }
  }
}