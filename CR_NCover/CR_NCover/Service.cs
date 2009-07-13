using System;
using System.Collections.Generic;
using System.IO;

namespace CR_NCover
{
	public class Service
	{
		private readonly FileSystemWatcher watcher = new FileSystemWatcher();

		public string ResultPath { get; private set; }
		public Action ResultsChanged { get; set; }

		public IList<CoverageResult> GetCoverageResultsForActiveDocument()
		{
			return ResultParser.Parse(ResultPath, VisualStudio.ActiveFilePath);
		}

		public void WatchForResults(string path, Action resultsChanged)
		{
			watcher.Path = path;
			watcher.Filter = "*.xml";
			watcher.Changed += (s, e) =>
			                   	{
			                   		ResultPath = e.FullPath;
			                   		ResultsChanged();
			                   	};


			watcher.EnableRaisingEvents = true;

			ResultsChanged = resultsChanged;
		}
	}
}