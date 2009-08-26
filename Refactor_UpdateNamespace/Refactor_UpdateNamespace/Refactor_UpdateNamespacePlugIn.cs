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
        }

        private string GetDefaultNamespace()
        {
            return CodeRush.Source.ActiveFileNode.Project.DefaultNamespace ?? CodeRush.Source.ActiveFileNode.Project.RootNamespace;
        }

        private string ExpectedNamespace()
        {
            var projectpath = Path.GetDirectoryName(CodeRush.Source.ActiveFileNode.Project.FilePath);
            var filepath = Path.GetDirectoryName(CodeRush.Documents.ActiveTextDocument.Path);
            var namespaceSuffix = filepath.Substring(projectpath.Length).Replace("\\", ".");

            return GetDefaultNamespace() + namespaceSuffix;
        }

        private void refactoringUpdateNamespace_Apply(object sender, ApplyContentEventArgs ea)
        {
            CodeRush.Documents.ActiveTextDocument.Replace(ea.Element.NameRange, ExpectedNamespace(), "Update To Default Namespace", true);
        }

        private void refactoringUpdateNamespace_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
        {
            ea.Available = ShouldBeAvailable(ea.Caret) ? true : false;
        }

        private bool ShouldBeAvailable(SourcePoint caret)
        {
            LanguageElement element = CodeRush.Source.ActiveFileNode.GetNodeAt(caret);
            bool inNamespace = element != null && element.ElementType == LanguageElementType.Namespace;
            bool nameSpaceIsDefault = element.Name == ExpectedNamespace();

            return (inNamespace && !nameSpaceIsDefault);
        }

        private void refactoringUpdateNamespace_LanguageSupported(LanguageSupportedEventArgs ea)
        {
            ea.Handled = ea.LanguageID.ToLower() == "csharp";
        }

    }
}