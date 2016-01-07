[![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/Download.png)](http://www.rorybecker.co.uk/DevExpress/Community/Plugins/Refactor_SortNamespacesByScope)      [![](http://dxcorecommunityplugins.googlecode.com/svn/trunk/Common/Graphics/InstallHelp.png)](http://code.google.com/p/dxcorecommunityplugins/wiki/InstallInstructions)
# Introduction #

MS `StyleCop` has a rule that requires System references to come before all other namespace references in a code file.

This plugin takes that suggestion one step further by sorting your namespace references alphabetically within the following groups:
  * System.`*`
  * Microsoft.`*`
  * {namespaces from referenced assemblies}
  * {namespaces from within the solution}

These last two are heuristically sorted based on whether or not the namespace starts with the name of a project in the current solution.

# Known Issues #

  * Namespace Aliases currently get sorted by scope of the aliased namespace.  MS StyleCop, out of the box, asks that aliases get listed last.

# Example #

So the following set of references:
  * namespace My.Project.Namespace;
  * namespace Microsoft.Web.Mvc;
  * namespace Xunit;
  * namespace System;
  * namespace Moq;
  * namespace System.Collections.Generic;

Would get rewritten as:
  * namespace System;
  * namespace System.Collections.Generic;
  * namespace Microsoft.Web.Mvc;
  * namespace Moq;
  * namespace Xunit;
  * namespace My.Project.Namespace;