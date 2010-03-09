//////////////////////////////////////////////////////////////////
//CR_ClassCleaner plugin provides organization capabilties to 
//Visual Studio when used with the DXCore framework.
//Copyright (C) 2006  John Luif
//
//This program is free software; you can redistribute it and/or
//modify it under the terms of the GNU General Public License
//as published by the Free Software Foundation version 2
//of the License.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program; if not, write to the Free Software
//Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
//////////////////////////////////////////////////////////////////
using System;
using System.ComponentModel;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;

namespace CR_ClassCleaner
{
    public partial class ClassCleanerPlugIn : StandardPlugIn
    {
        public override void FinalizePlugIn()
        {
            base.FinalizePlugIn();
        }

        public void NewInitializePlugIn()
        {
            base.InitializePlugIn();
            OptionsChanged += new OptionsChangedEventHandler(ClassCleanerPlugIn_OptionsChanged);
        }

        private void ClassCleanerPlugIn_OptionsChanged(OptionsChangedEventArgs ea)
        {
            ClassCleanerConfig.Current.Load();
        }

        private void ExecuteCutCurrentMember(ExecuteEventArgs ea)
        {
            bool selected =
                 Utilities.SelectCurrentMember(CodeRush.Caret, CodeRush.Documents.ActiveTextDocument);

            if (selected == true)
                CodeRush.Clipboard.Cut();
        }

        private void ExecuteOrganizeWithRegions(ExecuteEventArgs ea)
        {
            ClassOrganizer organizer = new ClassOrganizer();
            organizer.Organize(CodeRush.Documents.ActiveTextDocument,
                                     CodeRush.Source.ActiveType,
                                     true);
        }

        private void ExecuteOrganizeWORegions(ExecuteEventArgs ea)
        {
            ClassOrganizer organizer = new ClassOrganizer();
            organizer.Organize(CodeRush.Documents.ActiveTextDocument,
                                     CodeRush.Source.ActiveType,
                                     false);
        }

        private void ExecuteRemoveRegion(ExecuteEventArgs ea)
        {
            RegionHandler.RemoveRegions(CodeRush.Documents.ActiveTextDocument);
            Utilities.DeleteWhitespace(CodeRush.Documents.ActiveTextDocument);
        }

        private void ExecuteRemoveWhitespace(ExecuteEventArgs ea)
        {
            Utilities.DeleteWhitespace(CodeRush.Documents.ActiveTextDocument);
        }

        private void ExecuteSelectCurrentMember(ExecuteEventArgs ea)
        {
            Utilities.SelectCurrentMember(CodeRush.Caret, CodeRush.Documents.ActiveTextDocument);
        }

    }
}