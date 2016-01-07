[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/CR_NavigateToDefinition/)            [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)

#### Requires DXCore 10.2 since build 1510 ####
#### Requires DXCore 10.1 since build 1257 ####

# Introduction #

Something I found really annoying is that when you have references to assemblies, the "Go To Definition" command of visual studio, takes you to the metadata instead of the actual code. This plugin tries to solve that problem.

You can bind a key (or just use the Navigate menu) to navigate to the definition of a type, member or variable, and the plugin is going to search the code of all the projects loaded into the solution, if it finds the definition, opens the source code, if it doesn't, then the standard visual studio "Go To Definition" command will be executed.

This plugin could help until DevExpress implements [this suggestion made by Rory](http://www.devexpress.com/Support/Center/p/S131637.aspx) (that should be the ultimate solution for the "Go To Definition" metadata problem!)

# Configuration #
  * Assign the 'NavigateToDefinition' action to a key. (See [Bind a Key in CodeRush](http://rorybecker.blogspot.com/2009/08/how-to-bind-key-in-coderush.html))
  * Change the settings as needed in DevExpress options -> Editor -> Navigation -> Navigate To Definition

# Screenshots #

![http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_NavigateToDefinition/CR_NavigateToDefinition/images/options_page.png](http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_NavigateToDefinition/CR_NavigateToDefinition/images/options_page.png)

![http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_NavigateToDefinition/CR_NavigateToDefinition/images/jump_to.png](http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_NavigateToDefinition/CR_NavigateToDefinition/images/jump_to.png)

![http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_NavigateToDefinition/CR_NavigateToDefinition/images/jump_to_navigate.png](http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_NavigateToDefinition/CR_NavigateToDefinition/images/jump_to_navigate.png)

Original Author: **[Jorge Rowies](http://code.google.com/p/dxcorecommunityplugins/wiki/JorgeRowies)**