using System;
using DevExpress.CodeRush.Core;

namespace Refactor_Comments
{
	public static class StringExtensions
	{
		public static bool SupportsMultiLineComments(this string languageId)
		{
			if (languageId == null)
			{
				throw new ArgumentNullException("languageId");
			}
			return CodeRush.Language.SupportsMultiLineComments(languageId) || languageId == "JavaScript";
		}
	}
}
