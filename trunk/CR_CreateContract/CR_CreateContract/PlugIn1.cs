//-----------------------------------------------------------------------
// <copyright file="Plugin1.cs" company="Blah">
//     Copyright (c) Blah 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections.Generic;
using System.Linq;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.Refactor.Core;

namespace CR_CreateContract
{
    /// <summary>
    /// Hello World
    /// </summary>
    [System.Runtime.InteropServices.GuidAttribute("B6E713EB-7ED1-433D-B206-9430134C6FEF")]
    public partial class PlugIn1 : StandardPlugIn
    {
        /// <summary>
        /// Name of the ContractClassAttribute
        /// </summary>
        private const string ContractClassAttributeName = "ContractClass";

        /// <summary>
        /// Cached reference to the interface declaration.
        /// </summary>
        private TypeDeclaration interfaceDeclaration;

        /// <summary>
        /// A collection of all namespaces references found in the original file.
        /// </summary>
        private IList<NamespaceReference> interfaceNamespaceReferences;

        /// <summary>
        /// Cached reference to the active interface.
        /// </summary>
        private Interface activeInterface;

        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            //
            // TODO: Add your initialization code here.
            //
        }
        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            //
            // TODO: Add your finalization code here.
            //

            base.FinalizePlugIn();
        }
        #endregion

        private void cpCreateContract_Apply(object sender, ApplyContentEventArgs ea)
        {
            // Must have cached instance of interface and interface declaration.
            if (this.activeInterface == null || this.interfaceDeclaration == null)
            {
                return;
            }

            // Add attribute to interface
            string contractClassName = activeInterface.Name.TrimStart('I') + "Contract";
            ElementBuilder elementBuilder = new ElementBuilder();
            TypeReferenceExpression typeReferenceExpression = new TypeReferenceExpression(contractClassName);
            TypeOfExpression typeOfExpression = new TypeOfExpression(typeReferenceExpression);
            AttributeSection addedAttributeSection = elementBuilder.AddAttributeSection(null);
            Attribute addAttribute = elementBuilder.AddAttribute(addedAttributeSection, ContractClassAttributeName);
            elementBuilder.AddArgument(addAttribute, typeOfExpression);
            SourcePoint insertionPoint = SourcePoint.Empty;
            insertionPoint = activeInterface.Range.Top;
            string code = elementBuilder.GenerateCode(ea.TextDocument.Language);

            // Add using statement
            ElementBuilder elementBuilder1 = new ElementBuilder();
            NamespaceReference addNamespaceReference = elementBuilder1.AddNamespaceReference(null, "System.Diagnostics.Contracts");
            SourcePoint sourcePoint = SourcePoint.Empty;
            VisualStudioDocument parentDocument = activeInterface.GetParentDocument();
            interfaceNamespaceReferences = parentDocument.Nodes.OfType<NamespaceReference>().ToList();
            if (activeInterface.InsideNamespace)
            {
                Namespace parentNamespace = activeInterface.GetNamespace();
                var nestedNamespaceReferences = parentNamespace.Nodes.OfType<NamespaceReference>();
                interfaceNamespaceReferences = interfaceNamespaceReferences.Concat(nestedNamespaceReferences).ToList();
            }
            foreach (NamespaceReference item in interfaceNamespaceReferences)
            {
                sourcePoint = item.Range.Start;
                ////sourcePoint.Line++;
                string namespaceName = item.Name;
                string addedNamespaceName = addNamespaceReference.Name;
                if (namespaceName.StartsWith("System") && !addedNamespaceName.StartsWith("System"))
                {
                    continue;
                }

                var result = string.Compare(namespaceName, addedNamespaceName);
                if (result >= 0)
                {
                    break;
                }
            }
            interfaceNamespaceReferences.Add(addNamespaceReference);

            // Generate the code contract class.
            var contractClassBuilder = new ElementBuilder();

            Namespace interfaceNamespace = null;
            if (activeInterface.InsideNamespace)
            {
                interfaceNamespace = activeInterface.GetNamespace();
            }
            var contractClass = contractClassBuilder.AddClass(null, contractClassName);
            contractClass.Visibility = MemberVisibility.Public;
            contractClass.IsAbstract = true;
            var activeInterfaceTypeReference = new TypeReferenceExpression(activeInterface.Name);
            contractClass.AddSecondaryAncestorType(activeInterfaceTypeReference);
            var classAttributeSection = contractClassBuilder.AddAttributeSection(contractClass);
            var forAttribute = contractClassBuilder.AddAttribute(classAttributeSection, "ContractClassFor");
            var typeOfActiveInterface = new TypeOfExpression(activeInterfaceTypeReference);
            contractClassBuilder.AddArgument(forAttribute, typeOfActiveInterface);

            string classSummaryDocumentation = string.Format("{0} class contains CodeContract declarations for {1}.", contractClassName, activeInterface.Name);
            var summaryText = new XmlText(classSummaryDocumentation);
            var summaryElement = new XmlElement("summary");
            summaryElement.AddNode(summaryText);
            var classDocumentationComment = new XmlDocComment();
            classDocumentationComment.AddNode(summaryElement);
            contractClass.AddCommentNode(classDocumentationComment);

            foreach (Member member in activeInterface.AllMembers)
            {
                string memberQualifiedName = activeInterface.Name + CodeRush.Language.MemberAccessOperator + member.Name;
                Property interfaceProperty = member as Property;
                if (interfaceProperty != null)
                {
                    Property contractProperty = contractClassBuilder.AddProperty(contractClass, interfaceProperty.MemberType, memberQualifiedName);
                    if (interfaceProperty.DocComment != null)
                    {
                        var clonedComment = interfaceProperty.DocComment.Clone(ElementCloneOptions.Default) as XmlDocComment;
                        contractProperty.AddCommentNode(clonedComment);
                    }

                    if (interfaceProperty.HasGetter)
                    {
                        var todoComment = new Comment() { CommentType = CommentType.SingleLine, Name = " TODO: Add Contracts Here" };
                        Get contractPropertyGetter = contractClassBuilder.AddGetter(contractProperty);
                        contractPropertyGetter.InsertNode(0, todoComment);
                        var propertyDefaultValueExpression = new DefaultValueExpression(interfaceProperty.MemberTypeReference);
                        Return contractPropertyReturn = contractClassBuilder.AddReturn(contractPropertyGetter, propertyDefaultValueExpression);
                        contractClassBuilder.AddComment(contractPropertyReturn, " CodeContracts runtime will ignore this return value.", CommentType.SingleLine);
                    }

                    if (interfaceProperty.HasSetter)
                    {
                        Set contractPropertySetter = contractClassBuilder.AddSetter(contractProperty);
                        var todoComment = new Comment() { CommentType = CommentType.SingleLine, Name = " TODO: Add Contracts Here" };
                        contractPropertySetter.InsertNode(0, todoComment);
                    }

                    continue;
                }

                Method interfaceMethod = member as Method;
                if (interfaceMethod != null)
                {
                    Method contractMethod = contractClassBuilder.AddMethod(contractClass, interfaceMethod.MemberType, memberQualifiedName);
                    if (interfaceMethod.DocComment != null)
                    {
                        var clonedComment = interfaceMethod.DocComment.Clone(ElementCloneOptions.Default) as XmlDocComment;
                        contractMethod.AddCommentNode(clonedComment);
                    }

                    var todoComment = new Comment() { CommentType = CommentType.SingleLine, Name = " TODO: Add Contracts Here" };
                    contractMethod.InsertNode(0, todoComment);
                    if (interfaceMethod.ParameterCount > 0)
                    {
                        contractMethod.AddParameters(interfaceMethod.Parameters);
                    }

                    if (interfaceMethod.MethodType != MethodTypeEnum.Void)
                    {
                        DefaultValueExpression methodDefaultValueExpress = new DefaultValueExpression(interfaceMethod.MemberTypeReference);
                        Return methodReturn = contractClassBuilder.AddReturn(contractMethod, methodDefaultValueExpress);
                        contractClassBuilder.AddComment(methodReturn, " CodeContracts runtime will ignore this return value.", CommentType.SingleLine);
                    }
                }
            }

            // Contract class builder should now have the entire type declaration + attribute.
            // see if we want to put it in a namespace
            LanguageElement contractClassWithParent = null;
            if (interfaceNamespace != null)
            {
                SourceFile sourceFile = new SourceFile();
                foreach (Comment headerComment in activeInterface.FileNode.AllComments)
                {
                    Comment contractHeaderComment = new Comment();
                    contractHeaderComment.CommentType = headerComment.CommentType;
                    contractHeaderComment.Name = headerComment.Name.Replace(activeInterface.Name, contractClassName);
                    sourceFile.AddComment(contractHeaderComment);
                }

                Namespace contractNamespace = new Namespace(interfaceNamespace.Name);
                foreach (NamespaceReference ns in GetSortedNamespaces())
                {
                    contractNamespace.AddNode(ns);
                }
                contractNamespace.AddNode(contractClass);
                sourceFile.AddNode(contractNamespace);
                contractClassWithParent = sourceFile;
            }

            // TODO: What if it is not in a namespace?
            string supportedFileExtensions = CodeRush.Language.GetSupportedFileExtensions(ea.TextDocument.Language);
            string extension = supportedFileExtensions.Split(';')[0];
            string filenameWithExtension = System.IO.Path.ChangeExtension(contractClassName, extension);



            SourcePoint classInsertionPoint = sourcePoint.Clone();
            classInsertionPoint.Line++;

            // Add code contract file.
            var project = CodeRush.Source.ActiveProject;
            string projectPath = project.FilePath;
            string projectDirectoryPath = System.IO.Path.GetDirectoryName(projectPath);

            string filePath = System.IO.Path.Combine(projectDirectoryPath, filenameWithExtension);
            string contractClassFileContents = CodeRush.Language.GenerateElement(contractClassWithParent, ea.TextDocument.Language);

            // Undoable
            ////if (!System.IO.File.Exists(filePath))
            ////{
            ////    System.IO.File.WriteAllText(filePath, contractClassCode);
            ////    CreatedFileUndoUnit createdFileUndoUnit = new CreatedFileUndoUnit(filePath, contractClassCode);
            ////    CodeRush.UndoStack.Add(createdFileUndoUnit);
            ////    CodeRush.Solution.AddFileToProject(project.Name, filePath);
            ////    CodeRush.UndoStack.Add(new AddedProjectFileUndoUnit(project.Name, filePath));
            ////}
            // Undoable

            using (CodeRush.TextBuffers.NewMultiFileCompoundAction(cpCreateContract.ActionHintText))
            {
                CodeRush.Markers.Drop();

                // Update interface file
                var insertText = ea.TextDocument.InsertText(insertionPoint, code);
                var insertText1 = ea.TextDocument.InsertText(sourcePoint, elementBuilder1.GenerateCode(ea.TextDocument.Language));
                ////var classInsertText = ea.TextDocument.InsertText(classInsertionPoint, contractClassCode);

                ea.TextDocument.Format(insertText);
                ea.TextDocument.Format(insertText1);
                ////ea.TextDocument.Format(classInsertText);
                ea.TextDocument.Format();

                // Create contract file
                if (!System.IO.File.Exists(filePath))
                {
                    System.IO.File.WriteAllText(filePath, contractClassFileContents);
                    CreatedFileUndoUnit createdFileUndoUnit = new CreatedFileUndoUnit(filePath, contractClassFileContents);
                    CodeRush.UndoStack.Add(createdFileUndoUnit);
                    CodeRush.Solution.AddFileToProject(project.Name, filePath);
                    CodeRush.UndoStack.Add(new AddedProjectFileUndoUnit(project.Name, filePath));
                    CodeRush.File.Activate(filePath);
                    TextDocument contractClassDocument = CodeRush.Documents.GetTextDocument(filePath);
                    if (contractClassDocument != null)
                    {
                        SourcePoint commentInsertionPoint = contractClassDocument.Range.Top;
                        foreach (Comment headerComment in activeInterface.FileNode.AllComments)
                        {
                            Comment contractHeaderComment = new Comment();
                            contractHeaderComment.CommentType = headerComment.CommentType;
                            contractHeaderComment.Name = headerComment.Name.Replace(activeInterface.Name, contractClassName);
                            contractClassDocument.InsertText(commentInsertionPoint, CodeRush.Language.GenerateElement(contractHeaderComment, contractClassDocument.Language));
                            commentInsertionPoint.Line++;
                        }
                        contractClassDocument.Format();                        
                    }

                    ////if (interfaceNamespace != null)
                    ////{
                    ////    Namespace contractNamespace = new Namespace(interfaceNamespace.Name);
                    ////    contractClassDocument.FileNode.AddNode(contractNamespace);
                    ////    contractClassDocument.InsertText(contractClassDocument.Range.End, CodeRush.Language.GenerateElement(contractNamespace, ea.TextDocument.Language));
                    ////    CodeRush.Source.ParseIfNeeded();
                    ////    contractNamespace = contractClassDocument.GetNamespaces(contractNamespace.Name).FirstOrDefault() as Namespace;
                    ////    if (contractNamespace == null)
                    ////    {
                    ////        throw new System.ApplicationException("Failure to render namespace.");
                    ////    }

                    ////    contractClassDocument.InsertText(contractNamespace.BlockCodeRange.End, contractClassCode);
                    ////    CodeRush.Source.ParseIfNeeded();
                    ////}
                }
            }
        }

        private IEnumerable<NamespaceReference> GetSortedNamespaces()
        {
            var sortedOtherNamespaces = this.interfaceNamespaceReferences
                                        .Where(ns => !ns.Name.StartsWith("System"))
                                        .OrderBy(ns => ns.Name);
            return this.interfaceNamespaceReferences
                .Where(ns => ns.Name.StartsWith("System"))
                .OrderBy(ns => ns.Name)
                .Concat(sortedOtherNamespaces);
        }

        private void cpCreateContract_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
        {
            // Must be in an interface.
            activeInterface = ea.ClassInterfaceOrStruct as Interface;
            if (activeInterface == null)
            {
                return;
            }

            if (ea.Caret.Line == activeInterface.Range.Start.Line)
            {
                // Must not already have the ContractClassAttribute
                interfaceDeclaration = ea.CodeActive as TypeDeclaration;
                if (interfaceDeclaration != null)
                {
                    if (interfaceDeclaration.AttributeSectionsCount > 0)
                    {
                        AttributeSection attributeSection = interfaceDeclaration.AttributeSections[0] as AttributeSection;
                        if (attributeSection != null &&
                            attributeSection.AttributeCollection.OfType<Attribute>().Any(attr => attr.Name == ContractClassAttributeName))
                        {
                            return;
                        }
                    }
                }

                ea.Available = true;
            }
        }
    }
}