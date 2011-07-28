using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core.Replacement;
using DevExpress.CodeRush.Core;

namespace CR_SyncNamespacesToFolder
{
	public class FileChangeGroup
	{
		readonly Dictionary<string, List<TypeDeclaration>> _HandledImports = new Dictionary<string, List<TypeDeclaration>>();
		readonly FileChangeCollection _NamespaceDeclarations = new FileChangeCollection();
		readonly FileChangeCollection _NewImportCalls = new FileChangeCollection();
		readonly List<NamespaceReference> _NamespacesToRemove = new List<NamespaceReference>();
    readonly Dictionary<string, List<LanguageElement>> _QualifyingReferences = new Dictionary<string, List<LanguageElement>>();
		readonly Dictionary<string, List<LanguageElement>> _NamespaceImports = new Dictionary<string, List<LanguageElement>>();
		public void AddNamespaceDeclarationChange(SourceRange nameRange, string expectedNamespace)
		{
			_NamespaceDeclarations.Add(new FileChange(FileName, nameRange, expectedNamespace));
		}
		private void AddFileChanges(FileChangeCollection fileChangeCollection, Dictionary<string, List<LanguageElement>> references)
		{
			foreach (KeyValuePair<string, List<LanguageElement>> referenceList in references)
			{
				string expectedNamespace = referenceList.Key;
				List<LanguageElement> allReferences = referenceList.Value;
				foreach (LanguageElement reference in allReferences)
					fileChangeCollection.Add(new FileChange(reference.FileNode.Name, reference.Range, expectedNamespace));
			}
		}
		public void AddNamespaceImports(FileChangeCollection fileChangeCollection)
		{
			AddFileChanges(fileChangeCollection, _NamespaceImports);
		}
		public void AddQualifyingReferences(FileChangeCollection fileChangeCollection)
		{
			AddFileChanges(fileChangeCollection, _QualifyingReferences);
		}
		private static void AddReferenceChange(Dictionary<string, List<LanguageElement>> references, string expectedNamespace, LanguageElement namespaceReference)
		{
			if (!references.ContainsKey(expectedNamespace))
				references.Add(expectedNamespace, new List<LanguageElement>());
			references[expectedNamespace].Add(namespaceReference);
		}
		public void AddNamespaceImportChange(LanguageElement reference, string expectedNamespace)
		{
			AddReferenceChange(_NamespaceImports, expectedNamespace, reference);
		}

		public void AddNewImport(SourcePoint start, string namespaceName, SourceFile sourceFile, bool isLast)
		{
			string newImportCall = CodeRush.Language.GenerateElement(new NamespaceReference(namespaceName), sourceFile.Project.Language);
			if (isLast && newImportCall.EndsWith(Environment.NewLine))		// Remove cr/lf from last entry.
				newImportCall = newImportCall.Remove(newImportCall.Length - Environment.NewLine.Length);
			_NewImportCalls.Add(new FileChange(sourceFile.Name, start, newImportCall));
		}
    public void RemoveNamespaceImport(NamespaceReference import)
		{
			_NamespacesToRemove.Add(import);
		}
		public void RemoveOldNamespaceImports(FileChangeCollection fileChangeCollection)
		{
			foreach (NamespaceReference namespaceReference in _NamespacesToRemove)
				fileChangeCollection.Add(new FileChange(namespaceReference.FileNode.Name, namespaceReference.Range, String.Empty));
		}
    public void AddQualifyingReferenceChange(LanguageElement namespaceReference, string expectedNamespace)
		{
			AddReferenceChange(_QualifyingReferences, expectedNamespace, namespaceReference);
		}
		public List<TypeDeclaration> GetHandledImports(string fileName)
		{
			return _HandledImports[fileName];
		}
    public void PrepareForTrackingImports(string fileName)
		{
			if (!_HandledImports.ContainsKey(fileName))		// Track all the imports we've 
				_HandledImports.Add(fileName, new List<TypeDeclaration>());
		}
    private static void RemoveReference(Dictionary<string, List<LanguageElement>> references, LanguageElement reference)
		{
			foreach (string key in references.Keys)
			{
				List<LanguageElement> referencesList = references[key];
				int index = referencesList.IndexOf(reference);
				if (index < 0)
					continue;

				referencesList.RemoveAt(index);
				return;	
			}
		}
		private void RemoveOverlappingReferences(Dictionary<string, List<LanguageElement>> references)
		{
			List<LanguageElement> allRefs = new List<LanguageElement>();
			List<LanguageElement> elementsToRemove = new List<LanguageElement>();
			foreach (string key in references.Keys)
				foreach (LanguageElement element in references[key])
					if (allRefs.IndexOf(element) < 0)
						allRefs.Add(element);
					else
						elementsToRemove.Add(element);

			foreach (LanguageElement element in elementsToRemove)
				RemoveReference(references, element);

			for (int firstIndex = allRefs.Count - 1; firstIndex >= 0; firstIndex--)
			{
				LanguageElement first = allRefs[firstIndex];
				for (int secondIndex = allRefs.Count - 1; secondIndex >= firstIndex + 1; secondIndex--)
				{
					LanguageElement second = allRefs[secondIndex];
					if (first.Range.Contains(second.Range))
						RemoveReference(references, second);
					else if (second.Range.Contains(first.Range))
						RemoveReference(references, first);
				}
			}
		}
		public void RemoveOverlappingReferences()
		{
			RemoveOverlappingReferences(_QualifyingReferences);
			RemoveOverlappingReferences(_NamespaceImports);
		}
		public FileChangeGroup(string fileName)
		{
			FileName = fileName;
		}
		public Dictionary<string, List<TypeDeclaration>> HandledImports
		{
			get { return _HandledImports; }
		}
		public string FileName { get; private set; }
		public FileChangeCollection NamespaceDeclarations
		{
			get { return _NamespaceDeclarations; }
		}
		public Dictionary<string, List<LanguageElement>> NamespaceImports
		{
			get { return _NamespaceImports; }
		}
		public FileChangeCollection NewImportCalls
		{
			get { return _NewImportCalls; }
		}
		public Dictionary<string, List<LanguageElement>> QualifyingReferences
		{
			get { return _QualifyingReferences; }
		}
	}
}
