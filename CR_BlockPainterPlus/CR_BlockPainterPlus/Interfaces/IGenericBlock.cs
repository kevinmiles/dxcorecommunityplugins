using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.CodeRush.StructuralParser;

namespace CR_BlockPainterPlus
{
    public interface IGenericBlock
    {
        void AppendGenericTemplate(AccessSpecifiedElement element, StringBuilder metaBuilder);
        void AppendGenericTypes(AccessSpecifiedElement element, StringBuilder metaBuilder);
    }
}
