using ApprovalTests;
using CheckPoint;
using DevExpress.CodeRush.Core;
using NUnit.Framework;
using TypeMock.ArrangeActAssert;
using Monitor=CheckPoint.Monitor;

namespace CR_NCover.Tests.PluginSpecs
{
	[TestFixture]
	public class PluginWithNoSolutionOpen
	{
		[NUnit.Framework.SetUp]
		public void Setup()
		{
			pluginSentry = Monitor.Interactions(typeof (PlugIn));

			Isolate.WhenCalled(() => VisualStudio.HasASolutionOpened).WillReturn(false);
			Isolate.WhenCalled(() => VisualStudio.HasAnActiveTextView).WillReturn(true);


			plugin = new PlugIn();
		}

		[NUnit.Framework.TearDown]
		public void TearDown()
		{
			Isolate.CleanUp();
		}

		private PlugIn plugin;
		private Sentry pluginSentry;

		[NUnit.Framework.Test]
		public void DontStartTheExplorerWhenThePluginIsInitialized()
		{
			Approvals.Approve(pluginSentry.Report);
		}

		[NUnit.Framework.Test]
		public void StartTheServiceWhenASolutionIsOpen()
		{
			Isolate.WhenCalled(() => VisualStudio.Solution).WillReturn("");
			
			plugin.AfterOpeningSolution();

			Approvals.Approve(pluginSentry.Report);
		}
	}

	[TestFixture]
	public class PluginWithAnOpenSolution
	{
		[NUnit.Framework.SetUp]
		public void Setup()
		{
			pluginSentry = Monitor.Interactions(typeof (PlugIn));

			Isolate.WhenCalled(() => VisualStudio.Solution).WillReturn("");
			Isolate.WhenCalled(() => VisualStudio.HasASolutionOpened).WillReturn(true);
			Isolate.WhenCalled(() => VisualStudio.HasAnActiveTextView).WillReturn(true);

			plugin = new PlugIn();
		}

		[NUnit.Framework.TearDown]
		public void TearDown()
		{
			Isolate.CleanUp();
		}

		private PlugIn plugin;
		private Sentry pluginSentry;

		[NUnit.Framework.Test]
		[Ignore("Some issue with typemock on the CI system")]
		public void HideOverlayWhenTextHasChanged()
		{
			plugin.WhenTextChanges(null);

			Approvals.Approve(pluginSentry.Report);
		}

		[NUnit.Framework.Test]
		public void ShowOverlayWhenResultsUpdated()
		{
			plugin.WhenResultsUpdated();

			Approvals.Approve(pluginSentry.Report);
		}

		[NUnit.Framework.Test]
		public void StartTheExplorerWhenInitializingThePlugin()
		{
			Approvals.Approve(pluginSentry.Report);
		}

		[NUnit.Framework.Test]
		public void UpdateOverlayWhenPaintingBackground()
		{
			plugin.WhenPaintingBackground(null);

			Approvals.Approve(pluginSentry.Report);
		}
	}
}