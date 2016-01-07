[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/CR_ExtractHqlNamedQuery/)            [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)

#### Requires DXCore 10.1 since build 1256 ####

# Introduction #

Details about the features of this plugin can be found [here](http://jorgerowies.blogspot.com/2010/03/coderush-plugin-to-extract-nhibernates.html).

Note for CodeRush plugin developers:

A secondary purpose of this plugin is to try to show how to link two (or more) pieces of source code in a way that changing one of them triggers the change to the others, like when you rename a variable with refactor/rename (F2) and all the references of that variable are renamed accordingly.

I did some research about how the renaming was done in CodeRush plugin Refactor\_Renaming.dll and, taking some code from here and there, I came up with the following class:

http://code.google.com/p/dxcorecommunityplugins/source/browse/trunk/CR_ExtractHqlNamedQuery/CR_ExtractHqlNamedQuery/LinkedTextHelper.cs

Basically, to use this class you can do something like this:

```
          FileSourceRangeCollection collection = new FileSourceRangeCollection();
          collection.Add(
            new FileSourceRange(
              CodeRush.Source.Active.FileNode, 
              new SourceRange(1, 1, 1, 6))); //<- selects the first 5 characters in first line
          collection.Add(
            new FileSourceRange(
              CodeRush.Source.Active.FileNode,
              new SourceRange(2, 1, 2, 6))); //<- selects the first 5 characters in second line

          RefactoringContext context = new RefactoringContext(ea);
          LinkedTextHelper.ApplyRename(context, collection);
```

This will result in the following two pieces of code synchronized

![http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_ExtractHqlNamedQuery/CR_ExtractHqlNamedQuery/images/screenshot6.png](http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_ExtractHqlNamedQuery/CR_ExtractHqlNamedQuery/images/screenshot6.png)

# Configuration #
  * Assign the 'ExtractHqlNamedQuery' action to a key. (See [Bind a Key in CodeRush](http://rorybecker.blogspot.com/2009/08/how-to-bind-key-in-coderush.html))
  * Change the settings as needed in DevExpress options -> Editor -> Refactoring -> Extract Hql Named Query

Original Author: **[Jorge Rowies](http://code.google.com/p/dxcorecommunityplugins/wiki/JorgeRowies)**