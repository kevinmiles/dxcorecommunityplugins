[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/Refactor_MoveTypesToFiles/)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)

# Introduction #

This plugin allows to move all classes from active source file to their own files and it is very useful when you like to have single class in a single file and you work with some kind of source code generators (like XPO class wizard) which create single source file. Currently the plugin was designed and tested to work under C#.

## Sample ##

This plugins allows to move classes

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleProject
{
    class Class1
    {
    }
    
    class A
    {
        private int _value;
    }
    
    class B
    {
        public int Value { get; set; }
    }

    class C
    {
        class D { }
    }
}
```

which are in a single source file to their own source files.

Sample output (file B.cs):

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleProject
{
    class B
    {
        public int Value { get; set; }
    }
}
```

# Usage #

![http://www.virgotech.pl/wp-content/uploads/2012/03/Refactor_MoveTypesToFiles_01.png](http://www.virgotech.pl/wp-content/uploads/2012/03/Refactor_MoveTypesToFiles_01.png)

![http://www.virgotech.pl/wp-content/uploads/2012/03/Refactor_MoveTypesToFiles_02.png](http://www.virgotech.pl/wp-content/uploads/2012/03/Refactor_MoveTypesToFiles_02.png)

![http://www.virgotech.pl/wp-content/uploads/2012/03/Refactor_MoveTypesToFiles_03.png](http://www.virgotech.pl/wp-content/uploads/2012/03/Refactor_MoveTypesToFiles_03.png)

# History #

  * 25.03.2012 - First version released to public.

# Credits #

Krzysztof Blacha
You can contact me at krzysztof.blacha@gmail.com