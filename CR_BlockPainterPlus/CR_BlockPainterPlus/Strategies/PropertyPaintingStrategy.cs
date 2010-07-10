using System;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core;
using Ninject;

namespace CR_BlockPainterPlus
{
    internal sealed class PropertyPaintingStrategy : BlockPaintingStrategy
    {
        public override string BlockTypeName
        {
            get { return typeof(Property).Name; }
        }
    }
}
