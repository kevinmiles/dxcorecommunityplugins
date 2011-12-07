using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

using DevExpress.CodeRush.StructuralParser;
using DevExpress.Refactor.Diagnostics;
using DevExpress.DXCore.MetaData;

namespace CR_ImportNamespace
{
  public class TypeToAssemblyNamespaceMap : Dictionary<string, AssemblyNamespaceList>
  {
    string _AssemblyPath;
    FileSignature _AssemblySignature;

    Dictionary<string, TypeToAssemblyNamespaceMap> _Assemblies;

    public TypeToAssemblyNamespaceMap()
    {
      _Assemblies = new Dictionary<string, TypeToAssemblyNamespaceMap>();
    }

    static byte[] GetAssemblyBytes(string name)
    {
      using (Stream stream = File.Open(name, FileMode.Open, FileAccess.Read, FileShare.Read))
      {
        long streamLength = stream.Length;
        byte[] streamBytes = new byte[streamLength];
        stream.Read(streamBytes, 0, (int)streamLength);
        return streamBytes;
      }
    }
    static string GetFrameworkVersionStr(ExtendedFrameworkVersion frameworkVersion)
    {
      switch (frameworkVersion)
      {
        case ExtendedFrameworkVersion.Version10:
          return "1.0";
        case ExtendedFrameworkVersion.Version11:
          return "1.1";
        case ExtendedFrameworkVersion.Version20:
          return "2.0";
        case ExtendedFrameworkVersion.Version30:
          return "3.0";
        case ExtendedFrameworkVersion.Version35:
          return "3.5";
        case ExtendedFrameworkVersion.Version40:
          return "4.0";
        default:
          return "";
      }
    }

    public class AssemblyTypeLoadResult
    {
      IAssemblyInfo _AssemblyInfo;
      ITypeInfo[] _Types;

      public AssemblyTypeLoadResult()
      {
      }

      public IAssemblyInfo AssemblyInfo
      {
        get { return _AssemblyInfo; }
        set { _AssemblyInfo = value; }
      }
      public ITypeInfo[] Types
      {
        get { return _Types; }
        set { _Types = value; }
      }
    }

    static AssemblyTypeLoadResult LoadTypesFromAssembly(string assemblyPath)
    {
      IMetaDataScope scope = null;

      byte[] assemblyBytes = GetAssemblyBytes(assemblyPath);
      if (assemblyBytes == null)
        return null;

      try
      {
        scope = ScopeManager.FromBytes(assemblyBytes, ScopeOptions.OpenLocal | ScopeOptions.LoadInternalElements | ScopeOptions.LoadPrivateElements | ScopeOptions.LoadMethodBodies | ScopeOptions.LoadAssemblyReferences, new AssemblyResolver(), true);
      }
      catch (ArgumentException ex)
      {
        return null;
      }

      IAssemblyInfo info = scope.Assembly;
      ITypeInfo[] typeInfos = null;
      try
      {
        typeInfos = info.FindAllTypes(typeInfo =>
        {
          return typeInfo.Visibility == Visibility.Public;
        });
      }
      catch (Exception ex)
      {
        return null;
      }

      AssemblyTypeLoadResult result = new AssemblyTypeLoadResult();
      result.AssemblyInfo = info;
      result.Types = typeInfos;
      return result;
    }

    void AddTypeAssemblyMaps(List<TypeToAssemblyNamespaceMap> typeAssemblyMaps)
    {
      if (typeAssemblyMaps == null)
        return;
      foreach (TypeToAssemblyNamespaceMap map in typeAssemblyMaps)
      {
        string fileName = map._AssemblyPath.ToLowerInvariant();
        _Assemblies[fileName] = map;
      }
    }

    void MergeAssemblyMaps()
    {
      Clear();
      foreach (TypeToAssemblyNamespaceMap map in _Assemblies.Values)
      {
        foreach (string key in map.Keys)
        {
          AssemblyNamespaceList namespaceList = map[key];
          if (namespaceList != null)
          {
            AssemblyNamespaceList addedList;
            if (TryGetValue(key, out addedList))
              addedList.AddUnique(namespaceList);
            else
              Add(key, namespaceList);
          }
        }
      }
    }

