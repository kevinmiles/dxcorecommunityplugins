using System;
using System.Linq;
using DevExpress.CodeRush.Core;
using System.Reflection;
using System.Collections.Generic;

namespace CR_BlockPainterPlus
{

    [UserLevel(UserLevel.NewUser)]
    public partial class BlockPainterPlusOptions : OptionsPage
    {
        private Dictionary<string, IBlockPaintingStrategySettings> _settingsCatalog = new Dictionary<string, IBlockPaintingStrategySettings>();

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
            return @"Editor\Painting";
        }
        #endregion
        #region GetPageName
        public static string GetPageName()
        {
            return @"Block Painter";
        }
        #endregion

        #region Options Page Events

        private void BlockPainterPlusOptions_PreparePage(object sender, OptionsPageStorageEventArgs ea)
        {

        }

        private void BlockPainterPlusOptions_CommitChanges(object sender, CommitChangesEventArgs ea)
        {
            IList<IBlockPaintingStrategySettings> settingsList = bindingSource.DataSource as IList<IBlockPaintingStrategySettings>;

            foreach (IBlockPaintingStrategySettings settings in settingsList)
            {
                ea.Storage.WriteBoolean(settings.BlockTypeName, SettingNames.Enabled, settings.Enabled);
                ea.Storage.WriteBoolean(settings.BlockTypeName, SettingNames.ShowDetailedBlockMetaData, settings.ShowDetailedBlockMetaData);
                ea.Storage.WriteInt32(settings.BlockTypeName, SettingNames.MinimumBlockSize, settings.MinimumBlockSize);
                ea.Storage.WriteInt32(settings.BlockTypeName, SettingNames.PrefixAlpha, settings.PrefixAlpha);
                ea.Storage.WriteInt32(settings.BlockTypeName, SettingNames.PrefixBlue, settings.PrefixBlue);
                ea.Storage.WriteInt32(settings.BlockTypeName, SettingNames.PrefixGreen, settings.PrefixGreen);
                ea.Storage.WriteInt32(settings.BlockTypeName, SettingNames.PrefixRed, settings.PrefixRed);
                ea.Storage.WriteString(settings.BlockTypeName, SettingNames.PrefixText, settings.PrefixText);
                ea.Storage.WriteInt32(settings.BlockTypeName, SettingNames.BlockMetaDataAlpha, settings.BlockMetaDataAlpha);
                ea.Storage.WriteInt32(settings.BlockTypeName, SettingNames.BlockMetaDataBlue, settings.BlockMetaDataBlue);
                ea.Storage.WriteInt32(settings.BlockTypeName, SettingNames.BlockMetaDataGreen, settings.BlockMetaDataGreen);
                ea.Storage.WriteInt32(settings.BlockTypeName, SettingNames.BlockMetaDataRed, settings.BlockMetaDataRed);
            }
        }

        private void BlockPainterPlusOptions_CancelChanges(object sender, OptionsPageStorageEventArgs ea)
        {

        }
        #endregion


        private void BlockPainterPlusOptions_Load(object sender, EventArgs e)
        {
            IList<IBlockPaintingStrategySettings> settingsList = BlockPaintingStrategySettingsProvider.BlockPaintingStrategySettings.Values.ToList();
            settingsList = settingsList.OrderBy(x => x.BlockTypeName).ToList();
            bindingSource.DataSource = settingsList;
        }

        private void prefixColorSwatch_ColorChange(object sender, EventArgs e)
        {
            IBlockPaintingStrategySettings settings = bindingSource.Current as IBlockPaintingStrategySettings;
            if (settings != null)
            {
                settings.PrefixColor = prefixColorSwatch.Color;
            }
        }

        private void blockMetaColorSwatch_ColorChange(object sender, EventArgs e)
        {
            IBlockPaintingStrategySettings settings = bindingSource.Current as IBlockPaintingStrategySettings;
            if (settings != null)
            {
                settings.BlockMetaColor = blockMetaColorSwatch.Color;
            }
        }

        private void listBoxControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            IBlockPaintingStrategySettings settings = bindingSource.Current as IBlockPaintingStrategySettings;
            if (settings != null)
            {
                blockMetaColorSwatch.Color = settings.BlockMetaColor;
                prefixColorSwatch.Color = settings.PrefixColor;
            }
        }

        private void prefixAlphaTrackEdit_EditValueChanged(object sender, EventArgs e)
        {
            prefixColorSwatch.Update();
        }

        private void blockMetaAlphaEdit_EditValueChanged(object sender, EventArgs e)
        {
            //blockMetaColorSwatch.
        }

        private void BlockPainterPlusOptions_RestoreDefaults(object sender, OptionsPageEventArgs ea)
        {
            Storage.DeleteAll();
            Storage.UpdateStorage();

            BlockPaintingStrategySettingsProvider.ReloadSettings();
            bindingSource.DataSource = BlockPaintingStrategySettingsProvider.BlockPaintingStrategySettings.Values.ToList();
        }

        private void hyperLinkEdit1_OpenLink(object sender, DevExpress.DXCore.Controls.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:martin@shepherdoaks.com");
        }

        private void hyperLinkEdit2_OpenLink(object sender, DevExpress.DXCore.Controls.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=XF4C6KU5EPLYN");
        }
    }
}