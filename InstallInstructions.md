### Install ###
**Note: These instructions assume that you have already installed the** [Prerequisites](Prerequisites.md) **and that your DXCore version is 9.3.4 or higher.**


  1. Download the latest zip containing the plugin that you wish to install. Each plugin's wiki page has a download link that takes you to a folder containing...
    * The latest version of the plugin.
    * An archive folder with previous versions of the plugin.
  1. Exit Visual Studio to ensure no files are locked.
  1. Determine the location of your DXCore community plugin folder.
    * This folder will most likely be a path like:
      * `C:\Documents and Settings\YOURUSERNAME\My Documents\DevExpress\IDE Tools\Community\PlugIns` **OR**
      * `C:\Users\YOURUSERNAME\Documents\DevExpress\IDE Tools\Community\PlugIns` **OR**
      * `C:\Program Files\DevExpress 2009.1\IDETools\Community\PlugIns`
    * Your community plugin folder **may have been reconfigured to be elsewhere**.
    * Its current location can be determined from the Core\Settings [options page](http://sites.google.com/site/coderushdocs/screens/options).
  1. Extract the plugin DLL from the zip file you downloaded and save it in your community plugins folder.
  1. Right-click the plugin DLL, select "Properties," and on the "General" tab, click the "Unblock" button. This is required to enable DXCore to properly execute the code in the plugin.
  1. Restart Visual Studio. The new plugin should be available.

### Usage ###
Each plugin has its own usage instructions. See the [wiki](http://code.google.com/p/dxcorecommunityplugins/w/list) for more on each individual plugin.

### FAQ ###
For answers to some questions and some workarounds/info on known issues, check out the FrequentlyAskedQuestions page.