    TypeToAssemblyNamespaceMap CollectTypes(string assemblyPath, ExtendedFrameworkVersion frameworkVersion)
    {
      assemblyPath = assemblyPath.ToLowerInvariant();

      TypeToAssemblyNamespaceMap result = new TypeToAssemblyNamespaceMap();
      result._AssemblyPath = assemblyPath;
      result._AssemblySignature = new FileSignature(assemblyPath);

      AssemblyTypeLoadResult typeLoadResult = LoadTypesFromAssembly(assemblyPath);
      if (typeLoadResult == null)
        return result;
      
      foreach (ITypeInfo typeInfo in typeLoadResult.Types)
      {
        if (!result.ContainsKey(typeInfo.NestedName))
          result.Add(typeInfo.NestedName, new AssemblyNamespaceList());
        result[typeInfo.NestedName].AddUnique(new AssemblyNamespace(typeLoadResult.AssemblyInfo.FullName, typeInfo.Namespace, frameworkVersion));
      }
      return result;
    }
    List<TypeToAssemblyNamespaceMap> CollectTypesFromAssemblies(List<FileInfo> files, IAssemblyScanProgress progress, ExtendedFrameworkVersion frameworkVersion)
    {
      List<TypeToAssemblyNamespaceMap> result = new List<TypeToAssemblyNamespaceMap>();
      for (int i = 0; i < files.Count; i++)
      {
        try
        {
          FileInfo fileInfo = files[i];
          string text = fileInfo.Name;
          progress.UpdateProgress(i, text);
          TypeToAssemblyNamespaceMap typeAssemblyMap = CollectTypes(fileInfo.FullName, frameworkVersion);
          if (typeAssemblyMap != null)
            result.Add(typeAssemblyMap);
        }
        catch (Exception ex)
        {
          Log.SendException(ex);
        }
      }
      return result;
    }

    List<FileInfo> GetAllAssemblyFiles(string[] paths)
    {
      List<FileInfo> result = new List<FileInfo>();

      HashSet<string> assemblyFileHashSet = new HashSet<string>();
      foreach (string path in paths)
      {
        DirectoryInfo info = new DirectoryInfo(path);
        FileInfo[] files = info.GetFiles("*.dll");
        try
        {
          foreach (FileInfo fileInfo in files)
          {
            string assemblyPath = fileInfo.FullName.ToLowerInvariant();
            if (assemblyFileHashSet.Contains(assemblyPath))
              continue;
            result.Add(fileInfo);
          }
        }
        catch (Exception ex)
        {
          Log.SendException(ex);
        }
      }
      return result;
    }

    bool NeedToScanAssembly(FileInfo fileInfo)
    {
      string fileName = fileInfo.FullName.ToLowerInvariant();
      if (!_Assemblies.ContainsKey(fileName))
        return true;

      if (!File.Exists(fileName))
        return false;

      TypeToAssemblyNamespaceMap assemblyMap = _Assemblies[fileName];
      FileSignature fileSignature = assemblyMap._AssemblySignature;

      FileSignature actualSignature = new FileSignature(fileName);
      if (!actualSignature.Match(fileSignature))
        return true;

      return false;
    }

    List<FileInfo> GetAssembliesToScan(string[] paths)
    {
      List<FileInfo> assemblyFiles = GetAllAssemblyFiles(paths);
      List<FileInfo> result = new List<FileInfo>();
      foreach (FileInfo fileInfo in assemblyFiles)
      {
        if (NeedToScanAssembly(fileInfo))
          result.Add(fileInfo);
      }
      return result;
    }

    void RunOperationWithWaitCursor(System.Action action)
    {
      if (action == null)
        return;
      Cursor saveCursor = Cursor.Current;
      try
      {
        Cursor.Current = Cursors.WaitCursor;
        action();
      }
      finally
      {
        Cursor.Current = saveCursor;
      }
    }

