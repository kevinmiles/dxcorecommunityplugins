//-----------------------------------------------------------------------
// <copyright file="InterfaceUpdater.cs" company="Jim Counts">
//     Copyright (c) Jim Counts 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace CR_CreateContract
{
  using System;
  using System.Collections.Generic;
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;
  using System.Linq;
  using DevExpress.CodeRush.Core;
  using DevExpress.CodeRush.StructuralParser;

  /// <summary>
  /// Provides methods to link an interface to its contract class.
  /// </summary>
  public class InterfaceUpdater
  {
    /// <summary>
    /// string literal: ContractClass
    /// </summary>
    private const string ContractClassAttributeName = "ContractClass";

    /// <summary>
    /// string literal: System.Diagnostics.Contracts
    /// </summary>
    private const string ContractsNamespaceName = "System.Diagnostics.Contracts";
    
    /// <summary>
    /// The interface file node.
    /// </summary>
    private readonly SourceFile fileNode;

    /// <summary>
    /// The interface file path.
    /// </summary>
    private readonly string filePath;

    /// <summary>
    /// Name of the interface.
    /// </summary>
    private readonly string interfaceName;

    /// <summary>
    /// The interface namespace
    /// </summary>
    private readonly Namespace interfaceNamespace;

    /// <summary>
    /// The source interface.
    /// </summary>
    private readonly Interface sourceInterface;

    /// <summary>
    /// The source text document.
    /// </summary>
    private readonly TextDocument textDocument;

    /// <summary>
    /// The sorted namespace references, including the reference to the Contracts namespace.
    /// </summary>
    private IEnumerable<NamespaceReference> sortedNamespaceReferences;

    /// <summary>
    /// Initializes a new instance of the <see cref="InterfaceUpdater"/> class.
    /// </summary>
    /// <param name="textDocument">The text document.</param>
    /// <param name="sourceInterface">The source interface.</param>
    /// <param name="sourceFile">The source file.</param>
    /// <param name="interfaceFilePath">The interface file path.</param>
    /// <param name="interfaceName">Name of the interface.</param>
    public InterfaceUpdater(TextDocument textDocument, Interface sourceInterface, SourceFile sourceFile, string interfaceFilePath, string interfaceName)
    {
      if (string.IsNullOrEmpty(interfaceName))
      {
        throw new ArgumentException("interfaceName is null or empty.", "interfaceName");
      }

      if (textDocument == null)
      {
        throw new ArgumentNullException("textDocument", "textDocument is null.");
      }

      if (sourceInterface == null)
      {
        throw new ArgumentNullException("sourceInterface", "sourceInterface is null.");
      }
      
      if (sourceFile == null)
      {
        throw new ArgumentNullException("sourceFile", "sourceFile is null.");
      }
      
      if (string.IsNullOrEmpty(interfaceFilePath))
      {
        throw new ArgumentException("interfaceFilePath is null or empty.", "interfaceFilePath");
      }
      
      Contract.EndContractBlock();

      this.sourceInterface = sourceInterface;
      this.fileNode = sourceFile;
      this.filePath = interfaceFilePath;
      this.interfaceName = interfaceName;
      this.textDocument = textDocument;
      this.interfaceNamespace = sourceInterface.GetNamespace();
      this.ContractsNamespaceReference = new NamespaceReference(ContractsNamespaceName);
    }

    /// <summary>
    /// Gets the interface source file path.
    /// </summary>
    public string FilePath
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null, "Result will not be null.");
        return this.filePath;
      }
    }

    /// <summary>
    /// Gets the interface file comments.
    /// </summary>
    public IEnumerable<Comment> InterfaceFileComments
    {
      get
      {
        Contract.Ensures(Contract.Result<IEnumerable<Comment>>() != null, "Result will not be null.");
        var allComments = this.fileNode.AllComments;
        return allComments != null ? allComments.Cast<Comment>() : Enumerable.Empty<Comment>();
      }
    }

    /// <summary>
    /// Gets the interface file node.
    /// </summary>
    public SourceFile InterfaceFileNode
    {
      get
      {
        Contract.Ensures(Contract.Result<SourceFile>() != null, "Result will not be null.");
        return this.fileNode;
      }
    }

    /// <summary>
    /// Gets the interface namespace.
    /// </summary>
    public Namespace InterfaceNamespace
    {
      get
      {
        return this.interfaceNamespace;
      }
    }

    /// <summary>
    /// Gets the namespace references.
    /// </summary>
    public IEnumerable<NamespaceReference> NamespaceReferences
    {
      get
      {
        if (this.sortedNamespaceReferences == null)
        {
          var all = this.SourceInterface.AllNamespaceNodes()
            .Concat(new NamespaceReference[] { this.ContractsNamespaceReference });
          this.sortedNamespaceReferences = LanguageElementExtensions.SortNamespaceReferences(all);
        }

        return this.sortedNamespaceReferences;
      }
    }

    /// <summary>
    /// Gets the source interface.
    /// </summary>
    public Interface SourceInterface
    {
      get
      {
        Contract.Ensures(Contract.Result<Interface>() != null, "Result will not be null.");
        return this.sourceInterface;
      }
    }

    /// <summary>
    /// Gets the source interface text document.
    /// </summary>
    public TextDocument TextDocument
    {
      get
      {
        return this.textDocument;
      }
    }

    /// <summary>
    /// Gets or sets the contracts namespace reference.
    /// </summary>
    /// <value>
    /// The contracts namespace reference.
    /// </value>
    public NamespaceReference ContractsNamespaceReference { get; set; }

    /// <summary>
    /// Gets the name of the interface.
    /// </summary>
    /// <value>
    /// The name of the interface.
    /// </value>
    public string InterfaceName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null, "Result will not be null.");
        Contract.Ensures(Contract.Result<string>().Length > 0);
        return this.interfaceName;
      }
    }

    #region Public Methods

    /// <summary>
    /// Gets the code for interface attribute.
    /// </summary>
    /// <returns>
    /// The code for the interface attribute.
    /// </returns>
    public string CreateCodeForInterfaceAttribute()
    {
      var eb = new ElementBuilder();
      var attributeSection = eb.AddAttributeSection(null);
      var attribute = eb.AddAttribute(attributeSection, ContractClassAttributeName);
      var contractClassTypeReference = new TypeReferenceExpression(this.CreateContractClassName());
      eb.AddArgument(attribute, new TypeOfExpression(contractClassTypeReference));
      return eb.GenerateCode(this.TextDocument.Language);
    }

    /// <summary>
    /// Gets the code for namespace reference.
    /// </summary>
    /// <returns>
    /// The code for namespace reference.
    /// </returns>
    public string CreateCodeForNamespaceReference()
    {      
      var elementBuilder = new ElementBuilder();
      elementBuilder.AddNode(null, this.ContractsNamespaceReference);
      return elementBuilder.GenerateCode(this.TextDocument.Language);
    }

    /// <summary>
    /// Gets the name of the contract class.
    /// </summary>
    /// <returns>The name of the contract class.</returns>
    public string CreateContractClassName()
    {
      Contract.Ensures(Contract.Result<string>() != null, "Result will not be null.");
      return this.InterfaceName.TrimStart('I') + "Contract";
    }

    /// <summary>
    /// Gets the namespace reference target.
    /// </summary>
    /// <returns>The <see cref="SourcePoint"/> where the Contracts namespace 
    /// reference should be placed.</returns>
    public SourcePoint FindNamespaceReferenceTarget()
    {
      var referenceList = new LinkedList<NamespaceReference>(this.NamespaceReferences);
      var contractsReference = referenceList.Find(this.ContractsNamespaceReference);
      if (1 < referenceList.Count && contractsReference != null)
      {
        if (contractsReference.Next != null && contractsReference.Next.Value != null)
        {
          return contractsReference.Next.Value.Range.Start;
        }

        if (contractsReference.Previous != null && contractsReference.Previous.Value != null)
        {
          return contractsReference.Previous.Value.Range.End;
        }
      }

      return new SourcePoint(1, 1);
    }

    /// <summary>
    /// Updates the interface.
    /// </summary>
    /// <remarks>
    /// Adds a namespace reference to the interface text document.
    /// Adds an attribute which links the interface to the contract class.
    /// </remarks>
    public void UpdateInterface()
    {
      this.ApplyLinkingAttribute();
      this.ApplyNamespaceReference();
      this.TextDocument.Format();
    }
    #endregion

    /// <summary>
    /// Applies the attribute which links the interface to it's contract class.
    /// </summary>
    private void ApplyLinkingAttribute()
    {
      var insertedText = this.TextDocument.InsertText(
        this.SourceInterface.Range.Top,
        this.CreateCodeForInterfaceAttribute());
      this.TextDocument.Format(insertedText);
    }

    /// <summary>
    /// Applies the namespace reference.
    /// </summary>
    private void ApplyNamespaceReference()
    {
      var insertedText = this.TextDocument.InsertText(
        this.FindNamespaceReferenceTarget(),
        this.CreateCodeForNamespaceReference());
      this.TextDocument.Format(insertedText);
    }

    /// <summary>
    /// Container for invariant contracts.
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "False positive.")]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Called by Code Contracts tools.")]
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.filePath != null, "this.filePath must not be null.");
      Contract.Invariant(this.fileNode != null, "this.fileNode must not be null.");
      Contract.Invariant(this.sourceInterface != null, "this.sourceInterface must not be null.");
      Contract.Invariant(this.textDocument != null, "this.textDocument must not be null.");
      Contract.Invariant(!string.IsNullOrEmpty(this.interfaceName), "this.interfaceName must not be null.");
      Contract.Invariant(this.ContractsNamespaceReference != null, "this.ContractsNamespaceReference must not be null.");
    }
  }
}