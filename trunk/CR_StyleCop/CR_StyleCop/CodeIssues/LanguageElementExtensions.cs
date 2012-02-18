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
                    LanguageElementType.Finally,
                    LanguageElementType.If,
                    LanguageElementType.For,
                    LanguageElementType.ForEach,
                    LanguageElementType.While,
                    LanguageElementType.Do,
                };

        public static SourceRange GetKeywordRange(this LanguageElement element)
        {
            return new SourceRange(element.RecoveredRange.Start, element.RecoveredRange.Start.OffsetPoint(0, element.GetKeywordLength()));
        }

        public static string GetKeyword(this LanguageElement element)
        {
            return element.ElementType.ToString().Replace("Statement", string.Empty).ToLowerInvariant();
        }

        private static int GetKeywordLength(this LanguageElement element)
        {
            if (keywords.Contains(element.ElementType))
            {
                return element.ElementType.ToString().Length;
            }
            else if (element.ElementType == LanguageElementType.UnsafeStatement)
            {
                return 6;
            }
            else if (element.ElementType == LanguageElementType.UsingStatement)
            {
                return 5;
            }
            else
            {
                return element.Name.Length;
            }
        }
    }
}

