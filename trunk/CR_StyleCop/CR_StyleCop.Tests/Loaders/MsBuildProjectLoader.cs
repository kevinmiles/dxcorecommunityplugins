using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using DevExpress.CodeRush.StructuralParser;
using Microsoft.Build.BuildEngine;

namespace CR_StyleCop.Tests.Loaders
{
    public class MsBuildProjectLoader
    {
        Project _Project;

        public MsBuildProjectLoader(string prjPath)
        {
            LoadProjectFromFile(prjPath);
        }

        // private methods...
        IEnumerable<BuildItemGroup> GetBuildItemGroup(string[] itemTypes)
        {
            List<BuildItemGroup> result = new List<BuildItemGroup>();
            if (itemTypes == null || itemTypes.Length == 0)
            {
                result.Add(Project.EvaluatedItems);
                return result;
            }

            foreach (string itemType in itemTypes)
                result.Add(Project.GetEvaluatedItemsByName(itemType));
            return result;
        }

        string GetPath(Assembly assembly)
        {
            string result = new Uri(assembly.CodeBase).LocalPath;
            return result;
        }
        string GetDotNetRoot()
        {
            return Path.GetDirectoryName(GetPath(typeof(int).Assembly));
        }

        // public methods...
        public void LoadProjectFromFile(string prjPath)
        {
            if (prjPath == null)
                throw new ArgumentNullException("prjPath");

            string msBuildPath = GetDotNetRoot();
            Engine engine = new Engine(msBuildPath);
            _Project = new Project(engine);
            _Project.Load(prjPath);
        }

        public IEnumerable<string> GetProjectFiles(params string[] itemTypes)
        {
            foreach (BuildItem item in GetProjectItems(itemTypes))
                yield return item.FinalItemSpec;
        }

        public IEnumerable<BuildItem> GetProjectItems(params string[] itemTypes)
        {
            IEnumerable<BuildItemGroup> groups = GetBuildItemGroup(itemTypes);
            foreach (BuildItemGroup group in groups)
                foreach (BuildItem item in group)
                    yield return item;
        }

        public string GetPropertyWithCondition(string condition, string propertyName)
        {
            BuildPropertyGroupCollection groups = Project.PropertyGroups;
            if (groups == null || groups.Count == 0)
                return string.Empty;

            foreach (BuildPropertyGroup group in groups)
            {
                if (group.Condition == condition)
                {
                    foreach (BuildProperty property in group)
                    {
                        if (property.Name == propertyName)
                            return property.Value;
                    }
                }
            }
            return string.Empty;
        }

        public string GetProperty(string name)
        {
            return Project.GetEvaluatedProperty(name);
        }

        // public properties...
        public Project Project
        {
            get { return _Project; }
        }

        public string AssemblyName
        {
            get { return GetProperty("AssemblyName"); }
        }

        public string RootNamespace
        {
            get { return GetProperty("RootNamespace"); }
        }
        public FrameworkVersion TargetFramework
        {
            get
            {
                string targetFramework = GetProperty("TargetFrameworkVersion");
                switch (targetFramework)
                {
                    case "v2.0":
                        return FrameworkVersion.Version20;
                    case "v3.0":
                        return FrameworkVersion.Version30;
                    case "v3.5":
                        return FrameworkVersion.Version35;
                    case "v4.0":
                        return FrameworkVersion.Version40;
                    default:
                        return FrameworkVersion.Unknown;
                }
            }
        }
        public bool NotImportStdLib
        {
            get
            {
                string noStdLib = GetProperty("NoStdLib");
                return string.Compare(noStdLib, "true", true) == 0;
            }
        }
        public OptionStrict OptionStrict
        {
            get
            {
                string value = GetProperty("OptionStrict");
                if (value == null)
                    return OptionStrict.Off;
                return value.ToLower() == "off" ? OptionStrict.Off : OptionStrict.On;
            }
        }
    }
}