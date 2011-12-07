using System;
using System.Collections.Generic;

namespace CR_ImportNamespace
{
  public class AssemblyNamespaceList : List<AssemblyNamespace>
  {
    HashSet<Tuple<string, string>> hashSet;

    public AssemblyNamespaceList()
    {
      hashSet = new HashSet<Tuple<string, string>>();
    }

    public void AddUnique(IEnumerable<AssemblyNamespace> assemblyNamespaces)
    {
      foreach (AssemblyNamespace assemblyNamespace in assemblyNamespaces)
        AddUnique(assemblyNamespace);
    }
    public void AddUnique(AssemblyNamespace assemblyNamespace)
    {
      Tuple<string, string> tuple = new Tuple<string,string>(assemblyNamespace.Assembly, assemblyNamespace.Namespace);
      if (hashSet.Contains(tuple))
        return;
      hashSet.Add(tuple);
      Add(assemblyNamespace);
    }
  }
}