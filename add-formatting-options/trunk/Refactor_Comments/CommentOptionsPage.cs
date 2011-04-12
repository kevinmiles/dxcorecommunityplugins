using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;

namespace Refactor_Comments
{
	[UserLevel(UserLevel.NewUser)]
	public partial class CommentOptionsPage : OptionsPage
	{
		public static string GetCategory()
		{
			return @"Editor\Code Style";
		}

		public static string GetPageName()
		{
			return @"Comment Formatting";
		}

		protected override void Initialize()
		{
			base.Initialize();

			//
			// TODO: Add your initialization code here.
			//
		}
	}
}