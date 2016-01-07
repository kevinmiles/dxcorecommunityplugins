[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/CR_CCConsole/)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)

# Introduction #

This plugin provides a dockable window which will display the status of selected CruiseControl.Net projects, and update them on a periodic basis.  There is also a feature which will display an alert if a project moves from a Successful (green) status, to a Failed (red) status.

# Installation #

Installation instructions can be found on the InstallInstructions page.

# Setup #

You can configure the plugin by going to the DevExpress Menu in Visual Studio, selecting Options, and then going to IDE/CruiseControl.Net Options.

From the options page, first input the web address of your CruiseControl.Net server (this must be the address that hosts the web interface.  There is currently no support for the remoting CruiseControl channel).  With this entered, click Refresh.  If your information is correct, you should see a list of projects available on that server appear in the "Available Projects" list.  Within the list check any project that you would like to monitor.

From here you can also set the Update Interval in minutes, and enable/disable failed project notification.

# Usage #

Once configured, you can display the project status window by clicking on the CruiseControl.Net Icon on the Visual Studio Toolbar (only available with a solution loaded), or by going to the Tools menu and selecting "View CCNet"

# Why did I build this #

The company where I work uses CCNet as our CI tool, so it seemed like a good idea to add a tool that would allow me to view the status of the projects I was interested in from within Visual Studio.

# Credits #

Original Author: [Casey Kramer](CaseyKramer.md)