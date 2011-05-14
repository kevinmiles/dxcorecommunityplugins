using System;
using System.Collections.Generic;
using DevExpress.CodeRush.Core;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using DevExpress.DXCore.Controls.XtraGrid.Views.Grid;
using DevExpress.DXCore.Controls;

namespace CodeIssueAnalysis
{
    [UserLevel(UserLevel.NewUser)]
    public partial class CodeIssueOptions : OptionsPage
    {
        //the methods to get data from decoupled storage are not thread safe
        //they seem to drop results sometimes
        //so lists are generated for use on startup or change of options
        internal static List<string> fileInclusions = CodeIssueOptions.GetInclusions();
        internal static List<string> fileContentInclusions = CodeIssueOptions.GetContentInclusions();
        internal static List<string> fileExclusions = CodeIssueOptions.GetExclusions();
        internal static List<string> fileContentExclusions = CodeIssueOptions.GetContentExclusions();

        private const string OptionsCategory = "Editor\\Code Analysis";
        private const string OptionsPageName = "Code Issue Analysis";

        private const string Summary = "Summary";
        private const string IncludeSummaryKey = "Inc";
        private const string IncludeContentSummaryKey = "IncContent";
        private const string ExcludeSummaryKey = "Exc";
        private const string ExcludeContentSummaryKey = "ExcContent";

        private const string IncludeKey = "Inclusion";
        private const string IncludeContentKey = "IncludeContent"; 
        private const string ExcludeKey = "Exclusion";
        private const string ExcludeContentKey = "ExcludeContent";

        internal const string AllString = "All";
        internal const string Separator = "----------------------------------------";
        internal const string SaveString = "Save Layout And Filtering";
        internal const string RemoveString = "Remove Layout And Filtering";

        internal static string GetLayoutPath()
        {
            return new DevExpress.CodeRush.Common.DXCorePaths().CommunityPlugInsPath + Path.DirectorySeparatorChar + "CodeIssueAnalysisLayouts";
        }

        internal static void SetupSettingsLists()
        {
            fileInclusions = CodeIssueOptions.GetInclusions();
            fileContentInclusions = CodeIssueOptions.GetContentInclusions();
            fileExclusions = CodeIssueOptions.GetExclusions();
            fileContentExclusions = CodeIssueOptions.GetContentExclusions();
        }

        internal static void UpdateLayoutsList(ComboBox layoutList, bool includeDefaultStrings, string layoutName)
        {
            UpdateLayoutsList(layoutList, includeDefaultStrings);
            layoutList.SelectedItem = layoutName;
        }

