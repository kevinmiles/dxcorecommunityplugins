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

    string GetIdentifierAtCaret()
    {
      Word word;
      GetWordResult result = CodeRush.TextViews.Active.GetTokenAt(CodeRush.Caret.SourcePoint, out word);
      if (result == GetWordResult.Success && word.IsIdentifier)
        return word.Text;
      return null;
    }

    void ImportNamespace()
    {
      SolutionElement activeSolution = CodeRush.Source.ActiveSolution;
      if (activeSolution == null || activeSolution.ProjectElements == null || activeSolution.ProjectElements.Count == 0)
        return;

      AssemblyNamespaceList matchingTypes = null;
      string identifierAtCaret = GetIdentifierAtCaret();
      if (!String.IsNullOrEmpty(identifierAtCaret))
      {
        ProjectElement activeProject = CodeRush.Source.ActiveProject;
        if (activeProject != null)
          matchingTypes = GetMatchingTypes(activeProject, identifierAtCaret);
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
            ImportNamespace(envDteProject, matchingTypes[0]);
        }
      });
    }

    static List<AssemblyReference> GetProjectReferences(ProjectElement project)
    {
      if (project == null)
        return null;
      List<AssemblyReference> projectReferences = new List<AssemblyReference>();
      foreach (AssemblyReference reference in project.AssemblyReferences)
      {
        if (!reference.IsProjectReference)
          continue;
        projectReferences.Add(reference);
      }
      return projectReferences;
    }
    static List<AssemblyReference> GetAssemblyReferences(ProjectElement project)
    {
      if (project == null)
        return null;
      List<AssemblyReference> references = new List<AssemblyReference>();
      foreach (AssemblyReference reference in project.AssemblyReferences)
      {
        if (String.IsNullOrEmpty(reference.FilePath))
          continue;
        if (!reference.IsProjectReference)
          references.Add(reference);
      }
      return references;
    }
    static List<AssemblyReference> GetAllProjectReferences(ProjectElement project)
    {
      List<AssemblyReference> collector = new List<AssemblyReference>();
      GetAllProjectReferences(collector, project);
      return collector;
    }
    static bool ContainsReference(List<AssemblyReference> collector, AssemblyReference reference)
    {
      foreach (AssemblyReference item in collector)
        if (item.IsEqual(reference))
          return true;
      return false;
    }
    static ProjectElement GetReferencedProject(AssemblyReference reference)
    {
      if (!reference.IsProjectReference)
        return null;
      return reference.SourceProject;
    }
    static void GetAllProjectReferences(List<AssemblyReference> collector, ProjectElement project)
    {
      List<AssemblyReference> references = GetProjectReferences(project);
      foreach (AssemblyReference reference in references)
      {
        if (ContainsReference(collector, reference))
          continue;
        collector.Add(reference);
        ProjectElement referencedProject = GetReferencedProject(reference);
        GetAllProjectReferences(collector, referencedProject);
      }
    }
    static bool IsProjectReferencedBy(ProjectElement project, List<AssemblyReference> references)
    {
      foreach (AssemblyReference reference in references)
      {
        ProjectElement referencedProject = GetReferencedProject(reference);
        if (referencedProject == project)
          return true;
      }
      return false;
    }
    static ITypeElement[] GetTypesDeclaredInProject(ProjectElement project)
    {
      List<ITypeElement> result = new List<ITypeElement>();
      ProjectSourceModelCache sourceModel = project.SourceModel;
      if (sourceModel == null)
        return result.ToArray();
      IElementCollection allTypes = sourceModel.GetAllTypes();
      AddAllTypes(result, allTypes);
      return result.ToArray();
    }
    static void AddAllTypes(List<ITypeElement> typeList, IElementCollection types)
    {
      foreach (IElement element in types)
      {
        ITypeElement typeElement = element as ITypeElement;
        if (typeElement == null)
          continue;
        typeList.Add(typeElement);
      }
    }
    static ITypeElement[] GetTypesDeclaredInAssembly(AssemblyReference assemblyReference)
    {
      List<ITypeElement> result = new List<ITypeElement>();
      if (assemblyReference == null)
        return result.ToArray();
      MetaDataAssemblyModel assemblyModel = assemblyReference.AssemblyModel as MetaDataAssemblyModel;
      if (assemblyModel == null)
        return result.ToArray();
      
      IElementCollection rootElements = assemblyModel.RootElements;
      foreach (IElement element in rootElements)
      {
        IElementCollection allTypes = ElementCollector.Collect(element, DefaultElementFilters.Type);
        if (allTypes != null)
          AddAllTypes(result, allTypes);
      }
      return result.ToArray();
    }
    static List<ProjectElement> GetProjectsToScanForType(ProjectElement project)
    {
      List<ProjectElement> projectsToGetTypes = new List<ProjectElement>();
      SolutionElement solution = project.Solution as SolutionElement;
      foreach (ProjectElement prj in solution.AllProjects)
      {
        if (prj == project)
          continue;
        List<AssemblyReference> allProjectReferences = GetAllProjectReferences(prj);
        if (IsProjectReferencedBy(project, allProjectReferences))
          continue;
        projectsToGetTypes.Add(prj);
      }
      return projectsToGetTypes;
    }
    static TypeToAssemblyNamespaceMap ScanProjectTypes(List<ProjectElement> projectsToGetTypes)
    {
      TypeToAssemblyNamespaceMap result = new TypeToAssemblyNamespaceMap();
      if (projectsToGetTypes == null)
        return result;

      foreach (ProjectElement projectToGetTypes in projectsToGetTypes)
      {
        ITypeElement[] projectTypes = GetTypesDeclaredInProject(projectToGetTypes);
        if (projectTypes == null)
          continue;

        foreach (ITypeElement typeElement in projectTypes)
        {
          if (typeElement == null)
            continue;

          if (typeElement.ParentNamespace == null)
            continue;
          
          string typeNamespace = typeElement.ParentNamespace.FullName;
          if (String.IsNullOrEmpty(typeNamespace))
            continue;

          AssemblyNamespaceList namespaceList;
          if (!result.TryGetValue(typeElement.Name, out namespaceList))
          {
            namespaceList = new AssemblyNamespaceList();
            result.Add(typeElement.Name, namespaceList);
          }

          AssemblyNamespace nameSpace = new AssemblyNamespace();
          nameSpace.ReferenceProject = projectToGetTypes;
          nameSpace.Namespace = typeNamespace;
          namespaceList.Add(nameSpace);
        }
      }
      return result;
    }
    static TypeToAssemblyNamespaceMap ScanAssemblyReferenceTypes(List<AssemblyReference> assemblyReferences)
    {
      TypeToAssemblyNamespaceMap result = new TypeToAssemblyNamespaceMap();
      if (assemblyReferences == null)
        return result;

      foreach (AssemblyReference assemblyReference in assemblyReferences)
      {
        ITypeElement[] types = GetTypesDeclaredInAssembly(assemblyReference);
        if (types == null)
          continue;

        foreach (ITypeElement typeElement in types)
        {
          if (typeElement == null)
            continue;

          if (typeElement.ParentNamespace == null)
            continue;
          
          string typeNamespace = typeElement.ParentNamespace.FullName;
          if (String.IsNullOrEmpty(typeNamespace))
            continue;

          AssemblyNamespaceList namespaceList;
          if (!result.TryGetValue(typeElement.Name, out namespaceList))
          {
            namespaceList = new AssemblyNamespaceList();
            result.Add(typeElement.Name, namespaceList);
          }

          AssemblyNamespace nameSpace = new AssemblyNamespace();
          nameSpace.AssemblyFilePath = assemblyReference.FilePath;
          nameSpace.Namespace = typeNamespace;
          namespaceList.Add(nameSpace);
        }
      }
      return result;
    }
    static TypeToAssemblyNamespaceMap GetTypeToProjectReferenceNamespaceMap(ProjectElement project)
    {
      if (project == null)
        return null;
      List<ProjectElement> projectsToGetTypes = GetProjectsToScanForType(project);
      return ScanProjectTypes(projectsToGetTypes);
    }

    static TypeToAssemblyNamespaceMap GetTypeToAssemblyReferenceNamespaceMap(ProjectElement project)
    {
      if (project == null)
        return null;
      List<AssemblyReference> assemblyReferences = GetAssemblyReferences(project);
      return ScanAssemblyReferenceTypes(assemblyReferences);
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

      NamespacesResult namespaceResult = FastGetNamespaces(activeProject, typeName);
      if (namespaceResult == null || namespaceResult.State != LoadState.TypeFound)
        return;

      ApplyOperationWithUndoStack(() =>
      {
        AssemblyNamespaceList namespaces = namespaceResult.Namespaces;
        foreach (AssemblyNamespace assemblyNamespace in namespaces)
        {
          if (assemblyNamespace.Namespace == namespaceToImport)
          {
            Project envDteProject = CodeRush.Project.Active;
            ImportNamespace(envDteProject, assemblyNamespace);
            return;
          }
        }
      });
    }
    static void ImportNamespace(Project envDteProject, AssemblyNamespace assemblyNamespace)
    {
      if (envDteProject == null)
        return;

      if (!assemblyNamespace.IsProjectReference)
      {
        string referenceName = assemblyNamespace.GetReferenceName();
        AddReference(envDteProject, referenceName);
      }
      else
        AddProjectReference(envDteProject, assemblyNamespace.ReferenceProject);
      CodeRush.Source.DeclareNamespaceReference(assemblyNamespace.Namespace);
    }

    static NamespacesResult GetNamespaceFromReferencedProjects(ProjectElement project, string typeName)
    {
      NamespacesResult projectTypesResult = new NamespacesResult();
      projectTypesResult.State = LoadState.TypeNotFound;
      TypeToAssemblyNamespaceMap projectsTypeMap = GetTypeToProjectReferenceNamespaceMap(project);
      AssemblyNamespaceList projectsNamespaceList;
      if (projectsTypeMap != null && projectsTypeMap.TryGetValue(typeName, project.IsCaseSensitiveLanguage, out projectsNamespaceList))
      {
        projectTypesResult.State = LoadState.TypeFound;
        projectTypesResult.Namespaces = projectsNamespaceList;
      }
      return projectTypesResult;
    }

    static NamespacesResult GetNamespaceFromReferencedAssemblies(ProjectElement project, string typeName)
    {
      NamespacesResult projectTypesResult = new NamespacesResult();
      projectTypesResult.State = LoadState.TypeNotFound;
      TypeToAssemblyNamespaceMap projectsTypeMap = GetTypeToAssemblyReferenceNamespaceMap(project);
      AssemblyNamespaceList projectsNamespaceList;
      if (projectsTypeMap != null && projectsTypeMap.TryGetValue(typeName, project.IsCaseSensitiveLanguage, out projectsNamespaceList))
      {
        projectTypesResult.State = LoadState.TypeFound;
        projectTypesResult.Namespaces = projectsNamespaceList;
      }
      return projectTypesResult;
    }

    static NamespacesResult FastGetNamespaces(ProjectElement project, string typeName)
    {
      NamespacesResult result = new NamespacesResult();
      result.State = LoadState.NoActiveProject;
      if (project == null)
        return result;

      NamespacesResult projectTypesResult = GetNamespaceFromReferencedProjects(project, typeName);

      ExtendedFrameworkVersion frameworkVersion = GetExtendedFrameworkVersion(project);
      NamespacesResult dotNetTypesResult = _DotNetTypes.FastGetNamespaces(typeName, project.IsCaseSensitiveLanguage, frameworkVersion);

      NamespacesResult projectAndDotNetTypes = CombineResults(projectTypesResult, dotNetTypesResult);

      NamespacesResult referencesAssembliesTypesResult = GetNamespaceFromReferencedAssemblies(project, typeName);

      return projectAndDotNetTypes = CombineResults(projectAndDotNetTypes, referencesAssembliesTypesResult);
    }

    static NamespacesResult CombineResults(NamespacesResult first, NamespacesResult second)
    {
      NamespacesResult combinedResult = new NamespacesResult();
      if (first.State == LoadState.TypeFound || second.State == LoadState.TypeFound)
        combinedResult.State = LoadState.TypeFound;

      if (combinedResult.State != LoadState.TypeFound)
        if (first.State == LoadState.FrameworkNotLoaded || second.State == LoadState.FrameworkNotLoaded)
          combinedResult.State = LoadState.FrameworkNotLoaded;

      AssemblyNamespaceList combinedList = new AssemblyNamespaceList();
      combinedList.AddUnique(first.Namespaces);
      combinedList.AddUnique(second.Namespaces);

      combinedResult.Namespaces = combinedList;

      return combinedResult;
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
      AddAssemblyOrProjectReference(project, GetShortAssemblyName(assemblyName), false);
    }
    static void AddProjectReference(Project project, ProjectElement referencedProject)
    {
      if (project == null || referencedProject == null)
        return;
      AddAssemblyOrProjectReference(project, referencedProject.Name, true);
    }

    static void AddAssemblyOrProjectReference(Project project, string reference, bool isProjectReference)
    {
      if (String.IsNullOrEmpty(reference))
        return;

      VSProject vsProject = project as VSProject;
      if (vsProject == null)
        return;

      if (vsProject.HasReference(reference))
        return;

      if (isProjectReference)
        vsProject.AddProjectReference(reference);
      else
        project.AddReference(reference);

      CustomAddedAssemblyReferenceUndoUnit undo = new CustomAddedAssemblyReferenceUndoUnit(project.Name, reference);
      CodeRush.UndoStack.Add(undo);
    }

    static AssemblyNamespaceList GetMatchingTypes(ProjectElement project, string typeName)
    {
      bool caseSensitive = project.IsCaseSensitiveLanguage;
      ExtendedFrameworkVersion frameworkVersion = GetExtendedFrameworkVersion(project);

      NamespacesResult projectTypesResult = GetNamespaceFromReferencedProjects(project, typeName);
      if (projectTypesResult.State == LoadState.TypeFound)
        return projectTypesResult.Namespaces;

      IAssemblyPathsProvider pathsProvider = new DefaultAssemblyPathsProvider();
      TypeToAssemblyNamespaceMap dotNetTypesInThisFramework = _DotNetTypes.GetTypeToAssemblyMap(pathsProvider, frameworkVersion);
      if (!dotNetTypesInThisFramework.ContainsKey(typeName, caseSensitive))
      {
        NamespacesResult referencesAssembliesTypesResult = GetNamespaceFromReferencedAssemblies(project, typeName);
        if (referencesAssembliesTypesResult.State == LoadState.TypeFound)
          return referencesAssembliesTypesResult.Namespaces;
      }
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

    bool IsCaretOnDeclaredType(CheckContentAvailabilityEventArgs ea, out string typeName)
    {
      typeName = null;
      IReferenceExpression referenceExpression = ea.Element as IReferenceExpression;
      if (referenceExpression != null)
      {
        IElement declaration = referenceExpression.GetDeclaration();
        if (declaration != null)
          return true;
        typeName = ea.Element.Name;
      }
      else
      {
        typeName = GetIdentifierAtCaret();
        if (String.IsNullOrEmpty(typeName))
          return true;
        IElement declaration = ParserServices.SourceTreeResolver.ResolveType(ea.Element, typeName);
        if (declaration != null)
          return true;
      }
      return false;
    }

    // event handlers...
    void actImportNamespace_Execute(ExecuteEventArgs ea)
		{
			ImportNamespace();
		}
    void cpImportNamespace_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
    {
      if (ea.Element == null)
        return;

      ProjectElement project = ea.Element.Project as ProjectElement;
      if (project == null)
        return;

      string typeName;
      if (IsCaretOnDeclaredType(ea, out typeName))
        return;

      TryLoadTypesCache();

      NamespacesResult namespaceResult = FastGetNamespaces(project as ProjectElement, typeName);
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