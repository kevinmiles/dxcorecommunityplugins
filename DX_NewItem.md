[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/DX_NewItem/)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)

##### Build against DXCore 10.1. Requires the full version of CodeRush #####
### Introduction ###

This plugin is designed to prevent you needing to visit the "Add New Dialog" in order to use the Item templates found within it.

### Usage ###

Instead simply type the corresponding phrase and hit space.

  * **anwf** - Adds a new Windows Form (in a WinForms project).
  * **anwf** - Adds a new WebForm (in a Web project).
  * **anc** - Adds a new Class.
  * **anmd** - Adds a new Module (VB.Net only)
  * **anxf** - Adds a new XML file
  * **anxs** - Adds a new XML Schema

You get the Idea :)

### Initial Setup ###
This plugin does require a minimum amount of setup.

  1. Extract the 'Dynamic Lists`_*`.xml' files from the plugin zipfile to the correct settings folder.
    * Correct Default Location is '**%AppData%\CodeRush For VS.Net\1.1\Settings.XML\Core\**' (without quotes)
  1. Create a single CodeRush Template
Call it...
```
an?Template?
```

and change it's content to ...
```
«NewItem(«?Get(Template)»)»
```

### Further Configuration (Adding support for more Item Templates) ###

I have only added a few templates to the default list. Feel free to add any of your own.

Add a new item to the list of those recognized, by adjusting one of the **Neutral** Dynamic List which starts with 'Templates'.

There are currently 3 such lists, (Templates, TemplatesWeb and TemplatesWin) which have different contexts in order to support some mnemonic overloading.

Item Templates are (By Default) found under '**C:\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\ItemTemplates\**'

The value of a list item should be set to 'TemplatePath/TemplateName[|PreferredFilename.ext]' (sans quotes)

So for example the WebForm list item has a value of 'Web/WebForm|WebForm.aspx'

If in doubt check out the examples, or ping me RoryBecker for some help :)

### History ###
  * Build 1280
    * List syntax altered to '[Folder/]TemplatePath|ExampleFilename.Ext'
    * Additional lists added for different contexts. Includes example items.
  * Build 1272 - Initial Release
#### Credit ####
Author: RoryBecker