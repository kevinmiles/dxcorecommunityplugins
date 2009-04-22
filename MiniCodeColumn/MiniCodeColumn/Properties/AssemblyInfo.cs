/*
        Copyright (C) 2009 by Ralf Warnat
        This software is provided "as is" without express or implied warranty of any kind.
        It is labeled as "Works on my machine".        
*/

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DevExpress.CodeRush.Common;

[assembly: DXCoreAssembly(DXCoreAssemblyType.PlugIn, "MiniCodeColumn", PlugInLoadType.Demand, LoadAbilityType.LoadEnabled)]

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("MiniCodeColumn")]
[assembly: AssemblyDescription("DxCore - Plugin for displaying as Code-Preview-Column")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("MiniCodeColumn")]
[assembly: AssemblyCopyright("Copyright ©  2009 by Ralf Warnat")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("307496f2-be98-4407-86db-c48adbcf7e57")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
[assembly: AssemblyVersion("0.0.2.0")]
[assembly: AssemblyFileVersion("0.0.2.0")]
