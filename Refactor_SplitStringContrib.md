[http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/NDependLogo\_PoweredBy.PNG](http://www.ndepend.com)


[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/Refactor_SplitStringContrib/)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)

**Requires DXCore 10.2 since build 1515**

# Introduction #

This plugin enhances "Split String" refactoring provided with Refactor Pro. It allows VB developers to use '&' instead of '+' as their concatenation operator. Also, when caret is inside string, you can fast and easy split it into two at the caret position. What is more, right string will be moved to the next line.

Features were implemented in language agnostic way. However, plugin was tested only in C# and VB environments. If you find any troubles using it, post issue and I try to fix it ASAP.

# Usage #

Implementation of this plugin uses standard "Split String" refactoring available with CodeRush. AFAIK free versions of CodeRush and Refactor don't contain this refactoring, so you will need full version of Refactor Pro in order to use this plugin. Sorry.

First feature is integrated with "Split String" refactoring. In VB you can use '&' instead of '+' as your preferred concatenation operator.

Second feature works "as you type":

```
string myString = "Some long string that| should be splitted";
```

will be converted with enter into:

```
string myString = "Some long string that"
    |+ " should be splitted";
```

or into (depending on settings):

```
string myString = "Some long string that" +
    |" should be splitted";
```

| denotes caret in above listings.

# Options #

SplitStringContrib is fully configurable. Options page can be found in "Editor/Refactoring/Split String" section of DevExpress options window. Note that you must select "Advanced" or "Expert" user level to see this page.

# Known Issues #

When "Use '&' as concatenation operator in VB" option is active, emited code will use '&' but refactoring preview will still show '+'. This is limitation of standard "Split String" refactoring. Please track http://www.devexpress.com/Support/Center/p/B135602.aspx suggestion to bump this issue up in DevExpress priority queue.

# Credits #

Author: Przemysław Włodarczak.