[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://visualstudiogallery.msdn.microsoft.com/bee53ee3-8841-42ab-bddc-b14ed5a1a9ff)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)

# Introduction #
I use TextPad for text editing and one of the things I like a lot about TextPad is the Ctrl+J shortcut that allows you to join selected lines to each other. Visual Studio doesn't offer that line joining capability, so I decided to write it.

`CR_JoinLines` adds a command ("Join Lines") that you can bind to a keyboard shortcut (Ctrl+J, Enter is what I use) and will join lines in the code editor. It also allows for an optional delimiter, so if you want a pipe or comma or some other string (yes, string - you're not limited to a single character) inserted between the joined lines, you can do that.

![http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_JoinLines/screenshots/join_anim.gif](http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_JoinLines/screenshots/join_anim.gif)

# Installation #

  1. [Install the plugin from the Visual Studio Extension Gallery](http://visualstudiogallery.msdn.microsoft.com/bee53ee3-8841-42ab-bddc-b14ed5a1a9ff) or follow the InstallInstructions to place the `CR_JoinLines.dll` in the DXCore plugins folder.
  1. Open Visual Studio.
  1. From the DevExpress menu, select "Options," then "IDE/Shortcuts" from the treeview on the side.
  1. Add a new shortcut.
  1. Bind a key to the shortcut in the "Key 1" field ("Ctrl + Shift + J" is recommended).
  1. Bind a key to the shortcut in the "Key 2" field ("Enter" is recommended). This makes it so you have a two-key combination in order to execute the join. In this case, you'll type "Ctrl + J" then press "Enter" to execute the join. The second key is recommended in case you decide to allow for different types of line joins based on delimiter (more on this below).
  1. Set the shortcut's command to "Join Lines".
  1. Set the shortcut's context to "Focus/Documents/Source/Code Editor". (Hint: You can copy the "Code Editor" context from any existing command shortcut and paste it into your new shortcut by right-clicking.)
  1. `CR_JoinLines` allows you to optionally pass a string parameter to it as a delimiter to be used between joined lines.  For example, if you want to join several lines into a pipe-delimited list, you could provide the pipe character as the delimiter.  If you don't provide a delimiter, lines will just be joined directly together with no characters or space between them.  To provide a delimiter, type the delimiter (inside quotes) into the "Parameters" box for the shortcut.  For example, to have a pipe delimited join, type:  "|"
  1. It is recommended that the default binding of "Ctrl + Shift + J", "Enter" provides a join with no delimiter parameter.  To add additional types of joins - space delimited, comma-delimited, etc. - repeat steps 5 through 10, changing the second key binding (in step 7) to something intuitive and setting the appropriate delimiter parameter in step 10.  It's easy to remember the delimiter if you set the delimiter and the second key to the same character.  For example, a pipe-delimited join might have a key binding of "Ctrl + Shift + J", "|".
  1. Click "OK" to close the options window when you are done adding shortcuts.

# Options #
![![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_JoinLines/screenshots/shortcuts_sm.gif)](http://dxcorecommunityplugins.googlecode.com/svn/trunk/CR_JoinLines/screenshots/shortcuts_lg.gif)


The following describes the recommended key bindings to set up during installation:

|Binding|Command|Parameters|Context|Enabled|Send Key to IDE|
|:------|:------|:---------|:------|:------|:--------------|
|Ctrl+Shift+J, Enter|Join Lines|          |Code Editor|True   |False          |
|Ctrl+Shift+J, Space|Join Lines| " "      |Code Editor|True   |False          |
|Ctrl+Shift+J, ,|Join Lines| ", "     |Code Editor|True   |False          |

# Usage #

Once installed and bound to a keyboard shortcut (as noted above):

  1. Open a text document in the code editor in Visual Studio.
  1. Without selecting anything, enter the keyboard shortcut to activate the command.  The current line the caret is on will be joined with the subsequent line.
  1. Select multiple lines then enter the keyboard shortcut to activate the command.  All selected lines will be joined.

# Version History #
2.0.0.0:
  * Updated for .NET 4.0/VS 2010.
  * Fixed issues interacting with DXCore 11.x.
  * Added VSIX installer.
2.0.1.0:
  * Fixed bug where delimiter wasn't getting properly inserted.
2.1.0.0:
  * Added support for VS 2012, 2013.
  * Fixed bug when joining the last line in the file.
2.1.1.0:
  * Compatibility fix for DXCore 14.

# Credits #

Author: [Travis Illig](http://code.google.com/u/travis.illig/)

# Previous Version #
If you're not on VS2010/DXCore 11.2.8 yet, you can try [this older version of the plugin](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/CR_JoinLines/).