    IAssemblyScanProgress StartAssembliesScan(ExtendedFrameworkVersion frameworkVersion, List<FileInfo> files)
    {
      FrmCollectingClasses frmCollectingClasses = new FrmCollectingClasses();
      string frameworkVersionStr = GetFrameworkVersionStr(frameworkVersion);
      frmCollectingClasses.PerformingScanText = String.Format("Performing a one-time scan of the .NET {0} framework....", frameworkVersionStr);
      frmCollectingClasses.Show();
      frmCollectingClasses.Refresh();
      frmCollectingClasses.Start(files.Count);
      return frmCollectingClasses;
    }

    TypeToAssemblyNamespaceMap LoadAssemblyMap(BinaryReader reader, ExtendedFrameworkVersion frameworkVersion)
    {
      TypeToAssemblyNamespaceMap result = new TypeToAssemblyNamespaceMap();
      result._AssemblyPath = reader.ReadString();
      result._AssemblySignature = FileSignature.Read(reader);

      int typeCount = reader.ReadInt32();
      for (int typeIndex = 0; typeIndex < typeCount; typeIndex++)
      {
        string typeName = reader.ReadString();
        int namespaceCount = reader.ReadInt32();

        if (!result.ContainsKey(typeName))
          result.Add(typeName, new AssemblyNamespaceList());
        AssemblyNamespaceList assemblyNamespaces = result[typeName];
        for (int i = 0; i < namespaceCount; i++)
        {
          int assemblyIndex = reader.ReadInt32();
          int namespaceIndex = reader.ReadInt32();
          assemblyNamespaces.Add(new AssemblyNamespace(frameworkVersion, assemblyIndex, namespaceIndex));
        }
      }
      return result;
    }
    void SaveAssemblyMap(BinaryWriter writer, TypeToAssemblyNamespaceMap map)
    {
      writer.Write(map._AssemblyPath);
      map._AssemblySignature.Write(writer);

      writer.Write(map.Keys.Count);
      int typeIndex = 0;
      foreach (string typeName in map.Keys)
      {
        AssemblyNamespaceList assemblyNamespaces = map[typeName];
        writer.Write(typeName);
        writer.Write(assemblyNamespaces.Count);
        for (int i = 0; i < assemblyNamespaces.Count; i++)
        {
          AssemblyNamespace thisAssemblyNamespace = assemblyNamespaces[i];
          writer.Write(thisAssemblyNamespace.AssemblyIndex);
          writer.Write(thisAssemblyNamespace.NamespaceIndex);
        }
        typeIndex++;
      }
    }

    // public methods...
    public void Load(BinaryReader reader, ExtendedFrameworkVersion frameworkVersion)
    {
      int count = reader.ReadInt32();
      for (int i = 0; i < count; i++)
      {
        TypeToAssemblyNamespaceMap map = LoadAssemblyMap(reader, frameworkVersion);
        if (map != null)
        {
          string fileName = map._AssemblyPath.ToLowerInvariant();
          _Assemblies[fileName] = map;
        }
      }
      MergeAssemblyMaps();
    }

    public void Save(BinaryWriter writer)
    {
      int count = _Assemblies.Count;
      writer.Write(count);
      foreach (TypeToAssemblyNamespaceMap map in _Assemblies.Values)
        SaveAssemblyMap(writer, map);
    }

    public void ScanFrameworkAssemblies(ExtendedFrameworkVersion frameworkVersion, string[] frameworkPaths)
    {
      RunOperationWithWaitCursor(() =>
      {
        AssemblyNamespace.InitializeCache(frameworkVersion);
        List<FileInfo> files = GetAssembliesToScan(frameworkPaths);
        if (files == null || files.Count == 0)
          return;

        IAssemblyScanProgress progress = StartAssembliesScan(frameworkVersion, files);
        try
        {
          List<TypeToAssemblyNamespaceMap> typeAssemblyMaps = CollectTypesFromAssemblies(files, progress, frameworkVersion);
          if (typeAssemblyMaps != null)
            AddTypeAssemblyMaps(typeAssemblyMaps);
          
          MergeAssemblyMaps();
        }
        finally
        {
          progress.Stop();
        }
      });
    }
  }
}