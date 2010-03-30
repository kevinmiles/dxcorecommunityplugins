using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DevExpress.CodeRush.Core;


namespace CR_Initials
{
    public partial class OptInitials : DevExpress.CodeRush.Core.OptionsPage
    {     

        public static string GetCategory()
        {
            return @"Developer Initials";
        }

        public static string GetPageName()
        {
            return @"Initials";
        }

        public static DecoupledStorage Storage
        {
            get
            {
                return DevExpress.CodeRush.Core.CodeRush.Options.GetStorage(GetCategory(), GetPageName());
            }
        }

        public new static void Show()
        {
            DevExpress.CodeRush.Core.CodeRush.Command.Execute("Options", FullPath);
        }

        public static string FullPath
        {
            get
            {
                return GetCategory() + "\\" + GetPageName();
            }
        }


        public override string Category
        {
            get
            {
                return OptInitials.GetCategory();
            }
        }

        public override string PageName
        {
            get
            {
                return OptInitials.GetPageName();
            }
        }

        public OptInitials()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Save the current settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ea"></param>
        /// <developer>Paul Mrozowski</developer>
        /// <created>08/07/2006</created>
        private void OptInitials_CommitChanges(object sender, CommitChangesEventArgs ea)
        {
            ea.Storage.WriteString(CR_Initials.STR_Preferences, CR_Initials.STR_DevName, this.txtDevName.Text);
            ea.Storage.WriteString(CR_Initials.STR_Preferences, CR_Initials.STR_DevInitials, this.txtDevInitials.Text);
            ea.Storage.WriteBoolean(CR_Initials.STR_Preferences, CR_Initials.STR_FullNameComment, this.chkFullname.Checked);
            ea.Storage.WriteString(CR_Initials.STR_Preferences, CR_Initials.STR_DateFormat, this.txtDateFormat.Text);
        }

        /// <summary>
        /// Set things up on our page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ea"></param>
        /// <developer>Paul Mrozowski</developer>
        /// <created>08/07/2006</created>
        private void OptInitials_PreparePage(object sender, OptionsPageStorageEventArgs ea)
        {
            this.txtDevName.Text = ea.Storage.ReadString(CR_Initials.STR_Preferences, CR_Initials.STR_DevName);
            this.txtDevInitials.Text = ea.Storage.ReadString(CR_Initials.STR_Preferences, CR_Initials.STR_DevInitials);
            this.chkFullname.Checked = ea.Storage.ReadBoolean(CR_Initials.STR_Preferences, CR_Initials.STR_FullNameComment);
            this.txtDateFormat.Text = ea.Storage.ReadString(CR_Initials.STR_Preferences, CR_Initials.STR_DateFormat);

        }
    }
}

