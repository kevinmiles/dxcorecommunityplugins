This plug-in provides various ways to better interact with the results of the CodeRush Unit Test Runner.

[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/UnitTestErrorVisualizer/)  [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Screencast.png)](http://dxcorecommunityplugins.googlecode.com/svn/trunk/UnitTestErrorVisualizer/Screenshots/UnitTestErrorVisualizer_Demo.swf)    [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
##### Requires DXCore 10.2 since build 1547 #####
# Introduction #

The new Unit Test Runner in CodeRush 9.3.2 is nice. But I felt the built in on-screen pass/fail information was too sublte or lacking. I wanted more insight on the test results; in particular those that failed. This plug-in provides a few ways to get more information about the results of a test run.

# Details #

So far there are three optional visualizations that are applied to the CodeRush Unit Test Runner Results.

![http://dxcorecommunityplugins.googlecode.com/svn/trunk/UnitTestErrorVisualizer/Screenshots/DefaultOptions.png](http://dxcorecommunityplugins.googlecode.com/svn/trunk/UnitTestErrorVisualizer/Screenshots/DefaultOptions.png)
  1. The test attribute is shaded with red when a test faile, green when it passes, and yellow if it is skipped. No shading happens for tests that are pending or have been changed after the current result was run.
  1. The message is parsed to determine why the test failed and the key reason is displayed as overlay text at the end of the line of the assert which failed. (off by default)
  1. An arrow is drawn from the learn more icon to the failed assert and the why it failed information is placed in a rectangle over the arrow

The first two are really just carry over ideas from the RedGreen plug-in. Any one of the visualizations can be turned on/off from an options page.

# Problem Finding Assistance #
There are a few cases where a failed test is not easy to diagnose.
  * The expected and actual strings are long and the strings differ past about position 10-15. ![http://dxcorecommunityplugins.googlecode.com/svn/trunk/UnitTestErrorVisualizer/Screenshots/LongStringProblem.png](http://dxcorecommunityplugins.googlecode.com/svn/trunk/UnitTestErrorVisualizer/Screenshots/LongStringProblem.png)
  * The expected and/or actual contains non-printable characters ![http://dxcorecommunityplugins.googlecode.com/svn/trunk/UnitTestErrorVisualizer/Screenshots/NonPrintablesProblem.png](http://dxcorecommunityplugins.googlecode.com/svn/trunk/UnitTestErrorVisualizer/Screenshots/NonPrintablesProblem.png)
Both of these cases often lead me to want to use to debug the test so I can use the debugger features to more easily see the problem point.

## Solutions ##
This screen shot illustrates how the first problem is addressed: ![http://dxcorecommunityplugins.googlecode.com/svn/trunk/UnitTestErrorVisualizer/Screenshots/TargetErrors.png](http://dxcorecommunityplugins.googlecode.com/svn/trunk/UnitTestErrorVisualizer/Screenshots/TargetErrors.png)
The expected and actual strings are truncated beyond the context of the error. Next, the error portion of the actual string is colored a darker red. And finially when using the Arrow to Assert feature, a red vertical line is drawn where the actual begins to differ from the expected.

This screen shot illustrates how the second problem is addressed: ![http://dxcorecommunityplugins.googlecode.com/svn/trunk/UnitTestErrorVisualizer/Screenshots/NonPrintablesEscaped.png](http://dxcorecommunityplugins.googlecode.com/svn/trunk/UnitTestErrorVisualizer/Screenshots/NonPrintablesEscaped.png)
The most common non-printable characters are escaped so that they are visible in the expected and actual text.

There are options to control how these features behave.
# Limitations #
At present there is CodeRush provides no way to know when the status of a unit test result has changed, only a way to get the list. So some of these features don't appear until a refresh gives the plug-in a chance to check the current status.

Initially I attempted to make these features trigger off of the CodeRush 'X' failure icon, but as not all that unexpected, it caused issues with the features of the 'X'. I didn't like that, so I created my own icon. I have requesets in to DeveloperExpress so that I can hook into the tile without killing the built in behaviours. For now you'll have to deal with an extra icon.

Plug-in by Jim Argeropoulos