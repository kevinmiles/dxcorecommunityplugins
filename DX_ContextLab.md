[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://visualstudiogallery.msdn.microsoft.com/e3394c94-db96-4806-ba33-057cc0fec718)

# Introduction #
When working with DXCore Actions or other plugins, sometimes you need to enable, disable, or otherwise manipulate something based on whether a given context is satisfied. Unfortunately, it's hard to know what contexts are satisfied at any given time. That's where this plugin helps.

`DX_ContextLab` adds a tool window to the DevExpress\Diagnostics menu that you can set to poll every second for the complete list of contexts that are satisfied at any given time. It also adds a command ("Update Context Lab") that you can bind to a keyboard shortcut to manually refresh the list of satisfied contexts, should you not want polling to run.

![http://dxcorecommunityplugins.googlecode.com/svn/trunk/DX_ContextLab/screenshots/lab-window.png](http://dxcorecommunityplugins.googlecode.com/svn/trunk/DX_ContextLab/screenshots/lab-window.png)

# Installation #

  1. Make sure all instances of Visual Studio are closed.
  1. Copy the `DX_ContextLab.dll` assembly into your DXCore plug-ins folder.  This is typically something like: C:\Documents and Settings\username\My Documents\DevExpress\IDE Tools\Community\PlugIns
  1. Open Visual Studio.
  1. From the DevExpress menu, select "Options," then "IDE/Shortcuts" from the treeview on the side.
  1. Add a new shortcut.
  1. Bind a key to the shortcut in the "Key 1" field ("Ctrl + Shift + Alt + U" is recommended).
  1. Set the shortcut's command to "Update Context Lab".
  1. Do not set any context for the shortcut. It should be allowed to run always.
  1. Click "OK" to close the options window when you are done adding shortcuts.

# Options #

There are no configuration options available for this plugin beyond your choice of what key to bind the update action to, as described above.

# Usage #

  1. Open the DevExpress\Diagnostics menu and select "Context Lab."
  1. Optionally check the box to enable context polling. Enabling polling will refresh the list of satisfied contexts once every second. If you choose not to enable polling, you can use the shortcut key you defined to manually update the list of satisfied contexts.
  1. Navigate around as usual and do whatever debugging is needed.

![http://dxcorecommunityplugins.googlecode.com/svn/trunk/DX_ContextLab/screenshots/menu-location.png](http://dxcorecommunityplugins.googlecode.com/svn/trunk/DX_ContextLab/screenshots/menu-location.png)

# Release History #
  * 1.0.0.0: Initial release.
  * 2.0.0.0: Updated for latest DXCore. Published to VS Gallery.

# Credits #

Author: [Travis Illig](http://code.google.com/u/travis.illig/)