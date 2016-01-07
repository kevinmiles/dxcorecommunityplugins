[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/CR_AssignmentExpressionToEqualityTest/)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)
##### Requires DXCore 10.2 #####
### Introduction ###
This plugin is designed to detect and highlight assignments that might have been intended to be equality tests.

It provides a new CodeIssue "Assignment Expression to Equality Check" ...

![http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_AssignmentExpressionToEqualityTest/Screenshots/AssignmentToExpressionCodeIssue.png](http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_AssignmentExpressionToEqualityTest/Screenshots/AssignmentToExpressionCodeIssue.png)

...and corresponding CodeProvider...

![http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_AssignmentExpressionToEqualityTest/Screenshots/AssignmentToExpressionCodeProvider.png](http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_AssignmentExpressionToEqualityTest/Screenshots/AssignmentToExpressionCodeProvider.png)


### Usage ###
Simply place your caret on the assignment expression in question, and the "Assignment Expression to Equality Check" CodeProvider will be available as usual from the Code SmartTag menu

If you have the full version of CodeRush, then the CodeIssue will also be available and will highlight issues this:

These are both appropriate in the following situations

![http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_AssignmentExpressionToEqualityTest/Screenshots/AssignmentToExpressionCodeIssueInline.png](http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_AssignmentExpressionToEqualityTest/Screenshots/AssignmentToExpressionCodeIssueInline.png)

Once both highlighted issues have been resolved, the code will look like this:

![http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_AssignmentExpressionToEqualityTest/Screenshots/AssignmentToExpressionResult.png](http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_AssignmentExpressionToEqualityTest/Screenshots/AssignmentToExpressionResult.png)

### Credits ###

Author: [Mark Miller](http://community.devexpress.com/blogs/markmiller/default.aspx)