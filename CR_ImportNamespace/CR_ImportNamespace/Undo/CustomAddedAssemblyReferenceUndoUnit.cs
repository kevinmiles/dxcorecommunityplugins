using System;
using DevExpress.CodeRush.Interop.OLE.Helpers;

namespace CR_ImportNamespace
{
  public class CustomAddedAssemblyReferenceUndoUnit : BaseAssemblyReferenceUndoUnit
	{
		// constructors...
    public CustomAddedAssemblyReferenceUndoUnit(string projectName, string assemblyName)
      : base(projectName, assemblyName)
		{
		}
  
		// protected methods...
    protected override void Execute(CustomVSProjectWrapper projectWrapper)
    {
      projectWrapper.RemoveReference(AssemblyName);
    }
		protected override UndoUnit ReverseUnit()
		{
			return new CustomDeletedAssemblyReferenceUndoUnit(ProjectName, AssemblyName);
		}

		// protected properties...
		protected override string Description
		{
      get { return String.Format("Added {0} assembly reference into {1}", AssemblyName, ProjectName); }
		}
	}
}