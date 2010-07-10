using System;
using Ninject.Modules;

namespace CR_BlockPainterPlus
{
    public class BlockPainterPlusModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IBlockPainterPlusSettings>().To<BlockPainterPlusSetings>().InSingletonScope();
            Bind<IBlockPaintingStrategy>().ToProvider<BlockPaintingStrategyProvider>().InTransientScope();
            Bind<IBlockPaintingStrategySettings>().ToProvider<BlockPaintingStrategySettingsProvider>().InTransientScope();
            Bind<IDetailedBlockMetaData>().To<DetailedBlockMetaData>().InSingletonScope(); 
            Bind<IGenericBlock>().To<GenericBlock>().InSingletonScope();
        }
    }
}
