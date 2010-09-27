//using System;
//using System.ComponentModel;
//using System.Drawing;
//using System.Linq;
//using System.Windows.Forms;
//using DevExpress.CodeRush.Core;
//using System.Collections.Generic;

//namespace DX_StudioSizer
//{
//    [UserLevel(UserLevel.NewUser)]
//    public partial class Options1 : OptionsPage
//    {
//        private static string[] mDefaultResolutions =
//            new string[] { "1600x1200", "1600x1050", "1280x1024", "1280x720", "1024x768", "800x600" };
//        private const string SECTION_DX_Sizer = "DXStudioSizer";
//        private const string Setting_Resolution = "Resolutions";
//        // DXCore-generated code...
//        #region Initialize
//        protected override void Initialize()
//        {
//            base.Initialize();

//            //
//            // TODO: Add your initialization code here.
//            //
//        }
//        #endregion

//        #region GetCategory
//        public static string GetCategory()
//        {
//            return @"IDE";
//        }
//        #endregion
//        #region GetPageName
//        public static string GetPageName()
//        {
//            return @"Studio Sizer";
//        }
//        #endregion
//        #region Utils
//        private void AddResolution(decimal Width, decimal Height)
//        {
//            lstResolutions.Items.Add(String.Format("{0}x{1}", Width, Height));
//        }
//        private void AddResolution(string Resolution)
//        {
//            lstResolutions.Items.Add(Resolution);
//        }

//        #endregion
//        #region Events
//        private void cmdAdd_Click(object sender, EventArgs e)
//        {
//            NumericUpDown numWidthVariable = numWidth;
//            NumericUpDown numHeightVariable = numHeight;
//            AddResolution(numWidthVariable.Value, numHeightVariable.Value);
//        }

//        private void cmdRemove_Click(object sender, EventArgs e)
//        {
//            lstResolutions.Items.Remove(lstResolutions.SelectedIndex);
//        }
//        #endregion

//        private void Options1_RestoreDefaults(object sender, OptionsPageEventArgs ea)
//        {
//            lstResolutions.Items.Clear();
//            mDefaultResolutions.ToList<string>().ForEach(S => AddResolution(S));
//        }

//        private void Options1_PreparePage(object sender, OptionsPageStorageEventArgs ea)
//        {
//            lstResolutions.Items.Clear();
//            List<ScreenSize> Resolutions = GetResolutions(ea.Storage);
//            foreach (ScreenSize Resolution in Resolutions)
//            {
//                lstResolutions.Items.Add(Resolution);
//            }
//        }
//        public static List<ScreenSize> GetResolutions(DecoupledStorage storage)
//        {
//            string[] ResolutionStrings = storage.ReadStrings(SECTION_DX_Sizer, Setting_Resolution, mDefaultResolutions);
//            return ResolutionStrings.ToList().ConvertAll<ScreenSize>(I => new ScreenSize(I));
//        }
//        
//        private void Options1_CommitChanges(object sender, CommitChangesEventArgs ea)
//        {
//            ea.Storage.WriteStrings(SECTION_DX_Sizer,
//                                    Setting_Resolution,
//                                    lstResolutions.Items.OfType<string>().ToArray());
//        }
//    }
//}