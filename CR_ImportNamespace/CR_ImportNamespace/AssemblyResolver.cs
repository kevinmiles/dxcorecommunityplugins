using DevExpress.DXCore.MetaData;

namespace CR_ImportNamespace
{
  internal class AssemblyResolver : IAssemblyResolver
  {
    public IAssemblyInfo ResolvePartial(string partialName)
    {
      return ResolvePartial(partialName, ScopeOptions.None);
    }
    public IAssemblyInfo ResolvePartial(string partialName, ScopeOptions options)
    {
      IMetaDataScope scope = ScopeManager.FromPartialName(partialName, options);
      if (scope == null)
        return null;
      return scope.Assembly;
    }
    public IAssemblyInfo Resolve(string fullName)
    {
      return Resolve(fullName, ScopeOptions.None);
    }
    public IAssemblyInfo Resolve(string fullName, ScopeOptions options)
    {
      IMetaDataScope scope = ScopeManager.FromName(fullName, options);
      if (scope == null)
        return null;
      return scope.Assembly;
    }
  }
}