using System;
using DevExpress.CodeRush.Interop.OLE.Helpers;
using DevExpress.Refactor.Diagnostics;

using DXCoreEngine = DevExpress.CodeRush.Core.CodeRush;

namespace CR_ImportNamespace
{
  public abstract class BaseAssemblyReferenceUndoUnit : UndoUnit
  {
    // private fields...
    string _ProjectName;
    string _AssemblyName;

    // constructors...
    public BaseAssemblyReferenceUndoUnit(string projectName, string assemblyName)
    {
      _ProjectName = projectName;
      _AssemblyName = assemblyName;
    }
    
    protected virtual string GetOperationExceptionMessage(Exception ex)
    {
      return String.Format("Could not apply {0} operation for the {1} assembly reference due to {2} exception", GetType().Name, _AssemblyName, ex.Message);
    }

    protected virtual void LogOperationException(Exception ex)
    {
      string logMessage = GetOperationExceptionMessage(ex);
      Log.SendWarningWithStackTrace(logMessage);
    }

    protected virtual void Execute(CustomVSProjectWrapper projectWrapper)
    {
    }

    protected override void Execute()
    {
      EnvDTE.Project project = DXCoreEngine.Solution.FindEnvDTEProject(_ProjectName);
      if (project == null)
        return;

      CustomVSProjectWrapper wrapper = new CustomVSProjectWrapper(project);
      try
      {
        Execute(wrapper);
      }
      catch (Exception ex)
      {
        LogOperationException(ex);
      }
    }

    public string ProjectName
    {
      get { return _ProjectName; }
    }

    public string AssemblyName
    {
      get { return _AssemblyName; }
    }
  }
}