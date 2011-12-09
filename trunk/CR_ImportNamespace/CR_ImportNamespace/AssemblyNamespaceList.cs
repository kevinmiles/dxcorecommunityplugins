using System;
using System.Collections.Generic;

namespace CR_ImportNamespace
{
  public class AssemblyNamespaceList : IEnumerable<AssemblyNamespace>
  {
    List<AssemblyNamespace> _Data;
    HashSet<int> _HashSet;

    public AssemblyNamespaceList()
    {
      _Data = new List<AssemblyNamespace>();
      _HashSet = new HashSet<int>();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public void Add(AssemblyNamespace assemblyNamespace)
    {
      AddUnique(assemblyNamespace);
    }

    public void AddUnique(IEnumerable<AssemblyNamespace> assemblyNamespaces)
    {
      if (assemblyNamespaces == null)
        return;
      foreach (AssemblyNamespace assemblyNamespace in assemblyNamespaces)
        AddUnique(assemblyNamespace);
    }
    public void AddUnique(AssemblyNamespace assemblyNamespace)
    {
      if (assemblyNamespace == null)
        return;

      string referenceName = String.Empty;
      if (assemblyNamespace.IsProjectReference)
        referenceName = assemblyNamespace.ReferenceProject.Name;
      else
        referenceName = assemblyNamespace.Assembly;

      string hashStr = String.Concat(referenceName, assemblyNamespace.Namespace);
      int hashCode = hashStr.GetHashCode();
      if (_HashSet.Contains(hashCode))
        return;

      _HashSet.Add(hashCode);
      _Data.Add(assemblyNamespace);
    }

    public IEnumerator<AssemblyNamespace> GetEnumerator()
    {
      return _Data.GetEnumerator();
    }

    public int Count
    {
      get { return _Data.Count; }
    }
    public AssemblyNamespace this[int index]
    {
      get { return _Data[index]; }
    }
  }
}