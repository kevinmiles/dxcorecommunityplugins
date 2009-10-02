/*
 *
 * Refactor_UpdateNamespace
 * Copyright (C)2009 Stephen A. Bohlen
 * http://dxcorecommunityplugins.googlecode.com
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 *
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace Refactor_UpdateNamespace
{
    public partial class Refactor_UpdateNamespacePlugIn : StandardPlugIn
    {
        public override void FinalizePlugIn()
        {
            base.FinalizePlugIn();
        }

        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
            this.refactoringUpdateNamespace.CodeIssueMessage = "Namespace is not default";
        }

        private string GetDefaultNamespace(ProjectElement project)
        {
            return project.DefaultNamespace ?? project.RootNamespace;
        }

        private string ExpectedNamespace(ProjectElement project, string documentPath)
        {
            var projectpath = Path.GetDirectoryName(project.FilePath);
            var filepath = Path.GetDirectoryName(documentPath);
            var namespaceSuffix = filepath.Substring(projectpath.Length).Replace("\\", ".");
            return GetDefaultNamespace(project) + namespaceSuffix;
        }

        private string ExpectedNamespace(ProjectElement project, string documentPath, LanguageElement element)
        {
            string fullExpectedNamespace = ExpectedNamespace(project, documentPath);
            string parentNamespace = CurrentNamespace(element.Parent);
            return fullExpectedNamespace.StartsWith(parentNamespace + ".") ? fullExpectedNamespace.Substring(parentNamespace.Length + 1) : fullExpectedNamespace;
        }

        private string CurrentNamespace(LanguageElement element)
        {
            var names = new List<string>();
            while (element != null && element.ElementType == LanguageElementType.Namespace)
            {
                names.Add(element.Name);
                element = element.Parent;
            }
            return string.Join(".", names.Reverse<string>().ToArray());
        }

        private string CurrentNamespace(INamespaceElement namespaceElement)
        {
            return namespaceElement.FullName;
        }

        private bool ContainsOnlyNamespacesOrUsings(LanguageElement element)
        {
            return element.Nodes.Cast<LanguageElement>().All(node => node.ElementType == LanguageElementType.Namespace || node.ElementType == LanguageElementType.NamespaceReference || node.ElementType == LanguageElementType.Comment);
        }

        private bool IsInScope(ISourceFile scope, IElement element)
        {
            return element.Files.Cast<ISourceFile>().Any(file => file.Name == scope.Name);
        }

        private bool ContainsOnlyNamespacesOrUsings(ISourceFile scope, INamespaceElement namespaceElement)
        {
            return namespaceElement.Children.All<IElement>(child => !IsInScope(scope, child) || child is INamespaceElement || child is INamespaceReference);
        }

        private bool NamespaceShouldBeUpdated(LanguageElement element, string expectedNamespace)
        {
            bool inNamespace = element != null && element.ElementType == LanguageElementType.Namespace;
            if (!inNamespace) return false;

            string currentNamespace = CurrentNamespace(element);
            bool namespaceIsDefault = expectedNamespace == currentNamespace || (ContainsOnlyNamespacesOrUsings(element) && expectedNamespace.StartsWith(currentNamespace));

            bool parentNamespaceShouldBeUpdated = NamespaceShouldBeUpdated(element.Parent, expectedNamespace);
            return !parentNamespaceShouldBeUpdated && !namespaceIsDefault;
        }

        private bool NamespaceShouldBeUpdated(ISourceFile scope, IElement element, string expectedNamespace)
        {
            INamespaceElement namespaceElement = element as INamespaceElement;
            if (namespaceElement == null) return false;

            string currentNamespace = CurrentNamespace(namespaceElement);
            bool namespaceIsDefault = expectedNamespace == currentNamespace || (ContainsOnlyNamespacesOrUsings(scope, namespaceElement) && expectedNamespace.StartsWith(currentNamespace));

            bool parentNamespaceShouldBeUpdated = NamespaceShouldBeUpdated(scope, namespaceElement.Parent, expectedNamespace);
            return !parentNamespaceShouldBeUpdated && !namespaceIsDefault;
        }

        private bool ShouldBeAvailable(SourcePoint caret)
        {
            return NamespaceShouldBeUpdated(CodeRush.Source.ActiveFileNode.GetNodeAt(caret), ExpectedNamespace(CodeRush.Source.ActiveFileNode.Project, CodeRush.Documents.ActiveTextDocument.Path));
        }

        private IEnumerable<TextRange> GetNameSpaceRanges(ISourceFile scope, INamespaceElement namespaceElement)
        {
            int filesCount = namespaceElement.Files.Count;
            if (filesCount > 1)
            {
                for (int i = 0; i < filesCount; i++)
                {
                    ISourceFile elementFile = namespaceElement.Files[i];
                    if (elementFile.Name == scope.Name
                        && i < namespaceElement.NameRanges.Count)
                        yield return namespaceElement.NameRanges[i];
                }
                yield break;
            }
            yield return namespaceElement.FirstNameRange;
        }

        private IEnumerable<TextRange> GetWrongNamespaceNameRanges(ISourceFile scope, INamespaceElement namespaceElement)
        {
            foreach (var endRange in GetNameSpaceRanges(scope, namespaceElement))
            {
                INamespaceElement rootNamespace = namespaceElement;
                TextRange startRange = endRange;
                while (rootNamespace.ParentNamespace != null)
                {
                    rootNamespace = rootNamespace.ParentNamespace;
                    foreach (var range in GetNameSpaceRanges(scope, rootNamespace))
                    {
                        if (range.Start.Line == endRange.End.Line)
                        {
                            startRange = range;
                        }
                    }
                }
                yield return new TextRange(startRange.Start, endRange.End);
            }
        }

        private void refactoringUpdateNamespace_Apply(object sender, ApplyContentEventArgs ea)
        {
            CodeRush.Documents.ActiveTextDocument.Replace(ea.Element.NameRange, ExpectedNamespace(CodeRush.Source.ActiveFileNode.Project, CodeRush.Documents.ActiveTextDocument.Path, ea.Element), "Update To Default Namespace", true);
        }

        private void refactoringUpdateNamespace_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
        {
            ea.Available = ShouldBeAvailable(ea.Caret);
        }

        private void refactoringUpdateNamespace_LanguageSupported(LanguageSupportedEventArgs ea)
        {
            ea.Handled = ea.LanguageID.ToLower() == "csharp";
        }

        private void wrongNamespaceIssueProvider_CheckCodeIssues(object sender, CheckCodeIssuesEventArgs ea)
        {
            if (ea.IsSuppressed(ea.Scope))
                return;

            SourceFile scope = ea.Scope as SourceFile;
            if (scope == null)
                return;

            ProjectElement project = scope.Project as ProjectElement;
            if (project == null)
                return;

            foreach (IElement element in ea.GetEnumerable(ea.Scope, ElementFilters.Namespace))
            {
                var namespaceElement = element as INamespaceElement;
                if (namespaceElement == null)
                    continue;

                string path = scope.FilePath;
                string expectedNamespace = ExpectedNamespace(project, path);
                if (NamespaceShouldBeUpdated(scope, namespaceElement, expectedNamespace))
                {
                    foreach (var fullNameRange in GetWrongNamespaceNameRanges(scope, namespaceElement))
                    {
                        ea.AddHint(fullNameRange, "Namespace is not default", 10);
                    }
                }
            }
        }
    }
}