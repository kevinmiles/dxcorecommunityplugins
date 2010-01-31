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
using System.Globalization;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;

namespace CR_ClassCleaner
{
    [UserLevel(UserLevel.NewUser)]
    public partial class ClassCleanerOptions : OptionsPage
    {
        public static string GetCategory()
        {
            return ClassCleanerConfig.CatagoryName;
        }

        public static string GetPageName()
        {
            return ClassCleanerConfig.PageName;
        }

        /// <summary>
        /// DXCore-generated code... 
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ClassCleanerConfig.Current.Load();
            UpdatedDatasource();

            if (!GetStorage().ReadBoolean("Preferences", "LicenseViewed", false))
            {
                var license = new LicenseDisplayForm();
                license.StartPosition = FormStartPosition.CenterParent;
                license.ShowDialog(this);
                GetStorage().WriteBoolean("Preferences", "LicenseViewed", true);
            }
        }

        private void AddButtonClick(object sender, EventArgs e)
        {
            try
            {
                AddForm addForm = new AddForm();
                addForm.CodeGroupAdded += new EventHandler<CodeGroupTypeEventArgs>(CodeGroupAdded);
                addForm.StartPosition = FormStartPosition.CenterParent;
                addForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                Utilities.HandleException(ex);
            }
        }

        private void CellClicked(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 &&
                     codeGroupGrid.Columns[e.ColumnIndex] == editButtonColumn)
                {
                    CodeGroup group = groupBindingSource.Current as CodeGroup;
                    EditForm form = new EditForm(group);
                    form.ShowInTaskbar = false;

                    form.StartPosition = FormStartPosition.CenterParent;
                    form.ShowDialog(ParentForm);
                }
            }
            catch (Exception ex)
            {
                Utilities.HandleException(ex);
            }
        }

        private void CodeGroupAdded(object sender, CodeGroupTypeEventArgs e)
        {
            try
            {
                CodeGroup group =
                     (CodeGroup)Activator.CreateInstance(e.CodeGroupType, false);

                groupBindingSource.Add(group);
            }
            catch (Exception ex)
            {
                Utilities.HandleException(ex);
            }
        }

        private void DeleteButtonClick(object sender, EventArgs e)
        {
            try
            {
                groupBindingSource.RemoveCurrent();
            }
            catch (Exception ex)
            {
                Utilities.HandleException(ex);
            }
        }

        private void DownButtonClick(object sender, EventArgs e)
        {
            try
            {
                object current = groupBindingSource.Current;
                int index = groupBindingSource.IndexOf(current);
                index = index + 1 < groupBindingSource.Count ? index + 1 : index;

                MoveCurrent(current, index);
            }
            catch (Exception ex)
            {
                Utilities.HandleException(ex);
            }
        }

        private void ExportButtonClick(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            ShowFileDialog(
                 dialog,
                 "Configuration exported to {0}",
                 "Export Failed!",
                 delegate(string file)
                 {
                     return ClassCleanerConfig.Current.Export(file);
                 });
        }

        private void ImportButtonClick(object sender, EventArgs e)
        {
            bool imported = true;
            OpenFileDialog dialog = new OpenFileDialog();
            ShowFileDialog(
                 dialog,
                 "Configuration imported from {0}",
                 "Import Failed!",
                 delegate(string file)
                 {
                     imported = ClassCleanerConfig.Current.Import(file);
                     return imported;
                 });

            if (imported == true)
                UpdatedDatasource();
        }

        private void MoveCurrent(object current, int index)
        {
            groupBindingSource.RemoveCurrent();
            groupBindingSource.Insert(index, current);
            groupBindingSource.Position = index;
        }

        private void ResetButtonClick(object sender, EventArgs e)
        {
            try
            {
                ClassCleanerConfig.Current.Reset();
                UpdatedDatasource();
            }
            catch (Exception ex)
            {
                Utilities.HandleException(ex);
            }
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            ClassCleanerConfig.Current.Save();
        }

        private void ShowFileDialog(
                     FileDialog dialog,
                     string successMessage,
                     string failureMessage,
                     Predicate<string> filePredicate)
        {
            try
            {
                dialog.DefaultExt = "xml";
                dialog.AddExtension = true;
                dialog.Filter = "(*.xml)|*.xml";
                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    string file = dialog.FileName;
                    bool success = filePredicate(file);

                    if (success == true)
                        MessageBox.Show(
                             string.Format(CultureInfo.CurrentCulture,
                                                successMessage,
                                                file));
                    else
                        MessageBox.Show(failureMessage);
                }
            }
            catch (Exception ex)
            {
                Utilities.HandleException(ex);
            }
        }

        private void UpButtonClick(object sender, EventArgs e)
        {
            try
            {
                object current = groupBindingSource.Current;
                int index = groupBindingSource.IndexOf(current);
                index = index - 1 >= 0 ? index - 1 : index;

                MoveCurrent(current, index);
            }
            catch (Exception ex)
            {
                Utilities.HandleException(ex);
            }
        }

        private void UpdatedDatasource()
        {
            groupBindingSource.DataSource = null;
            groupBindingSource.DataSource = ClassCleanerConfig.Current.Groups;
            codeGroupGrid.Refresh();
        }

        //        private void ReplaceCurrent(CodeGroup codeGroup)
        //        {
        //            if (codeGroup != null)
        //            {
        //                object current = groupBindingSource.Current;
        //                int index = groupBindingSource.IndexOf(current);
        //                groupBindingSource.RemoveCurrent();
        //
        //                groupBindingSource.Insert(index, codeGroup);
        //                groupBindingSource.Position = index;
        //
        //                UpdatedDatasource();
        //            }
        //        }
    }
}