using ApprovalTests;
using CheckPoint;
using NUnit.Framework;
using TypeMock.ArrangeActAssert;
using TypeMock;

namespace CR_NCover.Tests.PluginSpecs
{
	[TestFixture]
	[Isolated]
	public class PluginTests
	{
		[Test]
		public void DontStartTheExplorerWhenThePluginIsInitialized()
		{
			Sentry pluginSentry = Monitor.Interactions(typeof(PlugIn));

			Isolate.WhenCalled(() => VisualStudio.HasASolutionOpened).WillReturn(false);
			Isolate.WhenCalled(() => VisualStudio.HasAnActiveTextView).WillReturn(true);

			PlugIn plugin = new PlugIn();

			Approvals.Approve(pluginSentry.Report);
		}

		[Test]
		public void StartTheServiceWhenASolutionIsOpen()
		{
			Sentry pluginSentry = Monitor.Interactions(typeof(PlugIn));

			Isolate.WhenCalled(() => VisualStudio.HasASolutionOpened).WillReturn(false);
			Isolate.WhenCalled(() => VisualStudio.HasAnActiveTextView).WillReturn(true);
			Isolate.WhenCalled(() => VisualStudio.Solution).WillReturn("");

			PlugIn plugin = new PlugIn();

			plugin.AfterOpeningSolution();

			Approvals.Approve(pluginSentry.Report);
		}

		[Test]
		public void HideOverlayWhenTextHasChanged()
		{
			Sentry pluginSentry = Monitor.Interactions(typeof(PlugIn));

			Isolate.WhenCalled(() => VisualStudio.HasASolutionOpened).WillReturn(false);

			PlugIn plugin = new PlugIn();

			plugin.WhenTextChanges(null);

			Approvals.Approve(pluginSentry.Report);
		}

		[Test]
		public void ShowOverlayWhenResultsUpdated()
		{
			Sentry pluginSentry = Monitor.Interactions(typeof(PlugIn));

			Isolate.WhenCalled(() => VisualStudio.Solution).WillReturn("");
			Isolate.WhenCalled(() => VisualStudio.HasASolutionOpened).WillReturn(true);
			Isolate.WhenCalled(() => VisualStudio.HasAnActiveTextView).WillReturn(true);
			PlugIn plugin = new PlugIn();

			plugin.WhenResultsUpdated();

			Approvals.Approve(pluginSentry.Report);
		}

		[Test]
		public void StartTheExplorerWhenInitializingThePlugin()
		{
			Sentry pluginSentry = Monitor.Interactions(typeof(PlugIn));

			Isolate.WhenCalled(() => VisualStudio.Solution).WillReturn("");
			Isolate.WhenCalled(() => VisualStudio.HasASolutionOpened).WillReturn(true);
			Isolate.WhenCalled(() => VisualStudio.HasAnActiveTextView).WillReturn(true);
			PlugIn plugin = new PlugIn();

			Approvals.Approve(pluginSentry.Report);
		}

		[Test]
		public void UpdateOverlayWhenPaintingBackground()
		{
			Sentry pluginSentry = Monitor.Interactions(typeof(PlugIn));

			Isolate.WhenCalled(() => VisualStudio.Solution).WillReturn("");
			Isolate.WhenCalled(() => VisualStudio.HasASolutionOpened).WillReturn(true);
			Isolate.WhenCalled(() => VisualStudio.HasAnActiveTextView).WillReturn(true);
			PlugIn plugin = new PlugIn();

			plugin.WhenPaintingBackground(null);

			Approvals.Approve(pluginSentry.Report);
		}
	}
}