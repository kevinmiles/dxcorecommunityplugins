CR_SortLines
-------------

DESCRIPTION:

CR_SortLines is a plug-in for the Developer Express, Inc. product, "DXCore
for Visual Studio .NET" (the engine behind CodeRush).  It provides the ability
to easily sort two or more lines in the code editor.


DISCLAIMER:

This plug-in is provided "AS IS" with no warranties of any kind. The entire risk
arising out of the use or performance of the plug-in is with you.  But hey, it's
free - you can't ask for much more than that.


REQUIREMENTS:

This product requires DXCore for Visual Studio .NET 1.1.40 or later.

DXCore is a free download from Developer Express, Inc.:
http://www.devexpress.com/Downloads/NET/DXCore/


INSTALLATION:

1) Make sure all instances of Visual Studio are closed.
2) Copy the CR_SortLines.dll assembly into your DXCore plug-ins folder.  This
is typically something like:
   C:\Program Files\Developer Express Inc\DXCore for Visual Studio NET\1.1\Bin\Plugins
3) Open Visual Studio.
4) From the DevExpress menu, select "Options," then "IDE/Shortcuts" from the
treeview on the side.
5) Add a new shortcut.  Bind a key to the command "SortLines" and set the
context to "Focus/Documents/Source/Code Editor".
(Hint: You can copy the "Code Editor" context from any existing command
shortcut and paste it into your new shortcut by right-clicking.)
6) Click "OK" to close the options window.


USAGE:

Once installed and bound to a keyboard shortcut (as noted above):
1) Open a text document in the code editor in Visual Studio.
2) Without selecting anything, enter the keyboard shortcut to activate the
command.  The sort dialog will pop up and ask you for sort options.  You must
have at least two lines in the document or nothing will happen.
3) Select multiple lines then enter the keyboard shortcut to activate the
command.  The sort dialog will pop up and ask you for sort options.  You must
have at least two lines selected or nothing will happen.


DELETE DUPLICATE LINES:
Checking the "delete duplicate lines" box will remove all of the duplicate
lines found during the sort.  Duplicates are removed such that the first
encountered version of a duplicate that is encountered is kept.  This is
important to remember when sorting using sort expressions (see below).  When
sorting without expressions, lines are considered duplicate if their original
versions match.  When sorting using sort expressions, lines are considered
duplicate if their sort expression version matches.


MATCH/SORT EXPRESSIONS:
Line match and sort expressions (as seen in the sort options dialog that pops
up when you execute the sort command) are an easy way to sort lines based on a
section of text in each line or a particular property of each line.  When the
match and sort expressions are used, the sort algorithm matches each line
against the line match expression, then executes a replace with the sort
expression.  The lines in the document are then sorted by the regular
expression replaced version of the line rather than by the original text.

For example, to sort lines ignoring whitespace at the beginning of each line use:
Line match expression:  ^\s*(.+)$
Sort expression:        $1

Or to sort a list of variable declarations in C# by variable name rather than
type, use:
Line match expression:  ^.*([^\s;]+);$
Sort expression:        $1

Sorting with expressions may take some experimentation to get used to, but it
can be handy, especially if sorting, say, tab-delimited data or some such.


KNOWN ISSUES:

None at this time.


VERSION HISTORY:

1.0.0.0906:
	* First version


CONTACT:

Travis Illig
tillig@paraesthesia.com
http://www.paraesthesia.com
