Occasionally you will find that a plugin does not appear to load.

The simplest manifestation of this, is that its expected functions do not appear to be in place.

For example:
  * Refactoring plugins should have menu items that appear on the 'Refactor' SmartTag menu.
  * CodeProvider plugins should have menu items that appear on the 'Code' SmartTag menu.
  * Plugin provided Actions, should appear in the 'IDE\Shortcuts' Option page in the command dropdown.

Before submitting a bug, first ensure that the plugin is in fact loaded.

These are a few steps that you can follow, in order to determine what is causing this to happen.

  * Check that the plugin is installed in the correct location on disk
    * see http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions
  * Check that the plugin is "loaded".
    * Open the Options Screen ('**DevExpress\Options**' or **Ctrl+Shift+Alt+O**)
    * Locate the "**Core\Plug-in Manager**" options page. This page is an 'Expert' page.
    * Find the plugin you are diagnosing in this list
    * Ensure the plugin "Load State" is Loaded

If you have confirmed that your plugin is indeed loaded, then you should see your individual plugin's documentation for details on it's interface.

If this is not the case then it may be time to file a bug.

