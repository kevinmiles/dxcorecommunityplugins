using System;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core;
using Ninject;
using System.Text;

namespace CR_BlockPainterPlus
{
    internal sealed class InterfacePaintingStrategy : BlockPaintingStrategy
    {
        [Inject()]
        public IGenericBlock GenericBlock { private get; set; }

        public override string BlockTypeName
        {
            get { return typeof(Interface).Name; }
        }

        public override SourcePoint PaintBlock(DelimiterCapableBlock block, DecorateLanguageElementEventArgs args, SourcePoint startPointToPaint, bool multipleBlocksOnLine)
        {
            SourcePoint result = startPointToPaint;
            if (Settings.Enabled && MeetsLengthRquirement(block))
            {
                StringBuilder customMetaBuilder = new StringBuilder();
                customMetaBuilder.Append(String.Format("{0} '{1}'", Settings.BlockTypeName, (block as Class).Name));
                GenericBlock.AppendGenericTypes(block as AccessSpecifiedElement, customMetaBuilder);
                GenericBlock.AppendGenericTemplate(block as AccessSpecifiedElement, customMetaBuilder);

                args.AddForegroundAdornment(
                    new BlockMetaDataDocumentAdornment(startPointToPaint, Settings, customMetaBuilder.ToString()));

                result = startPointToPaint.OffsetPoint(0, customMetaBuilder.Length);
            }
            return result;
        }
    }
}
