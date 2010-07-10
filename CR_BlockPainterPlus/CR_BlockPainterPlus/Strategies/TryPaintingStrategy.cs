﻿using System;
using DevExpress.CodeRush.StructuralParser;
using Ninject;
using DevExpress.CodeRush.Core;

namespace CR_BlockPainterPlus
{
    internal sealed class TryPaintingStrategy : BlockPaintingStrategy
    {
        public override string BlockTypeName
        {
            get { return typeof(Try).Name; }
        }
    }
}
