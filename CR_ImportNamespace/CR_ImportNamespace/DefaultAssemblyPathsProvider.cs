using System;
using System.IO;
using System.Collections.Generic;

using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core;

namespace CR_ImportNamespace
{
  public class DefaultAssemblyPathsProvider : IAssemblyPathsProvider
  {
    const string STR_ReferenceAssembliesPart = "Reference Assemblies\\Microsoft\\Framework\\.NETFramework\\";
    const string STR_ReferenceAssembliesSilverlightPart = "Reference Assemblies\\Microsoft\\Framework\\Silverlight\\";


    static void AddItemsToHashSet<T>(HashSet<T> hashSet, IEnumerable<T> items)
    {
      foreach (T item in items)
        AddItemToHashSet(hashSet, item);
    }
    static void AddItemToHashSet<T>(HashSet<T> hashSet, T item)
    {
      if (hashSet.Contains(item))
        return;
      hashSet.Add(item);
    }

    static void GetAssemblyPathsFromActiveProject(HashSet<string> result)
    {
      ProjectElement project = CodeRush.Source.ActiveProject;
      NodeList assemblyReferences = project.AssemblyReferences;
      foreach (AssemblyReference assemblyReference in assemblyReferences)
      {
        string filePath = assemblyReference.FilePath;
        if (String.IsNullOrEmpty(filePath))
          continue;
        string directory = Path.GetDirectoryName(filePath);
        directory = directory.ToLowerInvariant();
        AddItemToHashSet(result, directory);
      }
    }

    string GetFrameworkVersionPathPart(ExtendedFrameworkVersion frameworkVersion)
    {
      if (frameworkVersion == ExtendedFrameworkVersion.Version35)
        return "v3.5";

      if (frameworkVersion == ExtendedFrameworkVersion.Version35ClientProfile)
        return "v3.5\\Profile\\Client";

      if (frameworkVersion == ExtendedFrameworkVersion.Version40)
        return "v4.0";

      if (frameworkVersion == ExtendedFrameworkVersion.Version40ClientProfile)
        return "v4.0\\Profile\\Client";

      if (frameworkVersion == ExtendedFrameworkVersion.Version30Silverlight)
        return "v3.0";

      if (frameworkVersion == ExtendedFrameworkVersion.Version40Silverlight)
        return "v4.0";

      if (frameworkVersion == ExtendedFrameworkVersion.Version40SilverlightWindowsPhone)
        return "v4.0\\Profile\\WindowsPhone";

      return String.Empty;
    }

    bool CanGetReferenceAssembliesPath(ExtendedFrameworkVersion frameworkVersion)
    {
      return frameworkVersion == ExtendedFrameworkVersion.Version35 ||
        frameworkVersion == ExtendedFrameworkVersion.Version35ClientProfile ||
        frameworkVersion == ExtendedFrameworkVersion.Version40 ||
        frameworkVersion == ExtendedFrameworkVersion.Version40ClientProfile ||
        frameworkVersion == ExtendedFrameworkVersion.Version30Silverlight ||
        frameworkVersion == ExtendedFrameworkVersion.Version40Silverlight ||
        frameworkVersion == ExtendedFrameworkVersion.Version40SilverlightWindowsPhone;
    }

    bool IsSilveright(ExtendedFrameworkVersion frameworkVersion)
    {
      return frameworkVersion == ExtendedFrameworkVersion.Version30Silverlight ||
        frameworkVersion == ExtendedFrameworkVersion.Version40Silverlight ||
        frameworkVersion == ExtendedFrameworkVersion.Version40SilverlightWindowsPhone;
    }

    string GetReferenceAssembliesBasePath(ExtendedFrameworkVersion frameworkVersion)
    {
      string programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
      if (IsSilveright(frameworkVersion))
        return Path.Combine(programFiles, STR_ReferenceAssembliesSilverlightPart);
      return Path.Combine(programFiles, STR_ReferenceAssembliesPart);
    }

    string GetReferenceAssembliesPath(ExtendedFrameworkVersion frameworkVersion)
    {
      string versionPart = GetFrameworkVersionPathPart(frameworkVersion);
      if (String.IsNullOrEmpty(versionPart))
        return String.Empty;

      if (CanGetReferenceAssembliesPath(frameworkVersion))
      {
        string path = GetReferenceAssembliesBasePath(frameworkVersion);
        return Path.Combine(path, versionPart);
      }
      return string.Empty;
    }

    // public methods...
    public string[] GetPathsToScanAssemblies(ExtendedFrameworkVersion frameworkVersion)
    {
      HashSet<string> result = new HashSet<string>();

      //AddItemToHashSet(result, "c:\\TestAssemblies\\");

      FrameworkVersion version = ExtendedFrameworkVersionUtil.ToFrameworkVersion(frameworkVersion);
      string[] frameworkPaths = FrameworkHelper.GetFrameworkPaths(version);
      AddItemsToHashSet(result, frameworkPaths);

      string referenceAssembliesPath = GetReferenceAssembliesPath(frameworkVersion);
      if (!String.IsNullOrEmpty(referenceAssembliesPath))
        AddItemToHashSet(result, referenceAssembliesPath);

      /*
      string[] assemblyFolders = FrameworkHelper.GetAssemblyFoldersPaths(frameworkVersion);
      AddItemsToHashSet(result, assemblyFolders);

      GetAssemblyPathsFromActiveProject(result);
      */

      string[] paths = new string[result.Count];
      result.CopyTo(paths);
      return paths;
    }
  }
}