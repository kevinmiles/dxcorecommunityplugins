using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using DevExpress.CodeRush.Common;

namespace CR_FavoriteViews
{
	public class SuperView: INotifyPropertyChanged
	{
		private const string STR_ActiveFile = "ActiveFile";
		private const string STR_DocViewCount = "DocViewCount";
		private const string STR_Name = "Name";
		private const string STR_SuperView = "SuperView";
		// TODO: New properties added here must be addressed in the FromStorage and Store methods.
		private string _Name;
		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				if (_Name == value)
					return;
				_Name = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs(STR_Name));
			}
		}
		public string ActiveFile { get; set; }
		public IList<DocView> DocViews { get; set; }

		public static SuperView FromStorage(IDecoupledStorage storage, int index)
		{
			SuperView superView = new SuperView();
			string section = STR_SuperView + index.ToString();
			superView.Name = storage.ReadString(section, STR_Name);
			superView.ActiveFile = storage.ReadString(section, STR_ActiveFile);
			int docViewCount = storage.ReadInt32(section, STR_DocViewCount);
			for (int i = 0; i < docViewCount; i++)
			{
				DocView newDocView = DocView.FromStorage(storage, section, i);
				superView.DocViews.Add(newDocView);
			}
			return superView;
		}
    public void Store(IDecoupledStorage storage, int index)
		{
			string section = STR_SuperView + index.ToString();
			storage.WriteString(section, STR_Name, Name);
			storage.WriteString(section, STR_ActiveFile, ActiveFile);
			storage.WriteInt32(section, STR_DocViewCount, DocViews.Count);
			for (int i = 0; i < DocViews.Count; i++)
				DocViews[i].Store(storage, section, i);
		}
    public SuperView()
		{
			DocViews = new List<DocView>();
		}

		public override string ToString()
		{
			if (Name == null)
				return "Unnamed";
			return Name;
		}

		public void Restore(bool closeAllActiveDocsFirst)
		{
			if (closeAllActiveDocsFirst)
				DevExpress.CodeRush.Core.CodeRush.Command.Execute("Window.CloseAllDocuments");
			foreach (DocView docView in DocViews)
			{
				DevExpress.CodeRush.Core.Document activatedDoc = DevExpress.CodeRush.Core.CodeRush.File.Activate(docView.FileName);
				DevExpress.CodeRush.Core.TextDocument textDocument = activatedDoc as DevExpress.CodeRush.Core.TextDocument;
				if (textDocument != null)
				{
					DevExpress.CodeRush.Core.TextView activeView = textDocument.ActiveView;
					if (activeView != null)
					{
						activeView.Selection.Set(docView.SelectionAnchor, docView.SelectionActive);
						activeView.SetTopLine(docView.TopLine);
					}
				}
			}

			if (!String.IsNullOrEmpty(ActiveFile))
				DevExpress.CodeRush.Core.CodeRush.File.Activate(ActiveFile);
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion
	}
}
