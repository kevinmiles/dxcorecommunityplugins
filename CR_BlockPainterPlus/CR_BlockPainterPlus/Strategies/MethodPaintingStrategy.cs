using System;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core;
using Ninject;
using System.Text;

namespace CR_BlockPainterPlus
{
    internal sealed class MethodPaintingStrategy : BlockPaintingStrategy
    {
        [Inject()]
        public IGenericBlock GenericBlock { private get; set; }

        

        public override string BlockTypeName
        {
            get { return typeof(Method).Name; }
        }

        public override SourcePoint PaintBlock(DelimiterCapableBlock block, DecorateLanguageElementEventArgs args, SourcePoint startPointToPaint, bool multipleBlocksOnLine)
        {
            SourcePoint result = startPointToPaint;
            if (Settings.Enabled && MeetsLengthRquirement(block))
            {
                string customMetaString = GetMethodMetaString(block as Method);

                args.AddForegroundAdornment( new BlockMetaDataDocumentAdornment(startPointToPaint, Settings, customMetaString));

                result = startPointToPaint.OffsetPoint(0, customMetaString.Length);
            }

            return result;
        }

        private string GetMethodMetaString(Method method)
        {
            StringBuilder methodMetaBuilder = new StringBuilder();

            methodMetaBuilder.Append(method.Name);

            GenericBlock.AppendGenericTypes(method, methodMetaBuilder);

            methodMetaBuilder.Append(Words.OpenParen);

            for (int i = 0; i < method.Parameters.Count; i++)
            {
                Param parameter = method.Parameters[i] as Param;

                if (parameter.IsOutParam)
                {
                    methodMetaBuilder.Append(Words.OutParam);
                }

                if (parameter.IsReferenceParam)
                {
                    methodMetaBuilder.Append(Words.RefParam);
                }

                string simpleTypeName = CodeRush.Language.GetSimpleTypeName(parameter.GetTypeName());
                methodMetaBuilder.Append(simpleTypeName);


                if (i < method.Parameters.Count - 1)
                {
                    methodMetaBuilder.Append(Words.CommaDelimiter);
                }
            }
            methodMetaBuilder.Append(Words.CloseParen);

            GenericBlock.AppendGenericTemplate(method, methodMetaBuilder);

            return methodMetaBuilder.ToString();
        }
    }
}
