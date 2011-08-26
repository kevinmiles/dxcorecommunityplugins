//-----------------------------------------------------------------------
// <copyright file="LanguageElementExtensions.cs" company="Jim Counts">
//     Copyright (c) Jim Counts 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace CR_CreateContract
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using DevExpress.CodeRush.StructuralParser;

  /// <summary>
  /// Contains extension members to help the <see cref="ContractClassBuilder"/>.
  /// </summary>
  public static class LanguageElementExtensions
  {
    /// <summary>
    /// Clones the <see cref="XmlDocComment"/> comment from the interface class to the contract class.
    /// </summary>
    /// <param name="contractClass">The contract class.</param>
    /// <param name="interfaceMember">The interface member.</param>
    public static void CloneDocComment(this Class contractClass, Member interfaceMember)
    {
      if (contractClass == null)
      {
        throw new ArgumentNullException("contractClass", "contractClass is null.");
      }

      if (interfaceMember == null)
      {
        throw new ArgumentNullException("interfaceMember", "interfaceMember is null.");
      }

      Contract.EndContractBlock();
      if (interfaceMember.DocComment != null)
      {
        contractClass.AddNode(interfaceMember.DocComment.Clone() as XmlDocComment);
      }
    }

    /// <summary>
    /// Adds the TODO comment to an element.
    /// </summary>
    /// <param name="element">The element.</param>
    public static void AddToDoComment(this LanguageElement element)
    {
      if (element == null)
      {
        throw new ArgumentNullException("element", "element is null.");
      }

      Contract.EndContractBlock();
      var todo = new Comment()
      {
        CommentType = CommentType.SingleLine,
        Name = " TODO: Add Contracts Here"
      };
      element.InsertNode(0, todo);
    }

    /// <summary>
    /// Adds the interface namespace references to the container referred to by
    /// <paramref name="cursor"/>.
    /// </summary>
    /// <param name="cursor">The cursor.</param>
    /// <param name="namespaceNodes">The namespace nodes.</param>
    /// <returns>A reference to the object on which the method was invoked.</returns>
    public static LanguageElement AddNamespaceReferences(
      this LanguageElement cursor,
      IEnumerable<NamespaceReference> namespaceNodes)
    {
      if (cursor == null)
      {
        throw new ArgumentNullException("cursor", "cursor is null.");
      }

      if (namespaceNodes == null)
      {
        throw new ArgumentNullException("namespaceNodes", "namespaceNodes is null.");
      }

      Contract.EndContractBlock();

      foreach (NamespaceReference ns in namespaceNodes)
      {
        Contract.Assert(cursor != null);
        if (ns == null)
        {
          continue;
        }

        if (ns.Comments != null)
        {
          ns.Comments.Clear();
        }

        cursor.AddNode(ns);
      }

      return cursor;
    }

    /// <summary>
    /// Gets a collection of namespace references from the element's 
    /// parent document and namespace.  
    /// </summary>
    /// <param name="source">The source.</param>
    /// <returns>A collection of namespace references from the element's 
    /// parent document and namespace.</returns>
    public static IEnumerable<NamespaceReference> AllNamespaceNodes(this LanguageElement source)
    {
      if (source == null)
      {
        throw new ArgumentNullException("source", "source is null.");
      }

      Contract.Ensures(Contract.Result<IEnumerable<NamespaceReference>>() != null, "Result will not be null.");

      var namespaceNodes = Enumerable.Empty<NamespaceReference>();
      if (source.InsideNamespace)
      {
        var parentNamespace = source.GetNamespace();
        if (parentNamespace != null)
        {
          namespaceNodes = parentNamespace.NamespaceReferences();
        }
      }

      var parentDocument = source.GetParentDocument();
      if (parentDocument != null)
      {
        namespaceNodes = parentDocument.NamespaceReferences().Concat(namespaceNodes);
      }

      return namespaceNodes;
    }

    /// <summary>
    /// Sorts the namespace references.
    /// </summary>
    /// <param name="namespaces">The namespaces to sort.</param>
    /// <returns>The sorted namespace references.</returns>
    public static IEnumerable<NamespaceReference> SortNamespaceReferences(IEnumerable<NamespaceReference> namespaces)
    {
      if (namespaces == null)
      {
        throw new ArgumentNullException("namespaces", "namespaces is null.");
      }

      Contract.Ensures(Contract.Result<IEnumerable<NamespaceReference>>() != null, "Result will not be null.");

      return from reference in namespaces
             where reference != null
             orderby reference.Name.StartsWith("System", StringComparison.CurrentCulture), reference.Name
             select reference;
    }

    /// <summary>
    /// Returns the collection of namespace references for this element.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <returns>The collection of namespace references for this element.</returns>
    public static IEnumerable<NamespaceReference> NamespaceReferences(this LanguageElement source)
    {
      if (source == null)
      {
        throw new ArgumentNullException("source", "source is null.");
      }

      Contract.Ensures(Contract.Result<IEnumerable<NamespaceReference>>() != null, "Result will not be null.");

      return source.Nodes != null ?
        source.Nodes.OfType<NamespaceReference>() :
        Enumerable.Empty<NamespaceReference>();
    }
  }
}