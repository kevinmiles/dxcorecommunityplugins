//-----------------------------------------------------------------------
// <copyright file="ContractClassBuilder.cs" company="Jim Counts">
//     Copyright (c) Jim Counts 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace CR_CreateContract
{
  using System;
  using System.Diagnostics.CodeAnalysis;
  using System.Diagnostics.Contracts;
  using System.IO;
  using System.Text;
  using DevExpress.CodeRush.Core;
  using DevExpress.CodeRush.StructuralParser;
  using DevExpress.Refactor.Core;
  using CustomAttribute = DevExpress.CodeRush.StructuralParser.Attribute;

  /// <summary>
  /// Provides methods to create a ContractClass implementation for an interface.
  /// </summary>
  public class ContractClassBuilder
  {
    /// <summary>
    /// Provides verifiable access to certain CodeRush services.
    /// </summary>
    private readonly CodeRushProxy CodeRushProxy;

    /// <summary>
    /// string literal [ CodeContracts runtime will ignore this return value.]
    /// </summary>
    private const string RuntimeIgnored = " CodeContracts runtime will ignore this return value.";

    /// <summary>
    /// string literal: ContractClassFor
    /// </summary>
    private const string ContractClassFor = "ContractClassFor";

    /// <summary>
    /// Name of the contract class.
    /// </summary>
    private readonly string ContractClassName;

    /// <summary>
    /// Name of the interface.
    /// </summary>
    private readonly InterfaceUpdater interfaceUpdater;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContractClassBuilder"/> class.
    /// </summary>
    /// <param name="interfaceUpdater">The interface updater.</param>
    /// <param name="codeRushProxy">The code rush proxy.</param>
    public ContractClassBuilder(InterfaceUpdater interfaceUpdater, CodeRushProxy codeRushProxy)
    {
      if (interfaceUpdater == null)
      {
        throw new ArgumentNullException(message: "interfaceUpdater is null.", paramName: "interfaceUpdater");
      }

      if (codeRushProxy == null)
      {
        throw new ArgumentNullException("codeRushProxy", "codeRushProxy is null.");
      }

      Contract.EndContractBlock();

      this.ContractClassName = interfaceUpdater.CreateContractClassName();
      this.interfaceUpdater = interfaceUpdater;
      this.CodeRushProxy = codeRushProxy;
    }

    /// <summary>
    /// Gets the name of the interface.
    /// </summary>
    private string InterfaceName
    {
      get
      {
        Contract.Ensures(Contract.Result<string>().Length > 0);
        Contract.Ensures(Contract.Result<string>() != null, "Result will not be null.");
        return this.InterfaceUpdater.InterfaceName;
      }
    }

    /// <summary>
    /// Gets the interface namespace.
    /// </summary>
    private Namespace InterfaceNamespace
    {
      get
      {
        return this.InterfaceUpdater.InterfaceNamespace;
      }
    }

    /// <summary>
    /// Gets the source interface.
    /// </summary>
    private InterfaceUpdater InterfaceUpdater
    {
      get
      {
        Contract.Ensures(Contract.Result<InterfaceUpdater>() != null, "Result will not be null.");
        return this.interfaceUpdater;
      }
    }

    /// <summary>
    /// Gets the language.
    /// </summary>
    private string Language
    {
      get
      {
        Contract.Ensures(Contract.Result<string>() != null, "Result will not be null.");
        return this.InterfaceUpdater.TextDocument.Language ?? string.Empty;
      }
    }

    /// <summary>
    /// Creates the contract class file.
    /// </summary>
    public void CreateContractClassFile()
    {
      string filePath = this.GetContractFilePath();
      if (File.Exists(filePath))
      {
        return;
      }

      this.WriteContractFile(filePath);
      this.AddFileToSolution(filePath);
      this.CopyFileHeader(filePath);
    }

    /// <summary>
    /// Creates the setter.
    /// </summary>
    /// <param name="interfaceProperty">The interface property.</param>
    /// <returns>The setter; or null if the setter was not needed or could not be created.</returns>
    private static Set CreateSetter(Property interfaceProperty)
    {
      Contract.Requires(interfaceProperty != null, "interfaceProperty is null.");
      if (interfaceProperty.IsReadOnly)
      {
        return null;
      }

      Set setter = new Set();
      setter.AddToDoComment();

      return setter;
    }

    /// <summary>
    /// Adds the default return statement.
    /// </summary>
    /// <param name="contractElement">The contract element.</param>
    /// <param name="memberTypeReference">The member type reference.</param>
    private void AddDefaultReturn(LanguageElement contractElement, TypeReferenceExpression memberTypeReference)
    {
      Contract.Requires(contractElement != null, "contractElement is null.");
      Contract.Requires(memberTypeReference != null, "memberTypeReference is null.");

      var returnValue = !this.CodeRushProxy.Language.IsCSharp ?
        this.CodeRushProxy.Language.GetNullReferenceExpression() :
        new DefaultValueExpression(memberTypeReference);

      var methodReturn = new Return(returnValue);
      methodReturn.AddCommentNode(new Comment() { Name = RuntimeIgnored, CommentType = CommentType.SingleLine });
      contractElement.AddNode(methodReturn);
    }

    /// <summary>
    /// Creates the getter.
    /// </summary>
    /// <param name="interfaceProperty">The interface property.</param>
    /// <returns>The getter; or null if the getter was not needed, or could not be created.</returns>
    private Get CreateGetter(Property interfaceProperty)
    {
      Contract.Requires(interfaceProperty != null, "interfaceProperty is null.");
      if (interfaceProperty.IsWriteOnly)
      {
        return null;
      }

      var typeReferenceExp = interfaceProperty.MemberTypeReference;
      if (typeReferenceExp == null)
      {
        return null;
      }

      Get getter = new Get();
      getter.AddToDoComment();
      this.AddDefaultReturn(getter, typeReferenceExp);

      return getter;
    }
    
    /// <summary>
    /// Adds the file to solution.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    private void AddFileToSolution(string filePath)
    {
      string projectName = this.CodeRushProxy.Source.ActiveProject.Name;
      this.CodeRushProxy.Solution.AddFileToProject(projectName, filePath);
      this.CodeRushProxy.UndoStack.Add(new AddedProjectFileUndoUnit(projectName, filePath));
    }

    /// <summary>
    /// Adds the interface members to contract.
    /// </summary>
    /// <param name="contractClass">The contract class.</param>
    private void AddInterfaceMembersToContract(Class contractClass)
    {
      if (contractClass == null)
      {
        throw new ArgumentNullException("contractClass", "contractClass is null.");
      }

      Contract.EndContractBlock();
      var members = this.InterfaceUpdater.SourceInterface.AllMembers;
      if (members != null)
      {
        foreach (Member member in members)
        {
          Contract.Assert(contractClass != null);

          if (member != null)
          {
            if (this.TryAddMemberAsProperty(contractClass, member))
            {
              continue;
            }

            if (this.TryAddMemberAsMethod(contractClass, member))
            {
              continue;
            }
          }
        }
      }
    }

    /// <summary>
    /// Adds the linking attribute.
    /// </summary>
    /// <param name="contractClass">The contract class.</param>
    private void AddLinkingAttribute(Class contractClass)
    {
      Contract.Requires(contractClass != null, "contractClass must not be null.");

      var typeOfExp = new TypeOfExpression(new TypeReferenceExpression(this.InterfaceName));
      var attribute = new CustomAttribute() { Name = ContractClassFor };
      if (attribute.Arguments != null)
      {
        attribute.Arguments.Add(typeOfExp);
      }

      var section = new AttributeSection();
      if (section.AttributeCollection != null)
      {
        section.AttributeCollection.Add(attribute);
      }

      contractClass.AddAttributeSection(section);
    }

    /// <summary>
    /// Gets the class documentation comment.
    /// </summary>
    /// <returns>An <see cref="XmlDocComment"/> summarizing the purpose of the contract class.</returns>
    private XmlDocComment GetClassDocumentationComment()
    {
      var summary = new StringBuilder();
      summary.AppendLine();
      summary.AppendFormat(
        "{0} class contains CodeContract declarations for {1}.",
        this.ContractClassName,
        this.InterfaceName);
      summary.AppendLine();

      var summaryElement = new XmlElement("summary");
      summaryElement.AddNode(new XmlText(summary.ToString()));

      var classDocumentationComment = new XmlDocComment();
      classDocumentationComment.AddNode(summaryElement);
      return classDocumentationComment;
    }

    /// <summary>
    /// Copies the file header from the source interface to the contract file.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    private void CopyFileHeader(string filePath)
    {
      this.CodeRushProxy.File.Activate(filePath);

      // Do not dispose, belongs to CodeRush
      TextDocument contractClassDocument = this.CodeRushProxy.Documents.GetTextDocument(filePath);
      if (contractClassDocument == null)
      {
        return;
      }

      var commentInsertionPoint = contractClassDocument.Range.Top;
      foreach (var headerComment in this.InterfaceUpdater.InterfaceFileComments)
      {
        if (headerComment == null)
        {
          continue;
        }

        string commentText = headerComment.Name ?? string.Empty;
        var contractHeaderComment = new Comment()
                    {
                      CommentType = headerComment.CommentType,
                      Name = commentText.Replace(this.InterfaceName, this.ContractClassName)
                    };
        contractClassDocument.InsertText(commentInsertionPoint, this.CodeRushProxy.Language.GenerateElement(contractHeaderComment, this.Language));
        commentInsertionPoint.Line++;
      }

      contractClassDocument.Format();
    }

    /// <summary>
    /// Gets the code for the contract class file
    /// </summary>
    /// <returns>
    /// The code for the contract class file.
    /// </returns>
    private string GetCodeForContractClassFile()
    {
      return this.CodeRushProxy.Language.GenerateElement(
        this.GetContractClassFile(),
        this.InterfaceUpdater.TextDocument.Language);
    }

    /// <summary>
    /// Gets the contract class.
    /// </summary>
    /// <returns>The contract class.</returns>
    private Class GetContractClass()
    {
      var contractClass = new Class(this.ContractClassName);
      this.SetupClass(contractClass);
      ////this.AddXmlDocSummary(contractClass);
      this.AddLinkingAttribute(contractClass);
      this.AddInterfaceMembersToContract(contractClass);
      return contractClass;
    }

    /// <summary>
    /// Gets the contract class file.
    /// </summary>
    /// <returns>
    /// A <see cref="SourceFile"/> containing the contract class,
    /// namespace (if applicable), namespace references, and header comment.
    /// </returns>
    private SourceFile GetContractClassFile()
    {
      var sourceFile = new SourceFile();

      // Nested namespace?
      LanguageElement cursor = sourceFile;
      if (this.InterfaceNamespace != null)
      {
        cursor = new Namespace(this.InterfaceNamespace.Name);
        sourceFile.AddNode(cursor);
      }

      // Add namespace references
      var namespaceNodes = this.InterfaceUpdater.NamespaceReferences;
      cursor.AddNamespaceReferences(namespaceNodes);

      // Class
      cursor.AddNode(this.GetClassDocumentationComment());
      cursor.AddNode(this.GetContractClass());
      return sourceFile;
    }

    /// <summary>
    /// Gets the contract file path.
    /// </summary>
    /// <returns>The contract file path.</returns>
    private string GetContractFilePath()
    {
      var filename = Path.ChangeExtension(this.ContractClassName, this.GetLanguageFileExtension());
      var directoryPath = Path.GetDirectoryName(this.InterfaceUpdater.FilePath);
      return Path.Combine(directoryPath, filename);
    }

    /// <summary>
    /// Gets the language file extension.
    /// </summary>
    /// <returns>The language file extension.</returns>
    private string GetLanguageFileExtension()
    {
      string supportedFileExtensions = this.CodeRushProxy.Language.GetSupportedFileExtensions(this.Language);
      if (supportedFileExtensions == null)
      {
        return string.Empty;
      }

      return supportedFileExtensions.Split(';')[0];
    }

    /// <summary>
    /// Gets the name of the member qualified by the type name.
    /// </summary>
    /// <param name="name">The member name.</param>
    /// <returns>The type qualified member name.</returns>
    private string GetTypeQualifiedName(string name)
    {
      return this.InterfaceName + this.CodeRushProxy.Language.MemberAccessOperator + name;
    }

    /// <summary>
    /// Container for invariant contracts.
    /// </summary>
    [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "False positive.")]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Called by Code Contracts tools.")]
    [ContractInvariantMethod]
    private void ObjectInvariant()
    {
      Contract.Invariant(this.ContractClassName != null, "this.ContractClassName must not be null.");
      Contract.Invariant(this.CodeRushProxy != null, "this.CodeRushProxy must not be null.");
      Contract.Invariant(this.interfaceUpdater != null, "this.interfaceUpdater must not be null.");
    }

    /// <summary>
    /// Setups the basic class properties.
    /// </summary>
    /// <param name="contractClass">The contract class.</param>
    private void SetupClass(Class contractClass)
    {
      if (contractClass == null)
      {
        throw new ArgumentNullException("contractClass", "contractClass is null.");
      }

      Contract.EndContractBlock();

      contractClass.Visibility = MemberVisibility.Public;
      contractClass.IsAbstract = true;
      contractClass.AddSecondaryAncestorType(new TypeReferenceExpression(this.InterfaceName));
    }

    /// <summary>
    /// Tries to add member as a method.
    /// </summary>
    /// <param name="contractClass">The contract class.</param>
    /// <param name="member">The member.</param>
    /// <returns>
    ///   <c>true</c> if the member was a method and added; otherwise <c>false</c>
    /// </returns>
    private bool TryAddMemberAsMethod(Class contractClass, Member member)
    {
      Contract.Requires(contractClass != null, "contractClass is null.");
      Contract.Requires(member != null, "member is null.");

      var interfaceMethod = member as Method;
      if (interfaceMethod == null)
      {
        return false;
      }

      var method = new Method();
      this.SetupMemberDeclaration(method, interfaceMethod);
      if (string.IsNullOrEmpty(interfaceMethod.MemberType) || interfaceMethod.MemberType == "void")
      {
        method.MethodType = MethodTypeEnum.Void;
      }
      else
      {
        method.MethodType = MethodTypeEnum.Function;
        var typeReferenceExp = interfaceMethod.MemberTypeReference;
        if (typeReferenceExp != null)
        {
          this.AddDefaultReturn(method, typeReferenceExp);
        }
      }

      method.AddToDoComment();
      method.CopyParameters(interfaceMethod);

      contractClass.CloneDocComment(interfaceMethod);
      contractClass.AddNode(method);
      return true;
    }

    /// <summary>
    /// Setup the member's declaration.
    /// </summary>
    /// <param name="contractMember">The contract member.</param>
    /// <param name="interfaceMember">The interface member.</param>
    private void SetupMemberDeclaration(Member contractMember, Member interfaceMember)
    {
      if (contractMember == null)
      {
        throw new ArgumentNullException("contractMember", "contractMember is null.");
      }

      if (interfaceMember == null)
      {
        throw new ArgumentNullException("interfaceMember", "interfaceMember is null.");
      }

      Contract.EndContractBlock();
      contractMember.MemberType = interfaceMember.MemberType;
      string typeQualifiedName = this.GetTypeQualifiedName(interfaceMember.Name);
      if (this.CodeRushProxy.Language.IsCSharp)
      {
        contractMember.Name = typeQualifiedName;
      }
      else
      {
        contractMember.Name = interfaceMember.Name;
        contractMember.AddImplementsExpression(new ElementReferenceExpression(typeQualifiedName));
      }
    }

    /// <summary>
    /// Tries to add member as a property.
    /// </summary>
    /// <param name="contractClass">The contract class.</param>
    /// <param name="member">The member.</param>
    /// <returns>
    ///   <c>true</c> if the member was a property and added; otherwise <c>false</c>
    /// </returns>
    private bool TryAddMemberAsProperty(Class contractClass, Member member)
    {
      Contract.Requires(contractClass != null, "contractClass is null.");
      Contract.Requires(member != null, "member is null.");

      var interfaceProperty = member as Property;
      if (interfaceProperty == null)
      {
        return false;
      }

      var contractProperty = new Property();
      this.SetupMemberDeclaration(contractProperty, interfaceProperty);
      contractProperty.AddNode(this.CreateGetter(interfaceProperty));
      contractProperty.AddNode(CreateSetter(interfaceProperty));

      contractClass.CloneDocComment(interfaceProperty);
      contractClass.AddNode(contractProperty);
      return true;
    }

    /// <summary>
    /// Writes the contract file.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    private void WriteContractFile(string filePath)
    {
      string codeForContractClassFile = this.GetCodeForContractClassFile();
      File.WriteAllText(filePath, codeForContractClassFile);
      this.CodeRushProxy.UndoStack.Add(new CreatedFileUndoUnit(filePath, codeForContractClassFile));
    }
  }
}