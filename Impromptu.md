[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/Impromptu/)  [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Screencast.png)](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Impromptu/Screenshots/ImpromptuInAction.swf)    [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
##### Requires DXCore 10.2 since build 1540 #####
# Introduction #

I used to use TestDriven.Net before it required a license purchase. I loved it's Ad Hoc test run feature. This plug-in reproduces the behavior without a right click.

# Details #

When you create a method that takes no parameters, Impromptu will place a gear icon to the left of the method signature.

![http://dxcorecommunityplugins.googlecode.com/svn/trunk/Impromptu/Screenshots/ImpromptuIcon.png](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Impromptu/Screenshots/ImpromptuIcon.png)

If you click on the icon, Impromptu will use reflection to execute your method. The results are put into an output window and the window is automatically displayed.

![http://dxcorecommunityplugins.googlecode.com/svn/trunk/Impromptu/Screenshots/ImpromptuResults.png](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Impromptu/Screenshots/ImpromptuResults.png)

The first line displays details about what method was run.
Any following lines will be the results of your method. Results come from two places: First, anything your method writes to the Console are captured. Second, if your method has a return value, it is written using the default ToString() method.

Plug-in by Jim Argeropoulos