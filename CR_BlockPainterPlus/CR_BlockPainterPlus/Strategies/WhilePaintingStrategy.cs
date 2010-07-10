using System;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;
using Ninject;

namespace CR_BlockPainterPlus
{
    internal sealed class WhilePaintingStrategy : BlockPaintingStrategy
    {
        public override string BlockTypeName
        {
            get { return typeof(While).Name; }
        }
    }
}
