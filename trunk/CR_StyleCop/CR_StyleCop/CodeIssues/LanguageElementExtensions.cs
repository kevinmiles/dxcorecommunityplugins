namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.StructuralParser;

    internal static class LanguageElementExtensions
    {
        private static LanguageElementType[] keywords = new LanguageElementType[]
                {
                    LanguageElementType.Checked,
                    LanguageElementType.Unchecked,
                    LanguageElementType.Lock,
                    LanguageElementType.Try,
                    LanguageElementType.Finally
                };

        public static SourceRange GetKeywordRange(this LanguageElement element)
        {
            if (keywords.Contains(element.ElementType))
            {
                return new SourceRange(element.RecoveredRange.Start, element.RecoveredRange.Start.OffsetPoint(0, element.ElementType.ToString().Length));
            }
            else if (element.ElementType == LanguageElementType.UnsafeStatement)
            {
                return new SourceRange(element.RecoveredRange.Start, element.RecoveredRange.Start.OffsetPoint(0, 6));
            }
            else
            {
                return element.NameRange;
            }
        }
    }
}

