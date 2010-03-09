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
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.CodeRush.StructuralParser;

namespace CR_ClassCleaner
{
    public partial class EditForm : Form
    {
        private List<NameValueSelectected> elementTypes =
             new List<NameValueSelectected>();

        private CodeGroup group;

        private List<NameValueSelectected> visibilties =
                     new List<NameValueSelectected>();

        /// <summary>
        /// Initializes a new instance of the EditForm class.
        /// </summary>
        public EditForm(CodeGroup group)
            : this()
        {
            try
            {
                this.group = group;

                UpdateDataSources();
            }
            catch (Exception ex)
            {
                Utilities.HandleException(ex);
            }
        }

        public EditForm()
        {
            InitializeComponent();
        }

        private void DoneClicked(object sender, EventArgs e)
        {
            ElementTypeCodeGroup elementCodeGroup = group as ElementTypeCodeGroup;
            if (elementCodeGroup != null)
            {
                elementCodeGroup.ElementTypes.Clear();
                foreach (NameValueSelectected element in elementTypes)
                {
                    if (element.Selected == true)
                        elementCodeGroup.ElementTypes.Add((LanguageElementType)element.Value);
                }

                elementCodeGroup.Visibilty.Clear();
                foreach (NameValueSelectected visibilty in visibilties)
                {
                    if (visibilty.Selected == true)
                        elementCodeGroup.Visibilty.Add((MemberVisibility)visibilty.Value);
                }
            }

            ParentForm.Close();
        }

        private void LoadElementTypes()
        {
            Type type = typeof(LanguageElementType);

            FieldInfo[] fields = type.GetFields();

            for (int i = 0; i < fields.Length - 1; i++)
            {
                elementTypes.Add(
                     new NameValueSelectected(i, ((LanguageElementType)i).ToString()));
            }

            elementTypes.Sort(new NameValueSelectectedComparer());

            languageElementTypebindingSource.DataSource = elementTypes;
        }

        private void LoadVisibility()
        {
            Type type = typeof(MemberVisibility);

            FieldInfo[] fields = type.GetFields();

            for (int i = 0; i < fields.Length - 1; i++)
            {
                visibilties.Add(
                     new NameValueSelectected(i, ((MemberVisibility)i).ToString()));
            }

            visibilties.Sort(new NameValueSelectectedComparer());

            visibilityBindingSource.DataSource = visibilties;
        }

        private void SetPanelToFill(Panel panel)
        {
            panel.Dock = DockStyle.Fill;
            panel.BringToFront();
        }

        private void SetSelectedElementTypes(ElementTypeCodeGroup elementCodeGroup)
        {
            foreach (LanguageElementType elementType in elementCodeGroup.ElementTypes)
            {
                NameValueSelectected foundValue = elementTypes.Find(
                     delegate(NameValueSelectected languageElement)
                     {
                         return (int)elementType == languageElement.Value;
                     });

                if (foundValue != null)
                    foundValue.Selected = true;
            }
        }

        private void SetSelectedVisibilty(ElementTypeCodeGroup elementCodeGroup)
        {
            foreach (MemberVisibility visibility in elementCodeGroup.Visibilty)
            {
                NameValueSelectected foundValue = visibilties.Find(
                     delegate(NameValueSelectected memberVisibilty)
                     {
                         return (int)visibility == memberVisibilty.Value;
                     });

                if (foundValue != null)
                    foundValue.Selected = true;
            }
        }

        private void SetSplitContainerToFill(SplitContainer container)
        {
            container.Dock = DockStyle.Fill;
            container.BringToFront();
        }

        private void UpdateDataSources()
        {
            groupBindingSource.DataSource = group;

            if (group is RegexCodeGroup)
            {
                regexGroupBindingSource.DataSource = group;
                SetPanelToFill(regexPanel);
            }
            else if (group is ElementTypeCodeGroup)
            {
                elementTypeBindingSource.DataSource = group;
                SetSplitContainerToFill(elementSplitContainer);
                LoadElementTypes();
                SetSelectedElementTypes((ElementTypeCodeGroup)group);
                LoadVisibility();
                SetSelectedVisibilty((ElementTypeCodeGroup)group);
            }
        }

    }
}