using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.CodeRush.StructuralParser;

namespace CR_BlockPainterPlus
{
    internal sealed class UsingPaintingStrategy : BlockPaintingStrategy
    {
        public override string BlockTypeName
        {
            get { return typeof(UsingStatement).Name; }
        }
    }
}