        internal static void UpdateLayoutsList(ComboBox layoutList, bool includeDefaultStrings)
        {
            try
            {
                layoutList.Items.Clear();

                if (Directory.Exists(GetLayoutPath()) == false)
                    Directory.CreateDirectory(GetLayoutPath());

                if (includeDefaultStrings)
                    layoutList.Items.Add(AllString);

                foreach (string file in Directory.GetFiles(GetLayoutPath(), "*.xml"))
                {
                    layoutList.Items.Add(Path.GetFileName(file));
                }

                if (includeDefaultStrings)
                {
                    layoutList.Items.Add(Separator);
                    layoutList.Items.Add(SaveString);
                    layoutList.Items.Add(RemoveString);
                    layoutList.SelectedItem = AllString;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("Update Layout List Failed", "Failed to update the layout list");
            }
        }

        internal static void LoadLayout(GridView view, string name)
        {
            try
            {
                view.RestoreLayoutFromXml(GetLayoutPath() + Path.DirectorySeparatorChar + name);
            }
            catch (Exception err)
            {
                MessageBox.Show("Load Layout Failed", "Failed to load the layout");
            }
        }

        // DXCore-generated code...
        #region Initialize
        protected override void Initialize()
        {
            base.Initialize();
            LoadSettings();
        }
        #endregion

        #region GetCategory
        public static string GetCategory()
        {
            return @"Editor\Code Analysis";
        }
        #endregion
        #region GetPageName
        public static string GetPageName()
        {
            return @"Code Issues Analysis";
        }
        #endregion

        private  void SaveOptions()
        {
          using (DecoupledStorage settings = CodeRush.Options.GetStorage(OptionsCategory, OptionsPageName))
          {
            settings.WriteInt32(Summary, IncludeSummaryKey, listIncludeFiles.Items.Count);
            settings.WriteInt32(Summary, ExcludeSummaryKey, listExcludeFiles.Items.Count);
            settings.WriteInt32(Summary, ExcludeContentSummaryKey, listExcludeContent.Items.Count);

            for (int i = 0; i < listIncludeFiles.Items.Count; i++)
            {
                settings.WriteString(IncludeKey, IncludeKey + i, listIncludeFiles.Items[i].ToString(), true);
            }

            for (int i = 0; i < listIncludeContent.Items.Count; i++)
            {
                settings.WriteString(IncludeContentKey, IncludeContentKey + i, listIncludeContent.Items[i].ToString(), true);
            }

            for (int i = 0; i < listExcludeFiles.Items.Count; i++)
            {
                settings.WriteString(ExcludeKey, ExcludeKey + i, listExcludeFiles.Items[i].ToString(), true);
            }

            for (int i = 0; i < listExcludeContent.Items.Count; i++)
            {
                settings.WriteString(ExcludeContentKey, ExcludeContentKey + i, listExcludeContent.Items[i].ToString(), true);
            }
          }
          SetupSettingsLists();
        }

        private void LoadSettings()
        {
            foreach (string inclusion in GetInclusions())
            {
            	listIncludeFiles.Items.Add(inclusion);
            }

            foreach (string inclusion in GetContentInclusions())
            {
                listIncludeContent.Items.Add(inclusion);
            }

            foreach (string exclusion in GetExclusions())
            {
                listExcludeFiles.Items.Add(exclusion);
            }

            foreach (string exclusion in GetContentExclusions())
            {
                listExcludeContent.Items.Add(exclusion);
            }
        }

        internal static List<string> GetInclusions()
        {
            List<string> list = new List<string>();

            using (DecoupledStorage settings = CodeRush.Options.GetStorage(OptionsCategory, OptionsPageName))
            {
                int includeCount = settings.ReadInt32(Summary, IncludeSummaryKey, -1);

                if (includeCount == -1)
                {
                    list.Add(".*(?i)[.]cs$");
                    list.Add(".*(?i)[.]vb$");
                }
                else
                {
                    for (int i = 0; i < includeCount; i++)
                    {
                        list.Add(settings.ReadString(IncludeKey, IncludeKey + i, true));
                    }
                }
            }

            return list;
        }

        internal static List<string> GetContentInclusions()
        {
            List<string> list = new List<string>();

            using (DecoupledStorage settings = CodeRush.Options.GetStorage(OptionsCategory, OptionsPageName))
            {
                int includeCount = settings.ReadInt32(Summary, IncludeContentSummaryKey, 0);

                for (int i = 0; i < includeCount; i++)
                {
                    list.Add(settings.ReadString(IncludeContentKey, IncludeContentKey + i, true));
                }
            }

            return list;
        }

        internal static List<string> GetExclusions()
        {
            List<string> list = new List<string>();

            using (DecoupledStorage settings = CodeRush.Options.GetStorage(OptionsCategory, OptionsPageName))
            {
                int excludeCount = settings.ReadInt32(Summary, ExcludeSummaryKey, -1);

                if (excludeCount == -1)
                {
                    list.Add(".*(?i)[.]designer[.]cs$");
                    list.Add(".*(?i)[.]designer[.]vb$");
                    list.Add("(?i)AssemblyInfo[.]cs$");
                }
                else
                {
                    for (int i = 0; i < excludeCount; i++)
                    {
                        list.Add(settings.ReadString(ExcludeKey, ExcludeKey + i, true));
                    }
                }                
            }

            return list;
        }

        internal static List<string> GetContentExclusions()
        {
            List<string> list = new List<string>();

            using (DecoupledStorage settings = CodeRush.Options.GetStorage(OptionsCategory, OptionsPageName))
            {
                int excludeCount = settings.ReadInt32(Summary, ExcludeContentSummaryKey, 0);

                for (int i = 0; i < excludeCount; i++)
                {
                    list.Add(settings.ReadString(ExcludeContentKey, ExcludeContentKey + i, true));
                }
            }
        
            return list;
        }

        private void btnAddInclusion_Click(object sender, EventArgs e)
        {
            AddListItem(listIncludeFiles, txtInclude);
        }

        private void btnAddContentInclusion_Click(object sender, EventArgs e)
        {
            AddListItem(listIncludeContent, txtIncludeContent);
        }

        private void btnAddExclusion_Click(object sender, EventArgs e)
        {
            AddListItem(listExcludeFiles, txtExclude);
        }

        private void btnAddContentExclusion_Click(object sender, EventArgs e)
        {
            AddListItem(listExcludeContent, txtExcludeContent);
        }

        private void btnRemoveInclusion_Click(object sender, EventArgs e)
        {
            RemoveListItem(listIncludeFiles);
        }        

        private void btnRemoveContentInclusion_Click(object sender, EventArgs e)
        {
            RemoveListItem(listIncludeContent);
        }                

        private void btnRemoveExclusion_Click(object sender, EventArgs e)
        {
            RemoveListItem(listExcludeFiles);
        }        

        private void btnRemoveContentExclusion_Click(object sender, EventArgs e)
        {
            RemoveListItem(listExcludeContent);
        }

        private static void AddListItem(ListBox listBox, TextBox textBox)
        {
            if (!textBox.Text.Equals(""))
                listBox.Items.Add(textBox.Text);
        }

        private static void RemoveListItem(ListBox listBox)
        {
            if (listBox.SelectedIndex >= 0)
                listBox.Items.Remove(listBox.Items[listBox.SelectedIndex]);
        }

        private void CodeIssueOptions_CommitChanges(object sender, CommitChangesEventArgs ea)
        {
            SaveOptions();
        }
    }
}