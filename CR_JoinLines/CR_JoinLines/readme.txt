CR_JoinLines
-------------

DESCRIPTION:

CR_JoinLines is a plug-in for the Developer Express, Inc. product, "DXCore
for Visual Studio .NET" (the engine behind CodeRush).  It provides the ability
to easily join one or more lines in the code editor, including an optional
delimiter between lines.


DISCLAIMER:

This plug-in is provided "AS IS" with no warranties of any kind. The entire risk
arising out of the use or performance of the plug-in is with you.  But hey, it's
free - you can't ask for much more than that.


REQUIREMENTS:

This product requires DXCore for Visual Studio .NET 11.2.8 or later.

DXCore is a free download from Developer Express, Inc.:
http://www.devexpress.com/Downloads/NET/DXCore/


INSTALLATION:

1) Make sure all instances of Visual Studio are closed.

2) Copy the CR_JoinLines.dll assembly into your DXCore plug-ins folder.  This
is typically something like:
   C:\Users\YOURUSERNAME\Documents\DevExpress\IDE Tools\Community\PlugIns

3) Open Visual Studio.

4) From the DevExpress menu, select "Options," then "IDE/Shortcuts" from the
treeview on the side.

5) Add a new shortcut.

6) Bind a key to the shortcut in the "Key 1" field ("Ctrl + Shift + J" is recommended).

7) Bind a key to the shortcut in the "Key 2" field ("Enter" is recommended).
This makes it so you have a two-key combination in order to execute the join.
In this case, you'll type "Ctrl + J" then press "Enter" to execute the join.
The second key is recommended in case you decide to allow for different types
of line joins based on delimiter (more on this below).

8) Set the shortcut's command to "Join Lines".

9) Set the shortcut's context to "Focus/Documents/Source/Code Editor".
(Hint: You can copy the "Code Editor" context from any existing command
shortcut and paste it into your new shortcut by right-clicking.)

10) CR_JoinLines allows you to optionally pass a string parameter to it as a
delimiter to be used between joined lines.  For example, if you want to join
several lines into a pipe-delimited list, you could provide the pipe character
as the delimiter.  If you don't provide a delimiter, lines will just be joined
directly together with no characters or space between them.  To provide a
delimiter, type the delimiter (inside quotes) into the "Parameters" box for the
shortcut.  For example, to have a pipe delimited join, type:  "|"

11) It is recommended that the default binding of "Ctrl + Shift + J", "Enter" provides
a join with no delimiter parameter.  To add additional types of joins - space
delimited, comma-delimited, etc. - repeat steps 5 through 10, changing the
second key binding (in step 7) to something intuitive and setting the appropriate
delimiter parameter in step 10.  It's easy to remember the delimiter if you set
the delimiter and the second key to the same character.  For example, a
pipe-delimited join might have a key binding of "Ctrl + Shift + J", "|".

12) Click "OK" to close the options window when you are done adding shortcuts.


RECOMMENDED SETUP:

The following describes the recommended key bindings to set up during installation:

	Binding: Ctrl+Shift+J, Enter
	Command: Join Lines
	Parameters: 
	Context: Code Editor
	Enabled: True
	Send Key to IDE: False

	Binding: Ctrl+Shift+J, Space
	Command: Join Lines
	Parameters: " "
	Context: Code Editor
	Enabled: True
	Send Key to IDE: False

	Binding: Ctrl+Shift+J, ,
	Command: Join Lines
	Parameters: ", "
	Context: Code Editor
	Enabled: True
	Send Key to IDE: False


USAGE:

Once installed and bound to a keyboard shortcut (as noted above):
1) Open a text document in the code editor in Visual Studio.
2) Without selecting anything, enter the keyboard shortcut to activate the
command.  The current line the caret is on will be joined with the subsequent
line.
3) Select multiple lines then enter the keyboard shortcut to activate the
command.  All selected lines will be joined.


KNOWN ISSUES:

None at this time.


VERSION HISTORY:

1.0.0.0830:
	* First version
1.1.0.1012:
	* Added ability to provide Delimiter parameter to join lines with a
	  delimiter between each joined line.
1.1.1.0307:
	* Fixed bug with later versions of DXCore where joining based on
	  selection functioned incorrectly.
2.0.0.0:
	* Updated for .NET 4.0/VS 2010.
	* Fixed issues interacting with DXCore 11.x.
	* Added VSIX installer.


CONTACT:

Travis Illig
http://www.paraesthesia.com
