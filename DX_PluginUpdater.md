[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/DX_PluginUpdater/)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)
##### Built against DXCore 11.2.11 #####
### Introduction ###
This is a plugin designed to find New plugins and Updates to plugins you already have

Please read **Notes** and **Requirements** sections (below) before installing this plugin.

### **NOTE** ###
Many of the plugins downloaded by this plugin are not authored or controlled by DevExpress. The latest version of this plugin will not operate unless you indicate your understanding of this.

### Detailed version ###

Function 1: **Update Plugins** will scan the community site for any updates to plugins you already have installed.

Function 2: **Find New Plugins** will scan the community site for plugins you do not already have installed.

![https://github.com/RoryBecker/DX_PluginUpdater/raw/master/Screenshots/PluginUpdaterMenuItems.png](https://github.com/RoryBecker/DX_PluginUpdater/raw/master/Screenshots/PluginUpdaterMenuItems.png)

When invoked, each menu item will present you with a list of its plugins, and allow you to choose which to download and update\install.

![https://github.com/RoryBecker/DX_PluginUpdater/raw/master/Screenshots/PluginUpdaterPluginPicker.png](https://github.com/RoryBecker/DX_PluginUpdater/raw/master/Screenshots/PluginUpdaterPluginPicker.png)

### Usage ###

**Scenario 1**

You have several plugins installed and you are happy with this, but wonder if you have the latest versions of all of these.

  * Choose **DevExpress \ Update Plugins**. Plugin Updater will present you with a list of locally installed plugins which can be found on the community site.
  * Select those you'd like to update and click Ok.

**Scenario 2**

You are curious what other plugins might exist beyond those you already have in place.

  * Choose **DevExpress \ Find New Pugins**. Plugin Updater will present you with a list of plugins from the Community site, which you do not have installed on your machine.
  * Select those you'd like to install and click Ok.

### Options ###
The Plugin Updater has numerous options for you to play with...
![https://github.com/RoryBecker/DX_PluginUpdater/raw/master/Screenshots/PluginUpdaterOptions.png](https://github.com/RoryBecker/DX_PluginUpdater/raw/master/Screenshots/PluginUpdaterOptions.png)

### Notes ###

Plugin Updater will:

  * ...provide a list of plugins to download\update.
  * ...download the latest version of any selected plugin.
  * ...unzip the downloaded zip file into your currently configured Community plugins folder.
  * ...output messages in the status bar, letting you know what it's doing.
  * ...output messages in the 'Plugin updater' output window, letting you know what it's doing.
  * ...Offer to restart the DXCore (necessary to load plugin updates)

Plugin Updater does not:

  * ...know the specific requirements of any given plugin.
  * ...know if the plugin will run with a given version of VS, CodeRush
  * ...install any settings or key bindings.
  * ...locate plugins not hosted on the community site (Yet!)

> _Plugin Updater requires that you check the **Use raw assembly load** setting_. This setting is found on the **Core\Startup**. a Studio Restart will be necessary to ensure no plugins are locked.

> Plugin Updater currently operates with a subset of the plugins available from the community site. Please [let me know](RoryBecker.md) if you think a community plugin is stable \ important enough to be added to this list.

### Requirements ###

  * Requirements
    * You should have CodeRush 11.2.11 or better.
    * You must turn on "Use Raw Assembly Load"
      * DevExpress\Options ... Core \ Startup ... Last option.

### Credits ###

Author: [Rory Becker](http://devexpress.com/rory)