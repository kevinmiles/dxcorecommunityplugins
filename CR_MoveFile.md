[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/CR_MoveFile/)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)

# Introduction #

This plug-in is pretty self-explanatory.  It moves a file from one location within the solution to another, adding project references as needed.

# Installation #

Installation instructions can be found on the InstallInstructions page.

# Setup #
There is no additional setup needed.

# Usage #

When installed, you will see a "Move File" menu option in the CodeRush Code menu.  When you select this, you are then presented with a tree-view of the Solution's Project/Directory structure.  You then select the target directory you would like to move the file to (using keyboard arrow keys, or the mouse), and the plug-in will move the file for you.  If you are moving between projects, it will remove the file from the project it started in, and add it to the new project.  It will also add project references to the new project to any projects which were referencing objects defined in the file.


# Why did I build this #

This is primarily a way to re-structure projects without needing to use the mouse, since I am lazy.  It also allows me to use a more "pure" approach to TDD, so I can create a non-existing class declaration, and then use CodeRush to create the class, move it to it's own file, and finally move it to the project it belongs.

# Credits #

Original Author: [Casey Kramer](CaseyKramer.md)