[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/Refactor_Resolve/)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)
#### Requires DXCore 10.2 since build 1544 ####
#### Requires DXCore 10.1 since build 1264 ####
# Introduction #

Sometimes you know the name of the object you need, but you don't know the correct namespace.
This plugin tries to find the namespace you need and adds it to the imports (using) section.

This plugin is almost the same as the built-in functionality in VS, but the build in functionality in VS has some shortcomings:
  * When in C#, VS2005 is case sensitive, and can only find a exact match.
  * When in VB.NET, VS2005 can find the right namespace (case insensitive), but can not add a imports statement.
  * In C# in VS2005 it only works for declaring a variable, not for method parameters.
  * In C# and VB.NET in VS2005 it does not work when using the name of the class and a shared (static) method like:
```
regex.IsMatch(...
```

#### Usage ####

Once the plugin is installed, a extra settings page is added to the settings screen of CodeRush.

The plugins tries to resolve all classes, everything that can not be resolved, you can invoke the Refactor menu. There will be a extra menu item "Resolve" that tries to find the unresolved class in the referenced assemblies and all projects in the solution.

When nothing is found the user will be asked the plugins needs to search the GAC also. For performance reasons not the complete GAC is searched, only the Microsoft assemblies are searched.

#### Credits ####

Author: Koen Hoefkens