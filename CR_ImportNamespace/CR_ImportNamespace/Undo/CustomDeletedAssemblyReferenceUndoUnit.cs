using System;
using DevExpress.CodeRush.Interop.OLE.Helpers;

namespace CR_ImportNamespace
{
  public class CustomDeletedAssemblyReferenceUndoUnit : BaseAssemblyReferenceUndoUnit
	{
		// constructors...
    public CustomDeletedAssemblyReferenceUndoUnit(string projectName, string assemblyName)
      : base(projectName, assemblyName)
		{
		}
  
		// protected methods...
    protected override void Execute(CustomVSProjectWrapper projectWrapper)
    {
      projectWrapper.AddReference(AssemblyName);
    }
		protected override UndoUnit ReverseUnit()
		{
      return new CustomAddedAssemblyReferenceUndoUnit(ProjectName, AssemblyName);
		}

		// protected properties...
		protected override string Description
		{
      get { return String.Format("Deleted {0} assembly reference from {1}", AssemblyName, ProjectName); }
		}
	}
}