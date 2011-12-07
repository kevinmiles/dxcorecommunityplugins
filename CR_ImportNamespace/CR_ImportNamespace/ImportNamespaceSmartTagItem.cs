using DevExpress.CodeRush.Core;

namespace CR_ImportNamespace
{
  internal class ImportNamespaceSmartTagItem : SmartTagItem
  {
    AssemblyNamespace _AssemblyNamespace;

    public ImportNamespaceSmartTagItem(AssemblyNamespace assemblyNamespace)
      : base(assemblyNamespace.Namespace)
    {
      _AssemblyNamespace = assemblyNamespace;
    }

    public AssemblyNamespace AssemblyNamespace
    {
      get { return _AssemblyNamespace; }
      set { _AssemblyNamespace = value; }
    }
  }
}