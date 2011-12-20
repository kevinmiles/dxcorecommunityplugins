namespace CR_StyleCop.CodeIssues
{
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    public class AllTokensByTypeLocator
    {
        protected IEnumerable<CsToken> Flatten(CsToken token)
        {
            TypeToken typeToken = token as TypeToken;
            if (typeToken != null)
            {
                return typeToken.ChildTokens.SelectMany(child => Flatten(child));
            }

            return Enumerable.Repeat(token, 1);
        }
    }
}
