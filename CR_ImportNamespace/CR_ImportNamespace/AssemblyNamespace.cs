using System;
using System.IO;
using System.Collections.Generic;
using DevExpress.CodeRush.StructuralParser;
using System.Reflection;

namespace CR_ImportNamespace
{
  public class AssemblyNamespace
  {
    private bool _IsProjectReference;
    const string STR_AssemblyCacheFormat = "AssemblyCache{0}";

    readonly static Dictionary<ExtendedFrameworkVersion, List<string>> assemblyCache = new Dictionary<ExtendedFrameworkVersion, List<string>>();
    readonly static List<string> namespaceCache = new List<string>();

    readonly ExtendedFrameworkVersion frameworkVersion;
    int assemblyIndex;
    int namespaceIndex;

    ProjectElement referenceProject;

    string _AssemblyFilePath;

    public AssemblyNamespace()
    {
    }

    public AssemblyNamespace(string assemblyName, string @namespace, ExtendedFrameworkVersion frameworkVersion)
    {
      this.frameworkVersion = frameworkVersion;
      Assembly = assemblyName;
      Namespace = @namespace;
    }

    public AssemblyNamespace(ExtendedFrameworkVersion frameworkVersion, int assemblyIndex, int namespaceIndex)
    {
      this.frameworkVersion = frameworkVersion;
      this.assemblyIndex = assemblyIndex;
      this.namespaceIndex = namespaceIndex;
    }

    // private methods...
    static void WriteStrings(BinaryWriter writer, string[] strings)
    {
      writer.Write(strings.Length);
      foreach (string s in strings)
        writer.Write(s);
    }

    static string[] ReadStrings(BinaryReader reader)
    {
      int count = reader.ReadInt32();
      string[] result = new string[count];
      for (int i = 0; i < count; i++)
        result[i] = reader.ReadString();
      return result;
    }

    static string GetFileName(ExtendedFrameworkVersion frameworkVersion)
    {
      return String.Format(STR_AssemblyCacheFormat, frameworkVersion);
    }

    // public static methods...
    public static void InitializeCache(ExtendedFrameworkVersion frameworkVersion)
    {
      if (!assemblyCache.ContainsKey(frameworkVersion))
        assemblyCache.Add(frameworkVersion, new List<string>());
    }

    public static void SaveCache(CacheFileSystem fileSystem, ExtendedFrameworkVersion frameworkVersion)
    {
      string fileName = GetFileName(frameworkVersion);
      using (MemoryStream stream = new MemoryStream())
      {
        using (BinaryWriter writer = new BinaryWriter(stream))
        {
          bool hasAssemblyCache = assemblyCache.ContainsKey(frameworkVersion);
          writer.Write(hasAssemblyCache);
          if (hasAssemblyCache)
            WriteStrings(writer, assemblyCache[frameworkVersion].ToArray());
          WriteStrings(writer, namespaceCache.ToArray());
          writer.Flush();
          stream.Position = 0;
          fileSystem.Write(fileName, stream);
        }
      }
    }

    public static void LoadCache(CacheFileSystem fileSystem, ExtendedFrameworkVersion frameworkVersion)
    {
      if (assemblyCache.ContainsKey(frameworkVersion))
        assemblyCache[frameworkVersion].Clear();
      else
        assemblyCache.Add(frameworkVersion, new List<string>());

      string fileName = GetFileName(frameworkVersion);
      Stream stream = fileSystem.Read(fileName);
      if (stream == null)
        return;

      using (BinaryReader reader = new BinaryReader(stream))
      {
        bool hasAssemblyCache = reader.ReadBoolean();
        if (hasAssemblyCache)
          assemblyCache[frameworkVersion].AddRange(ReadStrings(reader));

        namespaceCache.Clear();
        namespaceCache.AddRange(ReadStrings(reader));
      }
    }

    public string GetReferenceName()
    {
      string referenceName = String.Empty;
      if (IsProjectReference)
        return ReferenceProject.Name;
      else if (!String.IsNullOrEmpty(AssemblyFilePath))
        return AssemblyFilePath;
      else
        return Assembly;
    }

    public string GetFullAssemblyName()
    {
      string referenceName = String.Empty;
      try
      {
        if (IsProjectReference)
          return ReferenceProject.Name;
        else if (!String.IsNullOrEmpty(AssemblyFilePath))
        {
          AssemblyName name = AssemblyName.GetAssemblyName(AssemblyFilePath);
          return name.FullName;
        }
        else
          return Assembly;
      }
      catch (Exception ex)
      {
        return String.Empty;
      }
    }

    // public properties...
    public string Assembly
    {
      get { return assemblyCache[frameworkVersion][AssemblyIndex]; }
      set
      {
        List<string> thisFrameworkAssemblyCache = assemblyCache[frameworkVersion];
        assemblyIndex = thisFrameworkAssemblyCache.IndexOf(value);
        if (assemblyIndex >= 0)
          return;
        assemblyIndex = thisFrameworkAssemblyCache.Count;
        thisFrameworkAssemblyCache.Add(value);
      }
    }

    public int AssemblyIndex
    {
      get { return assemblyIndex; }
    }

    public int NamespaceIndex
    {
      get { return namespaceIndex; }
    }

    public string Namespace
    {
      get { return namespaceCache[NamespaceIndex]; }
      set
      {
        namespaceIndex = namespaceCache.IndexOf(value);
        if (NamespaceIndex == -1)
        {
          namespaceIndex = namespaceCache.Count;
          namespaceCache.Add(value);
        }
      }
    }

    public string AssemblyFilePath
    {
      get { return _AssemblyFilePath; }
      set { _AssemblyFilePath = value; }
    }

    public bool IsProjectReference
    {
      get { return ReferenceProject != null; }
    }
    
    public ProjectElement ReferenceProject
    {
      get { return referenceProject; }
      set { referenceProject = value; }
    }
  }
}