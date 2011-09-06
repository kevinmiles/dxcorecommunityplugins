using System;
using System.Collections.Generic;
using System.Linq;

namespace DevExpress.CodeRush.Core
{
	// TODO: Delete and remove this class when MultiSelect services are integrated into the core, and rename "CodeRushPlaceholder" to "CodeRush" everywhere in this project.
	public class CodeRushPlaceholder
	{
		private static MultiSelectServices _MultiSelectServices = new MultiSelectServices();

		static CodeRushPlaceholder()
		{
			_MultiSelectServices.HookEvents();
			_MultiSelectServices.SetHighlightColor(OptMultiSelect.DefaultSelectionColor);
		}
		public static MultiSelectServices MultiSelect
		{
			get
			{
				return _MultiSelectServices;
			}
		}
	}
}
