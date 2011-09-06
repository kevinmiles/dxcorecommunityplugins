using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Menus;

namespace DevExpress.CodeRush.Core
{
	// TODO: CodeRush Devs: Let's move these methods into SourceRange...
	public static class SourceRangeExtensions
	{
		#region Overlaps
		/// <summary>
		/// Returns true if the specified range overlaps this range by at least one position. Adjacent ranges will return false.
		/// </summary>
		public static bool Overlaps(this SourceRange range1, SourceRange range2)
		{
			if (range1.Holds(range2.Top) || range1.Holds(range2.Bottom) || range2.Holds(range1.Top))
				return true;
			return false;
		}
		#endregion
		#region Holds
		/// <summary>
		/// Returns true if the specified SourcePoint is inside the specified range (but not at either of the ends of the SourceRange).
		/// </summary>
		public static bool Holds(this SourceRange range, SourcePoint testPoint)
		{
			return testPoint > range.Top && testPoint < range.Bottom;
		}
		#endregion
	}

	// TODO: CodeRush Devs: Let's move these methods into MenuBar...
	public static class MenuBarExtensions
	{
		#region FindItemByAction
		/// <summary>
		/// Gets the MenuControl inside this MenuBar corresponding to the specified Action.
		/// </summary>
		/// <param name="menuBar">The MenuBar to search.</param>
		/// <param name="action">The corresponding DXCore Action that the MenuControl was based upon.</param>
		public static IMenuControl FindItemByAction(this MenuBar menuBar, DevExpress.CodeRush.Core.Action action)
		{
			return menuBar.FindItemByActionName(action.ActionName);
		}
		#endregion
		#region FindItemByActionName
		/// <summary>
		/// Gets the MenuControl inside this MenuBar corresponding to the specified Action name.
		/// </summary>
		/// <param name="menuBar">The MenuBar to search.</param>
		/// <param name="actionName">The value assigned to the ActionName of the corresponding DXCore Action that the desired MenuControl was based upon.</param>
		public static MenuControl FindItemByActionName(this MenuBar menuBar, string actionName)
		{
			foreach (MenuControl item in menuBar)
				if (item.Tag == "DX." + actionName)
					return item;
			return null;
		}
		#endregion
	}
}
