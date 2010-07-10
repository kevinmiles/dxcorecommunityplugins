using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CR_BlockPainterPlus
{
    internal sealed class GenericBlock : IGenericBlock
    {
        public void AppendGenericTemplate(DevExpress.CodeRush.StructuralParser.AccessSpecifiedElement element, StringBuilder metaDataBuilder)
        {
            if (element.IsGeneric && element.GenericTemplate != null)
            {
                metaDataBuilder.Append(" ");
                metaDataBuilder.Append(element.GenericTemplate.ToString());
            }
        }

        public void AppendGenericTypes(DevExpress.CodeRush.StructuralParser.AccessSpecifiedElement element, StringBuilder metaDataBuilder)
        {
            if (element.IsGeneric)
            {
                metaDataBuilder.Append(element.GenericModifier);
            }
        }
    }
}
