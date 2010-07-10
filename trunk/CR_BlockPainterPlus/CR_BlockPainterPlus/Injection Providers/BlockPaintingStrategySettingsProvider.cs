using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DevExpress.CodeRush.Core;
using Ninject.Activation;
using System.Drawing;
using System.IO;

namespace CR_BlockPainterPlus
{
    public class BlockPaintingStrategySettingsProvider : Provider<IBlockPaintingStrategySettings>
    {
        public static Dictionary<string, IBlockPaintingStrategySettings> BlockPaintingStrategySettings { get; private set; }

        #region Constants
        private const string StrategySettingsSectionName = "blockPainterStrategySettings";
        #endregion

        #region Constructor
        static BlockPaintingStrategySettingsProvider()
        {
            BlockPaintingStrategySettings = LoadSettings();
        }
        #endregion

        protected override IBlockPaintingStrategySettings CreateInstance(IContext context)
        {
            IBlockPaintingStrategySettings result = null;

            string blockTypeName = context.Parameters.Single(x => x.Name == ParameterNames.BlockTypeName).GetValue(context) as string;
            if (BlockPaintingStrategySettings.ContainsKey(blockTypeName))
            {
                result = BlockPaintingStrategySettings[blockTypeName];
            }
            else
            {
                throw new InvalidOperationException(
                    String.Format(
                    "An attempt was made to load settings for block type '{0}' but the BlockPaintingStrategySettingsProvider didn't have an entry for it.", blockTypeName));
            }

            return result;
        }

        public static void ReloadSettings()
        {
            BlockPaintingStrategySettings = LoadSettings();
        }

        private static Dictionary<string, IBlockPaintingStrategySettings> LoadSettings()
        {
            Dictionary<string, IBlockPaintingStrategySettings> result = new Dictionary<string, IBlockPaintingStrategySettings>();

            using (DecoupledStorage storage = new DecoupledStorage( BlockPainterPlusOptions.GetCategory(), BlockPainterPlusOptions.GetPageName()))
            {
                Type blockPaintingStrategyType = typeof(IBlockPaintingStrategy);
                var blockPaintingStrategyTypes = from typeToEvaluate in Assembly.GetExecutingAssembly().GetTypes()
                                                 where blockPaintingStrategyType.IsAssignableFrom(typeToEvaluate) 
                                                 && typeToEvaluate.IsClass && !typeToEvaluate.IsAbstract
                                                 select typeToEvaluate;

                foreach (Type strategyType in blockPaintingStrategyTypes)
                {
                    IBlockPaintingStrategySettings settings = LoadSettingsForStrategy(strategyType, storage);
                    result.Add(settings.BlockTypeName, settings);
                }
            }

            return result;
        }

        private static IBlockPaintingStrategySettings LoadSettingsForStrategy(Type strategyType, DecoupledStorage storage)
        {
            object strategyInstance = Activator.CreateInstance(strategyType);
            string blockTypeName = strategyInstance.GetType().GetProperty("BlockTypeName").GetValue(strategyInstance, null) as string;
            IBlockPaintingStrategySettings result = new BlockPaintingStrategySettings(blockTypeName);

            result.Enabled = storage.ReadBoolean(result.BlockTypeName, SettingNames.Enabled, true);
            result.ShowDetailedBlockMetaData = storage.ReadBoolean(result.BlockTypeName, SettingNames.ShowDetailedBlockMetaData, DefaultValues.ShowDetailedBlockMetaData);
            result.MinimumBlockSize = (byte)storage.ReadInt32(result.BlockTypeName, SettingNames.MinimumBlockSize, 0);

            result.BlockMetaDataAlpha = (byte)storage.ReadInt32(result.BlockTypeName, SettingNames.BlockMetaDataAlpha, DefaultValues.BlockMetaDataAlpha);
            result.BlockMetaDataRed = (byte)storage.ReadInt32(result.BlockTypeName, SettingNames.BlockMetaDataRed, DefaultValues.BlockMetaDataRed);
            result.BlockMetaDataGreen = (byte)storage.ReadInt32(result.BlockTypeName, SettingNames.BlockMetaDataGreen, DefaultValues.BlockMetaDataGreen);
            result.BlockMetaDataBlue = (byte)storage.ReadInt32(result.BlockTypeName, SettingNames.BlockMetaDataBlue, DefaultValues.BlockMetaDataBlue);

            result.PrefixAlpha = (byte)storage.ReadInt32(result.BlockTypeName, SettingNames.PrefixAlpha, DefaultValues.PrefixAlpha);
            result.PrefixRed = (byte)storage.ReadInt32(result.BlockTypeName, SettingNames.PrefixRed, DefaultValues.PrefixRed);
            result.PrefixGreen = (byte)storage.ReadInt32(result.BlockTypeName, SettingNames.PrefixGreen, DefaultValues.PrefixGreen);
            result.PrefixBlue = (byte)storage.ReadInt32(result.BlockTypeName, SettingNames.PrefixBlue, DefaultValues.PrefixBlue);
            result.PrefixText = storage.ReadString(result.BlockTypeName, SettingNames.PrefixText, DefaultValues.PrefixText);

            return result;
        }
    }
}
