[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/MiniCodeColumn/)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)

<table><tr valign='top'><td>
<h5>Requires DXCore 10.2 since build 1566</h5>
<h3>Introduction</h3>

If you know about <a href='http://www.hanselman.com/blog/IntroducingRockScroll.aspx'>RockScroll</a>, this is for you, implemented as <a href='http://www.devexpress.com/Downloads/NET/IDETools/DXCore/'>DxCore</a> Plugin.<br>
<br>
<h3>Usage</h3>

Open the tool-window with the menu DevExpress -> Tool Windows -> MiniCodeColumn.<br>
A <b>double click</b> to select a word will <b>highlight</b> all occurrences<br>
in the whole file and highlight it with a red color in the mini code column.<br>
<br>
You can also <b>click</b> on the mini code column to <b>jump</b> to any code line.<br>
Colapsed regions will be fully painted (non collapsed)<br>
and splitting the texteditor is still possible (which was not possible with RockScrollV1).<br>
If code lines would exeed the available space, lines are compressed (2x, 3x etc.) and become <i>thicker</i>.<br>
<br>
In this new version you can use this tool window like the scrollbar:<br>
Click and drag inside the tool window and the code window will follow.<br>
<br>
<h3>Options</h3>

The options page finally arrived. The <a href='http://code.google.com/p/dxcorecommunityplugins/wiki/MiniCodeColumnOptions'>screenshot</a> was too big for this page.<br>
<br>
</td><td>
<img src='http://dxcorecommunityplugins.googlecode.com/svn/trunk/MiniCodeColumn/Documents/ScreenShot.png' />
</td></tr></table>

<table><tr valign='top'>
<td>

<h3>Credits</h3>

Originally I found <a href='http://www.hanselman.com/blog/IntroducingRockScroll.aspx'>RockScroll</a> on a BlogPost from Scott Hanselman, who credited this piece of software to Rocky Downs.<br>
<br>
I am R.Warnat and I created this plugin on top of their ideas.<br>
<br>
With some combinations of vista and/or service packs RockScroll<br>
stopped working on my machine and <i>I reinvented the wheel</i>.<br>
<br>
<br>
<h3>Small Update</h3>

The flickering and slowing down of the studio is fixed as well as<br>
handling the highliting of more than one doubleclicked word.<br>
<br>
<br>
If you find some serious errors , please use the <a href='http://code.google.com/p/dxcorecommunityplugins/issues/list'>issues tab</a>.<br>
<br>
<h3>Goodbye</h3>
For VS > 2012 this is obsolete.<br>
You can grab Power-Tools and switch the scroll-bar to "Show Full Mode"<br>
via context-menu on the scrollbar itself.