/*
 *
 * Refactor_ClassCleaner_Menu
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
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using CR_ClassCleaner;

namespace Refactor_ClassCleaner_Menu
{
    public partial class Refactor_ClassCleaner_PlugIn : StandardPlugIn
    {
        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            refactoringProviderWORegions.CheckAvailability += refactoringProviderWORegions_CheckAvailability;
            refactoringProviderWORegions.Apply += refactoringProviderWORegions_Apply;

            refactoringProviderWRegions.CheckAvailability += refactoringProviderWRegions_CheckAvailability;
            refactoringProviderWRegions.Apply += refactoringProviderWRegions_Apply;
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

        private Member GetActiveMember(LanguageElement element)
        {
            if (element == null)
            {
                return null;
            }
            Member member = element as Member;
            if (member != null)
            {
                LanguageElementType type = member.ElementType;
                if (((type == LanguageElementType.Method) || (type == LanguageElementType.Property)) || (type == LanguageElementType.Event))
                {
                    return member;
                }
            }
            return (element.GetParent(LanguageElementType.Method, new LanguageElementType[] { LanguageElementType.Property, LanguageElementType.Event }) as Member);
        }

        private bool IsValidSelection(LanguageElement member, TextViewSelection selection, SourcePoint caret)
        {
            if ((member == null) || (selection == null))
            {
                return false;
            }
            if (selection.Exists)
            {
                return false;
            }
            return (member.NameRange.Contains(caret) || CodeRush.Source.GetStartWordRange(member).Contains(caret));
        }

        void refactoringProviderWORegions_Apply(object sender, ApplyContentEventArgs ea)
        {
            ClassOrganizer organizer = new ClassOrganizer();
            organizer.Organize(CodeRush.Documents.ActiveTextDocument,
                                     CodeRush.Source.ActiveType,
                                     false);
        }

        void refactoringProviderWORegions_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
        {
            if (ea == null)
                return;

            if (!IsValidSelection(ea.Element, ea.TextView.Selection, ea.Caret))
            {
                return;
            }

            ea.Available = ea.Element.ElementType == LanguageElementType.Class;
        }

        void refactoringProviderWRegions_Apply(object sender, ApplyContentEventArgs ea)
        {
            ClassOrganizer organizer = new ClassOrganizer();
            organizer.Organize(CodeRush.Documents.ActiveTextDocument,
                                     CodeRush.Source.ActiveType,
                                     true);
        }

        void refactoringProviderWRegions_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
        {
            if (ea == null)
                return;

            if (!IsValidSelection(ea.Element, ea.TextView.Selection, ea.Caret))
            {
                return;
            }

            ea.Available = ea.Element.ElementType == LanguageElementType.Class;
        }


    }
}