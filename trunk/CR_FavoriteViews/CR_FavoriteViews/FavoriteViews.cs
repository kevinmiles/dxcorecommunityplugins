using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Common;

namespace CR_FavoriteViews
{
	[Title("FavViews")]
	public partial class FavoriteViews : ToolWindowPlugIn
	{
		private BindingList<SuperView> _AllViews = new BindingList<SuperView>();
		private bool _ChangingInternally;
    // DXCore-generated code...
		#region InitializePlugIn
		public override void InitializePlugIn()
		{
			base.InitializePlugIn();

			lstViews.DataSource = _AllViews;
			LoadViews();
		}
		#endregion
		#region FinalizePlugIn
		public override void FinalizePlugIn()
		{
			//
			// TODO: Add your finalization code here.
			//

			base.FinalizePlugIn();
		}
		#endregion

		private static IDecoupledStorage GetSolutionStorage()
		{
			// I'm pretty sure there's a way to get storage from a specified file, which would
			// allow us to store settings in with the solution. We'll look into that next time.
			return CodeRush.Source.GetStorage(CodeRush.Source.ActiveSolution, CodeRush.Source.ActiveSolution.Name + " - Favorite Views");
		}
    private void btnSaveCurrentView_Click(object sender, EventArgs e)
		{
			SuperView superView = new SuperView();
			Document activeDocument = CodeRush.Documents.Active;
			// activeDocument.ActiveWindow and 
			foreach (EnvDTE.Window window in activeDocument.Windows)
			{
				//window.
			}
			if (activeDocument != null)
				superView.ActiveFile = activeDocument.FullName;

			foreach (Document document in CodeRush.Documents.AllDocuments)
			{
				DocView docView = new DocView();
				docView.FileName = document.FullName;
				TextDocument textDocument = document as TextDocument;
				
				if (textDocument != null)
				{
					TextView firstView = textDocument.FirstView;

					if (firstView != null)
					{
						docView.TopLine = firstView.TopLine;
						TextViewSelection activeSelection = firstView.Selection;
						if (activeSelection != null)
						{
							docView.SelectionAnchor = activeSelection.AnchorPosition;
							docView.SelectionActive = activeSelection.ActivePosition;
						}
					}
				}
				superView.DocViews.Add(docView);
			}
			_AllViews.Add(superView);
			lstViews.SelectedItem = superView;
			SaveViews();
		}

		private void btnDeleteSelectedView_Click(object sender, EventArgs e)
		{
			if (lstViews.SelectedIndex >= 0)
			{
				_AllViews.RemoveAt(lstViews.SelectedIndex);
				SaveViews();
			}
		}

		private void lstViews_SelectedIndexChanged(object sender, EventArgs e)
		{
			SuperView superView = lstViews.SelectedItem as SuperView;
			if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
			{
				if (superView != null)
					superView.Restore(true);
			}
			if (superView != null)
			{
				_ChangingInternally = true;
				try
				{
					textBox1.Text = superView.Name;
				}
				finally
				{
					_ChangingInternally = false;
				}
			}
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			if (_ChangingInternally)
				return;
			SuperView superView = lstViews.SelectedItem as SuperView;

			if (superView != null)
			{
				superView.Name = textBox1.Text;
				SaveViews();		// Easiest to code - 1 line, but probably not the most performant.
			}
		}
		private void SaveViews()
		{
			// * CodeRush.Options.GetStorage(); For options page storage. Persisted!
			// * CodeRush.Source.GetStorage() -- useful for project or solution-based storage. Persisted!
			// * ActiveTextDocument.Storage - for storage bound to the document -- NOT persisted!
			//   But useful for session state associated with a document.
			IDecoupledStorage storage = GetSolutionStorage();
			storage.WriteInt32("FaveViews", "ViewCount", _AllViews.Count);
			for (int i = 0; i < _AllViews.Count; i++)
				_AllViews[i].Store(storage, i);
		}
		private void LoadViews()
		{
			_AllViews.Clear();
			IDecoupledStorage storage = GetSolutionStorage();
			int viewsCount = storage.ReadInt32("FaveViews", "ViewCount");
			for (int i = 0; i < viewsCount; i++)
			{
				SuperView newSuperView = SuperView.FromStorage(storage, i);
				_AllViews.Add(newSuperView);
			}
		}
		private void events_SolutionOpened()
		{
			LoadViews();
		}

		private void events_AfterClosingSolution()
		{
			_AllViews.Clear();
		}
	}
}