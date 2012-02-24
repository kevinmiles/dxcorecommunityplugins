using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.CodeRush.Common;

namespace CR_FavoriteViews
{
	public class DocView
	{
		private const string STR_FileName = "FileName";
		private const string STR_SelectionActive = "SelectionActive";
		private const string STR_SelectionAnchor = "SelectionAnchor";
		private const string STR_TopLine = "TopLine";
		private const string STR_DocView = ".DocView";
		// TODO: New properties added here must be addressed in the FromStorage and Store methods.
		public string FileName { get; set; }  
		public int SelectionActive { get; set; }
		public int SelectionAnchor { get; set; }
		public int TopLine { get; set; }
		// Alt+Home to drop a marker. Escape gets me back.
		public static DocView FromStorage(IDecoupledStorage storage, string parentSection, int index)
		{
			string section = parentSection + STR_DocView + index;
			DocView newDocView = new DocView();
			newDocView.FileName = storage.ReadString(section, STR_FileName);
			newDocView.SelectionActive = storage.ReadInt32(section, STR_SelectionActive);
			newDocView.SelectionAnchor = storage.ReadInt32(section, STR_SelectionAnchor);
			newDocView.TopLine = storage.ReadInt32(section, STR_TopLine);
			return newDocView;
		}
    public void Store(IDecoupledStorage storage, string parentSection, int index)
		{
			string section = parentSection + STR_DocView + index;
			storage.WriteString(section, STR_FileName, FileName);
			storage.WriteInt32(section, STR_SelectionActive, SelectionActive);
			storage.WriteInt32(section, STR_SelectionAnchor, SelectionAnchor);
			storage.WriteInt32(section, STR_TopLine, TopLine);
		}
    public DocView()
		{

		}
	}
}
