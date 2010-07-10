using System;
using System.Collections.Generic;
using System.Diagnostics;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using Ninject;
using Ninject.Parameters;
using System.Reflection;

namespace CR_BlockPainterPlus
{
    public partial class BlockPainterPlugIn : StandardPlugIn
    {
        private readonly string _optionsPageFullName =
            String.Format("{0}\\{1}", BlockPainterPlusOptions.GetCategory(), BlockPainterPlusOptions.GetPageName());

        private readonly List<int> _processedLines = new List<int>();
        private readonly IKernel _kernel = new StandardKernel();
        private BlockPainterPlusModule _blockPainterPlusModule;

        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            _blockPainterPlusModule = new BlockPainterPlusModule();
            _kernel.Load(_blockPainterPlusModule);

            base.InitializePlugIn();
        }
        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {

            base.FinalizePlugIn();
        }
        #endregion

        #region Plugin Events


        private void BlockPainterPlugIn_DecorateLanguageElement(object sender, DecorateLanguageElementEventArgs args)
        {
            DelimiterCapableBlock block = args.LanguageElement as DelimiterCapableBlock;

            if (block != null && block.HasDelimitedBlock && !_processedLines.Contains(block.EndLine))
            {
                IList<DelimiterCapableBlock> blocksOnLine = GetBlocksOnLine(block);
                SourcePoint startPointToPaint = new SourcePoint(block.EndLine, CodeRush.TextViews.Active.LengthOfLine(block.EndLine) + 1);

                DelimiterCapableBlock lastBlockOnLine = blocksOnLine[blocksOnLine.Count - 1];

                for (int i = 0; i < blocksOnLine.Count; i++)
                {
                    block = blocksOnLine[i];

                    IParameter blockTypeNameParameter = new Ninject.Parameters.Parameter(ParameterNames.BlockTypeName, block.GetType().Name, true);
                    IBlockPaintingStrategy strategy = _kernel.Get<IBlockPaintingStrategy>(blockTypeNameParameter);

                    if (i == 0)
                    {
                        startPointToPaint = strategy.PaintPrefix(block, args, startPointToPaint);
                    }

                    startPointToPaint = strategy.PaintBlock(block, args, startPointToPaint, blocksOnLine.Count > 1);
                }

                _processedLines.Add(block.EndLine);
            }
        }

        private void BlockPainterPlugIn_OptionsChanged(OptionsChangedEventArgs ea)
        {
            if (ea.OptionsPages.Contains(_optionsPageFullName))
            {
                BlockPaintingStrategyProvider.LoadBlockPaintingStrategies();
            }
        }

        #endregion

        

        private static IList<DelimiterCapableBlock> GetBlocksOnLine(DelimiterCapableBlock firstBlockOnLine)
        {
            IList<DelimiterCapableBlock> blocksOnLine = new List<DelimiterCapableBlock>();
            /* we always want to add the first block :-P */
            blocksOnLine.Add(firstBlockOnLine);

            /* iterate through every block after our first block until they are
             * not on the same line. the idea is to get every delimiter capable block 
             * that ends on the same line.*/

            LanguageElement siblingElement = firstBlockOnLine.NextCodeSibling;
            while (siblingElement != null)
            {
                if (siblingElement.EndLine != firstBlockOnLine.EndLine) break;

                if (siblingElement is DelimiterCapableBlock)
                {
                    DelimiterCapableBlock siblingBlock = siblingElement as DelimiterCapableBlock;
                    if (siblingBlock.HasDelimitedBlock)
                    {
                        blocksOnLine.Add(siblingBlock);
                    }
                }
                /* now assign the next element to the next code sibling. this prevents infinite looping.*/
                siblingElement = siblingElement.NextCodeSibling;
            }
            return blocksOnLine;
        }

        private void BlockPainterPlugIn_EditorScrolled(EditorScrolledEventArgs ea)
        {
            _processedLines.Clear();
        }

        private void BlockPainterPlugIn_EditorPaintForeground(EditorPaintEventArgs ea)
        {
            _processedLines.Clear();
        }

        private void BlockPainterPlugIn_EditorPaintBackground(EditorPaintEventArgs ea)
        {
            _processedLines.Clear();
        }


    }
}