using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;

namespace CR_NCover
{
	public partial class PlugIn : StandardPlugIn
	{
		private readonly CoverageOverlay overlay = new CoverageOverlay();
        private bool showingOverlay = false;
        private readonly Service service = new Service();

		public override void InitializePlugIn()
		{
			base.InitializePlugIn();

			if (VisualStudio.HasASolutionOpened)
				StartResultWatcher();
		}

		public void AfterOpeningSolution()
		{
			StartResultWatcher();
		}

		public void WhenResultsUpdated()
		{
            showingOverlay = true;
            
			if (VisualStudio.HasAnActiveTextView)
                overlay.Show(service.GetCoverageResultsForActiveDocument());
		}

		public void WhenPaintingBackground(EditorPaintEventArgs ea)
		{
            if (showingOverlay)
			    overlay.Update(service.GetCoverageResultsForActiveDocument());
		}

		public void WhenTextChanges(TextChangedEventArgs ea)
		{
            overlay.Hide();
            showingOverlay = false;  
		}

		private void StartResultWatcher()
		{
			string path = TestDriven.NCoverResultPathFor(VisualStudio.Solution);
			service.WatchForResults(path, WhenResultsUpdated);
		}
	}
}