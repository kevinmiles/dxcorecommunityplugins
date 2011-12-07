using System;
using System.IO;
using System.Collections.Generic;

using DevExpress.CodeRush.Common;
using DevExpress.CodeRush.StructuralParser;

namespace CR_ImportNamespace
{
  public class DotNetTypesCache : Dictionary<ExtendedFrameworkVersion, TypeToAssemblyNamespaceMap>
  {
    const string STR_DotNetTypesStorage = "DotNetTypesStorage";
    const string STR_DotNetTypesIndex = "DotNetTypesIndex";
    const string STR_Known_Types_ = "Known_Types_{0}";

    static string GetCacheFileName(ExtendedFrameworkVersion frameworkVersion)
    {
      return String.Format(STR_Known_Types_, frameworkVersion);
    }

    static CacheFileSystem CreateCacheFileSystem()
    {
      string cacheRoot = DXCorePaths.GetInstance().CacheStorageRoot;
      string storagePath = Path.Combine(cacheRoot, STR_DotNetTypesStorage);
      string indexPath = Path.Combine(cacheRoot, STR_DotNetTypesIndex);
      return new CacheFileSystem(storagePath, indexPath);
    }

    public void SaveCache(ExtendedFrameworkVersion frameworkVersion)
    {
      if (!ContainsKey(frameworkVersion))
        return;

      TypeToAssemblyNamespaceMap allTypes = this[frameworkVersion];
      if (allTypes.Count == 0)
        return;

      CacheFileSystem cacheFileSystem = CreateCacheFileSystem();
      AssemblyNamespace.SaveCache(cacheFileSystem, frameworkVersion);

      string cacheFileName = GetCacheFileName(frameworkVersion);
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (BinaryWriter writer = new BinaryWriter(memoryStream))
        {
          allTypes.Save(writer);
          writer.Flush();
          memoryStream.Position = 0;
          cacheFileSystem.Write(cacheFileName, memoryStream);
        }
      }

      cacheFileSystem.Close();
    }
    
    public bool LoadCache(ExtendedFrameworkVersion frameworkVersion)
    {
      if (!ContainsKey(frameworkVersion))
        Add(frameworkVersion, new TypeToAssemblyNamespaceMap());
      
      TypeToAssemblyNamespaceMap allTypes = this[frameworkVersion];

      CacheFileSystem cacheFileSystem = CreateCacheFileSystem();
      AssemblyNamespace.LoadCache(cacheFileSystem, frameworkVersion);

      string cacheFileName = GetCacheFileName(frameworkVersion);
      Stream stream = cacheFileSystem.Read(cacheFileName);
      if (stream == null)
      {
        cacheFileSystem.Close();
        return false;
      }

      using (BinaryReader reader = new BinaryReader(stream))
        allTypes.Load(reader, frameworkVersion);

      cacheFileSystem.Close();
      return true;
    }

    public NamespacesResult FastGetNamespaces(string typeName, ExtendedFrameworkVersion frameworkVersion)
    {
      NamespacesResult result = new NamespacesResult();
      result.State = LoadState.NoActiveProject;
      result.Namespaces = null;

      result.State = LoadState.FrameworkNotLoaded;
      if (!ContainsKey(frameworkVersion))
        return result;

      result.State = LoadState.TypeNotFound;
      TypeToAssemblyNamespaceMap knownTypes = this[frameworkVersion];
      if (!knownTypes.ContainsKey(typeName))
        return result;

      result.State = LoadState.TypeFound;
      result.Namespaces = knownTypes[typeName];
      return result;
    }

    public TypeToAssemblyNamespaceMap TryLoadTypesFromCache(ExtendedFrameworkVersion frameworkVersion)
    {
      if (!ContainsKey(frameworkVersion))
        Add(frameworkVersion, new TypeToAssemblyNamespaceMap());

      TypeToAssemblyNamespaceMap dotNetTypesInThisFramework = this[frameworkVersion];
      if (dotNetTypesInThisFramework.Count == 0)
      {
        if (!LoadCache(frameworkVersion))
        {
          Remove(frameworkVersion);
          return null;
        }
        dotNetTypesInThisFramework = this[frameworkVersion];
      }
      return dotNetTypesInThisFramework;
    }

    public TypeToAssemblyNamespaceMap GetTypeToAssemblyMap(IAssemblyPathsProvider pathsProvider, ExtendedFrameworkVersion frameworkVersion)
    {
      TypeToAssemblyNamespaceMap dotNetTypesInThisFramework = TryLoadTypesFromCache(frameworkVersion);
      if (dotNetTypesInThisFramework != null && dotNetTypesInThisFramework.Count != 0)
        return dotNetTypesInThisFramework;

      dotNetTypesInThisFramework = new TypeToAssemblyNamespaceMap();
      this[frameworkVersion] = dotNetTypesInThisFramework;

      string[] frameworkPaths = pathsProvider.GetPathsToScanAssemblies(frameworkVersion);
      dotNetTypesInThisFramework.ScanFrameworkAssemblies(frameworkVersion, frameworkPaths);
      SaveCache(frameworkVersion);
      return dotNetTypesInThisFramework;
    }

    public void ReScanAssemblies(IAssemblyPathsProvider pathsProvider, ExtendedFrameworkVersion frameworkVersion)
    {
      TypeToAssemblyNamespaceMap dotNetTypesInThisFramework;
      if (!TryGetValue(frameworkVersion, out dotNetTypesInThisFramework))
        return;
      string[] frameworkPaths = pathsProvider.GetPathsToScanAssemblies(frameworkVersion);
      dotNetTypesInThisFramework.ScanFrameworkAssemblies(frameworkVersion, frameworkPaths);
      SaveCache(frameworkVersion);
    }
  }
}