[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/CR_CreatePersistentClassList/)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)

# Introduction #

This plugin allows create type array for all persistent classes found inside active source file. It's useful when you work with large database and need to update your schema using XPO without having to reflect your assemblies for specific types. Currently the plugin was designed and tested to work under C#.

## Sample ##

This plugins allows to use source code like:

```
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace SampleProject
{
    class A { }

    [Persistent]
    class B { }

    [Persistent("TableC")]
    class C { }
}
```

and use it to generate this:

```
namespace SampleProject
{
	using System;

	public class PersistentClassHelper
	{
		public Type[] GetPersistentTypes()
		{
			Type[] persistentTypes = new Type[]
			{
				typeof(SampleProject.B),
				typeof(SampleProject.C)
			};

			return persistentTypes;
		} // GetPersistentTypes
	} // PersistentClassHelper
} // SampleProject
```

# History #

  * 25.03.2012 - First version released to public.

# Credits #

Krzysztof Blacha
You can contact me at krzysztof.blacha@gmail.com