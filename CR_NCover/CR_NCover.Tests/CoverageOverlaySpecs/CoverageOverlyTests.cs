using System.Collections.Generic;
using ApprovalTests;
using CheckPoint;
using DevExpress.CodeRush.Core;
using NUnit.Framework;
using TypeMock.ArrangeActAssert;
using Monitor=CheckPoint.Monitor;

namespace CR_NCover.Tests.CoverageOverlaySpecs
{
	[TestFixture]
	[Isolated]
	public class CoverageOverlyTests
	{
		[NUnit.Framework.SetUp]
		public void Setup()
		{
			visualStudioSentry = Monitor.Calls<VisualStudio>();

			overlay = new CoverageOverlay();
			Isolate.WhenCalled(() => CodeRush.TextViews.Active).ReturnRecursiveFake();
		}

		[NUnit.Framework.TearDown]
		public void TearDown()
		{
			Isolate.CleanUp();
		}

		private CoverageOverlay overlay;
		private Sentry visualStudioSentry;

		[NUnit.Framework.Test]
		public void DisplayOverlayOnShow()
		{
			overlay.Show(new List<CoverageResult> {new CoverageResult {VisitCount = 0}, new CoverageResult {VisitCount = 1}});

			Approvals.Approve(visualStudioSentry.Report);
		}

		[NUnit.Framework.Test]
		public void InvalidateOnHide()
		{
			overlay.Hide();

			Approvals.Approve(visualStudioSentry.Report);
		}
	}
}