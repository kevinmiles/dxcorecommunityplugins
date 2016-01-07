[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/DX_ZenCoding/)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)
#### Introduction ####
This plugin is an addition to the Template expansion engine within CodeRush. It will not work with CodeRush Xpress. It emulates some of the features found in the ZenCoding project. (http://code.google.com/p/zen-coding/) These features allow one to enter up html code more quickly.

#### Configuration ####
  * Install plugin as usual.
  * Import the HTML templates found in Html\_Zen.xml (in the zip file)
    * Visit the Editor\Templates options page
    * Change the language to HTML
    * Right click the Template tree and choose Import templates
    * Locate and select the Html\_Zen.xml file
  * Shortcut Options (DevExpress\Options IDE\Shortcuts)
    * Create a new shortcut.
      * Assign a key to ZenExpand.

#### Usage ####
  * Type a ZenCoding expression and then hit the ZenExpand key

#### Details ####
Sample expressions:
```
       div
       div#page
       div.class1
       div.class1.class2
       div.class1.class2#page



       div+div
       div+div+div
       div*5

       div>div*2>table>tr>td*4


       div#page>div.logo.otherlogo
       div#page>div.logo+ul#navigation
       div#page>div.logo+ul#navigation>li*5>a

       table>tr>td
       table>tr*5>td[Title]*2
       table>tr>td[count="$$"]*10

       div.master>div.child*2>table.table>tr.header+tr.data.#row$*5>td*3
```
#### Credits ####
Plugin by RoryBecker

ZenCoding web page -> http://code.google.com/p/zen-coding/