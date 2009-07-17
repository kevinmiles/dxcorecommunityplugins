using System;
using System.Collections.Generic;
using System.IO;

namespace CR_NCover
{
	public class Service
	{
		private readonly FileSystemWatcher watcher = new FileSystemWatcher();
		private readonly IDictionary<string, IList<CoverageResult>> cache = new Dictionary<string, IList<CoverageResult>>();

		public string ResultPath { get; private set; }
		public Action ResultsChanged { get; set; }

		public IList<CoverageResult> GetCoverageResultsForActiveDocument()
		{
			string sourceFile = VisualStudio.ActiveFilePath;
			
			if (cache.ContainsKey(sourceFile))
				return cache[sourceFile];

			var result = ResultParser.Parse(ResultPath, sourceFile);
			cache.Add(sourceFile, result);
			return result;
		}

		public void WatchForResults(string path, Action resultsChanged)
		{
			watcher.Path = path;
			watcher.Filter = "*.xml";
			watcher.Changed += (s, e) =>
			                   	{
									cache.Clear();
			                   		ResultPath = e.FullPath;
			                   		ResultsChanged();
			                   	};


			watcher.EnableRaisingEvents = true;

			ResultsChanged = resultsChanged;
		}
	}
}