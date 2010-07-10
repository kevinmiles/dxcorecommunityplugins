using System;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core;
using Ninject;

namespace CR_BlockPainterPlus
{
    internal sealed class ForPaintingStrategy : BlockPaintingStrategy
    {
        public override string BlockTypeName
        {
            get { return typeof(For).Name; }
        }
    }
}
