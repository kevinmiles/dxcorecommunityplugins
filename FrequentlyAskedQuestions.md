### Q: Why didn't the installation succeed? ###
Sometimes it looks like the installation of the plugins failed. This might be seen in one of the following scenarios:
  * **You copied the plugin(s) into the wrong folder.** Verify you copied them into the proper DXCore plugins folder. See InstallInstructions for more.
  * **You don't have DXCore/CodeRush/Refactor installed.** All of these plugins require at least DXCore, but some require CodeRush and/or Refactor as well. See the individual plugin pages in [the wiki](http://code.google.com/p/dxcorecommunityplugins/w/list) for specific requirements.
  * **You didn't restart Visual Studio.** If you installed the plugins while Visual Studio was running, you need to restart Visual Studio to allow them to register.

### Q: How do I know if DXCore has found the plugin I installed? ###
You can verify installation of Code and Refactoring plugins by looking at the appropriate catalog in the DevExpress Options menu.

#### To see the list of Refactoring plugins... ####

  1. From the DevExpress menu, select "Options...".
  1. Verify the "Level" in the lower left of the options window is set to "Advanced" or "Expert."
  1. In the tree view on the left, navigate to the `Editor\Refactoring` folder.
  1. Select the "Catalog" options page.

#### To see the list of Code Modification plugins... ####

  1. From the DevExpress menu, select "Options...".
  1. In the tree view on the left, navigate to the `Editor\Code Modification` folder.
  1. Select the "Catalog" options page.

### Q: I just extracted the plugins but can't find them - where'd they go? ###
Sometimes it appears that you've extracted the assemblies into the plugins folder but they aren't there when you look. **Double-check in Explorer** and verify they aren't there, but it's possible that the compression program (e.g., WinZip) created a sub-folder for your plugins or put them in a place you otherwise didn't expect.