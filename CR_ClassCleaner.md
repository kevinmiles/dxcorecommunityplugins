[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/CR_ClassCleaner)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)
##### Requires DXCore 10.2 since build 1479 #####
### Introduction ###
The ClassCleaner plugin features the ability to:
  * Organize a class and create regions
  * Organize a class without creating regions
  * Remove whitespace (removes excessive line breaks)
  * Remove regions (rids the class of regions)
### Configuration ###
  1. Add the assembly to your plugins folder – “DevExpress\IDE Tools\Community\PlugIns”
  1. Start Visual Studio and select “DevExpress>Options” from the top menu strip then navigate to “IDE\ShortCuts”.
  1. First I recommend creating a folder under “Code” called “Custom” and then within that “ClassCleaner”. The commands that need shortcuts are:
    * OrganizeWithRegions
    * !OrganizeWORegions
    * RemoveWhitespace
    * RemoveRegions
    * SelectCurrentMember
    * CutCurrentMember
![http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_ClassCleaner/Screenshots/ClassCleanerActions.jpg](http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_ClassCleaner/Screenshots/ClassCleanerActions.jpg)

  1. Add a shortcut for each command.  The keystrokes I use are:
    * RemoveRegions: ctrl+shift+alt+I
    * RemoveWhitespace: ctrl+shift+alt+K
    * OrganizeWithRegions: ctrl+shift+alt+L
    * !OrganizeWORegions: ctrl+shift+alt+O
    * SelectCurrentMember ctrl+P
    * CutCurrentMember ctrl+shift+P
  1. For each shortcut make sure “Use” is set to “InClass” which is located at “Editor/Code/InClass”.
### Usage ###
The next step is to open a class file, put your cursor inside the class, and press ctrl+shift+alt+L or ctrl+shift+alt+O to organize the file.  To test out “RemoveWhitespace”: open a class file, inside the class hit “Enter” five times, and then press ctrl+shift+alt+L.  To test out “RemoveRegions”: open a class file that contains regions, place the cursor inside the class, and press ctrl+shift+alt+I.
NOTE:  As stated above your cursor must be inside the class.
What and which order ClassCleaner organizes a class is configurable.  Navigate to “DevExpress>Options” on the top menu strip.   Next go to “Editor/ClassCleaner” to view the configuration.

![http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_ClassCleaner/Screenshots/ClassCleanerOptions.jpg](http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_ClassCleaner/Screenshots/ClassCleanerOptions.jpg)

The ClassCleaner configuration allows users to add code groups, delete code groups, and change the order of how code blocks will be organized.  Another useful feature is the ability to import and export code group configuration settings.  This allows the settings to be distributed around the team or even have different ClassCleaner configuration files for different projects.  The "Reset" button does just that it resets to the original default settings.

### Credits ###
Written by John Luiff