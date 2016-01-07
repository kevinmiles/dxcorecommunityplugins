[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/CR_NavigateToTest/)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)
##### Requires DXCore 10.2 since build 1538 #####
# Introduction #

This plugin provides a new Navigation Item (in the Jump To) menu which lists all test classes that reference either the current class, or the type reference under the cursor (so you can right click on a reference to a class, and see where it is being tested).  Test classes are determined by looking at all of the references for the class being searched for, and returning only those that have appropriate test class attributes.  This should work with NUnit, MbUnit, and MSTest.  Currently, xUnit.Net is not supported, since it does not use a class level attribute to identify tests.

**Update:** I've changed the text displayed in the menu to (hopefully) make it easier to find among the navigation options.  Now the menu displays "Test ⇒ ClassName".  I've also modified the behavior for cases where there is a single test.  The plugin will now navigate directly to the test when the menu item is selected (rather than showing the class/method picker).

**Invoke Jump-To menu:**

![http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_NavigateToTest/ScreenShots/NavToTest-ScreenShot%20-%200.jpg](http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_NavigateToTest/ScreenShots/NavToTest-ScreenShot%20-%200.jpg)

**Select Tests Menu Item:**

![http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_NavigateToTest/ScreenShots/NavToTest-ScreenShot%20-%201.jpg](http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_NavigateToTest/ScreenShots/NavToTest-ScreenShot%20-%201.jpg)

# Installation #

Installation instructions can be found on the InstallInstructions page.

# Setup #

There is no additional setup required.

# Usage #

Once installed, you can navigate to a test class in your solution by right clicking within your class (pretty much anything other than an actual Type name like those used in variable and method declarations), and opening the Jump To menu.  If there are Test classes available, you will see an entry with the format "<Class Name> Test(s) >", so if you are working within a class named MyTestableClass, the menu entry would be "MyTestableClass Tests".  Selecting this option will open a tree which contains all the references to your class within test code.  The first level of the tree contains the test classes, next are the methods within those classes that contain references, and finally the actual reference itself.  You can navigate to any of these points in your test code by selecting the item in the tree and pressing the **ENTER** key.  Alternately you can double-click on an item within the tree to navigate.  To close the window use the **ESC** key.

# Why did I build this #

I work within a fairly large solution with multiple developers.  While we try to apply conventions to our test names and locations, it is still time consuming to look through several hundred test files within your solution to find the one you want.  With this plugin it becomes quite easy to get to your test classes quickly.

# Credits #

Original Author: [Casey Kramer](CaseyKramer.md)