using DevExpress.CodeRush.Core;

namespace CR_ImportNamespace
{
  public class CustomVSProjectWrapper : VSProject
  {
    EnvDTE.Project _Project;

    public CustomVSProjectWrapper(EnvDTE.Project project)
      : base(project)
    {
      _Project = project;
    }

    public EnvDTE.Project Project
    {
      get { return _Project; }
    }
  }
}