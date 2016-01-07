[http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/NDependLogo\_PoweredBy.PNG](http://www.ndepend.com)


[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/CR_SmartQuotes/)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)

**Requires DXCore 10.2 since build 1480**

# Introduction #

This plugin brings auto complete of quotes to CodeRush. Features are based on auto complete of parens and brackets, which are supported out of the box:
  * Auto complete of single and double quotes
  * Support for text fields
  * Easy delete of empty single and double quotes
  * Ignoring of closing single and double quotes while typing
  * Integrated with CodeRush Feature UI

Features were implemented in language agnostic way. However, plugin was tested only in c# environment. If you find any troubles using it, post issue and I try to fix it ASAP.

There is some additional logic in "auto complete" feature:
  * single quote is not completed inside comments or in compiler directive. This is for better support of natural languages, which can use apostrophes.
  * single and double quotes are not completed if are escaped with \. This is to ease entering quotes as part of string.

# Usage #

This plugin works "as you type". There is no need for additional steps to activate its features.

# Options #

SmartQuotes is fully configurable. Options page can be found in "Editor/Auto Complete/Quotes & Double Quotes" section of DevExpress options window. Note that you must select "Advanced" or "Expert" user level to see this page.

# Future work #

  * Better support for verbatim strings
  * Improved ToString selection embedding

# Credits #

Author: Przemysław Włodarczak.