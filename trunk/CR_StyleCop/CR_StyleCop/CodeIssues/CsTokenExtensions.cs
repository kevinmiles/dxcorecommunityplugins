namespace CR_StyleCop.CodeIssues
{
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal static class CsTokenExtensions
    {
        public static IEnumerable<CsToken> Flatten(this IEnumerable<CsToken> tokens)
        {
            return tokens.SelectMany(token => Flatten(token));
        }

        private static IEnumerable<CsToken> Flatten(CsToken token)
        {
            var typeToken = token as TypeToken;
            if (typeToken != null)
            {
                return typeToken.ChildTokens.SelectMany(child => Flatten(child));
            }

            var attributeToken = token as Attribute;
            if (attributeToken != null)
            {
                return attributeToken.ChildTokens.SelectMany(child => Flatten(child));
            }

            var xmlHeader = token as XmlHeader;
            if (xmlHeader != null)
            {
                return xmlHeader.ChildTokens.SelectMany(child => Flatten(child));
            }

            return Enumerable.Repeat(token, 1);
        }
    }
}
