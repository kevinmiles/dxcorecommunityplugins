[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/DX_MoveCode/)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Screencast.png)](http://tv.devexpress.com/CRMoveCode.movie)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)
##### Requires DXCore 10.2 since build 1558 #####
# Introduction #

The 4 actions provided by this plugin, allow you to move code around more efficiently than before.
This plugin does not operate on lines of code, but instead on complete statements.

# Configuration #
[Bind a key](http://rorybecker.blogspot.com/2009/08/how-to-bind-key-in-coderush.html) to each of MoveCodeLeft, MoveCodeRight, MoveCodeUp and MoveCodeDown.

It's been suggested (Thanks Ruben) that some good keys to bind these actions to would be Ctrl+Alt+Up|Down|Left|Right. These are similar to those used by Word and PowerPoint which as pointed out, might enable you to make good use of some muscle memory :)

_Note: Build 675 has additional experimental 'MoveCaret' versions of these new commands._

# Usage #
Place your caret on a line with a statement, and activate any of the commands as required.

# Details #

**Moving Code Up and Down**

The first 2 commands made available by this plugin are MoveCodeUp and MoveCodeDown.

Because this plugin operates on statements rather than lines of code, when you move a statement down, it will skip past an 'if clause' or a 'for loop' rather than move inside it.

For example:

```
//-------------------------------------------------------------
// Sample 1a
private void SomeMethod()
{
  int SomeInt; // <- Moving this 'down' leads to sample 1b.
  if (true)
  {
  }
}

//-------------------------------------------------------------
// Sample 1b
private void SomeMethod()
{
  if (true)
  {
  }
  int SomeInt; // <- Moving this 'up' leads to sample 1a.
}

```

**Move Code Left or Right**

The next 2 commands are 'MoveCodeLeft' and 'MoveCodeRight'. Their purpose is to deal with scenarios where you might want to move code in and out of parent/child code blocks like 'If' and 'For'

MoveCodeRight looks for the next sibling statement which possesses it's own code block (Such as a 'For' or 'If') and moves the statement so as to position it first inside of said Codeblock.

MoveCodeLeft Looks to move the current statement immediately prior what every code block might already be within.

For Example:

```
//-------------------------------------------------------------
//Sample 2a
private void SomeMethod()
{
  int SomeInt; // <- Moving this 'right' leads to the sample 2b.
  int int1 = 1;
  int int2 = 1;
  int int3 = 1;
  if (true)
  {
    int int4 = 1;
    int int5 = 1;
    int int6 = 1;
  }
  int int7 = 1;
  int int8 = 1;
  int int9 = 1;
}

//-------------------------------------------------------------
//Sample 2b
private void SomeMethod()
{
  int int1 = 1;
  int int2 = 1;
  int int3 = 1;
  if (true)
  {
    int SomeInt; // <- Moving this 'left' leads to sample 2c.
    int int4 = 1;
    int int5 = 1;
    int int6 = 1;
  }
  int int7 = 1;
  int int8 = 1;
  int int9 = 1;
}

//-------------------------------------------------------------
//Sample 2c

private void SomeMethod()
{
  int int1 = 1;
  int int2 = 1;
  int int3 = 1;
  int SomeInt; 
  if (true)
  {
    int int4 = 1;
    int int5 = 1;
    int int6 = 1;
  }
  int int7 = 1;
  int int8 = 1;
  int int9 = 1;
}
```

# History #
  * Build 1370 - Options page added allowing a choice between 2 movement algorithms.
  * Build 821 - Small fix to ensure moved code remains visible on screen.
  * Build 709 - MoveCode[Up|Down] now works with Methods if caret on signature.
  * Build 675 - MoveCaretUp / MoveCaretDown / MoveCaretLeft / MoveCaretRight actions.

# Future Plans #

  * Move Selection (With intelligent selection Adjustment) functionality
  * Option to use TargetPicker for Methods and Selections.
  * Allow MoveCodeLeft, MoveCodeRight to adjust the existence braces on a c# block where the number of statements changes between 1 and 2.

# Credits #
RoryBecker