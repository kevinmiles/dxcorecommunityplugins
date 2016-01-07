[http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/NDependLogo\_PoweredBy.PNG](http://www.ndepend.com)


[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/CR_SmartGenerics/)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)

**Requires DXCore 10.2 since build 1513**

# Disclaimer #

This plugin works only for c#.

# Introduction #

This plugin brings auto complete of generics operators in c# to CodeRush. Features are based on auto complete of parens and brackets, which are supported out of the box:
  * Auto complete of <> when < was inserted as part of generics
  * Support for text fields
  * Adding spaces inside < and > (< | >)
  * Easy delete of empty <>
  * Ignoring of closing > while typing
  * Integrated with CodeRush Feature UI

I did my best to detect whether < was typed as part of generics or as "lower than" operator. Please let me know when both cases are confused resulting in redundant or lacking >.

If you find any troubles using it, post issue and I try to fix it ASAP.

# Usage #

This plugin works "as you type". There is no need for additional steps to activate its features.

# Options #

SmartGenerics is fully configurable. Options page can be found in "Editor/Auto Complete/Generics" section of DevExpress options window. Note that you must select "Advanced" or "Expert" user level to see this page.

# Future work #

  * Support for other languages.

# Credits #

Author: Przemysław Włodarczak.