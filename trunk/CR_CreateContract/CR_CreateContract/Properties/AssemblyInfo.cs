//-----------------------------------------------------------------------
// <copyright file="AssemblyInfo.cs" company="Jim Counts">
//     Copyright (c) Jim Counts 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using DevExpress.CodeRush.Common;

[assembly: DXCoreAssembly(DXCoreAssemblyType.PlugIn, "CR_CreateContract", PlugInLoadType.Demand, LoadAbilityType.LoadEnabled)]

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("CR_CreateContract")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Jim Counts")]
[assembly: AssemblyProduct("CR_CreateContract")]
[assembly: AssemblyCopyright("Copyright © Jim Counts 2011")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("44487997-cbeb-4c70-a5ac-aadfe849c7b7")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
[assembly: AssemblyVersion("1.0.*")]
[assembly: NeutralResourcesLanguageAttribute("en-US")]
[module: SuppressMessage("Microsoft.Design", "CA1014:MarkAssembliesWithClsCompliant", Justification = "Depends on libraries that are not compliant.")]
[module: SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames", Justification = "PlugIn should not be placed in the GAC.")]
[module: SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Justification = "Name follows PlugIn naming convention.")]
[module: SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores", Scope = "namespace", Target = "CR_CreateContract", Justification = "Name follows PlugIn naming convention.")]
[module: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "CR_CreateContract", Justification = "Types must be in thier own namespace to avoid conflicts with other PlugIns.")]
