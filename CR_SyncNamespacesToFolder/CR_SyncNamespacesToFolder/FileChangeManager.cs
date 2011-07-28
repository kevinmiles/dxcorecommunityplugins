using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.CodeRush.Core.Replacement;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core;

namespace CR_SyncNamespacesToFolder
{
	public class FileChangeManager : Dictionary<string, FileChangeGroup>
	{
		public void ApplyChanges()
		{
			FileChangeCollection fileChangeCollection = new FileChangeCollection();
			foreach (FileChangeGroup fileChangeGroup in Values)
			{
				fileChangeGroup.RemoveOverlappingReferences();
				foreach (FileChange namespaceDeclarationChange in fileChangeGroup.NamespaceDeclarations)
					fileChangeCollection.Add(namespaceDeclarationChange);

				foreach (FileChange newImportCall in fileChangeGroup.NewImportCalls)
					fileChangeCollection.Add(newImportCall);

				fileChangeGroup.RemoveOldNamespaceImports(fileChangeCollection);
				fileChangeGroup.AddNamespaceImports(fileChangeCollection);
				fileChangeGroup.AddQualifyingReferences(fileChangeCollection);
			}
			CodeRush.File.ApplyChanges(fileChangeCollection);
		}
		
	}
}
