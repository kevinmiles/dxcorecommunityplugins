# Introduction #

This is a DxCore plugin which will examine the currently loaded class and determine if it should implement IDisposable based on the properties and fields defined on the class.  It uses the new Code Issue capabilities in CodeRush v9.

[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/CR_ShouldBeDisposable/)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)

# Installation #
  1. Make sure all instances of Visual Studio are closed
  1. Copy the CR\_ShouldBeDisposable.dll file into your Community Plugins directory (this is a user configurable location, but defaults to C:\Program Files\DevExpress 2009.1\IDETools\Community\PlugIns)
  1. Restart Visual Studio

# Details #

With the plugin installed and Code Issues enabled, any class which has a field or property that implements System.IDisposable, and does not itself implement System.IDisposable will have a new code issue added at the class level.  Implementing the IDisposable interface will resolve the issue.

### Future Enhancements ###
  * Verify the members implementing IDisposable are actually disposed.
  * Implement a code provider which will implement the IDisposable pattern, and link it to the code issue.

# Credits #
Author: [Casey Kramer](CaseyKramer.md)