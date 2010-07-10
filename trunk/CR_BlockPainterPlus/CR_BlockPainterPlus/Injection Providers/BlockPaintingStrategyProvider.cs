using Ninject.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CR_BlockPainterPlus
{
    public class BlockPaintingStrategyProvider : Provider<IBlockPaintingStrategy>
    {
        private static readonly Dictionary<string, IBlockPaintingStrategy> _blockPaintingStrategies = new Dictionary<string, IBlockPaintingStrategy>();
        
        protected override IBlockPaintingStrategy CreateInstance(IContext context)
        {
            IBlockPaintingStrategy result = null;

            if (_blockPaintingStrategies.Count == 0)
            {
                LoadBlockPaintingStrategies();
            }

            string blockTypeName = context.Parameters.Single( x => x.Name == ParameterNames.BlockTypeName).GetValue(context) as string;
            if (_blockPaintingStrategies.ContainsKey(blockTypeName)) 
            {
                result = _blockPaintingStrategies[blockTypeName];
            }

            return result;
        }

        public static void LoadBlockPaintingStrategies()
        {
            _blockPaintingStrategies.Clear();

            Type blockPaintingStrategyType = typeof(IBlockPaintingStrategy);

            var blockPaintingStrategyTypes =  from typeToEvaluate in Assembly.GetExecutingAssembly().GetTypes()
                                              where blockPaintingStrategyType.IsAssignableFrom(typeToEvaluate) 
                                              && typeToEvaluate.IsClass && !typeToEvaluate.IsAbstract
                                              select typeToEvaluate;

            foreach (Type typeToInstantiate in blockPaintingStrategyTypes)
            {
                IBlockPaintingStrategy strategy = Activator.CreateInstance(typeToInstantiate) as IBlockPaintingStrategy;
                _blockPaintingStrategies.Add(strategy.BlockTypeName, strategy);
            }
        }

    }
}
