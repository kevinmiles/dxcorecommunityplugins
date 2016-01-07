[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/CR_UnitTestsRunClassTests/)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)
##### Built using DXCore 11.2 #####
### Introduction ###

This plugin provides 2 new actions which may be bound to keys.

These actions are **UnitTestsRunClassTests** and **UnitTestsDebugClassTests**.

These function similarly to the other UnitTestsXXXXX actions, in that a given set of tests are either run or debugged depending on the action invoked.

This plugin will look for a class named similarly to the active class but with a suffix of `"_Tests"`. It will then run or debug all tests within that class.

So for example... when invoked from within a class 'MyClass', the plugin will look for a class called 'MyClass\_Tests' and execute any tests within that.

The plugin will look for this class in several locations.
```
 - [OriginalNamespace].[SourceClass]_Tests (Original Project)
 - [OriginalNamespace].[SourceClass]_Tests (Other Projects)
 - [InitialOriginalNamespace]_Tests.[RemainingNamespace].[SourceClass]_Tests (Original Project)
 - [InitialOriginalNamespace]_Tests.[RemainingNamespace].[SourceClass]_Tests (Other Projects)
```
If none of these are found, the plugin will search all classes in all solutions, looking for the first one called `[SourceClass]_Tests`

The first of these, if any, to be found will have any tests found within it executed.

### Configuration ###
Simply [add bindings](http://community.devexpress.com/blogs/rorybecker/archive/2010/10/05/binding-keys-in-coderush.aspx) to **UnitTestsRunClassTests** and **UnitTestsDebugClassTests**

**Note** Since build 2174, there is now a configuration page in the option sscreen.

Using this page you may configure alternative values for the **Test Class Suffix** and the **Test Namespace Suffix**

### Usage ###

Invoke the provided actions using the bound shortcuts from within a class which has a corresponding `_Test` class

### Credits ###

Author: RoryBecker