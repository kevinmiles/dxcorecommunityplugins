[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/Refactor_Comments/)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)
##### Requires DXCore 10.2 since build 1765 #####
### Introduction ###
This plugin provides refactorings for changing multi-line comments into groups of single-line comments and back again.


### Usage ###
**Multi-> Multiple Single**

Place your caret within a multi-line comment and invoke the "**Convert To Singleline Comments**" refactoring.

Your comment will be broken into several single line comments.

**Multiple Single -> Multi**

Place your caret within any singleline comment which has 1 or more sibling comments and invoke the "**Convert to Multiline Comment**" refactoring

Your comments will be merged into a single multiline comment

### Limitations ###
This plugin is designed to work in any language that supports multiline comments. As such it will not work in VB.Net

### Credits ###

Author: RoryBecker