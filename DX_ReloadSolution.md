[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/DX_ReloadSolution/)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)

### Introduction ###
Some times VS gets it's knickers in a twist and all manner of things fowl up.

Sometimes a restart of studio is needed, but occasionally reloading the current solution to let VS reparse everything is quite sufficient.

However I'm lazy. I don't like having to say **File\Close Solution** followed by **File\Recent Projects and Solutions\My Solution**

This plugin a new item on the File menu which causes VS to unload and then reload the current solution.

![http://dxcorecommunityplugins.googlecode.com/svn/trunk/DX_ReloadSolution/Screenshots/Screenshot1.png](http://dxcorecommunityplugins.googlecode.com/svn/trunk/DX_ReloadSolution/Screenshots/Screenshot1.png)

This functionality is also bound to an Action called 'ReloadSolution' so you can bind it to a key in the CodeRush options :)

### Future Features ###

  * Add Accelerator key to menu item.
  * Move item to bottom of File menu