[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/CR_CreatePartialClasses/)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Feedback.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/Feedback)

# Introduction #

This plugin allows to create partial classes for all classes found in active source file. Currently the plugin was designed and tested to work under C#.

## Sample ##

This plugins allows to use source code like:

```
using System;

namespace SampleProject
{
    class A { }

    partial class B { }

    public partial class C { }
}
```

and use it to generate this:

```
namespace SampleProject
{
	using System;

	/// <summary>
	/// Your comment here
	/// </summary>
	public partial class B
	{
	} // B

	/// <summary>
	/// Your comment here
	/// </summary>
	public partial class C
	{
	} // C
} // SampleProject

```

# History #

  * 25.03.2012 - First version released to public.

# Credits #

Krzysztof Blacha
You can contact me at krzysztof.blacha@gmail.com