using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DevExpress.CodeRush.Common;

[assembly: DXCoreAssembly(DXCoreAssemblyType.PlugIn, "RedGreen", PlugInLoadType.Demand)]

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("RedGreen")]
[assembly: AssemblyDescription("A unit test runner that reports test status in the editor")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("RedGreen")]
[assembly: AssemblyCopyright("Copyright ©  2008 Renaissance Learning, Inc. and James Argeropoulos")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("a69c0ff5-9a06-4cff-a6bd-b947d7e7cf19")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
[assembly: AssemblyVersion("1.1.0.0")]
[assembly: AssemblyFileVersion("1.1.0.0")]

[assembly: InternalsVisibleTo("RedGreenTests")]
