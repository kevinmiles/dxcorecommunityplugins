//-----------------------------------------------------------------------
// <copyright file="SourceModelProxy.cs" company="Jim Counts">
//     Copyright (c) Jim Counts 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace CR_CreateContract
{
  using System;
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;
  using DevExpress.CodeRush.Core;
  using DevExpress.CodeRush.StructuralParser;

  /// <summary>
  /// Provides a wrapper around <see cref="SourceModelServices"/> which ensures certain 
  /// properties are not null using checks and contracts.
  /// </summary>
  public class SourceModelProxy : ProxyBase
  {
    /// <summary>
    /// The <see cref="ProjectElement"/> containing the active document.
    /// </summary>
    private readonly ProjectElement activeProject;

    /// <summary>
    /// The active source file
    /// </summary>
    private readonly SourceFile activeSourceFile;

    /// <summary>
    /// Initializes a new instance of the <see cref="SourceModelProxy"/> class.
    /// </summary>
    /// <param name="source">The source.</param>
    public SourceModelProxy(SourceModelServices source)
    {
      if (source == null)
      {
        throw new ArgumentNullException("source", "source is null.");
      }

      Contract.EndContractBlock();

      this.activeProject = GetNonNull(source.ActiveProject, "source.ActiveProject");
      this.activeSourceFile = GetNonNull(source.ActiveSourceFile, "source.ActiveSourceFile");
    }

    /// <summary>
    /// Gets the <see cref="ProjectElement"/> containing the active document.
    /// </summary>
    public ProjectElement ActiveProject
    {
      get
      {
        Contract.Ensures(Contract.Result<ProjectElement>() != null, "Result will not be null.");
        return this.activeProject;
      }
    }

    /// <summary>
    /// Gets the active source file.
    /// </summary>
    public SourceFile ActiveSourceFile
    {
      get
      {
        Contract.Ensures(Contract.Result<SourceFile>() != null, "Result will not be null.");
        return this.activeSourceFile;
      }
    }

    /// <summary>
    /// Container for invariant contracts.
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Used by CodeContracts runtime.")]
    [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "False positive.")]
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.activeProject != null, "this.activeProject must not be null.");
      Contract.Invariant(this.activeSourceFile != null, "this.activeSourceFile must not be null.");
    }
  }
}