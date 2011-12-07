using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_ImportNamespace
{
	public partial class PlugIn1 : StandardPlugIn
	{
    const string STR_ImportNamespace = "Import Namespace";

    readonly static DotNetTypesCache _DotNetTypes = new DotNetTypesCache();
    List<ISmartTagItem> _ImportNamespaceItems;

		// DXCore-generated code...
		#region InitializePlugIn
		public override void InitializePlugIn()
		{
			base.InitializePlugIn();
		}
		#endregion
		#region FinalizePlugIn
		public override void FinalizePlugIn()
		{
			base.FinalizePlugIn();
		}
		#endregion

    // private methods...
    List<ISmartTagItem> GetImportNamespaceSmartTagItems(AssemblyNamespaceList assemblyNamespaces)
    {
      List<ISmartTagItem> result = new List<ISmartTagItem>();
      foreach (AssemblyNamespace assemblyNamespace in assemblyNamespaces)
      {
        ImportNamespaceSmartTagItem item = new ImportNamespaceSmartTagItem(assemblyNamespace);
        item.Execute += delegate(object sender, EventArgs e)
        {
          Project envDteProject = CodeRush.Project.Active;
          if (envDteProject == null)
            return;
          AssemblyNamespace itemAssemblyNamespace = item.AssemblyNamespace;
          AddReference(envDteProject, itemAssemblyNamespace.Assembly);
          CodeRush.Source.DeclareNamespaceReference(itemAssemblyNamespace.Namespace);
        };
        result.Add(item);
      }
      return result;
    }
    void ShowImportNamespaceSmartTagMenu(AssemblyNamespaceList assemblyNamespaces)
    {
      _ImportNamespaceItems = GetImportNamespaceSmartTagItems(assemblyNamespaces);
      SynchronizationContext.Current.Post(delegate(object state)
      {
        Point point = CodeRush.SmartTags.GetPopupMenuPoint();
        TextView textView = CodeRush.TextViews.Active;
        if (textView != null)
          point.Offset(0, textView.LineHeight);

        CodeRush.SmartTags.ShowPopupMenu(point, stImportNamespace);
      }, null);
    }

    static EnvDTE.Property GetProperty(ProjectElement project, string name)
    {
      try
      {
        EnvDTE.Project envDteProject = CodeRush.Solution.FindEnvDTEProject(project.Name);
        if (envDteProject == null)
          return null;

        EnvDTE.Properties properties = envDteProject.Properties;
        if (properties == null)
          return null;

        return properties.Item(name);
      }
      catch
      {
        return null;
      }
    }
    static string GetTargetFrameworkMonikerPropertyValue(ProjectElement project)
    {
      const string STR_TargetFrameworkMoniker = "TargetFrameworkMoniker";
      try
      {
        EnvDTE.Property property = GetProperty(project, STR_TargetFrameworkMoniker);
        if (property == null)
          return null;

        return property.Value as string;
      }
      catch
      {
        return null;
      }
    }

    static bool IsClientProfileFramework(ProjectElement project)
    {
      string moniker = GetTargetFrameworkMonikerPropertyValue(project);
      if (String.IsNullOrEmpty(moniker))
        return false;
      return moniker.Contains("Client");
    }

    static bool IsWindowsPhoneProfile(ProjectElement project)
    {
      string moniker = GetTargetFrameworkMonikerPropertyValue(project);
      if (String.IsNullOrEmpty(moniker))
        return false;
      return moniker.Contains("WindowsPhone");
    }

    static ExtendedFrameworkVersion GetExtendedFrameworkVersion(ProjectElement project)
    {
      FrameworkVersion version = project.TargetFramework;
      ExtendedFrameworkVersion extVersion = ExtendedFrameworkVersionUtil.FromFrameworkVersion(version);
      if (IsClientProfileFramework(project))
        extVersion = ExtendedFrameworkVersionUtil.GetClientProfileVersion(extVersion);
      if (project.IsSilverlightProject)
        extVersion = ExtendedFrameworkVersionUtil.GetSilverlightVersion(extVersion);
      if (IsWindowsPhoneProfile(project))
        extVersion = ExtendedFrameworkVersionUtil.GetWindowsPhoneProfileVersion(extVersion);
      return extVersion;
    }

    void ImportNamespace()
    {
      SolutionElement activeSolution = CodeRush.Source.ActiveSolution;
      if (activeSolution == null || activeSolution.ProjectElements == null || activeSolution.ProjectElements.Count == 0)
        return;

      Word word;
      GetWordResult result = CodeRush.TextViews.Active.GetTokenAt(CodeRush.Caret.SourcePoint, out word);
      AssemblyNamespaceList matchingTypes = null;

      if (result == GetWordResult.Success && word.IsIdentifier)
      {
        ProjectElement activeProject = CodeRush.Source.ActiveProject;

        if (activeProject != null)
          matchingTypes = GetMatchingTypes(GetExtendedFrameworkVersion(activeProject), word.Text, activeProject.IsCaseSensitiveLanguage);
      }

      if (matchingTypes == null || matchingTypes.Count <= 0)
        return;

      ApplyOperationWithUndoStack(() =>
      {
        Project envDteProject = CodeRush.Project.Active;
        if (envDteProject != null)
        {
          if (matchingTypes.Count > 1)
            ShowImportNamespaceSmartTagMenu(matchingTypes);
          else
          {
            AddReference(envDteProject, matchingTypes[0].Assembly);
            CodeRush.Source.DeclareNamespaceReference(matchingTypes[0].Namespace);
          }
        }
      });
    }

    // private static methods...
    static void ApplyOperationWithUndoStack(System.Action action)
    {
      if (action == null)
        return;
      CodeRush.UndoStack.BeginUpdate(STR_ImportNamespace);
      try
      {
        action();
      }
      finally
      {
        CodeRush.UndoStack.EndUpdate();
      }
    }
    static void ImportNamespace(string typeName, string namespaceToImport)
    {
      ProjectElement activeProject = CodeRush.Source.ActiveProject;
      if (activeProject == null)
        return;

      ExtendedFrameworkVersion frameworkVersion = GetExtendedFrameworkVersion(activeProject);
      if (!_DotNetTypes.ContainsKey(frameworkVersion))
        return;

      TypeToAssemblyNamespaceMap knownTypes = _DotNetTypes[frameworkVersion];
      if (!knownTypes.ContainsKey(typeName, activeProject.IsCaseSensitiveLanguage))
        return;

      ApplyOperationWithUndoStack(() =>
      {
        AssemblyNamespaceList namespaces = knownTypes.GetNamespaceList(typeName);
        foreach (AssemblyNamespace assemblyNamespace in namespaces)
        {
          if (assemblyNamespace.Namespace == namespaceToImport)
          {
            Project envDteProject = CodeRush.Project.Active;
            if (envDteProject != null)
            {
              AddReference(envDteProject, assemblyNamespace.Assembly);
              CodeRush.Source.DeclareNamespaceReference(assemblyNamespace.Namespace);
            }
            return;
          }
        }
      });
    }

    static NamespacesResult FastGetNamespaces(string typeName)
    {
      NamespacesResult result = new NamespacesResult();
      result.State = LoadState.NoActiveProject;

      ProjectElement activeProject = CodeRush.Source.ActiveProject;
      if (activeProject == null)
        return result;

      ExtendedFrameworkVersion frameworkVersion = GetExtendedFrameworkVersion(activeProject);
      return _DotNetTypes.FastGetNamespaces(typeName, activeProject.IsCaseSensitiveLanguage, frameworkVersion);
    }

    static string GetShortAssemblyName(string assemblyName)
    {
      if (String.IsNullOrEmpty(assemblyName))
        return null;
      int commaIndex = assemblyName.IndexOf(",");
      if (commaIndex >= 0)
        return assemblyName.Substring(0, commaIndex);
      return assemblyName;
    }
    static void AddReference(Project project, string assemblyName)
    {
      if (project == null || String.IsNullOrEmpty(assemblyName))
        return;

      VSProject vsProject = project as VSProject;
      if (vsProject == null)
        return;

      string shortAssemblyName = GetShortAssemblyName(assemblyName);
      if (String.IsNullOrEmpty(shortAssemblyName))
        return;

      if (vsProject.HasReference(shortAssemblyName))
        return;

      project.AddReference(assemblyName);
      CustomAddedAssemblyReferenceUndoUnit undo = new CustomAddedAssemblyReferenceUndoUnit(project.Name, shortAssemblyName);
      CodeRush.UndoStack.Add(undo);
    }

    static AssemblyNamespaceList GetMatchingTypes(ExtendedFrameworkVersion frameworkVersion, string typeName, bool caseSensitive)
    {
      IAssemblyPathsProvider pathsProvider = new DefaultAssemblyPathsProvider();
      TypeToAssemblyNamespaceMap dotNetTypesInThisFramework = _DotNetTypes.GetTypeToAssemblyMap(pathsProvider, frameworkVersion);
      if (!dotNetTypesInThisFramework.ContainsKey(typeName, caseSensitive))
        return null;
      return dotNetTypesInThisFramework.GetNamespaceList(typeName, caseSensitive);
    }

    static void TryLoadTypesCache()
    {
      SolutionElement activeSolution = CodeRush.Source.ActiveSolution;
      if (activeSolution == null || activeSolution.ProjectElements == null || activeSolution.ProjectElements.Count == 0)
        return;

      ProjectElement activeProject = CodeRush.Source.ActiveProject;
      if (activeProject != null)
      {
        IAssemblyPathsProvider pathsProvider = new DefaultAssemblyPathsProvider();
        ExtendedFrameworkVersion frameworkVersion = GetExtendedFrameworkVersion(activeProject);
        _DotNetTypes.TryLoadTypesFromCache(frameworkVersion);
        
        // The following line is for rescanning of changed or new assemblies
        _DotNetTypes.ReScanAssemblies(pathsProvider, frameworkVersion);
      }
    }

    // event handlers...
    void actImportNamespace_Execute(ExecuteEventArgs ea)
		{
			ImportNamespace();
		}
    void cpImportNamespace_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
    {
      TypeReferenceExpression typeReferenceExpression = ea.Element as TypeReferenceExpression;
      if (typeReferenceExpression == null)
        return;

      IElement declaration = typeReferenceExpression.GetDeclaration();
      string typeName = ea.Element.Name;
      if (declaration != null)
        return;

      TryLoadTypesCache();

      NamespacesResult namespaceResult = FastGetNamespaces(typeName);
      LoadState state = namespaceResult.State;
      AssemblyNamespaceList namespaces = namespaceResult.Namespaces;
      if (state == LoadState.NoActiveProject || state == LoadState.TypeNotFound)
        return;

      if (state == LoadState.TypeFound)
        if (namespaces.Count == 1)
          ea.MenuCaption = String.Format("Import Namespace ({0})", namespaces[0].Namespace);
        else
          foreach (AssemblyNamespace assemblyNamespace in namespaces)
            ea.AddSubMenuItem(assemblyNamespace.Namespace);
      else // state == State.FrameworkNotLoaded
        ea.MenuCaption = "Import Namespace...";

      ea.Available = true;
    }

		void cpImportNamespace_Apply(object sender, ApplyContentEventArgs ea)
		{
			DevExpress.Refactor.Core.SubMenuItem selectedSubMenuItem = ea.SelectedSubMenuItem;
      if (selectedSubMenuItem == null)
        SynchronizationContext.Current.Post(state => ImportNamespace(), null);
			else
				ImportNamespace(ea.Element.Name, selectedSubMenuItem.Caption);
		}

    void stImportNamespace_GetSmartTagItems(object sender, GetSmartTagItemsEventArgs ea)
    {
      ea.AddRange(_ImportNamespaceItems.ToArray());
      _ImportNamespaceItems = null;
    }
    void stImportNamespace_GetSmartTagItemColors(object sender, GetSmartTagItemColorsEventArgs ea)
    {
      ea.PopupMenuColors = new CodePopupMenuColors();
    }
	}
}