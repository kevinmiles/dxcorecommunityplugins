[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/CodeIssueAnalysis/)            [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
##### Requires DXCore 10.2 since build 1531 #####
##### Requires DXCore 10.1 since build 1195 #####
# Introduction #

This plugin assists developers by scanning all files in a project or solution for code issues. The code issues are listed in a grid within a tool window. The grid allows grouping, filtering and sorting of code issues by type, message, solution, project and file name.

Files can be included or excluded from analysis based on their file name or content using regular expressions in the options.

**Double click a code issue to jump to it in code.**

![http://img341.imageshack.us/img341/9384/codeissueanalysis.png](http://img341.imageshack.us/img341/9384/codeissueanalysis.png)

# Usage #

Press the solution or project button to begin analysis. This may take some time dependant on the size of your project. Once analyzed a list of code issues will be shown. Analysis can be cancelled at any time.

# CodeRush Xpress Users #

Unfortunately CodeIssues is disabled in the latest version of CRXpress so this plugin will not work for you.

# Installation #

Install by copying the CodeIssueAnalysis.dll to your dxcore plugins directory.

# Keyboard Shortcut #

Follow the guide [here](http://rorybecker.blogspot.com/2009/08/how-to-bind-key-in-coderush.html) on how to add a keyboard shortcut. The command is OpenCodeIssueAnalysis.

# Options #

Files are included and excluded from analysis by regular expressions either on their file name or content.

By default the analysis includes .cs and .vb files and excludes .designer.cs and .designer.vb files.

These options can be modified on the options page at:

**Editor -> Code Analysis -> Code Issues Analysis**<br>
<img src='http://img4.imageshack.us/img4/2633/settingsaa.png' />

<h1>Save Layout and Filtering</h1>

You can now save grid grouping and filtering for re-use using the drop down in the top right.<br>
<br>
<h1>Export Code Issues To HTML</h1>

The filtered results in your grid can be exported to an HTML table including any grouping you have set for printing and editing outside of visual studio. The table will also open nicely in Excel for additional editing.<br>
<br>
Simply press the HTML export button it will ask if you wish to expand any groupings you have in the grid as it only exports what is visible in your grid. Then select a location and file name to save the code issues.<br>
<br>
<h1>Credits</h1>
Author: Peter Thorpe