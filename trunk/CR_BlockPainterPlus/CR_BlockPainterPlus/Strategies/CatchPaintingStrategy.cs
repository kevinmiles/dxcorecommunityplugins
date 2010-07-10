using System;
using DevExpress.CodeRush.StructuralParser;
using Ninject;
using DevExpress.CodeRush.Core;

namespace CR_BlockPainterPlus
{
    internal sealed class CatchPaintingStrategy : BlockPaintingStrategy
    {
        public override string BlockTypeName
        {
            get { return typeof(Catch).Name; }
        }
    }
}
