[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://visualstudiogallery.msdn.microsoft.com/ebe64669-9236-4e73-9336-5c07f524d763)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)

# Introduction #

Visual Studio allows you to specify different code formatting rules for different languages it understands, but in many cases the formatting only applies to new code you're writing. For example, C# lines generally get formatted when you hit the end of the line and type the semicolon... but the only line that gets formatted is the one you just completed.

Wouldn't it be nice if the whole document would adhere to the same formatting without you having to pay attention to it?

With this plugin, you can automatically have Visual Studio apply code formatting to the entire document you're working on when you save. That way you don't have to think about it - your documents will always be formatted.

# Automatic Installation #
Use the Visual Studio Extension Manager to [install the plugin from the Visual Studio Extension Gallery](http://visualstudiogallery.msdn.microsoft.com/ebe64669-9236-4e73-9336-5c07f524d763).

# Manual Installation #
Follow the InstallInstructions to place the `DX_FormatOnSave.dll` in the DXCore plugins folder.

# Configuration #
While usage is transparent - documents automatically get formatted when you save them - you can configure which documents the formatting will get applied to.

From the DevExpress menu, select "Options." In the tree on the left, go to "Editor/Code Style" and select the "Format On Save" options window. There you will be able to select which document types get formatted.

![https://dxcorecommunityplugins.googlecode.com/svn/trunk/DX_FormatOnSave/screenshots/options.png](https://dxcorecommunityplugins.googlecode.com/svn/trunk/DX_FormatOnSave/screenshots/options.png)

# Release Notes #
  * 2.0.1: Visual Studio 2012 compatibility.
  * 2.0.2: Fix for [Issue #147](https://code.google.com/p/dxcorecommunityplugins/issues/detail?id=#147) - exception when save-on-close. Unable to do save-on-close.
  * 2.0.3: Added support to handle save-on-close. Unable to save-on-close if VS is also shutting down at the same time.

# For Plugin Developers #
If you are a plugin developer, the source to this plugin may interest you. It shows you how to use lower-level Visual Studio functions that DXCore doesn't expose (yet) from within a DXCore plugin. In this case, DXCore doesn't expose a "Before Document Save" event, but Visual Studio does. This plugin handles the native event from inside a DXCore plugin.

# Credits #

Author: [Travis Illig](http://code.google.com/u/travis.illig/)