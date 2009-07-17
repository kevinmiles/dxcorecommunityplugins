using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using CheckPoint;
using ApprovalTests;
using TypeMock.ArrangeActAssert;
using System.Drawing;

namespace CR_NCover.Tests.ServiceSpecs
{
	[TestFixture]
	[Isolated]
	public class ServiceTests
	{
		[Test]
		public void WatchForResultsShouldConfigureWatcher()
		{
			var serviceSentry = Monitor.Interactions(typeof(Service));
			
			Service service = new Service();
			service.WatchForResults("", () => { });

			Approvals.Approve(serviceSentry.Report);
		}

		[Test]
		public void CacheResults()
		{
			var serviceSentry = Monitor.Interactions(typeof(Service));
			
			Isolate.WhenCalled(() => VisualStudio.ActiveFilePath).WillReturn("foo.cs");

			Service service = new Service();
			service.GetCoverageResultsForActiveDocument();
			service.GetCoverageResultsForActiveDocument();

			Approvals.Approve(serviceSentry.Report);
		}
	}
}
