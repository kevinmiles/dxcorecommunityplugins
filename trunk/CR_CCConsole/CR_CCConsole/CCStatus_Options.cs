using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using System.Collections.Generic;

namespace CR_CCConsole
{
    [UserLevel(UserLevel.NewUser)]
    public partial class CCStatus_Options : OptionsPage
    {
        // DXCore-generated code...
        #region Initialize
        protected override void Initialize()
        {
            base.Initialize();
        }
        #endregion

        #region GetCategory
        public static string GetCategory()
        {
            return @"IDE";
        }
        #endregion
        #region GetPageName
        public static string GetPageName()
        {
            return @"CruiseControl.Net Options";
        }
        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            var server = CCStatusConfig.GetServer(tbBuildServer.Text);
            var projects = server.GetProjects();
            clbProjects.Items.Clear();
            foreach (var project in projects)
            {
                clbProjects.Items.Add(project.Name);
            }
        }

        private void CCStatus_Options_PreparePage(object sender, OptionsPageStorageEventArgs ea)
        {
            tbBuildServer.Text = CCStatusConfig.Server;
            var projects = CCStatusConfig.ProjectList;
            var selected = CCStatusConfig.SelectedProjects;
            foreach (var proj in projects)
            {
                clbProjects.Items.Add(proj, selected.Contains(proj));
            }
            nudUpdateInterval.Value = CCStatusConfig.UpdateInterval;
            cbNotifyOnFailure.Checked = CCStatusConfig.NotifyOnFailure;
        }

        private void CCStatus_Options_CommitChanges(object sender, CommitChangesEventArgs ea)
        {
            CCStatusConfig.Server = tbBuildServer.Text;
            CCStatusConfig.ProjectList = clbProjects.Items.Cast<string>().ToList();
            CCStatusConfig.SelectedProjects = clbProjects.CheckedItems.Cast<string>().ToList();
            CCStatusConfig.UpdateInterval = (int)nudUpdateInterval.Value;
            CCStatusConfig.NotifyOnFailure = cbNotifyOnFailure.Checked;
        }

        private void btnNotifyTest_Click(object sender, EventArgs e)
        {
            BigFeedback feedback = new BigFeedback();
            feedback.Text = "Project 1 is now FAILING!";
            feedback.Show();
        }
    }

    internal static class CCStatusConfig
    {
        private const string STR_CurrentView = "CurrentView";
        private const string STR_FailingProjects = "FailingProjects";
        private static DecoupledStorage _storage;
        private const string STR_SelectedProjects = "SelectedProjects";
        private const string STR_ProjectList = "ProjectList";
        private const string STR_UpdateInterval = "UpdateInterval";
        private const string STR_Server = "Server";
        private const string STR_NotifyOnFailure = "NotifyOnFailure";
        private const string STR_PassingProjects = "PassingProjects";

        private static DecoupledStorage Storage
        {
            get { return _storage ?? CCStatus_Options.Storage; }
        }

        public static CCServer GetServer()
        {
            return GetServer(Server);
        }

        public static CCServer GetServer(string serverName)
        {
            return new CCServer(serverName);
        }
        
        public static List<CCProject> GetSelectedProjects()
        {
            var server = GetServer();
            var projects = server.GetProjects().Where(p => SelectedProjects.Contains(p.Name)).ToList();
            var failing = projects.Where(p => p.LastBuildStatus == CCBuildStatus.Failure);
            FailingProjects = PassingProjects.Where(p => failing.Any(f => f.Name == p)).ToList();
            PassingProjects = projects.Where(p => p.LastBuildStatus == CCBuildStatus.Success).Select(p => p.Name).ToList();
            return projects;
        }

        public static string Server
        {
            get 
            { 
                using (var storage = CCStatus_Options.Storage)
                {
                    return storage.ReadString("Settings", STR_Server); 
                }
                
            }
            set 
            {
                using (var storage = CCStatus_Options.Storage)
                {
                    storage.WriteString("Settings", STR_Server, value);
                }
            }
        }

        public static int UpdateInterval
        {
            get 
            {
                using (var storage = CCStatus_Options.Storage)
                {
                    return storage.ReadInt32("Settings", STR_UpdateInterval, 1);
                }
            }
            set 
            {
                using (var storage = CCStatus_Options.Storage)
                {
                    storage.WriteInt32("Settings", STR_UpdateInterval, value);
                }
            }
        }

        public static List<string> ProjectList
        {
            get 
            {
                using (var storage = CCStatus_Options.Storage)
                {
                    return storage.ReadStrings("Settings", STR_ProjectList).ToList();
                }
            }
            set 
            {
                using (var storage = CCStatus_Options.Storage)
                {
                    storage.WriteStrings("Settings", STR_ProjectList, value.ToArray());
                }
            }
        }

        public static List<string> SelectedProjects
        {
            get 
            {
                using (var storage = CCStatus_Options.Storage)
                {
                    return storage.ReadStrings("Settings", STR_SelectedProjects).ToList();
                }
            }
            set
            {
                using (var storage = CCStatus_Options.Storage)
                {
                    storage.WriteStrings("Settings", STR_SelectedProjects, value.ToArray());
                }
            }
        }

        public static bool NotifyOnFailure
        {
            get
            {
                using (var storage = CCStatus_Options.Storage)
                {
                    return storage.ReadBoolean("Settings", STR_NotifyOnFailure, false);
                }
            }
            set
            {
                using (var storage = CCStatus_Options.Storage)
                {
                    storage.WriteBoolean("Settings", STR_NotifyOnFailure, value);
                }
            }

        }

        public static List<string> FailingProjects
        {
            get
            {
                using (var storage = CCStatus_Options.Storage)
                {
                    return storage.ReadStrings("Settings", STR_FailingProjects).ToList();
                }
            }
            set
            {
            	using (var storage = CCStatus_Options.Storage)
                {
                    storage.WriteStrings("Settings", STR_FailingProjects, value.ToArray());
                }
            }
        }

        public static List<string> PassingProjects
        {
            get 
            {
                using (var storage = CCStatus_Options.Storage)
                {
                    return storage.ReadStrings("Settings", STR_PassingProjects).ToList();
                }
            }
            set 
            {
                using (var storage = CCStatus_Options.Storage)
                {
                    storage.WriteStrings("Settings", STR_PassingProjects, value.ToArray());
                }
            }
        }
        public static View CurrentView
        {
            get 
            {
                using (var storage = CCStatus_Options.Storage)
                {
                    return (View)storage.ReadInt32("Settings", STR_CurrentView, (int)View.LargeIcon);
                }
            }
            set 
            {
                using (var storage = CCStatus_Options.Storage)
                {
                    storage.WriteInt32("Settings", STR_CurrentView, (int)value);
                }
            }
        }
    }
}