[http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/NDependLogo\_PoweredBy.PNG](http://www.ndepend.com)


[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/CR_StackOverflowIssues/)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)

**Requires DXCore 10.2 since build 1516**

# Introduction #

This plugin adds 1 code issue together with 2 possible fixes:
  * 'StackOverflowException in runtime' code issue
  * 'Change to base call' refactoring - available when code issue is reported for overriden property. Changes recursive call into call to base implementation of property
  * 'Change to field call' refactoring - available always when code issue is reported. Changes recursive call into field call.

'StackOverflowException in runtime' code issue is reported when property calls itself recusively. Below code will report 2 issues, first for return statement in getter and second for assignment in setter:
```
public string PropertyName
{
    get
    {
        return PropertyName;
    }
    set
    {
        PropertyName = value;
    }
}
```

# Usage #

This plugin integrates itself with other code issues and refactorings provided by CodeRush. Features of this plugin are language independent.

# Credits #

Author: Przemysław Włodarczak.