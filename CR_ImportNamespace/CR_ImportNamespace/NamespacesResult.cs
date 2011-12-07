namespace CR_ImportNamespace
{
  public class NamespacesResult
  {
    LoadState _State;
    AssemblyNamespaceList _Namespaces;

    public NamespacesResult()
    {
    }

    public LoadState State
    {
      get { return _State; }
      set { _State = value; }
    }

    public AssemblyNamespaceList Namespaces
    {
      get { return _Namespaces; }
      set { _Namespaces = value; }
    }
  }
}