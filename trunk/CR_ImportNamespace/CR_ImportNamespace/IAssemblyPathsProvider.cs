using DevExpress.CodeRush.StructuralParser;

namespace CR_ImportNamespace
{
  public interface IAssemblyPathsProvider
  {
    string[] GetPathsToScanAssemblies(ExtendedFrameworkVersion frameworkVersion);
  }
}