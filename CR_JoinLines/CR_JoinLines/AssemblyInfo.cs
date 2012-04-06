using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using DevExpress.CodeRush.Common;

[assembly: DXCoreAssembly(DXCoreAssemblyType.PlugIn, "CR_JoinLines")]
[assembly: ComVisible(false)]
[assembly: SecurityTransparent]
[assembly: SecurityRules(SecurityRuleSet.Level2)]

[assembly: AssemblyTitle("CR_JoinLines")]
[assembly: AssemblyDescription("DXCore plug-in providing a command to join text lines in the editor")]
[assembly: AssemblyProduct("DXCore for Visual Studio .NET")]
[assembly: AssemblyCopyright("Copyright © 2005 Travis Illig")]
[assembly: AssemblyVersion("2.0.0.0")]
